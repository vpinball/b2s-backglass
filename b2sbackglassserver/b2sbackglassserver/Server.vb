Imports System
Imports System.Text
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

<ProgId("B2S.Server"), ComClass(Server.ClassID, Server.InterfaceID, Server.EventsID)> _
Public Class Server

    Implements IDisposable

    Private Declare Function IsWindow Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean
    Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, Msg As UInteger, wParam As Integer, lParam As Integer) As Integer
    Public Const WM_SYSCOMMAND As Integer = &H112
    Public Const SC_CLOSE As Integer = &HF060

    Private Const exeName As String = "B2SBackglassServerEXE.exe"

    Private formBackglass As formBackglass = Nothing
    Private isBackglassKilled As Boolean = False

    Private timer As Windows.Forms.Timer = Nothing

    Private process As Process = Nothing

    Private tableHandle As Integer = 0
    Private tableCount As Integer = 0
    Private tableReset As Boolean = False

    Private isChangedLampsCalled As Boolean = False
    Private isChangedSolenoidsCalled As Boolean = False
    Private isChangedGIStringsCalled As Boolean = False
    Private isChangedLEDsCalled As Boolean = False

    Private errorlog As Log = Nothing


#Region "COM GUIDs"
    ' GUIDs provide the COM identity for this class 
    Public Const ClassID As String = "09e233a3-cc79-457a-b49e-f637588891e5"
    Public Const InterfaceID As String = "5693c68c-5834-466d-aaac-a86922076efd"
    Public Const EventsID As String = "a48a5c0a-656c-4253-9f33-2426cc9c87b7"
#End Region


#Region "IDisposable support"

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then

            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
            If B2SData.IsHyperpinRunning Then
                Try
                    Me.Stop()
                Catch
                End Try
            End If

        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region


#Region "constructor and end timer"

    Public Sub New()

        ' mabye create the base registry key
        If Registry.CurrentUser.OpenSubKey("Software") Is Nothing Then Registry.CurrentUser.CreateSubKey("Software")
        If Registry.CurrentUser.OpenSubKey("Software\B2S") Is Nothing Then Registry.CurrentUser.CreateSubKey("Software\B2S")
        If Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME") Is Nothing Then Registry.CurrentUser.CreateSubKey("Software\B2S\VPinMAME")

        ' remove some registry stuff
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
            regkey.DeleteValue("B2SGameName", False)
            regkey.DeleteValue("B2SB2SName", False)
        End Using

        ' prepare error log
        errorlog = New Log("B2SServerErrorLog")
        errorlog.LogPath = My.Application.Info.DirectoryPath
        errorlog.IsLogOn = True

        ' maybe prepare plugins
        B2SSettings.Load(, True)
        If B2SSettings.ArePluginsOn Then
            B2SSettings.PluginHost = New PluginHost(True)
        End If
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
            regkey.SetValue("Plugins", If(B2SSettings.PluginHost IsNot Nothing, B2SSettings.PluginHost.Plugins.Count, 0))
        End Using

        ' config main timer
        timer = New Windows.Forms.Timer
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Interval = 37

    End Sub

    Private Sub Timer_Tick()

        ' check whether the table is still running
        If tableHandle <> 0 AndAlso Not IsWindow(tableHandle) Then

            Try
                Me.Stop()
            Finally
                Me.Dispose()
            End Try

        Else

            ' maybe reload settings
            If B2SData.IsBackglassStartedAsEXE Then
                Static settingscounter As Integer = 0
                settingscounter += 1

                ' check for set switches
                Static whatsupwithswitches As Boolean = True
                Static look4switches As Boolean = False
                If whatsupwithswitches Then
                    Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
                        Dim switch As Integer = 0
                        If Not look4switches Then
                            switch = CInt(regkey.GetValue("B2SSetSwitch", "-1"))
                            If switch > -1 Then
                                whatsupwithswitches = (switch = 1)
                                look4switches = (switch = 1)
                                regkey.DeleteValue("B2SSetSwitch", False)
                            End If
                        End If
                        If look4switches Then
                            For i As Integer = 1 To 2
                                switch = CInt(regkey.GetValue("B2SSetSwitch" & i.ToString(), "0"))
                                If switch > 0 Then
                                    B2SAnimation.SetSwitch(switch)
                                    regkey.DeleteValue("B2SSetSwitch" & i.ToString(), False)
                                End If
                            Next
                        End If
                    End Using
                End If

                ' maybe reload settings
                If settingscounter >= 200 Then
                    settingscounter = 0
                    Dim reloadSettings As Boolean = (Registry.CurrentUser.OpenSubKey("Software\B2S").GetValue("B2SReloadSettings", 0) = 1)
                    If reloadSettings Then
                        Registry.CurrentUser.OpenSubKey("Software\B2S", True).SetValue("B2SReloadSettings", 0)
                        B2SSettings.Load(False)
                    End If
                End If

                ' maybe open plugins window
                If (settingscounter Mod 10) = 0 AndAlso B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost IsNot Nothing AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                    Dim openPluginWindow As Boolean = (Registry.CurrentUser.OpenSubKey("Software\B2S").GetValue("PluginsOpenDialog", 0) = 1)
                    If openPluginWindow Then
                        Dim pluginScreenBounds As System.Drawing.Rectangle = Nothing
                        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
                            Dim screensettings As String = regkey.GetValue("PluginsScreen").ToString
                            If Not String.IsNullOrEmpty(screensettings) Then
                                Dim tmp As String() = regkey.GetValue("PluginsScreen").ToString.Split(",")
                                If tmp.Length >= 4 Then
                                    pluginScreenBounds = New System.Drawing.Rectangle(CInt(tmp(0)), CInt(tmp(1)), CInt(tmp(2)), CInt(tmp(3)))
                                End If
                            End If
                            regkey.DeleteValue("PluginsScreen", False)
                            regkey.DeleteValue("PluginsOpenDialog", False)
                        End Using
                        B2SSettings.PluginHost.ShowPluginWindow(, pluginScreenBounds)
                    End If
                End If
            End If

            ' have a look for important pollings
            Static counter As Integer = 0
            Static callLamps As Boolean = False
            Static callSolenoids As Boolean = False
            Static callGIStrings As Boolean = False
            Static callLEDs As Boolean = False
            If counter <= 25 Then
                counter += 1
                callLamps = Not isChangedLampsCalled AndAlso (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseRomLamps OrElse B2SData.UseAnimationLamps)
                callSolenoids = Not isChangedSolenoidsCalled AndAlso (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseRomSolenoids OrElse B2SData.UseAnimationSolenoids)
                callGIStrings = Not isChangedGIStringsCalled AndAlso (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseRomGIStrings OrElse B2SData.UseAnimationGIStrings)
                callLEDs = Not isChangedLEDsCalled AndAlso (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels)
                CheckTableHandle()
            Else
                If B2SSettings.IsROMControlled Then
                    If callLamps Then Dim chg As Object = ChangedLamps()
                    If callSolenoids Then Dim chg As Object = ChangedSolenoids()
                    If callGIStrings Then Dim chg As Object = ChangedGIStrings()
                    If callLEDs Then Dim chg As Object = ChangedLEDs(&HFFFFFFFF, &HFFFFFFFF)
                End If
            End If

            ' maybe the table is reseted so reset some stuff
            If tableReset Then
                tableCount = 0
                counter = 0
                callLamps = False
                callSolenoids = False
                callGIStrings = False
                callLEDs = False
                isChangedLampsCalled = False
                isChangedSolenoidsCalled = False
                isChangedGIStringsCalled = False
                isChangedLEDsCalled = False
                'B2SData.IsBackglassStartedAsEXE = False
                tableReset = False
            End If

            End If

    End Sub

#End Region


#Region "Visual PinMAME COM object"

    Private ReadOnly Property VPinMAME() As Object
        Get
            Return B2SData.VPinMAME
        End Get
    End Property

#End Region


#Region "Visual PinMAME control and some general properties"

    Public ReadOnly Property B2SServerVersion() As String
        Get
            Return B2SSettings.DirectB2SVersion
        End Get
    End Property

    Public ReadOnly Property B2SServerDirectory() As String
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly().Location
        End Get
    End Property

    Public Property GameName() As String
        Get
            Return VPinMAME.GameName
        End Get
        Set(ByVal value As String)
            VPinMAME.GameName = value
            B2SSettings.GameName = value
            B2SSettings.B2SName = String.Empty
        End Set
    End Property
    Public Property B2SName() As String
        Get
            Return B2SSettings.B2SName
        End Get
        Set(ByVal value As String)
            B2SSettings.B2SName = value.Replace(" ", "")
            B2SSettings.GameName = String.Empty
        End Set
    End Property

    Public Property TableName() As String
        Get
            Return B2SData.TableFileName
        End Get
        Set(ByVal value As String)
            B2SData.TableFileName = value
        End Set
    End Property
    Public WriteOnly Property WorkingDir() As String
        Set(value As String)
            IO.Directory.SetCurrentDirectory(value)
            B2SData.TestMode = True
        End Set
    End Property
    Public Sub SetPath(ByVal path As String)
        IO.Directory.SetCurrentDirectory(path)
    End Sub
    Public ReadOnly Property InstallDir() As String
        Get
            Return VPinMAME.InstallDir
        End Get
    End Property

    Public ReadOnly Property Games(ByVal gamename As String) As Object
        Get
            Return VPinMAME.Games(gamename)
        End Get
    End Property

    Public ReadOnly Property Settings() As Object
        Get
            Return VPinMAME.Settings
        End Get
    End Property

    Public ReadOnly Property Running() As Boolean
        Get
            Try
                Return VPinMAME.Running
            Catch ex As Exception
                errorlog.WriteLogEntry(DateTime.Now & ": Get Running ('" & ex.Message & "')")
                Return False
            End Try
        End Get
    End Property

    Public Property Pause() As Boolean
        Get
            Try
                Return VPinMAME.Pause
            Catch ex As Exception
                errorlog.WriteLogEntry(DateTime.Now & ": Get Pause ('" & ex.Message & "')")
                Return False
            End Try
        End Get
        Set(ByVal value As Boolean)
            Try
                VPinMAME.Pause = value
                If B2SSettings.ArePluginsOn Then
                    If value Then
                        B2SSettings.PluginHost.PinMamePause()
                    Else
                        B2SSettings.PluginHost.PinMameContinue()
                    End If
                End If
            Catch ex As Exception
                errorlog.WriteLogEntry(DateTime.Now & ": Set Pause ('" & ex.Message & "')")
            End Try
        End Set
    End Property

    Public ReadOnly Property Version() As String
        Get
            Return VPinMAME.Version
        End Get
    End Property

    Public Sub Run(Optional ByVal handle As Object = 0)

        ' startup
        tableHandle = CInt(handle)
        Startup()

        ' maybe initialize plugin stuff
        If B2SSettings.ArePluginsOn Then
            B2SSettings.PluginHost.PluginInit(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), String.Format("{0}.vpt", B2SData.TableFileName)), If(Not String.IsNullOrEmpty(B2SName), B2SName, GameName))
        End If

        ' show the main form
        ShowBackglassForm()

        If B2SSettings.IsROMControlled Then

            ' run VPM
            VPinMAME.Run(handle)

            ' maybe run plugins
            If B2SSettings.ArePluginsOn Then
                B2SSettings.PluginHost.PinMameRun()
                'Plugins.ShowPluginWindow()  'only for testing
            End If

        End If

        ' maybe set to topmost because of test mode
        If B2SData.TestMode AndAlso B2SData.IsBackglassRunning AndAlso formBackglass IsNot Nothing Then
            formBackglass.TopMost = True
        End If

    End Sub

    Public Sub [Stop]()

        Try
            Try
                timer.Stop()
                HideBackglassForm()
            Catch
            End Try

            Try
                B2SData.Stop()
            Catch
            Finally
                KillBackglassForm()
            End Try
        Catch
        End Try

        If B2SSettings.ArePluginsOn Then
            Try
                B2SSettings.PluginHost.PinMameStop()
                B2SSettings.PluginHost.PluginFinish()
                B2SSettings.PluginHost.ClosePluginWindow()
            Catch
            End Try
        End If

    End Sub

    Public Property LaunchBackglass() As Boolean
        Get
            Return B2SData.LaunchBackglass
        End Get
        Set(ByVal value As Boolean)
            B2SData.LaunchBackglass = value
        End Set
    End Property

#End Region


