<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formToolReelsAndLEDs
    Inherits formBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formToolReelsAndLEDs))
        Me.lvReelsAndLEDs = New System.Windows.Forms.ListView()
        Me.ilReelsAndLEDs = New System.Windows.Forms.ImageList(Me.components)
        Me.lblNumberOfPlayers = New System.Windows.Forms.Label()
        Me.cmbNumberOfPlayers = New System.Windows.Forms.ComboBox()
        Me.lblDigits = New System.Windows.Forms.Label()
        Me.btnPerfectScaleWidthFix = New System.Windows.Forms.Button()
        Me.btnChangeLEDColor = New System.Windows.Forms.Button()
        Me.B2SLine1 = New B2SBackglassDesigner.B2SLine()
        Me.B2SLine2 = New B2SBackglassDesigner.B2SLine()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.lblSpacing = New System.Windows.Forms.Label()
        Me.numericSpacing = New System.Windows.Forms.NumericUpDown()
        Me.B2SLine3 = New B2SBackglassDesigner.B2SLine()
        Me.numericDigits = New System.Windows.Forms.NumericUpDown()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.lblID = New System.Windows.Forms.Label()
        Me.chkDream7 = New System.Windows.Forms.CheckBox()
        Me.txtLocationY = New System.Windows.Forms.TextBox()
        Me.txtLocationX = New System.Windows.Forms.TextBox()
        Me.txtSizeHeight = New System.Windows.Forms.TextBox()
        Me.txtSizeWidth = New System.Windows.Forms.TextBox()
        Me.cmbB2SScoreType = New System.Windows.Forms.ComboBox()
        Me.lblB2SScoreType = New System.Windows.Forms.Label()
        Me.lblB2SID = New System.Windows.Forms.Label()
        Me.B2SLine4 = New B2SBackglassDesigner.B2SLine()
        Me.txtB2SStartID = New System.Windows.Forms.TextBox()
        Me.B2SLine5 = New B2SBackglassDesigner.B2SLine()
        Me.btnReelIllumination = New System.Windows.Forms.Button()
        Me.cmbB2SPlayerNo = New System.Windows.Forms.ComboBox()
        Me.lblB2SPlayerNo = New System.Windows.Forms.Label()
        Me.cmbInitState = New System.Windows.Forms.ComboBox()
        Me.lblInitState = New System.Windows.Forms.Label()
        CType(Me.numericSpacing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericDigits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvReelsAndLEDs
        '
        resources.ApplyResources(Me.lvReelsAndLEDs, "lvReelsAndLEDs")
        Me.lvReelsAndLEDs.LargeImageList = Me.ilReelsAndLEDs
        Me.lvReelsAndLEDs.Name = "lvReelsAndLEDs"
        Me.lvReelsAndLEDs.SmallImageList = Me.ilReelsAndLEDs
        Me.lvReelsAndLEDs.UseCompatibleStateImageBehavior = False
        '
        'ilReelsAndLEDs
        '
        Me.ilReelsAndLEDs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.ilReelsAndLEDs, "ilReelsAndLEDs")
        Me.ilReelsAndLEDs.TransparentColor = System.Drawing.Color.Transparent
        '
        'lblNumberOfPlayers
        '
        resources.ApplyResources(Me.lblNumberOfPlayers, "lblNumberOfPlayers")
        Me.lblNumberOfPlayers.Name = "lblNumberOfPlayers"
        '
        'cmbNumberOfPlayers
        '
        resources.ApplyResources(Me.cmbNumberOfPlayers, "cmbNumberOfPlayers")
        Me.cmbNumberOfPlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNumberOfPlayers.FormattingEnabled = True
        Me.cmbNumberOfPlayers.Items.AddRange(New Object() {resources.GetString("cmbNumberOfPlayers.Items"), resources.GetString("cmbNumberOfPlayers.Items1"), resources.GetString("cmbNumberOfPlayers.Items2"), resources.GetString("cmbNumberOfPlayers.Items3"), resources.GetString("cmbNumberOfPlayers.Items4"), resources.GetString("cmbNumberOfPlayers.Items5"), resources.GetString("cmbNumberOfPlayers.Items6")})
        Me.cmbNumberOfPlayers.Name = "cmbNumberOfPlayers"
        '
        'lblDigits
        '
        resources.ApplyResources(Me.lblDigits, "lblDigits")
        Me.lblDigits.Name = "lblDigits"
        '
        'btnPerfectScaleWidthFix
        '
        resources.ApplyResources(Me.btnPerfectScaleWidthFix, "btnPerfectScaleWidthFix")
        Me.btnPerfectScaleWidthFix.Name = "btnPerfectScaleWidthFix"
        Me.btnPerfectScaleWidthFix.UseVisualStyleBackColor = True
        '
        'btnChangeLEDColor
        '
        resources.ApplyResources(Me.btnChangeLEDColor, "btnChangeLEDColor")
        Me.btnChangeLEDColor.Name = "btnChangeLEDColor"
        Me.btnChangeLEDColor.UseVisualStyleBackColor = True
        '
        'B2SLine1
        '
        resources.ApplyResources(Me.B2SLine1, "B2SLine1")
        Me.B2SLine1.Name = "B2SLine1"
        '
        'B2SLine2
        '
        resources.ApplyResources(Me.B2SLine2, "B2SLine2")
        Me.B2SLine2.Name = "B2SLine2"
        '
        'lblLocation
        '
        resources.ApplyResources(Me.lblLocation, "lblLocation")
        Me.lblLocation.Name = "lblLocation"
        '
        'lblSize
        '
        resources.ApplyResources(Me.lblSize, "lblSize")
        Me.lblSize.Name = "lblSize"
        '
        'lblSpacing
        '
        resources.ApplyResources(Me.lblSpacing, "lblSpacing")
        Me.lblSpacing.Name = "lblSpacing"
        '
        'numericSpacing
        '
        resources.ApplyResources(Me.numericSpacing, "numericSpacing")
        Me.numericSpacing.Name = "numericSpacing"
        '
        'B2SLine3
        '
        resources.ApplyResources(Me.B2SLine3, "B2SLine3")
        Me.B2SLine3.Name = "B2SLine3"
        '
        'numericDigits
        '
        resources.ApplyResources(Me.numericDigits, "numericDigits")
        Me.numericDigits.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numericDigits.Name = "numericDigits"
        '
        'txtID
        '
        resources.ApplyResources(Me.txtID, "txtID")
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        '
        'lblID
        '
        resources.ApplyResources(Me.lblID, "lblID")
        Me.lblID.Name = "lblID"
        '
        'chkDream7
        '
        resources.ApplyResources(Me.chkDream7, "chkDream7")
        Me.chkDream7.Name = "chkDream7"
        Me.chkDream7.UseVisualStyleBackColor = True
        '
        'txtLocationY
        '
        resources.ApplyResources(Me.txtLocationY, "txtLocationY")
        Me.txtLocationY.Name = "txtLocationY"
        '
        'txtLocationX
        '
        resources.ApplyResources(Me.txtLocationX, "txtLocationX")
        Me.txtLocationX.Name = "txtLocationX"
        '
        'txtSizeHeight
        '
        resources.ApplyResources(Me.txtSizeHeight, "txtSizeHeight")
        Me.txtSizeHeight.Name = "txtSizeHeight"
        '
        'txtSizeWidth
        '
        resources.ApplyResources(Me.txtSizeWidth, "txtSizeWidth")
        Me.txtSizeWidth.Name = "txtSizeWidth"
        '
        'cmbB2SScoreType
        '
        resources.ApplyResources(Me.cmbB2SScoreType, "cmbB2SScoreType")
        Me.cmbB2SScoreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbB2SScoreType.FormattingEnabled = True
        Me.cmbB2SScoreType.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbB2SScoreType.Items"), resources.GetString("cmbB2SScoreType.Items1")})
        Me.cmbB2SScoreType.Name = "cmbB2SScoreType"
        '
        'lblB2SScoreType
        '
        resources.ApplyResources(Me.lblB2SScoreType, "lblB2SScoreType")
        Me.lblB2SScoreType.Name = "lblB2SScoreType"
        '
        'lblB2SID
        '
        resources.ApplyResources(Me.lblB2SID, "lblB2SID")
        Me.lblB2SID.Name = "lblB2SID"
        '
        'B2SLine4
        '
        resources.ApplyResources(Me.B2SLine4, "B2SLine4")
        Me.B2SLine4.Name = "B2SLine4"
        '
        'txtB2SStartID
        '
        resources.ApplyResources(Me.txtB2SStartID, "txtB2SStartID")
        Me.txtB2SStartID.Name = "txtB2SStartID"
        '
        'B2SLine5
        '
        resources.ApplyResources(Me.B2SLine5, "B2SLine5")
        Me.B2SLine5.Name = "B2SLine5"
        '
        'btnReelIllumination
        '
        resources.ApplyResources(Me.btnReelIllumination, "btnReelIllumination")
        Me.btnReelIllumination.Name = "btnReelIllumination"
        Me.btnReelIllumination.UseVisualStyleBackColor = True
        '
        'cmbB2SPlayerNo
        '
        resources.ApplyResources(Me.cmbB2SPlayerNo, "cmbB2SPlayerNo")
        Me.cmbB2SPlayerNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbB2SPlayerNo.FormattingEnabled = True
        Me.cmbB2SPlayerNo.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbB2SPlayerNo.Items"), resources.GetString("cmbB2SPlayerNo.Items1"), resources.GetString("cmbB2SPlayerNo.Items2"), resources.GetString("cmbB2SPlayerNo.Items3"), resources.GetString("cmbB2SPlayerNo.Items4"), resources.GetString("cmbB2SPlayerNo.Items5")})
        Me.cmbB2SPlayerNo.Name = "cmbB2SPlayerNo"
        '
        'lblB2SPlayerNo
        '
        resources.ApplyResources(Me.lblB2SPlayerNo, "lblB2SPlayerNo")
        Me.lblB2SPlayerNo.Name = "lblB2SPlayerNo"
        '
        'cmbInitState
        '
        resources.ApplyResources(Me.cmbInitState, "cmbInitState")
        Me.cmbInitState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInitState.FormattingEnabled = True
        Me.cmbInitState.Items.AddRange(New Object() {resources.GetString("cmbInitState.Items"), resources.GetString("cmbInitState.Items1")})
        Me.cmbInitState.Name = "cmbInitState"
        '
        'lblInitState
        '
        resources.ApplyResources(Me.lblInitState, "lblInitState")
        Me.lblInitState.Name = "lblInitState"
        '
        'formToolReelsAndLEDs
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Controls.Add(Me.cmbInitState)
        Me.Controls.Add(Me.lblInitState)
        Me.Controls.Add(Me.cmbB2SPlayerNo)
        Me.Controls.Add(Me.lblB2SPlayerNo)
        Me.Controls.Add(Me.btnReelIllumination)
        Me.Controls.Add(Me.B2SLine5)
        Me.Controls.Add(Me.txtB2SStartID)
        Me.Controls.Add(Me.B2SLine4)
        Me.Controls.Add(Me.lblB2SID)
        Me.Controls.Add(Me.cmbB2SScoreType)
        Me.Controls.Add(Me.lblB2SScoreType)
        Me.Controls.Add(Me.txtSizeHeight)
        Me.Controls.Add(Me.txtSizeWidth)
        Me.Controls.Add(Me.txtLocationY)
        Me.Controls.Add(Me.txtLocationX)
        Me.Controls.Add(Me.chkDream7)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.numericDigits)
        Me.Controls.Add(Me.B2SLine3)
        Me.Controls.Add(Me.numericSpacing)
        Me.Controls.Add(Me.lblSpacing)
        Me.Controls.Add(Me.lblSize)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.B2SLine2)
        Me.Controls.Add(Me.B2SLine1)
        Me.Controls.Add(Me.btnChangeLEDColor)
        Me.Controls.Add(Me.btnPerfectScaleWidthFix)
        Me.Controls.Add(Me.lblNumberOfPlayers)
        Me.Controls.Add(Me.cmbNumberOfPlayers)
        Me.Controls.Add(Me.lblDigits)
        Me.Controls.Add(Me.lvReelsAndLEDs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formToolReelsAndLEDs"
        Me.ShowInTaskbar = False
        CType(Me.numericSpacing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericDigits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvReelsAndLEDs As System.Windows.Forms.ListView
    Friend WithEvents ilReelsAndLEDs As System.Windows.Forms.ImageList
    Friend WithEvents lblNumberOfPlayers As System.Windows.Forms.Label
    Friend WithEvents cmbNumberOfPlayers As System.Windows.Forms.ComboBox
    Friend WithEvents lblDigits As System.Windows.Forms.Label
    Friend WithEvents btnPerfectScaleWidthFix As System.Windows.Forms.Button
    Friend WithEvents btnChangeLEDColor As System.Windows.Forms.Button
    Friend WithEvents B2SLine1 As B2SBackglassDesigner.B2SLine
    Friend WithEvents B2SLine2 As B2SBackglassDesigner.B2SLine
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents lblSpacing As System.Windows.Forms.Label
    Friend WithEvents numericSpacing As System.Windows.Forms.NumericUpDown
    Friend WithEvents B2SLine3 As B2SBackglassDesigner.B2SLine
    Friend WithEvents numericDigits As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents chkDream7 As System.Windows.Forms.CheckBox
    Friend WithEvents txtLocationY As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationX As System.Windows.Forms.TextBox
    Friend WithEvents txtSizeHeight As System.Windows.Forms.TextBox
    Friend WithEvents txtSizeWidth As System.Windows.Forms.TextBox
    Friend WithEvents cmbB2SScoreType As System.Windows.Forms.ComboBox
    Friend WithEvents lblB2SScoreType As System.Windows.Forms.Label
    Friend WithEvents lblB2SID As System.Windows.Forms.Label
    Friend WithEvents B2SLine4 As B2SBackglassDesigner.B2SLine
    Friend WithEvents txtB2SStartID As System.Windows.Forms.TextBox
    Friend WithEvents B2SLine5 As B2SBackglassDesigner.B2SLine
    Friend WithEvents btnReelIllumination As System.Windows.Forms.Button
    Friend WithEvents cmbB2SPlayerNo As System.Windows.Forms.ComboBox
    Friend WithEvents lblB2SPlayerNo As System.Windows.Forms.Label
    Friend WithEvents cmbInitState As System.Windows.Forms.ComboBox
    Friend WithEvents lblInitState As System.Windows.Forms.Label
End Class
