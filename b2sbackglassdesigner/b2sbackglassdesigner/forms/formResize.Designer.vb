<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formResize
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formResize))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.numPercentage = New System.Windows.Forms.NumericUpDown()
        Me.rbByPercentage = New System.Windows.Forms.RadioButton()
        Me.rbByAbsoluteSize = New System.Windows.Forms.RadioButton()
        Me.cbMaintainAspectRatio = New System.Windows.Forms.CheckBox()
        Me.lblWidth = New System.Windows.Forms.Label()
        Me.lblHeight = New System.Windows.Forms.Label()
        Me.numWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.numHeight = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.B2SLine2 = New B2SBackglassDesigner.B2SLine()
        Me.B2SLine1 = New B2SBackglassDesigner.B2SLine()
        CType(Me.numPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numHeight, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'numPercentage
        '
        resources.ApplyResources(Me.numPercentage, "numPercentage")
        Me.numPercentage.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.numPercentage.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numPercentage.Name = "numPercentage"
        Me.numPercentage.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'rbByPercentage
        '
        resources.ApplyResources(Me.rbByPercentage, "rbByPercentage")
        Me.rbByPercentage.Name = "rbByPercentage"
        Me.rbByPercentage.TabStop = True
        Me.rbByPercentage.UseVisualStyleBackColor = True
        '
        'rbByAbsoluteSize
        '
        resources.ApplyResources(Me.rbByAbsoluteSize, "rbByAbsoluteSize")
        Me.rbByAbsoluteSize.Name = "rbByAbsoluteSize"
        Me.rbByAbsoluteSize.TabStop = True
        Me.rbByAbsoluteSize.UseVisualStyleBackColor = True
        '
        'cbMaintainAspectRatio
        '
        resources.ApplyResources(Me.cbMaintainAspectRatio, "cbMaintainAspectRatio")
        Me.cbMaintainAspectRatio.Name = "cbMaintainAspectRatio"
        Me.cbMaintainAspectRatio.UseVisualStyleBackColor = True
        '
        'lblWidth
        '
        resources.ApplyResources(Me.lblWidth, "lblWidth")
        Me.lblWidth.Name = "lblWidth"
        '
        'lblHeight
        '
        resources.ApplyResources(Me.lblHeight, "lblHeight")
        Me.lblHeight.Name = "lblHeight"
        '
        'numWidth
        '
        resources.ApplyResources(Me.numWidth, "numWidth")
        Me.numWidth.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.numWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numWidth.Name = "numWidth"
        Me.numWidth.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'numHeight
        '
        resources.ApplyResources(Me.numHeight, "numHeight")
        Me.numHeight.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.numHeight.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numHeight.Name = "numHeight"
        Me.numHeight.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'B2SLine2
        '
        resources.ApplyResources(Me.B2SLine2, "B2SLine2")
        Me.B2SLine2.Name = "B2SLine2"
        '
        'B2SLine1
        '
        resources.ApplyResources(Me.B2SLine1, "B2SLine1")
        Me.B2SLine1.Name = "B2SLine1"
        '
        'formResize
        '
        Me.AcceptButton = Me.btnOk
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.numHeight)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.numWidth)
        Me.Controls.Add(Me.lblHeight)
        Me.Controls.Add(Me.lblWidth)
        Me.Controls.Add(Me.B2SLine2)
        Me.Controls.Add(Me.cbMaintainAspectRatio)
        Me.Controls.Add(Me.rbByAbsoluteSize)
        Me.Controls.Add(Me.rbByPercentage)
        Me.Controls.Add(Me.numPercentage)
        Me.Controls.Add(Me.B2SLine1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Name = "formResize"
        CType(Me.numPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numHeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents B2SLine1 As B2SBackglassDesigner.B2SLine
    Friend WithEvents numPercentage As System.Windows.Forms.NumericUpDown
    Friend WithEvents rbByPercentage As System.Windows.Forms.RadioButton
    Friend WithEvents rbByAbsoluteSize As System.Windows.Forms.RadioButton
    Friend WithEvents cbMaintainAspectRatio As System.Windows.Forms.CheckBox
    Friend WithEvents B2SLine2 As B2SBackglassDesigner.B2SLine
    Friend WithEvents lblWidth As System.Windows.Forms.Label
    Friend WithEvents lblHeight As System.Windows.Forms.Label
    Friend WithEvents numWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents numHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
