<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formVPM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formVPM))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.groupGeneral = New System.Windows.Forms.GroupBox()
        Me.chkDoubleSize = New System.Windows.Forms.CheckBox()
        Me.chkShowDMDOnly = New System.Windows.Forms.CheckBox()
        Me.chkShowFrame = New System.Windows.Forms.CheckBox()
        Me.btnResume = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.chkShowTitle = New System.Windows.Forms.CheckBox()
        Me.cmbROMName = New System.Windows.Forms.ComboBox()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.lblROMName = New System.Windows.Forms.Label()
        Me.txtVPTablesFolder = New System.Windows.Forms.TextBox()
        Me.grpSettings = New System.Windows.Forms.GroupBox()
        Me.chkCopyBackglassFileToVPTablesFolder = New System.Windows.Forms.CheckBox()
        Me.btnVPTablesFolder = New System.Windows.Forms.Button()
        Me.lblVPTablesFolder = New System.Windows.Forms.Label()
        Me.TimerInfos = New System.Windows.Forms.Timer(Me.components)
        Me.grpInfo = New System.Windows.Forms.GroupBox()
        Me.lvwStats = New System.Windows.Forms.ListView()
        Me.hdIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdCountOff = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdCountOn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.groupGeneral.SuspendLayout()
        Me.grpSettings.SuspendLayout()
        Me.grpInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClose.Location = New System.Drawing.Point(508, 509)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(115, 25)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'groupGeneral
        '
        Me.groupGeneral.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupGeneral.Controls.Add(Me.chkDoubleSize)
        Me.groupGeneral.Controls.Add(Me.chkShowDMDOnly)
        Me.groupGeneral.Controls.Add(Me.chkShowFrame)
        Me.groupGeneral.Controls.Add(Me.btnResume)
        Me.groupGeneral.Controls.Add(Me.btnPause)
        Me.groupGeneral.Controls.Add(Me.chkShowTitle)
        Me.groupGeneral.Controls.Add(Me.cmbROMName)
        Me.groupGeneral.Controls.Add(Me.btnStop)
        Me.groupGeneral.Controls.Add(Me.btnStart)
        Me.groupGeneral.Controls.Add(Me.lblROMName)
        Me.groupGeneral.Location = New System.Drawing.Point(12, 8)
        Me.groupGeneral.Name = "groupGeneral"
        Me.groupGeneral.Size = New System.Drawing.Size(610, 152)
        Me.groupGeneral.TabIndex = 13
        Me.groupGeneral.TabStop = False
        Me.groupGeneral.Text = "General"
        '
        'chkDoubleSize
        '
        Me.chkDoubleSize.Location = New System.Drawing.Point(103, 122)
        Me.chkDoubleSize.Name = "chkDoubleSize"
        Me.chkDoubleSize.Size = New System.Drawing.Size(141, 17)
        Me.chkDoubleSize.TabIndex = 4
        Me.chkDoubleSize.Text = "D&ouble size"
        Me.chkDoubleSize.UseVisualStyleBackColor = True
        '
        'chkShowDMDOnly
        '
        Me.chkShowDMDOnly.Location = New System.Drawing.Point(103, 99)
        Me.chkShowDMDOnly.Name = "chkShowDMDOnly"
        Me.chkShowDMDOnly.Size = New System.Drawing.Size(141, 17)
        Me.chkShowDMDOnly.TabIndex = 3
        Me.chkShowDMDOnly.Text = "Show &DMD only"
        Me.chkShowDMDOnly.UseVisualStyleBackColor = True
        '
        'chkShowFrame
        '
        Me.chkShowFrame.Location = New System.Drawing.Point(103, 76)
        Me.chkShowFrame.Name = "chkShowFrame"
        Me.chkShowFrame.Size = New System.Drawing.Size(141, 17)
        Me.chkShowFrame.TabIndex = 2
        Me.chkShowFrame.Text = "Show window &border"
        Me.chkShowFrame.UseVisualStyleBackColor = True
        '
        'btnResume
        '
        Me.btnResume.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnResume.Enabled = False
        Me.btnResume.Location = New System.Drawing.Point(472, 110)
        Me.btnResume.Name = "btnResume"
        Me.btnResume.Size = New System.Drawing.Size(126, 25)
        Me.btnResume.TabIndex = 8
        Me.btnResume.Text = "&Resume"
        Me.btnResume.UseVisualStyleBackColor = True
        '
        'btnPause
        '
        Me.btnPause.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPause.Enabled = False
        Me.btnPause.Location = New System.Drawing.Point(472, 79)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(126, 25)
        Me.btnPause.TabIndex = 7
        Me.btnPause.Text = "&Pause"
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'chkShowTitle
        '
        Me.chkShowTitle.Location = New System.Drawing.Point(103, 53)
        Me.chkShowTitle.Name = "chkShowTitle"
        Me.chkShowTitle.Size = New System.Drawing.Size(141, 17)
        Me.chkShowTitle.TabIndex = 1
        Me.chkShowTitle.Text = "Show window &title"
        Me.chkShowTitle.UseVisualStyleBackColor = True
        '
        'cmbROMName
        '
        Me.cmbROMName.FormattingEnabled = True
        Me.cmbROMName.Location = New System.Drawing.Point(103, 20)
        Me.cmbROMName.Name = "cmbROMName"
        Me.cmbROMName.Size = New System.Drawing.Size(141, 21)
        Me.cmbROMName.Sorted = True
        Me.cmbROMName.TabIndex = 0
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(472, 48)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(126, 25)
        Me.btnStop.TabIndex = 6
        Me.btnStop.Text = "&Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStart.Location = New System.Drawing.Point(472, 17)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(126, 25)
        Me.btnStart.TabIndex = 5
        Me.btnStart.Text = "&Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'lblROMName
        '
        Me.lblROMName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblROMName.Location = New System.Drawing.Point(14, 23)
        Me.lblROMName.Name = "lblROMName"
        Me.lblROMName.Size = New System.Drawing.Size(83, 13)
        Me.lblROMName.TabIndex = 3
        Me.lblROMName.Text = "ROM name:"
        Me.lblROMName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVPTablesFolder
        '
        Me.txtVPTablesFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVPTablesFolder.Location = New System.Drawing.Point(103, 20)
        Me.txtVPTablesFolder.MaxLength = 100
        Me.txtVPTablesFolder.Name = "txtVPTablesFolder"
        Me.txtVPTablesFolder.Size = New System.Drawing.Size(471, 21)
        Me.txtVPTablesFolder.TabIndex = 9
        '
        'grpSettings
        '
        Me.grpSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSettings.Controls.Add(Me.chkCopyBackglassFileToVPTablesFolder)
        Me.grpSettings.Controls.Add(Me.btnVPTablesFolder)
        Me.grpSettings.Controls.Add(Me.lblVPTablesFolder)
        Me.grpSettings.Controls.Add(Me.txtVPTablesFolder)
        Me.grpSettings.Location = New System.Drawing.Point(12, 166)
        Me.grpSettings.Name = "grpSettings"
        Me.grpSettings.Size = New System.Drawing.Size(610, 74)
        Me.grpSettings.TabIndex = 14
        Me.grpSettings.TabStop = False
        Me.grpSettings.Text = "Settings"
        '
        'chkCopyBackglassFileToVPTablesFolder
        '
        Me.chkCopyBackglassFileToVPTablesFolder.Location = New System.Drawing.Point(103, 47)
        Me.chkCopyBackglassFileToVPTablesFolder.Name = "chkCopyBackglassFileToVPTablesFolder"
        Me.chkCopyBackglassFileToVPTablesFolder.Size = New System.Drawing.Size(309, 18)
        Me.chkCopyBackglassFileToVPTablesFolder.TabIndex = 11
        Me.chkCopyBackglassFileToVPTablesFolder.Text = "Copy backglass file to VP tables folder"
        Me.chkCopyBackglassFileToVPTablesFolder.UseVisualStyleBackColor = True
        '
        'btnVPTablesFolder
        '
        Me.btnVPTablesFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnVPTablesFolder.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.btnVPTablesFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnVPTablesFolder.Location = New System.Drawing.Point(575, 20)
        Me.btnVPTablesFolder.Name = "btnVPTablesFolder"
        Me.btnVPTablesFolder.Size = New System.Drawing.Size(23, 22)
        Me.btnVPTablesFolder.TabIndex = 10
        Me.btnVPTablesFolder.Text = "..."
        Me.btnVPTablesFolder.UseVisualStyleBackColor = True
        '
        'lblVPTablesFolder
        '
        Me.lblVPTablesFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblVPTablesFolder.Location = New System.Drawing.Point(6, 23)
        Me.lblVPTablesFolder.Name = "lblVPTablesFolder"
        Me.lblVPTablesFolder.Size = New System.Drawing.Size(91, 13)
        Me.lblVPTablesFolder.TabIndex = 10
        Me.lblVPTablesFolder.Text = "VP tables folder:"
        Me.lblVPTablesFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TimerInfos
        '
        '
        'grpInfo
        '
        Me.grpInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpInfo.Controls.Add(Me.lvwStats)
        Me.grpInfo.Controls.Add(Me.txtInfo)
        Me.grpInfo.Location = New System.Drawing.Point(12, 246)
        Me.grpInfo.Name = "grpInfo"
        Me.grpInfo.Size = New System.Drawing.Size(610, 254)
        Me.grpInfo.TabIndex = 15
        Me.grpInfo.TabStop = False
        Me.grpInfo.Text = "Info"
        '
        'lvwStats
        '
        Me.lvwStats.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwStats.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.hdIndex, Me.hdType, Me.hdID, Me.hdCountOff, Me.hdCountOn})
        Me.lvwStats.FullRowSelect = True
        Me.lvwStats.GridLines = True
        Me.lvwStats.HideSelection = False
        Me.lvwStats.Location = New System.Drawing.Point(250, 21)
        Me.lvwStats.MultiSelect = False
        Me.lvwStats.Name = "lvwStats"
        Me.lvwStats.Size = New System.Drawing.Size(348, 221)
        Me.lvwStats.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvwStats.TabIndex = 13
        Me.lvwStats.UseCompatibleStateImageBehavior = False
        Me.lvwStats.View = System.Windows.Forms.View.Details
        '
        'hdIndex
        '
        Me.hdIndex.Text = "Index"
        Me.hdIndex.Width = 0
        '
        'hdType
        '
        Me.hdType.Text = "Type"
        Me.hdType.Width = 40
        '
        'hdID
        '
        Me.hdID.Text = "ID"
        Me.hdID.Width = 40
        '
        'hdCountOff
        '
        Me.hdCountOff.Text = "Off"
        Me.hdCountOff.Width = 40
        '
        'hdCountOn
        '
        Me.hdCountOn.Text = "On"
        Me.hdCountOn.Width = 40
        '
        'txtInfo
        '
        Me.txtInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtInfo.Location = New System.Drawing.Point(11, 21)
        Me.txtInfo.MaxLength = 100
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo.Size = New System.Drawing.Size(233, 221)
        Me.txtInfo.TabIndex = 12
        '
        'formVPM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(634, 543)
        Me.Controls.Add(Me.grpInfo)
        Me.Controls.Add(Me.grpSettings)
        Me.Controls.Add(Me.groupGeneral)
        Me.Controls.Add(Me.btnClose)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formVPM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "VPinMAME starter"
        Me.groupGeneral.ResumeLayout(False)
        Me.grpSettings.ResumeLayout(False)
        Me.grpSettings.PerformLayout()
        Me.grpInfo.ResumeLayout(False)
        Me.grpInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents groupGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents lblROMName As System.Windows.Forms.Label
    Friend WithEvents txtVPTablesFolder As System.Windows.Forms.TextBox
    Friend WithEvents cmbROMName As System.Windows.Forms.ComboBox
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnResume As System.Windows.Forms.Button
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents chkShowTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkDoubleSize As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowDMDOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowFrame As System.Windows.Forms.CheckBox
    Friend WithEvents grpSettings As System.Windows.Forms.GroupBox
    Friend WithEvents lblVPTablesFolder As System.Windows.Forms.Label
    Friend WithEvents btnVPTablesFolder As System.Windows.Forms.Button
    Friend WithEvents TimerInfos As System.Windows.Forms.Timer
    Friend WithEvents grpInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Friend WithEvents chkCopyBackglassFileToVPTablesFolder As System.Windows.Forms.CheckBox
    Friend WithEvents lvwStats As System.Windows.Forms.ListView
    Friend WithEvents hdType As System.Windows.Forms.ColumnHeader
    Friend WithEvents hdID As System.Windows.Forms.ColumnHeader
    Friend WithEvents hdCountOff As System.Windows.Forms.ColumnHeader
    Friend WithEvents hdCountOn As System.Windows.Forms.ColumnHeader
    Friend WithEvents hdIndex As System.Windows.Forms.ColumnHeader
End Class
