Imports System
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports System.Text

<ComClass(Server.ClassID, Server.InterfaceID, Server.EventsID)> _
Public Class Server

    Private Declare Function IsWindow Lib "user32" (ByVal hwnd As IntPtr) As Boolean

    Private formBackglass As formBackglass = Nothing

    Private timer As Windows.Forms.Timer = Nothing

    Private tableHandle As Integer = 0
    Private tableCount As Integer = 0

    Private isChangedLampsCalled As Boolean = False
    Private isChangedSolenoidsCalled As Boolean = False
    Private isChangedGIStringsCalled As Boolean = False
    Private isChangedLEDsCalled As Boolean = False


#Region "COM GUIDs"
    ' GUIDs provide the COM identity for this class 
    Public Const ClassID As String = "09e233a3-cc79-457a-b49e-f637588891e5"
    Public Const InterfaceID As String = "5693c68c-5834-466d-aaac-a86922076efd"
    Public Const EventsID As String = "a48a5c0a-656c-4253-9f33-2426cc9c87b7"
#End Region

#Region "constructor and end timer"

    Public Sub New()

        ' config main timer
        timer = New Windows.Forms.Timer
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Interval = 37

    End Sub

    Private Sub Timer_Tick()

        'timer.Stop()

        ' check whether the table is left
        If tableHandle <> 0 AndAlso Not IsWindow(tableHandle) Then
            Me.Stop()
        End If

        ' have a look for important pollings
        Static counter As Integer = 0
        Static callLamps As Boolean = False
        Static callSolenoids As Boolean = False
        Static callGIStrings As Boolean = False
        Static callLEDs As Boolean = False
        If counter <= 25 Then
            counter += 1
            callLamps = Not isChangedLampsCalled AndAlso B2SData.UseRomLamps
            callSolenoids = Not isChangedSolenoidsCalled AndAlso B2SData.UseRomSolenoids
            callGIStrings = Not isChangedGIStringsCalled AndAlso B2SData.UseRomGIStrings
            callLEDs = Not isChangedLEDsCalled AndAlso (B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels)
            CheckTableHandle()
        Else
            If B2SSettings.IsGameNameSet Then
                If callLamps Then Dim chg As Object = ChangedLamps()
                If callSolenoids Then Dim chg As Object = ChangedSolenoids()
                If callGIStrings Then Dim chg As Object = ChangedGIStrings()
                If callLEDs Then Dim chg As Object = ChangedLEDs(&HFFFFFFFF, &HFFFFFFFF)
            End If
        End If

        'timer.Start()

    End Sub

#End Region

#Region "Visual PinMAME COM object"

    Private _vpinmame As Object = Nothing
    Private ReadOnly Property VPinMAME() As Object
        Get
            If _vpinmame Is Nothing Then
                _vpinmame = CreateObject("VPinMAME.Controller")
            End If
            Return _vpinmame
        End Get
    End Property

#End Region

#Region "Visual PinMAME control"

    Public Property GameName() As String
        Get
            Return VPinMAME.GameName
        End Get
        Set(ByVal value As String)
            VPinMAME.GameName = value
            B2SSettings.GameName = value
        End Set
    End Property

    Public ReadOnly Property Games(ByVal gamename As Object) As Object
        Get
            Return VPinMAME.Games(gamename)
        End Get
    End Property

    Public ReadOnly Property Running() As Boolean
        Get
            Return VPinMAME.Running
        End Get
    End Property

    Public Property Pause() As Boolean
        Get
            Return VPinMAME.Pause
        End Get
        Set(ByVal value As Boolean)
            VPinMAME.Pause = value
        End Set
    End Property

    Public ReadOnly Property Version() As String
        Get
            Return VPinMAME.Version
        End Get
    End Property

    Public Sub Run(Optional ByVal handle As Object = 0)
        tableHandle = handle
        Startup()
        ShowBackglassForm()
        If B2SSettings.IsGameNameSet Then
            VPinMAME.Run(handle)
        End If
    End Sub

    Public Sub [Stop]()
        HideBackglassForm()
        timer.Stop()
        VPinMAME.Stop()
        KillBackglassForm()
    End Sub

#End Region

