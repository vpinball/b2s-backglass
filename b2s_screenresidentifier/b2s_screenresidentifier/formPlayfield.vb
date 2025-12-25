Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Win32

Public Class formPlayfield

    Private formBackglass As formBackglass = Nothing
    Private formBackground As formBackground = Nothing
    Private formDMD As formDMD = Nothing
    Public Shared Function SafeReadRegistry(ByVal keyname As String, ByVal valuename As String, ByVal defaultvalue As String) As String
        '    Public Property GlobalFileName As String = SafeReadRegistry("Software\B2S", "B2SScreenResFileNameOverride", "ScreenRes.txt")

        Try
            Return CStr(Registry.CurrentUser.OpenSubKey(keyname).GetValue(valuename, defaultvalue))
        Catch ex As Exception
            Return defaultvalue
        End Try
    End Function

    Private Sub formPlayfield_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim B2SResFileEnding As String = SafeReadRegistry("Software\B2S", "B2SResFileEndingOverride", ".res")

        formBackglass = New formBackglass
        formBackground = New formBackground

        formBackglass.formBackground = formBackground
        formDMD = New formDMD
        formDMD.formBackglass = formBackglass
        lblCopyright.Text = "Version " & Application.ProductVersion & " " & My.Application.Info.Copyright.ToString & " by Herweh && B2S Team"
        ' get all saved data
        If My.Application.CommandLineArgs.Count > 0 Then
            FileName = My.Application.CommandLineArgs.ElementAt(0)
            ' If started from B2SBackglassServer either directly (PureEXE=true) or as backglass through VPX (false) or from explorer PureEXE = Nothing
            If My.Application.CommandLineArgs.Count > 1 Then PureEXE = My.Application.CommandLineArgs.ElementAt(1).Equals("-pureexe=true")
            ' In case a Table or backglass file is thrown on the executable
            If Not Path.GetExtension(FileName).ToLower().Equals(B2SResFileEnding) And Not Path.GetExtension(FileName).ToLower().Equals(".txt") And Not FileName.ToLower().Equals(GlobalFileName.ToLower()) Then
                FileName = Path.ChangeExtension(FileName, B2SResFileEnding)
            End If

            If File.Exists(FileName) Then
                GetSettings(FileName)
            ElseIf File.Exists(GlobalFileName) Then
                GetSettings(GlobalFileName)
            ElseIf File.Exists(Path.Combine(Application.StartupPath(), GlobalFileName)) Then
                GetSettings(Path.Combine(Application.StartupPath(), GlobalFileName))
            End If
        Else
            GetSettings(FileName)
        End If

        StartupPlayfield()
        ' create all other forms
        If (PureEXE.HasValue And PureEXE) Or Not PureEXE.HasValue Then
            Me.TopMost = False
            formBackground.TopMost = False
            formBackglass.TopMost = False
            formDMD.TopMost = False
        End If

        Me.BringToFront()

        formBackground.BringToFront()
        formBackground.Show()
        formBackground.Visible = BackgroundActive

        formBackglass.BringToFront()
        formBackglass.Show()
        formBackglass.chkBackgroundActive.Checked = BackgroundActive

        formDMD.BringToFront()
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
        formBackground.BringToFront()
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


    Private Sub radio1deviceNo_CheckedChanged(sender As Object, e As EventArgs) Handles Radio1deviceNo.CheckedChanged
        BackglassMonitorType = ""
    End Sub
    Private Sub radio2pixelCoord_CheckedChanged(sender As Object, e As EventArgs) Handles Radio2pixelCoord.CheckedChanged
        BackglassMonitorType = "@"
    End Sub
    Private Sub radio3screenIndex_CheckedChanged(sender As Object, e As EventArgs) Handles Radio3screenIndex.CheckedChanged
        BackglassMonitorType = "="
    End Sub
    Private Sub buttonSave_Click(sender As System.Object, e As System.EventArgs) Handles buttonSave.Click
        If Me.chkSaveEnhanced.Checked Then
            Me.chkSaveEnhanced.Font = New Font(Me.chkSaveEnhanced.Font, FontStyle.Bold)
            Me.chkSaveEnhanced.ForeColor = Color.Black
        Else
            Me.chkSaveEnhanced.Font = New Font(Me.chkSaveEnhanced.Font, FontStyle.Regular)
            If formBackglass.chkBackgroundActive.Checked Then Me.chkSaveEnhanced.ForeColor = Color.Red
        End If

        SaveResFile(FileName)
    End Sub

    Private Sub buttonSaveGlobal_Click(sender As Object, e As EventArgs) Handles buttonSaveGlobal.Click
        Dim saveFilename As String

        If IO.File.Exists(GlobalFileName) Or CurDir() = Application.StartupPath() Then
            saveFilename = GlobalFileName
        Else
            saveFilename = IO.Path.Combine(Application.StartupPath(), GlobalFileName)
        End If

        If MessageBox.Show($"Do you really want to save these settings globally?{vbCrLf}filename: {saveFilename}", My.Application.Info.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            SaveResFile(saveFilename)
        End If
    End Sub

    Private Sub SaveResFile(ResFileName As String)
        If Not IO.File.Exists(ResFileName) Then
            IO.File.CreateText(ResFileName).Close()
        End If

        Dim currentScreen = 0
        ' open file
        FileOpen(1, ResFileName, OpenMode.Output)

        If Me.chkSaveComments.Checked And Me.chkSaveEnhanced.Checked Then PrintLine(1, "# V" + Application.ProductVersion)

        If Me.chkSaveComments.Checked Then
            PrintLine(1, "# This is a ScreenRes file for the B2SBackglassServer.")
            PrintLine(1, "# From release 1.3.1.1 comment lines like this starting with a '#' are supported.")
            PrintLine(1, "# Playfield Screen resolution width/height")
        End If
        WriteLine(1, CInt(Me.txtPlayfieldSizeWidth.Text))
        WriteLine(1, CInt(Me.txtPlayfieldSizeHeight.Text))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Backglass width/height")
        WriteLine(1, CInt(formBackglass.txtBackglassSizeWidth.Text))
        WriteLine(1, CInt(formBackglass.txtBackglassSizeHeight.Text))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Define Backglass screen using Display Devicename screen number (\\.\DISPLAY)x or screen coordinates (@x) or screen index (=x)")

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

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Backglass x/y position relative to the upper left corner of the screen selected")
        WriteLine(1, CInt(formBackglass.txtBackglassLocationX.Text))
        WriteLine(1, CInt(formBackglass.txtBackglassLocationY.Text))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# width/height of the B2S (or Full) DMD area")
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, formDMD.Size.Width, CInt(formDMD.txtDMDSizeWidth.Text)))
        WriteLine(1, If(formDMD.chkDMDAtDefaultLocation.Checked, formDMD.Size.Height, CInt(formDMD.txtDMDSizeHeight.Text)))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# x/y position of the B2S (or Full) DMD area - relative to the upper left corner of the backglass window")
        If formDMD.chkDMDAtDefaultLocation.Checked Then
            WriteLine(1, 0)
            WriteLine(1, 0)
        Else
            WriteLine(1, CInt(formDMD.txtDMDLocationX.Text) - CInt(formBackglass.txtBackglassLocationX.Text) + Screen.FromControl(formDMD).Bounds.Location.X - Screen.FromControl(formBackglass).Bounds.Location.X)
            WriteLine(1, CInt(formDMD.txtDMDLocationY.Text) - CInt(formBackglass.txtBackglassLocationY.Text) + Screen.FromControl(formDMD).Bounds.Location.Y - Screen.FromControl(formBackglass).Bounds.Location.Y)
        End If

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Y-flip, flips the LED display upside down")
        WriteLine(1, If(formDMD.chkDMDFlipY.Checked, 1, 0))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Background x/y position - relative to the backglass screen - has to be activated in the settings")
        If formBackglass.chkBackgroundActive.Checked Then
            WriteLine(1, CInt(formBackground.txtBackgroundLocationX.Text) + Screen.FromControl(formBackground).Bounds.Location.X - Screen.FromControl(formBackglass).Bounds.Location.X)
            WriteLine(1, CInt(formBackground.txtBackgroundLocationY.Text) + Screen.FromControl(formBackground).Bounds.Location.Y - Screen.FromControl(formBackglass).Bounds.Location.Y)
        Else
            WriteLine(1, 0)
            WriteLine(1, 0)
        End If

        If Me.chkSaveComments.Checked Then PrintLine(1, "# Background width/height")
        WriteLine(1, If(Not formBackglass.chkBackgroundActive.Checked, 0, CInt(formBackground.txtBackgroundSizeWidth.Text)))
        WriteLine(1, If(Not formBackglass.chkBackgroundActive.Checked, 0, CInt(formBackground.txtBackgroundSizeHeight.Text)))

        If Me.chkSaveComments.Checked Then PrintLine(1, "# path to the background image (C:\path\Frame) or black if none selected")
        PrintLine(1, formBackground.TxtBackgroundPath.Text)

        If Me.chkSaveComments.Checked Then
            PrintLine(1, "# This line would turn off B2SWindowPunch if activated")
            PrintLine(1, "#B2SWindowPunch=off")
        End If
        ' If no comments are added but the enhanced format is active, this is needed!
        If Not Me.chkSaveComments.Checked And Me.chkSaveEnhanced.Checked Then PrintLine(1, "# V" + Application.ProductVersion + " This is needed from B2S Server 2.0 even if comments are deactivated to mark the version 2 file format. It is ignored on older releases, but your bg forms might be switched.")


        FileClose(1)

        MessageBox.Show(My.Resources.SettingsAreSaved, My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

        IsDirty = False
    End Sub

    Private Sub StartupPlayfield()
        Dim currentScreen = 0

        IsInStartup = True
        Me.ResFileLabel.Text = Path.GetFileName(FileName)

        'If the filename coming in as parameter is already the "global" name then we can only save as global
        If Path.GetFileName(FileName).Equals(GlobalFileName) Then
            buttonSaveGlobal.Enabled = False
            buttonSaveGlobal.Visible = False
        End If

        radio1Screen.Checked = True
        If screenCount <= 2 Then
            radio3Screen.Enabled = False
        End If
        If screenCount <= 1 Then
            radio2Screen.Enabled = False
        End If
        Me.chkSaveComments.Checked = SaveComments
        If VersionTwoFile Then
            Me.chkSaveEnhanced.Font = New Font(Me.chkSaveEnhanced.Font, FontStyle.Bold)
        Else
            If BackgroundActive Then Me.chkSaveEnhanced.ForeColor = Color.Red
        End If
        ' If background is active, we default to the new V2 res format
        If VersionTwoFile Or BackgroundActive Then Me.chkSaveEnhanced.Checked = True

        If FileFound Then
            For Each scr As Screen In ScreensOrdered

                currentScreen += 1
                ' set playfield
                If scr.Primary Then
                    Me.Location = scr.Bounds.Location
                    Me.Size = PlayfieldSize
                    If PlayfieldSize = scr.Bounds.Size Then
                        Me.chkPlayfieldFullSize.Checked = True
                    End If
                End If

                ' set backglass, background and DMD
                If (BackglassMonitorType = "" AndAlso scr.DeviceName.Substring(11) = BackglassMonitor) Or
                   (BackglassMonitorType = "@" AndAlso scr.Bounds.Location.X = CInt(BackglassMonitor)) Or
                   (BackglassMonitorType = "=" AndAlso currentScreen = CInt(BackglassMonitor)) Then

                    ' Backglass
                    formBackglass.Location = scr.Bounds.Location + BackglassLocation
                    formBackglass.Size = BackglassSize
                    If BackglassSize = scr.Bounds.Size Then formBackglass.chkBackglassFullSize.Checked = True

                    ' Background
                    formBackground.Location = scr.Bounds.Location + BackgroundLocation

                    If BackgroundSize.Height = 0 And BackgroundSize.Width = 0 Then
                        BackgroundActive = False
                        formBackground.Visible = False
                    Else
                        formBackground.Size = BackgroundSize
                        BackgroundActive = True
                        formBackground.Visible = True
                    End If

                    ' DMD
                    formDMD.Location = scr.Bounds.Location + BackglassLocation + DMDLocation
                    formDMD.Size = DMDSize

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
                End If
            Next

            If screenCount = 1 Then
                radio1Screen.Checked = True
            ElseIf formBackglass.Bounds.IntersectsWith(formDMD.Bounds) OrElse Screen.FromControl(formBackglass).Bounds.IntersectsWith(formDMD.Bounds) Then
                radio2Screen.Checked = True
            Else
                radio3Screen.Checked = True
            End If
        End If

        If BackglassMonitorType = "@" Then
            Radio2pixelCoord.Checked = True
        ElseIf BackglassMonitorType = "=" Then
            Radio3screenIndex.Checked = True
        Else
            Radio1deviceNo.Checked = True
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
