Imports System.Text

Public Class formBackglass

    Public ReadOnly Property BackglassScreenNo() As Integer
        Get
            Dim device As String = Screen.FromControl(Me).DeviceName
            Return CInt(device.Substring(11, 1))
        End Get
    End Property

    Private Sub formBackglass_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate
        If Me.ActiveControl IsNot Nothing Then
            Select Case Me.ActiveControl.Name
                Case "txtBackglassLocationX"
                    txtBackglassLocationX_Validated(Me, New EventArgs)
                Case "txtBackglassLocationY"
                    txtBackglassLocationY_Validated(Me, New EventArgs)
                Case "txtBackglassSizeWidth"
                    txtBackglassSizeWidth_Validated(Me, New EventArgs)
                Case "txtBackglassSizeHeight"
                    txtBackglassSizeHeight_Validated(Me, New EventArgs)
            End Select
        End If
    End Sub

    Private Sub formBackglass_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
    End Sub

    Private Sub formBackglass_LocationChanged(sender As Object, e As System.EventArgs) Handles Me.LocationChanged
        BackglassInfo(Me)
    End Sub
    Private Sub formBackglass_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        BackglassInfo(Me)
    End Sub

    Private Sub formPlayfield_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right Then
            If e.Shift Then
                SizeMe(Me, e.KeyCode)
            ElseIf Not e.Shift AndAlso Not e.Alt AndAlso Not e.Control Then
                MoveMe(Me, e.KeyCode)
            End If
        End If
    End Sub

    Private Sub txtBackglassLocationX_Validated(sender As System.Object, e As System.EventArgs) Handles txtBackglassLocationX.Validated
        If Not String.IsNullOrEmpty(txtBackglassLocationX.Text) AndAlso IsNumeric(txtBackglassLocationX.Text) Then
            IsDirty = True
            Me.Location = New Point(txtBackglassLocationX.Text, Me.Location.Y)
        End If
    End Sub
    Private Sub txtBackglassLocationY_Validated(sender As System.Object, e As System.EventArgs) Handles txtBackglassLocationY.Validated
        If Not String.IsNullOrEmpty(txtBackglassLocationY.Text) AndAlso IsNumeric(txtBackglassLocationY.Text) Then
            IsDirty = True
            Me.Location = New Point(Me.Location.X, txtBackglassLocationY.Text)
        End If
    End Sub
    Private Sub txtBackglassSizeWidth_Validated(sender As System.Object, e As System.EventArgs) Handles txtBackglassSizeWidth.Validated
        If Not String.IsNullOrEmpty(txtBackglassSizeWidth.Text) AndAlso IsNumeric(txtBackglassSizeWidth.Text) Then
            IsDirty = True
            Me.Size = New Size(txtBackglassSizeWidth.Text, Me.Size.Height)
        End If
    End Sub
    Private Sub txtBackglassSizeHeight_Validated(sender As System.Object, e As System.EventArgs) Handles txtBackglassSizeHeight.Validated
        If Not String.IsNullOrEmpty(txtBackglassSizeHeight.Text) AndAlso IsNumeric(txtBackglassSizeHeight.Text) Then
            IsDirty = True
            Me.Size = New Size(Me.Size.Width, txtBackglassSizeHeight.Text)
        End If
    End Sub

    Private Sub chkBackglassFullSize_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBackglassFullSize.CheckedChanged
        IsDirty = True
        Me.WindowState = If(chkBackglassFullSize.Checked, FormWindowState.Maximized, FormWindowState.Normal)
    End Sub

    Private Sub BackglassInfo(ByRef form As formBackglass)
        IsDirty = True

        Dim currentScreen As Screen = Screen.FromControl(form)
        Dim screenCount As Integer = Screen.AllScreens.Count

        ' backglass
        txtBackglassScreen.Text = ShortDevice(currentScreen.DeviceName)
        txtBackglassScreenSizeWidth.Text = currentScreen.Bounds.Width
        txtBackglassScreenSizeHeight.Text = currentScreen.Bounds.Height
        chkBackglassFullSize.Checked = (form.WindowState = FormWindowState.Maximized)
        If form.WindowState = FormWindowState.Maximized Then
            txtBackglassLocationX.Text = "0"
            txtBackglassLocationY.Text = "0"
            txtBackglassSizeWidth.Text = currentScreen.Bounds.Width
            txtBackglassSizeHeight.Text = currentScreen.Bounds.Height
            txtBackglassLocationX.ReadOnly = True
            txtBackglassLocationY.ReadOnly = True
            txtBackglassSizeWidth.ReadOnly = True
            txtBackglassSizeHeight.ReadOnly = True
        Else
            txtBackglassLocationX.Text = form.Location.X - currentScreen.Bounds.Location.X
            txtBackglassLocationY.Text = form.Location.Y - currentScreen.Bounds.Location.Y
            txtBackglassSizeWidth.Text = form.Size.Width
            txtBackglassSizeHeight.Text = form.Size.Height
            txtBackglassLocationX.ReadOnly = False
            txtBackglassLocationY.ReadOnly = False
            txtBackglassSizeWidth.ReadOnly = False
            txtBackglassSizeHeight.ReadOnly = False
        End If
    End Sub

    Public Sub OnValidate(ByVal scr As Screen)
        IsDirty = True
        Dim width As Integer = CInt(txtBackglassSizeWidth.Text)
        Dim height As Integer = CInt(txtBackglassSizeHeight.Text)
        Me.Location = If((scr IsNot Nothing), scr.Bounds.Location, New Point(0, 0)) + New Point(txtBackglassLocationX.Text, txtBackglassLocationY.Text)
        Me.Size = New Size(width, height)
    End Sub

End Class