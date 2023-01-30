Imports System
Imports System.Windows.Forms

Public Class formBackground
    Friend formBackglass As formBackglass = Nothing

    Private Sub formBackglass_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate
        If Me.ActiveControl IsNot Nothing Then
            Select Case Me.ActiveControl.Name
                Case "txtBackgroundLocationX"
                    txtBackgroundLocationX_Validated(Me, New EventArgs)
                Case "txtBackgroundLocationY"
                    txtBackgroundLocationY_Validated(Me, New EventArgs)
                Case "txtBackgroundSizeWidth"
                    txtBackgroundSizeWidth_Validated(Me, New EventArgs)
                Case "txtBackgroundSizeHeight"
                    txtBackgroundSizeHeight_Validated(Me, New EventArgs)
            End Select
        End If
    End Sub

    Private Sub formBackground_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        BackgroundActive = False
        'formBackglass.BackgroundActiveCheckBox.Checked = False
    End Sub
    Private Sub formPlayfield_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        IsDirty = False
        TxtBackgroundPath.Text = BackgroundPath
    End Sub

    Private Sub formBackground_LocationChanged(sender As Object, e As System.EventArgs) Handles Me.LocationChanged
        BackgroundInfo(Me)
    End Sub
    Private Sub formBackground_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        BackgroundInfo(Me)
    End Sub

    Private Sub formBackground_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right Then
            If e.Shift Then
                SizeMe(Me, e.KeyCode)
            ElseIf Not e.Shift AndAlso Not e.Alt AndAlso Not e.Control Then
                MoveMe(Me, e.KeyCode)
            End If
        End If
    End Sub

    Private Sub txtBackgroundLocationX_Validated(sender As System.Object, e As System.EventArgs) Handles txtBackgroundLocationX.Validated
        If Not String.IsNullOrEmpty(txtBackgroundLocationX.Text) AndAlso IsNumeric(txtBackgroundLocationX.Text) Then
            Dim currentScreen As Screen = Screen.FromControl(Me)

            IsDirty = True
            Me.Location = New Point(txtBackgroundLocationX.Text + currentScreen.Bounds.Location.X, Me.Location.Y)
        End If
    End Sub
    Private Sub txtBackgroundLocationY_Validated(sender As System.Object, e As System.EventArgs) Handles txtBackgroundLocationY.Validated
        If Not String.IsNullOrEmpty(txtBackgroundLocationY.Text) AndAlso IsNumeric(txtBackgroundLocationY.Text) Then
            Dim currentScreen As Screen = Screen.FromControl(Me)

            IsDirty = True
            Me.Location = New Point(Me.Location.X, txtBackgroundLocationY.Text + currentScreen.Bounds.Location.Y)
        End If
    End Sub
    Private Sub txtBackgroundSizeWidth_Validated(sender As System.Object, e As System.EventArgs) Handles txtBackgroundSizeWidth.Validated
        If Not String.IsNullOrEmpty(txtBackgroundSizeWidth.Text) AndAlso IsNumeric(txtBackgroundSizeWidth.Text) Then
            IsDirty = True
            Me.Size = New Size(txtBackgroundSizeWidth.Text, Me.Size.Height)
        End If
    End Sub
    Private Sub txtBackgroundSizeHeight_Validated(sender As System.Object, e As System.EventArgs) Handles txtBackgroundSizeHeight.Validated
        If Not String.IsNullOrEmpty(txtBackgroundSizeHeight.Text) AndAlso IsNumeric(txtBackgroundSizeHeight.Text) Then
            IsDirty = True
            Me.Size = New Size(Me.Size.Width, txtBackgroundSizeHeight.Text)
        End If
    End Sub

    Private Sub chkBackgroundAtDefaultLocation_CheckedChanged(sender As System.Object, e As System.EventArgs)
        BackgroundInfo(Me)
        'If formBackglass IsNot Nothing Then formBackglass.chkBackglassGrillVisible.Checked = chkBackgroundAtDefaultLocation.Checked
    End Sub

    Private Sub BackgroundInfo(ByRef form As formBackground)
        IsDirty = True

        Dim currentScreen As Screen = Screen.FromControl(form)

        ' Background
        If String.Compare(txtBackgroundScreen.Text, ShortDevice(currentScreen.DeviceName)) <> 0 Then
            Dim screenSize As Size
            Dim dpi As Integer

            screenSize = TrueResolution(currentScreen.DeviceName)
            txtBackgroundScreenSizeWidth.Text = screenSize.Width
            txtBackgroundScreenSizeHeight.Text = screenSize.Height

            dpi = 100 * screenSize.Width / currentScreen.Bounds.Width

            txtBackgroundScreenScale.Text = dpi & "%"
            If dpi <> 100 Then
                txtBackgroundScreenScale.BackColor = Color.Red
            Else
                txtBackgroundScreenScale.BackColor = txtBackgroundScreenSizeHeight.BackColor
            End If
        End If

        txtBackgroundScreen.Text = ShortDevice(currentScreen.DeviceName)

        txtBackgroundLocationX.Text = form.Location.X - currentScreen.Bounds.Location.X
        txtBackgroundLocationY.Text = form.Location.Y - currentScreen.Bounds.Location.Y
        txtBackgroundSizeWidth.Text = form.Size.Width
        txtBackgroundSizeHeight.Text = form.Size.Height

    End Sub

    Public Sub ReValidate(posX As String, posY As String, width As String, height As String)

        txtBackgroundLocationX.Text = posX
        txtBackgroundLocationX_Validated(Me, New EventArgs)
        txtBackgroundLocationY.Text = posY
        txtBackgroundLocationY_Validated(Me, New EventArgs)
        txtBackgroundSizeWidth.Text = width
        txtBackgroundSizeWidth_Validated(Me, New EventArgs)
        txtBackgroundSizeHeight.Text = height
        txtBackgroundSizeHeight_Validated(Me, New EventArgs)


    End Sub
    Public Sub OnValidate(ByVal scr As Screen, ByVal _BackgroundAtDefaultLocation As Boolean)
        IsDirty = True
        'If chkBackgroundAtDefaultLocation.Checked Then
        'Dim width As Integer = Math.Max(CInt(formBackglass.Size.Width / 2), 425)
        'Dim height As Integer = Math.Max(CInt(formBackglass.Size.Height / 3), 260)
        'Dim x As Integer = If((scr IsNot Nothing), 0, formBackglass.Location.X) + Width / 2
        'Dim y As Integer = If((scr IsNot Nothing), 0, formBackglass.Location.Y) + 2 * formBackglass.Size.Height / 3
        'Me.Location = If((scr IsNot Nothing), scr.Bounds.Location, New Point(0, 0)) + New Point(x, y)
        'Me.Size = New Size(Width, Height)

        'End If
    End Sub

End Class