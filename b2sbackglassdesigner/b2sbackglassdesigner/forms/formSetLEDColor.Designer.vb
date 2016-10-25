<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formSetLEDColor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formSetLEDColor))
        Me.B2SColorBarLEDs = New B2SBackglassDesigner.B2SColorBar()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.grpGeneral = New System.Windows.Forms.GroupBox()
        Me.btnGetColor = New System.Windows.Forms.Button()
        Me.TimerGetColor = New System.Windows.Forms.Timer(Me.components)
        Me.grpGeneral.SuspendLayout()
        Me.SuspendLayout()
        '
        'B2SColorBarLEDs
        '
        resources.ApplyResources(Me.B2SColorBarLEDs, "B2SColorBarLEDs")
        Me.B2SColorBarLEDs.CurrentColor = System.Drawing.Color.Black
        Me.B2SColorBarLEDs.Name = "B2SColorBarLEDs"
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
        Me.grpGeneral.Controls.Add(Me.B2SColorBarLEDs)
        Me.grpGeneral.Name = "grpGeneral"
        Me.grpGeneral.TabStop = False
        '
        'btnGetColor
        '
        resources.ApplyResources(Me.btnGetColor, "btnGetColor")
        Me.btnGetColor.Name = "btnGetColor"
        Me.btnGetColor.UseVisualStyleBackColor = True
        '
        'TimerGetColor
        '
        Me.TimerGetColor.Interval = 50
        '
        'formSetLEDColor
        '
        Me.AcceptButton = Me.btnOk
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.btnGetColor)
        Me.Controls.Add(Me.grpGeneral)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formSetLEDColor"
        Me.grpGeneral.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents B2SColorBarLEDs As B2SBackglassDesigner.B2SColorBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents grpGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents btnGetColor As System.Windows.Forms.Button
    Friend WithEvents TimerGetColor As System.Windows.Forms.Timer
End Class
