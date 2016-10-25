Imports System.Text

Public Class formPlayfield

    Private formBackglass As formBackglass = Nothing
    Private formDMD As formDMD = Nothing

    Private Sub formPlayfield_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        formBackglass = New formBackglass
        formDMD = New formDMD
        formDMD.formBackglass = formBackglass
        ' get all saved data
        GetSettings()
        StartupPlayfield()
        ' create all other forms
        formBackglass.Show()
        formDMD.Show()
        IsDirty = False
    End Sub

    Private Sub formPlayfield_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        IsDirty = False
    End Sub

    Private Sub formPlayfield_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' have a look for not saved changes
        If IsDirty Then
            Dim ret As DialogResult = MessageBox.Show(My.Resources.SaveChanges, My.Resources.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If ret = Windows.Forms.DialogResult.Yes Then
                buttonSave.PerformClick()
            ElseIf ret = Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub formPlayfield_LocationChanged(sender As Object, e As System.EventArgs) Handles Me.LocationChanged
        PlayfieldInfo(Me)
    End Sub
    Private Sub formPlayfield_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        PlayfieldInfo(Me)
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

    Private Sub chkPlayfieldFullSize_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPlayfieldFullSize.CheckedChanged
        IsDirty = True
        Me.WindowState = If(chkPlayfieldFullSize.Checked, FormWindowState.Maximized, FormWindowState.Normal)
    End Sub

    Private Sub buttonBringMeTheOtherWindows_Click(sender As System.Object, e As System.EventArgs) Handles buttonBringMeTheOtherWindows.Click
        'formBackglass.SetDesktopLocation(10, 10)
        formBackglass.BringToFront()
        formDMD.BringToFront()
    End Sub
 
    Private Sub radio1Screen_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radio1Screen.CheckedChanged
        If radio1Screen.Checked Then MaybeDoRecommendation()
    End Sub
    Private Sub radio2Screen_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radio2Screen.CheckedChanged
        If radio2Screen.Checked Then MaybeDoRecommendation()
    End Sub
    Private Sub radio3Screen_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radio3Screen.CheckedChanged
        If radio3Screen.Checked Then MaybeDoRecommendation()
    End Sub

    Private Sub buttonSave_Click(sender As System.Object, e As System.EventArgs) Handles buttonSave.Click
        If Not IO.File.Exists(FileName) Then
            IO.File.CreateText(FileName).Close()
        End If

        ' open file
        FileOpen(1, FileName, OpenMode.Output)

        WriteLine(1, CInt(Me.txtPlayfieldSizeWidth.Text))
        WriteLine(1, CInt(Me.txtPlayfieldSizeHeight.Text))
        WriteLine(1, CInt(formBackglass.txtBackglassSizeWidth.Text))
        WriteLine(1, CInt(formBackglass.txtBackglassSizeHeight.Text))
        WriteLine(1, formBackglass.BackglassScreenNo)
        WriteLine(1, CInt(formBackglass.txtBackglassLocationX.Text))
        WriteLine(1, CInt(formBackglass.txtBackglassLocationY.Text))
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, formDMD.Size.Width, CInt(formDMD.txtDMDSizeWidth.Text)))
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, formDMD.Size.Height, CInt(formDMD.txtDMDSizeHeight.Text)))
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, 0, Screen.FromControl(formDMD).Bounds.X - Screen.FromControl(formBackglass).Bounds.X + CInt(formDMD.txtDMDLocationX.Text)))
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, 0, Screen.FromControl(formDMD).Bounds.Y - Screen.FromControl(formBackglass).Bounds.Y + CInt(formDMD.txtDMDLocationY.Text)))
        WriteLine(1, If(formDMD.chkDMDFlipY.Checked, 1, 0))

        ' close file handle
        FileClose(1)

        MessageBox.Show(My.Resources.SettingsAreSaved, My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

        IsDirty = False
    End Sub

    Private Sub StartupPlayfield()
        IsInStartup = True

        radio1Screen.Checked = True
        If screenCount <= 2 Then
            radio3Screen.Enabled = False
        End If
        If screenCount <= 1 Then
            radio2Screen.Enabled = False
        End If

        If FileFound Then
            For Each scr As Screen In Screen.AllScreens
                ' set playfield
                If scr.Primary Then
                    Me.Location = scr.Bounds.Location
                    If PlayfieldSize = scr.Bounds.Size Then
                        Me.chkPlayfieldFullSize.Checked = True
                    End If
                End If

                ' set backglass and DMD
                Dim len As Integer = BackglassMonitor.Length
                If scr.DeviceName.Substring(0, len) = BackglassMonitor Then
                    formBackglass.Location = scr.Bounds.Location + BackglassLocation
                    If BackglassSize = scr.Bounds.Size Then
                        formBackglass.chkBackglassFullSize.Checked = True
                    Else
                        formBackglass.Size = BackglassSize
                    End If

                    formDMD.Location = scr.Bounds.Location + DMDLocation
                    formDMD.Size = DMDSize
                End If

                ' DMD default location
                formDMD.chkDMDAtDefaultLocation.Checked = (DMDLocation = New Point(0, 0))
                If formDMD.chkDMDAtDefaultLocation.Checked Then
                    Dim width As Integer = Math.Max(CInt(If(formBackglass.chkBackglassFullSize.Checked, scr.Bounds.Size.Width, formBackglass.Size.Width) / 2), 425)
                    Dim height As Integer = Math.Max(CInt(If(formBackglass.chkBackglassFullSize.Checked, scr.Bounds.Size.Height, formBackglass.Size.Height) / 3), 260)
                    Dim x As Integer = If((scr IsNot Nothing), 0, formBackglass.Location.X) + width / 2
                    Dim y As Integer = If((scr IsNot Nothing), 0, formBackglass.Location.Y) + 2 * If(formBackglass.chkBackglassFullSize.Checked, scr.Bounds.Size.Height, formBackglass.Size.Height) / 3
                    formDMD.Location = If((scr IsNot Nothing), scr.Bounds.Location, New Point(0, 0)) + New Point(x, y)
                    formDMD.Size = New Size(width, height)
                    formDMD.chkDMDAtDefaultLocation.Checked = (DMDLocation = New Point(0, 0))
                End If
                formBackglass.chkBackglassGrillVisible.Checked = (DMDLocation = New Point(0, 0))
                formDMD.chkDMDFlipY.Checked = DMDFlipY
            Next

            If screenCount = 1 Then
                radio1Screen.Checked = True
            ElseIf formBackglass.Bounds.IntersectsWith(formDMD.Bounds) OrElse Screen.FromControl(formBackglass).Bounds.IntersectsWith(formDMD.Bounds) Then
                radio2Screen.Checked = True
            Else
                radio3Screen.Checked = True
            End If
        End If

        IsInStartup = False
    End Sub

    Private Sub PlayfieldInfo(ByRef form As formPlayfield)
        IsDirty = True

        Dim currentScreen As Screen = Screen.FromControl(form)
        Dim screenCount As Integer = Screen.AllScreens.Count

        ' general
        If lblInfo.Text.Contains("{0}") Then
            lblInfo.Text = String.Format(lblInfo.Text, screenCount)
        End If

        ' playfield
        txtPlayfieldScreen.Text = ShortDevice(currentScreen.DeviceName)
        txtPlayfieldScreenSizeWidth.Text = currentScreen.Bounds.Width
        txtPlayfieldScreenSizeHeight.Text = currentScreen.Bounds.Height
        chkPlayfieldFullSize.Checked = (form.WindowState = FormWindowState.Maximized)
        If form.WindowState = FormWindowState.Maximized Then
            txtPlayfieldLocationX.Text = "0"
            txtPlayfieldLocationY.Text = "0"
            txtPlayfieldSizeWidth.Text = currentScreen.Bounds.Width
            txtPlayfieldSizeHeight.Text = currentScreen.Bounds.Height
        Else
            txtPlayfieldLocationX.Text = form.Location.X
            txtPlayfieldLocationY.Text = form.Location.Y
            txtPlayfieldSizeWidth.Text = form.Size.Width
            txtPlayfieldSizeHeight.Text = form.Size.Height
        End If

        'If ShortDevice(currentScreen.DeviceName).Equals("DISPLAY1", StringComparison.CurrentCultureIgnoreCase) Then
    End Sub

    Private Sub MaybeDoRecommendation()
        If IsInStartup Then Return
        If IsFirstCheck() OrElse MessageBox.Show(My.Resources.DoRecommendation, My.Resources.AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim recommendedScreenCount As Integer = If(radio1Screen.Checked, 1, If(radio2Screen.Checked, 2, 3))
            Me.chkPlayfieldFullSize.Checked = True
            formBackglass.WindowState = FormWindowState.Normal
            formDMD.WindowState = FormWindowState.Normal
            Select Case recommendedScreenCount
                Case 1
                    Dim scr1 As Screen = Screen.AllScreens(0)
                    formBackglass.txtBackglassLocationX.Text = 0
                    formBackglass.txtBackglassLocationY.Text = 0
                    formBackglass.txtBackglassSizeWidth.Text = CInt(scr1.Bounds.Width / 2)
                    formBackglass.txtBackglassSizeHeight.Text = CInt(scr1.Bounds.Height / 2)
                    formBackglass.OnValidate(Nothing)
                    formDMD.chkDMDAtDefaultLocation.Checked = True
                    formDMD.OnValidate(Nothing, True)
                Case 2
                    Dim scr2 As Screen = Screen.AllScreens(1)
                    If screenCount > 2 AndAlso scr2.Bounds.Location.X > Screen.AllScreens(2).Bounds.Location.X Then
                        scr2 = Screen.AllScreens(2)
                    End If
                    formBackglass.txtBackglassLocationX.Text = 0
                    formBackglass.txtBackglassLocationY.Text = 0
                    formBackglass.txtBackglassSizeWidth.Text = CInt(scr2.Bounds.Width / 2)
                    formBackglass.txtBackglassSizeHeight.Text = CInt(scr2.Bounds.Height / 2)
                    formBackglass.OnValidate(scr2)
                    formBackglass.WindowState = FormWindowState.Maximized
                    formDMD.chkDMDAtDefaultLocation.Checked = True
                    formDMD.OnValidate(scr2, True)
                Case 3
                    Dim scr2 As Screen = Screen.AllScreens(1)
                    Dim scr3 As Screen = Screen.AllScreens(2)
                    If scr2.Bounds.Location.X > scr3.Bounds.Location.X Then
                        scr2 = scr3
                        scr3 = Screen.AllScreens(1)
                    End If
                    formBackglass.txtBackglassLocationX.Text = 0
                    formBackglass.txtBackglassLocationY.Text = 0
                    formBackglass.txtBackglassSizeWidth.Text = CInt(scr2.Bounds.Width / 2)
                    formBackglass.txtBackglassSizeHeight.Text = CInt(scr2.Bounds.Height / 2)
                    formBackglass.OnValidate(scr2)
                    formBackglass.WindowState = FormWindowState.Maximized
                    formDMD.txtDMDLocationX.Text = 100
                    formDMD.txtDMDLocationY.Text = 100
                    formDMD.txtDMDSizeWidth.Text = scr3.Bounds.Width - 200
                    formDMD.txtDMDSizeHeight.Text = Math.Max(CInt(scr3.Bounds.Height / 2) - 100, 200)
                    formDMD.Size = New Size(CInt(formDMD.txtDMDSizeWidth.Text), CInt(formDMD.txtDMDSizeHeight.Text))
                    formDMD.Location = scr3.Bounds.Location + New Point(100, 75)
                    formDMD.chkDMDAtDefaultLocation.Checked = False
                    formDMD.OnValidate(scr3, False)
            End Select
            formBackglass.BringToFront()
            formDMD.BringToFront()
        End If
    End Sub
    Private Function IsFirstCheck() As Boolean
        Dim ret As Boolean = True
        If ret AndAlso radio1Screen.Checked Then ret = False
        If ret AndAlso radio2Screen.Checked Then ret = False
        If ret AndAlso radio3Screen.Checked Then ret = False
        Return ret
    End Function
 
End Class
