Imports System.Text

Module Module1

    Public Const FileName As String = "ScreenRes.txt"

    Public ReadOnly screenCount As Integer = Screen.AllScreens.Count

    Public Property IsInStartup() As Boolean = False

    Public Property FileFound() As Boolean = False
    Public Property PlayfieldSize() As Size = New Size(0, 0)
    Public Property BackglassMonitor() As String = String.Empty
    Public Property BackglassSize() As Size = New Size(0, 0)
    Public Property BackglassLocation() As Point = New Point(0, 0)
    Public Property BackglassGrillHeight() As Integer = 0
    Public Property DMDSize() As Size = New Size(0, 0)
    Public Property DMDLocation() As Point = New Point(0, 0)
    Public Property DMDFlipY() As Boolean = False
 
    Public Function ShortDevice(ByVal device As String) As String
        Return device.Replace("\\", "").Replace(".\", "")
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
        If IO.File.Exists(FileName) Then
            FileFound = True

            ' open settings file
            FileOpen(1, FileName, OpenMode.Input)

            ' get all settings
            Dim line(50) As String
            Dim i As Integer = 0
            Do Until EOF(1)
                line(i) = LineInput(1)
                i += 1
            Loop
            line(i) = 0
            line(i + 1) = 0
            PlayfieldSize = New Size(CInt(line(0)), CInt(line(1)))
            BackglassSize = New Size(CInt(line(2)), CInt(line(3)))
            BackglassMonitor = "\\.\DISPLAY" + line(4)
            BackglassLocation = New Point(CInt(line(5)), CInt(line(6)))
            DMDSize = New Size(CInt(line(7)), CInt(line(8)))
            DMDLocation = New Point(CInt(line(9)), CInt(line(10)))
            DMDFlipY = (Trim(line(11)) = "1")

            ' close file handle
            FileClose(1)
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
