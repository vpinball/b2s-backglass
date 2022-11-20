Imports System.Text
Imports Microsoft.Win32

Module Module1
    Private Const DESKTOPVERTRES As Integer = &H75
    Private Const DESKTOPHORZRES As Integer = &H76
    Public Property FileName As String = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\B2S").GetValue("B2SScreenResFileNameOverride", "ScreenRes.txt")

    Public ReadOnly screenCount As Integer = Screen.AllScreens.Count
    Public Property ScreensOrdered() = Screen.AllScreens.OrderBy(Function(sc) sc.Bounds.Location.X).ToArray()

    Public Property IsInStartup() As Boolean = False

    Public Property FileFound() As Boolean = False
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
    Public Property BackgroundLocation() As Point = New Point(0, 0)
    Public Property BackgroundPath() As String = String.Empty
    Public Property SaveComments() As Boolean = False
    Private Declare Function GetDeviceCaps Lib "gdi32" (ByVal hdc As IntPtr, ByVal nIndex As Integer) As Integer
    Private Declare Function CreateDCA Lib "gdi32" (lpszDriver As String, lpszDevice As String, lpszOutput As String, lpInitData As IntPtr) As Integer
    Private Declare Function DeleteDC Lib "gdi32" (ByVal hdc As IntPtr) As Boolean
    Public Function ShortDevice(ByVal device As String) As String
        Return device.Replace("\\", "").Replace(".\", "")
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

    Public Sub GetSettings()
        If My.Application.CommandLineArgs.Count > 0 AndAlso IO.File.Exists(My.Application.CommandLineArgs.ElementAt(0)) Then
            FileName = My.Application.CommandLineArgs.ElementAt(0)
        End If
        If IO.File.Exists(FileName) Then
            FileFound = True

            ' open settings file
            FileOpen(1, FileName, OpenMode.Input)

            ' get all settings
            Dim line(50) As String
            Dim i As Integer = 0
            Do Until EOF(1) Or i > 20
                line(i) = LineInput(1)
                If (line(i).StartsWith("#")) Then
                    SaveComments = True
                    Continue Do
                End If
                i += 1
            Loop
            ' close file handle
            FileClose(1)

            line(i) = 0
            line(i + 1) = 0
            PlayfieldSize = New Size(CInt(line(0)), CInt(line(1)))
            BackglassSize = New Size(CInt(line(2)), CInt(line(3)))
            BackglassMonitor = line(4)

            If BackglassMonitor.StartsWith("@") Or BackglassMonitor.StartsWith("=") Then
                BackglassMonitorType = Mid(BackglassMonitor, 1, 1)
                BackglassMonitor = BackglassMonitor.Substring(1)
            End If

            BackglassLocation = New Point(CInt(line(5)), CInt(line(6)))
            DMDSize = New Size(CInt(line(7)), CInt(line(8)))
            DMDLocation = New Point(CInt(line(9)), CInt(line(10)))
            DMDFlipY = (Trim(line(11)) = "1")

            If (i > 15) Then
                BackgroundLocation = New Point(CInt(line(12)), CInt(line(13)))
                BackgroundSize = New Size(CInt(line(14)), CInt(line(15)))
                BackgroundPath = line(16)
            Else
                BackgroundLocation = New Point(0, 0)
                BackgroundSize = New Point(0, 0)
                BackgroundPath = ""
            End If

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