#Region "customization"

    Public Property SplashInfoLine() As String
        Get
            Return VPinMAME.SplashInfoLine
        End Get
        Set(ByVal value As String)
            VPinMAME.SplashInfoLine = value
        End Set
    End Property

    Public Property ShowFrame() As Boolean
        Get
            Return VPinMAME.ShowFrame
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.ShowFrame = value
        End Set
    End Property
    Public Property ShowTitle() As Boolean
        Get
            Return VPinMAME.ShowTitle
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.ShowTitle = value
        End Set
    End Property
    Public Property ShowDMDOnly() As Boolean
        Get
            Return VPinMAME.ShowDMDOnly
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.ShowDMDOnly = value
        End Set
    End Property
    Public Property ShowPinDMD() As Boolean
        Get
            Return VPinMAME.ShowPinDMD
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.ShowPinDMD = value
        End Set
    End Property

    Public Property LockDisplay() As Boolean
        Get
            Return VPinMAME.LockDisplay
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.LockDisplay = value
        End Set
    End Property

    Public Property DoubleSize() As Boolean
        Get
            Return VPinMAME.DoubleSize
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.DoubleSize = value
        End Set
    End Property

    Private _hidden As Boolean
    Public Property Hidden() As Boolean
        Get
            Return _hidden
        End Get
        Set(ByVal value As Boolean)
            _hidden = value
            If B2SSettings.HideDMD <> Windows.Forms.CheckState.Indeterminate Then
                _hidden = (B2SSettings.HideDMD = Windows.Forms.CheckState.Checked)
            End If
            VPinMAME.hidden = _hidden 'If(B2SData.IsBackglassStartedAsEXE, value, True)
        End Set
    End Property

    Public Sub SetDisplayPosition(ByVal x As Object, ByVal y As Object, Optional ByVal handle As Object = Nothing)
        If handle <> Nothing Then
            VPinMAME.SetDisplayPosition(x, y, handle)
        Else
            VPinMAME.SetDisplayPosition(x, y)
        End If
    End Sub

    Public Sub ShowOptsDialog(ByVal handle As Object)
        VPinMAME.ShowOptsDialog(handle)
    End Sub
    Public Sub ShowPathesDialog(ByVal handle As Object)
        VPinMAME.ShowPathesDialog(handle)
    End Sub
    Public Sub ShowAboutDialog(ByVal handle As Object)
        VPinMAME.ShowAboutDialog(handle)
    End Sub

    Public Sub CheckROMS(ByVal showoptions As Object, ByVal handle As Object)
        VPinMAME.CheckROMS(showoptions, handle)
    End Sub

#End Region


#Region "game settings"

    Public Property HandleKeyboard() As Boolean
        Get
            Return VPinMAME.HandleKeyboard
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.HandleKeyboard = value
        End Set
    End Property

    Public Property HandleMechanics() As Int16
        Get
            Return VPinMAME.HandleMechanics
        End Get
        Set(ByVal value As Int16)
            VPinMAME.HandleMechanics = value
        End Set
    End Property

#End Region


#Region "polling functions"

    Private statelogChangedLamps As Log = New Log("LampsState")
    Private statelogChangedSolenoids As Log = New Log("SolenoidsState")
    Private statelogChangedGIStrings As Log = New Log("GIStringsState")
    Private statelogChangedLEDs As Log = New Log("LEDState")

    'Private timelogChangedLamps As Log = New Log("Lamps")
    'Private timelogChangedSolenoids As Log = New Log("Solenoids")

    'Private statChangedLamps As Statistics = New Statistics(timelogChangedLamps)
    'Private statChangedSolenoids As Statistics = New Statistics(timelogChangedSolenoids)

    Public ReadOnly Property ChangedLamps() As Object
        Get
            Try
                isChangedLampsCalled = True
                Dim chg As Object = VPinMAME.ChangedLamps()
                If B2SData.GetLampsData() Then
                    'If B2SData.IsBackglassRunning AndAlso
                    '    (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseRomLamps OrElse B2SData.UseAnimationLamps OrElse B2SSettings.IsLampsStateLogOn OrElse B2SData.TestMode OrElse B2SStatistics.LogStatistics) AndAlso
                    '    Not B2SSettings.AllOff AndAlso Not B2SSettings.LampsOff Then
                    CheckLamps(DirectCast(chg, Object(,)))
                End If
                If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                    B2SSettings.PluginHost.DataReceive(Convert.ToChar("L"), chg)
                End If
                Return chg
            Catch ex As Exception
                errorlog.WriteLogEntry(DateTime.Now & ": ChangedLamps ('" & ex.Message & "')")
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property ChangedSolenoids() As Object
        Get
            Try
                isChangedSolenoidsCalled = True
                Dim chg As Object = VPinMAME.ChangedSolenoids()
                If B2SData.GetSolenoidsData() Then
                    'If B2SData.IsBackglassRunning AndAlso
                    '    (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseRomSolenoids OrElse B2SData.UseAnimationSolenoids OrElse B2SSettings.IsSolenoidsStateLogOn OrElse B2SData.TestMode OrElse B2SStatistics.LogStatistics) AndAlso
                    '    Not B2SSettings.AllOff AndAlso Not B2SSettings.SolenoidsOff Then
                    CheckSolenoids(DirectCast(chg, Object(,)))
                End If
                If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                    B2SSettings.PluginHost.DataReceive(Convert.ToChar("S"), chg)
                End If
                Return chg
            Catch ex As Exception
                errorlog.WriteLogEntry(DateTime.Now & ": ChangedSolenoids ('" & ex.Message & "')")
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property ChangedGIStrings() As Object
        Get
            Try
                isChangedGIStringsCalled = True
                Dim chg As Object = VPinMAME.ChangedGIStrings()
                If B2SData.GetGIStringsData() Then
                    'If B2SData.IsBackglassRunning AndAlso
                    '    (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseRomGIStrings OrElse B2SData.UseAnimationGIStrings OrElse B2SSettings.IsGIStringsStateLogOn OrElse B2SData.TestMode OrElse B2SStatistics.LogStatistics) AndAlso
                    '    Not B2SSettings.AllOff AndAlso Not B2SSettings.GIStringsOff Then
                    CheckGIStrings(DirectCast(chg, Object(,)))
                End If
                If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                    B2SSettings.PluginHost.DataReceive(Convert.ToChar("G"), chg)
                End If
                Return chg
            Catch ex As Exception
                errorlog.WriteLogEntry(DateTime.Now & ": ChangedGIStrings ('" & ex.Message & "')")
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property ChangedLEDs(ByVal mask2 As Object, ByVal mask1 As Object) As Object
        Get
            Try
                isChangedLEDsCalled = True
                Dim chg As Object = VPinMAME.ChangedLEDs(mask2, mask1) ' (&HFFFFFFFF, &HFFFFFFFF) 
                If B2SData.GetLEDsData() Then
                    'If B2SData.IsBackglassRunning AndAlso
                    '    (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels OrElse B2SSettings.IsLEDsStateLogOn) AndAlso
                    '    Not B2SSettings.AllOff AndAlso Not B2SSettings.LEDsOff Then
                    CheckLEDs(DirectCast(chg, Object(,)))
                End If
                If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                    B2SSettings.PluginHost.DataReceive(Convert.ToChar("D"), chg)
                End If
                Return chg
            Catch ex As Exception
                errorlog.WriteLogEntry(DateTime.Now & ": ChangedLEDs ('" & ex.Message & "')")
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property NewSoundCommands() As Object
        Get
            Try
                Dim chg As Object = VPinMAME.NewSoundCommands()
                Return chg
            Catch ex As Exception
                errorlog.WriteLogEntry(DateTime.Now & ": NewSoundCommands ('" & ex.Message & "')")
                Return Nothing
            End Try
        End Get
    End Property

