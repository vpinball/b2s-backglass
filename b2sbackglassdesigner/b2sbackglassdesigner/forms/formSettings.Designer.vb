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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formSettings))
        Me.groupGeneral = New System.Windows.Forms.GroupBox()
        Me.btnFileName = New System.Windows.Forms.Button()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblTableType = New System.Windows.Forms.Label()
        Me.cmbTableType = New System.Windows.Forms.ComboBox()
        Me.chkCreateEMDefaults = New System.Windows.Forms.CheckBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.groupTable = New System.Windows.Forms.GroupBox()
        Me.lblNumberOfPlayers = New System.Windows.Forms.Label()
        Me.cmbNumberOfPlayers = New System.Windows.Forms.ComboBox()
        Me.groupB2S = New System.Windows.Forms.GroupBox()
        Me.chkDualBackglass = New System.Windows.Forms.CheckBox()
        Me.lblDestType = New System.Windows.Forms.Label()
        Me.cmbDestType = New System.Windows.Forms.ComboBox()
        Me.lblCommMode = New System.Windows.Forms.Label()
        Me.cmbCommMode = New System.Windows.Forms.ComboBox()
        Me.lblDMDLocation = New System.Windows.Forms.Label()
        Me.cmbDMDLocation = New System.Windows.Forms.ComboBox()
        Me.lblB2SDataCount = New System.Windows.Forms.Label()
        Me.cmbB2SDataCount = New System.Windows.Forms.ComboBox()
        Me.grpBackground = New System.Windows.Forms.GroupBox()
        Me.txtArtwork = New System.Windows.Forms.TextBox()
        Me.lblArtwork = New System.Windows.Forms.Label()
        Me.txtAuthor = New System.Windows.Forms.TextBox()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.groupGeneral.SuspendLayout()
        Me.groupTable.SuspendLayout()
        Me.groupB2S.SuspendLayout()
        Me.grpBackground.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupGeneral
        '
        resources.ApplyResources(Me.groupGeneral, "groupGeneral")
        Me.groupGeneral.Controls.Add(Me.btnFileName)
        Me.groupGeneral.Controls.Add(Me.lblFileName)
        Me.groupGeneral.Controls.Add(Me.txtFileName)
        Me.groupGeneral.Controls.Add(Me.lblName)
        Me.groupGeneral.Controls.Add(Me.txtName)
        Me.groupGeneral.Controls.Add(Me.lblTableType)
        Me.groupGeneral.Controls.Add(Me.cmbTableType)
        Me.groupGeneral.Name = "groupGeneral"
        Me.groupGeneral.TabStop = False
        '
        'btnFileName
        '
        resources.ApplyResources(Me.btnFileName, "btnFileName")
        Me.btnFileName.Name = "btnFileName"
        Me.btnFileName.UseVisualStyleBackColor = True
        '
        'lblFileName
        '
        resources.ApplyResources(Me.lblFileName, "lblFileName")
        Me.lblFileName.Name = "lblFileName"
        '
        'txtFileName
        '
        resources.ApplyResources(Me.txtFileName, "txtFileName")
        Me.txtFileName.Name = "txtFileName"
        '
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.Name = "lblName"
        '
        'txtName
        '
        resources.ApplyResources(Me.txtName, "txtName")
        Me.txtName.Name = "txtName"
        '
        'lblTableType
        '
        resources.ApplyResources(Me.lblTableType, "lblTableType")
        Me.lblTableType.Name = "lblTableType"
        '
        'cmbTableType
        '
        resources.ApplyResources(Me.cmbTableType, "cmbTableType")
        Me.cmbTableType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTableType.FormattingEnabled = True
        Me.cmbTableType.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbTableType.Items"), resources.GetString("cmbTableType.Items1"), resources.GetString("cmbTableType.Items2"), resources.GetString("cmbTableType.Items3")})
        Me.cmbTableType.Name = "cmbTableType"
        '
        'chkCreateEMDefaults
        '
        resources.ApplyResources(Me.chkCreateEMDefaults, "chkCreateEMDefaults")
        Me.chkCreateEMDefaults.Name = "chkCreateEMDefaults"
        Me.chkCreateEMDefaults.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        resources.ApplyResources(Me.btnOk, "btnOk")
        Me.btnOk.Name = "btnOk"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'groupTable
        '
        resources.ApplyResources(Me.groupTable, "groupTable")
        Me.groupTable.Controls.Add(Me.lblNumberOfPlayers)
        Me.groupTable.Controls.Add(Me.cmbNumberOfPlayers)
        Me.groupTable.Name = "groupTable"
        Me.groupTable.TabStop = False
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
        'groupB2S
        '
        resources.ApplyResources(Me.groupB2S, "groupB2S")
        Me.groupB2S.Controls.Add(Me.chkDualBackglass)
        Me.groupB2S.Controls.Add(Me.lblDestType)
        Me.groupB2S.Controls.Add(Me.cmbDestType)
        Me.groupB2S.Controls.Add(Me.chkCreateEMDefaults)
        Me.groupB2S.Controls.Add(Me.lblCommMode)
        Me.groupB2S.Controls.Add(Me.cmbCommMode)
        Me.groupB2S.Controls.Add(Me.lblDMDLocation)
        Me.groupB2S.Controls.Add(Me.cmbDMDLocation)
        Me.groupB2S.Controls.Add(Me.lblB2SDataCount)
        Me.groupB2S.Controls.Add(Me.cmbB2SDataCount)
        Me.groupB2S.Name = "groupB2S"
        Me.groupB2S.TabStop = False
        '
        'chkDualBackglass
        '
        resources.ApplyResources(Me.chkDualBackglass, "chkDualBackglass")
        Me.chkDualBackglass.Name = "chkDualBackglass"
        Me.chkDualBackglass.UseVisualStyleBackColor = True
        '
        'lblDestType
        '
        resources.ApplyResources(Me.lblDestType, "lblDestType")
        Me.lblDestType.Name = "lblDestType"
        '
        'cmbDestType
        '
        resources.ApplyResources(Me.cmbDestType, "cmbDestType")
        Me.cmbDestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDestType.FormattingEnabled = True
        Me.cmbDestType.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbDestType.Items"), resources.GetString("cmbDestType.Items1")})
        Me.cmbDestType.Name = "cmbDestType"
        '
        'lblCommMode
        '
        resources.ApplyResources(Me.lblCommMode, "lblCommMode")
        Me.lblCommMode.Name = "lblCommMode"
        '
        'cmbCommMode
        '
        resources.ApplyResources(Me.cmbCommMode, "cmbCommMode")
        Me.cmbCommMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCommMode.FormattingEnabled = True
        Me.cmbCommMode.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbCommMode.Items"), resources.GetString("cmbCommMode.Items1")})
        Me.cmbCommMode.Name = "cmbCommMode"
        '
        'lblDMDLocation
        '
        resources.ApplyResources(Me.lblDMDLocation, "lblDMDLocation")
        Me.lblDMDLocation.Name = "lblDMDLocation"
        '
        'cmbDMDLocation
        '
        resources.ApplyResources(Me.cmbDMDLocation, "cmbDMDLocation")
        Me.cmbDMDLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDMDLocation.FormattingEnabled = True
        Me.cmbDMDLocation.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbDMDLocation.Items"), resources.GetString("cmbDMDLocation.Items1"), resources.GetString("cmbDMDLocation.Items2"), resources.GetString("cmbDMDLocation.Items3")})
        Me.cmbDMDLocation.Name = "cmbDMDLocation"
        '
        'lblB2SDataCount
        '
        resources.ApplyResources(Me.lblB2SDataCount, "lblB2SDataCount")
        Me.lblB2SDataCount.Name = "lblB2SDataCount"
        '
        'cmbB2SDataCount
        '
        resources.ApplyResources(Me.cmbB2SDataCount, "cmbB2SDataCount")
        Me.cmbB2SDataCount.FormattingEnabled = True
        Me.cmbB2SDataCount.Items.AddRange(New Object() {resources.GetString("cmbB2SDataCount.Items"), resources.GetString("cmbB2SDataCount.Items1"), resources.GetString("cmbB2SDataCount.Items2"), resources.GetString("cmbB2SDataCount.Items3"), resources.GetString("cmbB2SDataCount.Items4"), resources.GetString("cmbB2SDataCount.Items5"), resources.GetString("cmbB2SDataCount.Items6"), resources.GetString("cmbB2SDataCount.Items7"), resources.GetString("cmbB2SDataCount.Items8"), resources.GetString("cmbB2SDataCount.Items9"), resources.GetString("cmbB2SDataCount.Items10"), resources.GetString("cmbB2SDataCount.Items11"), resources.GetString("cmbB2SDataCount.Items12"), resources.GetString("cmbB2SDataCount.Items13"), resources.GetString("cmbB2SDataCount.Items14"), resources.GetString("cmbB2SDataCount.Items15"), resources.GetString("cmbB2SDataCount.Items16"), resources.GetString("cmbB2SDataCount.Items17"), resources.GetString("cmbB2SDataCount.Items18"), resources.GetString("cmbB2SDataCount.Items19"), resources.GetString("cmbB2SDataCount.Items20"), resources.GetString("cmbB2SDataCount.Items21"), resources.GetString("cmbB2SDataCount.Items22"), resources.GetString("cmbB2SDataCount.Items23"), resources.GetString("cmbB2SDataCount.Items24"), resources.GetString("cmbB2SDataCount.Items25"), resources.GetString("cmbB2SDataCount.Items26"), resources.GetString("cmbB2SDataCount.Items27"), resources.GetString("cmbB2SDataCount.Items28"), resources.GetString("cmbB2SDataCount.Items29"), resources.GetString("cmbB2SDataCount.Items30"), resources.GetString("cmbB2SDataCount.Items31"), resources.GetString("cmbB2SDataCount.Items32"), resources.GetString("cmbB2SDataCount.Items33"), resources.GetString("cmbB2SDataCount.Items34"), resources.GetString("cmbB2SDataCount.Items35"), resources.GetString("cmbB2SDataCount.Items36"), resources.GetString("cmbB2SDataCount.Items37"), resources.GetString("cmbB2SDataCount.Items38"), resources.GetString("cmbB2SDataCount.Items39"), resources.GetString("cmbB2SDataCount.Items40"), resources.GetString("cmbB2SDataCount.Items41"), resources.GetString("cmbB2SDataCount.Items42"), resources.GetString("cmbB2SDataCount.Items43"), resources.GetString("cmbB2SDataCount.Items44"), resources.GetString("cmbB2SDataCount.Items45"), resources.GetString("cmbB2SDataCount.Items46"), resources.GetString("cmbB2SDataCount.Items47"), resources.GetString("cmbB2SDataCount.Items48"), resources.GetString("cmbB2SDataCount.Items49"), resources.GetString("cmbB2SDataCount.Items50")})
        Me.cmbB2SDataCount.Name = "cmbB2SDataCount"
        '
        'grpBackground
        '
        resources.ApplyResources(Me.grpBackground, "grpBackground")
        Me.grpBackground.Controls.Add(Me.txtArtwork)
        Me.grpBackground.Controls.Add(Me.lblArtwork)
        Me.grpBackground.Controls.Add(Me.txtAuthor)
        Me.grpBackground.Controls.Add(Me.lblAuthor)
        Me.grpBackground.Name = "grpBackground"
        Me.grpBackground.TabStop = False
        '
        'txtArtwork
        '
        resources.ApplyResources(Me.txtArtwork, "txtArtwork")
        Me.txtArtwork.Name = "txtArtwork"
        '
        'lblArtwork
        '
        resources.ApplyResources(Me.lblArtwork, "lblArtwork")
        Me.lblArtwork.Name = "lblArtwork"
        '
        'txtAuthor
        '
        resources.ApplyResources(Me.txtAuthor, "txtAuthor")
        Me.txtAuthor.Name = "txtAuthor"
        '
        'lblAuthor
        '
        resources.ApplyResources(Me.lblAuthor, "lblAuthor")
        Me.lblAuthor.Name = "lblAuthor"
        '
        'formSettings
        '
        Me.AcceptButton = Me.btnOk
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.grpBackground)
        Me.Controls.Add(Me.groupB2S)
        Me.Controls.Add(Me.groupTable)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.groupGeneral)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formSettings"
        Me.groupGeneral.ResumeLayout(False)
        Me.groupGeneral.PerformLayout()
        Me.groupTable.ResumeLayout(False)
        Me.groupB2S.ResumeLayout(False)
        Me.groupB2S.PerformLayout()
        Me.grpBackground.ResumeLayout(False)
        Me.grpBackground.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents groupGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents lblTableType As System.Windows.Forms.Label
    Friend WithEvents cmbTableType As System.Windows.Forms.ComboBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents groupTable As System.Windows.Forms.GroupBox
    Friend WithEvents groupB2S As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumberOfPlayers As System.Windows.Forms.Label
    Friend WithEvents cmbNumberOfPlayers As System.Windows.Forms.ComboBox
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblB2SDataCount As System.Windows.Forms.Label
    Friend WithEvents cmbB2SDataCount As System.Windows.Forms.ComboBox
    Friend WithEvents lblDMDLocation As System.Windows.Forms.Label
    Friend WithEvents cmbDMDLocation As System.Windows.Forms.ComboBox
    Friend WithEvents lblCommMode As System.Windows.Forms.Label
    Friend WithEvents cmbCommMode As System.Windows.Forms.ComboBox
    Friend WithEvents btnFileName As System.Windows.Forms.Button
    Friend WithEvents chkCreateEMDefaults As System.Windows.Forms.CheckBox
    Friend WithEvents lblDestType As System.Windows.Forms.Label
    Friend WithEvents cmbDestType As System.Windows.Forms.ComboBox
    Friend WithEvents grpBackground As System.Windows.Forms.GroupBox
    Friend WithEvents txtAuthor As System.Windows.Forms.TextBox
    Friend WithEvents lblAuthor As System.Windows.Forms.Label
    Friend WithEvents chkDualBackglass As System.Windows.Forms.CheckBox
    Friend WithEvents txtArtwork As System.Windows.Forms.TextBox
    Friend WithEvents lblArtwork As System.Windows.Forms.Label
End Class