#Region "customization"

    Private _startBackglassEXE As Boolean = True
    Public Property StartBackglassEXE() As Boolean
        Get
            Return _startBackglassEXE
        End Get
        Set(ByVal value As Boolean)
            _startBackglassEXE = value
        End Set
    End Property

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
            VPinMAME.hidden = True
        End Set
    End Property

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
            isChangedLampsCalled = True
            Dim chg As Object = VPinMAME.ChangedLamps()
            If (B2SData.UseRomLamps OrElse B2SData.UseAnimationLamps OrElse B2SSettings.IsLampsStateLogOn) AndAlso Not B2SSettings.AllOff AndAlso Not B2SSettings.LampsOff AndAlso B2SData.IsBackglassVisible Then
                CheckLamps(DirectCast(chg, Object(,)))
            End If
            Return chg
        End Get
    End Property

    Public ReadOnly Property ChangedSolenoids() As Object
        Get
            isChangedSolenoidsCalled = True
            Dim chg As Object = VPinMAME.ChangedSolenoids()
            If (B2SData.UseRomSolenoids OrElse B2SData.UseAnimationSolenoids OrElse B2SSettings.IsSolenoidsStateLogOn) AndAlso Not B2SSettings.AllOff AndAlso Not B2SSettings.SolenoidsOff AndAlso B2SData.IsBackglassVisible Then
                CheckSolenoids(DirectCast(chg, Object(,)))
            End If
            Return chg
        End Get
    End Property

    Public ReadOnly Property ChangedGIStrings() As Object
        Get
            isChangedGIStringsCalled = True
            Dim chg As Object = VPinMAME.ChangedGIStrings()
            If (B2SData.UseRomGIStrings OrElse B2SData.UseAnimationGIStrings OrElse B2SSettings.IsGIStringsStateLogOn) AndAlso Not B2SSettings.AllOff AndAlso Not B2SSettings.GIStringsOff AndAlso B2SData.IsBackglassVisible Then
                CheckGIStrings(DirectCast(chg, Object(,)))
            End If
            Return chg
        End Get
    End Property

    Public ReadOnly Property ChangedLEDs(ByVal mask2 As Object, ByVal mask1 As Object) As Object
        Get
            isChangedLEDsCalled = True
            Dim chg As Object = VPinMAME.ChangedLEDs(mask2, mask1) ' (&HFFFFFFFF, &HFFFFFFFF) 
            If (B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels OrElse B2SSettings.IsLEDsStateLogOn) AndAlso Not B2SSettings.AllOff AndAlso Not B2SSettings.LEDsOff AndAlso B2SData.IsBackglassVisible Then
                CheckLEDs(DirectCast(chg, Object(,)))
            End If
            Return chg
        End Get
    End Property

    Public ReadOnly Property NewSoundCommands() As Object
        Get
            Dim chg As Object = VPinMAME.NewSoundCommands()
            Return chg
        End Get
    End Property

