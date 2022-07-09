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
        Me.grpPlugins = New System.Windows.Forms.GroupBox()
        Me.chkShowStartupError = New System.Windows.Forms.CheckBox()
        Me.chkActivatePlugins = New System.Windows.Forms.CheckBox()
        Me.btnPluginSettings = New System.Windows.Forms.Button()
        Me.grpStartMode = New System.Windows.Forms.GroupBox()
        Me.lblDefaultStartMode = New System.Windows.Forms.Label()
        Me.chkStartAsEXE = New System.Windows.Forms.CheckBox()
        Me.cmbDefaultStartMode = New System.Windows.Forms.ComboBox()
        Me.btnMore = New System.Windows.Forms.Button()
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
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.grpScreenshot = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbScreenshotType = New System.Windows.Forms.ComboBox()
        Me.btnScreenshotPath = New System.Windows.Forms.Button()
        Me.btnCloseSettings = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.grpVisibility = New System.Windows.Forms.GroupBox()
        Me.cmbB2SDMD = New System.Windows.Forms.ComboBox()
        Me.lblB2SDMD = New System.Windows.Forms.Label()
        Me.cmbDMD = New System.Windows.Forms.ComboBox()
        Me.lblDMD = New System.Windows.Forms.Label()
        Me.cmbGrill = New System.Windows.Forms.ComboBox()
        Me.lblGrill = New System.Windows.Forms.Label()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
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
        Me.btnDonate = New System.Windows.Forms.Button()
        Me.PanelSettings.SuspendLayout()
        Me.grpPlugins.SuspendLayout()
        Me.grpStartMode.SuspendLayout()
        Me.grpGeneral.SuspendLayout()
        Me.grpAnimationSettings.SuspendLayout()
        Me.grpScreenshot.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.PanelSettings.Controls.Add(Me.grpPlugins)
        Me.PanelSettings.Controls.Add(Me.grpStartMode)
        Me.PanelSettings.Controls.Add(Me.btnMore)
        Me.PanelSettings.Controls.Add(Me.grpGeneral)
        Me.PanelSettings.Controls.Add(Me.grpAnimationSettings)
        Me.PanelSettings.Controls.Add(Me.lblVersion)
        Me.PanelSettings.Controls.Add(Me.grpScreenshot)
        Me.PanelSettings.Controls.Add(Me.btnCloseSettings)
        Me.PanelSettings.Controls.Add(Me.PictureBox1)
        Me.PanelSettings.Controls.Add(Me.lblCopyright)
        Me.PanelSettings.Controls.Add(Me.grpVisibility)
        Me.PanelSettings.Controls.Add(Me.btnSaveSettings)
        Me.PanelSettings.Controls.Add(Me.grpLEDs)
        Me.PanelSettings.Controls.Add(Me.grpPerfTuning)
        Me.PanelSettings.Controls.Add(Me.btnDonate)
        Me.PanelSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSettings.Location = New System.Drawing.Point(0, 0)
        Me.PanelSettings.Name = "PanelSettings"
        Me.PanelSettings.Size = New System.Drawing.Size(504, 572)
        Me.PanelSettings.TabIndex = 2
        '
        'grpPlugins
        '
        Me.grpPlugins.Controls.Add(Me.chkShowStartupError)
        Me.grpPlugins.Controls.Add(Me.chkActivatePlugins)
        Me.grpPlugins.Controls.Add(Me.btnPluginSettings)
        Me.grpPlugins.Location = New System.Drawing.Point(15, 479)
        Me.grpPlugins.Name = "grpPlugins"
        Me.grpPlugins.Size = New System.Drawing.Size(475, 50)
        Me.grpPlugins.TabIndex = 36
        Me.grpPlugins.TabStop = False
        Me.grpPlugins.Text = "Plugins"
        '
        'chkShowStartupError
        '
        Me.chkShowStartupError.AutoSize = True
        Me.chkShowStartupError.Location = New System.Drawing.Point(118, 25)
        Me.chkShowStartupError.Name = "chkShowStartupError"
        Me.chkShowStartupError.Size = New System.Drawing.Size(183, 17)
        Me.chkShowStartupError.TabIndex = 21
        Me.chkShowStartupError.Text = "Error message without backglass"
        Me.chkShowStartupError.UseVisualStyleBackColor = True
        '
        'chkActivatePlugins
        '
        Me.chkActivatePlugins.AutoSize = True
        Me.chkActivatePlugins.Location = New System.Drawing.Point(8, 25)
        Me.chkActivatePlugins.Name = "chkActivatePlugins"
        Me.chkActivatePlugins.Size = New System.Drawing.Size(102, 17)
        Me.chkActivatePlugins.TabIndex = 20
        Me.chkActivatePlugins.Text = "Activate plugins"
        Me.chkActivatePlugins.UseVisualStyleBackColor = True
        '
        'btnPluginSettings
        '
        Me.btnPluginSettings.Enabled = False
        Me.btnPluginSettings.Location = New System.Drawing.Point(314, 20)
        Me.btnPluginSettings.Name = "btnPluginSettings"
        Me.btnPluginSettings.Size = New System.Drawing.Size(154, 24)
        Me.btnPluginSettings.TabIndex = 22
        Me.btnPluginSettings.Text = "Plugin settings"
        Me.btnPluginSettings.UseVisualStyleBackColor = True
        '
        'grpStartMode
        '
        Me.grpStartMode.Controls.Add(Me.lblDefaultStartMode)
        Me.grpStartMode.Controls.Add(Me.chkStartAsEXE)
        Me.grpStartMode.Controls.Add(Me.cmbDefaultStartMode)
        Me.grpStartMode.Location = New System.Drawing.Point(15, 161)
        Me.grpStartMode.Name = "grpStartMode"
        Me.grpStartMode.Size = New System.Drawing.Size(474, 48)
        Me.grpStartMode.TabIndex = 2
        Me.grpStartMode.TabStop = False
        Me.grpStartMode.Text = "Backglass start mode"
        '
        'lblDefaultStartMode
        '
        Me.lblDefaultStartMode.AutoSize = True
        Me.lblDefaultStartMode.Location = New System.Drawing.Point(262, 23)
        Me.lblDefaultStartMode.Name = "lblDefaultStartMode"
        Me.lblDefaultStartMode.Size = New System.Drawing.Size(101, 13)
        Me.lblDefaultStartMode.TabIndex = 24
        Me.lblDefaultStartMode.Text = "Default start mode:"
        '
        'chkStartAsEXE
        '
        Me.chkStartAsEXE.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkStartAsEXE.Location = New System.Drawing.Point(6, 17)
        Me.chkStartAsEXE.Name = "chkStartAsEXE"
        Me.chkStartAsEXE.Size = New System.Drawing.Size(228, 24)
        Me.chkStartAsEXE.TabIndex = 6
        Me.chkStartAsEXE.Text = "Start this backglass in EXE mode"
        Me.chkStartAsEXE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStartAsEXE.UseVisualStyleBackColor = True
        '
        'cmbDefaultStartMode
        '
        Me.cmbDefaultStartMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultStartMode.FormattingEnabled = True
        Me.cmbDefaultStartMode.Items.AddRange(New Object() {"Standard", "In EXE"})
        Me.cmbDefaultStartMode.Location = New System.Drawing.Point(369, 20)
        Me.cmbDefaultStartMode.Name = "cmbDefaultStartMode"
        Me.cmbDefaultStartMode.Size = New System.Drawing.Size(98, 21)
        Me.cmbDefaultStartMode.TabIndex = 7
        '
        'btnMore
        '
        Me.btnMore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMore.Location = New System.Drawing.Point(15, 541)
        Me.btnMore.Name = "btnMore"
        Me.btnMore.Size = New System.Drawing.Size(73, 24)
        Me.btnMore.TabIndex = 32
        Me.btnMore.Text = "More ..."
        Me.btnMore.UseVisualStyleBackColor = True
        '
        'grpGeneral
        '
        Me.grpGeneral.Controls.Add(Me.btnHyperpin)
        Me.grpGeneral.Controls.Add(Me.lblFile)
        Me.grpGeneral.Controls.Add(Me.cmbMode)
        Me.grpGeneral.Controls.Add(Me.lblMode)
        Me.grpGeneral.Controls.Add(Me.cmbMatchingFileNames)
        Me.grpGeneral.Controls.Add(Me.btnCheck)
        Me.grpGeneral.Location = New System.Drawing.Point(15, 52)
        Me.grpGeneral.Name = "grpGeneral"
        Me.grpGeneral.Size = New System.Drawing.Size(474, 51)
        Me.grpGeneral.TabIndex = 0
        Me.grpGeneral.TabStop = False
        Me.grpGeneral.Text = "General"
        '
        'btnHyperpin
        '
        Me.btnHyperpin.Location = New System.Drawing.Point(370, 18)
        Me.btnHyperpin.Name = "btnHyperpin"
        Me.btnHyperpin.Size = New System.Drawing.Size(98, 24)
        Me.btnHyperpin.TabIndex = 2
        Me.btnHyperpin.Text = "Locate VP.xml"
        Me.btnHyperpin.UseVisualStyleBackColor = True
        '
        'lblFile
        '
        Me.lblFile.AutoSize = True
        Me.lblFile.Location = New System.Drawing.Point(162, 23)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(27, 13)
        Me.lblFile.TabIndex = 14
        Me.lblFile.Text = "File:"
        Me.lblFile.Visible = False
        '
        'cmbMode
        '
        Me.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMode.FormattingEnabled = True
        Me.cmbMode.Items.AddRange(New Object() {"Authentic", "Fantasy"})
        Me.cmbMode.Location = New System.Drawing.Point(45, 20)
        Me.cmbMode.Name = "cmbMode"
        Me.cmbMode.Size = New System.Drawing.Size(248, 21)
        Me.cmbMode.TabIndex = 0
        '
        'lblMode
        '
        Me.lblMode.AutoSize = True
        Me.lblMode.Location = New System.Drawing.Point(4, 23)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(37, 13)
        Me.lblMode.TabIndex = 13
        Me.lblMode.Text = "Mode:"
        Me.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMatchingFileNames
        '
        Me.cmbMatchingFileNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMatchingFileNames.FormattingEnabled = True
        Me.cmbMatchingFileNames.Location = New System.Drawing.Point(195, 20)
        Me.cmbMatchingFileNames.Name = "cmbMatchingFileNames"
        Me.cmbMatchingFileNames.Size = New System.Drawing.Size(156, 21)
        Me.cmbMatchingFileNames.Sorted = True
        Me.cmbMatchingFileNames.TabIndex = 1
        Me.cmbMatchingFileNames.Visible = False
        '
        'btnCheck
        '
        Me.btnCheck.Location = New System.Drawing.Point(370, 18)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(98, 24)
        Me.btnCheck.TabIndex = 2
        Me.btnCheck.Text = "Check monitors"
        Me.btnCheck.UseVisualStyleBackColor = True
        Me.btnCheck.Visible = False
        '
        'grpAnimationSettings
        '
        Me.grpAnimationSettings.Controls.Add(Me.cmbAnimationSetting)
        Me.grpAnimationSettings.Controls.Add(Me.cmbAnimations)
        Me.grpAnimationSettings.Location = New System.Drawing.Point(15, 371)
        Me.grpAnimationSettings.Name = "grpAnimationSettings"
        Me.grpAnimationSettings.Size = New System.Drawing.Size(474, 51)
        Me.grpAnimationSettings.TabIndex = 5
        Me.grpAnimationSettings.TabStop = False
        Me.grpAnimationSettings.Text = "Animation settings and tuning"
        '
        'cmbAnimationSetting
        '
        Me.cmbAnimationSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnimationSetting.FormattingEnabled = True
        Me.cmbAnimationSetting.Items.AddRange(New Object() {"1 x", "2 x", "3 x", "4 x", "5 x", "10 x", "Off"})
        Me.cmbAnimationSetting.Location = New System.Drawing.Point(239, 20)
        Me.cmbAnimationSetting.Name = "cmbAnimationSetting"
        Me.cmbAnimationSetting.Size = New System.Drawing.Size(111, 21)
        Me.cmbAnimationSetting.TabIndex = 17
        '
        'cmbAnimations
        '
        Me.cmbAnimations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnimations.FormattingEnabled = True
        Me.cmbAnimations.Location = New System.Drawing.Point(7, 20)
        Me.cmbAnimations.Name = "cmbAnimations"
        Me.cmbAnimations.Size = New System.Drawing.Size(226, 21)
        Me.cmbAnimations.TabIndex = 16
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(54, 30)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(265, 13)
        Me.lblVersion.TabIndex = 27
        Me.lblVersion.Text = "Server version {0}, Backglass file version {1}"
        '
        'grpScreenshot
        '
        Me.grpScreenshot.Controls.Add(Me.Label1)
        Me.grpScreenshot.Controls.Add(Me.cmbScreenshotType)
        Me.grpScreenshot.Controls.Add(Me.btnScreenshotPath)
        Me.grpScreenshot.Location = New System.Drawing.Point(15, 425)
        Me.grpScreenshot.Name = "grpScreenshot"
        Me.grpScreenshot.Size = New System.Drawing.Size(475, 50)
        Me.grpScreenshot.TabIndex = 6
        Me.grpScreenshot.TabStop = False
        Me.grpScreenshot.Text = "Screenshot"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(268, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Image file type:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbScreenshotType
        '
        Me.cmbScreenshotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbScreenshotType.FormattingEnabled = True
        Me.cmbScreenshotType.Items.AddRange(New Object() {"PNG", "JPG", "GIF", "BMP"})
        Me.cmbScreenshotType.Location = New System.Drawing.Point(357, 18)
        Me.cmbScreenshotType.Name = "cmbScreenshotType"
        Me.cmbScreenshotType.Size = New System.Drawing.Size(111, 21)
        Me.cmbScreenshotType.TabIndex = 19
        '
        'btnScreenshotPath
        '
        Me.btnScreenshotPath.Location = New System.Drawing.Point(8, 18)
        Me.btnScreenshotPath.Name = "btnScreenshotPath"
        Me.btnScreenshotPath.Size = New System.Drawing.Size(226, 24)
        Me.btnScreenshotPath.TabIndex = 18
        Me.btnScreenshotPath.Text = "Screenshot path"
        Me.btnScreenshotPath.UseVisualStyleBackColor = True
        '
        'btnCloseSettings
        '
        Me.btnCloseSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseSettings.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCloseSettings.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseSettings.Location = New System.Drawing.Point(414, 541)
        Me.btnCloseSettings.Name = "btnCloseSettings"
        Me.btnCloseSettings.Size = New System.Drawing.Size(78, 24)
        Me.btnCloseSettings.TabIndex = 35
        Me.btnCloseSettings.Text = "Close"
        Me.btnCloseSettings.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(16, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(31, 32)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(54, 4)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(423, 26)
        Me.lblCopyright.TabIndex = 23
        Me.lblCopyright.Text = "B2S Backglass Server" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Copyright (c) 2012-2022 by Stefan Wuehr (""Herweh""). All rig" & _
    "hts reserved."
        '
        'grpVisibility
        '
        Me.grpVisibility.Controls.Add(Me.cmbB2SDMD)
        Me.grpVisibility.Controls.Add(Me.lblB2SDMD)
        Me.grpVisibility.Controls.Add(Me.cmbDMD)
        Me.grpVisibility.Controls.Add(Me.lblDMD)
        Me.grpVisibility.Controls.Add(Me.cmbGrill)
        Me.grpVisibility.Controls.Add(Me.lblGrill)
        Me.grpVisibility.Location = New System.Drawing.Point(15, 106)
        Me.grpVisibility.Name = "grpVisibility"
        Me.grpVisibility.Size = New System.Drawing.Size(474, 53)
        Me.grpVisibility.TabIndex = 1
        Me.grpVisibility.TabStop = False
        Me.grpVisibility.Text = "Visibility"
        '
        'cmbB2SDMD
        '
        Me.cmbB2SDMD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbB2SDMD.FormattingEnabled = True
        Me.cmbB2SDMD.Items.AddRange(New Object() {"Visible", "Hidden"})
        Me.cmbB2SDMD.Location = New System.Drawing.Point(370, 21)
        Me.cmbB2SDMD.Name = "cmbB2SDMD"
        Me.cmbB2SDMD.Size = New System.Drawing.Size(98, 21)
        Me.cmbB2SDMD.TabIndex = 5
        '
        'lblB2SDMD
        '
        Me.lblB2SDMD.AutoSize = True
        Me.lblB2SDMD.Location = New System.Drawing.Point(310, 24)
        Me.lblB2SDMD.Name = "lblB2SDMD"
        Me.lblB2SDMD.Size = New System.Drawing.Size(54, 13)
        Me.lblB2SDMD.TabIndex = 15
        Me.lblB2SDMD.Text = "B2S DMD:"
        '
        'cmbDMD
        '
        Me.cmbDMD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDMD.FormattingEnabled = True
        Me.cmbDMD.Items.AddRange(New Object() {"Visible", "Hidden", "Standard"})
        Me.cmbDMD.Location = New System.Drawing.Point(195, 21)
        Me.cmbDMD.Name = "cmbDMD"
        Me.cmbDMD.Size = New System.Drawing.Size(98, 21)
        Me.cmbDMD.TabIndex = 4
        '
        'lblDMD
        '
        Me.lblDMD.AutoSize = True
        Me.lblDMD.Location = New System.Drawing.Point(156, 24)
        Me.lblDMD.Name = "lblDMD"
        Me.lblDMD.Size = New System.Drawing.Size(33, 13)
        Me.lblDMD.TabIndex = 13
        Me.lblDMD.Text = "DMD:"
        '
        'cmbGrill
        '
        Me.cmbGrill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGrill.FormattingEnabled = True
        Me.cmbGrill.Items.AddRange(New Object() {"Visible", "Hidden", "Standard"})
        Me.cmbGrill.Location = New System.Drawing.Point(46, 21)
        Me.cmbGrill.Name = "cmbGrill"
        Me.cmbGrill.Size = New System.Drawing.Size(98, 21)
        Me.cmbGrill.TabIndex = 3
        '
        'lblGrill
        '
        Me.lblGrill.AutoSize = True
        Me.lblGrill.Location = New System.Drawing.Point(13, 24)
        Me.lblGrill.Name = "lblGrill"
        Me.lblGrill.Size = New System.Drawing.Size(28, 13)
        Me.lblGrill.TabIndex = 11
        Me.lblGrill.Text = "Grill:"
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSettings.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveSettings.Location = New System.Drawing.Point(96, 541)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(311, 24)
        Me.btnSaveSettings.TabIndex = 34
        Me.btnSaveSettings.Text = "Save settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'grpLEDs
        '
        Me.grpLEDs.Controls.Add(Me.lblGlowing)
        Me.grpLEDs.Controls.Add(Me.radioStandardLED)
        Me.grpLEDs.Controls.Add(Me.cmbGlowing)
        Me.grpLEDs.Controls.Add(Me.radioDream7LED)
        Me.grpLEDs.Controls.Add(Me.chkWireframe)
        Me.grpLEDs.Controls.Add(Me.chkBulbs)
        Me.grpLEDs.Location = New System.Drawing.Point(15, 288)
        Me.grpLEDs.Name = "grpLEDs"
        Me.grpLEDs.Size = New System.Drawing.Size(474, 80)
        Me.grpLEDs.TabIndex = 4
        Me.grpLEDs.TabStop = False
        Me.grpLEDs.Text = "LED settings and tuning"
        '
        'lblGlowing
        '
        Me.lblGlowing.Location = New System.Drawing.Point(303, 53)
        Me.lblGlowing.Name = "lblGlowing"
        Me.lblGlowing.Size = New System.Drawing.Size(48, 13)
        Me.lblGlowing.TabIndex = 25
        Me.lblGlowing.Text = "Glowing:"
        Me.lblGlowing.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'radioStandardLED
        '
        Me.radioStandardLED.Appearance = System.Windows.Forms.Appearance.Button
        Me.radioStandardLED.Location = New System.Drawing.Point(6, 20)
        Me.radioStandardLED.Name = "radioStandardLED"
        Me.radioStandardLED.Size = New System.Drawing.Size(111, 24)
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
        Me.cmbGlowing.Location = New System.Drawing.Point(357, 50)
        Me.cmbGlowing.Name = "cmbGlowing"
        Me.cmbGlowing.Size = New System.Drawing.Size(111, 21)
        Me.cmbGlowing.TabIndex = 15
        '
        'radioDream7LED
        '
        Me.radioDream7LED.Appearance = System.Windows.Forms.Appearance.Button
        Me.radioDream7LED.Location = New System.Drawing.Point(124, 20)
        Me.radioDream7LED.Name = "radioDream7LED"
        Me.radioDream7LED.Size = New System.Drawing.Size(110, 24)
        Me.radioDream7LED.TabIndex = 12
        Me.radioDream7LED.TabStop = True
        Me.radioDream7LED.Text = "Use 'Dream7' LEDs"
        Me.radioDream7LED.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.radioDream7LED.UseVisualStyleBackColor = True
        '
        'chkWireframe
        '
        Me.chkWireframe.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkWireframe.Location = New System.Drawing.Point(240, 20)
        Me.chkWireframe.Name = "chkWireframe"
        Me.chkWireframe.Size = New System.Drawing.Size(111, 24)
        Me.chkWireframe.TabIndex = 13
        Me.chkWireframe.Text = "LED &wireframe on"
        Me.chkWireframe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkWireframe.UseVisualStyleBackColor = True
        '
        'chkBulbs
        '
        Me.chkBulbs.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkBulbs.Location = New System.Drawing.Point(357, 20)
        Me.chkBulbs.Name = "chkBulbs"
        Me.chkBulbs.Size = New System.Drawing.Size(111, 24)
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
        Me.grpPerfTuning.Location = New System.Drawing.Point(15, 211)
        Me.grpPerfTuning.Name = "grpPerfTuning"
        Me.grpPerfTuning.Size = New System.Drawing.Size(474, 75)
        Me.grpPerfTuning.TabIndex = 3
        Me.grpPerfTuning.TabStop = False
        Me.grpPerfTuning.Text = "Performance tuning"
        '
        'numLampsSkipFrames
        '
        Me.numLampsSkipFrames.Location = New System.Drawing.Point(118, 20)
        Me.numLampsSkipFrames.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numLampsSkipFrames.Name = "numLampsSkipFrames"
        Me.numLampsSkipFrames.Size = New System.Drawing.Size(51, 21)
        Me.numLampsSkipFrames.TabIndex = 8
        '
        'lblLampsBlackTurns
        '
        Me.lblLampsBlackTurns.AutoSize = True
        Me.lblLampsBlackTurns.Location = New System.Drawing.Point(16, 22)
        Me.lblLampsBlackTurns.Name = "lblLampsBlackTurns"
        Me.lblLampsBlackTurns.Size = New System.Drawing.Size(91, 13)
        Me.lblLampsBlackTurns.TabIndex = 10
        Me.lblLampsBlackTurns.Text = "Skip lamp frames:"
        '
        'lblLEDBlackTurns
        '
        Me.lblLEDBlackTurns.AutoSize = True
        Me.lblLEDBlackTurns.Location = New System.Drawing.Point(218, 48)
        Me.lblLEDBlackTurns.Name = "lblLEDBlackTurns"
        Me.lblLEDBlackTurns.Size = New System.Drawing.Size(87, 13)
        Me.lblLEDBlackTurns.TabIndex = 22
        Me.lblLEDBlackTurns.Text = "Skip LED frames:"
        '
        'numSolenoidsSkipFrames
        '
        Me.numSolenoidsSkipFrames.Location = New System.Drawing.Point(314, 20)
        Me.numSolenoidsSkipFrames.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.numSolenoidsSkipFrames.Name = "numSolenoidsSkipFrames"
        Me.numSolenoidsSkipFrames.Size = New System.Drawing.Size(51, 21)
        Me.numSolenoidsSkipFrames.TabIndex = 9
        '
        'numLEDSkipFrames
        '
        Me.numLEDSkipFrames.Location = New System.Drawing.Point(314, 46)
        Me.numLEDSkipFrames.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numLEDSkipFrames.Name = "numLEDSkipFrames"
        Me.numLEDSkipFrames.Size = New System.Drawing.Size(51, 21)
        Me.numLEDSkipFrames.TabIndex = 11
        '
        'lblSolenoidBlackTurns
        '
        Me.lblSolenoidBlackTurns.AutoSize = True
        Me.lblSolenoidBlackTurns.Location = New System.Drawing.Point(197, 22)
        Me.lblSolenoidBlackTurns.Name = "lblSolenoidBlackTurns"
        Me.lblSolenoidBlackTurns.Size = New System.Drawing.Size(108, 13)
        Me.lblSolenoidBlackTurns.TabIndex = 12
        Me.lblSolenoidBlackTurns.Text = "Skip solenoid frames:"
        '
        'numGISkipFrames
        '
        Me.numGISkipFrames.Location = New System.Drawing.Point(118, 46)
        Me.numGISkipFrames.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.numGISkipFrames.Name = "numGISkipFrames"
        Me.numGISkipFrames.Size = New System.Drawing.Size(51, 21)
        Me.numGISkipFrames.TabIndex = 10
        '
        'lblGIBlackTurns
        '
        Me.lblGIBlackTurns.AutoSize = True
        Me.lblGIBlackTurns.Location = New System.Drawing.Point(27, 48)
        Me.lblGIBlackTurns.Name = "lblGIBlackTurns"
        Me.lblGIBlackTurns.Size = New System.Drawing.Size(80, 13)
        Me.lblGIBlackTurns.TabIndex = 19
        Me.lblGIBlackTurns.Text = "Skip GI frames:"
        '
        'btnDonate
        '
        Me.btnDonate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDonate.Image = CType(resources.GetObject("btnDonate.Image"), System.Drawing.Image)
        Me.btnDonate.Location = New System.Drawing.Point(57, 49)
        Me.btnDonate.Name = "btnDonate"
        Me.btnDonate.Size = New System.Drawing.Size(435, 31)
        Me.btnDonate.TabIndex = 35
        Me.btnDonate.Text = "   If you want to donate, click here for infos ..."
        Me.btnDonate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDonate.UseVisualStyleBackColor = True
        Me.btnDonate.Visible = False
        '
        'formSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 572)
        Me.Controls.Add(Me.PanelSettings)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formSettings"
        Me.Opacity = 0.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backglass settings ..."
        Me.PanelSettings.ResumeLayout(False)
        Me.PanelSettings.PerformLayout()
        Me.grpPlugins.ResumeLayout(False)
        Me.grpPlugins.PerformLayout()
        Me.grpStartMode.ResumeLayout(False)
        Me.grpStartMode.PerformLayout()
        Me.grpGeneral.ResumeLayout(False)
        Me.grpGeneral.PerformLayout()
        Me.grpAnimationSettings.ResumeLayout(False)
        Me.grpScreenshot.ResumeLayout(False)
        Me.grpScreenshot.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbScreenshotType As System.Windows.Forms.ComboBox
    Friend WithEvents btnScreenshotPath As System.Windows.Forms.Button
    Friend WithEvents btnCloseSettings As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
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
    Friend WithEvents btnDonate As System.Windows.Forms.Button
    Friend WithEvents grpPlugins As System.Windows.Forms.GroupBox
    Friend WithEvents chkActivatePlugins As System.Windows.Forms.CheckBox
    Friend WithEvents btnPluginSettings As System.Windows.Forms.Button
    Friend WithEvents chkShowStartupError As System.Windows.Forms.CheckBox
End Class
