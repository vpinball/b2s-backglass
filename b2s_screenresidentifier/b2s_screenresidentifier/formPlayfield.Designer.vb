﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formPlayfield
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formPlayfield))
        Me.buttonSave = New System.Windows.Forms.Button()
        Me.radio1Screen = New System.Windows.Forms.RadioButton()
        Me.radio2Screen = New System.Windows.Forms.RadioButton()
        Me.radio3Screen = New System.Windows.Forms.RadioButton()
        Me.buttonBringMeTheOtherWindows = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.groupPlayfield = New System.Windows.Forms.GroupBox()
        Me.lblPlayfieldScreenScale = New System.Windows.Forms.Label()
        Me.txtPlayfieldScreenScale = New System.Windows.Forms.TextBox()
        Me.lblPlayfieldLocationY = New System.Windows.Forms.Label()
        Me.txtPlayfieldLocationY = New System.Windows.Forms.TextBox()
        Me.txtPlayfieldLocationX = New System.Windows.Forms.TextBox()
        Me.lblPlayfieldLocationX = New System.Windows.Forms.Label()
        Me.lblPlayfieldScreenSizeHeight = New System.Windows.Forms.Label()
        Me.txtPlayfieldScreenSizeHeight = New System.Windows.Forms.TextBox()
        Me.txtPlayfieldScreenSizeWidth = New System.Windows.Forms.TextBox()
        Me.lblPlayfieldScreenSizeWidth = New System.Windows.Forms.Label()
        Me.chkPlayfieldFullSize = New System.Windows.Forms.CheckBox()
        Me.lblPlayfieldSizeHeight = New System.Windows.Forms.Label()
        Me.txtPlayfieldSizeHeight = New System.Windows.Forms.TextBox()
        Me.txtPlayfieldSizeWidth = New System.Windows.Forms.TextBox()
        Me.lblPlayfieldSize = New System.Windows.Forms.Label()
        Me.txtPlayfieldScreen = New System.Windows.Forms.TextBox()
        Me.lblPlayfieldScreen = New System.Windows.Forms.Label()
        Me.lblPlayfield = New System.Windows.Forms.Label()
        Me.panelChooseSetup = New System.Windows.Forms.Panel()
        Me.lblChooseSetup = New System.Windows.Forms.Label()
        Me.lblInfo2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.buttonSaveGlobal = New System.Windows.Forms.Button()
        Me.ResFileLabel = New System.Windows.Forms.TextBox()
        Me.chkSaveComments = New System.Windows.Forms.CheckBox()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.chkSaveEnhanced = New System.Windows.Forms.CheckBox()
        Me.groupPlayfield.SuspendLayout()
        Me.panelChooseSetup.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'buttonSave
        '
        Me.buttonSave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonSave.Location = New System.Drawing.Point(3, 3)
        Me.buttonSave.MaximumSize = New System.Drawing.Size(146, 43)
        Me.buttonSave.MinimumSize = New System.Drawing.Size(120, 43)
        Me.buttonSave.Name = "buttonSave"
        Me.buttonSave.Size = New System.Drawing.Size(120, 43)
        Me.buttonSave.TabIndex = 3
        Me.buttonSave.Text = "&Save settings"
        Me.buttonSave.UseVisualStyleBackColor = True
        '
        'radio1Screen
        '
        Me.radio1Screen.AutoSize = True
        Me.radio1Screen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radio1Screen.Location = New System.Drawing.Point(261, 15)
        Me.radio1Screen.Name = "radio1Screen"
        Me.radio1Screen.Size = New System.Drawing.Size(146, 20)
        Me.radio1Screen.TabIndex = 0
        Me.radio1Screen.TabStop = True
        Me.radio1Screen.Text = "... 1-screen setup"""
        Me.radio1Screen.UseVisualStyleBackColor = True
        '
        'radio2Screen
        '
        Me.radio2Screen.AutoSize = True
        Me.radio2Screen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radio2Screen.Location = New System.Drawing.Point(421, 15)
        Me.radio2Screen.Name = "radio2Screen"
        Me.radio2Screen.Size = New System.Drawing.Size(146, 20)
        Me.radio2Screen.TabIndex = 1
        Me.radio2Screen.TabStop = True
        Me.radio2Screen.Text = "... 2-screen setup"""
        Me.radio2Screen.UseVisualStyleBackColor = True
        '
        'radio3Screen
        '
        Me.radio3Screen.AutoSize = True
        Me.radio3Screen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radio3Screen.Location = New System.Drawing.Point(581, 15)
        Me.radio3Screen.Name = "radio3Screen"
        Me.radio3Screen.Size = New System.Drawing.Size(146, 20)
        Me.radio3Screen.TabIndex = 2
        Me.radio3Screen.TabStop = True
        Me.radio3Screen.Text = "... 3-screen setup"""
        Me.radio3Screen.UseVisualStyleBackColor = True
        '
        'buttonBringMeTheOtherWindows
        '
        Me.buttonBringMeTheOtherWindows.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonBringMeTheOtherWindows.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonBringMeTheOtherWindows.Location = New System.Drawing.Point(897, 5)
        Me.buttonBringMeTheOtherWindows.Name = "buttonBringMeTheOtherWindows"
        Me.buttonBringMeTheOtherWindows.Size = New System.Drawing.Size(188, 43)
        Me.buttonBringMeTheOtherWindows.TabIndex = 4
        Me.buttonBringMeTheOtherWindows.Text = "Where are the other windows?"
        Me.buttonBringMeTheOtherWindows.UseVisualStyleBackColor = True
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.Location = New System.Drawing.Point(15, 15)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(1087, 144)
        Me.lblInfo.TabIndex = 4
        Me.lblInfo.Text = resources.GetString("lblInfo.Text")
        '
        'groupPlayfield
        '
        Me.groupPlayfield.Controls.Add(Me.lblPlayfieldScreenScale)
        Me.groupPlayfield.Controls.Add(Me.txtPlayfieldScreenScale)
        Me.groupPlayfield.Controls.Add(Me.lblPlayfieldLocationY)
        Me.groupPlayfield.Controls.Add(Me.txtPlayfieldLocationY)
        Me.groupPlayfield.Controls.Add(Me.txtPlayfieldLocationX)
        Me.groupPlayfield.Controls.Add(Me.lblPlayfieldLocationX)
        Me.groupPlayfield.Controls.Add(Me.lblPlayfieldScreenSizeHeight)
        Me.groupPlayfield.Controls.Add(Me.txtPlayfieldScreenSizeHeight)
        Me.groupPlayfield.Controls.Add(Me.txtPlayfieldScreenSizeWidth)
        Me.groupPlayfield.Controls.Add(Me.lblPlayfieldScreenSizeWidth)
        Me.groupPlayfield.Controls.Add(Me.chkPlayfieldFullSize)
        Me.groupPlayfield.Controls.Add(Me.lblPlayfieldSizeHeight)
        Me.groupPlayfield.Controls.Add(Me.txtPlayfieldSizeHeight)
        Me.groupPlayfield.Controls.Add(Me.txtPlayfieldSizeWidth)
        Me.groupPlayfield.Controls.Add(Me.lblPlayfieldSize)
        Me.groupPlayfield.Controls.Add(Me.txtPlayfieldScreen)
        Me.groupPlayfield.Controls.Add(Me.lblPlayfieldScreen)
        Me.groupPlayfield.Controls.Add(Me.lblPlayfield)
        Me.groupPlayfield.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupPlayfield.Location = New System.Drawing.Point(15, 269)
        Me.groupPlayfield.Name = "groupPlayfield"
        Me.groupPlayfield.Size = New System.Drawing.Size(1073, 195)
        Me.groupPlayfield.TabIndex = 5
        Me.groupPlayfield.TabStop = False
        Me.groupPlayfield.Text = "Playfield"
        '
        'lblPlayfieldScreenScale
        '
        Me.lblPlayfieldScreenScale.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfieldScreenScale.Location = New System.Drawing.Point(386, 79)
        Me.lblPlayfieldScreenScale.Name = "lblPlayfieldScreenScale"
        Me.lblPlayfieldScreenScale.Size = New System.Drawing.Size(231, 20)
        Me.lblPlayfieldScreenScale.TabIndex = 26
        Me.lblPlayfieldScreenScale.Text = "dpi scaling, shows red if not 100%"
        Me.lblPlayfieldScreenScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlayfieldScreenScale
        '
        Me.txtPlayfieldScreenScale.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayfieldScreenScale.Location = New System.Drawing.Point(287, 79)
        Me.txtPlayfieldScreenScale.Name = "txtPlayfieldScreenScale"
        Me.txtPlayfieldScreenScale.ReadOnly = True
        Me.txtPlayfieldScreenScale.Size = New System.Drawing.Size(93, 23)
        Me.txtPlayfieldScreenScale.TabIndex = 25
        '
        'lblPlayfieldLocationY
        '
        Me.lblPlayfieldLocationY.AutoSize = True
        Me.lblPlayfieldLocationY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfieldLocationY.Location = New System.Drawing.Point(210, 112)
        Me.lblPlayfieldLocationY.Name = "lblPlayfieldLocationY"
        Me.lblPlayfieldLocationY.Size = New System.Drawing.Size(11, 16)
        Me.lblPlayfieldLocationY.TabIndex = 22
        Me.lblPlayfieldLocationY.Text = ","
        Me.lblPlayfieldLocationY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlayfieldLocationY
        '
        Me.txtPlayfieldLocationY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayfieldLocationY.Location = New System.Drawing.Point(227, 108)
        Me.txtPlayfieldLocationY.Name = "txtPlayfieldLocationY"
        Me.txtPlayfieldLocationY.ReadOnly = True
        Me.txtPlayfieldLocationY.Size = New System.Drawing.Size(54, 23)
        Me.txtPlayfieldLocationY.TabIndex = 10
        '
        'txtPlayfieldLocationX
        '
        Me.txtPlayfieldLocationX.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayfieldLocationX.Location = New System.Drawing.Point(152, 108)
        Me.txtPlayfieldLocationX.Name = "txtPlayfieldLocationX"
        Me.txtPlayfieldLocationX.ReadOnly = True
        Me.txtPlayfieldLocationX.Size = New System.Drawing.Size(54, 23)
        Me.txtPlayfieldLocationX.TabIndex = 9
        '
        'lblPlayfieldLocationX
        '
        Me.lblPlayfieldLocationX.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfieldLocationX.Location = New System.Drawing.Point(12, 110)
        Me.lblPlayfieldLocationX.Name = "lblPlayfieldLocationX"
        Me.lblPlayfieldLocationX.Size = New System.Drawing.Size(134, 20)
        Me.lblPlayfieldLocationX.TabIndex = 19
        Me.lblPlayfieldLocationX.Text = "Playfield location:"
        Me.lblPlayfieldLocationX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPlayfieldScreenSizeHeight
        '
        Me.lblPlayfieldScreenSizeHeight.AutoSize = True
        Me.lblPlayfieldScreenSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfieldScreenSizeHeight.Location = New System.Drawing.Point(210, 83)
        Me.lblPlayfieldScreenSizeHeight.Name = "lblPlayfieldScreenSizeHeight"
        Me.lblPlayfieldScreenSizeHeight.Size = New System.Drawing.Size(11, 16)
        Me.lblPlayfieldScreenSizeHeight.TabIndex = 18
        Me.lblPlayfieldScreenSizeHeight.Text = ","
        Me.lblPlayfieldScreenSizeHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlayfieldScreenSizeHeight
        '
        Me.txtPlayfieldScreenSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayfieldScreenSizeHeight.Location = New System.Drawing.Point(227, 79)
        Me.txtPlayfieldScreenSizeHeight.Name = "txtPlayfieldScreenSizeHeight"
        Me.txtPlayfieldScreenSizeHeight.ReadOnly = True
        Me.txtPlayfieldScreenSizeHeight.Size = New System.Drawing.Size(54, 23)
        Me.txtPlayfieldScreenSizeHeight.TabIndex = 8
        '
        'txtPlayfieldScreenSizeWidth
        '
        Me.txtPlayfieldScreenSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayfieldScreenSizeWidth.Location = New System.Drawing.Point(152, 79)
        Me.txtPlayfieldScreenSizeWidth.Name = "txtPlayfieldScreenSizeWidth"
        Me.txtPlayfieldScreenSizeWidth.ReadOnly = True
        Me.txtPlayfieldScreenSizeWidth.Size = New System.Drawing.Size(54, 23)
        Me.txtPlayfieldScreenSizeWidth.TabIndex = 7
        '
        'lblPlayfieldScreenSizeWidth
        '
        Me.lblPlayfieldScreenSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfieldScreenSizeWidth.Location = New System.Drawing.Point(12, 81)
        Me.lblPlayfieldScreenSizeWidth.Name = "lblPlayfieldScreenSizeWidth"
        Me.lblPlayfieldScreenSizeWidth.Size = New System.Drawing.Size(134, 20)
        Me.lblPlayfieldScreenSizeWidth.TabIndex = 15
        Me.lblPlayfieldScreenSizeWidth.Text = "Current screen size:"
        Me.lblPlayfieldScreenSizeWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkPlayfieldFullSize
        '
        Me.chkPlayfieldFullSize.AutoSize = True
        Me.chkPlayfieldFullSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlayfieldFullSize.Location = New System.Drawing.Point(152, 166)
        Me.chkPlayfieldFullSize.Name = "chkPlayfieldFullSize"
        Me.chkPlayfieldFullSize.Size = New System.Drawing.Size(150, 20)
        Me.chkPlayfieldFullSize.TabIndex = 13
        Me.chkPlayfieldFullSize.Text = "Playfield is fullsized"
        Me.chkPlayfieldFullSize.UseVisualStyleBackColor = True
        '
        'lblPlayfieldSizeHeight
        '
        Me.lblPlayfieldSizeHeight.AutoSize = True
        Me.lblPlayfieldSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfieldSizeHeight.Location = New System.Drawing.Point(210, 141)
        Me.lblPlayfieldSizeHeight.Name = "lblPlayfieldSizeHeight"
        Me.lblPlayfieldSizeHeight.Size = New System.Drawing.Size(11, 16)
        Me.lblPlayfieldSizeHeight.TabIndex = 13
        Me.lblPlayfieldSizeHeight.Text = ","
        Me.lblPlayfieldSizeHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlayfieldSizeHeight
        '
        Me.txtPlayfieldSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayfieldSizeHeight.Location = New System.Drawing.Point(227, 137)
        Me.txtPlayfieldSizeHeight.Name = "txtPlayfieldSizeHeight"
        Me.txtPlayfieldSizeHeight.ReadOnly = True
        Me.txtPlayfieldSizeHeight.Size = New System.Drawing.Size(54, 23)
        Me.txtPlayfieldSizeHeight.TabIndex = 12
        '
        'txtPlayfieldSizeWidth
        '
        Me.txtPlayfieldSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayfieldSizeWidth.Location = New System.Drawing.Point(152, 137)
        Me.txtPlayfieldSizeWidth.Name = "txtPlayfieldSizeWidth"
        Me.txtPlayfieldSizeWidth.ReadOnly = True
        Me.txtPlayfieldSizeWidth.Size = New System.Drawing.Size(54, 23)
        Me.txtPlayfieldSizeWidth.TabIndex = 11
        '
        'lblPlayfieldSize
        '
        Me.lblPlayfieldSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfieldSize.Location = New System.Drawing.Point(12, 139)
        Me.lblPlayfieldSize.Name = "lblPlayfieldSize"
        Me.lblPlayfieldSize.Size = New System.Drawing.Size(134, 20)
        Me.lblPlayfieldSize.TabIndex = 10
        Me.lblPlayfieldSize.Text = "Playfield size:"
        Me.lblPlayfieldSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlayfieldScreen
        '
        Me.txtPlayfieldScreen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayfieldScreen.Location = New System.Drawing.Point(152, 50)
        Me.txtPlayfieldScreen.Name = "txtPlayfieldScreen"
        Me.txtPlayfieldScreen.ReadOnly = True
        Me.txtPlayfieldScreen.Size = New System.Drawing.Size(217, 23)
        Me.txtPlayfieldScreen.TabIndex = 6
        '
        'lblPlayfieldScreen
        '
        Me.lblPlayfieldScreen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfieldScreen.Location = New System.Drawing.Point(12, 52)
        Me.lblPlayfieldScreen.Name = "lblPlayfieldScreen"
        Me.lblPlayfieldScreen.Size = New System.Drawing.Size(134, 20)
        Me.lblPlayfieldScreen.TabIndex = 8
        Me.lblPlayfieldScreen.Text = "Current screen:"
        Me.lblPlayfieldScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPlayfield
        '
        Me.lblPlayfield.AutoSize = True
        Me.lblPlayfield.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayfield.Location = New System.Drawing.Point(6, 23)
        Me.lblPlayfield.Name = "lblPlayfield"
        Me.lblPlayfield.Size = New System.Drawing.Size(833, 16)
        Me.lblPlayfield.TabIndex = 5
        Me.lblPlayfield.Text = "This is the dummy window for the playfield. The playfield has to be on your prima" &
    "ry screen, 'DISPLAY 1', and has to be fullsized."
        '
        'panelChooseSetup
        '
        Me.panelChooseSetup.Controls.Add(Me.lblChooseSetup)
        Me.panelChooseSetup.Controls.Add(Me.radio1Screen)
        Me.panelChooseSetup.Controls.Add(Me.radio2Screen)
        Me.panelChooseSetup.Controls.Add(Me.radio3Screen)
        Me.panelChooseSetup.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelChooseSetup.Location = New System.Drawing.Point(15, 159)
        Me.panelChooseSetup.Name = "panelChooseSetup"
        Me.panelChooseSetup.Size = New System.Drawing.Size(1088, 61)
        Me.panelChooseSetup.TabIndex = 6
        '
        'lblChooseSetup
        '
        Me.lblChooseSetup.AutoSize = True
        Me.lblChooseSetup.Font = New System.Drawing.Font("Tahoma", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChooseSetup.Location = New System.Drawing.Point(-1, 17)
        Me.lblChooseSetup.Name = "lblChooseSetup"
        Me.lblChooseSetup.Size = New System.Drawing.Size(235, 16)
        Me.lblChooseSetup.TabIndex = 1
        Me.lblChooseSetup.Text = """I want to setup my system as a ..."
        '
        'lblInfo2
        '
        Me.lblInfo2.AutoSize = True
        Me.lblInfo2.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInfo2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo2.Location = New System.Drawing.Point(15, 220)
        Me.lblInfo2.Name = "lblInfo2"
        Me.lblInfo2.Size = New System.Drawing.Size(916, 32)
        Me.lblInfo2.TabIndex = 7
        Me.lblInfo2.Text = resources.GetString("lblInfo2.Text")
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkSaveEnhanced)
        Me.Panel2.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel2.Controls.Add(Me.chkSaveComments)
        Me.Panel2.Controls.Add(Me.lblCopyright)
        Me.Panel2.Controls.Add(Me.buttonBringMeTheOtherWindows)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(15, 483)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1088, 87)
        Me.Panel2.TabIndex = 8
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.buttonSave)
        Me.FlowLayoutPanel1.Controls.Add(Me.buttonSaveGlobal)
        Me.FlowLayoutPanel1.Controls.Add(Me.ResFileLabel)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(874, 52)
        Me.FlowLayoutPanel1.TabIndex = 9
        '
        'buttonSaveGlobal
        '
        Me.buttonSaveGlobal.Location = New System.Drawing.Point(129, 3)
        Me.buttonSaveGlobal.MaximumSize = New System.Drawing.Size(146, 43)
        Me.buttonSaveGlobal.MinimumSize = New System.Drawing.Size(120, 43)
        Me.buttonSaveGlobal.Name = "buttonSaveGlobal"
        Me.buttonSaveGlobal.Size = New System.Drawing.Size(120, 43)
        Me.buttonSaveGlobal.TabIndex = 8
        Me.buttonSaveGlobal.Text = "Save &global"
        Me.buttonSaveGlobal.UseVisualStyleBackColor = True
        '
        'ResFileLabel
        '
        Me.ResFileLabel.AccessibleDescription = "ScreenRes file"
        Me.ResFileLabel.AccessibleName = "Filename"
        Me.ResFileLabel.Dock = System.Windows.Forms.DockStyle.Left
        Me.ResFileLabel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ResFileLabel.Location = New System.Drawing.Point(255, 12)
        Me.ResFileLabel.Margin = New System.Windows.Forms.Padding(3, 12, 3, 12)
        Me.ResFileLabel.Name = "ResFileLabel"
        Me.ResFileLabel.ReadOnly = True
        Me.ResFileLabel.Size = New System.Drawing.Size(547, 23)
        Me.ResFileLabel.TabIndex = 7
        '
        'chkSaveComments
        '
        Me.chkSaveComments.AccessibleDescription = "Save Comments"
        Me.chkSaveComments.AccessibleName = "chk_save_comments"
        Me.chkSaveComments.AutoSize = True
        Me.chkSaveComments.Location = New System.Drawing.Point(3, 61)
        Me.chkSaveComments.Name = "chkSaveComments"
        Me.chkSaveComments.Size = New System.Drawing.Size(167, 23)
        Me.chkSaveComments.TabIndex = 6
        Me.chkSaveComments.Text = "generate comments"
        Me.chkSaveComments.UseVisualStyleBackColor = True
        '
        'lblCopyright
        '
        Me.lblCopyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCopyright.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(907, 68)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(178, 13)
        Me.lblCopyright.TabIndex = 5
        Me.lblCopyright.Text = My.Application.Info.Copyright.ToString & " by Herweh && B2S Team"
        Me.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkSaveEnhanced
        '
        Me.chkSaveEnhanced.AccessibleDescription = "Save Comments"
        Me.chkSaveEnhanced.AccessibleName = "chk_save_enhanced"
        Me.chkSaveEnhanced.AutoSize = True
        Me.chkSaveEnhanced.Location = New System.Drawing.Point(176, 61)
        Me.chkSaveEnhanced.Name = "chkSaveEnhanced"
        Me.chkSaveEnhanced.Size = New System.Drawing.Size(408, 23)
        Me.chkSaveEnhanced.TabIndex = 10
        Me.chkSaveEnhanced.Text = "Enhanced res file (no Backglass && Background switch)"
        Me.chkSaveEnhanced.UseVisualStyleBackColor = True
        '
        'formPlayfield
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1118, 585)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.lblInfo2)
        Me.Controls.Add(Me.panelChooseSetup)
        Me.Controls.Add(Me.groupPlayfield)
        Me.Controls.Add(Me.lblInfo)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimizeBox = False
        Me.Name = "formPlayfield"
        Me.Padding = New System.Windows.Forms.Padding(15)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Playfield - B2S Screen Resolution Identifier"
        Me.TopMost = True
        Me.groupPlayfield.ResumeLayout(False)
        Me.groupPlayfield.PerformLayout()
        Me.panelChooseSetup.ResumeLayout(False)
        Me.panelChooseSetup.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents buttonSave As System.Windows.Forms.Button
    Friend WithEvents radio1Screen As System.Windows.Forms.RadioButton
    Friend WithEvents radio2Screen As System.Windows.Forms.RadioButton
    Friend WithEvents radio3Screen As System.Windows.Forms.RadioButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents groupPlayfield As System.Windows.Forms.GroupBox
    Friend WithEvents lblPlayfield As System.Windows.Forms.Label
    Friend WithEvents lblPlayfieldSizeHeight As System.Windows.Forms.Label
    Friend WithEvents txtPlayfieldSizeHeight As System.Windows.Forms.TextBox
    Friend WithEvents txtPlayfieldSizeWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblPlayfieldSize As System.Windows.Forms.Label
    Friend WithEvents txtPlayfieldScreen As System.Windows.Forms.TextBox
    Friend WithEvents lblPlayfieldScreen As System.Windows.Forms.Label
    Friend WithEvents chkPlayfieldFullSize As System.Windows.Forms.CheckBox
    Friend WithEvents lblPlayfieldScreenSizeHeight As System.Windows.Forms.Label
    Friend WithEvents txtPlayfieldScreenSizeHeight As System.Windows.Forms.TextBox
    Friend WithEvents txtPlayfieldScreenSizeWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblPlayfieldScreenSizeWidth As System.Windows.Forms.Label
    Friend WithEvents lblPlayfieldLocationY As System.Windows.Forms.Label
    Friend WithEvents txtPlayfieldLocationY As System.Windows.Forms.TextBox
    Friend WithEvents txtPlayfieldLocationX As System.Windows.Forms.TextBox
    Friend WithEvents lblPlayfieldLocationX As System.Windows.Forms.Label
    Friend WithEvents buttonBringMeTheOtherWindows As System.Windows.Forms.Button
    Friend WithEvents panelChooseSetup As System.Windows.Forms.Panel
    Friend WithEvents lblChooseSetup As System.Windows.Forms.Label
    Friend WithEvents lblInfo2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents chkSaveComments As CheckBox
    Friend WithEvents txtPlayfieldScreenScale As TextBox
    Friend WithEvents lblPlayfieldScreenScale As Label
    Friend WithEvents ResFileLabel As TextBox
    Friend WithEvents buttonSaveGlobal As Button
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents chkSaveEnhanced As CheckBox
End Class
