#Disable Warning BC42016, BC42017, BC42018, BC42019, BC42032

Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms

Public Class B2SScreen
    Public Property ScreensOrdered() As Screen() = Screen.AllScreens.OrderBy(Function(sc) sc.Bounds.Location.X).ToArray()
    Public Property VersionTwoFile() As Boolean = False

    Public Shared formBackglass As formBackglass = Nothing
    Public formDMD As formDMD = Nothing
    Public formbackground As Background = Nothing
    Public debugLog As Log = New Log("B2SDebugLog")

    Public Enum eDMDViewMode
        NotDefined = 0
        NoDMD = 1
        ShowDMD = 2
        ShowDMDOnlyAtDefaultLocation = 3
        DoNotShowDMDAtDefaultLocation = 4
    End Enum

    Public Property PlayfieldSize() As Size = New Size(0, 0)
    Public Property BackglassMonitor() As String = String.Empty
    Public Property BackglassScreen() As Screen = Nothing
    Public Property BackglassSize() As Size = New Size(0, 0)
    Public Property BackglassLocation() As Point = New Point(0, 0)
    Public Property BackglassGrillHeight() As Integer = 0
    Public Property BackglassSmallGrillHeight() As Integer = 0
    Public Property DMDSize() As Size = New Size(0, 0)
    Public Property DMDLocation() As Point = New Point(0, 0)
    Public Property DMDFlipY() As Boolean = False
    Public Property DMDAtDefaultLocation() As Boolean = True
    Public Property DMDViewMode() As eDMDViewMode = eDMDViewMode.NotDefined

    Public Property BackgroundSize() As Size = New Size(0, 0)
    Public Property BackgroundLocation() As Point = New Point(0, 0)
    Public Property BackgroundPath() As String = String.Empty

    Public Property BackglassCutOff() As Rectangle = Rectangle.Empty

    Public Property IsDMDToBeShown() As Boolean = False
    Public Property StartBackground() As Boolean = False

    Public Property BackglassRescaleFactor As SizeF = New SizeF(1, 1)
    Public Property ScreenDpiFactor As SizeF = New SizeF(1, 1)

#Region "constructor and startup"

    Public Sub New()
        debugLog.IsLogOn = B2SSettings.B2SDebugLog
        debugLog.WriteLogEntry("B2SScreen.New")

        'searchPathLog.WriteLogEntry("Start Search ScreenRes")


        ' read settings file
        ReadB2SSettingsFromFile()
        debugLog.WriteLogEntry("B2SScreen.New DONE")
    End Sub

    Public Sub Start(ByVal _formBackglass As Form)

        ' here we go with one form for the backglass and no DMD and no grill
        Me.Start(_formBackglass, Nothing, New Size(0, 0), eDMDViewMode.NoDMD, 0, 0)

    End Sub
    Public Sub Start(ByVal _formBackglass As Form, ByVal _BackglassGrillHeight As Integer, ByVal _BackglassSmallGrillHeight As Integer)

        ' here we go with one form for the backglass and no DMD but with grill
        Me.Start(_formBackglass, Nothing, New Size(0, 0), eDMDViewMode.NoDMD, _BackglassGrillHeight, _BackglassSmallGrillHeight)

    End Sub
    Public Sub Start(ByVal _formBackglass As Form, ByVal _formDMD As Form, ByVal _DefaultDMDLocation As Point)

        ' here we go with two forms for the backglass and DMD and default values for the DMD view mode and the grill height
        Me.Start(_formBackglass, _formDMD, _DefaultDMDLocation, eDMDViewMode.ShowDMD, 0, 0)

    End Sub
    Public Sub Start(ByVal _formBackglass As Form, ByVal _formDMD As Form, ByVal _DefaultDMDLocation As Point, ByVal _DMDViewMode As eDMDViewMode, ByVal _BackglassGrillHeight As Integer, ByVal _BackglassSmallGrillHeight As Integer)

        ' here we go with one or two forms for the backglass and the DMD
        formBackglass = _formBackglass
        Me.formDMD = _formDMD

        Me.formbackground = New Background


        ' get all backglass settings
        GetB2SSettings(_DefaultDMDLocation, _DMDViewMode, _BackglassGrillHeight, _BackglassSmallGrillHeight)

        ' show all backglass stuff
        Show()

    End Sub

#End Region