#Region "private polling functions"

    Private isVisibleStateSet As Boolean = False
    Private lastTopVisible As Boolean = False
    Private lastSecondVisible As Boolean = False

    Private lastRandomStartedAnimation As String = String.Empty

    Private collectLampsData As B2SCollectData = New B2SCollectData(B2SSettings.LampsSkipFrames)
    Private collectSolenoidsData As B2SCollectData = New B2SCollectData(B2SSettings.SolenoidsSkipFrames)
    Private collectGIStringsData As B2SCollectData = New B2SCollectData(B2SSettings.GIStringsSkipFrames)
    Private collectLEDsData As B2SCollectData = New B2SCollectData(B2SSettings.LEDsSkipFrames)

    Private sb As StringBuilder = New StringBuilder
    Private sb2Log As StringBuilder = New StringBuilder

    Private Sub CheckLamps(ByVal lamps As Object(,))

        statelogChangedLamps.IsLogOn = B2SSettings.IsLampsStateLogOn

        If lamps IsNot Nothing AndAlso IsArray(lamps) Then

            If B2SData.TestMode Then
                sb2Log.Length = 0
            End If

            ' get thru all lamp info
            For i As Integer = 0 To lamps.GetUpperBound(0)

                ' get lamp data
                Dim lampid As Integer = CInt(lamps(i, 0))
                Dim lampstate As Integer = CInt(lamps(i, 1))

                ' maybe write log
                If statelogChangedLamps.IsLogOn Then
                    statelogChangedLamps.WriteLogEntry(DateTime.Now & ": Collecting data (" & (lamps.GetUpperBound(0) + 1) & "): " & lampid & " - " & lampstate)
                End If
                If B2SData.TestMode Then
                    sb2Log.Append("L,")
                    sb2Log.Append(lampid)
                    sb2Log.Append(",")
                    sb2Log.Append(lampstate)
                    sb2Log.Append(";")
                End If

                ' maybe log statistics
                If Not B2SStatistics.LogStatistics Then

                    ' check whether there is need to do something
                    If B2SData.IsBackglassStartedAsEXE Then

                        ' collect all lamp data
                        collectLampsData.Add(lampid, New B2SCollectData.CollectData(lampstate, B2SCollectData.eCollectedDataType.Standard, True))

                    ElseIf B2SData.UseRomLamps OrElse B2SData.UseAnimationLamps Then

                        ' collect illumination data
                        If formBackglass.TopRomIDType = B2SBaseBox.eRomIDType.Lamp AndAlso formBackglass.TopRomID = lampid Then
                            collectLampsData.Add(lampid, New B2SCollectData.CollectData(lampstate, B2SCollectData.eCollectedDataType.TopImage))
                        ElseIf formBackglass.SecondRomIDType = B2SBaseBox.eRomIDType.Lamp AndAlso formBackglass.SecondRomID = lampid Then
                            collectLampsData.Add(lampid, New B2SCollectData.CollectData(lampstate, B2SCollectData.eCollectedDataType.SecondImage))
                        End If
                        If B2SData.UsedRomLampIDs.ContainsKey(lampid) Then
                            collectLampsData.Add(lampid, New B2SCollectData.CollectData(lampstate, B2SCollectData.eCollectedDataType.Standard))
                        End If

                        ' collect animation data
                        If B2SData.UsedAnimationLampIDs.ContainsKey(lampid) OrElse B2SData.UsedRandomAnimationLampIDs.ContainsKey(lampid) Then
                            collectLampsData.Add(lampid, New B2SCollectData.CollectData(lampstate, B2SCollectData.eCollectedDataType.Animation))
                        End If

                    End If

                Else

                    If Not B2SStatistics.LampsStats.ContainsKey(lampid) Then B2SStatistics.LampsStats.Add(lampid, New B2SStatistics.StatsCollection)
                    B2SStatistics.LampsStats(lampid).Add(lampstate)

                End If

            Next

            If B2SData.TestMode Then
                Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
                    regkey.SetValue("Info", regkey.GetValue("Info", String.Empty) & sb2Log.ToString())
                End Using
            End If

        End If

        ' maybe log statistics
        If Not B2SStatistics.LogStatistics Then

            ' one collection loop is done
            collectLampsData.DataAdded()

            ' maybe show the collected data
            If collectLampsData.ShowData() Then

                If B2SData.IsBackglassStartedAsEXE Then
                    ' get current registry infos
                    sb.Length = 0
                    sb.Append(Registry.CurrentUser.OpenSubKey("Software\B2S").GetValue("B2SLamps", New String("0", 401)).ToString())
                End If

                For Each lampdata As KeyValuePair(Of Integer, B2SCollectData.CollectData) In collectLampsData

                    ' get lamp data
                    Dim lampid As Integer = lampdata.Key
                    Dim lampstate As Integer = lampdata.Value.State
                    Dim datatypes As Integer = lampdata.Value.Types

                    ' maybe write log
                    If statelogChangedLamps.IsLogOn Then
                        statelogChangedLamps.WriteLogEntry(DateTime.Now & ": Applying data (" & collectLampsData.Count & ")  : " & lampid & " - " & lampstate & " - (" & datatypes & ")")
                    End If

                    If B2SData.IsBackglassStartedAsEXE Then

                        ' enter new lamp state
                        sb.Remove(lampid, 1)
                        If lampstate <> 0 Then
                            sb.Insert(lampid, "1")
                        Else
                            sb.Insert(lampid, "0")
                        End If

                    Else

                        ' illumination stuff
                        If (datatypes And B2SCollectData.eCollectedDataType.TopImage) <> 0 OrElse (datatypes And B2SCollectData.eCollectedDataType.SecondImage) <> 0 Then
                            Dim topvisible As Boolean = lastTopVisible
                            Dim secondvisible As Boolean = lastSecondVisible
                            If (datatypes And B2SCollectData.eCollectedDataType.TopImage) <> 0 Then
                                topvisible = (lampstate <> 0)
                                If formBackglass.TopRomInverted Then topvisible = Not topvisible
                            ElseIf (datatypes And B2SCollectData.eCollectedDataType.SecondImage) <> 0 Then
                                secondvisible = (lampstate <> 0)
                                If formBackglass.SecondRomInverted Then topvisible = Not topvisible
                            End If
                            If lastTopVisible <> topvisible OrElse lastSecondVisible <> secondvisible OrElse Not isVisibleStateSet Then
                                B2SData.IsOffImageVisible = False
                                isVisibleStateSet = True
                                lastTopVisible = topvisible
                                lastSecondVisible = secondvisible
                                If topvisible AndAlso secondvisible Then
                                    formBackglass.BackgroundImage = formBackglass.TopAndSecondLightImage
                                ElseIf topvisible Then
                                    formBackglass.BackgroundImage = formBackglass.TopLightImage
                                ElseIf secondvisible Then
                                    formBackglass.BackgroundImage = formBackglass.SecondLightImage
                                Else
                                    formBackglass.BackgroundImage = formBackglass.DarkImage
                                    B2SData.IsOffImageVisible = True
                                End If
                            End If
                        End If
                        If (datatypes And B2SCollectData.eCollectedDataType.Standard) <> 0 Then
                            For Each picbox As B2SPictureBox In B2SData.UsedRomLampIDs(lampid)
                                'If picbox IsNot Nothing Then
                                If picbox IsNot Nothing AndAlso (Not B2SData.UseIlluminationLocks OrElse String.IsNullOrEmpty(picbox.GroupName) OrElse Not B2SData.IlluminationLocks.ContainsKey(picbox.GroupName)) Then
                                    Dim visible As Boolean = (lampstate <> 0)
                                    If picbox.RomInverted Then visible = Not visible
                                    If B2SData.UseRotatingImage AndAlso B2SData.RotatingPictureBox(0) IsNot Nothing AndAlso picbox.Equals(B2SData.RotatingPictureBox(0)) Then
                                        If visible Then
                                            formBackglass.StartRotation()
                                        Else
                                            formBackglass.StopRotation()
                                        End If
                                    Else
                                        picbox.Visible = visible
                                    End If
                                End If
                            Next
                        End If

                        ' animation stuff
                        If (datatypes And B2SCollectData.eCollectedDataType.Animation) <> 0 Then
                            If B2SData.UsedAnimationLampIDs.ContainsKey(lampid) Then
                                For Each animation As B2SData.AnimationInfo In B2SData.UsedAnimationLampIDs(lampid)
                                    Dim start As Boolean = (lampstate <> 0)
                                    If animation.Inverted Then start = Not start
                                    If start Then
                                        formBackglass.StartAnimation(animation.AnimationName)
                                    Else
                                        formBackglass.StopAnimation(animation.AnimationName)
                                    End If
                                Next
                            End If
                            ' random animation start
                            If B2SData.UsedRandomAnimationLampIDs.ContainsKey(lampid) Then
                                Dim start As Boolean = (lampstate <> 0)
                                Dim isrunning As Boolean = False
                                If start Then
                                    For Each matchinganimation As B2SData.AnimationInfo In B2SData.UsedRandomAnimationLampIDs(lampid)
                                        If formBackglass.IsAnimationRunning(matchinganimation.AnimationName) Then
                                            isrunning = True
                                            Exit For
                                        End If
                                    Next
                                End If
                                If start Then
                                    If Not isrunning Then
                                        Dim random As Integer = RandomStarter(B2SData.UsedRandomAnimationLampIDs(lampid).Length)
                                        Dim animation As B2SData.AnimationInfo = B2SData.UsedRandomAnimationLampIDs(lampid)(random)
                                        lastRandomStartedAnimation = animation.AnimationName
                                        formBackglass.StartAnimation(lastRandomStartedAnimation)
                                    End If
                                Else
                                    If Not String.IsNullOrEmpty(lastRandomStartedAnimation) Then
                                        formBackglass.StopAnimation(lastRandomStartedAnimation)
                                        lastRandomStartedAnimation = String.Empty
                                    End If
                                End If
                            End If
                        End If

                    End If

                Next

                If B2SData.IsBackglassStartedAsEXE Then
                    ' write into registry
                    Registry.CurrentUser.OpenSubKey("Software\B2S", True).SetValue("B2SLamps", sb.ToString())
                End If

                ' reset all current data
                collectLampsData.ClearData(B2SSettings.LampsSkipFrames)

            End If

        End If

    End Sub
    Private Sub CheckSolenoids(ByVal solenoids As Object(,))

        statelogChangedSolenoids.IsLogOn = B2SSettings.IsSolenoidsStateLogOn

        'If statelogChangedSolenoids.IsLogOn Then
        '    Static stopwatch As Stopwatch = New Stopwatch()
        '    If Not stopwatch.IsRunning Then stopwatch.Start()
        '    statelogChangedSolenoids.WriteLogEntry(DateTime.Now & " (" & stopwatch.ElapsedMilliseconds & ")")
        '    stopwatch.Restart()
        'End If

        If solenoids IsNot Nothing AndAlso IsArray(solenoids) Then

            If B2SData.TestMode Then
                sb2Log.Length = 0
            End If

            ' get thru all solenoid info
            For i As Integer = 0 To solenoids.GetUpperBound(0)

                ' get solenoid data
                Dim solenoidid As Integer = CInt(solenoids(i, 0))
                Dim solenoidstate As Integer = CInt(solenoids(i, 1))

                ' maybe write log(s)
                If statelogChangedSolenoids.IsLogOn Then
                    statelogChangedSolenoids.WriteLogEntry(DateTime.Now & ": Collecting data (" & (solenoids.GetUpperBound(0) + 1) & "): " & solenoidid & " - " & solenoidstate)
                End If
                If B2SData.TestMode Then
                    sb2Log.Append("S,")
                    sb2Log.Append(solenoidid)
                    sb2Log.Append(",")
                    sb2Log.Append(solenoidstate)
                    sb2Log.Append(";")
                End If

                ' maybe log statistics
                If Not B2SStatistics.LogStatistics Then

                    ' check whether there is need to do something
                    If B2SData.IsBackglassStartedAsEXE Then

                        ' collect all solenoid data
                        collectSolenoidsData.Add(solenoidid, New B2SCollectData.CollectData(solenoidstate, B2SCollectData.eCollectedDataType.Standard, True))

                    ElseIf B2SData.UseRomSolenoids OrElse B2SData.UseAnimationSolenoids Then

                        ' collect illumination data
                        If formBackglass.TopRomIDType = B2SBaseBox.eRomIDType.Solenoid AndAlso formBackglass.TopRomID = solenoidid Then
                            collectSolenoidsData.Add(solenoidid, New B2SCollectData.CollectData(solenoidstate, B2SCollectData.eCollectedDataType.TopImage))
                        ElseIf formBackglass.SecondRomIDType = B2SBaseBox.eRomIDType.Solenoid AndAlso formBackglass.SecondRomID = solenoidid Then
                            collectSolenoidsData.Add(solenoidid, New B2SCollectData.CollectData(solenoidstate, B2SCollectData.eCollectedDataType.SecondImage))
                        End If
                        If B2SData.UsedRomSolenoidIDs.ContainsKey(solenoidid) Then
                            collectSolenoidsData.Add(solenoidid, New B2SCollectData.CollectData(solenoidstate, B2SCollectData.eCollectedDataType.Standard))
                        End If

                        ' collect animation data
                        If B2SData.UsedAnimationSolenoidIDs.ContainsKey(solenoidid) OrElse B2SData.UsedRandomAnimationSolenoidIDs.ContainsKey(solenoidid) Then
                            collectSolenoidsData.Add(solenoidid, New B2SCollectData.CollectData(solenoidstate, B2SCollectData.eCollectedDataType.Animation))
                        End If

                    End If

                Else

                    If Not B2SStatistics.SolenoidsStats.ContainsKey(solenoidid) Then B2SStatistics.SolenoidsStats.Add(solenoidid, New B2SStatistics.StatsCollection)
                    B2SStatistics.SolenoidsStats(solenoidid).Add(solenoidstate)

                End If

            Next

            If B2SData.TestMode Then
                Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
                    regkey.SetValue("Info", regkey.GetValue("Info", String.Empty) & sb2Log.ToString())
                End Using
            End If

        End If

        ' maybe log statistics
        If Not B2SStatistics.LogStatistics Then

            ' one collection loop is done
            collectSolenoidsData.DataAdded()

            ' maybe show the collected data
            If collectSolenoidsData.ShowData() Then

                If B2SData.IsBackglassStartedAsEXE Then
                    ' get current registry infos
                    sb.Length = 0
                    sb.Append(Registry.CurrentUser.OpenSubKey("Software\B2S").GetValue("B2SSolenoids", New String("0", 251)).ToString())
                End If

                For Each solenoiddata As KeyValuePair(Of Integer, B2SCollectData.CollectData) In collectSolenoidsData

                    ' get solenoid data
                    Dim solenoidid As Integer = solenoiddata.Key
                    Dim solenoidstate As Integer = solenoiddata.Value.State
                    Dim datatypes As Integer = solenoiddata.Value.Types

                    ' maybe write log
                    If statelogChangedSolenoids.IsLogOn Then
                        statelogChangedSolenoids.WriteLogEntry(DateTime.Now & ": Applying data (" & collectSolenoidsData.Count & ")  : " & solenoidid & " - " & solenoidstate & " (" & datatypes & ")")
                    End If

                    If B2SData.IsBackglassStartedAsEXE Then

                        ' enter new solenoid state  
                        sb.Remove(solenoidid, 1)
                        If (solenoidstate <> 0) Then
                            sb.Insert(solenoidid, "1")
                        Else
                            sb.Insert(solenoidid, "0")
                        End If

                    Else

                            ' illumination stuff
                            If (datatypes And B2SCollectData.eCollectedDataType.TopImage) <> 0 OrElse (datatypes And B2SCollectData.eCollectedDataType.SecondImage) <> 0 Then
                            Dim topvisible As Boolean = lastTopVisible
                            Dim secondvisible As Boolean = lastSecondVisible
                            If (datatypes And B2SCollectData.eCollectedDataType.TopImage) <> 0 Then
                                topvisible = (solenoidstate <> 0)
                                If formBackglass.TopRomInverted Then topvisible = Not topvisible
                            ElseIf (datatypes And B2SCollectData.eCollectedDataType.SecondImage) <> 0 Then
                                secondvisible = (solenoidstate <> 0)
                                If formBackglass.SecondRomInverted Then topvisible = Not topvisible
                            End If
                            If lastTopVisible <> topvisible OrElse lastSecondVisible <> secondvisible OrElse Not isVisibleStateSet Then
                                B2SData.IsOffImageVisible = False
                                isVisibleStateSet = True
                                lastTopVisible = topvisible
                                lastSecondVisible = secondvisible
                                If topvisible AndAlso secondvisible Then
                                    formBackglass.BackgroundImage = formBackglass.TopAndSecondLightImage
                                ElseIf topvisible Then
                                    formBackglass.BackgroundImage = formBackglass.TopLightImage
                                ElseIf secondvisible Then
                                    formBackglass.BackgroundImage = formBackglass.SecondLightImage
                                Else
                                    formBackglass.BackgroundImage = formBackglass.DarkImage
                                    B2SData.IsOffImageVisible = True
                                End If
                            End If
                        End If
                        If (datatypes And B2SCollectData.eCollectedDataType.Standard) <> 0 Then
                            For Each picbox As B2SPictureBox In B2SData.UsedRomSolenoidIDs(solenoidid)
                                'If picbox IsNot Nothing Then
                                If picbox IsNot Nothing AndAlso (Not B2SData.UseIlluminationLocks OrElse String.IsNullOrEmpty(picbox.GroupName) OrElse Not B2SData.IlluminationLocks.ContainsKey(picbox.GroupName)) Then
                                    Dim visible As Boolean = (solenoidstate <> 0)
                                    If picbox.RomInverted Then visible = Not visible
                                    If B2SData.UseRotatingImage AndAlso B2SData.RotatingPictureBox(0) IsNot Nothing AndAlso picbox.Equals(B2SData.RotatingPictureBox(0)) Then
                                        If visible Then
                                            formBackglass.StartRotation()
                                        Else
                                            formBackglass.StopRotation()
                                        End If
                                    Else
                                        picbox.Visible = visible
                                    End If
                                End If
                            Next
                        End If

                        ' animation stuff
                        If (datatypes And B2SCollectData.eCollectedDataType.Animation) <> 0 Then
                            If B2SData.UsedAnimationSolenoidIDs.ContainsKey(solenoidid) Then
                                For Each animation As B2SData.AnimationInfo In B2SData.UsedAnimationSolenoidIDs(solenoidid)
                                    Dim start As Boolean = (solenoidstate <> 0)
                                    If animation.Inverted Then start = Not start
                                    If start Then
                                        formBackglass.StartAnimation(animation.AnimationName)
                                    Else
                                        formBackglass.StopAnimation(animation.AnimationName)
                                    End If
                                Next
                            End If
                            ' random animation start
                            If B2SData.UsedRandomAnimationSolenoidIDs.ContainsKey(solenoidid) Then
                                Dim start As Boolean = (solenoidstate <> 0)
                                Dim isrunning As Boolean = False
                                If start Then
                                    For Each matchinganimation As B2SData.AnimationInfo In B2SData.UsedRandomAnimationSolenoidIDs(solenoidid)
                                        If formBackglass.IsAnimationRunning(matchinganimation.AnimationName) Then
                                            isrunning = True
                                            Exit For
                                        End If
                                    Next
                                End If
                                If start Then
                                    If Not isrunning Then
                                        Dim random As Integer = RandomStarter(B2SData.UsedRandomAnimationSolenoidIDs(solenoidid).Length)
                                        Dim animation As B2SData.AnimationInfo = B2SData.UsedRandomAnimationSolenoidIDs(solenoidid)(random)
                                        lastRandomStartedAnimation = animation.AnimationName
                                        formBackglass.StartAnimation(lastRandomStartedAnimation)
                                    End If
                                Else
                                    If Not String.IsNullOrEmpty(lastRandomStartedAnimation) Then
                                        formBackglass.StopAnimation(lastRandomStartedAnimation)
                                        lastRandomStartedAnimation = String.Empty
                                    End If
                                End If
                            End If
                        End If

                    End If

                Next

                If B2SData.IsBackglassStartedAsEXE Then
                    ' write into registry
                    Registry.CurrentUser.OpenSubKey("Software\B2S", True).SetValue("B2SSolenoids", sb.ToString())
                End If

                ' reset all current data
                collectSolenoidsData.ClearData(B2SSettings.SolenoidsSkipFrames)

            End If

        End If

    End Sub
    Private Sub CheckGIStrings(ByVal gistrings As Object(,))

        statelogChangedGIStrings.IsLogOn = B2SSettings.IsGIStringsStateLogOn

        If gistrings IsNot Nothing AndAlso IsArray(gistrings) Then

            If B2SData.TestMode Then
                sb2Log.Length = 0
            End If

            ' get thru all gistring info
            For i As Integer = 0 To gistrings.GetUpperBound(0)

                ' get gistring data
                Dim gistringid As Integer = CInt(gistrings(i, 0)) + 1
                Dim gistringstate As Integer = CInt(gistrings(i, 1))

                ' maybe write log(s)
                If statelogChangedGIStrings.IsLogOn Then
                    statelogChangedGIStrings.WriteLogEntry(DateTime.Now & ": Collecting data (" & (gistrings.GetUpperBound(0) + 1) & "): " & gistringid & " - " & gistringstate)
                End If
                If B2SData.TestMode Then
                    sb2Log.Append("G,")
                    sb2Log.Append(gistringid)
                    sb2Log.Append(",")
                    sb2Log.Append(gistringstate)
                    sb2Log.Append(";")
                End If

                ' maybe log statistics
                If Not B2SStatistics.LogStatistics Then

                    ' check whether there is need to do something
                    If B2SData.IsBackglassStartedAsEXE Then

                        ' collect all gistring data
                        collectGIStringsData.Add(gistringid, New B2SCollectData.CollectData(gistringstate, B2SCollectData.eCollectedDataType.Standard))

                    ElseIf B2SData.UseRomGIStrings OrElse B2SData.UseAnimationGIStrings Then

                        ' collect illumination data
                        If formBackglass.TopRomIDType = B2SBaseBox.eRomIDType.GIString AndAlso formBackglass.TopRomID = gistringid Then
                            collectGIStringsData.Add(gistringid, New B2SCollectData.CollectData(gistringstate, B2SCollectData.eCollectedDataType.TopImage))
                        ElseIf formBackglass.SecondRomIDType = B2SBaseBox.eRomIDType.GIString AndAlso formBackglass.SecondRomID = gistringid Then
                            collectGIStringsData.Add(gistringid, New B2SCollectData.CollectData(gistringstate, B2SCollectData.eCollectedDataType.SecondImage))
                        End If
                        If B2SData.UsedRomGIStringIDs.ContainsKey(gistringid) Then
                            collectGIStringsData.Add(gistringid, New B2SCollectData.CollectData(gistringstate, B2SCollectData.eCollectedDataType.Standard))
                        End If

                        ' collect animation data
                        If B2SData.UsedAnimationGIStringIDs.ContainsKey(gistringid) OrElse B2SData.UsedRandomAnimationGIStringIDs.ContainsKey(gistringid) Then
                            collectGIStringsData.Add(gistringid, New B2SCollectData.CollectData(gistringstate, B2SCollectData.eCollectedDataType.Animation))
                        End If

                    End If

                Else

                    If Not B2SStatistics.GIStringsStats.ContainsKey(gistringid) Then B2SStatistics.GIStringsStats.Add(gistringid, New B2SStatistics.StatsCollection)
                    B2SStatistics.GIStringsStats(gistringid).Add(gistringstate)

                End If

            Next

            If B2SData.TestMode Then
                Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
                    regkey.SetValue("Info", regkey.GetValue("Info", String.Empty) & sb2Log.ToString())
                End Using
            End If

        End If

        ' maybe log statistics
        If Not B2SStatistics.LogStatistics Then

            ' one collection loop is done
            collectGIStringsData.DataAdded()

            ' maybe show the collected data
            If collectGIStringsData.ShowData() Then

                If B2SData.IsBackglassStartedAsEXE Then
                    ' get current registry infos
                    sb.Length = 0
                    sb.Append(Registry.CurrentUser.OpenSubKey("Software\B2S").GetValue("B2SGIStrings", New String("0", 251)).ToString())
                End If

                For Each gistringdata As KeyValuePair(Of Integer, B2SCollectData.CollectData) In collectGIStringsData

                    ' get gistring data
                    Dim gistringid As Integer = gistringdata.Key
                    Dim gistringstate As Integer = gistringdata.Value.State
                    Dim datatypes As Integer = gistringdata.Value.Types

                    ' maybe write log
                    If statelogChangedGIStrings.IsLogOn Then
                        statelogChangedGIStrings.WriteLogEntry(DateTime.Now & ": Applying data (" & collectGIStringsData.Count & ")  : " & gistringid & " - " & gistringstate & " (" & datatypes & ")")
                    End If

                    If B2SData.IsBackglassStartedAsEXE Then

                        ' enter new gistring state
                        sb.Remove(gistringid, 1)
                        sb.Insert(gistringid, gistringstate.ToString())

                    Else

                        ' illumination stuff
                        If (datatypes And B2SCollectData.eCollectedDataType.TopImage) <> 0 OrElse (datatypes And B2SCollectData.eCollectedDataType.SecondImage) <> 0 Then
                            Dim topvisible As Boolean = lastTopVisible
                            Dim secondvisible As Boolean = lastSecondVisible
                            If (datatypes And B2SCollectData.eCollectedDataType.TopImage) <> 0 Then
                                topvisible = (gistringstate > 4)
                                If formBackglass.TopRomInverted Then topvisible = Not topvisible
                            ElseIf (datatypes And B2SCollectData.eCollectedDataType.SecondImage) <> 0 Then
                                secondvisible = (gistringstate > 4)
                                If formBackglass.SecondRomInverted Then topvisible = Not topvisible
                            End If
                            If lastTopVisible <> topvisible OrElse lastSecondVisible <> secondvisible OrElse Not isVisibleStateSet Then
                                B2SData.IsOffImageVisible = False
                                isVisibleStateSet = True
                                lastTopVisible = topvisible
                                lastSecondVisible = secondvisible
                                If topvisible AndAlso secondvisible Then
                                    formBackglass.BackgroundImage = formBackglass.TopAndSecondLightImage
                                ElseIf topvisible Then
                                    formBackglass.BackgroundImage = formBackglass.TopLightImage
                                ElseIf secondvisible Then
                                    formBackglass.BackgroundImage = formBackglass.SecondLightImage
                                Else
                                    formBackglass.BackgroundImage = formBackglass.DarkImage
                                    B2SData.IsOffImageVisible = True
                                End If
                            End If
                        End If
                        If (datatypes And B2SCollectData.eCollectedDataType.Standard) <> 0 Then
                            For Each picbox As B2SPictureBox In B2SData.UsedRomGIStringIDs(gistringid)
                                'If picbox IsNot Nothing Then
                                If picbox IsNot Nothing AndAlso (Not B2SData.UseIlluminationLocks OrElse String.IsNullOrEmpty(picbox.GroupName) OrElse Not B2SData.IlluminationLocks.ContainsKey(picbox.GroupName)) Then
                                    Dim visible As Boolean = (gistringstate > 4)
                                    If picbox.RomInverted Then visible = Not visible
                                    If B2SData.UseRotatingImage AndAlso B2SData.RotatingPictureBox(0) IsNot Nothing AndAlso picbox.Equals(B2SData.RotatingPictureBox(0)) Then
                                        If visible Then
                                            formBackglass.StartRotation()
                                        Else
                                            formBackglass.StopRotation()
                                        End If
                                    Else
                                        picbox.Visible = visible
                                    End If
                                End If
                            Next
                        End If

                        ' animation stuff
                        If (datatypes And B2SCollectData.eCollectedDataType.Animation) <> 0 Then
                            If B2SData.UsedAnimationGIStringIDs.ContainsKey(gistringid) Then
                                For Each animation As B2SData.AnimationInfo In B2SData.UsedAnimationGIStringIDs(gistringid)
                                    Dim start As Boolean = (gistringstate > 4)
                                    If animation.Inverted Then start = Not start
                                    If start Then
                                        formBackglass.StartAnimation(animation.AnimationName)
                                    Else
                                        formBackglass.StopAnimation(animation.AnimationName)
                                    End If
                                Next
                            End If
                            ' random animation start
                            If B2SData.UsedRandomAnimationGIStringIDs.ContainsKey(gistringid) Then
                                Dim start As Boolean = (gistringstate > 4)
                                Dim isrunning As Boolean = False
                                If start Then
                                    For Each matchinganimation As B2SData.AnimationInfo In B2SData.UsedRandomAnimationGIStringIDs(gistringid)
                                        If formBackglass.IsAnimationRunning(matchinganimation.AnimationName) Then
                                            isrunning = True
                                            Exit For
                                        End If
                                    Next
                                End If
                                If start Then
                                    If Not isrunning Then
                                        Dim random As Integer = RandomStarter(B2SData.UsedRandomAnimationGIStringIDs(gistringid).Length)
                                        Dim animation As B2SData.AnimationInfo = B2SData.UsedRandomAnimationGIStringIDs(gistringid)(random)
                                        lastRandomStartedAnimation = animation.AnimationName
                                        formBackglass.StartAnimation(lastRandomStartedAnimation)
                                    End If
                                Else
                                    If Not String.IsNullOrEmpty(lastRandomStartedAnimation) Then
                                        formBackglass.StopAnimation(lastRandomStartedAnimation)
                                        lastRandomStartedAnimation = String.Empty
                                    End If
                                End If
                            End If
                        End If

                    End If

                Next

                If B2SData.IsBackglassStartedAsEXE Then
                    ' write into registry
                    Registry.CurrentUser.OpenSubKey("Software\B2S", True).SetValue("B2SGIStrings", sb.ToString())
                End If

                ' reset all current data
                collectGIStringsData.ClearData(B2SSettings.GIStringsSkipFrames)

            End If

        End If

    End Sub
    Private Sub CheckLEDs(ByVal leds As Object(,))

        statelogChangedLEDs.IsLogOn = B2SSettings.IsLEDsStateLogOn

        If leds IsNot Nothing AndAlso IsArray(leds) Then

            ' get thru all changed LEDs
            For i As Integer = 0 To leds.GetUpperBound(0)

                Dim digit As Integer = CInt(leds(i, 0))
                Dim value As Integer = CInt(leds(i, 2))

                ' maybe write log
                If statelogChangedLEDs.IsLogOn Then
                    statelogChangedLEDs.WriteLogEntry(DateTime.Now & ": Collecting data (" & (leds.GetUpperBound(0) + 1) & ")  : " & digit & " - " & value)
                End If

                ' check whether leds are used
                If B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels Then
                    collectLEDsData.Add(digit, New B2SCollectData.CollectData(value, 0))
                End If

            Next

        End If

        ' one collection loop is done
        collectLEDsData.DataAdded()

        ' maybe show the collected data
        If collectLEDsData.ShowData() Then

            Dim useLEDs As Boolean = (B2SData.UseLEDs AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered)
            Dim useLEDDisplays As Boolean = (B2SData.UseLEDDisplays AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7)
            Dim useReels As Boolean = (B2SData.UseReels)

            ' maybe open regkey for writing into registry
            Dim regkey As RegistryKey = Nothing
            If B2SData.IsBackglassStartedAsEXE Then
                regkey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
            End If

            For Each leddata As KeyValuePair(Of Integer, B2SCollectData.CollectData) In collectLEDsData

                Dim digit As Integer = leddata.Key
                Dim value As Integer = leddata.Value.State

                ' maybe write log
                If statelogChangedLEDs.IsLogOn Then
                    statelogChangedLEDs.WriteLogEntry(DateTime.Now & ": Applying data (" & (leds.GetUpperBound(0) + 1) & ")  : " & digit & " - " & value)
                End If

                ' check whether leds or reels are used or the data is stored in the registry
                If B2SData.IsBackglassStartedAsEXE Then

                    ' write new LED state into registry
                    regkey.SetValue("B2SLED" & (digit + 1).ToString(), value)

                Else

                    If useLEDs Then

                        ' rendered LEDs are used
                        Dim ledname As String = "LEDBox" & (digit + 1).ToString()
                        If B2SData.LEDs.ContainsKey(ledname) Then
                            B2SData.LEDs(ledname).Value = value
                        End If

                    End If

                    If useLEDDisplays Then

                        ' Dream 7 displays are used
                        If B2SData.LEDDisplayDigits.ContainsKey(digit) Then
                            With B2SData.LEDDisplayDigits(digit)
                                .LEDDisplay.SetValue(.Digit, value)
                            End With
                        End If

                    End If

                    If useReels Then

                        ' reels are used
                        Dim reelname As String = "ReelBox" & (digit + 1).ToString()
                        If B2SData.Reels.ContainsKey(reelname) Then
                            B2SData.Reels(reelname).Value = value
                        End If

                    End If

                End If

            Next

            ' maybe close opened regkey
            If B2SData.IsBackglassStartedAsEXE Then
                regkey.Close()
            End If

            ' reset all current data
            collectLEDsData.ClearData(B2SSettings.LEDsSkipFrames)

        End If

    End Sub

