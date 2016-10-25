<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formToolResources
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formToolResources))
        Me.lbImages = New System.Windows.Forms.ListBox()
        Me.PanelImages = New System.Windows.Forms.Panel()
        Me.txtRomID = New System.Windows.Forms.TextBox()
        Me.lblRomID = New System.Windows.Forms.Label()
        Me.cmbROMIDType = New System.Windows.Forms.ComboBox()
        Me.lblRomIDType = New System.Windows.Forms.Label()
        Me.cmbImageType = New System.Windows.Forms.ComboBox()
        Me.lblImageType = New System.Windows.Forms.Label()
        Me.PanelImages.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbImages
        '
        Me.lbImages.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        resources.ApplyResources(Me.lbImages, "lbImages")
        Me.lbImages.FormattingEnabled = True
        Me.lbImages.Name = "lbImages"
        '
        'PanelImages
        '
        Me.PanelImages.Controls.Add(Me.txtRomID)
        Me.PanelImages.Controls.Add(Me.lblRomID)
        Me.PanelImages.Controls.Add(Me.cmbROMIDType)
        Me.PanelImages.Controls.Add(Me.lblRomIDType)
        Me.PanelImages.Controls.Add(Me.cmbImageType)
        Me.PanelImages.Controls.Add(Me.lblImageType)
        resources.ApplyResources(Me.PanelImages, "PanelImages")
        Me.PanelImages.Name = "PanelImages"
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
        'cmbImageType
        '
        resources.ApplyResources(Me.cmbImageType, "cmbImageType")
        Me.cmbImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbImageType.FormattingEnabled = True
        Me.cmbImageType.Items.AddRange(New Object() {Global.B2SBackglassDesigner.My.Resources.Resources.TXT_B2SIDType, resources.GetString("cmbImageType.Items"), resources.GetString("cmbImageType.Items1")})
        Me.cmbImageType.Name = "cmbImageType"
        '
        'lblImageType
        '
        resources.ApplyResources(Me.lblImageType, "lblImageType")
        Me.lblImageType.Name = "lblImageType"
        '
        'formToolResources
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Controls.Add(Me.lbImages)
        Me.Controls.Add(Me.PanelImages)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formToolResources"
        Me.ShowInTaskbar = False
        Me.PanelImages.ResumeLayout(False)
        Me.PanelImages.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbImages As System.Windows.Forms.ListBox
    Friend WithEvents PanelImages As System.Windows.Forms.Panel
    Friend WithEvents cmbImageType As System.Windows.Forms.ComboBox
    Friend WithEvents lblImageType As System.Windows.Forms.Label
    Friend WithEvents cmbROMIDType As System.Windows.Forms.ComboBox
    Friend WithEvents lblRomIDType As System.Windows.Forms.Label
    Friend WithEvents txtRomID As System.Windows.Forms.TextBox
    Friend WithEvents lblRomID As System.Windows.Forms.Label
End Class
