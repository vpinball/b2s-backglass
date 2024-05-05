<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formSettings
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formSettings))
        Me.TimerOpacity = New System.Windows.Forms.Timer(Me.components)
        Me.PanelSettings = New System.Windows.Forms.Panel()
        Me.lowerPanel = New System.Windows.Forms.Panel()
        Me.btnCloseSettings = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.lblNonAvailableSettings = New System.Windows.Forms.Label()
        Me.btnEditScreenRes = New System.Windows.Forms.Button()
        Me.btnMore = New System.Windows.Forms.Button()
        Me.headerPanel = New System.Windows.Forms.Panel()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.B2SLogo = New System.Windows.Forms.PictureBox()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.grpPlugins = New System.Windows.Forms.GroupBox()
        Me.chkShowStartupError = New System.Windows.Forms.CheckBox()
        Me.chkActivatePlugins = New System.Windows.Forms.CheckBox()
        Me.btnPluginSettings = New System.Windows.Forms.Button()
        Me.grpStartMode = New System.Windows.Forms.GroupBox()
        Me.chkDisableFuzzyMatching = New System.Windows.Forms.CheckBox()
        Me.lblDefaultStartMode = New System.Windows.Forms.Label()
        Me.chkStartAsEXE = New System.Windows.Forms.CheckBox()
        Me.cmbDefaultStartMode = New System.Windows.Forms.ComboBox()
        Me.grpGeneral = New System.Windows.Forms.GroupBox()
        Me.btnHyperpin = New System.Windows.Forms.Button()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.cmbMode = New System.Windows.Forms.ComboBox()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.cmbMatchingFileNames = New System.Windows.Forms.ComboBox()
        Me.btnCheck = New System.Windows.Forms.Button()
        Me.grpAnimationSettings = New System.Windows.Forms.GroupBox()
        Me.cmbAnimationSetting = New System.Windows.Forms.ComboBox()
        Me.cmbAnimations = New System.Windows.Forms.ComboBox()
        Me.grpScreenshot = New System.Windows.Forms.GroupBox()
        Me.lblFileType = New System.Windows.Forms.Label()
        Me.cmbScreenshotType = New System.Windows.Forms.ComboBox()
        Me.btnScreenshotPath = New System.Windows.Forms.Button()
        Me.grpVisibility = New System.Windows.Forms.GroupBox()
        Me.chkFormNoFocus = New System.Windows.Forms.CheckBox()
        Me.lblBackground = New System.Windows.Forms.Label()
        Me.lblFormFront = New System.Windows.Forms.Label()
        Me.cmbFormFront = New System.Windows.Forms.ComboBox()
        Me.cmbBackground = New System.Windows.Forms.ComboBox()
        Me.cmbB2SDMD = New System.Windows.Forms.ComboBox()
        Me.lblB2SDMD = New System.Windows.Forms.Label()
        Me.cmbDMD = New System.Windows.Forms.ComboBox()
        Me.lblDMD = New System.Windows.Forms.Label()
        Me.cmbGrill = New System.Windows.Forms.ComboBox()
        Me.lblGrill = New System.Windows.Forms.Label()
        Me.grpLEDs = New System.Windows.Forms.GroupBox()
        Me.lblGlowing = New System.Windows.Forms.Label()
        Me.radioStandardLED = New System.Windows.Forms.RadioButton()
        Me.cmbGlowing = New System.Windows.Forms.ComboBox()
        Me.radioDream7LED = New System.Windows.Forms.RadioButton()
        Me.chkWireframe = New System.Windows.Forms.CheckBox()
        Me.chkBulbs = New System.Windows.Forms.CheckBox()
        Me.grpPerfTuning = New System.Windows.Forms.GroupBox()
        Me.numLampsSkipFrames = New System.Windows.Forms.NumericUpDown()
        Me.lblLampsBlackTurns = New System.Windows.Forms.Label()
        Me.lblLEDBlackTurns = New System.Windows.Forms.Label()
        Me.numSolenoidsSkipFrames = New System.Windows.Forms.NumericUpDown()
        Me.numLEDSkipFrames = New System.Windows.Forms.NumericUpDown()
        Me.lblSolenoidBlackTurns = New System.Windows.Forms.Label()
        Me.numGISkipFrames = New System.Windows.Forms.NumericUpDown()
        Me.lblGIBlackTurns = New System.Windows.Forms.Label()
        Me.B2SLogoToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbB2SBackglass = New System.Windows.Forms.ComboBox()
        Me.lblB2SBackglass = New System.Windows.Forms.Label()
        Me.PanelSettings.SuspendLayout()
        Me.lowerPanel.SuspendLayout()
        Me.headerPanel.SuspendLayout()
        CType(Me.B2SLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPlugins.SuspendLayout()
        Me.grpStartMode.SuspendLayout()
        Me.grpGeneral.SuspendLayout()
        Me.grpAnimationSettings.SuspendLayout()
        Me.grpScreenshot.SuspendLayout()
        Me.grpVisibility.SuspendLayout()
        Me.grpLEDs.SuspendLayout()
        Me.grpPerfTuning.SuspendLayout()
        CType(Me.numLampsSkipFrames, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numSolenoidsSkipFrames, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numLEDSkipFrames, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numGISkipFrames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimerOpacity
        '
        '
        'PanelSettings
        '
        Me.PanelSettings.Controls.Add(Me.lowerPanel)
        Me.PanelSettings.Controls.Add(Me.headerPanel)
        Me.PanelSettings.Controls.Add(Me.grpPlugins)
        Me.PanelSettings.Controls.Add(Me.grpStartMode)
        Me.PanelSettings.Controls.Add(Me.grpGeneral)
        Me.PanelSettings.Controls.Add(Me.grpAnimationSettings)
        Me.PanelSettings.Controls.Add(Me.grpScreenshot)
        Me.PanelSettings.Controls.Add(Me.grpVisibility)
        Me.PanelSettings.Controls.Add(Me.grpLEDs)
        Me.PanelSettings.Controls.Add(Me.grpPerfTuning)
        Me.PanelSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSettings.Location = New System.Drawing.Point(0, 0)
        Me.PanelSettings.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.PanelSettings.Name = "PanelSettings"
        Me.PanelSettings.Size = New System.Drawing.Size(1072, 1406)
        Me.PanelSettings.TabIndex = 2
        '
        'lowerPanel
        '
        Me.lowerPanel.Controls.Add(Me.btnCloseSettings)
        Me.lowerPanel.Controls.Add(Me.btnSaveSettings)
        Me.lowerPanel.Controls.Add(Me.lblNonAvailableSettings)
        Me.lowerPanel.Controls.Add(Me.btnEditScreenRes)
        Me.lowerPanel.Controls.Add(Me.btnMore)
        Me.lowerPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lowerPanel.Location = New System.Drawing.Point(0, 1260)
        Me.lowerPanel.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.lowerPanel.Name = "lowerPanel"
        Me.lowerPanel.Size = New System.Drawing.Size(1072, 146)
        Me.lowerPanel.TabIndex = 42
        '
        'btnCloseSettings
        '
        Me.btnCloseSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseSettings.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCloseSettings.Location = New System.Drawing.Point(892, 74)
        Me.btnCloseSettings.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnCloseSettings.Name = "btnCloseSettings"
        Me.btnCloseSettings.Size = New System.Drawing.Size(156, 48)
        Me.btnCloseSettings.TabIndex = 35
        Me.btnCloseSettings.Text = "Close"
        Me.btnCloseSettings.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSettings.Location = New System.Drawing.Point(624, 74)
        Me.btnSaveSettings.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(256, 48)
        Me.btnSaveSettings.TabIndex = 34
        Me.btnSaveSettings.Text = "Save settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'lblNonAvailableSettings
        '
        Me.lblNonAvailableSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNonAvailableSettings.Location = New System.Drawing.Point(458, 32)
        Me.lblNonAvailableSettings.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblNonAvailableSettings.Name = "lblNonAvailableSettings"
        Me.lblNonAvailableSettings.Size = New System.Drawing.Size(590, 26)
        Me.lblNonAvailableSettings.TabIndex = 40
        Me.lblNonAvailableSettings.Text = "* all settings in Italic is only available when run in EXE mode!"
        '
        'btnEditScreenRes
        '
        Me.btnEditScreenRes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEditScreenRes.Location = New System.Drawing.Point(182, 74)
        Me.btnEditScreenRes.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnEditScreenRes.Name = "btnEditScreenRes"
        Me.btnEditScreenRes.Size = New System.Drawing.Size(198, 48)
        Me.btnEditScreenRes.TabIndex = 39
        Me.btnEditScreenRes.Text = "Edit ScreenRes..."
        Me.btnEditScreenRes.UseVisualStyleBackColor = True
        '
        'btnMore
        '
        Me.btnMore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMore.Location = New System.Drawing.Point(24, 74)
        Me.btnMore.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnMore.Name = "btnMore"
        Me.btnMore.Size = New System.Drawing.Size(146, 48)
        Me.btnMore.TabIndex = 32
        Me.btnMore.Text = "More ..."
        Me.btnMore.UseVisualStyleBackColor = True
        '
        'headerPanel
        '
        Me.headerPanel.Controls.Add(Me.lblVersion)
        Me.headerPanel.Controls.Add(Me.B2SLogo)
        Me.headerPanel.Controls.Add(Me.lblCopyright)
        Me.headerPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.headerPanel.Location = New System.Drawing.Point(0, 0)
        Me.headerPanel.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.headerPanel.Name = "headerPanel"
        Me.headerPanel.Size = New System.Drawing.Size(1072, 116)
        Me.headerPanel.TabIndex = 41
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(140, 70)
        Me.lblVersion.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(436, 25)
        Me.lblVersion.TabIndex = 38
        Me.lblVersion.Text = "Server version {0}, Backglass file version {1}"
        '
        'B2SLogo
        '
        Me.B2SLogo.Image = CType(resources.GetObject("B2SLogo.Image"), System.Drawing.Image)
        Me.B2SLogo.Location = New System.Drawing.Point(12, 0)
        Me.B2SLogo.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.B2SLogo.Name = "B2SLogo"
        Me.B2SLogo.Size = New System.Drawing.Size(116, 96)
        Me.B2SLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.B2SLogo.TabIndex = 2
        Me.B2SLogo.TabStop = False
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Location = New System.Drawing.Point(140, 4)
        Me.lblCopyright.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(463, 50)
        Me.lblCopyright.TabIndex = 23
        Me.lblCopyright.Text = "{0}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "{1} by Herweh && B2S Team. All rights reserved."
        '
        'grpPlugins
        '
        Me.grpPlugins.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.grpPlugins.AutoSize = True
        Me.grpPlugins.Controls.Add(Me.chkShowStartupError)
        Me.grpPlugins.Controls.Add(Me.chkActivatePlugins)
        Me.grpPlugins.Controls.Add(Me.btnPluginSettings)
        Me.grpPlugins.Location = New System.Drawing.Point(18, 1114)
        Me.grpPlugins.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpPlugins.Name = "grpPlugins"
        Me.grpPlugins.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpPlugins.Size = New System.Drawing.Size(1030, 134)
        Me.grpPlugins.TabIndex = 37
        Me.grpPlugins.TabStop = False
        Me.grpPlugins.Text = "Plugins"
        '
        'chkShowStartupError
        '
        Me.chkShowStartupError.AutoSize = True
        Me.chkShowStartupError.Location = New System.Drawing.Point(296, 60)
        Me.chkShowStartupError.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkShowStartupError.Name = "chkShowStartupError"
        Me.chkShowStartupError.Size = New System.Drawing.Size(361, 29)
        Me.chkShowStartupError.TabIndex = 21
        Me.chkShowStartupError.Text = "Error message without backglass"
        Me.chkShowStartupError.UseVisualStyleBackColor = True
        '
        'chkActivatePlugins
        '
        Me.chkActivatePlugins.AutoSize = True
        Me.chkActivatePlugins.Location = New System.Drawing.Point(12, 60)
        Me.chkActivatePlugins.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkActivatePlugins.Name = "chkActivatePlugins"
        Me.chkActivatePlugins.Size = New System.Drawing.Size(196, 29)
        Me.chkActivatePlugins.TabIndex = 20
        Me.chkActivatePlugins.Text = "Activate plugins"
        Me.chkActivatePlugins.UseVisualStyleBackColor = True
        '
        'btnPluginSettings
        '
        Me.btnPluginSettings.Enabled = False
        Me.btnPluginSettings.Location = New System.Drawing.Point(722, 50)
        Me.btnPluginSettings.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnPluginSettings.Name = "btnPluginSettings"
        Me.btnPluginSettings.Size = New System.Drawing.Size(240, 48)
        Me.btnPluginSettings.TabIndex = 22
        Me.btnPluginSettings.Text = "Plugin settings"
        Me.btnPluginSettings.UseVisualStyleBackColor = True
        '
        'grpStartMode
        '
        Me.grpStartMode.Controls.Add(Me.chkDisableFuzzyMatching)
        Me.grpStartMode.Controls.Add(Me.lblDefaultStartMode)
        Me.grpStartMode.Controls.Add(Me.chkStartAsEXE)
        Me.grpStartMode.Controls.Add(Me.cmbDefaultStartMode)
        Me.grpStartMode.Location = New System.Drawing.Point(24, 460)
        Me.grpStartMode.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpStartMode.Name = "grpStartMode"
        Me.grpStartMode.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpStartMode.Size = New System.Drawing.Size(1030, 98)
        Me.grpStartMode.TabIndex = 2
        Me.grpStartMode.TabStop = False
        Me.grpStartMode.Text = "Backglass start mode"
        '
        'chkDisableFuzzyMatching
        '
        Me.chkDisableFuzzyMatching.AutoSize = True
        Me.chkDisableFuzzyMatching.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDisableFuzzyMatching.Location = New System.Drawing.Point(654, 46)
        Me.chkDisableFuzzyMatching.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkDisableFuzzyMatching.Name = "chkDisableFuzzyMatching"
        Me.chkDisableFuzzyMatching.Size = New System.Drawing.Size(265, 30)
        Me.chkDisableFuzzyMatching.TabIndex = 40
        Me.chkDisableFuzzyMatching.Text = "Exact .directb2s match"
        Me.chkDisableFuzzyMatching.UseVisualStyleBackColor = True
        '
        'lblDefaultStartMode
        '
        Me.lblDefaultStartMode.AutoSize = True
        Me.lblDefaultStartMode.Location = New System.Drawing.Point(324, 46)
        Me.lblDefaultStartMode.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblDefaultStartMode.Name = "lblDefaultStartMode"
        Me.lblDefaultStartMode.Size = New System.Drawing.Size(145, 25)
        Me.lblDefaultStartMode.TabIndex = 26
        Me.lblDefaultStartMode.Text = "Default mode:"
        '
        'chkStartAsEXE
        '
        Me.chkStartAsEXE.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkStartAsEXE.Location = New System.Drawing.Point(12, 40)
        Me.chkStartAsEXE.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkStartAsEXE.Name = "chkStartAsEXE"
        Me.chkStartAsEXE.Size = New System.Drawing.Size(302, 42)
        Me.chkStartAsEXE.TabIndex = 6
        Me.chkStartAsEXE.Text = "Start this backglass as EXE"
        Me.chkStartAsEXE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStartAsEXE.UseVisualStyleBackColor = True
        '
        'cmbDefaultStartMode
        '
        Me.cmbDefaultStartMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultStartMode.FormattingEnabled = True
        Me.cmbDefaultStartMode.Items.AddRange(New Object() {"Standard", "In EXE"})
        Me.cmbDefaultStartMode.Location = New System.Drawing.Point(484, 40)
        Me.cmbDefaultStartMode.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbDefaultStartMode.Name = "cmbDefaultStartMode"
        Me.cmbDefaultStartMode.Size = New System.Drawing.Size(148, 33)
        Me.cmbDefaultStartMode.TabIndex = 7
        '
        'grpGeneral
        '
        Me.grpGeneral.Controls.Add(Me.btnHyperpin)
        Me.grpGeneral.Controls.Add(Me.lblFile)
        Me.grpGeneral.Controls.Add(Me.cmbMode)
        Me.grpGeneral.Controls.Add(Me.lblMode)
        Me.grpGeneral.Controls.Add(Me.cmbMatchingFileNames)
        Me.grpGeneral.Controls.Add(Me.btnCheck)
        Me.grpGeneral.Location = New System.Drawing.Point(24, 128)
        Me.grpGeneral.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpGeneral.Name = "grpGeneral"
        Me.grpGeneral.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpGeneral.Size = New System.Drawing.Size(1030, 102)
        Me.grpGeneral.TabIndex = 0
        Me.grpGeneral.TabStop = False
        Me.grpGeneral.Text = "General"
        '
        'btnHyperpin
        '
        Me.btnHyperpin.Location = New System.Drawing.Point(740, 36)
        Me.btnHyperpin.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnHyperpin.Name = "btnHyperpin"
        Me.btnHyperpin.Size = New System.Drawing.Size(196, 48)
        Me.btnHyperpin.TabIndex = 2
        Me.btnHyperpin.Text = "Locate VP.xml"
        Me.btnHyperpin.UseVisualStyleBackColor = True
        '
        'lblFile
        '
        Me.lblFile.AutoSize = True
        Me.lblFile.Location = New System.Drawing.Point(324, 48)
        Me.lblFile.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(53, 25)
        Me.lblFile.TabIndex = 15
        Me.lblFile.Text = "File:"
        Me.lblFile.Visible = False
        '
        'cmbMode
        '
        Me.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMode.FormattingEnabled = True
        Me.cmbMode.Items.AddRange(New Object() {"Authentic", "Fantasy"})
        Me.cmbMode.Location = New System.Drawing.Point(94, 40)
        Me.cmbMode.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbMode.Name = "cmbMode"
        Me.cmbMode.Size = New System.Drawing.Size(488, 33)
        Me.cmbMode.TabIndex = 0
        '
        'lblMode
        '
        Me.lblMode.AutoSize = True
        Me.lblMode.Location = New System.Drawing.Point(8, 46)
        Me.lblMode.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(72, 25)
        Me.lblMode.TabIndex = 13
        Me.lblMode.Text = "Mode:"
        Me.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMatchingFileNames
        '
        Me.cmbMatchingFileNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMatchingFileNames.FormattingEnabled = True
        Me.cmbMatchingFileNames.Location = New System.Drawing.Point(414, 40)
        Me.cmbMatchingFileNames.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbMatchingFileNames.Name = "cmbMatchingFileNames"
        Me.cmbMatchingFileNames.Size = New System.Drawing.Size(308, 33)
        Me.cmbMatchingFileNames.Sorted = True
        Me.cmbMatchingFileNames.TabIndex = 1
        Me.cmbMatchingFileNames.Visible = False
        '
        'btnCheck
        '
        Me.btnCheck.Location = New System.Drawing.Point(740, 36)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(196, 48)
        Me.btnCheck.TabIndex = 2
        Me.btnCheck.Text = "Check monitors"
        Me.btnCheck.UseVisualStyleBackColor = True
        Me.btnCheck.Visible = False
        '
        'grpAnimationSettings
        '
        Me.grpAnimationSettings.Controls.Add(Me.cmbAnimationSetting)
        Me.grpAnimationSettings.Controls.Add(Me.cmbAnimations)
        Me.grpAnimationSettings.Location = New System.Drawing.Point(24, 900)
        Me.grpAnimationSettings.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpAnimationSettings.Name = "grpAnimationSettings"
        Me.grpAnimationSettings.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpAnimationSettings.Size = New System.Drawing.Size(1030, 102)
        Me.grpAnimationSettings.TabIndex = 5
        Me.grpAnimationSettings.TabStop = False
        Me.grpAnimationSettings.Text = "Animation settings and tuning"
        '
        'cmbAnimationSetting
        '
        Me.cmbAnimationSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnimationSetting.FormattingEnabled = True
        Me.cmbAnimationSetting.Items.AddRange(New Object() {"1 x", "2 x", "3 x", "4 x", "5 x", "10 x", "Off"})
        Me.cmbAnimationSetting.Location = New System.Drawing.Point(478, 40)
        Me.cmbAnimationSetting.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbAnimationSetting.Name = "cmbAnimationSetting"
        Me.cmbAnimationSetting.Size = New System.Drawing.Size(218, 33)
        Me.cmbAnimationSetting.TabIndex = 17
        '
        'cmbAnimations
        '
        Me.cmbAnimations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnimations.FormattingEnabled = True
        Me.cmbAnimations.Location = New System.Drawing.Point(14, 40)
        Me.cmbAnimations.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbAnimations.Name = "cmbAnimations"
        Me.cmbAnimations.Size = New System.Drawing.Size(448, 33)
        Me.cmbAnimations.TabIndex = 16
        '
        'grpScreenshot
        '
        Me.grpScreenshot.Controls.Add(Me.lblFileType)
        Me.grpScreenshot.Controls.Add(Me.cmbScreenshotType)
        Me.grpScreenshot.Controls.Add(Me.btnScreenshotPath)
        Me.grpScreenshot.Location = New System.Drawing.Point(24, 1014)
        Me.grpScreenshot.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpScreenshot.Name = "grpScreenshot"
        Me.grpScreenshot.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpScreenshot.Size = New System.Drawing.Size(1030, 100)
        Me.grpScreenshot.TabIndex = 6
        Me.grpScreenshot.TabStop = False
        Me.grpScreenshot.Text = "Screenshot"
        '
        'lblFileType
        '
        Me.lblFileType.AutoSize = True
        Me.lblFileType.Location = New System.Drawing.Point(536, 38)
        Me.lblFileType.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblFileType.Name = "lblFileType"
        Me.lblFileType.Size = New System.Drawing.Size(157, 25)
        Me.lblFileType.TabIndex = 0
        Me.lblFileType.Text = "Image file type:"
        Me.lblFileType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbScreenshotType
        '
        Me.cmbScreenshotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbScreenshotType.FormattingEnabled = True
        Me.cmbScreenshotType.Items.AddRange(New Object() {"PNG", "JPG", "GIF", "BMP"})
        Me.cmbScreenshotType.Location = New System.Drawing.Point(714, 32)
        Me.cmbScreenshotType.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbScreenshotType.Name = "cmbScreenshotType"
        Me.cmbScreenshotType.Size = New System.Drawing.Size(218, 33)
        Me.cmbScreenshotType.TabIndex = 19
        '
        'btnScreenshotPath
        '
        Me.btnScreenshotPath.Location = New System.Drawing.Point(16, 32)
        Me.btnScreenshotPath.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnScreenshotPath.Name = "btnScreenshotPath"
        Me.btnScreenshotPath.Size = New System.Drawing.Size(452, 48)
        Me.btnScreenshotPath.TabIndex = 18
        Me.btnScreenshotPath.Text = "Screenshot path"
        Me.btnScreenshotPath.UseVisualStyleBackColor = True
        '
        'grpVisibility
        '
        Me.grpVisibility.Controls.Add(Me.cmbB2SBackglass)
        Me.grpVisibility.Controls.Add(Me.lblB2SBackglass)
        Me.grpVisibility.Controls.Add(Me.chkFormNoFocus)
        Me.grpVisibility.Controls.Add(Me.lblBackground)
        Me.grpVisibility.Controls.Add(Me.lblFormFront)
        Me.grpVisibility.Controls.Add(Me.cmbFormFront)
        Me.grpVisibility.Controls.Add(Me.cmbBackground)
        Me.grpVisibility.Controls.Add(Me.cmbB2SDMD)
        Me.grpVisibility.Controls.Add(Me.lblB2SDMD)
        Me.grpVisibility.Controls.Add(Me.cmbDMD)
        Me.grpVisibility.Controls.Add(Me.lblDMD)
        Me.grpVisibility.Controls.Add(Me.cmbGrill)
        Me.grpVisibility.Controls.Add(Me.lblGrill)
        Me.grpVisibility.Location = New System.Drawing.Point(24, 240)
        Me.grpVisibility.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpVisibility.Name = "grpVisibility"
        Me.grpVisibility.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpVisibility.Size = New System.Drawing.Size(1030, 208)
        Me.grpVisibility.TabIndex = 1
        Me.grpVisibility.TabStop = False
        Me.grpVisibility.Text = "Visibility"
        '
        'chkFormNoFocus
        '
        Me.chkFormNoFocus.AutoSize = True
        Me.chkFormNoFocus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormNoFocus.Location = New System.Drawing.Point(372, 156)
        Me.chkFormNoFocus.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkFormNoFocus.Name = "chkFormNoFocus"
        Me.chkFormNoFocus.Size = New System.Drawing.Size(194, 30)
        Me.chkFormNoFocus.TabIndex = 42
        Me.chkFormNoFocus.Text = "Form No Focus"
        Me.chkFormNoFocus.UseVisualStyleBackColor = True
        '
        'lblBackground
        '
        Me.lblBackground.AutoSize = True
        Me.lblBackground.Location = New System.Drawing.Point(626, 158)
        Me.lblBackground.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblBackground.Name = "lblBackground"
        Me.lblBackground.Size = New System.Drawing.Size(133, 25)
        Me.lblBackground.TabIndex = 17
        Me.lblBackground.Text = "Background:"
        '
        'lblFormFront
        '
        Me.lblFormFront.AutoSize = True
        Me.lblFormFront.Location = New System.Drawing.Point(24, 158)
        Me.lblFormFront.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblFormFront.Name = "lblFormFront"
        Me.lblFormFront.Size = New System.Drawing.Size(128, 25)
        Me.lblFormFront.TabIndex = 41
        Me.lblFormFront.Text = "Bring Forms"
        '
        'cmbFormFront
        '
        Me.cmbFormFront.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFormFront.FormattingEnabled = True
        Me.cmbFormFront.Items.AddRange(New Object() {"Form to Back", "Standard", "Form to Top"})
        Me.cmbFormFront.Location = New System.Drawing.Point(160, 152)
        Me.cmbFormFront.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbFormFront.Name = "cmbFormFront"
        Me.cmbFormFront.Size = New System.Drawing.Size(176, 33)
        Me.cmbFormFront.TabIndex = 27
        '
        'cmbBackground
        '
        Me.cmbBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBackground.FormattingEnabled = True
        Me.cmbBackground.Items.AddRange(New Object() {"Visible", "Hidden", "Standard"})
        Me.cmbBackground.Location = New System.Drawing.Point(782, 152)
        Me.cmbBackground.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbBackground.Name = "cmbBackground"
        Me.cmbBackground.Size = New System.Drawing.Size(156, 33)
        Me.cmbBackground.TabIndex = 16
        '
        'cmbB2SDMD
        '
        Me.cmbB2SDMD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbB2SDMD.FormattingEnabled = True
        Me.cmbB2SDMD.Items.AddRange(New Object() {"Visible", "Hidden"})
        Me.cmbB2SDMD.Location = New System.Drawing.Point(290, 94)
        Me.cmbB2SDMD.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbB2SDMD.Name = "cmbB2SDMD"
        Me.cmbB2SDMD.Size = New System.Drawing.Size(156, 33)
        Me.cmbB2SDMD.TabIndex = 5
        '
        'lblB2SDMD
        '
        Me.lblB2SDMD.AutoSize = True
        Me.lblB2SDMD.Location = New System.Drawing.Point(168, 100)
        Me.lblB2SDMD.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblB2SDMD.Name = "lblB2SDMD"
        Me.lblB2SDMD.Size = New System.Drawing.Size(112, 25)
        Me.lblB2SDMD.TabIndex = 15
        Me.lblB2SDMD.Text = "B2S DMD:"
        '
        'cmbDMD
        '
        Me.cmbDMD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDMD.FormattingEnabled = True
        Me.cmbDMD.Items.AddRange(New Object() {"Visible", "Hidden", "Standard"})
        Me.cmbDMD.Location = New System.Drawing.Point(590, 36)
        Me.cmbDMD.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbDMD.Name = "cmbDMD"
        Me.cmbDMD.Size = New System.Drawing.Size(156, 33)
        Me.cmbDMD.TabIndex = 4
        '
        'lblDMD
        '
        Me.lblDMD.AutoSize = True
        Me.lblDMD.Location = New System.Drawing.Point(520, 42)
        Me.lblDMD.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblDMD.Name = "lblDMD"
        Me.lblDMD.Size = New System.Drawing.Size(66, 25)
        Me.lblDMD.TabIndex = 13
        Me.lblDMD.Text = "DMD:"
        '
        'cmbGrill
        '
        Me.cmbGrill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGrill.FormattingEnabled = True
        Me.cmbGrill.Items.AddRange(New Object() {"Visible", "Hidden", "Standard"})
        Me.cmbGrill.Location = New System.Drawing.Point(290, 36)
        Me.cmbGrill.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbGrill.Name = "cmbGrill"
        Me.cmbGrill.Size = New System.Drawing.Size(156, 33)
        Me.cmbGrill.TabIndex = 3
        '
        'lblGrill
        '
        Me.lblGrill.AutoSize = True
        Me.lblGrill.Location = New System.Drawing.Point(226, 42)
        Me.lblGrill.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblGrill.Name = "lblGrill"
        Me.lblGrill.Size = New System.Drawing.Size(56, 25)
        Me.lblGrill.TabIndex = 11
        Me.lblGrill.Text = "Grill:"
        '
        'grpLEDs
        '
        Me.grpLEDs.Controls.Add(Me.lblGlowing)
        Me.grpLEDs.Controls.Add(Me.radioStandardLED)
        Me.grpLEDs.Controls.Add(Me.cmbGlowing)
        Me.grpLEDs.Controls.Add(Me.radioDream7LED)
        Me.grpLEDs.Controls.Add(Me.chkWireframe)
        Me.grpLEDs.Controls.Add(Me.chkBulbs)
        Me.grpLEDs.Location = New System.Drawing.Point(24, 728)
        Me.grpLEDs.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpLEDs.Name = "grpLEDs"
        Me.grpLEDs.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpLEDs.Size = New System.Drawing.Size(1030, 160)
        Me.grpLEDs.TabIndex = 4
        Me.grpLEDs.TabStop = False
        Me.grpLEDs.Text = "LED settings and tuning"
        '
        'lblGlowing
        '
        Me.lblGlowing.Location = New System.Drawing.Point(606, 106)
        Me.lblGlowing.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblGlowing.Name = "lblGlowing"
        Me.lblGlowing.Size = New System.Drawing.Size(96, 26)
        Me.lblGlowing.TabIndex = 25
        Me.lblGlowing.Text = "Glowing:"
        Me.lblGlowing.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'radioStandardLED
        '
        Me.radioStandardLED.Appearance = System.Windows.Forms.Appearance.Button
        Me.radioStandardLED.Location = New System.Drawing.Point(12, 40)
        Me.radioStandardLED.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.radioStandardLED.Name = "radioStandardLED"
        Me.radioStandardLED.Size = New System.Drawing.Size(222, 48)
        Me.radioStandardLED.TabIndex = 11
        Me.radioStandardLED.TabStop = True
        Me.radioStandardLED.Text = "Use simple LEDs"
        Me.radioStandardLED.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.radioStandardLED.UseVisualStyleBackColor = True
        '
        'cmbGlowing
        '
        Me.cmbGlowing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGlowing.FormattingEnabled = True
        Me.cmbGlowing.Items.AddRange(New Object() {"Off", "Low", "Medium", "High", "Default"})
        Me.cmbGlowing.Location = New System.Drawing.Point(714, 100)
        Me.cmbGlowing.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cmbGlowing.Name = "cmbGlowing"
        Me.cmbGlowing.Size = New System.Drawing.Size(218, 33)
        Me.cmbGlowing.TabIndex = 15
        '
        'radioDream7LED
        '
        Me.radioDream7LED.Appearance = System.Windows.Forms.Appearance.Button
        Me.radioDream7LED.Location = New System.Drawing.Point(248, 40)
        Me.radioDream7LED.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.radioDream7LED.Name = "radioDream7LED"
        Me.radioDream7LED.Size = New System.Drawing.Size(220, 48)
        Me.radioDream7LED.TabIndex = 12
        Me.radioDream7LED.TabStop = True
        Me.radioDream7LED.Text = "Use 'Dream7' LEDs"
        Me.radioDream7LED.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.radioDream7LED.UseVisualStyleBackColor = True
        '
        'chkWireframe
        '
        Me.chkWireframe.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkWireframe.Location = New System.Drawing.Point(480, 40)
        Me.chkWireframe.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkWireframe.Name = "chkWireframe"
        Me.chkWireframe.Size = New System.Drawing.Size(222, 48)
        Me.chkWireframe.TabIndex = 13
        Me.chkWireframe.Text = "LED &wireframe on"
        Me.chkWireframe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkWireframe.UseVisualStyleBackColor = True
        '
        'chkBulbs
        '
        Me.chkBulbs.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkBulbs.Location = New System.Drawing.Point(714, 40)
        Me.chkBulbs.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.chkBulbs.Name = "chkBulbs"
        Me.chkBulbs.Size = New System.Drawing.Size(222, 48)
        Me.chkBulbs.TabIndex = 14
        Me.chkBulbs.Text = "LED &bulb light on"
        Me.chkBulbs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkBulbs.UseVisualStyleBackColor = True
        '
        'grpPerfTuning
        '
        Me.grpPerfTuning.Controls.Add(Me.numLampsSkipFrames)
        Me.grpPerfTuning.Controls.Add(Me.lblLampsBlackTurns)
        Me.grpPerfTuning.Controls.Add(Me.lblLEDBlackTurns)
        Me.grpPerfTuning.Controls.Add(Me.numSolenoidsSkipFrames)
        Me.grpPerfTuning.Controls.Add(Me.numLEDSkipFrames)
        Me.grpPerfTuning.Controls.Add(Me.lblSolenoidBlackTurns)
        Me.grpPerfTuning.Controls.Add(Me.numGISkipFrames)
        Me.grpPerfTuning.Controls.Add(Me.lblGIBlackTurns)
        Me.grpPerfTuning.Location = New System.Drawing.Point(24, 570)
        Me.grpPerfTuning.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpPerfTuning.Name = "grpPerfTuning"
        Me.grpPerfTuning.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.grpPerfTuning.Size = New System.Drawing.Size(1030, 146)
        Me.grpPerfTuning.TabIndex = 3
        Me.grpPerfTuning.TabStop = False
        Me.grpPerfTuning.Text = "Performance tuning"
        '
        'numLampsSkipFrames
        '
        Me.numLampsSkipFrames.Location = New System.Drawing.Point(234, 36)
        Me.numLampsSkipFrames.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.numLampsSkipFrames.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numLampsSkipFrames.Name = "numLampsSkipFrames"
        Me.numLampsSkipFrames.Size = New System.Drawing.Size(102, 31)
        Me.numLampsSkipFrames.TabIndex = 8
        '
        'lblLampsBlackTurns
        '
        Me.lblLampsBlackTurns.AutoSize = True
        Me.lblLampsBlackTurns.Location = New System.Drawing.Point(28, 44)
        Me.lblLampsBlackTurns.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblLampsBlackTurns.Name = "lblLampsBlackTurns"
        Me.lblLampsBlackTurns.Size = New System.Drawing.Size(183, 25)
        Me.lblLampsBlackTurns.TabIndex = 10
        Me.lblLampsBlackTurns.Text = "Skip lamp frames:"
        '
        'lblLEDBlackTurns
        '
        Me.lblLEDBlackTurns.AutoSize = True
        Me.lblLEDBlackTurns.Location = New System.Drawing.Point(502, 92)
        Me.lblLEDBlackTurns.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblLEDBlackTurns.Name = "lblLEDBlackTurns"
        Me.lblLEDBlackTurns.Size = New System.Drawing.Size(178, 25)
        Me.lblLEDBlackTurns.TabIndex = 22
        Me.lblLEDBlackTurns.Text = "Skip LED frames:"
        '
        'numSolenoidsSkipFrames
        '
        Me.numSolenoidsSkipFrames.Location = New System.Drawing.Point(690, 36)
        Me.numSolenoidsSkipFrames.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.numSolenoidsSkipFrames.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.numSolenoidsSkipFrames.Name = "numSolenoidsSkipFrames"
        Me.numSolenoidsSkipFrames.Size = New System.Drawing.Size(102, 31)
        Me.numSolenoidsSkipFrames.TabIndex = 9
        '
        'numLEDSkipFrames
        '
        Me.numLEDSkipFrames.Location = New System.Drawing.Point(690, 88)
        Me.numLEDSkipFrames.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.numLEDSkipFrames.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numLEDSkipFrames.Name = "numLEDSkipFrames"
        Me.numLEDSkipFrames.Size = New System.Drawing.Size(102, 31)
        Me.numLEDSkipFrames.TabIndex = 11
        '
        'lblSolenoidBlackTurns
        '
        Me.lblSolenoidBlackTurns.AutoSize = True
        Me.lblSolenoidBlackTurns.Location = New System.Drawing.Point(460, 40)
        Me.lblSolenoidBlackTurns.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblSolenoidBlackTurns.Name = "lblSolenoidBlackTurns"
        Me.lblSolenoidBlackTurns.Size = New System.Drawing.Size(218, 25)
        Me.lblSolenoidBlackTurns.TabIndex = 12
        Me.lblSolenoidBlackTurns.Text = "Skip solenoid frames:"
        '
        'numGISkipFrames
        '
        Me.numGISkipFrames.Location = New System.Drawing.Point(234, 88)
        Me.numGISkipFrames.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.numGISkipFrames.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.numGISkipFrames.Name = "numGISkipFrames"
        Me.numGISkipFrames.Size = New System.Drawing.Size(102, 31)
        Me.numGISkipFrames.TabIndex = 10
        '
        'lblGIBlackTurns
        '
        Me.lblGIBlackTurns.AutoSize = True
        Me.lblGIBlackTurns.Location = New System.Drawing.Point(50, 94)
        Me.lblGIBlackTurns.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblGIBlackTurns.Name = "lblGIBlackTurns"
        Me.lblGIBlackTurns.Size = New System.Drawing.Size(158, 25)
        Me.lblGIBlackTurns.TabIndex = 19
        Me.lblGIBlackTurns.Text = "Skip GI frames:"
        '
        'cmbB2SBackglass
        '
        Me.cmbB2SBackglass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbB2SBackglass.FormattingEnabled = True
        Me.cmbB2SBackglass.Items.AddRange(New Object() {"Visible", "Hidden"})
        Me.cmbB2SBackglass.Location = New System.Drawing.Point(590, 92)
        Me.cmbB2SBackglass.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbB2SBackglass.Name = "cmbB2SBackglass"
        Me.cmbB2SBackglass.Size = New System.Drawing.Size(156, 33)
        Me.cmbB2SBackglass.TabIndex = 43
        '
        'lblB2SBackglass
        '
        Me.lblB2SBackglass.AutoSize = True
        Me.lblB2SBackglass.Location = New System.Drawing.Point(468, 98)
        Me.lblB2SBackglass.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblB2SBackglass.Name = "lblB2SBackglass"
        Me.lblB2SBackglass.Size = New System.Drawing.Size(117, 25)
        Me.lblB2SBackglass.TabIndex = 44
        Me.lblB2SBackglass.Text = "Backglass:"
        '
        'formSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1072, 1406)
        Me.Controls.Add(Me.PanelSettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formSettings"
        Me.Opacity = 0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings..."
        Me.TopMost = True
        Me.PanelSettings.ResumeLayout(False)
        Me.PanelSettings.PerformLayout()
        Me.lowerPanel.ResumeLayout(False)
        Me.headerPanel.ResumeLayout(False)
        Me.headerPanel.PerformLayout()
        CType(Me.B2SLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPlugins.ResumeLayout(False)
        Me.grpPlugins.PerformLayout()
        Me.grpStartMode.ResumeLayout(False)
        Me.grpStartMode.PerformLayout()
        Me.grpGeneral.ResumeLayout(False)
        Me.grpGeneral.PerformLayout()
        Me.grpAnimationSettings.ResumeLayout(False)
        Me.grpScreenshot.ResumeLayout(False)
        Me.grpScreenshot.PerformLayout()
        Me.grpVisibility.ResumeLayout(False)
        Me.grpVisibility.PerformLayout()
        Me.grpLEDs.ResumeLayout(False)
        Me.grpPerfTuning.ResumeLayout(False)
        Me.grpPerfTuning.PerformLayout()
        CType(Me.numLampsSkipFrames, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numSolenoidsSkipFrames, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numLEDSkipFrames, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numGISkipFrames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TimerOpacity As System.Windows.Forms.Timer
    Friend WithEvents PanelSettings As System.Windows.Forms.Panel
    Friend WithEvents grpAnimationSettings As System.Windows.Forms.GroupBox
    Friend WithEvents cmbAnimationSetting As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAnimations As System.Windows.Forms.ComboBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents grpScreenshot As System.Windows.Forms.GroupBox
    Friend WithEvents lblFileType As System.Windows.Forms.Label
    Friend WithEvents cmbScreenshotType As System.Windows.Forms.ComboBox
    Friend WithEvents btnScreenshotPath As System.Windows.Forms.Button
    Friend WithEvents btnCloseSettings As System.Windows.Forms.Button
    Friend WithEvents B2SLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents grpVisibility As System.Windows.Forms.GroupBox
    Friend WithEvents cmbB2SDMD As System.Windows.Forms.ComboBox
    Friend WithEvents lblB2SDMD As System.Windows.Forms.Label
    Friend WithEvents cmbDMD As System.Windows.Forms.ComboBox
    Friend WithEvents lblDMD As System.Windows.Forms.Label
    Friend WithEvents cmbGrill As System.Windows.Forms.ComboBox
    Friend WithEvents lblGrill As System.Windows.Forms.Label
    Friend WithEvents cmbMatchingFileNames As System.Windows.Forms.ComboBox
    Friend WithEvents btnCheck As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents grpLEDs As System.Windows.Forms.GroupBox
    Friend WithEvents lblGlowing As System.Windows.Forms.Label
    Friend WithEvents radioStandardLED As System.Windows.Forms.RadioButton
    Friend WithEvents cmbGlowing As System.Windows.Forms.ComboBox
    Friend WithEvents radioDream7LED As System.Windows.Forms.RadioButton
    Friend WithEvents chkWireframe As System.Windows.Forms.CheckBox
    Friend WithEvents chkBulbs As System.Windows.Forms.CheckBox
    Friend WithEvents grpPerfTuning As System.Windows.Forms.GroupBox
    Friend WithEvents chkStartAsEXE As System.Windows.Forms.CheckBox
    Friend WithEvents numLampsSkipFrames As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblLampsBlackTurns As System.Windows.Forms.Label
    Friend WithEvents lblLEDBlackTurns As System.Windows.Forms.Label
    Friend WithEvents numSolenoidsSkipFrames As System.Windows.Forms.NumericUpDown
    Friend WithEvents numLEDSkipFrames As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSolenoidBlackTurns As System.Windows.Forms.Label
    Friend WithEvents numGISkipFrames As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblGIBlackTurns As System.Windows.Forms.Label
    Friend WithEvents grpGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents btnMore As System.Windows.Forms.Button
    Friend WithEvents cmbMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblMode As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents btnHyperpin As System.Windows.Forms.Button
    Friend WithEvents lblDefaultStartMode As System.Windows.Forms.Label
    Friend WithEvents cmbDefaultStartMode As System.Windows.Forms.ComboBox
    Friend WithEvents grpStartMode As System.Windows.Forms.GroupBox
    Friend WithEvents grpPlugins As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowStartupError As System.Windows.Forms.CheckBox
    Friend WithEvents chkActivatePlugins As System.Windows.Forms.CheckBox
    Friend WithEvents btnPluginSettings As System.Windows.Forms.Button
    Friend WithEvents lblFormFront As Windows.Forms.Label
    Friend WithEvents cmbFormFront As Windows.Forms.ComboBox
    Friend WithEvents chkDisableFuzzyMatching As Windows.Forms.CheckBox
    Friend WithEvents chkFormNoFocus As Windows.Forms.CheckBox
    Friend WithEvents btnEditScreenRes As Windows.Forms.Button
    Friend WithEvents lblBackground As Windows.Forms.Label
    Friend WithEvents cmbBackground As Windows.Forms.ComboBox
    Friend WithEvents lblNonAvailableSettings As Windows.Forms.Label
    Friend WithEvents B2SLogoToolTip As Windows.Forms.ToolTip
    Friend WithEvents lowerPanel As Windows.Forms.Panel
    Friend WithEvents headerPanel As Windows.Forms.Panel
    Friend WithEvents cmbB2SBackglass As Windows.Forms.ComboBox
    Friend WithEvents lblB2SBackglass As Windows.Forms.Label
End Class
