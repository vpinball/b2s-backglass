<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formSetReelIllumination
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formSetReelIllumination))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.grpGeneral = New System.Windows.Forms.GroupBox()
        Me.B2SLine5 = New B2SBackglassDesigner.B2SLine()
        Me.B2SLine2 = New B2SBackglassDesigner.B2SLine()
        Me.lblIntensity = New System.Windows.Forms.Label()
        Me.TrackBarIntensity = New System.Windows.Forms.TrackBar()
        Me.cmbIlluLocation = New System.Windows.Forms.ComboBox()
        Me.lblIlluLocation = New System.Windows.Forms.Label()
        Me.lblB2SID = New System.Windows.Forms.Label()
        Me.lblB2SIDType = New System.Windows.Forms.Label()
        Me.cmbB2SIDType = New System.Windows.Forms.ComboBox()
        Me.txtB2SValue = New System.Windows.Forms.TextBox()
        Me.lblB2SValue = New System.Windows.Forms.Label()
        Me.txtB2SID = New System.Windows.Forms.TextBox()
        Me.grpGeneral.SuspendLayout()
        CType(Me.TrackBarIntensity, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'grpGeneral
        '
        resources.ApplyResources(Me.grpGeneral, "grpGeneral")
        Me.grpGeneral.Controls.Add(Me.B2SLine5)
        Me.grpGeneral.Controls.Add(Me.B2SLine2)
        Me.grpGeneral.Controls.Add(Me.lblIntensity)
        Me.grpGeneral.Controls.Add(Me.TrackBarIntensity)
        Me.grpGeneral.Controls.Add(Me.cmbIlluLocation)
        Me.grpGeneral.Controls.Add(Me.lblIlluLocation)
        Me.grpGeneral.Controls.Add(Me.lblB2SID)
        Me.grpGeneral.Controls.Add(Me.lblB2SIDType)
        Me.grpGeneral.Controls.Add(Me.cmbB2SIDType)
        Me.grpGeneral.Controls.Add(Me.txtB2SValue)
        Me.grpGeneral.Controls.Add(Me.lblB2SValue)
        Me.grpGeneral.Controls.Add(Me.txtB2SID)
        Me.grpGeneral.Name = "grpGeneral"
        Me.grpGeneral.TabStop = False
        '
        'B2SLine5
        '
        resources.ApplyResources(Me.B2SLine5, "B2SLine5")
        Me.B2SLine5.Name = "B2SLine5"
        '
        'B2SLine2
        '
        resources.ApplyResources(Me.B2SLine2, "B2SLine2")
        Me.B2SLine2.Name = "B2SLine2"
        '
        'lblIntensity
        '
        resources.ApplyResources(Me.lblIntensity, "lblIntensity")
        Me.lblIntensity.Name = "lblIntensity"
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
        'cmbIlluLocation
        '
        resources.ApplyResources(Me.cmbIlluLocation, "cmbIlluLocation")
        Me.cmbIlluLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIlluLocation.FormattingEnabled = True
        Me.cmbIlluLocation.Items.AddRange(New Object() {resources.GetString("cmbIlluLocation.Items"), resources.GetString("cmbIlluLocation.Items1"), resources.GetString("cmbIlluLocation.Items2"), resources.GetString("cmbIlluLocation.Items3")})
        Me.cmbIlluLocation.Name = "cmbIlluLocation"
        '
        'lblIlluLocation
        '
        resources.ApplyResources(Me.lblIlluLocation, "lblIlluLocation")
        Me.lblIlluLocation.Name = "lblIlluLocation"
        '
        'lblB2SID
        '
        resources.ApplyResources(Me.lblB2SID, "lblB2SID")
        Me.lblB2SID.Name = "lblB2SID"
        '
        'lblB2SIDType
        '
        resources.ApplyResources(Me.lblB2SIDType, "lblB2SIDType")
        Me.lblB2SIDType.Name = "lblB2SIDType"
        '
        'cmbB2SIDType
        '
        resources.ApplyResources(Me.cmbB2SIDType, "cmbB2SIDType")
        Me.cmbB2SIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbB2SIDType.FormattingEnabled = True
        Me.cmbB2SIDType.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbB2SIDType.Items"), resources.GetString("cmbB2SIDType.Items1"), resources.GetString("cmbB2SIDType.Items2"), resources.GetString("cmbB2SIDType.Items3"), resources.GetString("cmbB2SIDType.Items4"), resources.GetString("cmbB2SIDType.Items5"), resources.GetString("cmbB2SIDType.Items6"), resources.GetString("cmbB2SIDType.Items7"), resources.GetString("cmbB2SIDType.Items8"), resources.GetString("cmbB2SIDType.Items9"), resources.GetString("cmbB2SIDType.Items10")})
        Me.cmbB2SIDType.Name = "cmbB2SIDType"
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
        'txtB2SID
        '
        resources.ApplyResources(Me.txtB2SID, "txtB2SID")
        Me.txtB2SID.Name = "txtB2SID"
        '
        'formSetReelIllumination
        '
        Me.AcceptButton = Me.btnOk
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.grpGeneral)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formSetReelIllumination"
        Me.grpGeneral.ResumeLayout(False)
        Me.grpGeneral.PerformLayout()
        CType(Me.TrackBarIntensity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents grpGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents B2SLine5 As B2SBackglassDesigner.B2SLine
    Friend WithEvents B2SLine2 As B2SBackglassDesigner.B2SLine
    Friend WithEvents lblIntensity As System.Windows.Forms.Label
    Friend WithEvents TrackBarIntensity As System.Windows.Forms.TrackBar
    Friend WithEvents cmbIlluLocation As System.Windows.Forms.ComboBox
    Friend WithEvents lblIlluLocation As System.Windows.Forms.Label
    Friend WithEvents lblB2SID As System.Windows.Forms.Label
    Friend WithEvents lblB2SIDType As System.Windows.Forms.Label
    Friend WithEvents cmbB2SIDType As System.Windows.Forms.ComboBox
    Friend WithEvents txtB2SValue As System.Windows.Forms.TextBox
    Friend WithEvents lblB2SValue As System.Windows.Forms.Label
    Friend WithEvents txtB2SID As System.Windows.Forms.TextBox
End Class