#End Region

#End Region


#Region "game input/output"

    Public ReadOnly Property Lamp(ByVal number As Object) As Boolean
        Get
            Return VPinMAME.Lamp(number)
        End Get
    End Property

    Public ReadOnly Property Solenoid(ByVal number As Object) As Boolean
        Get
            Return VPinMAME.Solenoid(number)
        End Get
    End Property

    Public ReadOnly Property GIString(ByVal number As Object) As Boolean
        Get
            Return VPinMAME.GIString(number)
        End Get
    End Property

    Public Property Switch(ByVal number As Object) As Boolean
        Get
            Return VPinMAME.Switch(number)
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.Switch(number) = value
            If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                If IsNumeric(number) Then
                    B2SSettings.PluginHost.DataReceive(Convert.ToChar("W"), Convert.ToInt32(number), If(value, 1, 0))
                End If
            End If
        End Set
    End Property

    'Private statelogMech As Log = New Log("GetMech")
    Public Property Mech(ByVal number As Object) As Integer
        Get
            Dim value As Integer = VPinMAME.Mech(number)
            If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                If IsNumeric(number) Then
                    B2SSettings.PluginHost.DataReceive(Convert.ToChar("M"), Convert.ToInt32(number), value)
                End If
            End If
            Return value
        End Get
        Set(ByVal value As Integer)
            VPinMAME.Mech(number) = value
            If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                If IsNumeric(number) Then
                    B2SSettings.PluginHost.DataReceive(Convert.ToChar("M"), Convert.ToInt32(number), value)
                End If
            End If
        End Set
    End Property
    Public ReadOnly Property GetMech(ByVal number As Object) As Object
        Get
            'Return VPinMAME.GetMech(number)
            Dim mech As Object = VPinMAME.GetMech(number)
            If B2SData.IsBackglassRunning AndAlso
                (B2SData.IsBackglassStartedAsEXE OrElse B2SData.UseRomMechs OrElse B2SData.TestMode OrElse B2SStatistics.LogStatistics) AndAlso
                Not B2SSettings.AllOff Then
                CheckGetMech(number, mech)
            End If
            If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
                If IsNumeric(number) AndAlso IsNumeric(mech) Then
                    B2SSettings.PluginHost.DataReceive(Convert.ToChar("N"), Convert.ToInt32(number), Convert.ToInt32(mech))
                End If
            End If
            Return mech
        End Get
    End Property

    Public Property Dip(ByVal number As Object) As Integer
        Get
            Return VPinMAME.Dip(number)
        End Get
        Set(ByVal value As Integer)
            VPinMAME.Dip(number) = value
        End Set
    End Property

    Public Property SolMask(ByVal number As Object) As Integer
        Get
            Return VPinMAME.SolMask(number)
        End Get
        Set(ByVal value As Integer)
            VPinMAME.SolMask(number) = value
        End Set
    End Property

    Public ReadOnly Property RawDmdWidth As Integer
        Get
            Return VPinMAME.RawDmdWidth
        End Get
    End Property

    Public ReadOnly Property RawDmdHeight As Integer
        Get
            Return VPinMAME.RawDmdHeight
        End Get
    End Property

    Public ReadOnly Property RawDmdPixels As Object
        Get
            Return VPinMAME.RawDmdPixels
        End Get
    End Property

    Public ReadOnly Property RawDmdColoredPixels As Object
        Get
            Return VPinMAME.RawDmdColoredPixels
        End Get
    End Property

    Public ReadOnly Property ChangedNVRAM As Object
        Get
            Return VPinMAME.ChangedNVRAM
        End Get
    End Property




