Imports System.Text
Imports System.Drawing
'Imports System.IO
'Imports System.Windows.Forms.VisualStyles.VisualStyleElement

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

        Dim currentScreen = 0
        ' open file
        FileOpen(1, FileName, OpenMode.Output)

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Playfield Screen resolution width/height")
        WriteLine(1, CInt(Me.txtPlayfieldSizeWidth.Text))
        WriteLine(1, CInt(Me.txtPlayfieldSizeHeight.Text))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Backglass Screen resolution width/height")
        WriteLine(1, CInt(formBackglass.txtBackglassSizeWidth.Text))
        WriteLine(1, CInt(formBackglass.txtBackglassSizeHeight.Text))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Define Backglass using Display Devicename screen number (\\.\DISPLAY)x or screen coordinates (@x) or screen index (=x)")
        If BackglassMonitorType.StartsWith("@") Then
            PrintLine(1, BackglassMonitorType & Me.txtPlayfieldSizeWidth.Text)
        ElseIf BackglassMonitorType.StartsWith("=") Then
            For Each scr As Screen In ScreensOrdered
                currentScreen += 1
                If scr.DeviceName.Substring(11) = formBackglass.BackglassScreenNo.ToString Then
                    PrintLine(1, BackglassMonitorType & currentScreen)
                End If
            Next
        Else
            WriteLine(1, formBackglass.BackglassScreenNo)
        End If

        If Me.chkSaveComments.Checked Then PrintLine(1, "# x position for the backglass relative To the upper left corner Of the screen selected")
        WriteLine(1, CInt(formBackglass.txtBackglassLocationX.Text))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# y position for the backglass On the selected display (Normally left at 0)")
        WriteLine(1, CInt(formBackglass.txtBackglassLocationY.Text))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# width/height Of the DMD area In pixels - For 3 screen setup")
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, formDMD.Size.Width, CInt(formDMD.txtDMDSizeWidth.Text)))
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, formDMD.Size.Height, CInt(formDMD.txtDMDSizeHeight.Text)))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# X/Y position Of the DMD area relative To the upper left corner of the backglass screen - For 3 screen setup")
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, 0, Screen.FromControl(formDMD).Bounds.X - Screen.FromControl(formBackglass).Bounds.X + CInt(formDMD.txtDMDLocationX.Text)))
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, 0, Screen.FromControl(formDMD).Bounds.Y - Screen.FromControl(formBackglass).Bounds.Y + CInt(formDMD.txtDMDLocationY.Text)))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Y-flip, flips the LED display upside down")
        WriteLine(1, If(formDMD.chkDMDFlipY.Checked, 1, 0))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# X/Y position pos When StartBackground Is active, relative To upper left corner Of Playfield ('Small' Button In the Options)")
        PrintLine(1, BackgroundLocation.X.ToString)
        PrintLine(1, BackgroundLocation.Y.ToString)

        If Me.chkSaveComments.Checked Then PrintLine(1, "# width/height Of the backglass When StartBackground Is active")
        PrintLine(1, BackgroundSize.Width.ToString)
        PrintLine(1, BackgroundSize.Height.ToString)

        If Me.chkSaveComments.Checked Then PrintLine(1, "# C:\path\Frame (The path To the location where you have the background image)")
        PrintLine(1, BackgroundPath.ToString)

        ' close file handle
        FileClose(1)

        MessageBox.Show(My.Resources.SettingsAreSaved, My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

        IsDirty = False
    End Sub

    Private Sub StartupPlayfield()
        Dim currentScreen = 0
        'Dim filePath = Path.Combine(Application.StartupPath, "Test.txt")

        IsInStartup = True

        radio1Screen.Checked = True
        If screenCount <= 2 Then
            radio3Screen.Enabled = False
        End If
        If screenCount <= 1 Then
            radio2Screen.Enabled = False
        End If
        Me.chkSaveComments.Checked = SaveComments
        If FileFound Then
            For Each scr As Screen In ScreensOrdered
                'File.AppendAllText(filePath, Environment.NewLine & "BackglassMonitor: " + BackglassMonitorType + BackglassMonitor)

                currentScreen += 1
                ' set playfield
                If scr.Primary Then
                    Me.Location = scr.Bounds.Location
                    If PlayfieldSize = scr.Bounds.Size Then
                        Me.chkPlayfieldFullSize.Checked = True
                    End If
                End If
                'File.AppendAllText(filePath, Environment.NewLine + "Comparing: " + scr.DeviceName)
                'File.AppendAllText(filePath, Environment.NewLine + "Location.X: " + scr.Bounds.Location.X.ToString)
                'File.AppendAllText(filePath, Environment.NewLine + "currentScreen: " + currentScreen.ToString)

                ' set backglass and DMD
                If (scr.DeviceName.Substring(11) = BackglassMonitor) Or
                   (BackglassMonitorType = "@" AndAlso scr.Bounds.Location.X = CInt(BackglassMonitor)) Or
                   (BackglassMonitorType = "=" AndAlso currentScreen = CInt(BackglassMonitor)) Then
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

        ' general
        If lblInfo.Text.Contains("{0}") Then
            lblInfo.Text = String.Format(lblInfo.Text, screenCount)
        End If

        ' playfield
        If String.Compare(txtPlayfieldScreen.Text, ShortDevice(currentScreen.DeviceName)) <> 0 Then
            Dim screenSize As Size = TrueResolution(currentScreen.DeviceName)
            Dim dpi As Integer

            txtPlayfieldScreenSizeWidth.Text = screenSize.Width
            txtPlayfieldScreenSizeHeight.Text = screenSize.Height

            dpi = 100 * screenSize.Width / currentScreen.Bounds.Width

            txtPlayfieldScreenScale.Text = dpi & "%"
            If dpi <> 100 Then
                txtPlayfieldScreenScale.BackColor = Color.Red
            Else
                txtPlayfieldScreenScale.BackColor = txtPlayfieldScreenSizeHeight.BackColor
            End If
        End If

        txtPlayfieldScreen.Text = ShortDevice(currentScreen.DeviceName)

        chkPlayfieldFullSize.Checked = (form.WindowState = FormWindowState.Maximized)
        If form.WindowState = FormWindowState.Maximized Then
            txtPlayfieldLocationX.Text = "0"
            txtPlayfieldLocationY.Text = "0"
            txtPlayfieldSizeWidth.Text = txtPlayfieldScreenSizeWidth.Text
            txtPlayfieldSizeHeight.Text = txtPlayfieldScreenSizeHeight.Text
        Else
            txtPlayfieldLocationX.Text = form.Location.X
            txtPlayfieldLocationY.Text = form.Location.Y
            txtPlayfieldSizeWidth.Text = form.Size.Width
            txtPlayfieldSizeHeight.Text = form.Size.Height
        End If

    End Sub

    Private Sub MaybeDoRecommendation()
        If IsInStartup Then Return
        If IsFirstCheck() OrElse MessageBox.Show(My.Resources.DoRecommendation, My.Resources.AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim recommendedScreenCount As Integer = If(radio1Screen.Checked, 1, If(radio2Screen.Checked, 2, 3))
            Dim scr1 As Screen = ScreensOrdered(0)

            formBackglass.WindowState = FormWindowState.Normal
            formDMD.WindowState = FormWindowState.Normal

            Me.Location = scr1.Bounds.Location
            Me.txtPlayfieldLocationX.Text = 0
            Me.txtPlayfieldLocationY.Text = 0
            Me.chkPlayfieldFullSize.Checked = True

            Select Case recommendedScreenCount
                Case 1
                    formBackglass.txtBackglassLocationX.Text = 0
                    formBackglass.txtBackglassLocationY.Text = 0
                    formBackglass.txtBackglassSizeWidth.Text = CInt(scr1.Bounds.Width / 2)
                    formBackglass.txtBackglassSizeHeight.Text = CInt(scr1.Bounds.Height / 2)
                    formBackglass.OnValidate(Nothing)
                    formDMD.chkDMDAtDefaultLocation.Checked = True
                    formDMD.OnValidate(Nothing, True)
                Case 2
                    Dim scr2 As Screen = ScreensOrdered(1)
                    If screenCount > 2 AndAlso scr2.Bounds.Location.X > ScreensOrdered(2).Bounds.Location.X Then
                        scr2 = ScreensOrdered(2)
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
                    Dim scr2 As Screen = ScreensOrdered(1)
                    Dim scr3 As Screen = ScreensOrdered(2)
                    If scr2.Bounds.Location.X > scr3.Bounds.Location.X Then
                        scr2 = scr3
                        scr3 = ScreensOrdered(1)
                    End If
                    formBackglass.txtBackglassLocationX.Text = 0
                    formBackglass.txtBackglassLocationY.Text = 0
                    formBackglass.txtBackglassSizeWidth.Text = CInt(scr2.Bounds.Width / 2)
                    formBackglass.txtBackglassSizeHeight.Text = CInt(scr2.Bounds.Height / 2)
                    formBackglass.Location = scr2.Bounds.Location
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
