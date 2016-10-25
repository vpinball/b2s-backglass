<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formAddSnippit
    Inherits formBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formAddSnippit))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.PictureBoxImage = New System.Windows.Forms.PictureBox()
        Me.txtImageFileName = New System.Windows.Forms.TextBox()
        Me.lblImageFileName = New System.Windows.Forms.Label()
        Me.btnChooseImageFile = New System.Windows.Forms.Button()
        Me.txtX = New System.Windows.Forms.TextBox()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.txtY = New System.Windows.Forms.TextBox()
        Me.B2SLine1 = New B2SBackglassDesigner.B2SLine()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        CType(Me.PictureBoxImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Name = "btnClose"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        resources.ApplyResources(Me.btnApply, "btnApply")
        Me.btnApply.Name = "btnApply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'PictureBoxImage
        '
        resources.ApplyResources(Me.PictureBoxImage, "PictureBoxImage")
        Me.PictureBoxImage.Name = "PictureBoxImage"
        Me.PictureBoxImage.TabStop = False
        '
        'txtImageFileName
        '
        resources.ApplyResources(Me.txtImageFileName, "txtImageFileName")
        Me.txtImageFileName.Name = "txtImageFileName"
        '
        'lblImageFileName
        '
        resources.ApplyResources(Me.lblImageFileName, "lblImageFileName")
        Me.lblImageFileName.Name = "lblImageFileName"
        '
        'btnChooseImageFile
        '
        resources.ApplyResources(Me.btnChooseImageFile, "btnChooseImageFile")
        Me.btnChooseImageFile.Name = "btnChooseImageFile"
        Me.btnChooseImageFile.UseVisualStyleBackColor = True
        '
        'txtX
        '
        resources.ApplyResources(Me.txtX, "txtX")
        Me.txtX.Name = "txtX"
        '
        'lblLocation
        '
        resources.ApplyResources(Me.lblLocation, "lblLocation")
        Me.lblLocation.Name = "lblLocation"
        '
        'txtY
        '
        resources.ApplyResources(Me.txtY, "txtY")
        Me.txtY.Name = "txtY"
        '
        'B2SLine1
        '
        resources.ApplyResources(Me.B2SLine1, "B2SLine1")
        Me.B2SLine1.Name = "B2SLine1"
        '
        'txtName
        '
        resources.ApplyResources(Me.txtName, "txtName")
        Me.txtName.Name = "txtName"
        '
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.Name = "lblName"
        '
        'formAddSnippit
        '
        Me.AcceptButton = Me.btnApply
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.CancelButton = Me.btnClose
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.txtY)
        Me.Controls.Add(Me.txtX)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.btnChooseImageFile)
        Me.Controls.Add(Me.txtImageFileName)
        Me.Controls.Add(Me.lblImageFileName)
        Me.Controls.Add(Me.PictureBoxImage)
        Me.Controls.Add(Me.B2SLine1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnApply)
        Me.Name = "formAddSnippit"
        CType(Me.PictureBoxImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents PictureBoxImage As System.Windows.Forms.PictureBox
    Friend WithEvents B2SLine1 As B2SBackglassDesigner.B2SLine
    Friend WithEvents txtImageFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblImageFileName As System.Windows.Forms.Label
    Friend WithEvents btnChooseImageFile As System.Windows.Forms.Button
    Friend WithEvents txtX As System.Windows.Forms.TextBox
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents txtY As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
End Class