#Region "private polling functions"

    Private isVisibleStateSet As Boolean = False
    Private lastTopVisible As Boolean = False
    Private lastSecondVisible As Boolean = False

    Private collectLampsData As B2SCollectData = New B2SCollectData(B2SSettings.LampsSkipFrames)
    Private collectSolenoidsData As B2SCollectData = New B2SCollectData(B2SSettings.SolenoidsSkipFrames)
    Private collectGIStringsData As B2SCollectData = New B2SCollectData(B2SSettings.GIStringsSkipFrames)
    Private collectLEDsData As B2SCollectData = New B2SCollectData(B2SSettings.LEDsSkipFrames)

    Private Sub CheckLamps(ByVal lamps As Object(,))

        statelogChangedLamps.IsLogOn = B2SSettings.IsLampsStateLogOn

        If StartBackglassEXE Then

            If lamps IsNot Nothing AndAlso IsArray(lamps) Then

                Dim sb As StringBuilder = New StringBuilder
                sb.Append(Registry.CurrentUser.OpenSubKey("B2S").GetValue("B2SLamps", New String("0", 250)).ToString())

                ' get thru all lamp info
                For i As Integer = 0 To lamps.GetUpperBound(0)

                    ' get lamp data
                    Dim lampid As Integer = CInt(lamps(i, 0))
                    Dim lampstate As Integer = CInt(lamps(i, 1))

                    ' maybe write log
                    If statelogChangedLamps.IsLogOn Then
                        statelogChangedLamps.WriteLogEntry(DateTime.Now & ": Collecting data (" & (lamps.GetUpperBound(0) + 1) & "): " & lampid & " - " & lampstate)
                    End If

                    ' check whether there is need to do something
                    If B2SData.UseRomLamps OrElse B2SData.UseAnimationLamps Then

                        sb.Remove(lampid, 1)
                        sb.Insert(lampid, lampstate.ToString())

                    End If

                Next

                Registry.CurrentUser.OpenSubKey("B2S", True).SetValue("B2SLamps", sb.ToString())
                
            End If

        Else

            If lamps IsNot Nothing AndAlso IsArray(lamps) Then

                ' get thru all lamp info
                For i As Integer = 0 To lamps.GetUpperBound(0)

                    ' get lamp data
                    Dim lampid As Integer = CInt(lamps(i, 0))
                    Dim lampstate As Integer = CInt(lamps(i, 1))

                    ' maybe write log
                    If statelogChangedLamps.IsLogOn Then
                        statelogChangedLamps.WriteLogEntry(DateTime.Now & ": Collecting data (" & (lamps.GetUpperBound(0) + 1) & "): " & lampid & " - " & lampstate)
                    End If

                    ' check whether there is need to do something
                    If B2SData.UseRomLamps OrElse B2SData.UseAnimationLamps Then

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
                        If B2SData.UsedAnimationLampIDs.ContainsKey(lampid) Then
                            collectLampsData.Add(lampid, New B2SCollectData.CollectData(lampstate, B2SCollectData.eCollectedDataType.Animation))
                        End If

                    End If

                Next

            End If

            ' one collection loop is done
            collectLampsData.DataAdded()

            ' maybe show the collected data
            If collectLampsData.ShowData() Then

                For Each lampdata As KeyValuePair(Of Integer, B2SCollectData.CollectData) In collectLampsData

                    ' get lamp data
                    Dim lampid As Integer = lampdata.Key
                    Dim lampstate As Integer = lampdata.Value.State
                    Dim datatypes As Integer = lampdata.Value.Types

                    ' maybe write log
                    If statelogChangedLamps.IsLogOn Then
                        statelogChangedLamps.WriteLogEntry(DateTime.Now & ": Applying data (" & collectLampsData.Count & ")  : " & lampid & " - " & lampstate & " - (" & datatypes & ")")
                    End If

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
                                formBackglass.BackgroundImage = formBackglass.LightImage
                                B2SData.IsOffImageVisible = True
                            End If
                        End If
                    End If
                    If (datatypes And B2SCollectData.eCollectedDataType.Standard) <> 0 Then
                        For Each picbox As B2SPictureBox In B2SData.UsedRomLampIDs(lampid)
                            If picbox IsNot Nothing Then
                                Dim visible As Boolean = (lampstate <> 0)
                                If picbox.RomInverted Then visible = Not visible
                                picbox.Visible = visible
                            End If
                        Next
                    End If

                    ' animation stuff
                    If (datatypes And B2SCollectData.eCollectedDataType.Animation) <> 0 Then
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

                Next

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

        If StartBackglassEXE Then

            If solenoids IsNot Nothing AndAlso IsArray(solenoids) Then

                Dim sb As StringBuilder = New StringBuilder
                sb.Append(Registry.CurrentUser.OpenSubKey("B2S").GetValue("B2SSolenoids", New String("0", 250)).ToString())

                ' get thru all lamp info
                For i As Integer = 0 To solenoids.GetUpperBound(0)

                    ' get lamp data
                    Dim solenoidid As Integer = CInt(solenoids(i, 0))
                    Dim solenoidstate As Integer = CInt(solenoids(i, 1))

                    ' maybe write log
                    If statelogChangedSolenoids.IsLogOn Then
                        statelogChangedSolenoids.WriteLogEntry(DateTime.Now & ": Collecting data (" & (solenoids.GetUpperBound(0) + 1) & "): " & solenoidid & " - " & solenoidstate)
                    End If

                    ' check whether there is need to do something
                    If B2SData.UseRomSolenoids OrElse B2SData.UseAnimationSolenoids Then

                        sb.Remove(solenoidid, 1)
                        sb.Insert(solenoidid, solenoidstate.ToString())

                    End If

                Next

                Registry.CurrentUser.OpenSubKey("B2S", True).SetValue("B2SSolenoids", sb.ToString())

            End If

        Else

            If solenoids IsNot Nothing AndAlso IsArray(solenoids) Then

                ' get thru all solenoid info
                For i As Integer = 0 To solenoids.GetUpperBound(0)

                    ' get solenoid data
                    Dim solenoidid As Integer = CInt(solenoids(i, 0))
                    Dim solenoidstate As Integer = CInt(solenoids(i, 1))

                    ' maybe write log
                    If statelogChangedSolenoids.IsLogOn Then
                        statelogChangedSolenoids.WriteLogEntry(DateTime.Now & ": Collecting data (" & (solenoids.GetUpperBound(0) + 1) & "): " & solenoidid & " - " & solenoidstate)
                    End If

                    ' check whether there is need to do something
                    If B2SData.UseRomSolenoids OrElse B2SData.UseAnimationSolenoids Then

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
                        If B2SData.UsedAnimationSolenoidIDs.ContainsKey(solenoidid) Then
                            collectSolenoidsData.Add(solenoidid, New B2SCollectData.CollectData(solenoidstate, B2SCollectData.eCollectedDataType.Animation))
                        End If

                    End If

                Next

            End If

            ' one collection loop is done
            collectSolenoidsData.DataAdded()

            ' maybe show the collected data
            If collectSolenoidsData.ShowData() Then

                For Each solenoiddata As KeyValuePair(Of Integer, B2SCollectData.CollectData) In collectSolenoidsData

                    ' get solenoid data
                    Dim solenoidid As Integer = solenoiddata.Key
                    Dim solenoidstate As Integer = solenoiddata.Value.State
                    Dim datatypes As Integer = solenoiddata.Value.Types

                    ' maybe write log
                    If statelogChangedSolenoids.IsLogOn Then
                        statelogChangedSolenoids.WriteLogEntry(DateTime.Now & ": Applying data (" & collectSolenoidsData.Count & ")  : " & solenoidid & " - " & solenoidstate & " (" & datatypes & ")")
                    End If

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
                                formBackglass.BackgroundImage = formBackglass.LightImage
                                B2SData.IsOffImageVisible = True
                            End If
                        End If
                    End If
                    If (datatypes And B2SCollectData.eCollectedDataType.Standard) <> 0 Then
                        For Each picbox As B2SPictureBox In B2SData.UsedRomSolenoidIDs(solenoidid)
                            If picbox IsNot Nothing Then
                                Dim visible As Boolean = (solenoidstate <> 0)
                                If picbox.RomInverted Then visible = Not visible
                                picbox.Visible = visible
                            End If
                        Next
                    End If

                    ' animation stuff
                    If (datatypes And B2SCollectData.eCollectedDataType.Animation) <> 0 Then
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

                Next

                ' reset all current data
                collectSolenoidsData.ClearData(B2SSettings.SolenoidsSkipFrames)

            End If

        End If

    End Sub
    Private Sub CheckGIStrings(ByVal gistrings As Object(,))

        statelogChangedGIStrings.IsLogOn = B2SSettings.IsGIStringsStateLogOn

        If gistrings IsNot Nothing AndAlso IsArray(gistrings) Then

            ' get thru all gistring info
            For i As Integer = 0 To gistrings.GetUpperBound(0)

                ' get gistring data
                Dim gistringid As Integer = CInt(gistrings(i, 0))
                Dim gistringstate As Integer = CInt(gistrings(i, 1))

                ' maybe write log
                If statelogChangedGIStrings.IsLogOn Then
                    statelogChangedGIStrings.WriteLogEntry(DateTime.Now & ": Collecting data (" & (gistrings.GetUpperBound(0) + 1) & "): " & gistringid & " - " & gistringstate)
                End If

                ' check whether there is need to do something
                If B2SData.UseRomGIStrings OrElse B2SData.UseAnimationGIStrings Then

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
                    If B2SData.UsedAnimationGIStringIDs.ContainsKey(gistringid) Then
                        collectGIStringsData.Add(gistringid, New B2SCollectData.CollectData(gistringstate, B2SCollectData.eCollectedDataType.Animation))
                    End If

                End If

            Next

        End If

        ' one collection loop is done
        collectGIStringsData.DataAdded()

        ' maybe show the collected data
        If collectGIStringsData.ShowData() Then

            For Each gistringdata As KeyValuePair(Of Integer, B2SCollectData.CollectData) In collectGIStringsData

                ' get gistring data
                Dim gistringid As Integer = gistringdata.Key
                Dim gistringstate As Integer = gistringdata.Value.State
                Dim datatypes As Integer = gistringdata.Value.Types

                ' maybe write log
                If statelogChangedGIStrings.IsLogOn Then
                    statelogChangedGIStrings.WriteLogEntry(DateTime.Now & ": Applying data (" & collectGIStringsData.Count & ")  : " & gistringid & " - " & gistringstate & " (" & datatypes & ")")
                End If

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
                            formBackglass.BackgroundImage = formBackglass.LightImage
                            B2SData.IsOffImageVisible = True
                        End If
                    End If
                End If
                If (datatypes And B2SCollectData.eCollectedDataType.Standard) <> 0 Then
                    For Each picbox As B2SPictureBox In B2SData.UsedRomGIStringIDs(gistringid)
                        If picbox IsNot Nothing Then
                            Dim visible As Boolean = (gistringstate > 4)
                            If picbox.RomInverted Then visible = Not visible
                            picbox.Visible = visible
                        End If
                    Next
                End If

                ' animation stuff
                If (datatypes And B2SCollectData.eCollectedDataType.Animation) <> 0 Then
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

            Next

            ' reset all current data
            collectGIStringsData.ClearData(B2SSettings.GIStringsSkipFrames)

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
                If B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels Then
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

            For Each leddata As KeyValuePair(Of Integer, B2SCollectData.CollectData) In collectLEDsData

                Dim digit As Integer = leddata.Key
                Dim value As Integer = leddata.Value.State

                ' maybe write log
                If statelogChangedLEDs.IsLogOn Then
                    statelogChangedLEDs.WriteLogEntry(DateTime.Now & ": Applying data (" & (leds.GetUpperBound(0) + 1) & ")  : " & digit & " - " & value)
                End If

                ' check whether leds or reels are used
                If useLEDs Then

                    ' rendered LEDs are used
                    Dim ledname As String = "LEDBox" & (digit + 1).ToString()
                    If B2SData.LEDs.ContainsKey(ledname) Then
                        B2SData.LEDs(ledname).Value = value
                    End If

                ElseIf useLEDDisplays Then

                    ' Dream 7 displays are used
                    If B2SData.LEDDisplayDigits.ContainsKey(digit) Then
                        With B2SData.LEDDisplayDigits(digit)
                            .LEDDisplay.SetValue(.Digit, value)
                        End With
                    End If

                ElseIf useReels Then

                    ' reels are used
                    Dim reelname As String = "ReelBox" & (digit + 1).ToString()
                    If B2SData.Reels.ContainsKey(reelname) Then
                        B2SData.Reels(reelname).Value = value
                    End If

                End If

            Next

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
        End Set
    End Property

    Public Property Mech(ByVal number As Object) As Integer
        Get
            Return VPinMAME.Mech(number)
        End Get
        Set(ByVal value As Integer)
            VPinMAME.Mech(number) = value
        End Set
    End Property
    Public ReadOnly Property GetMech(ByVal number As Object) As Object
        Get
            Return VPinMAME.GetMech(number)
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

