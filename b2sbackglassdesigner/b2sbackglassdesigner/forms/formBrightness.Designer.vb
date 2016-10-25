<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formBrightness
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formBrightness))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.PictureBoxPreview = New System.Windows.Forms.PictureBox()
        Me.TrackBarBrightness = New System.Windows.Forms.TrackBar()
        Me.NumericUpDownBrightness = New System.Windows.Forms.NumericUpDown()
        Me.chkIgnoreGrill = New System.Windows.Forms.CheckBox()
        Me.B2SLine1 = New B2SBackglassDesigner.B2SLine()
        CType(Me.PictureBoxPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarBrightness, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownBrightness, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'PictureBoxPreview
        '
        resources.ApplyResources(Me.PictureBoxPreview, "PictureBoxPreview")
        Me.PictureBoxPreview.Name = "PictureBoxPreview"
        Me.PictureBoxPreview.TabStop = False
        '
        'TrackBarBrightness
        '
        resources.ApplyResources(Me.TrackBarBrightness, "TrackBarBrightness")
        Me.TrackBarBrightness.Maximum = 100
        Me.TrackBarBrightness.Minimum = -100
        Me.TrackBarBrightness.Name = "TrackBarBrightness"
        '
        'NumericUpDownBrightness
        '
        resources.ApplyResources(Me.NumericUpDownBrightness, "NumericUpDownBrightness")
        Me.NumericUpDownBrightness.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDownBrightness.Name = "NumericUpDownBrightness"
        '
        'chkIgnoreGrill
        '
        resources.ApplyResources(Me.chkIgnoreGrill, "chkIgnoreGrill")
        Me.chkIgnoreGrill.Name = "chkIgnoreGrill"
        Me.chkIgnoreGrill.UseVisualStyleBackColor = True
        '
        'B2SLine1
        '
        resources.ApplyResources(Me.B2SLine1, "B2SLine1")
        Me.B2SLine1.Name = "B2SLine1"
        '
        'formBrightness
        '
        Me.AcceptButton = Me.btnOk
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.chkIgnoreGrill)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.NumericUpDownBrightness)
        Me.Controls.Add(Me.TrackBarBrightness)
        Me.Controls.Add(Me.PictureBoxPreview)
        Me.Controls.Add(Me.B2SLine1)
        Me.Name = "formBrightness"
        CType(Me.PictureBoxPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarBrightness, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownBrightness, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents B2SLine1 As B2SBackglassDesigner.B2SLine
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents PictureBoxPreview As System.Windows.Forms.PictureBox
    Friend WithEvents TrackBarBrightness As System.Windows.Forms.TrackBar
    Friend WithEvents NumericUpDownBrightness As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkIgnoreGrill As System.Windows.Forms.CheckBox
End Class