#Region "private mech function"

    Private Sub CheckGetMech(ByVal number As Object, ByVal mech As Object)

        'statelogMech.IsLogOn = True
        'statelogMech.WriteLogEntry(number.ToString() & ": " & mech.ToString())

        If IsNumeric(number) AndAlso CInt(number) > 0 AndAlso IsNumeric(mech) Then

            Dim mechid As Integer = CInt(number)
            Dim mechvalue As Integer = CInt(mech)
            ' patch mechvalue for older backglasses
            If String.IsNullOrEmpty(B2SSettings.BackglassFileVersion) Then
                Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S")
                    B2SSettings.BackglassFileVersion = regkey.GetValue("B2SBackglassFileVersion", String.Empty)
                End Using
            End If
            If Not String.IsNullOrEmpty(B2SSettings.BackglassFileVersion) AndAlso B2SSettings.BackglassFileVersion <= "1.1" Then
                mechvalue -= 1
            End If

            If B2SData.IsBackglassStartedAsEXE Then

                If mechid >= 1 AndAlso mechid <= 5 Then
                    Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
                        regkey.SetValue("B2SMechs" & mechid.ToString(), mechvalue)
                    End Using
                End If

            Else

                If B2SData.UsedRomMechIDs.ContainsKey(mechid) Then
                    If B2SData.RotatingPictureBox(mechid) IsNot Nothing AndAlso B2SData.RotatingImages(mechid) IsNot Nothing AndAlso B2SData.RotatingImages(mechid).Count > 0 AndAlso B2SData.RotatingImages(mechid).ContainsKey(mechvalue) Then
                        B2SData.RotatingPictureBox(mechid).BackgroundImage = B2SData.RotatingImages(mechid)(mechvalue)
                        B2SData.RotatingPictureBox(mechid).Visible = True
                    End If
                End If

            End If

        End If

    End Sub

#End Region

#End Region


