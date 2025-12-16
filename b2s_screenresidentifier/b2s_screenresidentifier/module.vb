Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.Win32

Module Module1
    Private Const DESKTOPVERTRES As Integer = &H75
    Private Const DESKTOPHORZRES As Integer = &H76
    Public Property GlobalFileName As String = SafeReadRegistry("Software\B2S", "B2SScreenResFileNameOverride", "ScreenRes.txt")
    Public Property FileName As String = GlobalFileName
    Public ReadOnly screenCount As Integer = Screen.AllScreens.Count
    Public Property ScreensOrdered() = Screen.AllScreens.OrderBy(Function(sc) sc.Bounds.Location.X).ToArray()

    Public Property IsInStartup() As Boolean = False

    Public Property FileFound() As Boolean = False
    Public Property PureEXE() As Nullable(Of Boolean) = Nothing
    Public Property PlayfieldSize() As Size = New Size(0, 0)
    Public Property BackglassMonitor() As String = String.Empty
    Public Property BackglassMonitorType() As String = String.Empty
    Public Property BackglassSize() As Size = New Size(0, 0)
    Public Property BackglassLocation() As Point = New Point(0, 0)
    Public Property BackglassGrillHeight() As Integer = 0
    Public Property DMDSize() As Size = New Size(0, 0)
    Public Property DMDLocation() As Point = New Point(0, 0)
    Public Property DMDFlipY() As Boolean = False
    Public Property BackgroundSize() As Size = New Size(0, 0)
    Public Property BackgroundActive() As Boolean = True
    Public Property BackgroundLocation() As Point = New Point(0, 0)
    Public Property BackgroundPath() As String = String.Empty
    Public Property SaveComments() As Boolean = False
    Public Property VersionTwoFile() As Boolean = False

    Private Declare Function GetDeviceCaps Lib "gdi32" (ByVal hdc As IntPtr, ByVal nIndex As Integer) As Integer
    Private Declare Function CreateDCA Lib "gdi32" (lpszDriver As String, lpszDevice As String, lpszOutput As String, lpInitData As IntPtr) As Integer
    Private Declare Function DeleteDC Lib "gdi32" (ByVal hdc As IntPtr) As Boolean
    Public Function ShortDevice(ByVal device As String) As String
        Return device.Replace("\\", "").Replace(".\", "")
    End Function

    Public Function SafeReadRegistry(ByVal keyname As String, ByVal valuename As String, ByVal defaultvalue As String) As String
        '    Public Property GlobalFileName As String = SafeReadRegistry("Software\B2S", "B2SScreenResFileNameOverride", "ScreenRes.txt")

        Try
            Return Registry.CurrentUser.OpenSubKey(keyname).GetValue(valuename, defaultvalue)
        Catch ex As Exception
            Return defaultvalue
        End Try
    End Function


    Public Function TrueResolution(ByVal hwnd As IntPtr)
        Using g As Graphics = Graphics.FromHwnd(hwnd)
            Dim hdc As IntPtr = g.GetHdc
            Dim TrueScreenSize As New Size(GetDeviceCaps(hdc, DESKTOPHORZRES), GetDeviceCaps(hdc, DESKTOPVERTRES))
            g.ReleaseHdc(hdc)

            Return TrueScreenSize
        End Using

    End Function
    Public Function TrueResolution(ByVal deviceName As String)
        Dim screen As IntPtr = CreateDCA(deviceName, Nothing, Nothing, 0)
        Dim TrueScreenSize As New Size(GetDeviceCaps(screen, DESKTOPHORZRES), GetDeviceCaps(screen, DESKTOPVERTRES))
        DeleteDC(screen)

        Return TrueScreenSize

    End Function

    Private _IsDirty As Boolean = False
    Public Property IsDirty() As Boolean
        Get
            Return _IsDirty
        End Get
        Set(ByVal value As Boolean)
            _IsDirty = value
        End Set
    End Property

    ''' <summary>
    ''' Calculates an integer value from a string, which may be a percentage (e.g., "50%") or an absolute value.
    ''' </summary>
    Public Function CalcValue(StringValue As String, totalValue As Integer) As Integer
        If StringValue Is Nothing Then Return 0

        StringValue = StringValue.Trim()
        If StringValue.EndsWith("%") Then
            Dim percentStr As String = StringValue.Substring(0, StringValue.Length - 1)
            Dim percentValue As Double
            If Double.TryParse(percentStr, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, percentValue) Then
                Return CInt(totalValue * percentValue / 100.0)
            Else
                Return 0
            End If
        Else
            Dim intValue As Integer
            If Integer.TryParse(StringValue, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, intValue) Then
                Return intValue
            Else
                Return 0
            End If
        End If
    End Function

    ''' <summary>
    ''' Checks if a string is a valid value (integer or percent).
    ''' </summary>
    Public Function IsValidValue(value As String) As Boolean
        If String.IsNullOrWhiteSpace(value) Then Return False

        value = value.Trim()

        ' Check for percent value
        If value.EndsWith("%") Then
            Dim percentStr As String = value.Substring(0, value.Length - 1)
            Dim percentValue As Double
            Return Double.TryParse(percentStr, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, percentValue)
        End If

        ' Check for absolute integer value
        Dim intValue As Integer
        Return Integer.TryParse(value, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, intValue)
    End Function

    Public Sub GetSettings(ResFileName)
        If IO.File.Exists(ResFileName) Then
            FileFound = True

            ' open settings file
            FileOpen(1, ResFileName, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)

            ' get all settings
            Dim line(50) As String
            Dim i As Integer = 0
            Do Until EOF(1) Or i > 30
                line(i) = LineInput(1)
                If (line(i).StartsWith("#")) Then
                    If (line(i).Replace(" ", "").StartsWith("#V2")) Then
                        VersionTwoFile = True
                    Else
                        SaveComments = True
                    End If
                    Continue Do
                End If
                i += 1
            Loop
            ' close file handle
            FileClose(1)

            line(i) = 0
            line(i + 1) = 0
            
            ' Get the primary (leftmost) screen for playfield calculations
            Dim primaryScreen As Screen = ScreensOrdered(0)
            
            ' Calculate playfield size (uses leftmost screen as reference for percent values)
            PlayfieldSize = New Size(CalcValue(line(0), primaryScreen.Bounds.Width), CalcValue(line(1), primaryScreen.Bounds.Height))
            
            ' First get the backglass monitor info to determine which screen to use
            BackglassMonitor = line(4)
            BackglassMonitorType = ""
            If BackglassMonitor.StartsWith("@") Or BackglassMonitor.StartsWith("=") Then
                BackglassMonitorType = Mid(BackglassMonitor, 1, 1)
                BackglassMonitor = BackglassMonitor.Substring(1)
            End If
            
            ' Find the backglass screen
            Dim backglassScreen As Screen = primaryScreen
            Dim currentScreen = 0
            For Each scr As Screen In ScreensOrdered
                currentScreen += 1
                If (BackglassMonitorType = "" AndAlso scr.DeviceName = "\\.\DISPLAY" + BackglassMonitor) Or
                   (BackglassMonitorType = "@" AndAlso scr.Bounds.Location.X = CInt(BackglassMonitor)) Or
                   (BackglassMonitorType = "=" AndAlso currentScreen = CInt(BackglassMonitor)) Then
                    backglassScreen = scr
                    Exit For
                End If
            Next
            
            ' Calculate backglass values (uses selected backglass screen as reference for percent values)
            BackglassSize = New Size(CalcValue(line(2), backglassScreen.Bounds.Width), CalcValue(line(3), backglassScreen.Bounds.Height))
            BackglassLocation = New Point(CalcValue(line(5), backglassScreen.Bounds.Width), CalcValue(line(6), backglassScreen.Bounds.Height))
            DMDSize = New Size(CalcValue(line(7), backglassScreen.Bounds.Width), CalcValue(line(8), backglassScreen.Bounds.Height))
            DMDLocation = New Point(CalcValue(line(9), backglassScreen.Bounds.Width), CalcValue(line(10), backglassScreen.Bounds.Height))
            DMDFlipY = (Trim(line(11)) = "1")

            If (i > 15) Then
                BackgroundLocation = New Point(CalcValue(line(12), backglassScreen.Bounds.Width), CalcValue(line(13), backglassScreen.Bounds.Height))
                BackgroundSize = New Size(CalcValue(line(14), backglassScreen.Bounds.Width), CalcValue(line(15), backglassScreen.Bounds.Height))
                BackgroundPath = line(16)
            Else
                BackgroundLocation = New Point(0, 0)
                BackgroundSize = New Size(0, 0)
                BackgroundPath = ""
            End If
            BackgroundActive = Not BackgroundSize.IsEmpty

        End If
    End Sub

    Public Sub MoveMe(ByRef form As Form, ByVal keyCode As Windows.Forms.Keys)
        If form.WindowState <> FormWindowState.Maximized Then
            Select Case keyCode
                Case Keys.Down
                    form.Location = New Point(form.Location.X, form.Location.Y + 1)
                Case Keys.Up
                    form.Location = New Point(form.Location.X, form.Location.Y - 1)
                Case Keys.Left
                    form.Location = New Point(form.Location.X - 1, form.Location.Y)
                Case Keys.Right
                    form.Location = New Point(form.Location.X + 1, form.Location.Y)
            End Select
        End If
    End Sub

    Public Sub SizeMe(ByRef form As Form, ByVal keyCode As Windows.Forms.Keys)
        If form.WindowState <> FormWindowState.Maximized Then
            Select Case keyCode
                Case Keys.Down
                    form.Size = New Size(form.Size.Width, form.Size.Height + 1)
                Case Keys.Up
                    form.Size = New Size(form.Size.Width, form.Size.Height - 1)
                Case Keys.Left
                    form.Size = New Size(form.Size.Width - 1, form.Size.Height)
                Case Keys.Right
                    form.Size = New Size(form.Size.Width + 1, form.Size.Height)
            End Select
        End If
    End Sub

    Public Class B2STextBox

        Inherits TextBox

        Private oldValue As String

        Protected Overrides Sub OnGotFocus(e As System.EventArgs)
            oldValue = Me.Text
            If Not IsNumeric(oldValue) Then
                oldValue = "200"
            End If
            MyBase.OnGotFocus(e)
        End Sub

        Protected Overrides Sub OnLostFocus(e As System.EventArgs)
            If Not IsNumeric(Me.Text) Then
                Me.Text = oldValue
            End If
            MyBase.OnLostFocus(e)
        End Sub

    End Class

End Module
