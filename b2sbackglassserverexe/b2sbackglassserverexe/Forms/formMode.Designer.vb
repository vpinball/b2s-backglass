<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formMode
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
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.TimerOpacity = New System.Windows.Forms.Timer(Me.components)
        Me.TimerShutDown = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'lblInfo
        '
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.Location = New System.Drawing.Point(12, 20)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(338, 20)
        Me.lblInfo.TabIndex = 0
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TimerOpacity
        '
        '
        'TimerShutDown
        '
        '
        'formMode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 62)
        Me.Controls.Add(Me.lblInfo)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formMode"
        Me.Opacity = 0.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backglass dual mode ..."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents TimerOpacity As System.Windows.Forms.Timer
    Friend WithEvents TimerShutDown As System.Windows.Forms.Timer
End Class