#Region "non VPinMAME support"

    ' backup method to set data
    Public Sub B2SSetData(ByVal idORname As Object, ByVal value As Object)

        If IsNumeric(value) Then
            If TypeOf idORname Is String Then
                MyB2SSetData(idORname.ToString(), CInt(value))
            ElseIf IsNumeric(idORname) Then
                MyB2SSetData(CInt(idORname), CInt(value))
            End If
        End If
      
    End Sub
    ' pulse the set data
    Public Sub B2SPulseData(ByVal idORname As Object)

        If TypeOf idORname Is String Then
            MyB2SSetData(idORname.ToString(), 1)
            MyB2SSetData(idORname.ToString(), 0)
        ElseIf IsNumeric(idORname) Then
            MyB2SSetData(CInt(idORname), 1)
            MyB2SSetData(CInt(idORname), 0)
        End If

    End Sub

    ' method to set illumination
    Public Sub B2SSetIllumination(ByVal name As Object, ByVal value As Object)

        MyB2SSetData(name.ToString(), CInt(value))

    End Sub

    ' LED method(s)
    Public Sub B2SSetLED(ByVal digit As Object, ByVal valueORtext As Object)

        If TypeOf valueORtext Is String Then
            MyB2SSetLED(CInt(digit), valueORtext.ToString())
        ElseIf IsNumeric(valueORtext) Then
            MyB2SSetLED(CInt(digit), CInt(valueORtext))
        End If

    End Sub
    Public Sub B2SSetLEDDisplay(ByVal display As Object, ByVal text As Object)

        MyB2SSetLEDDisplay(display, text)

    End Sub

    ' reel method(s)
    Public Sub B2SSetReel(ByVal digit As Object, ByVal value As Object)

        MyB2SSetScore(CInt(digit), CInt(value), True)

    End Sub

    ' score: 1-24
    Public Sub B2SSetScore(ByVal display As Object, ByVal value As Object)

        MyB2SSetScore(GetFirstDigitOfDisplay(CInt(display)), CInt(value))

    End Sub
    Public Sub B2SSetScorePlayer(ByVal playerno As Object, ByVal score As Object)

        MyB2SSetScorePlayer(CInt(playerno), CInt(score))

    End Sub
    Public Sub B2SSetScorePlayer1(ByVal score As Object)

        MyB2SSetScorePlayer(1, CInt(score))

    End Sub
    Public Sub B2SSetScorePlayer2(ByVal score As Object)

        MyB2SSetScorePlayer(2, CInt(score))

    End Sub
    Public Sub B2SSetScorePlayer3(ByVal score As Object)

        MyB2SSetScorePlayer(3, CInt(score))

    End Sub
    Public Sub B2SSetScorePlayer4(ByVal score As Object)

        MyB2SSetScorePlayer(4, CInt(score))

    End Sub
    Public Sub B2SSetScorePlayer5(ByVal score As Object)

        MyB2SSetScorePlayer(5, CInt(score))

    End Sub
    Public Sub B2SSetScorePlayer6(ByVal score As Object)

        MyB2SSetScorePlayer(6, CInt(score))

    End Sub
    Public Sub B2SSetScoreDigit(ByVal digit As Object, ByVal value As Object)

        MyB2SSetScore(CInt(digit), CInt(value), False)

    End Sub

    ' score rollover: 25-28
    Public Sub B2SSetScoreRollover(ByVal id As Object, ByVal value As Object)

        MyB2SSetData(CInt(id), CInt(value))

    End Sub
    Public Sub B2SSetScoreRolloverPlayer1(ByVal value As Object)

        MyB2SSetData(25, CInt(value))

    End Sub
    Public Sub B2SSetScoreRolloverPlayer2(ByVal value As Object)

        MyB2SSetData(26, CInt(value))

    End Sub
    Public Sub B2SSetScoreRolloverPlayer3(ByVal value As Object)

        MyB2SSetData(27, CInt(value))

    End Sub
    Public Sub B2SSetScoreRolloverPlayer4(ByVal value As Object)

        MyB2SSetData(28, CInt(value))

    End Sub

    ' credits: 29
    Public Sub B2SSetCredits(ByVal digitORvalue As Object, Optional ByVal value As Object = Nothing)

        If value Is Nothing Then
            MyB2SSetScore(29, CInt(digitORvalue), False)
        Else
            MyB2SSetScore(CInt(digitORvalue), CInt(value), False)
        End If

    End Sub

    ' player up: 30
    Public Sub B2SSetPlayerUp(ByVal idORvalue As Object, Optional ByVal value As Object = Nothing)

        If value Is Nothing Then
            MyB2SSetData(30, CInt(idORvalue))
        Else
            MyB2SSetData(CInt(idORvalue), CInt(value))
        End If

    End Sub

    ' can play: 31
    Public Sub B2SSetCanPlay(ByVal idORvalue As Object, Optional ByVal value As Object = Nothing)

        If value Is Nothing Then
            MyB2SSetData(31, CInt(idORvalue))
        Else
            MyB2SSetData(CInt(idORvalue), CInt(value))
        End If

    End Sub

    ' ball in play: 32
    Public Sub B2SSetBallInPlay(ByVal idORvalue As Object, Optional ByVal value As Object = Nothing)

        If value Is Nothing Then
            MyB2SSetData(32, CInt(idORvalue))
        Else
            MyB2SSetData(CInt(idORvalue), CInt(value))
        End If

    End Sub

    ' tilt: 33
    Public Sub B2SSetTilt(ByVal idORvalue As Object, Optional ByVal value As Object = Nothing)

        If value Is Nothing Then
            MyB2SSetData(33, CInt(idORvalue))
        Else
            MyB2SSetData(CInt(idORvalue), CInt(value))
        End If

    End Sub

    ' match: 34
    Public Sub B2SSetMatch(ByVal idORvalue As Object, Optional ByVal value As Object = Nothing)

        If value Is Nothing Then
            MyB2SSetData(34, CInt(idORvalue))
        Else
            MyB2SSetData(CInt(idORvalue), CInt(value))
        End If

    End Sub

    ' game over: 35
    Public Sub B2SSetGameOver(ByVal idORvalue As Object, Optional ByVal value As Object = Nothing)

        If value Is Nothing Then
            MyB2SSetData(35, CInt(idORvalue))
        Else
            MyB2SSetData(CInt(idORvalue), CInt(value))
        End If

    End Sub

    ' shoot again: 36
    Public Sub B2SSetShootAgain(ByVal idORvalue As Object, Optional ByVal value As Object = Nothing)

        If value Is Nothing Then
            MyB2SSetData(36, CInt(idORvalue))
        Else
            MyB2SSetData(CInt(idORvalue), CInt(value))
        End If

    End Sub

    ' animation stuff
    Public Sub B2SStartAnimation(ByVal animationname As String, Optional ByVal playreverse As Boolean = False)

        MyB2SStartAnimation(animationname, playreverse)

    End Sub
    Public Sub B2SStartAnimationReverse(ByVal animationname As String)

        MyB2SStartAnimation(animationname, True)

    End Sub
    Public Sub B2SStopAnimation(ByVal animationname As String)

        MyB2SStopAnimation(animationname)

    End Sub
    Public Sub B2SStopAllAnimations()

        MyB2SStopAllAnimations()

    End Sub

    Public Function B2SIsAnimationRunning(ByVal animationname As String) As Boolean

        Return MyB2SIsAnimationRunning(animationname)

    End Function

    Public Sub StartAnimation(ByVal animationname As String, Optional ByVal playreverse As Boolean = False)

        MyB2SStartAnimation(animationname, playreverse)

    End Sub
    Public Sub StopAnimation(ByVal animationname As String)

        MyB2SStopAnimation(animationname)

    End Sub

    ' rotation stuff
    Public Sub B2SStartRotation()

        MyB2SStartRotation()

    End Sub
    Public Sub B2SStopRotation()

        MyB2SStopRotation()

    End Sub

    ' score displays visibility
    Public Sub B2SShowScoreDisplays()

        MyB2SShowOrHideScoreDisplays(True)

    End Sub
    Public Sub B2SHideScoreDisplays()

        MyB2SShowOrHideScoreDisplays(False)

    End Sub

    ' sound stuff
    Public Sub B2SStartSound(ByVal soundname As String)

        MyB2SPlaySound(soundname)

    End Sub
    Public Sub B2SPlaySound(ByVal soundname As String)

        MyB2SPlaySound(soundname)

    End Sub
    Public Sub B2SStopSound(ByVal soundname As String)

        MyB2SStopSound(soundname)

    End Sub
    Public Sub B2SMapSound(ByVal digit As Object, ByVal soundname As String)



    End Sub