#End Region

#Region "non VPinMAME support"

    Public Sub SetScore(ByVal digit As Integer, ByVal value As Integer)

    End Sub

    Public Sub SetPlayer(ByVal player As Integer)

    End Sub

#End Region

#Region "private stuff"

    Private Sub Startup()

        ' get thru all processes
        Dim processes As Processes = New Processes()
        B2SData.TableFileName = processes.TableName
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

        Try
            If formBackglass Is Nothing Then
                formBackglass = New formBackglass()
            End If
            VPinMAME.hidden = B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels OrElse B2SSettings.HideDMD
            formBackglass.Show()
            formBackglass.TopMost = True
            formBackglass.BringToFront()
            formBackglass.TopMost = False
            B2SData.IsBackglassVisible = True
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message, My.Resources.AppTitle, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub HideBackglassForm()
        If formBackglass IsNot Nothing Then
            formBackglass.Hide()
        End If
    End Sub
    Private Sub KillBackglassForm()

        If formBackglass IsNot Nothing Then
            On Error Resume Next
            For I As Integer = formBackglass.Controls.Count - 1 To 0 Step -1
                formBackglass.Controls(I).BackgroundImage.Dispose()
                formBackglass.Controls(I).BackgroundImage = Nothing
                formBackglass.Controls(I).Dispose()
            Next
            formBackglass.TopLightImage.Dispose()
            formBackglass.TopLightImage = Nothing
            If formBackglass.SecondLightImage IsNot Nothing Then
                formBackglass.SecondLightImage.Dispose()
                formBackglass.SecondLightImage = Nothing
                formBackglass.TopAndSecondLightImage.Dispose()
                formBackglass.TopAndSecondLightImage = Nothing
            End If
            formBackglass.BackgroundImage.Dispose()
            formBackglass.BackgroundImage = Nothing
            B2SData.ClearAll()
            B2SSettings.ClearAll()
            formBackglass.Close()
            formBackglass.Dispose()
            GC.Collect()
            GC.SuppressFinalize(formBackglass)
            GC.SuppressFinalize(Me)
            GC.Collect()
            B2SData.IsBackglassVisible = False
        End If

    End Sub

#End Region


End Class
