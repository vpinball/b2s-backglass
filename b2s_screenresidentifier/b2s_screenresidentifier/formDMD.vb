Imports System.Text

Public Class formDMD

    Friend formBackglass As formBackglass = Nothing

    Private Sub formBackglass_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate
        If Me.ActiveControl IsNot Nothing Then
            Select Case Me.ActiveControl.Name
                Case "txtDMDLocationX"
                    txtDMDLocationX_Validated(Me, New EventArgs)
                Case "txtDMDLocationY"
                    txtDMDLocationY_Validated(Me, New EventArgs)
                Case "txtDMDSizeWidth"
                    txtDMDSizeWidth_Validated(Me, New EventArgs)
                Case "txtDMDSizeHeight"
                    txtDMDSizeHeight_Validated(Me, New EventArgs)
            End Select
        End If
    End Sub

    Private Sub formDMD_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
    End Sub

    Private Sub formDMD_LocationChanged(sender As Object, e As System.EventArgs) Handles Me.LocationChanged
        chkDMDAtDefaultLocation.Checked = False
        DMDInfo(Me)
    End Sub
    Private Sub formDMD_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        chkDMDAtDefaultLocation.Checked = False
        DMDInfo(Me)
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

    Private Sub txtDMDLocationX_Validated(sender As System.Object, e As System.EventArgs) Handles txtDMDLocationX.Validated
        If Not String.IsNullOrEmpty(txtDMDLocationX.Text) AndAlso IsNumeric(txtDMDLocationX.Text) Then
            IsDirty = True
            Me.Location = New Point(txtDMDLocationX.Text, Me.Location.Y)
        End If
    End Sub
    Private Sub txtDMDLocationY_Validated(sender As System.Object, e As System.EventArgs) Handles txtDMDLocationY.Validated
        If Not String.IsNullOrEmpty(txtDMDLocationY.Text) AndAlso IsNumeric(txtDMDLocationY.Text) Then
            IsDirty = True
            Me.Location = New Point(Me.Location.X, txtDMDLocationY.Text)
        End If
    End Sub
    Private Sub txtDMDSizeWidth_Validated(sender As System.Object, e As System.EventArgs) Handles txtDMDSizeWidth.Validated
        If Not String.IsNullOrEmpty(txtDMDSizeWidth.Text) AndAlso IsNumeric(txtDMDSizeWidth.Text) Then
            IsDirty = True
            Me.Size = New Size(txtDMDSizeWidth.Text, Me.Size.Height)
        End If
    End Sub
    Private Sub txtDMDSizeHeight_Validated(sender As System.Object, e As System.EventArgs) Handles txtDMDSizeHeight.Validated
        If Not String.IsNullOrEmpty(txtDMDSizeHeight.Text) AndAlso IsNumeric(txtDMDSizeHeight.Text) Then
            IsDirty = True
            Me.Size = New Size(Me.Size.Width, txtDMDSizeHeight.Text)
        End If
    End Sub

    Private Sub chkDMDAtDefaultLocation_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDMDAtDefaultLocation.CheckedChanged
        DMDInfo(Me)
        If formBackglass IsNot Nothing Then formBackglass.chkBackglassGrillVisible.Checked = chkDMDAtDefaultLocation.Checked
    End Sub

    Private Sub DMDInfo(ByRef form As formDMD)
        IsDirty = True

        Dim currentScreen As Screen = Screen.FromControl(form)
        Dim screenCount As Integer = Screen.AllScreens.Count

        ' DMD
        txtDMDScreen.Text = ShortDevice(currentScreen.DeviceName)
        txtDMDScreenSizeWidth.Text = currentScreen.Bounds.Width
        txtDMDScreenSizeHeight.Text = currentScreen.Bounds.Height
        If chkDMDAtDefaultLocation.Checked Then
            txtDMDLocationX.Text = ""
            txtDMDLocationY.Text = ""
            txtDMDSizeWidth.Text = ""
            txtDMDSizeHeight.Text = ""
        Else
            txtDMDLocationX.Text = form.Location.X - currentScreen.Bounds.Location.X
            txtDMDLocationY.Text = form.Location.Y - currentScreen.Bounds.Location.Y
            txtDMDSizeWidth.Text = form.Size.Width
            txtDMDSizeHeight.Text = form.Size.Height
        End If
    End Sub

    Public Sub OnValidate(ByVal scr As Screen, ByVal _DMDAtDefaultLocation As Boolean)
        IsDirty = True
        If chkDMDAtDefaultLocation.Checked Then
            Dim width As Integer = Math.Max(CInt(formBackglass.Size.Width / 2), 425)
            Dim height As Integer = Math.Max(CInt(formBackglass.Size.Height / 3), 260)
            Dim x As Integer = If((scr IsNot Nothing), 0, formBackglass.Location.X) + width / 2
            Dim y As Integer = If((scr IsNot Nothing), 0, formBackglass.Location.Y) + 2 * formBackglass.Size.Height / 3
            Me.Location = If((scr IsNot Nothing), scr.Bounds.Location, New Point(0, 0)) + New Point(x, y)
            Me.Size = New Size(width, height)
            chkDMDAtDefaultLocation.Checked = _DMDAtDefaultLocation
        End If
    End Sub

End Class