#Region "private non VPinMAME support"

    Private Sub MyB2SSetData(ByVal id As Integer, ByVal value As Integer)

        If B2SData.IsBackglassRunning Then

            If B2SData.IsBackglassStartedAsEXE Then

                If id <= 250 Then
                    Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
                        ' get current registry infos
                        sb.Length = 0
                        sb.Append(regkey.GetValue("B2SSetData", New String(Chr(0), 251)).ToString())

                        ' enter new lamp state
                        sb.Remove(id, 1)
                        sb.Insert(id, Chr(value))

                        ' write into registry
                        regkey.SetValue("B2SSetData", sb.ToString())
                    End Using
                End If

            Else

                ' illumination stuff
                If (formBackglass.TopRomIDType = B2SBaseBox.eRomIDType.Lamp AndAlso formBackglass.TopRomID = id) OrElse (formBackglass.SecondRomIDType = B2SBaseBox.eRomIDType.Lamp AndAlso formBackglass.SecondRomID = id) Then
                    Dim topvisible As Boolean = lastTopVisible
                    Dim secondvisible As Boolean = lastSecondVisible
                    If (formBackglass.TopRomIDType = B2SBaseBox.eRomIDType.Lamp AndAlso formBackglass.TopRomID = id) Then
                        topvisible = (value <> 0)
                        If formBackglass.TopRomInverted Then topvisible = Not topvisible
                    ElseIf (formBackglass.SecondRomIDType = B2SBaseBox.eRomIDType.Lamp AndAlso formBackglass.SecondRomID = id) <> 0 Then
                        secondvisible = (value <> 0)
                        If formBackglass.SecondRomInverted Then topvisible = Not topvisible
                    End If
                    If lastTopVisible <> topvisible OrElse lastSecondVisible <> secondvisible OrElse Not isVisibleStateSet Then
                        B2SData.IsOffImageVisible = False
                        isVisibleStateSet = True
                        lastTopVisible = topvisible
                        lastSecondVisible = secondvisible
                        If topvisible AndAlso secondvisible Then
                            formBackglass.BackgroundImage = formBackglass.TopAndSecondLightImage
                        ElseIf topvisible Then
                            formBackglass.BackgroundImage = formBackglass.TopLightImage
                        ElseIf secondvisible Then
                            formBackglass.BackgroundImage = formBackglass.SecondLightImage
                        Else
                            formBackglass.BackgroundImage = formBackglass.DarkImage
                            B2SData.IsOffImageVisible = True
                        End If
                    End If
                End If
                If B2SData.UsedRomLampIDs.ContainsKey(id) Then
                    For Each picbox As B2SPictureBox In B2SData.UsedRomLampIDs(id)
                        If picbox IsNot Nothing AndAlso (Not B2SData.UseIlluminationLocks OrElse String.IsNullOrEmpty(picbox.GroupName) OrElse Not B2SData.IlluminationLocks.ContainsKey(picbox.GroupName)) Then
                            If picbox.RomIDValue > 0 Then
                                picbox.Visible = (picbox.RomIDValue = value)
                            Else
                                Dim visible As Boolean = (value <> 0)
                                If picbox.RomInverted Then visible = Not visible
                                If B2SData.UseRotatingImage AndAlso B2SData.RotatingPictureBox(0) IsNot Nothing AndAlso picbox.Equals(B2SData.RotatingPictureBox(0)) Then
                                    If visible Then
                                        formBackglass.StartRotation()
                                    Else
                                        formBackglass.StopRotation()
                                    End If
                                Else
                                    picbox.Visible = visible
                                End If
                            End If
                        End If
                    Next
                End If
                If B2SData.UsedRomReelLampIDs.ContainsKey(id) Then
                    For Each reelbox As B2SReelBox In B2SData.UsedRomReelLampIDs(id)
                        If reelbox IsNot Nothing AndAlso (Not B2SData.UseIlluminationLocks OrElse String.IsNullOrEmpty(reelbox.GroupName) OrElse Not B2SData.IlluminationLocks.ContainsKey(reelbox.GroupName)) Then
                            If reelbox.RomIDValue > 0 Then
                                reelbox.Illuminated = (reelbox.RomIDValue = value)
                            Else
                                Dim illuminated As Boolean = (value <> 0)
                                If reelbox.RomInverted Then illuminated = Not illuminated
                                reelbox.Illuminated = illuminated
                            End If
                        End If
                    Next
                End If

                ' animation stuff
                If B2SData.UsedAnimationLampIDs.ContainsKey(id) Then
                    For Each animation As B2SData.AnimationInfo In B2SData.UsedAnimationLampIDs(id)
                        Dim start As Boolean = (value <> 0)
                        If animation.Inverted Then start = Not start
                        If start Then
                            formBackglass.StartAnimation(animation.AnimationName)
                        Else
                            formBackglass.StopAnimation(animation.AnimationName)
                        End If
                    Next
                End If

            End If

        End If

        If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
            B2SSettings.PluginHost.DataReceive(Convert.ToChar("E"), id, value)
        End If

    End Sub
    Private Sub MyB2SSetData(ByVal groupname As String, ByVal value As Integer)

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
                ' add current groupname to the already existing string
                Dim illugroups As String = regkey.GetValue("B2SIlluGroupsByName", String.Empty)
                regkey.SetValue("B2SIlluGroupsByName", illugroups & Chr(1) & groupname & "=" & value.ToString())
            End Using

        Else

            ' only do the lightning stuff if the group has a name
            If Not String.IsNullOrEmpty(groupname) AndAlso B2SData.IlluminationGroups.ContainsKey(groupname) Then
                ' get all matching picture boxes
                For Each picbox As B2SPictureBox In B2SData.IlluminationGroups(groupname)
                    If picbox.RomIDValue > 0 Then
                        picbox.Visible = (picbox.RomIDValue = value)
                    Else
                        picbox.Visible = (value <> 0)
                    End If
                Next
            End If
        End If

        'If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
        '    B2SSettings.PluginHost.DataReceive("E", Convert.ToString(groupname), Convert.ToInt32(value))
        'End If

    End Sub

    Private Sub MyB2SSetLED(ByVal digit As Integer, ByVal value As Object)

        If Not B2SData.IsBackglassRunning Then Return

        Dim useLEDs As Boolean = (B2SData.LEDs.ContainsKey("LEDBox" & digit.ToString()) AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered)
        Dim useLEDDisplays As Boolean = (B2SData.LEDDisplayDigits.ContainsKey(digit - 1) AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7)

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
                regkey.SetValue("B2SLED" & digit.ToString(), value)
            End Using

        ElseIf useLEDs Then

            ' rendered LEDs are used
            Dim ledname As String = "LEDBox" & digit.ToString()
            If B2SData.LEDs.ContainsKey(ledname) Then
                B2SData.LEDs(ledname).Value = value
            End If

        ElseIf useLEDDisplays Then

            ' Dream 7 displays are used
            If B2SData.LEDDisplayDigits.ContainsKey(digit - 1) Then
                With B2SData.LEDDisplayDigits(digit - 1)
                    .LEDDisplay.SetValue(.Digit, value)
                End With
            End If

        End If

        'If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
        'B2SSettings.PluginHost.DataReceive("C", Convert.ToInt32(digit), Convert.ToInt32(value))
        'End If

    End Sub
    Private Sub MyB2SSetLEDDisplay(ByVal display As Object, ByVal text As Object)

        If Not B2SData.IsBackglassRunning Then Return

        Dim digit As Integer = GetFirstDigitOfDisplay(display)

        Dim useLEDs As Boolean = (B2SData.LEDs.ContainsKey("LEDBox" & digit.ToString()) AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered)
        Dim useLEDDisplays As Boolean = (B2SData.LEDDisplayDigits.ContainsKey(digit - 1) AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7)

        If B2SData.IsBackglassStartedAsEXE Then

            ' TODO

        ElseIf useLEDs Then

            ' TODO

        ElseIf useLEDDisplays Then

            If B2SData.LEDDisplayDigits.ContainsKey(digit) Then
                With B2SData.LEDDisplayDigits(digit)
                    .LEDDisplay.Text = text
                End With
            End If

        End If

    End Sub

    Private Function GetFirstDigitOfDisplay(ByVal display As Integer) As Integer

        Dim ret As Integer = 0
        If B2SData.IsBackglassStartedAsEXE Then
            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S")
                Dim info As String = regkey.GetValue("B2SScoreDisplay" & display.ToString(), String.Empty)
                If IsNumeric(info) Then
                    ret = CInt(info)
                End If
            End Using
        Else
            For Each reel As KeyValuePair(Of String, B2SReelBox) In B2SData.Reels
                If reel.Value.DisplayID = display Then
                    ret = reel.Value.StartDigit
                    Exit For
                End If
            Next
            If ret = 0 Then
                For Each led As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
                    If led.Value.DisplayID = display Then
                        ret = led.Value.StartDigit
                        Exit For
                    End If
                Next
            End If
        End If
        Return ret

    End Function

    Private Sub MyB2SSetScore(ByVal digit As Integer,
                              ByVal value As Object,
                              ByVal AnimateReelChange As Boolean,
                              Optional ByVal useLEDs As Boolean = False,
                              Optional ByVal useLEDDisplays As Boolean = False,
                              Optional ByVal useReels As Boolean = False,
                              Optional ByVal reeltype As Integer = 0,
                              Optional ByVal ledtype As B2SLED.eLEDType = B2SLED.eLEDType.Undefined)

        If B2SData.IsBackglassRunning Then

            If digit > 0 Then

                If Not B2SData.IsBackglassStartedAsEXE AndAlso Not useLEDs AndAlso Not useLEDDisplays AndAlso Not useReels Then
                    useLEDs = (B2SData.LEDs.ContainsKey("LEDBox" & digit.ToString()) AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered)
                    useLEDDisplays = (B2SData.LEDDisplayDigits.ContainsKey(digit - 1) AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7)
                    useReels = (B2SData.Reels.ContainsKey("ReelBox" & digit.ToString()))
                End If

                If B2SData.IsBackglassStartedAsEXE Then

                    ' set data to registry matching the current score digit type
                    Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
                        regkey.SetValue("B2SLED" & digit.ToString(), If(Not AnimateReelChange, CInt(value), MyB2SConvertValueToLED(value, ledtype)))
                    End Using

                ElseIf useLEDs Then

                    ' rendered LEDs are used
                    Dim ledname As String = "LEDBox" & digit.ToString()
                    B2SData.LEDs(ledname).Text = value.ToString()

                ElseIf useLEDDisplays Then

                    ' Dream 7 displays are used
                    With B2SData.LEDDisplayDigits(digit - 1)
                        .LEDDisplay.SetValue(.Digit, value.ToString())
                    End With

                ElseIf useReels Then

                    ' reels are used
                    Dim reelname As String = "ReelBox" & digit.ToString()
                    B2SData.Reels(reelname).Text(AnimateReelChange) = CInt(value)

                End If

            End If

        End If

        If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
            If IsNumeric(value) Then
                B2SSettings.PluginHost.DataReceive(Convert.ToChar("B"), digit, Convert.ToInt32(value))
            End If
        End If

    End Sub
    Private Sub MyB2SSetScore(ByVal digit As Integer, ByVal score As Integer)

        If B2SData.IsBackglassRunning Then

            If digit > 0 Then

                Dim useLEDs As Boolean = (B2SData.LEDs.ContainsKey("LEDBox" & digit.ToString()) AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered)
                Dim useLEDDisplays As Boolean = (B2SData.LEDDisplayDigits.ContainsKey(digit - 1) AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7)
                Dim useReels As Boolean = (B2SData.Reels.ContainsKey("ReelBox" & digit.ToString()))

                If B2SData.IsBackglassStartedAsEXE Then

                    Dim reeltype As Integer = 0
                    Dim ledtype As B2SLED.eLEDType = B2SLED.eLEDType.Undefined
                    Dim startdigit As Integer = 0
                    Dim digits As Integer = 0
                    Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S")
                        Dim info As String() = regkey.GetValue("B2SScoreDigit" & digit.ToString(), String.Empty).ToString.Split(",")
                        If info.Length = 4 Then
                            reeltype = CInt(info(0))
                            ledtype = CInt(info(1))
                            startdigit = CInt(info(2))
                            digits = CInt(info(3))
                        End If
                    End Using
                    Dim scoreAsString As String = If((reeltype = 3), score.ToString("D" & digits.ToString()), New String(" ", digits - score.ToString().Length) & score.ToString())
                    For i As Integer = startdigit + digits - 1 To startdigit Step -1
                        MyB2SSetScore(i, scoreAsString.Substring(i - startdigit, 1), True, useLEDs, useLEDDisplays, useReels, reeltype, ledtype)
                    Next

                ElseIf useLEDs Then

                    ' check the passed digit
                    Dim led As String = "LEDBox" & digit.ToString()

                    ' get all necessary display data
                    Dim startdigit As Integer = B2SData.LEDs(led).StartDigit
                    Dim player As Integer = B2SData.LEDs(led).ID
                    Dim digits As Integer = B2SData.LEDs(led).Digits
                    Dim scoreAsString As String = New String(" ", digits - score.ToString().Length) & score.ToString()

                    ' set digits
                    For i As Integer = startdigit + digits - 1 To startdigit Step -1
                        B2SData.LEDs("LEDBox" & i.ToString()).Text = scoreAsString.Substring(i - startdigit, 1)
                    Next

                ElseIf useLEDDisplays Then

                    With B2SData.LEDDisplayDigits(digit - 1)

                        ' get all necessary display data
                        Dim digits As Integer = .LEDDisplay.Digits
                        Dim scoreAsString As String = New String(" ", digits - score.ToString().Length) & score.ToString()

                        ' set digits
                        For i As Integer = digits - 1 To 0 Step -1
                            .LEDDisplay.SetValue(i, scoreAsString.Substring(i, 1))
                        Next

                    End With

                ElseIf useReels Then

                    ' get the necessary infos
                    Dim reel As String = "ReelBox" & digit.ToString()
                    Dim id As Integer = B2SData.Reels(reel).DisplayID

                    ' set value
                    If B2SData.ReelDisplays.ContainsKey(id) Then
                        B2SData.ReelDisplays(id).Score = score
                    End If

                End If

            End If

        End If

        If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
            B2SSettings.PluginHost.DataReceive(Convert.ToChar("B"), digit, score)
        End If

    End Sub
    Private Sub MyB2SSetScorePlayer(ByVal playerno As Integer, ByVal score As Integer)

        If B2SData.IsBackglassRunning Then

            If playerno > 0 Then

                If B2SData.IsBackglassStartedAsEXE Then

                    Dim reeltype As Integer() = Nothing
                    Dim ledtype As B2SLED.eLEDType() = Nothing
                    Dim startdigit As Integer() = Nothing
                    Dim digits As Integer() = Nothing
                    Dim digitcount As Integer = 0
                    Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S")
                        Dim scoreinfo As String = regkey.GetValue("B2SScorePlayer" & playerno.ToString(), String.Empty).ToString()
                        If Not String.IsNullOrEmpty(scoreinfo) Then
                            Dim infos As String() = scoreinfo.Split(";")
                            ReDim reeltype(infos.Length - 1)
                            ReDim ledtype(infos.Length - 1)
                            ReDim startdigit(infos.Length - 1)
                            ReDim digits(infos.Length - 1)
                            For i As Integer = 0 To infos.Length - 1
                                Dim info As String() = infos(i).Split(",")
                                If info.Length = 4 Then
                                    reeltype(i) = CInt(info(0))
                                    ledtype(i) = CInt(info(1))
                                    startdigit(i) = CInt(info(2))
                                    digits(i) = CInt(info(3))
                                    digitcount += digits(i)
                                End If
                            Next
                        End If
                    End Using
                    If digitcount > 0 Then
                        Dim scoreAsStringX As String = score.ToString("D" & digitcount.ToString())
                        Dim scoreAsString As String = New String(" ", Math.Max(0, digitcount - score.ToString().Length)) & score.ToString()
                        If scoreAsStringX.Length > digitcount Then
                            scoreAsStringX = scoreAsStringX.Substring(scoreAsStringX.Length - digitcount)
                            scoreAsString = scoreAsString.Substring(scoreAsString.Length - digitcount)
                        End If
                        For j = 0 To reeltype.Length - 1
                            For i As Integer = startdigit(j) + digits(j) - 1 To startdigit(j) Step -1
                                If reeltype(j) = "3" Then
                                    MyB2SSetScore(i, scoreAsStringX.Substring(i - startdigit(j), 1), True, , , , reeltype(j), ledtype(j))
                                Else
                                    MyB2SSetScore(i, scoreAsString.Substring(i - startdigit(j), 1), True, , , , reeltype(j), ledtype(j))
                                End If
                            Next
                            scoreAsStringX = scoreAsStringX.Substring(digits(j))
                            scoreAsString = scoreAsString.Substring(digits(j))
                        Next
                    End If

                Else

                    ' set score to player class
                    If B2SData.Players.ContainsKey(playerno) Then
                        B2SData.Players(playerno).Score = score
                    End If

                End If

            End If

        End If

        If B2SSettings.ArePluginsOn AndAlso B2SSettings.PluginHost.Plugins.Count > 0 Then
            B2SSettings.PluginHost.DataReceive(Convert.ToChar("C"), playerno, score)
        End If

    End Sub

    Private Function MyB2SConvertValueToLED(ByVal realvalue As String, ByVal type As B2SLED.eLEDType) As Integer
        Dim ret As Integer = 0
        Select Case type
            Case B2SLED.eLEDType.LED8, B2SLED.eLEDType.Undefined
                Select Case realvalue
                    Case " " : ret = 0
                    Case "0" : ret = 63
                    Case "1" : ret = 6
                    Case "2" : ret = 91
                    Case "3" : ret = 79
                    Case "4" : ret = 102
                    Case "5" : ret = 109
                    Case "6" : ret = 125
                    Case "7" : ret = 7
                    Case "8" : ret = 127
                    Case "9" : ret = 111
                End Select
            Case B2SLED.eLEDType.LED10
                Select Case realvalue
                    Case " " : ret = 0
                    Case "0" : ret = 63
                    Case "1" : ret = 768
                    Case "2" : ret = 91
                    Case "3" : ret = 79
                    Case "4" : ret = 102
                    Case "5" : ret = 109
                    Case "6" : ret = 124
                    Case "7" : ret = 7
                    Case "8" : ret = 127
                    Case "9" : ret = 103
                End Select
            Case B2SLED.eLEDType.LED14

            Case B2SLED.eLEDType.LED16
                ' not implemented right now
        End Select
        Return ret
    End Function

    Private Sub MyB2SStartAnimation(ByVal animationname As String,
                                    ByVal playreverse As Boolean)

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)

                ' get thru all animations of the registry and update or add the new started animation
                Dim found As Boolean = False
                Dim animationinfo As String = regkey.GetValue("B2SAnimations", String.Empty).ToString()
                Dim currentanimations As String() = animationinfo.Split(Chr(1))
                For i As Integer = 0 To currentanimations.Length - 1
                    If currentanimations(i).StartsWith(animationname & "=") Then
                        found = True
                        currentanimations(i) = animationname & "=" & If(playreverse, "2", "1")
                        Exit For
                    End If
                Next
                If Not found Then
                    ReDim Preserve currentanimations(currentanimations.Length)
                    currentanimations(currentanimations.Length - 1) = animationname & "=" & If(playreverse, "2", "1")
                End If
                regkey.SetValue("B2SAnimations", Join(currentanimations, Chr(1)))

            End Using

        Else

            formBackglass.StartAnimation(animationname, playreverse)

        End If

    End Sub
    Private Sub MyB2SStopAnimation(ByVal animationname As String)

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)

                ' get thru all animations of the registry and update or add the stopped animation
                Dim found As Boolean = False
                Dim animationinfo As String = regkey.GetValue("B2SAnimations", String.Empty).ToString()
                If Not String.IsNullOrEmpty(animationinfo) Then
                    Dim currentanimations As String() = animationinfo.Split(Chr(1))
                    For i As Integer = 0 To currentanimations.Length - 1
                        If currentanimations(i).StartsWith(animationname & "=") Then
                            found = True
                            currentanimations(i) = animationname & "=0"
                            Exit For
                        End If
                    Next
                    If Not found Then
                        ReDim Preserve currentanimations(currentanimations.Length)
                        currentanimations(currentanimations.Length - 1) = animationname & "=0"
                    End If
                    regkey.SetValue("B2SAnimations", Join(currentanimations, Chr(1)))
                End If

            End Using

        Else

            formBackglass.StopAnimation(animationname)

        End If

    End Sub
    Private Sub MyB2SStopAllAnimations()

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)

                ' get thru all animations of the registry and update all animation to stop them
                Dim found As Boolean = False
                Dim animationinfo As String = regkey.GetValue("B2SAnimations", String.Empty).ToString()
                If Not String.IsNullOrEmpty(animationinfo) Then
                    Dim currentanimations As String() = animationinfo.Split(Chr(1))
                    For i As Integer = 0 To currentanimations.Length - 1
                        If Not String.IsNullOrEmpty(currentanimations(i)) AndAlso currentanimations(i).Contains("=") Then
                            currentanimations(i) = currentanimations(i).Substring(0, currentanimations(i).Length - 1) & "0"
                        End If
                    Next
                    regkey.SetValue("B2SAnimations", Join(currentanimations, Chr(1)))
                End If

            End Using

        Else

            formBackglass.StopAllAnimations()

        End If

    End Sub

    Private Function MyB2SIsAnimationRunning(ByVal animationname As String) As Boolean

        If Not B2SData.IsBackglassRunning Then Return False

        If B2SData.IsBackglassStartedAsEXE Then

            ' TODO
            Return False

        Else

            Return formBackglass.IsAnimationRunning(animationname)

        End If

    End Function

    Private Sub MyB2SStartRotation()

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)

                ' right now just one rotation per backglass so just set a "1" or "0"
                regkey.SetValue("B2SRotations", "1")

            End Using

        Else

            formBackglass.StartRotation()

        End If

    End Sub
    Private Sub MyB2SStopRotation()

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)

                ' right now just one rotation per backglass so just set a "1" or "0"
                regkey.SetValue("B2SRotations", "0")

            End Using

        Else

            formBackglass.StopRotation()

        End If

    End Sub

    Private Sub MyB2SShowOrHideScoreDisplays(ByVal visible As Boolean)

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
                regkey.SetValue("B2SHideScoreDisplays", If(Not visible, 1, 0))
            End Using

        Else

            If visible Then
                formBackglass.ShowScoreDisplays()
            Else
                formBackglass.HideScoreDisplays()
            End If

        End If

    End Sub

    Private Sub MyB2SPlaySound(ByVal soundname As String)

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)

                ' get thru all sounds of the registry and update or add the new started sound
                Dim found As Boolean = False
                Dim soundinfo As String = regkey.GetValue("B2SSounds", String.Empty).ToString()
                Dim currentsounds As String() = soundinfo.Split(Chr(1))
                For i As Integer = 0 To currentsounds.Length - 1
                    If currentsounds(i).StartsWith(soundname & "=") Then
                        found = True
                        currentsounds(i) = soundname & "=1"
                        Exit For
                    End If
                Next
                If Not found Then
                    ReDim Preserve currentsounds(currentsounds.Length)
                    currentsounds(currentsounds.Length - 1) = soundname & "=1"
                End If
                regkey.SetValue("B2SSounds", Join(currentsounds, Chr(1)))

            End Using

        Else

            formBackglass.PlaySound(soundname)

        End If

    End Sub
    Private Sub MyB2SStopSound(ByVal soundname As String)

        If Not B2SData.IsBackglassRunning Then Return

        If B2SData.IsBackglassStartedAsEXE Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)

                ' get thru all sounds of the registry and update or add the stopped sound
                Dim found As Boolean = False
                Dim soundinfo As String = regkey.GetValue("B2SSounds", String.Empty).ToString()
                If Not String.IsNullOrEmpty(soundinfo) Then
                    Dim currentsounds As String() = soundinfo.Split(Chr(1))
                    For i As Integer = 0 To currentsounds.Length - 1
                        If currentsounds(i).StartsWith(soundname & "=") Then
                            found = True
                            currentsounds(i) = soundname & "=0"
                            Exit For
                        End If
                    Next
                    If Not found Then
                        ReDim Preserve currentsounds(currentsounds.Length)
                        currentsounds(currentsounds.Length - 1) = soundname & "=0"
                    End If
                    regkey.SetValue("B2SSounds", Join(currentsounds, Chr(1)))
                End If

            End Using

        Else

            formBackglass.StopSound(soundname)

        End If

    End Sub

