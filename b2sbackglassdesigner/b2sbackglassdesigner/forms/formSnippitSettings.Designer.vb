<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formSnippitSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formSnippitSettings))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.groupGeneral = New System.Windows.Forms.GroupBox()
        Me.numericZOrder = New System.Windows.Forms.NumericUpDown()
        Me.lblZOrder = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblRotatingSteps = New System.Windows.Forms.Label()
        Me.grpRotating = New System.Windows.Forms.GroupBox()
        Me.lblRotationStopping = New System.Windows.Forms.Label()
        Me.cmbRotationStopBehaviour = New System.Windows.Forms.ComboBox()
        Me.numericMechID = New System.Windows.Forms.NumericUpDown()
        Me.lblMechID = New System.Windows.Forms.Label()
        Me.cmbRotatingDirection = New System.Windows.Forms.ComboBox()
        Me.lblRotationDirection = New System.Windows.Forms.Label()
        Me.numericRotatingSteps = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.numericRotatingInterval = New System.Windows.Forms.NumericUpDown()
        Me.lblRotatingInterval = New System.Windows.Forms.Label()
        Me.groupGeneral.SuspendLayout()
        CType(Me.numericZOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRotating.SuspendLayout()
        CType(Me.numericMechID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericRotatingSteps, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericRotatingInterval, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'groupGeneral
        '
        resources.ApplyResources(Me.groupGeneral, "groupGeneral")
        Me.groupGeneral.Controls.Add(Me.numericZOrder)
        Me.groupGeneral.Controls.Add(Me.lblZOrder)
        Me.groupGeneral.Controls.Add(Me.cmbType)
        Me.groupGeneral.Controls.Add(Me.lblType)
        Me.groupGeneral.Controls.Add(Me.lblName)
        Me.groupGeneral.Controls.Add(Me.txtName)
        Me.groupGeneral.Name = "groupGeneral"
        Me.groupGeneral.TabStop = False
        '
        'numericZOrder
        '
        resources.ApplyResources(Me.numericZOrder, "numericZOrder")
        Me.numericZOrder.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.numericZOrder.Name = "numericZOrder"
        '
        'lblZOrder
        '
        resources.ApplyResources(Me.lblZOrder, "lblZOrder")
        Me.lblZOrder.Name = "lblZOrder"
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Items.AddRange(New Object() {resources.GetString("cmbType.Items"), resources.GetString("cmbType.Items1"), resources.GetString("cmbType.Items2")})
        resources.ApplyResources(Me.cmbType, "cmbType")
        Me.cmbType.Name = "cmbType"
        '
        'lblType
        '
        resources.ApplyResources(Me.lblType, "lblType")
        Me.lblType.Name = "lblType"
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
        'lblRotatingSteps
        '
        resources.ApplyResources(Me.lblRotatingSteps, "lblRotatingSteps")
        Me.lblRotatingSteps.Name = "lblRotatingSteps"
        '
        'grpRotating
        '
        resources.ApplyResources(Me.grpRotating, "grpRotating")
        Me.grpRotating.Controls.Add(Me.lblRotationStopping)
        Me.grpRotating.Controls.Add(Me.cmbRotationStopBehaviour)
        Me.grpRotating.Controls.Add(Me.numericMechID)
        Me.grpRotating.Controls.Add(Me.lblMechID)
        Me.grpRotating.Controls.Add(Me.cmbRotatingDirection)
        Me.grpRotating.Controls.Add(Me.lblRotationDirection)
        Me.grpRotating.Controls.Add(Me.numericRotatingSteps)
        Me.grpRotating.Controls.Add(Me.Label2)
        Me.grpRotating.Controls.Add(Me.numericRotatingInterval)
        Me.grpRotating.Controls.Add(Me.lblRotatingInterval)
        Me.grpRotating.Controls.Add(Me.lblRotatingSteps)
        Me.grpRotating.Name = "grpRotating"
        Me.grpRotating.TabStop = False
        '
        'lblRotationStopping
        '
        resources.ApplyResources(Me.lblRotationStopping, "lblRotationStopping")
        Me.lblRotationStopping.Name = "lblRotationStopping"
        '
        'cmbRotationStopBehaviour
        '
        resources.ApplyResources(Me.cmbRotationStopBehaviour, "cmbRotationStopBehaviour")
        Me.cmbRotationStopBehaviour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRotationStopBehaviour.FormattingEnabled = True
        Me.cmbRotationStopBehaviour.Items.AddRange(New Object() {resources.GetString("cmbRotationStopBehaviour.Items"), resources.GetString("cmbRotationStopBehaviour.Items1"), resources.GetString("cmbRotationStopBehaviour.Items2"), resources.GetString("cmbRotationStopBehaviour.Items3")})
        Me.cmbRotationStopBehaviour.Name = "cmbRotationStopBehaviour"
        '
        'numericMechID
        '
        resources.ApplyResources(Me.numericMechID, "numericMechID")
        Me.numericMechID.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numericMechID.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericMechID.Name = "numericMechID"
        Me.numericMechID.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblMechID
        '
        resources.ApplyResources(Me.lblMechID, "lblMechID")
        Me.lblMechID.Name = "lblMechID"
        '
        'cmbRotatingDirection
        '
        Me.cmbRotatingDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRotatingDirection.FormattingEnabled = True
        Me.cmbRotatingDirection.Items.AddRange(New Object() {resources.GetString("cmbRotatingDirection.Items"), resources.GetString("cmbRotatingDirection.Items1")})
        resources.ApplyResources(Me.cmbRotatingDirection, "cmbRotatingDirection")
        Me.cmbRotatingDirection.Name = "cmbRotatingDirection"
        '
        'lblRotationDirection
        '
        resources.ApplyResources(Me.lblRotationDirection, "lblRotationDirection")
        Me.lblRotationDirection.Name = "lblRotationDirection"
        '
        'numericRotatingSteps
        '
        resources.ApplyResources(Me.numericRotatingSteps, "numericRotatingSteps")
        Me.numericRotatingSteps.Maximum = New Decimal(New Integer() {90, 0, 0, 0})
        Me.numericRotatingSteps.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.numericRotatingSteps.Name = "numericRotatingSteps"
        Me.numericRotatingSteps.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'numericRotatingInterval
        '
        resources.ApplyResources(Me.numericRotatingInterval, "numericRotatingInterval")
        Me.numericRotatingInterval.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.numericRotatingInterval.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numericRotatingInterval.Name = "numericRotatingInterval"
        Me.numericRotatingInterval.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'lblRotatingInterval
        '
        resources.ApplyResources(Me.lblRotatingInterval, "lblRotatingInterval")
        Me.lblRotatingInterval.Name = "lblRotatingInterval"
        '
        'formSnippitSettings
        '
        Me.AcceptButton = Me.btnOk
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.grpRotating)
        Me.Controls.Add(Me.groupGeneral)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formSnippitSettings"
        Me.groupGeneral.ResumeLayout(False)
        Me.groupGeneral.PerformLayout()
        CType(Me.numericZOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRotating.ResumeLayout(False)
        Me.grpRotating.PerformLayout()
        CType(Me.numericMechID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericRotatingSteps, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericRotatingInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents groupGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblRotatingSteps As System.Windows.Forms.Label
    Friend WithEvents grpRotating As System.Windows.Forms.GroupBox
    Friend WithEvents lblRotatingInterval As System.Windows.Forms.Label
    Friend WithEvents numericRotatingInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents numericZOrder As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblZOrder As System.Windows.Forms.Label
    Friend WithEvents numericRotatingSteps As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbRotatingDirection As System.Windows.Forms.ComboBox
    Friend WithEvents lblRotationDirection As System.Windows.Forms.Label
    Friend WithEvents numericMechID As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMechID As System.Windows.Forms.Label
    Friend WithEvents cmbRotationStopBehaviour As System.Windows.Forms.ComboBox
    Friend WithEvents lblRotationStopping As System.Windows.Forms.Label
End Class
