Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.IO

Public Class formSettings

    Public B2SScreen As B2SScreen = Nothing
    Public B2SAnimation As B2SAnimation = Nothing
    Public formBackglass As formBackglass = Nothing

    Public activateMsgBoxAtSaving As Boolean = False
    Public isSettingsScreenDirty As Boolean = False

    Private formSettingsMore As formSettingsMore = Nothing

    Private fadeIn As Boolean = True
    Private Const fadeStep As Single = 0.2

    Private Class Animations4Settings
        Public Name As String = String.Empty
        Public SlowDown As Integer = 1

        Public Sub New(ByVal _name As String, ByVal _slowdown As Integer)
            Name = _name
            SlowDown = _slowdown
        End Sub

        Public Overrides Function ToString() As String
            Return Name + If(SlowDown = 1, "", " (" & If(SlowDown = 0, "Off", SlowDown.ToString & "x") & ")")
        End Function
    End Class

    Private Sub formSettings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        formSettingsMore = New formSettingsMore(Me, formBackglass)

        ' load data
        Dim _isdirty As Boolean = isSettingsScreenDirty
        ' set version info
        lblVersion.Text = String.Format("Server version {0}, backglass file version {1}", B2SSettings.DirectB2SVersion, B2SSettings.BackglassFileVersion)
        ' get more data
        formSettingsMore.btnLogPath.Text = "Log path: " & B2SSettings.LogPath
        formSettingsMore.chkLogLamps.Checked = B2SSettings.IsLampsStateLogOn
        formSettingsMore.chkLogSolenoids.Checked = B2SSettings.IsSolenoidsStateLogOn
        formSettingsMore.chkLogGIStrings.Checked = B2SSettings.IsGIStringsStateLogOn
        formSettingsMore.chkLogLEDs.Checked = B2SSettings.IsLEDsStateLogOn
        formSettingsMore.chkAllOut.Enabled = B2SSettings.IsROMControlled
        formSettingsMore.chkAllOff.Enabled = B2SSettings.IsROMControlled
        formSettingsMore.chkLampsOff.Enabled = (B2SData.UseRomLamps OrElse B2SData.UseAnimationLamps) AndAlso B2SSettings.IsROMControlled
        formSettingsMore.chkSolenoidsOff.Enabled = (B2SData.UseRomSolenoids OrElse B2SData.UseAnimationSolenoids) AndAlso B2SSettings.IsROMControlled
        formSettingsMore.chkGIStringsOff.Enabled = (B2SData.UseRomGIStrings OrElse B2SData.UseAnimationGIStrings) AndAlso B2SSettings.IsROMControlled
        formSettingsMore.chkLEDsOff.Enabled = (B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels)
        ' get panel data
        If B2SData.DualBackglass Then
            cmbMode.SelectedIndex = CInt(B2SSettings.CurrentDualMode) - 1
        Else
            cmbMode.Text = String.Empty
            cmbMode.Enabled = False
            cmbGrill.Focus()
        End If
        cmbDMD.SelectedIndex = B2SSettings.HideDMD
        cmbGrill.SelectedIndex = B2SSettings.HideGrill
        cmbB2SDMD.SelectedIndex = If(B2SSettings.HideB2SDMD, 1, 0)
        btnScreenshotPath.Text = "Screenshot path: " & B2SSettings.ScreenshotPath
        cmbScreenshotType.SelectedIndex = B2SSettings.ScreenshotFileType
        numLampsSkipFrames.Enabled = (B2SData.UseRomLamps OrElse B2SData.UseAnimationLamps) AndAlso B2SSettings.IsROMControlled
        numLampsSkipFrames.Value = B2SSettings.LampsSkipFrames
        numSolenoidsSkipFrames.Enabled = (B2SData.UseRomSolenoids OrElse B2SData.UseAnimationSolenoids) AndAlso B2SSettings.IsROMControlled
        numSolenoidsSkipFrames.Value = B2SSettings.SolenoidsSkipFrames
        numGISkipFrames.Enabled = (B2SData.UseRomGIStrings OrElse B2SData.UseAnimationGIStrings) AndAlso B2SSettings.IsROMControlled
        numGISkipFrames.Value = B2SSettings.GIStringsSkipFrames
        chkStartAsEXE.Checked = B2SSettings.StartAsEXE
        cmbDefaultStartMode.SelectedIndex = B2SSettings.DefaultStartMode - 1
        numLEDSkipFrames.Enabled = (B2SData.UseLEDs OrElse B2SData.UseLEDDisplays OrElse B2SData.UseReels) AndAlso B2SSettings.IsROMControlled
        numLEDSkipFrames.Value = B2SSettings.LEDsSkipFrames
        If Not B2SData.UseReels Then
            radioStandardLED.Checked = (B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered)
            radioDream7LED.Checked = (B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7)
        Else
            radioStandardLED.Enabled = False
            radioDream7LED.Enabled = False
        End If
        chkBulbs.Checked = B2SSettings.IsGlowBulbOn
        chkFormFront.Checked = B2SSettings.FormToFront
        chkDisableFuzzyMatching.Checked = B2SSettings.DisableFuzzyMatching

        cmbGlowing.SelectedIndex = If(B2SSettings.GlowIndex <> -1, B2SSettings.GlowIndex, cmbGlowing.Items.Count - 1)
        activateMsgBoxAtSaving = False
        ' get animation info
        cmbAnimations.Items.Clear()
        For Each animation As String In B2SAnimation.Animations
            cmbAnimations.Items.Add(New Animations4Settings(animation, B2SAnimation.AnimationSlowDown(animation)))
        Next
        If cmbAnimations.Items.Count > 0 Then
            cmbAnimations.SelectedIndex = 0
            cmbAnimations.Items.Add(New Animations4Settings("(All animations)", B2SSettings.AllAnimationSlowDown))
        End If
        ' maybe show matching file names combo box
        If B2SSettings.MatchingFileNames IsNot Nothing AndAlso B2SSettings.MatchingFileNames.Length >= 2 Then
            cmbMatchingFileNames.Items.Clear()
            For Each filename As String In B2SSettings.MatchingFileNames
                cmbMatchingFileNames.Items.Add(filename)
            Next
            If Not String.IsNullOrEmpty(B2SSettings.MatchingFileName) Then
                cmbMatchingFileNames.Text = B2SSettings.MatchingFileName
            Else
                cmbMatchingFileNames.SelectedIndex = 0
            End If
            btnCheck.Visible = False
            cmbMode.Width = cmbDMD.Width
            lblFile.Visible = True
            cmbMatchingFileNames.Visible = True
        End If
        chkSmall.Checked = B2SSettings.StartBackground

        ' plugin stuff
        chkActivatePlugins.Checked = B2SSettings.ArePluginsOn
        chkShowStartupError.Checked = B2SSettings.ShowStartupError
        If B2SSettings.ArePluginsOn AndAlso Registry.CurrentUser.OpenSubKey("Software\B2S").GetValue("Plugins", 0) > 0 Then
            btnPluginSettings.Enabled = True
        End If
        ' size panel
        PanelSettings.Location = New Point((Me.Size.Width - PanelSettings.Width) / 2, (Me.Size.Height - PanelSettings.Height) / 2)
        ' reset dirty flag to previous state
        isSettingsScreenDirty = _isdirty

        ' set and start timers
        TimerOpacity.Interval = 10
        TimerOpacity.Start()

    End Sub

    Private Sub formSettings_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape OrElse e.KeyCode = Keys.S Then
            btnCloseSettings.PerformClick()
        End If
    End Sub

    Private Sub TimerOpacity_Tick(sender As System.Object, e As System.EventArgs) Handles TimerOpacity.Tick

        If fadeIn Then
            Me.Opacity += fadeStep
            If Me.Opacity >= 1 Then
                TimerOpacity.Stop()
                fadeIn = False
            End If
        Else
            Me.Opacity -= fadeStep
            If Me.Opacity <= 0 Then
                TimerOpacity.Stop()
                Try
                    Me.Close()
                    Me.Dispose()
                Catch
                End Try
            End If
        End If

    End Sub

    Private Sub btnDonate_Click(sender As System.Object, e As System.EventArgs) Handles btnDonate.Click

        MessageBox.Show("You can show your appreciation for 'B2S Backglass Designer' and 'B2S Backglass Server' and support future development by donating. " & vbCrLf &
                      "I strongly feel that 'B2S Backglass Designer' and 'B2S Backglass Server' should remain free of charge as my gift to the online community. " & vbCrLf &
                      "Please note that donations to 'B2S Backglass Designer' and 'B2S Backglass Server' are not tax-deductible for income tax purposes." & vbCrLf & vbCrLf &
                      "Send whatever amount you want to send to my PayPal address 'stefan.wuehr@freenet.de'." & vbCrLf & vbCrLf &
                      "Thank you very much.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
    Private Sub btnMore_Click(sender As System.Object, e As System.EventArgs) Handles btnMore.Click

        formSettingsMore.ShowDialog(Me, formBackglass)

    End Sub
    Private Sub btnSaveSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSettings.Click

        B2SSettings.Save(B2SAnimation)
        B2SSettings.Save(, True)
        isSettingsScreenDirty = False
        If activateMsgBoxAtSaving Then
            MessageBox.Show(My.Resources.MSG_ChangesNeedARestart, My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub
    Private Sub btnCloseSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseSettings.Click

        If IsDirty() Then Return
        fadeIn = False
        TimerOpacity.Start()

    End Sub

    Private Sub cmbMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbMode.SelectedIndexChanged
        isSettingsScreenDirty = True
        B2SSettings.CurrentDualMode = cmbMode.SelectedIndex + 1
        If formBackglass IsNot Nothing Then
            formBackglass.BackgroundImage = formBackglass.DarkImage
            formBackglass.Refresh()
        End If
        B2SAnimation.RestartAnimations()
    End Sub
    Private Sub btnCheck_Click(sender As System.Object, e As System.EventArgs) Handles btnCheck.Click
        Dim text As String = "Your backglass monitor is " & B2SScreen.BackglassMonitor & vbCrLf & vbCrLf
        For Each scr As Screen In Screen.AllScreens
            text &= scr.DeviceName & ": Running on " & scr.BitsPerPixel.ToString() & " bit" & vbCrLf
        Next
        MessageBox.Show(text, "Check monitors", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub btnHyperpin_Click(sender As System.Object, e As System.EventArgs) Handles btnHyperpin.Click
        B2SSettings.HyperpinXMLFile = String.Empty
        'If Not B2SSettings.LocateHyperpinXMLFile() Then
        If MessageBox.Show("Please locate and select your 'Visual Pinball.xml' file.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.OK Then
            Using ofd As OpenFileDialog = New OpenFileDialog
                ofd.Filter = "XML file (*.xml)|*.xml|ALL (*.*)|*.*"
                ofd.FileName = "Visual Pinball.xml"
                If ofd.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Dim name As String = ofd.FileName
                    Dim fi As FileInfo = New FileInfo(name)
                    If Not fi.Name.Equals("Visual Pinball.xml") Then
                        MessageBox.Show("Sorry, this file isn't a 'Visual Pinball.xml' file.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf File.Exists(name) Then
                        Try
                            Dim xml As Xml.XmlDocument = New Xml.XmlDocument()
                            xml.Load(name)
                        Catch ex As Exception
                            MessageBox.Show("The following error occurred opening the file '" & name & "':" & vbCrLf & vbCrLf & ex.Message, My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End Try
                        B2SSettings.HyperpinXMLFile = name
                        B2SSettings.Save(, , True)
                        MessageBox.Show("Your 'Visual Pinball.xml' file could be located correctly.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("'Visual Pinball.xml' could not be found.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            End Using
        End If
        'If MessageBox.Show("To locate 'Hyperpin' correctly please select the 'Hyperpin' folder.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.OK Then
        '    Using fbd As FolderBrowserDialog = New FolderBrowserDialog
        '        If fbd.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        '            If IO.File.Exists(IO.Path.Combine(fbd.SelectedPath, "hyperpin.exe")) Then
        '                If IO.File.Exists(IO.Path.Combine(fbd.SelectedPath, "Databases", "Visual Pinball", "Visual Pinball.xml")) Then
        '                    B2SSettings.HyperpinXMLFile = IO.Path.Combine(fbd.SelectedPath, "Databases", "Visual Pinball", "Visual Pinball.xml")
        '                    B2SSettings.Save(, , True)
        '                    MessageBox.Show("'Hyperpin' could be located correctly.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                Else
        '                    MessageBox.Show("'Visual Pinball.xml' could not be found in folder '" & IO.Path.Combine(fbd.SelectedPath, "Databases", "Visual Pinball") & "'.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                End If
        '            Else
        '                MessageBox.Show("'Hyperpin.exe' could not be found in folder '" & fbd.SelectedPath & "'.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            End If
        '        End If
        '    End Using
        'End If
        'Else
        'MessageBox.Show("'Hyperpin' could be found in '" & IO.Directory.GetParent(B2SSettings.HyperpinXMLFile).FullName & "'.", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If
    End Sub
    Private Sub cmbMatchingFileNames_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMatchingFileNames.SelectedIndexChanged
        isSettingsScreenDirty = True
        B2SSettings.MatchingFileName = cmbMatchingFileNames.Text
    End Sub

    Private Sub cmbGrill_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbGrill.SelectedIndexChanged
        activateMsgBoxAtSaving = True
        isSettingsScreenDirty = True
        B2SSettings.HideGrill = cmbGrill.SelectedIndex
    End Sub
    Private Sub cmbDMD_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDMD.SelectedIndexChanged
        activateMsgBoxAtSaving = True
        isSettingsScreenDirty = True
        B2SSettings.HideDMD = cmbDMD.SelectedIndex
    End Sub
    Private Sub cmbB2SDMD_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbB2SDMD.SelectedIndexChanged
        activateMsgBoxAtSaving = True
        isSettingsScreenDirty = True
        B2SSettings.HideB2SDMD = (cmbB2SDMD.SelectedIndex = 1)
    End Sub
    Private Sub chkStartAsEXE_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkStartAsEXE.CheckedChanged
        activateMsgBoxAtSaving = True
        isSettingsScreenDirty = True
        B2SSettings.StartAsEXE = chkStartAsEXE.Checked
    End Sub
    Private Sub chkSmall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSmall.CheckedChanged
        activateMsgBoxAtSaving = True
        isSettingsScreenDirty = True
        B2SSettings.StartBackground = chkSmall.Checked
    End Sub
    Private Sub cmbDefaultStartMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDefaultStartMode.SelectedIndexChanged
        isSettingsScreenDirty = True
        B2SSettings.DefaultStartMode = cmbDefaultStartMode.SelectedIndex + 1
    End Sub

    Private Sub radioDream7LED_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radioDream7LED.CheckedChanged
        isSettingsScreenDirty = True
        If radioDream7LED.Checked Then
            B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7
        ElseIf radioStandardLED.Checked Then
            B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered
        End If
        For Each led As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
            led.Value.Visible = (B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered) AndAlso Not led.Value.Hidden
        Next
        For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
            leddisplay.Value.Visible = (B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7) AndAlso Not leddisplay.Value.Hidden
        Next
        chkWireframe.Enabled = radioDream7LED.Checked
        chkBulbs.Enabled = radioDream7LED.Checked
        cmbGlowing.Enabled = radioDream7LED.Checked
    End Sub
    Private Sub radioStandardLED_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radioStandardLED.CheckedChanged
        ' nothing to do, all handled at radioDream7LED
    End Sub
    'Private Sub chkUseDream7_CheckedChanged(sender As System.Object, e As System.EventArgs)
    'B2SSettings.UsedLEDType = If(chkUseDream7.Checked, B2SSettings.eLEDTypes.Dream7, B2SSettings.eLEDTypes.Rendered)
    'For Each led As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
    '    led.Value.Visible = (B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered)
    'Next
    'For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
    '    leddisplay.Value.Visible = (B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7)
    'Next
    'chkWireframe.Enabled = chkUseDream7.Checked
    'chkBulbs.Enabled = chkUseDream7.Checked
    'btnGlow.Enabled = chkUseDream7.Checked
    'End Sub
    Private Sub chkWireframe_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkWireframe.CheckedChanged
        Static wireframe As Boolean = False
        wireframe = Not wireframe
        For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
            leddisplay.Value.WireFrame = wireframe
        Next
    End Sub
    Private Sub chkBulbs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBulbs.CheckedChanged
        isSettingsScreenDirty = True
        B2SSettings.IsGlowBulbOn = chkBulbs.Checked
        Static bulbsizeF As SizeF = SizeF.Empty
        bulbsizeF = If(bulbsizeF.Equals(SizeF.Empty), New SizeF(0.1, 0.4), SizeF.Empty)
        For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
            leddisplay.Value.BulbSize = bulbsizeF
        Next
    End Sub
    Private Sub cmbGlowing_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbGlowing.SelectedIndexChanged
        isSettingsScreenDirty = True
        B2SSettings.GlowIndex = cmbGlowing.SelectedIndex
        Dim glow As Integer = B2SSettings.GlowIndex * 8
        If glow = 32 Then glow = B2SSettings.DefaultGlow
        For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
            leddisplay.Value.Glow = glow
        Next
        If B2SSettings.GlowIndex = cmbGlowing.Items.Count - 1 Then B2SSettings.GlowIndex = -1
    End Sub

    Private Sub numLampsSkipFrames_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numLampsSkipFrames.ValueChanged
        isSettingsScreenDirty = True
        B2SSettings.LampsSkipFrames = numLampsSkipFrames.Value
    End Sub
    Private Sub numSolenoidSkipFrames_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numSolenoidsSkipFrames.ValueChanged
        isSettingsScreenDirty = True
        B2SSettings.SolenoidsSkipFrames = numSolenoidsSkipFrames.Value
    End Sub
    Private Sub numGISkipFrames_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numGISkipFrames.ValueChanged
        isSettingsScreenDirty = True
        B2SSettings.GIStringsSkipFrames = numGISkipFrames.Value
    End Sub
    Private Sub numLEDSkipFrames_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numLEDSkipFrames.ValueChanged
        isSettingsScreenDirty = True
        B2SSettings.LEDsSkipFrames = numLEDSkipFrames.Value
    End Sub

    Private ignoreChanges As Boolean = False
    Private Sub cmbAnimations_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbAnimations.SelectedIndexChanged
        If ignoreChanges Then Return
        If cmbAnimations.SelectedItem IsNot Nothing Then
            ignoreChanges = True
            Dim slowdown As Integer = DirectCast(cmbAnimations.SelectedItem, Animations4Settings).SlowDown
            If slowdown = 0 Then
                cmbAnimationSetting.SelectedIndex = cmbAnimationSetting.Items.Count - 1
            ElseIf slowdown >= 1 AndAlso slowdown <= 5 Then
                cmbAnimationSetting.SelectedIndex = slowdown - 1
            ElseIf slowdown = 10 Then
                cmbAnimationSetting.SelectedIndex = 5
            End If
            cmbAnimationSetting.Enabled = Not (B2SSettings.AllAnimationSlowDown <> 1 AndAlso cmbAnimations.SelectedIndex < cmbAnimations.Items.Count - 1)
            ignoreChanges = False
        End If
    End Sub
    Private Sub cmbAnimationSetting_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbAnimationSetting.SelectedIndexChanged
        If ignoreChanges Then Return
        If cmbAnimations.SelectedItem IsNot Nothing Then
            Dim name As String = DirectCast(cmbAnimations.SelectedItem, Animations4Settings).Name
            Dim slowdown As Integer = DirectCast(cmbAnimations.SelectedItem, Animations4Settings).SlowDown
            If cmbAnimationSetting.Text = "Off" Then
                slowdown = 0
            Else
                slowdown = CInt(cmbAnimationSetting.Text.Replace(" x", ""))
            End If
            Dim index As Integer = cmbAnimations.SelectedIndex
            ignoreChanges = True
            cmbAnimations.Items.RemoveAt(index)
            cmbAnimations.Items.Insert(index, New Animations4Settings(name, slowdown))
            cmbAnimations.SelectedIndex = index
            If cmbAnimations.SelectedIndex < cmbAnimations.Items.Count - 1 Then
                B2SAnimation.AnimationSlowDown(name) = slowdown
            Else
                B2SSettings.AllAnimationSlowDown = slowdown
            End If
            ignoreChanges = False
        End If
    End Sub

    Private Sub btnScreenshotPath_Click(sender As System.Object, e As System.EventArgs) Handles btnScreenshotPath.Click
        Using fbd As FolderBrowserDialog = New FolderBrowserDialog()
            fbd.SelectedPath = B2SSettings.ScreenshotPath
            If fbd.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                isSettingsScreenDirty = True
                B2SSettings.ScreenshotPath = fbd.SelectedPath
                btnScreenshotPath.Text = "Screenshot path: " & B2SSettings.ScreenshotPath
            End If
        End Using
    End Sub
    Private Sub cmbScreenshotType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbScreenshotType.SelectedIndexChanged
        isSettingsScreenDirty = True
        B2SSettings.ScreenshotFileType = cmbScreenshotType.SelectedIndex
    End Sub

    Private Sub chkActivatePlugins_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivatePlugins.CheckedChanged
        activateMsgBoxAtSaving = True
        isSettingsScreenDirty = True
        B2SSettings.ArePluginsOn = chkActivatePlugins.Checked
    End Sub
    Private Sub chkShowStartupError_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowStartupError.CheckedChanged
        isSettingsScreenDirty = True
        B2SSettings.ShowStartupError = chkShowStartupError.Checked
    End Sub
    Private Sub btnPluginSettings_Click(sender As Object, e As EventArgs) Handles btnPluginSettings.Click
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S", True)
            With Me 'B2SScreen.BackglassScreen.Bounds
                regkey.SetValue("PluginsScreen", .Location.X & "," & .Location.Y & "," & .Size.Width & "," & .Size.Height)
            End With
            regkey.SetValue("PluginsOpenDialog", 1)
        End Using
    End Sub

    Private Function IsDirty() As Boolean
        Dim ret As Boolean = False
        If isSettingsScreenDirty Then
            Dim result As DialogResult = MessageBox.Show(My.Resources.MSG_IsDirty, My.Resources.AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.Yes Then
                btnSaveSettings.PerformClick()
            ElseIf result = Windows.Forms.DialogResult.Cancel Then
                ret = True
            End If
        End If
        Return ret
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles chkFormFront.CheckedChanged
        B2SSettings.FormToFront = chkFormFront.Checked
    End Sub

    Private Sub chkDisableFuzzyMatching_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisableFuzzyMatching.CheckedChanged
        B2SSettings.DisableFuzzyMatching = chkDisableFuzzyMatching.Checked
    End Sub
End Class