#End Region

#End Region


#Region "private stuff"

    Private Sub Startup()

        ' get thru all processes
        Dim processes As Processes = New Processes()
        If Not String.IsNullOrEmpty(processes.TableName) AndAlso Not B2SData.TestMode Then
            B2SData.TableFileName = processes.TableName
        End If
        B2SData.IsHyperpinRunning = processes.IsHyperpinRunning
        If processes.TableCount > 1 Then
            ' maybe do here something anytime
        End If

        ' start end timer
        timer.Start()

    End Sub

    Private Sub CheckTableHandle()

        If tableHandle = 0 Then
            Dim processes As Processes = New Processes()
            tableHandle = processes.TableHandle
        End If

    End Sub

    Private Sub ShowBackglassForm()

        If Not B2SData.LaunchBackglass Then Return

        ' remove a lot registry stuff for initialisation
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
            regkey.SetValue("B2SGameName", B2SSettings.GameName)
            regkey.SetValue("B2SB2SName", B2SSettings.B2SName)
            'regkey.DeleteValue("B2SData", False) ' do not remove B2SDATA because of crashes of already existing B2S exes
            regkey.DeleteValue("B2SAnimations", False)
            regkey.DeleteValue("B2SRotations", False)
            regkey.DeleteValue("B2SSounds", False)
            regkey.DeleteValue("B2SLamps", False)
            regkey.DeleteValue("B2SSolenoids", False)
            regkey.DeleteValue("B2SGIStrings", False)
            regkey.DeleteValue("B2SSetData", False)
            regkey.DeleteValue("B2SReloadSettings", False)
            regkey.DeleteValue("B2SHideScoreDisplays", False)
            regkey.DeleteValue("B2SIlluGroupsByName", False)
            For i As Integer = 1 To 5
                regkey.DeleteValue("B2SMechs" & i.ToString(), False)
            Next
            For i As Integer = 0 To 200
                regkey.DeleteValue("B2SLED" & i.ToString(), False)
                regkey.DeleteValue("B2SReel" & i.ToString(), False)
                regkey.DeleteValue("B2SScoreDigit" & i.ToString(), False)
                regkey.DeleteValue("B2SScoreDigits" & i.ToString(), False)
                regkey.DeleteValue("B2SScoreDisplay" & i.ToString(), False)
                regkey.DeleteValue("B2SScorePlayer" & i.ToString(), False)
            Next
            regkey.DeleteValue("B2SBackglassFileVersion", False)
        End Using

        B2SSettings.Load(False)
        B2SStatistics.LogStatistics = B2SSettings.IsStatisticsBackglassOn
        If Not B2SStatistics.LogStatistics Then

            If B2SSettings.StartAsEXE AndAlso Not B2SData.TestMode Then

                B2SData.IsBackglassStartedAsEXE = True

                Try

                    ' check from where to start the server exe
                    Dim exeFound As Boolean = False
                    Dim exePath As String = String.Empty
                    If IO.File.Exists(IO.Path.Combine(IO.Directory.GetCurrentDirectory, exeName)) Then
                        exeFound = True
                        exePath = IO.Directory.GetCurrentDirectory
                    ElseIf IO.File.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, exeName)) Then
                        exeFound = True
                        exePath = My.Application.Info.DirectoryPath
                    End If

                    ' maybe throw exception because the exe can not be found
                    If Not exeFound Then
                        Throw New Exception("Can not find '" & exeName & "' in folder '" & IO.Directory.GetCurrentDirectory & "' and in folder '" & My.Application.Info.DirectoryPath & "'")
                    End If

                    ' start new process
                    process = New Process()
                    process.StartInfo.FileName = IO.Path.Combine(exePath, exeName)
                    process.StartInfo.Arguments = """" & B2SData.TableFileName & """ """ & If(B2SData.TestMode, "1", "0") & """"
                    process.Start()

                    If B2SSettings.IsROMControlled Then
                        VPinMAME.hidden = If((B2SSettings.HideDMD = Windows.Forms.CheckState.Indeterminate), Hidden, (B2SSettings.HideDMD = Windows.Forms.CheckState.Checked)) ' Hidden OrElse B2SSettings.HideDMD
                    End If

                    B2SData.IsBackglassVisible = True

                Catch ex As Exception
                    If B2SSettings.ShowStartupError Then
                        Windows.Forms.MessageBox.Show(ex.Message, My.Resources.AppTitle, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                    End If
                    KillBackglassForm()
                End Try

            Else

                B2SData.IsBackglassStartedAsEXE = False

                Try

                    ' create backglass form
                    If formBackglass Is Nothing Then
                        formBackglass = New formBackglass()
                    End If

                    If B2SSettings.IsROMControlled Then
                        VPinMAME.hidden = If((B2SSettings.HideDMD = Windows.Forms.CheckState.Indeterminate), Hidden, (B2SSettings.HideDMD = Windows.Forms.CheckState.Checked)) ' B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels OrElse B2SSettings.HideDMD
                    End If

                    formBackglass.Show()
                    formBackglass.TopMost = True
                    formBackglass.BringToFront()
                    formBackglass.TopMost = False

                    B2SData.IsBackglassVisible = True

                Catch ex As Exception
                    If B2SSettings.ShowStartupError Then
                        Windows.Forms.MessageBox.Show(ex.Message, My.Resources.AppTitle, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                    End If
                    KillBackglassForm()
                End Try

            End If

            isBackglassKilled = False

        Else

            ' create backglass form
            If formBackglass Is Nothing Then
                formBackglass = New formBackglass(True)
            End If

            If VPinMAME IsNot Nothing Then
                VPinMAME.hidden = If((B2SSettings.HideDMD = Windows.Forms.CheckState.Indeterminate), Hidden, (B2SSettings.HideDMD = Windows.Forms.CheckState.Checked)) ' B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels OrElse B2SSettings.HideDMD
            End If

            formBackglass.Show()
            formBackglass.TopMost = True
            formBackglass.BringToFront()
            formBackglass.TopMost = False

            B2SData.IsBackglassVisible = True

        End If

    End Sub
    Private Sub HideBackglassForm()

        If B2SData.IsBackglassStartedAsEXE Then

            Try
                KillBackglassForm()
            Catch
            End Try

        Else

            If formBackglass IsNot Nothing Then
                formBackglass.Hide()
            End If

        End If

    End Sub
    Private Sub KillBackglassForm()

        If Not isBackglassKilled Then

            isBackglassKilled = True

            B2SData.IsBackglassVisible = False

            If B2SData.IsBackglassStartedAsEXE Then

                If process IsNot Nothing Then
                    Try
                        process.Kill()
                        process.Close()
                        process.Dispose()
                    Finally
                        process = Nothing
                    End Try
                End If

                Try
                    Dim processes As Processes = New Processes()
                    If CInt(processes.BackglassFormHandle) > 0 Then
                        SendMessage(processes.BackglassFormHandle, WM_SYSCOMMAND, SC_CLOSE, 0)
                    End If
                Catch
                End Try

            Else

                If formBackglass IsNot Nothing Then

                    Try
                        formBackglass.Close()
                        formBackglass.Dispose()
                    Finally
                        formBackglass = Nothing
                    End Try

                End If

            End If

            tableReset = True

        End If

    End Sub

    Private Function RandomStarter(ByVal top As Integer) As Integer

        Static lastone As Integer = -1
        Dim ret As Integer = -1
        Do Until ret >= 0 AndAlso ret < top AndAlso ret <> lastone
            Dim random As Random = New Random(Date.Now.Millisecond)
            ret = CInt(Math.Truncate(random.NextDouble() * top))
        Loop
        lastone = ret
        Return ret

    End Function

#End Region


End Class
