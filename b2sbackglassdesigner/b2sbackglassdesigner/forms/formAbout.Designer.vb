<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formAbout
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formAbout))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.panelAbout = New System.Windows.Forms.Panel()
        Me.txtCredits = New System.Windows.Forms.TextBox()
        Me.lblCredits = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.PictureBoxIcon = New System.Windows.Forms.PictureBox()
        Me.PictureBoxAbout = New System.Windows.Forms.PictureBox()
        Me.btnDonation = New System.Windows.Forms.Button()
        Me.panelAbout.SuspendLayout()
        CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxAbout, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Name = "btnClose"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'panelAbout
        '
        resources.ApplyResources(Me.panelAbout, "panelAbout")
        Me.panelAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelAbout.Controls.Add(Me.txtCredits)
        Me.panelAbout.Controls.Add(Me.lblCredits)
        Me.panelAbout.Controls.Add(Me.lblCopyright)
        Me.panelAbout.Controls.Add(Me.lblTitle)
        Me.panelAbout.Controls.Add(Me.PictureBoxIcon)
        Me.panelAbout.Controls.Add(Me.PictureBoxAbout)
        Me.panelAbout.Name = "panelAbout"
        '
        'txtCredits
        '
        resources.ApplyResources(Me.txtCredits, "txtCredits")
        Me.txtCredits.Name = "txtCredits"
        Me.txtCredits.ReadOnly = True
        '
        'lblCredits
        '
        resources.ApplyResources(Me.lblCredits, "lblCredits")
        Me.lblCredits.Name = "lblCredits"
        '
        'lblCopyright
        '
        resources.ApplyResources(Me.lblCopyright, "lblCopyright")
        Me.lblCopyright.Name = "lblCopyright"
        '
        'lblTitle
        '
        resources.ApplyResources(Me.lblTitle, "lblTitle")
        Me.lblTitle.BackColor = System.Drawing.Color.White
        Me.lblTitle.Name = "lblTitle"
        '
        'PictureBoxIcon
        '
        Me.PictureBoxIcon.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.PictureBoxIcon, "PictureBoxIcon")
        Me.PictureBoxIcon.Name = "PictureBoxIcon"
        Me.PictureBoxIcon.TabStop = False
        '
        'PictureBoxAbout
        '
        resources.ApplyResources(Me.PictureBoxAbout, "PictureBoxAbout")
        Me.PictureBoxAbout.BackColor = System.Drawing.Color.White
        Me.PictureBoxAbout.Name = "PictureBoxAbout"
        Me.PictureBoxAbout.TabStop = False
        '
        'btnDonation
        '
        resources.ApplyResources(Me.btnDonation, "btnDonation")
        Me.btnDonation.Name = "btnDonation"
        Me.btnDonation.UseVisualStyleBackColor = True
        '
        'formAbout
        '
        Me.AcceptButton = Me.btnClose
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnClose
        Me.Controls.Add(Me.btnDonation)
        Me.Controls.Add(Me.panelAbout)
        Me.Controls.Add(Me.btnClose)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formAbout"
        Me.panelAbout.ResumeLayout(False)
        Me.panelAbout.PerformLayout()
        CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxAbout, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents panelAbout As System.Windows.Forms.Panel
    Friend WithEvents PictureBoxAbout As System.Windows.Forms.PictureBox
    Friend WithEvents txtCredits As System.Windows.Forms.TextBox
    Friend WithEvents lblCredits As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents PictureBoxIcon As System.Windows.Forms.PictureBox
    Friend WithEvents btnDonation As System.Windows.Forms.Button
End Class