#Region "get backglass settings and show backglass"

    Private Sub ReadB2SSettingsFromFile()
        Dim loadFileName As String = String.Empty
        debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile Start Search ScreenRes")

        Try
            Dim loadFileNames() As String = {IO.Path.ChangeExtension(B2SData.BackglassFileName, B2SSettings.B2SResFileEnding),   ' .\BackglassName.res
                                             B2SData.TableFileName & B2SSettings.B2SResFileEnding,                      ' .\TableName.res
                                             IO.Path.Combine(B2SData.TableFileName, B2SSettings.B2SScreenResFileName),   ' .\TableName\ScreenRes.txt
                                             B2SSettings.B2SScreenResFileName,                                           ' .\ScreenRes.txt
                                             IO.Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), B2SSettings.B2SScreenResFileName)' B2SFolder\ScreenRes.txt
                                            }
            For Each testFileName As String In loadFileNames

                debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile Test " & testFileName)

                If IO.File.Exists(testFileName) Then
                    loadFileName = testFileName
                    B2SSettings.LoadedResFilePath = Path.GetFullPath(loadFileName)
                    debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile Found ScreenRes " & loadFileName)

                    Exit For
                End If
            Next
        Catch
        End Try
        debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile Stop Search ScreenRes")


        If Not loadFileName = String.Empty Then

            ' open settings file
            FileOpen(1, loadFileName, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)

            ' get all settings
            Dim line(50) As String
            Dim i As Integer = 0
            Do Until EOF(1) Or i > 20
                line(i) = LineInput(1)
                If (line(i).StartsWith("#")) Then
                    If (line(i).Replace(" ", "").StartsWith("#V2")) Then VersionTwoFile = True
                    Continue Do
                End If
                i += 1
            Loop
            ' close file handle
            FileClose(1)
            debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile A version #2 file " & Me.BackgroundPath)

            line(i) = 0
            If i < 49 Then line(i + 1) = 0

            Me.BackglassMonitor = line(4)
            EvalateBackglassScreen()

            Me.PlayfieldSize = New Size(CInt(CalcValue(line(0), ScreensOrdered(0).Bounds.Width)), CInt(CalcValue(line(1), ScreensOrdered(0).Bounds.Height)))
            If (line(0).Contains("%") Or line(1).Contains("%")) Then debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile PlayfieldSize: " &
                                                                                                  line(0) & "," & line(1) & "->" & Me.PlayfieldSize.Width & "," & Me.PlayfieldSize.Height)
            debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile Backglass Screen Size: " & ScreenDpiFactor.Width & "," & ScreenDpiFactor.Height & "->" & Me.BackglassScreen.Bounds.Width & "," & Me.BackglassScreen.Bounds.Height)
            Me.BackglassSize = New Size(CInt(CalcValue(line(2), Me.BackglassScreen.Bounds.Width)), CInt(CalcValue(line(3), Me.BackglassScreen.Bounds.Height)))
            If (line(2).Contains("%") Or line(3).Contains("%")) Then debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile BackglassSize: " &
                                                                                                  line(2) & "," & line(3) & "->" & Me.BackglassSize.Width & "," & Me.BackglassSize.Height)

            Me.BackglassLocation = New Point(CInt(CalcValue(line(5), Me.BackglassScreen.Bounds.Width)), CInt(CalcValue(line(6), Me.BackglassScreen.Bounds.Height)))
            If (line(5).Contains("%") Or line(6).Contains("%")) Then debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile BackglassLocation: " &
                                                                                                  line(5) & "," & line(6) & "->" & Me.BackglassLocation.X & "," & Me.BackglassLocation.Y)

            Me.DMDSize = New Size(CInt(CalcValue(line(7), Me.BackglassScreen.Bounds.Width)), CInt(CalcValue(line(8), Me.BackglassScreen.Bounds.Height)))
            If (line(7).Contains("%") Or line(8).Contains("%")) Then debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile DMDSize: " &
                                                                                                  line(7) & "," & line(8) & "->" & Me.DMDSize.Width & "," & Me.DMDSize.Height)

            Me.DMDLocation = New Point(CInt(CalcValue(line(9), Me.BackglassScreen.Bounds.Width)), CInt(CalcValue(line(10), Me.BackglassScreen.Bounds.Height)))
            If (line(9).Contains("%") Or line(10).Contains("%")) Then debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile DMDLocation: " &
                                                                                                  line(9) & "," & line(10) & "->" & Me.DMDLocation.X & "," & Me.DMDLocation.Y)

            Me.DMDFlipY = (Trim(line(11)) = "1")

            If (i > 15) Then
                Me.BackgroundLocation = New Point(CInt(CalcValue(line(12), Me.BackglassScreen.Bounds.Width)), CInt(CalcValue(line(13), Me.BackglassScreen.Bounds.Height)))
                If (line(12).Contains("%") Or line(13).Contains("%")) Then debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile BackgroundLocation: " &
                                                                                                  line(12) & "," & line(13) & "->" & Me.BackgroundLocation.X & "," & Me.BackgroundLocation.Y)
                Me.BackgroundSize = New Size(CInt(CalcValue(line(14), Me.BackglassScreen.Bounds.Width)), CInt(CalcValue(line(15), Me.BackglassScreen.Bounds.Height)))
                If (line(14).Contains("%") Or line(15).Contains("%")) Then debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile BackgroundSize: " &
                                                                                                  line(14) & "," & line(15) & "->" & Me.BackgroundSize.Width & "," & Me.BackgroundSize.Height)
                Me.BackgroundPath = line(16)
                If Me.BackgroundPath.Contains("{") Then
                    debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile BackgroundPath contains template:" & Me.BackgroundPath)
                    ' We will try to replace the placeholders with the real values
                    Me.BackgroundPath = GetBackgroundPath(Me.BackgroundPath, B2SData.TableFileName, B2SSettings.GameName)
                    debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile GetBackgroundPath called and returned " & Me.BackgroundPath)
                End If
            Else
                Me.BackgroundLocation = New Point(0, 0)
                Me.BackgroundSize = New Size(0, 0)
                Me.BackgroundPath = ""
            End If

        Else

            debugLog.WriteLogEntry("B2SScreen.ReadB2SSettingsFromFile no B2S screen resolution file found")

            MessageBox.Show("There is no B2S screen resolution file '" & B2SSettings.B2SScreenResFileName & "' in the current folder '" & IO.Directory.GetCurrentDirectory() & "'." & vbCrLf & vbCrLf &
                            "Please create this file with the tool 'B2S_ScreenResIdentifier.exe'.",
                            "B2S backglass error", MessageBoxButtons.OK, MessageBoxIcon.Error)
