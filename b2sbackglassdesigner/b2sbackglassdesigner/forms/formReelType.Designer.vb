<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formReelType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formReelType))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.GroupBoxImages = New System.Windows.Forms.GroupBox()
        Me.lblMS = New System.Windows.Forms.Label()
        Me.btnImportLEDs = New System.Windows.Forms.Button()
        Me.cmbRollingDirection = New System.Windows.Forms.ComboBox()
        Me.lblRollingDirection = New System.Windows.Forms.Label()
        Me.numIntermediateCount = New System.Windows.Forms.NumericUpDown()
        Me.lblIntermediateCount = New System.Windows.Forms.Label()
        Me.numRollingInterval = New System.Windows.Forms.NumericUpDown()
        Me.lblRollingInterval = New System.Windows.Forms.Label()
        Me.lvEMCreditReels = New System.Windows.Forms.ListView()
        Me.lvLEDs = New System.Windows.Forms.ListView()
        Me.btnImportCreditReels = New System.Windows.Forms.Button()
        Me.btnImportReels = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.lvEMReels = New System.Windows.Forms.ListView()
        Me.ilReels = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBoxRendered = New System.Windows.Forms.GroupBox()
        Me.btnGetColor = New System.Windows.Forms.Button()
        Me.chkUseDream7LEDs = New System.Windows.Forms.CheckBox()
        Me.PanelRenderedLEDs = New System.Windows.Forms.Panel()
        Me.RenderedLED14 = New B2SBackglassDesigner.B2SRenderedLEDPanel()
        Me.RenderedLED08 = New B2SBackglassDesigner.B2SRenderedLEDPanel()
        Me.RenderedLED10 = New B2SBackglassDesigner.B2SRenderedLEDPanel()
        Me.PanelDream7LEDs = New System.Windows.Forms.Panel()
        Me.txtShear = New System.Windows.Forms.TextBox()
        Me.txtSize = New System.Windows.Forms.TextBox()
        Me.txtGlow = New System.Windows.Forms.TextBox()
        Me.LabelGlow = New System.Windows.Forms.Label()
        Me.LabelShear = New System.Windows.Forms.Label()
        Me.TrackBarShear = New System.Windows.Forms.TrackBar()
        Me.TrackBarThickness = New System.Windows.Forms.TrackBar()
        Me.LabelThickness = New System.Windows.Forms.Label()
        Me.TrackBarGlow = New System.Windows.Forms.TrackBar()
        Me.Dream7LED14 = New B2SBackglassDesigner.B2SDream7LEDPanel()
        Me.Dream7LED10 = New B2SBackglassDesigner.B2SDream7LEDPanel()
        Me.Dream7LED08 = New B2SBackglassDesigner.B2SDream7LEDPanel()
        Me.cmbDefaultColors = New System.Windows.Forms.ComboBox()
        Me.B2SColorBarLEDs = New B2SBackglassDesigner.B2SColorBar()
        Me.lblInfo2 = New System.Windows.Forms.Label()
        Me.ilLEDs = New System.Windows.Forms.ImageList(Me.components)
        Me.ilEMCeditReels = New System.Windows.Forms.ImageList(Me.components)
        Me.TimerGetColor = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBoxImages.SuspendLayout()
        CType(Me.numIntermediateCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numRollingInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxRendered.SuspendLayout()
        Me.PanelRenderedLEDs.SuspendLayout()
        Me.PanelDream7LEDs.SuspendLayout()
        CType(Me.TrackBarShear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarThickness, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarGlow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        resources.ApplyResources(Me.btnOk, "btnOk")
        Me.btnOk.Name = "btnOk"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'GroupBoxImages
        '
        resources.ApplyResources(Me.GroupBoxImages, "GroupBoxImages")
        Me.GroupBoxImages.Controls.Add(Me.lblMS)
        Me.GroupBoxImages.Controls.Add(Me.btnImportLEDs)
        Me.GroupBoxImages.Controls.Add(Me.cmbRollingDirection)
        Me.GroupBoxImages.Controls.Add(Me.lblRollingDirection)
        Me.GroupBoxImages.Controls.Add(Me.numIntermediateCount)
        Me.GroupBoxImages.Controls.Add(Me.lblIntermediateCount)
        Me.GroupBoxImages.Controls.Add(Me.numRollingInterval)
        Me.GroupBoxImages.Controls.Add(Me.lblRollingInterval)
        Me.GroupBoxImages.Controls.Add(Me.lvEMCreditReels)
        Me.GroupBoxImages.Controls.Add(Me.lvLEDs)
        Me.GroupBoxImages.Controls.Add(Me.btnImportCreditReels)
        Me.GroupBoxImages.Controls.Add(Me.btnImportReels)
        Me.GroupBoxImages.Controls.Add(Me.lblInfo)
        Me.GroupBoxImages.Controls.Add(Me.lvEMReels)
        Me.GroupBoxImages.Name = "GroupBoxImages"
        Me.GroupBoxImages.TabStop = False
        '
        'lblMS
        '
        resources.ApplyResources(Me.lblMS, "lblMS")
        Me.lblMS.Name = "lblMS"
        '
        'btnImportLEDs
        '
        resources.ApplyResources(Me.btnImportLEDs, "btnImportLEDs")
        Me.btnImportLEDs.Name = "btnImportLEDs"
        Me.btnImportLEDs.UseVisualStyleBackColor = True
        '
        'cmbRollingDirection
        '
        resources.ApplyResources(Me.cmbRollingDirection, "cmbRollingDirection")
        Me.cmbRollingDirection.FormattingEnabled = True
        Me.cmbRollingDirection.Items.AddRange(New Object() {resources.GetString("cmbRollingDirection.Items"), resources.GetString("cmbRollingDirection.Items1")})
        Me.cmbRollingDirection.Name = "cmbRollingDirection"
        '
        'lblRollingDirection
        '
        resources.ApplyResources(Me.lblRollingDirection, "lblRollingDirection")
        Me.lblRollingDirection.Name = "lblRollingDirection"
        '
        'numIntermediateCount
        '
        resources.ApplyResources(Me.numIntermediateCount, "numIntermediateCount")
        Me.numIntermediateCount.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numIntermediateCount.Name = "numIntermediateCount"
        '
        'lblIntermediateCount
        '
        resources.ApplyResources(Me.lblIntermediateCount, "lblIntermediateCount")
        Me.lblIntermediateCount.Name = "lblIntermediateCount"
        '
        'numRollingInterval
        '
        resources.ApplyResources(Me.numRollingInterval, "numRollingInterval")
        Me.numRollingInterval.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.numRollingInterval.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.numRollingInterval.Name = "numRollingInterval"
        Me.numRollingInterval.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'lblRollingInterval
        '
        resources.ApplyResources(Me.lblRollingInterval, "lblRollingInterval")
        Me.lblRollingInterval.Name = "lblRollingInterval"
        '
        'lvEMCreditReels
        '
        resources.ApplyResources(Me.lvEMCreditReels, "lvEMCreditReels")
        Me.lvEMCreditReels.HideSelection = False
        Me.lvEMCreditReels.Name = "lvEMCreditReels"
        Me.lvEMCreditReels.UseCompatibleStateImageBehavior = False
        Me.lvEMCreditReels.View = System.Windows.Forms.View.List
        '
        'lvLEDs
        '
        resources.ApplyResources(Me.lvLEDs, "lvLEDs")
        Me.lvLEDs.HideSelection = False
        Me.lvLEDs.Name = "lvLEDs"
        Me.lvLEDs.UseCompatibleStateImageBehavior = False
        Me.lvLEDs.View = System.Windows.Forms.View.List
        '
        'btnImportCreditReels
        '
        resources.ApplyResources(Me.btnImportCreditReels, "btnImportCreditReels")
        Me.btnImportCreditReels.Name = "btnImportCreditReels"
        Me.btnImportCreditReels.UseVisualStyleBackColor = True
        '
        'btnImportReels
        '
        resources.ApplyResources(Me.btnImportReels, "btnImportReels")
        Me.btnImportReels.Name = "btnImportReels"
        Me.btnImportReels.UseVisualStyleBackColor = True
        '
        'lblInfo
        '
        resources.ApplyResources(Me.lblInfo, "lblInfo")
        Me.lblInfo.Name = "lblInfo"
        '
        'lvEMReels
        '
        resources.ApplyResources(Me.lvEMReels, "lvEMReels")
        Me.lvEMReels.HideSelection = False
        Me.lvEMReels.Name = "lvEMReels"
        Me.lvEMReels.UseCompatibleStateImageBehavior = False
        Me.lvEMReels.View = System.Windows.Forms.View.List
        '
        'ilReels
        '
        Me.ilReels.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.ilReels, "ilReels")
        Me.ilReels.TransparentColor = System.Drawing.Color.Transparent
        '
        'GroupBoxRendered
        '
        resources.ApplyResources(Me.GroupBoxRendered, "GroupBoxRendered")
        Me.GroupBoxRendered.Controls.Add(Me.btnGetColor)
        Me.GroupBoxRendered.Controls.Add(Me.chkUseDream7LEDs)
        Me.GroupBoxRendered.Controls.Add(Me.PanelRenderedLEDs)
        Me.GroupBoxRendered.Controls.Add(Me.PanelDream7LEDs)
        Me.GroupBoxRendered.Controls.Add(Me.cmbDefaultColors)
        Me.GroupBoxRendered.Controls.Add(Me.B2SColorBarLEDs)
        Me.GroupBoxRendered.Controls.Add(Me.lblInfo2)
        Me.GroupBoxRendered.Name = "GroupBoxRendered"
        Me.GroupBoxRendered.TabStop = False
        '
        'btnGetColor
        '
        resources.ApplyResources(Me.btnGetColor, "btnGetColor")
        Me.btnGetColor.Name = "btnGetColor"
        Me.btnGetColor.UseVisualStyleBackColor = True
        '
        'chkUseDream7LEDs
        '
        resources.ApplyResources(Me.chkUseDream7LEDs, "chkUseDream7LEDs")
        Me.chkUseDream7LEDs.Checked = True
        Me.chkUseDream7LEDs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseDream7LEDs.Name = "chkUseDream7LEDs"
        Me.chkUseDream7LEDs.UseVisualStyleBackColor = True
        '
        'PanelRenderedLEDs
        '
        Me.PanelRenderedLEDs.Controls.Add(Me.RenderedLED14)
        Me.PanelRenderedLEDs.Controls.Add(Me.RenderedLED08)
        Me.PanelRenderedLEDs.Controls.Add(Me.RenderedLED10)
        resources.ApplyResources(Me.PanelRenderedLEDs, "PanelRenderedLEDs")
        Me.PanelRenderedLEDs.Name = "PanelRenderedLEDs"
        '
        'RenderedLED14
        '
        Me.RenderedLED14.LabelText = "14"
        Me.RenderedLED14.LEDType = B2SBackglassDesigner.B2SRenderedLED.eLEDType.LED14
        resources.ApplyResources(Me.RenderedLED14, "RenderedLED14")
        Me.RenderedLED14.Name = "RenderedLED14"
        Me.RenderedLED14.Selected = False
        '
        'RenderedLED08
        '
        Me.RenderedLED08.LabelText = "7"
        Me.RenderedLED08.LEDType = B2SBackglassDesigner.B2SRenderedLED.eLEDType.LED8
        resources.ApplyResources(Me.RenderedLED08, "RenderedLED08")
        Me.RenderedLED08.Name = "RenderedLED08"
        Me.RenderedLED08.Selected = False
        '
        'RenderedLED10
        '
        Me.RenderedLED10.LabelText = "10"
        Me.RenderedLED10.LEDType = B2SBackglassDesigner.B2SRenderedLED.eLEDType.LED10
        resources.ApplyResources(Me.RenderedLED10, "RenderedLED10")
        Me.RenderedLED10.Name = "RenderedLED10"
        Me.RenderedLED10.Selected = False
        '
        'PanelDream7LEDs
        '
        Me.PanelDream7LEDs.Controls.Add(Me.txtShear)
        Me.PanelDream7LEDs.Controls.Add(Me.txtSize)
        Me.PanelDream7LEDs.Controls.Add(Me.txtGlow)
        Me.PanelDream7LEDs.Controls.Add(Me.LabelGlow)
        Me.PanelDream7LEDs.Controls.Add(Me.LabelShear)
        Me.PanelDream7LEDs.Controls.Add(Me.TrackBarShear)
        Me.PanelDream7LEDs.Controls.Add(Me.TrackBarThickness)
        Me.PanelDream7LEDs.Controls.Add(Me.LabelThickness)
        Me.PanelDream7LEDs.Controls.Add(Me.TrackBarGlow)
        Me.PanelDream7LEDs.Controls.Add(Me.Dream7LED14)
        Me.PanelDream7LEDs.Controls.Add(Me.Dream7LED10)
        Me.PanelDream7LEDs.Controls.Add(Me.Dream7LED08)
        resources.ApplyResources(Me.PanelDream7LEDs, "PanelDream7LEDs")
        Me.PanelDream7LEDs.Name = "PanelDream7LEDs"
        '
        'txtShear
        '
        resources.ApplyResources(Me.txtShear, "txtShear")
        Me.txtShear.Name = "txtShear"
        Me.txtShear.ReadOnly = True
        '
        'txtSize
        '
        resources.ApplyResources(Me.txtSize, "txtSize")
        Me.txtSize.Name = "txtSize"
        Me.txtSize.ReadOnly = True
        '
        'txtGlow
        '
        resources.ApplyResources(Me.txtGlow, "txtGlow")
        Me.txtGlow.Name = "txtGlow"
        Me.txtGlow.ReadOnly = True
        '
        'LabelGlow
        '
        resources.ApplyResources(Me.LabelGlow, "LabelGlow")
        Me.LabelGlow.Name = "LabelGlow"
        '
        'LabelShear
        '
        resources.ApplyResources(Me.LabelShear, "LabelShear")
        Me.LabelShear.Name = "LabelShear"
        '
        'TrackBarShear
        '
        resources.ApplyResources(Me.TrackBarShear, "TrackBarShear")
        Me.TrackBarShear.Maximum = 25
        Me.TrackBarShear.Name = "TrackBarShear"
        '
        'TrackBarThickness
        '
        resources.ApplyResources(Me.TrackBarThickness, "TrackBarThickness")
        Me.TrackBarThickness.Maximum = 25
        Me.TrackBarThickness.Minimum = 1
        Me.TrackBarThickness.Name = "TrackBarThickness"
        Me.TrackBarThickness.Value = 1
        '
        'LabelThickness
        '
        resources.ApplyResources(Me.LabelThickness, "LabelThickness")
        Me.LabelThickness.Name = "LabelThickness"
        '
        'TrackBarGlow
        '
        resources.ApplyResources(Me.TrackBarGlow, "TrackBarGlow")
        Me.TrackBarGlow.Maximum = 25
        Me.TrackBarGlow.Name = "TrackBarGlow"
        '
        'Dream7LED14
        '
        Me.Dream7LED14.LabelText = "14"
        Me.Dream7LED14.LEDColor = System.Drawing.Color.Empty
        Me.Dream7LED14.LEDType = B2SBackglassDesigner.SegmentNumberType.FourteenSegment
        resources.ApplyResources(Me.Dream7LED14, "Dream7LED14")
        Me.Dream7LED14.Name = "Dream7LED14"
        Me.Dream7LED14.Selected = False
        '
        'Dream7LED10
        '
        Me.Dream7LED10.LabelText = "10"
        Me.Dream7LED10.LEDColor = System.Drawing.Color.Empty
        Me.Dream7LED10.LEDType = B2SBackglassDesigner.SegmentNumberType.TenSegment
        resources.ApplyResources(Me.Dream7LED10, "Dream7LED10")
        Me.Dream7LED10.Name = "Dream7LED10"
        Me.Dream7LED10.Selected = False
        '
        'Dream7LED08
        '
        Me.Dream7LED08.LabelText = "7"
        Me.Dream7LED08.LEDColor = System.Drawing.Color.Empty
        Me.Dream7LED08.LEDType = B2SBackglassDesigner.SegmentNumberType.SevenSegment
        resources.ApplyResources(Me.Dream7LED08, "Dream7LED08")
        Me.Dream7LED08.Name = "Dream7LED08"
        Me.Dream7LED08.Selected = False
        '
        'cmbDefaultColors
        '
        resources.ApplyResources(Me.cmbDefaultColors, "cmbDefaultColors")
        Me.cmbDefaultColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultColors.FormattingEnabled = True
        Me.cmbDefaultColors.Items.AddRange(New Object() {resources.GetString("cmbDefaultColors.Items"), resources.GetString("cmbDefaultColors.Items1"), resources.GetString("cmbDefaultColors.Items2"), resources.GetString("cmbDefaultColors.Items3"), resources.GetString("cmbDefaultColors.Items4")})
        Me.cmbDefaultColors.Name = "cmbDefaultColors"
        '
        'B2SColorBarLEDs
        '
        resources.ApplyResources(Me.B2SColorBarLEDs, "B2SColorBarLEDs")
        Me.B2SColorBarLEDs.CurrentColor = System.Drawing.Color.Black
        Me.B2SColorBarLEDs.Name = "B2SColorBarLEDs"
        '
        'lblInfo2
        '
        resources.ApplyResources(Me.lblInfo2, "lblInfo2")
        Me.lblInfo2.Name = "lblInfo2"
        '
        'ilLEDs
        '
        Me.ilLEDs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.ilLEDs, "ilLEDs")
        Me.ilLEDs.TransparentColor = System.Drawing.Color.Transparent
        '
        'ilEMCeditReels
        '
        Me.ilEMCeditReels.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.ilEMCeditReels, "ilEMCeditReels")
        Me.ilEMCeditReels.TransparentColor = System.Drawing.Color.Transparent
        '
        'TimerGetColor
        '
        Me.TimerGetColor.Interval = 50
        '
        'formReelType
        '
        Me.AcceptButton = Me.btnOk
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.GroupBoxRendered)
        Me.Controls.Add(Me.GroupBoxImages)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.KeyPreview = True
        Me.Name = "formReelType"
        Me.GroupBoxImages.ResumeLayout(False)
        Me.GroupBoxImages.PerformLayout()
        CType(Me.numIntermediateCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numRollingInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxRendered.ResumeLayout(False)
        Me.GroupBoxRendered.PerformLayout()
        Me.PanelRenderedLEDs.ResumeLayout(False)
        Me.PanelDream7LEDs.ResumeLayout(False)
        Me.PanelDream7LEDs.PerformLayout()
        CType(Me.TrackBarShear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarThickness, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarGlow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents GroupBoxImages As System.Windows.Forms.GroupBox
    Friend WithEvents btnImportReels As System.Windows.Forms.Button
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents lvEMReels As System.Windows.Forms.ListView
    Friend WithEvents ilReels As System.Windows.Forms.ImageList
    Friend WithEvents GroupBoxRendered As System.Windows.Forms.GroupBox
    Friend WithEvents lblInfo2 As System.Windows.Forms.Label
    Friend WithEvents B2SColorBarLEDs As B2SBackglassDesigner.B2SColorBar
    Friend WithEvents cmbDefaultColors As System.Windows.Forms.ComboBox
    Friend WithEvents btnImportCreditReels As System.Windows.Forms.Button
    Friend WithEvents lvLEDs As System.Windows.Forms.ListView
    Friend WithEvents ilLEDs As System.Windows.Forms.ImageList
    Friend WithEvents lvEMCreditReels As System.Windows.Forms.ListView
    Friend WithEvents ilEMCeditReels As System.Windows.Forms.ImageList
    Friend WithEvents btnGetColor As System.Windows.Forms.Button
    Friend WithEvents TimerGetColor As System.Windows.Forms.Timer
    Friend WithEvents PanelRenderedLEDs As System.Windows.Forms.Panel
    Friend WithEvents RenderedLED08 As B2SBackglassDesigner.B2SRenderedLEDPanel
    Friend WithEvents RenderedLED10 As B2SBackglassDesigner.B2SRenderedLEDPanel
    Friend WithEvents RenderedLED14 As B2SBackglassDesigner.B2SRenderedLEDPanel
    Friend WithEvents PanelDream7LEDs As System.Windows.Forms.Panel
    Friend WithEvents LabelShear As System.Windows.Forms.Label
    Friend WithEvents TrackBarShear As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBarThickness As System.Windows.Forms.TrackBar
    Friend WithEvents LabelThickness As System.Windows.Forms.Label
    Friend WithEvents TrackBarGlow As System.Windows.Forms.TrackBar
    Friend WithEvents Dream7LED14 As B2SBackglassDesigner.B2SDream7LEDPanel
    Friend WithEvents Dream7LED10 As B2SBackglassDesigner.B2SDream7LEDPanel
    Friend WithEvents Dream7LED08 As B2SBackglassDesigner.B2SDream7LEDPanel
    Friend WithEvents LabelGlow As System.Windows.Forms.Label
    Friend WithEvents chkUseDream7LEDs As System.Windows.Forms.CheckBox
    Friend WithEvents txtGlow As System.Windows.Forms.TextBox
    Friend WithEvents txtShear As System.Windows.Forms.TextBox
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    Friend WithEvents numIntermediateCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblIntermediateCount As System.Windows.Forms.Label
    Friend WithEvents numRollingInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblRollingInterval As System.Windows.Forms.Label
    Friend WithEvents cmbRollingDirection As System.Windows.Forms.ComboBox
    Friend WithEvents lblRollingDirection As System.Windows.Forms.Label
    Friend WithEvents btnImportLEDs As System.Windows.Forms.Button
    Friend WithEvents lblMS As System.Windows.Forms.Label
End Class
