<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formToolIllumination
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formToolIllumination))
        Me.lblSize = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.lblIlluminationText = New System.Windows.Forms.Label()
        Me.txtIlluminationText = New System.Windows.Forms.TextBox()
        Me.B2SLine1 = New B2SBackglassDesigner.B2SLine()
        Me.btnFonts = New System.Windows.Forms.Button()
        Me.B2SLine2 = New B2SBackglassDesigner.B2SLine()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.lblInitState = New System.Windows.Forms.Label()
        Me.cmbInitState = New System.Windows.Forms.ComboBox()
        Me.TrackBarIntensity = New System.Windows.Forms.TrackBar()
        Me.lblIntensity = New System.Windows.Forms.Label()
        Me.B2SLine3 = New B2SBackglassDesigner.B2SLine()
        Me.txtRomID = New System.Windows.Forms.TextBox()
        Me.lblRomID = New System.Windows.Forms.Label()
        Me.cmbROMIDType = New System.Windows.Forms.ComboBox()
        Me.lblRomIDType = New System.Windows.Forms.Label()
        Me.lblDodgeColor = New System.Windows.Forms.Label()
        Me.cmbDodgeColor = New System.Windows.Forms.ComboBox()
        Me.txtB2SID = New System.Windows.Forms.TextBox()
        Me.lblB2SID = New System.Windows.Forms.Label()
        Me.chkRomInverted = New System.Windows.Forms.CheckBox()
        Me.txtSizeHeight = New System.Windows.Forms.TextBox()
        Me.txtSizeWidth = New System.Windows.Forms.TextBox()
        Me.txtLocationY = New System.Windows.Forms.TextBox()
        Me.txtLocationX = New System.Windows.Forms.TextBox()
        Me.rbAlignLeft = New System.Windows.Forms.RadioButton()
        Me.rbAlignCenter = New System.Windows.Forms.RadioButton()
        Me.rbAlignRight = New System.Windows.Forms.RadioButton()
        Me.B2SLine4 = New B2SBackglassDesigner.B2SLine()
        Me.btnSnippitSettings = New System.Windows.Forms.Button()
        Me.cmbB2SIDType = New System.Windows.Forms.ComboBox()
        Me.lblB2SIDType = New System.Windows.Forms.Label()
        Me.txtB2SValue = New System.Windows.Forms.TextBox()
        Me.lblB2SValue = New System.Windows.Forms.Label()
        Me.B2SLine5 = New B2SBackglassDesigner.B2SLine()
        Me.btnLightColor = New System.Windows.Forms.Button()
        Me.cmbDualMode = New System.Windows.Forms.ComboBox()
        Me.lblDualMode = New System.Windows.Forms.Label()
        Me.cmbIlluMode = New System.Windows.Forms.ComboBox()
        Me.lblIlluMode = New System.Windows.Forms.Label()
        CType(Me.TrackBarIntensity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSize
        '
        resources.ApplyResources(Me.lblSize, "lblSize")
        Me.lblSize.Name = "lblSize"
        '
        'lblLocation
        '
        resources.ApplyResources(Me.lblLocation, "lblLocation")
        Me.lblLocation.Name = "lblLocation"
        '
        'lblIlluminationText
        '
        resources.ApplyResources(Me.lblIlluminationText, "lblIlluminationText")
        Me.lblIlluminationText.Name = "lblIlluminationText"
        '
        'txtIlluminationText
        '
        resources.ApplyResources(Me.txtIlluminationText, "txtIlluminationText")
        Me.txtIlluminationText.Name = "txtIlluminationText"
        '
        'B2SLine1
        '
        resources.ApplyResources(Me.B2SLine1, "B2SLine1")
        Me.B2SLine1.Name = "B2SLine1"
        '
        'btnFonts
        '
        resources.ApplyResources(Me.btnFonts, "btnFonts")
        Me.btnFonts.Name = "btnFonts"
        Me.btnFonts.UseVisualStyleBackColor = True
        '
        'B2SLine2
        '
        resources.ApplyResources(Me.B2SLine2, "B2SLine2")
        Me.B2SLine2.Name = "B2SLine2"
        '
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.Name = "lblName"
        '
        'lblID
        '
        resources.ApplyResources(Me.lblID, "lblID")
        Me.lblID.Name = "lblID"
        '
        'txtName
        '
        resources.ApplyResources(Me.txtName, "txtName")
        Me.txtName.Name = "txtName"
        '
        'txtID
        '
        resources.ApplyResources(Me.txtID, "txtID")
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        '
        'lblInitState
        '
        resources.ApplyResources(Me.lblInitState, "lblInitState")
        Me.lblInitState.Name = "lblInitState"
        '
        'cmbInitState
        '
        resources.ApplyResources(Me.cmbInitState, "cmbInitState")
        Me.cmbInitState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInitState.FormattingEnabled = True
        Me.cmbInitState.Items.AddRange(New Object() {resources.GetString("cmbInitState.Items"), resources.GetString("cmbInitState.Items1"), resources.GetString("cmbInitState.Items2")})
        Me.cmbInitState.Name = "cmbInitState"
        '
        'TrackBarIntensity
        '
        resources.ApplyResources(Me.TrackBarIntensity, "TrackBarIntensity")
        Me.TrackBarIntensity.LargeChange = 2
        Me.TrackBarIntensity.Maximum = 5
        Me.TrackBarIntensity.Minimum = 1
        Me.TrackBarIntensity.Name = "TrackBarIntensity"
        Me.TrackBarIntensity.Value = 1
        '
        'lblIntensity
        '
        resources.ApplyResources(Me.lblIntensity, "lblIntensity")
        Me.lblIntensity.Name = "lblIntensity"
        '
        'B2SLine3
        '
        resources.ApplyResources(Me.B2SLine3, "B2SLine3")
        Me.B2SLine3.Name = "B2SLine3"
        '
        'txtRomID
        '
        resources.ApplyResources(Me.txtRomID, "txtRomID")
        Me.txtRomID.Name = "txtRomID"
        '
        'lblRomID
        '
        resources.ApplyResources(Me.lblRomID, "lblRomID")
        Me.lblRomID.Name = "lblRomID"
        '
        'cmbROMIDType
        '
        resources.ApplyResources(Me.cmbROMIDType, "cmbROMIDType")
        Me.cmbROMIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbROMIDType.FormattingEnabled = True
        Me.cmbROMIDType.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbROMIDType.Items"), resources.GetString("cmbROMIDType.Items1"), resources.GetString("cmbROMIDType.Items2")})
        Me.cmbROMIDType.Name = "cmbROMIDType"
        '
        'lblRomIDType
        '
        resources.ApplyResources(Me.lblRomIDType, "lblRomIDType")
        Me.lblRomIDType.Name = "lblRomIDType"
        '
        'lblDodgeColor
        '
        resources.ApplyResources(Me.lblDodgeColor, "lblDodgeColor")
        Me.lblDodgeColor.Name = "lblDodgeColor"
        '
        'cmbDodgeColor
        '
        resources.ApplyResources(Me.cmbDodgeColor, "cmbDodgeColor")
        Me.cmbDodgeColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDodgeColor.FormattingEnabled = True
        Me.cmbDodgeColor.Items.AddRange(New Object() {resources.GetString("cmbDodgeColor.Items"), resources.GetString("cmbDodgeColor.Items1"), resources.GetString("cmbDodgeColor.Items2"), resources.GetString("cmbDodgeColor.Items3"), resources.GetString("cmbDodgeColor.Items4"), resources.GetString("cmbDodgeColor.Items5"), resources.GetString("cmbDodgeColor.Items6"), resources.GetString("cmbDodgeColor.Items7")})
        Me.cmbDodgeColor.Name = "cmbDodgeColor"
        '
        'txtB2SID
        '
        resources.ApplyResources(Me.txtB2SID, "txtB2SID")
        Me.txtB2SID.Name = "txtB2SID"
        '
        'lblB2SID
        '
        resources.ApplyResources(Me.lblB2SID, "lblB2SID")
        Me.lblB2SID.Name = "lblB2SID"
        '
        'chkRomInverted
        '
        resources.ApplyResources(Me.chkRomInverted, "chkRomInverted")
        Me.chkRomInverted.Name = "chkRomInverted"
        Me.chkRomInverted.UseVisualStyleBackColor = True
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
        'rbAlignLeft
        '
        resources.ApplyResources(Me.rbAlignLeft, "rbAlignLeft")
        Me.rbAlignLeft.Image = Global.B2SBackglassDesigner.My.Resources.Resources.AdjustLeft
        Me.rbAlignLeft.Name = "rbAlignLeft"
        Me.rbAlignLeft.TabStop = True
        Me.rbAlignLeft.UseVisualStyleBackColor = True
        '
        'rbAlignCenter
        '
        resources.ApplyResources(Me.rbAlignCenter, "rbAlignCenter")
        Me.rbAlignCenter.Image = Global.B2SBackglassDesigner.My.Resources.Resources.AdjustCenter
        Me.rbAlignCenter.Name = "rbAlignCenter"
        Me.rbAlignCenter.TabStop = True
        Me.rbAlignCenter.UseVisualStyleBackColor = True
        '
        'rbAlignRight
        '
        resources.ApplyResources(Me.rbAlignRight, "rbAlignRight")
        Me.rbAlignRight.Image = Global.B2SBackglassDesigner.My.Resources.Resources.AdjustRight
        Me.rbAlignRight.Name = "rbAlignRight"
        Me.rbAlignRight.TabStop = True
        Me.rbAlignRight.UseVisualStyleBackColor = True
        '
        'B2SLine4
        '
        resources.ApplyResources(Me.B2SLine4, "B2SLine4")
        Me.B2SLine4.Name = "B2SLine4"
        '
        'btnSnippitSettings
        '
        resources.ApplyResources(Me.btnSnippitSettings, "btnSnippitSettings")
        Me.btnSnippitSettings.Name = "btnSnippitSettings"
        Me.btnSnippitSettings.UseVisualStyleBackColor = True
        '
        'cmbB2SIDType
        '
        resources.ApplyResources(Me.cmbB2SIDType, "cmbB2SIDType")
        Me.cmbB2SIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbB2SIDType.FormattingEnabled = True
        Me.cmbB2SIDType.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbB2SIDType.Items"), resources.GetString("cmbB2SIDType.Items1"), resources.GetString("cmbB2SIDType.Items2"), resources.GetString("cmbB2SIDType.Items3"), resources.GetString("cmbB2SIDType.Items4"), resources.GetString("cmbB2SIDType.Items5"), resources.GetString("cmbB2SIDType.Items6"), resources.GetString("cmbB2SIDType.Items7"), resources.GetString("cmbB2SIDType.Items8"), resources.GetString("cmbB2SIDType.Items9"), resources.GetString("cmbB2SIDType.Items10")})
        Me.cmbB2SIDType.Name = "cmbB2SIDType"
        '
        'lblB2SIDType
        '
        resources.ApplyResources(Me.lblB2SIDType, "lblB2SIDType")
        Me.lblB2SIDType.Name = "lblB2SIDType"
        '
        'txtB2SValue
        '
        resources.ApplyResources(Me.txtB2SValue, "txtB2SValue")
        Me.txtB2SValue.Name = "txtB2SValue"
        '
        'lblB2SValue
        '
        resources.ApplyResources(Me.lblB2SValue, "lblB2SValue")
        Me.lblB2SValue.Name = "lblB2SValue"
        '
        'B2SLine5
        '
        resources.ApplyResources(Me.B2SLine5, "B2SLine5")
        Me.B2SLine5.Name = "B2SLine5"
        '
        'btnLightColor
        '
        resources.ApplyResources(Me.btnLightColor, "btnLightColor")
        Me.btnLightColor.Name = "btnLightColor"
        Me.btnLightColor.UseVisualStyleBackColor = True
        '
        'cmbDualMode
        '
        resources.ApplyResources(Me.cmbDualMode, "cmbDualMode")
        Me.cmbDualMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDualMode.FormattingEnabled = True
        Me.cmbDualMode.Items.AddRange(New Object() {resources.GetString("cmbDualMode.Items"), resources.GetString("cmbDualMode.Items1"), resources.GetString("cmbDualMode.Items2")})
        Me.cmbDualMode.Name = "cmbDualMode"
        '
        'lblDualMode
        '
        resources.ApplyResources(Me.lblDualMode, "lblDualMode")
        Me.lblDualMode.Name = "lblDualMode"
        '
        'cmbIlluMode
        '
        resources.ApplyResources(Me.cmbIlluMode, "cmbIlluMode")
        Me.cmbIlluMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIlluMode.FormattingEnabled = True
        Me.cmbIlluMode.Items.AddRange(New Object() {resources.GetString("cmbIlluMode.Items"), resources.GetString("cmbIlluMode.Items1")})
        Me.cmbIlluMode.Name = "cmbIlluMode"
        '
        'lblIlluMode
        '
        resources.ApplyResources(Me.lblIlluMode, "lblIlluMode")
        Me.lblIlluMode.Name = "lblIlluMode"
        '
        'formToolIllumination
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Controls.Add(Me.cmbIlluMode)
        Me.Controls.Add(Me.lblIlluMode)
        Me.Controls.Add(Me.cmbDualMode)
        Me.Controls.Add(Me.lblDualMode)
        Me.Controls.Add(Me.btnLightColor)
        Me.Controls.Add(Me.B2SLine5)
        Me.Controls.Add(Me.btnSnippitSettings)
        Me.Controls.Add(Me.B2SLine4)
        Me.Controls.Add(Me.rbAlignRight)
        Me.Controls.Add(Me.rbAlignCenter)
        Me.Controls.Add(Me.rbAlignLeft)
        Me.Controls.Add(Me.txtSizeHeight)
        Me.Controls.Add(Me.txtSizeWidth)
        Me.Controls.Add(Me.txtLocationY)
        Me.Controls.Add(Me.txtLocationX)
        Me.Controls.Add(Me.chkRomInverted)
        Me.Controls.Add(Me.cmbDodgeColor)
        Me.Controls.Add(Me.lblDodgeColor)
        Me.Controls.Add(Me.lblRomIDType)
        Me.Controls.Add(Me.lblRomID)
        Me.Controls.Add(Me.B2SLine3)
        Me.Controls.Add(Me.lblIntensity)
        Me.Controls.Add(Me.txtIlluminationText)
        Me.Controls.Add(Me.TrackBarIntensity)
        Me.Controls.Add(Me.cmbInitState)
        Me.Controls.Add(Me.lblInitState)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.btnFonts)
        Me.Controls.Add(Me.B2SLine1)
        Me.Controls.Add(Me.lblIlluminationText)
        Me.Controls.Add(Me.lblSize)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.lblB2SID)
        Me.Controls.Add(Me.lblB2SIDType)
        Me.Controls.Add(Me.txtB2SValue)
        Me.Controls.Add(Me.lblB2SValue)
        Me.Controls.Add(Me.B2SLine2)
        Me.Controls.Add(Me.txtRomID)
        Me.Controls.Add(Me.cmbB2SIDType)
        Me.Controls.Add(Me.cmbROMIDType)
        Me.Controls.Add(Me.txtB2SID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formToolIllumination"
        Me.ShowInTaskbar = False
        CType(Me.TrackBarIntensity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents lblIlluminationText As System.Windows.Forms.Label
    Friend WithEvents txtIlluminationText As System.Windows.Forms.TextBox
    Friend WithEvents B2SLine1 As B2SBackglassDesigner.B2SLine
    Friend WithEvents btnFonts As System.Windows.Forms.Button
    Friend WithEvents B2SLine2 As B2SBackglassDesigner.B2SLine
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents lblInitState As System.Windows.Forms.Label
    Friend WithEvents cmbInitState As System.Windows.Forms.ComboBox
    Friend WithEvents TrackBarIntensity As System.Windows.Forms.TrackBar
    Friend WithEvents lblIntensity As System.Windows.Forms.Label
    Friend WithEvents B2SLine3 As B2SBackglassDesigner.B2SLine
    Friend WithEvents txtRomID As System.Windows.Forms.TextBox
    Friend WithEvents lblRomID As System.Windows.Forms.Label
    Friend WithEvents cmbROMIDType As System.Windows.Forms.ComboBox
    Friend WithEvents lblRomIDType As System.Windows.Forms.Label
    Friend WithEvents lblDodgeColor As System.Windows.Forms.Label
    Friend WithEvents cmbDodgeColor As System.Windows.Forms.ComboBox
    Friend WithEvents txtB2SID As System.Windows.Forms.TextBox
    Friend WithEvents lblB2SID As System.Windows.Forms.Label
    Friend WithEvents chkRomInverted As System.Windows.Forms.CheckBox
    Friend WithEvents txtSizeHeight As System.Windows.Forms.TextBox
    Friend WithEvents txtSizeWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationY As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationX As System.Windows.Forms.TextBox
    Friend WithEvents rbAlignLeft As System.Windows.Forms.RadioButton
    Friend WithEvents rbAlignCenter As System.Windows.Forms.RadioButton
    Friend WithEvents rbAlignRight As System.Windows.Forms.RadioButton
    Friend WithEvents B2SLine4 As B2SBackglassDesigner.B2SLine
    Friend WithEvents btnSnippitSettings As System.Windows.Forms.Button
    Friend WithEvents cmbB2SIDType As System.Windows.Forms.ComboBox
    Friend WithEvents lblB2SIDType As System.Windows.Forms.Label
    Friend WithEvents txtB2SValue As System.Windows.Forms.TextBox
    Friend WithEvents lblB2SValue As System.Windows.Forms.Label
    Friend WithEvents B2SLine5 As B2SBackglassDesigner.B2SLine
    Friend WithEvents btnLightColor As System.Windows.Forms.Button
    Friend WithEvents cmbDualMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblDualMode As System.Windows.Forms.Label
    Friend WithEvents cmbIlluMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblIlluMode As System.Windows.Forms.Label
End Class