#If B2S = "DLL" Then
            Stop
#Else
            End
#End If
        End If
    End Sub
    Private Function CalcValue(StringValue As String, totalValue As Integer) As Integer
        ' Calculates an integer value from a string, which may be a percentage (e.g., "50%") or an absolute value.
        ' If the string ends with '%', it returns the percentage of totalValue.
        ' If the string is a number, it returns the integer value.
        ' Returns 0 if the string is invalid or cannot be parsed.

        If StringValue Is Nothing Then Return 0

        StringValue = StringValue.Trim()
        If StringValue.EndsWith("%") Then
            Dim percentStr As String = StringValue.Substring(0, StringValue.Length - 1)
            Dim percentValue As Double
            If Double.TryParse(percentStr, NumberStyles.Any, CultureInfo.InvariantCulture, percentValue) Then
                Return CInt(totalValue * percentValue / 100.0)
            Else
                Return 0
            End If
        Else
            Dim intValue As Integer
            If Integer.TryParse(StringValue, NumberStyles.Any, CultureInfo.InvariantCulture, intValue) Then
                Return intValue '/ ScreenDpiFactor.Height
            Else
                Return 0
            End If
        End If
    End Function
    Private Function GetBackgroundPath(BackgroundPath As String, ByVal TableFileName As String, ByVal GameName As String) As String
        Dim pattern As String = "^(?'name'[\w \-\!']+)(\((?'manufactor'[A-Za-z ]+)? (?'year'[\d{4}]+)\))?(?'extra'.*)?$"
        Dim regex As New Regex(pattern)
        Dim replacedSomething As Boolean = False

        Dim newPath As String = BackgroundPath

        Dim allGroupNames As List(Of String) = New List(Of String) From {"tablename", "gamename"}

        allGroupNames.AddRange(regex.GetGroupNames())
        allGroupNames.RemoveAll(Function(s) s.Length = 1)

        If regex.IsMatch(TableFileName) Then
            For Each groupName As String In allGroupNames
                For Each replaceName As String In allGroupNames
                    If groupName = replaceName And newPath.Contains("{" + replaceName + "}") Then
                        Select Case replaceName
                            Case "tablename"
                                newPath = newPath.Replace("{" + replaceName + "}", TableFileName)
                                replacedSomething = True
                            Case "gamename"
                                newPath = newPath.Replace("{" + replaceName + "}", GameName)
                                replacedSomething = True
                            Case Else
                                Dim replaceValue As String = regex.Match(TableFileName).Groups(replaceName).Value.Trim()
                                If Not String.IsNullOrEmpty(replaceValue) Then
                                    replacedSomething = True
                                    newPath = newPath.Replace("{" + replaceName + "}", replaceValue)
                                End If
                        End Select
                    Else
                        newPath = newPath.Replace("{" + replaceName + "}", "")
                    End If
                Next
                If File.Exists(newPath) And replacedSomething Then
                    Return newPath
                Else
                    newPath = BackgroundPath
                    replacedSomething = False
                End If
            Next
        End If
        For Each replaceName As String In allGroupNames
            newPath = newPath.Replace("{" + replaceName + "}", "")
        Next
        Return newPath

    End Function

    Private Sub GetB2SSettings(ByVal _DefaultDMDLocation As Point, ByVal _DMDViewMode As eDMDViewMode, ByVal _BackglassGrillHeight As Integer, ByVal _BackglassSmallGrillHeight As Integer)

        Me.DMDViewMode = _DMDViewMode

        ' show or do not show the grill and do some more DMD stuff
        Dim showTheGrill As Boolean = (Me.DMDLocation.X = 0 AndAlso Me.DMDLocation.Y = 0)
        If B2SSettings.HideGrill = CheckState.Unchecked Then
            showTheGrill = True
        ElseIf B2SSettings.HideGrill = CheckState.Checked Then
            showTheGrill = False
        End If

        If showTheGrill Then

            ' show the grill
            Me.DMDAtDefaultLocation = True

            ' use default values for DMD location
            If _DefaultDMDLocation <> Nothing AndAlso _DMDViewMode <> eDMDViewMode.DoNotShowDMDAtDefaultLocation Then
                Me.DMDLocation = _DefaultDMDLocation
            End If

            ' maybe do some corrections since there is a small grill
            If _BackglassSmallGrillHeight > 0 AndAlso formBackglass.DarkImage IsNot Nothing Then
                If Me.DMDLocation.Y > 0 Then
                    Me.DMDLocation = New Point(Me.DMDLocation.X, Me.DMDLocation.Y - _BackglassSmallGrillHeight)
                End If

                Me.BackglassSmallGrillHeight = _BackglassSmallGrillHeight

                Me.BackglassCutOff = New Rectangle(0, formBackglass.DarkImage.Height - _BackglassGrillHeight - _BackglassSmallGrillHeight, formBackglass.DarkImage.Width, _BackglassSmallGrillHeight)

                ' shrink some images to remove the small grill
                formBackglass.DarkImage4Authentic = CutOutImage(formBackglass.DarkImage4Authentic, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                If formBackglass.TopLightImage4Authentic IsNot Nothing Then
                    formBackglass.TopLightImage4Authentic = CutOutImage(formBackglass.TopLightImage4Authentic, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If formBackglass.SecondLightImage4Authentic IsNot Nothing Then
                    formBackglass.SecondLightImage4Authentic = CutOutImage(formBackglass.SecondLightImage4Authentic, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If formBackglass.TopAndSecondLightImage4Authentic IsNot Nothing Then
                    formBackglass.TopAndSecondLightImage4Authentic = CutOutImage(formBackglass.TopAndSecondLightImage4Authentic, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If formBackglass.DarkImage4Fantasy IsNot Nothing Then
                    formBackglass.DarkImage4Fantasy = CutOutImage(formBackglass.DarkImage4Fantasy, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If formBackglass.TopLightImage4Fantasy IsNot Nothing Then
                    formBackglass.TopLightImage4Fantasy = CutOutImage(formBackglass.TopLightImage4Fantasy, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If formBackglass.SecondLightImage4Fantasy IsNot Nothing Then
                    formBackglass.SecondLightImage4Fantasy = CutOutImage(formBackglass.SecondLightImage4Fantasy, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If formBackglass.TopAndSecondLightImage4Fantasy IsNot Nothing Then
                    formBackglass.TopAndSecondLightImage4Fantasy = CutOutImage(formBackglass.TopAndSecondLightImage4Fantasy, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If

                ' set background image and new backglass form height (without grill)
                formBackglass.BackgroundImage = formBackglass.DarkImage
                formBackglass.Size = formBackglass.DarkImage.Size

            End If

        Else

            ' do not show the grill (if possible)
            Me.DMDAtDefaultLocation = False

            ' maybe hide grill
            If _BackglassGrillHeight > 0 AndAlso formBackglass.DarkImage IsNot Nothing Then

                Me.BackglassGrillHeight = _BackglassGrillHeight
                Me.BackglassSmallGrillHeight = _BackglassSmallGrillHeight

                Me.BackglassCutOff = New Rectangle(0, formBackglass.DarkImage.Height - _BackglassGrillHeight, formBackglass.DarkImage.Width, _BackglassGrillHeight)

                ' shrink some images to remove the grill
                formBackglass.DarkImage4Authentic = ResizeImage(formBackglass.DarkImage4Authentic, _BackglassGrillHeight)
                If formBackglass.TopLightImage4Authentic IsNot Nothing Then
                    formBackglass.TopLightImage4Authentic = ResizeImage(formBackglass.TopLightImage4Authentic, _BackglassGrillHeight)
                End If
                If formBackglass.SecondLightImage4Authentic IsNot Nothing Then
                    formBackglass.SecondLightImage4Authentic = ResizeImage(formBackglass.SecondLightImage4Authentic, _BackglassGrillHeight)
                End If
                If formBackglass.TopAndSecondLightImage4Authentic IsNot Nothing Then
                    formBackglass.TopAndSecondLightImage4Authentic = ResizeImage(formBackglass.TopAndSecondLightImage4Authentic, _BackglassGrillHeight)
                End If
                If formBackglass.DarkImage4Fantasy IsNot Nothing Then
                    formBackglass.DarkImage4Fantasy = ResizeImage(formBackglass.DarkImage4Fantasy, _BackglassGrillHeight)
                End If
                If formBackglass.TopLightImage4Fantasy IsNot Nothing Then
                    formBackglass.TopLightImage4Fantasy = ResizeImage(formBackglass.TopLightImage4Fantasy, _BackglassGrillHeight)
                End If
                If formBackglass.SecondLightImage4Fantasy IsNot Nothing Then
                    formBackglass.SecondLightImage4Fantasy = ResizeImage(formBackglass.SecondLightImage4Fantasy, _BackglassGrillHeight)
                End If
                If formBackglass.TopAndSecondLightImage4Fantasy IsNot Nothing Then
                    formBackglass.TopAndSecondLightImage4Fantasy = ResizeImage(formBackglass.TopAndSecondLightImage4Fantasy, _BackglassGrillHeight)
                End If

                ' set background image and new backglass form height (without grill)
                formBackglass.BackgroundImage = formBackglass.DarkImage
                formBackglass.Size = formBackglass.DarkImage.Size

            End If

            ' maybe rotate DMD image
            If Me.DMDFlipY AndAlso Me.formDMD IsNot Nothing AndAlso Me.formDMD.BackgroundImage IsNot Nothing Then
                Me.formDMD.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
            End If
        End If

    End Sub

    Private Sub EvalateBackglassScreen()
        On Error Resume Next

        ' get the correct screen
        Me.BackglassScreen = ScreensOrdered(0)
        Dim s As Screen
        Dim currentScreen = 0

        Const S_OK As Integer = &H0

        'searchPathLog.WriteLogEntry("BackglassMonitor " & BackglassMonitor)
        For Each s In ScreensOrdered
            currentScreen += 1
            'searchPathLog.WriteLogEntry("Screen: " & (s.DeviceName) & " Location " & s.Bounds.Location.X & " #" & currentScreen)
            If Left(BackglassMonitor, 1) = "@" Then
                If s.Bounds.Location.X = CInt(Mid(BackglassMonitor, 2)) Then
                    Me.BackglassScreen = s
                    'searchPathLog.WriteLogEntry("Found: @" & (s.Bounds.Location.X))
                    Exit For
                End If
            ElseIf Left(BackglassMonitor, 1) = "=" Then
                If currentScreen = CInt(Mid(BackglassMonitor, 2)) Then
                    Me.BackglassScreen = s
                    Exit For
                End If
            ElseIf s.DeviceName = "\\.\DISPLAY" + BackglassMonitor Then
                'searchPathLog.WriteLogEntry("Found: " & (s.DeviceName))
                Me.BackglassScreen = s
                Exit For
            End If
        Next
        Dim dpiX As UInt32
        Dim dpiY As UInt32
        Dim result As Integer = GetDpiForMonitor(GetMonitorHandle(Me.BackglassScreen), 0, dpiX, dpiY)
        If result = S_OK Then
            ScreenDpiFactor = New SizeF(dpiX / 96.0, dpiY / 96.0)
        End If
        debugLog.WriteLogEntry("DpiFactor =" & ScreenDpiFactor.Width)
        On Error GoTo 0

    End Sub

    Private Sub Show()
        'Dim searchPathLog As Log = New Log("BackglassShow")
        'searchPathLog.IsLogOn = B2SSettings.IsBackglassSearchLogOn

        'searchPathLog.WriteLogEntry("Start Show")

        'On Error Resume Next
        If (Not Me.BackgroundSize.IsEmpty) And ((B2SSettings.StartBackground.HasValue And B2SSettings.StartBackground) Or
                                                (Not B2SSettings.StartBackground.HasValue And B2SSettings.GlobalStartBackground.HasValue And B2SSettings.GlobalStartBackground)) Then
            StartBackground = True
        End If

        ' first of all get the info whether the DMD is to be shown or not
        IsDMDToBeShown = (Me.formDMD IsNot Nothing AndAlso Not Point.Empty.Equals(Me.DMDLocation) AndAlso
            ((Me.DMDViewMode = eDMDViewMode.ShowDMD) OrElse
             (Me.DMDViewMode = eDMDViewMode.ShowDMDOnlyAtDefaultLocation AndAlso Me.DMDAtDefaultLocation) OrElse
             (Me.DMDViewMode = eDMDViewMode.DoNotShowDMDAtDefaultLocation AndAlso Not Me.DMDAtDefaultLocation)))

        'EvalateBackglassScreen()

        ' Westworld show background form, only if background is set and enabled in setting
        Dim DMDKeepBackglassLocation = Me.BackglassLocation
        If StartBackground Then
            If Not VersionTwoFile Then
                Dim swapSize = Me.BackgroundSize
                Dim swapLocation = Me.BackgroundLocation
                Me.BackgroundSize = Me.BackglassSize
                Me.BackglassSize = swapSize
                Me.BackgroundLocation = Me.BackglassLocation
                Me.BackglassLocation = swapLocation
            End If

            Me.formbackground.StartPosition = FormStartPosition.Manual
            Me.formbackground.BackgroundImageLayout = ImageLayout.Stretch
            Me.formbackground.FormBorderStyle = FormBorderStyle.None
            Me.formbackground.ControlBox = False
            Me.formbackground.MaximizeBox = False
            Me.formbackground.MinimizeBox = False
            Me.formbackground.Location = Me.BackglassScreen.Bounds.Location + Me.BackgroundLocation
            Me.formbackground.Size = Me.BackgroundSize
            Me.formbackground.Text = "B2S Backglass Server"
            Me.formbackground.BackColor = Color.Black
            If (File.Exists(Me.BackgroundPath)) Then
                Me.formbackground.BackgroundImage = Image.FromFile(Me.BackgroundPath) ' ("C:\backglass.png")
            End If
            Me.formbackground.Show()
            If Not B2SSettings.PureEXE Then
                If B2SSettings.FormToBack Then
                    Me.formbackground.SendToBack()
                    Me.formbackground.ShowInTaskbar = False
                ElseIf B2SSettings.FormToFront Then
                    Me.formbackground.BringToFront()
                    Me.formbackground.TopMost = True
                    If B2SSettings.FormNoFocus Then Me.formbackground.ShowInTaskbar = False
                Else
                    Me.formbackground.BringToFront()
                End If
            End If
        End If
        ' set forms to background image size
        If formBackglass IsNot Nothing AndAlso formBackglass.BackgroundImage IsNot Nothing Then
            formBackglass.Size = formBackglass.BackgroundImage.Size
        End If
        If Me.formDMD IsNot Nothing AndAlso Me.formDMD.BackgroundImage IsNot Nothing Then
            Me.formDMD.Size = Me.formDMD.BackgroundImage.Size
        End If

        ' calculate backglass rescale factors

        If formBackglass.BackgroundImage IsNot Nothing Then
            BackglassRescaleFactor = New SizeF(CSng(formBackglass.BackgroundImage.Width / Me.BackglassSize.Width), CSng(formBackglass.BackgroundImage.Height / Me.BackglassSize.Height))
        Else
            BackglassRescaleFactor = New SizeF(CSng(formBackglass.Width / Me.BackglassSize.Width), CSng(formBackglass.Height / Me.BackglassSize.Height))
        End If

        ' maybe rescale the location and the size because this is the default and therefore it has to be done
        Dim rescaleDMDX As Single = 1
        Dim rescaleDMDY As Single = 1
        If IsDMDToBeShown Then
            If Me.DMDAtDefaultLocation Then
                Me.DMDSize = Me.formDMD.Size
                If BackglassRescaleFactor.Width <> 1 OrElse BackglassRescaleFactor.Height <> 1 Then
                    Me.DMDLocation = New Point(Int(Me.DMDLocation.X / BackglassRescaleFactor.Width), Int(Me.DMDLocation.Y / BackglassRescaleFactor.Height))
                    Me.DMDSize = New Size(Int(Me.DMDSize.Width / BackglassRescaleFactor.Width), Int(Me.DMDSize.Height / BackglassRescaleFactor.Height))
                End If
            End If

            ' calculate DMD rescale factors
            rescaleDMDX = Me.formDMD.Width / Me.DMDSize.Width
            rescaleDMDY = Me.formDMD.Height / Me.DMDSize.Height
            If Me.formDMD.BackgroundImage IsNot Nothing Then
                rescaleDMDX = Me.formDMD.BackgroundImage.Width / Me.DMDSize.Width
                rescaleDMDY = Me.formDMD.BackgroundImage.Height / Me.DMDSize.Height
            End If
        End If

        ' move and scale all picked objects
        ScaleAllControls(BackglassRescaleFactor.Width, BackglassRescaleFactor.Height, rescaleDMDX, rescaleDMDY)

        ' show the backglass form
        formBackglass.StartPosition = FormStartPosition.Manual
        formBackglass.BackgroundImageLayout = ImageLayout.Stretch
        formBackglass.FormBorderStyle = FormBorderStyle.None
        formBackglass.ControlBox = False
        formBackglass.MaximizeBox = False
        formBackglass.MinimizeBox = False
        formBackglass.Location = Me.BackglassScreen.Bounds.Location + Me.BackglassLocation
        formBackglass.Size = Me.BackglassSize

        If Not B2SSettings.PureEXE Then
            If B2SSettings.FormToFront Then
                ' bring backglass screen to the front and force it to stay
                formBackglass.TopMost = True
                formBackglass.BringToFront()
                If B2SSettings.FormNoFocus Then formBackglass.ShowInTaskbar = False
            ElseIf B2SSettings.FormToBack Then
                ' bring backglass screen to the back and force it to stay
                formBackglass.SendToBack()
                formBackglass.ShowInTaskbar = False
            Else
                formBackglass.BringToFront()
            End If
        End If

        If StartBackground Then
            formBackglass.Text = "B2S Backglass"
            formBackglass.ShowInTaskbar = False

            formBackglass.Show(Me.formbackground)
        Else
            ' Without background picture the backglass is the main form
            formBackglass.Text = "B2S Backglass Server"
            formBackglass.Show()
        End If
        ' maybe show DMD form
        If IsDMDToBeShown Then
            ' set DMD location relative to the backglass location
            Me.formDMD.StartPosition = FormStartPosition.Manual
            Me.formDMD.BackgroundImageLayout = ImageLayout.Stretch
            Me.formDMD.FormBorderStyle = FormBorderStyle.None
            Me.formDMD.ControlBox = False
            Me.formDMD.MaximizeBox = False
            Me.formDMD.MinimizeBox = False
            Me.formDMD.Location = Me.BackglassScreen.Bounds.Location + DMDKeepBackglassLocation + Me.DMDLocation
            Me.formDMD.Size = Me.DMDSize
            Me.formDMD.Text = "B2S DMD"

            If B2SSettings.FormToFront Then
                If B2SSettings.FormNoFocus Then Me.formDMD.ShowInTaskbar = False

                Me.formDMD.BringToFront()
                Me.formDMD.TopMost = True

                If Me.DMDAtDefaultLocation Then
                    ' DMD and Back Glass one unit, make sure they stay together
                    Me.formDMD.Show(formBackglass)
                Else
                    ' DMD and Back Glass separate and accessed separately
                    Me.formDMD.Show()
                End If
            ElseIf B2SSettings.FormToBack Then
                ' DMD and Back Glass one unit, make sure they are together and also make sure it is impossible to activate
                Me.formDMD.ShowInTaskbar = False
                Me.formDMD.SendToBack()
                Me.formDMD.Show(formBackglass)
            Else
                ' show the DMD form without grill
                Me.formDMD.BringToFront()
                Me.formDMD.Show()
            End If
        End If

    End Sub

    Private Sub ScaleAllControls(ByVal _rescaleX As Single, ByVal _rescaleY As Single, ByVal _rescaleDMDX As Single, ByVal _rescaleDMDY As Single)

        ' get scale info for all picked objects and scale some of them
        For Each cntrlinfo As KeyValuePair(Of String, B2SReelBox) In B2SData.Reels
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, If(isOnDMD, _rescaleDMDX, _rescaleX), If(isOnDMD, _rescaleDMDY, _rescaleY), isOnDMD)
        Next
        For Each cntrlinfo As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, If(isOnDMD, _rescaleDMDX, _rescaleX), If(isOnDMD, _rescaleDMDY, _rescaleY), isOnDMD, isOnDMD AndAlso DMDFlipY AndAlso Not DMDAtDefaultLocation)
        Next
        For Each cntrlinfo As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, If(isOnDMD, _rescaleDMDX, _rescaleX), If(isOnDMD, _rescaleDMDY, _rescaleY), isOnDMD, isOnDMD AndAlso DMDFlipY AndAlso Not DMDAtDefaultLocation)
        Next
        For Each cntrlinfo As KeyValuePair(Of String, B2SPictureBox) In B2SData.Illuminations
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, _rescaleX, _rescaleY, isOnDMD)
        Next
        For Each cntrlinfo As KeyValuePair(Of String, B2SPictureBox) In B2SData.DMDIlluminations
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, _rescaleDMDX, _rescaleDMDY, isOnDMD, DMDFlipY AndAlso Not DMDAtDefaultLocation)
        Next

        ' get scale info for the LED areas
        For Each ledarea As KeyValuePair(Of String, B2SData.LEDAreaInfo) In B2SData.LEDAreas
            If ledarea.Value.IsOnDMD Then
                If formDMD IsNot Nothing Then
                    Dim y As Single = ledarea.Value.Rect.Y / _rescaleDMDY
                    If DMDFlipY AndAlso Not DMDAtDefaultLocation Then
                        y = formDMD.Height / _rescaleDMDY - y - ledarea.Value.Rect.Height / _rescaleDMDY
                    End If
                    ledarea.Value.Rect = Rectangle.Round(New RectangleF(ledarea.Value.Rect.X / _rescaleDMDX, y, ledarea.Value.Rect.Width / _rescaleDMDX, ledarea.Value.Rect.Height / _rescaleDMDY))
                End If
            Else
                ledarea.Value.Rect = Rectangle.Round(New RectangleF(ledarea.Value.Rect.X / _rescaleX, ledarea.Value.Rect.Y / _rescaleY, ledarea.Value.Rect.Width / _rescaleX, ledarea.Value.Rect.Height / _rescaleY))
            End If
        Next

        ' and now recalc the backglass cut off rectangle
        If BackglassCutOff <> Nothing Then
            BackglassCutOff = Rectangle.Round(New RectangleF(BackglassCutOff.X / _rescaleX, BackglassCutOff.Y / _rescaleY, BackglassCutOff.Width / _rescaleX, BackglassCutOff.Height / _rescaleY))
        End If

    End Sub

    Private Sub ScaleControl(ByVal _cntrl As B2SBaseBox, ByVal _rescaleX As Single, ByVal _rescaleY As Single, ByVal isOnDMD As Boolean, Optional ByVal flipY As Boolean = False)

        ' calculate the exact location and size of all controls
        If Not isOnDMD AndAlso BackglassCutOff <> Nothing Then
            If BackglassCutOff.IntersectsWith(New Rectangle(_cntrl.Left, _cntrl.Top, _cntrl.Width, _cntrl.Height)) Then
                _cntrl.RectangleF = New RectangleF(0, 0, 0, 0)
            ElseIf BackglassCutOff.Top < _cntrl.Top Then
                _cntrl.RectangleF = New RectangleF(_cntrl.Left / _rescaleX, (_cntrl.Top - Me.BackglassSmallGrillHeight) / _rescaleY, _cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY)
            Else
                _cntrl.RectangleF = New RectangleF(_cntrl.Left / _rescaleX, _cntrl.Top / _rescaleY, _cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY)
            End If
        Else
            _cntrl.RectangleF = New RectangleF(_cntrl.Left / _rescaleX, _cntrl.Top / _rescaleY, _cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY)
        End If

        ' scale not more than the LED and reel boxes
        If TypeOf _cntrl Is B2SLEDBox OrElse TypeOf _cntrl Is B2SReelBox Then
            _cntrl.Location = New Point(Point.Round(_cntrl.RectangleF.Location))
            _cntrl.Size = New Size(Size.Round(_cntrl.RectangleF.Size))
        End If

        ' maybe flip DMD images
        If flipY AndAlso formDMD IsNot Nothing Then
            If TypeOf _cntrl Is B2SLEDBox Then
            Else
                Dim picbox As B2SPictureBox = TryCast(_cntrl, B2SPictureBox)
                If picbox IsNot Nothing Then
                    ' set new top location
                    Dim newY As Single = formDMD.Height / _rescaleY - picbox.RectangleF.Y - picbox.RectangleF.Height
                    picbox.RectangleF = New RectangleF(picbox.RectangleF.X, newY, picbox.RectangleF.Width, picbox.RectangleF.Height)
                    ' flip the images
                    If picbox.BackgroundImage IsNot Nothing Then
                        picbox.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
                    End If
                    If picbox.OffImage IsNot Nothing Then
                        picbox.OffImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
                    End If
                End If
            End If
        End If

    End Sub
    Private Sub ScaleControl(ByVal _cntrl As Dream7Display, ByVal _rescaleX As Single, ByVal _rescaleY As Single, ByVal isOnDMD As Boolean, Optional ByVal flipY As Boolean = False)

        ' store current LED location
        Dim rectF As RectangleF = New RectangleF(New PointF(_cntrl.Left / _rescaleX, _cntrl.Top / _rescaleY), New Size(_cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY))

        ' scale LED display
        _cntrl.Location = Point.Round(New PointF(_cntrl.Left / _rescaleX, _cntrl.Top / _rescaleY))
        _cntrl.Size = Size.Round(New Size(_cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY))
        _cntrl.Spacing = _cntrl.Spacing / _rescaleX
        _cntrl.OffsetWidth = _cntrl.OffsetWidth / _rescaleX
        _cntrl.Thickness = _cntrl.Thickness / _rescaleX

        ' maybe flip LEDs
        If flipY Then
            _cntrl.Mirrored = True
            ' set new top location
            If formDMD IsNot Nothing Then
                Dim newY As Single = formDMD.Height / _rescaleY - rectF.Y - rectF.Height
                _cntrl.Location = Point.Round(New PointF(rectF.X, newY))
            End If
        End If

    End Sub

    Private Function ResizeImage(ByVal sourceimage As Image, ByVal grillheight As Integer) As Image
        Dim imageWithoutGrill As Bitmap = New Bitmap(sourceimage.Width, sourceimage.Height - grillheight)
        Using gr As Graphics = Graphics.FromImage(imageWithoutGrill)
            gr.DrawImage(sourceimage, New Rectangle(0, 0, imageWithoutGrill.Width, imageWithoutGrill.Height + grillheight))
        End Using
        Return imageWithoutGrill
    End Function
    Private Function CutOutImage(ByVal sourceimage As Image, ByVal grillheight As Integer, ByVal smallgrillheight As Integer) As Image
        Dim imageWithoutSmallGrill As Bitmap = New Bitmap(sourceimage.Width, sourceimage.Height - smallgrillheight)
        Using imageBackglass As Bitmap = PartFromImage(sourceimage, New Rectangle(0, 0, sourceimage.Width, sourceimage.Height - grillheight - smallgrillheight))
            Using imageGrill As Bitmap = PartFromImage(sourceimage, New Rectangle(0, sourceimage.Height - grillheight, sourceimage.Width, grillheight))
                Using gr As Graphics = Graphics.FromImage(imageWithoutSmallGrill)
                    gr.DrawImage(imageBackglass, New Rectangle(0, 0, imageBackglass.Width, imageBackglass.Height))
                    gr.DrawImage(imageGrill, New Rectangle(0, imageBackglass.Height, imageGrill.Width, imageGrill.Height))
                End Using
            End Using
        End Using
        Return imageWithoutSmallGrill
    End Function
    Private Function PartFromImage(image As Image, area As Rectangle) As Image
        If image Is Nothing Then Return Nothing
        Dim imageBackground As Bitmap = CType(image, Bitmap)
        Dim imagePart As Image = New Bitmap(area.Width, area.Height)
        imagePart = imageBackground.Clone(area, Imaging.PixelFormat.Format32bppArgb)
        Return imagePart
    End Function

#End Region


#Region "screenshot stuff"

    Public Function MakeScreenShot(ByVal filename As String,
                                   ByVal fileformat As Imaging.ImageFormat) As Boolean

        ' get screenshot
        Dim screenshot As Bitmap = New Bitmap(formBackglass.Width, formBackglass.Height)
        Using gr As Graphics = Graphics.FromImage(screenshot)
            gr.CopyFromScreen(formBackglass.Location, Point.Empty, formBackglass.Size)
        End Using

        ' save it
        Try
            screenshot.Save(filename, fileformat)
        Catch ex As Exception
            MessageBox.Show(ex.Message, My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            screenshot.Dispose()
            screenshot = Nothing
        End Try

        Return True

    End Function

#End Region
    <DllImport("Shcore.dll")>
    Private Shared Function GetDpiForMonitor(hmonitor As IntPtr, dpiType As Integer, ByRef dpiX As UInteger, ByRef dpiY As UInteger) As Integer
    End Function

    <DllImport("User32.dll")>
    Private Shared Function MonitorFromPoint(pt As Point, dwFlags As UInteger) As IntPtr
    End Function

    Private Const MONITOR_DEFAULTTONEAREST As UInteger = 2

    Public Shared Function GetMonitorHandle(screen As Screen) As IntPtr
        ' Verwenden Sie die linke obere Ecke des Bildschirms
        Return MonitorFromPoint(screen.Bounds.Location, MONITOR_DEFAULTTONEAREST)
    End Function
End Class
