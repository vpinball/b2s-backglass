<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formDMD
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
        Me.groupDMD = New System.Windows.Forms.GroupBox()
        Me.chkDMDFlipY = New System.Windows.Forms.CheckBox()
        Me.lblDMDLocationY = New System.Windows.Forms.Label()
        Me.txtDMDLocationY = New WindowsApplication1.Module1.B2STextBox()
        Me.txtDMDLocationX = New WindowsApplication1.Module1.B2STextBox()
        Me.lblDMDLocationX = New System.Windows.Forms.Label()
        Me.lblDMDScreenSizeHeight = New System.Windows.Forms.Label()
        Me.txtDMDScreenSizeHeight = New WindowsApplication1.Module1.B2STextBox()
        Me.txtDMDScreenSizeWidth = New WindowsApplication1.Module1.B2STextBox()
        Me.lblDMDScreenSizeWidth = New System.Windows.Forms.Label()
        Me.chkDMDAtDefaultLocation = New System.Windows.Forms.CheckBox()
        Me.lblDMDSizeHeight = New System.Windows.Forms.Label()
        Me.txtDMDSizeHeight = New WindowsApplication1.Module1.B2STextBox()
        Me.txtDMDSizeWidth = New WindowsApplication1.Module1.B2STextBox()
        Me.lblDMDSize = New System.Windows.Forms.Label()
        Me.txtDMDScreen = New System.Windows.Forms.TextBox()
        Me.lblDMDScreen = New System.Windows.Forms.Label()
        Me.lblDMD = New System.Windows.Forms.Label()
        Me.groupDMD.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupDMD
        '
        Me.groupDMD.Controls.Add(Me.chkDMDFlipY)
        Me.groupDMD.Controls.Add(Me.lblDMDLocationY)
        Me.groupDMD.Controls.Add(Me.txtDMDLocationY)
        Me.groupDMD.Controls.Add(Me.txtDMDLocationX)
        Me.groupDMD.Controls.Add(Me.lblDMDLocationX)
        Me.groupDMD.Controls.Add(Me.lblDMDScreenSizeHeight)
        Me.groupDMD.Controls.Add(Me.txtDMDScreenSizeHeight)
        Me.groupDMD.Controls.Add(Me.txtDMDScreenSizeWidth)
        Me.groupDMD.Controls.Add(Me.lblDMDScreenSizeWidth)
        Me.groupDMD.Controls.Add(Me.chkDMDAtDefaultLocation)
        Me.groupDMD.Controls.Add(Me.lblDMDSizeHeight)
        Me.groupDMD.Controls.Add(Me.txtDMDSizeHeight)
        Me.groupDMD.Controls.Add(Me.txtDMDSizeWidth)
        Me.groupDMD.Controls.Add(Me.lblDMDSize)
        Me.groupDMD.Controls.Add(Me.txtDMDScreen)
        Me.groupDMD.Controls.Add(Me.lblDMDScreen)
        Me.groupDMD.Controls.Add(Me.lblDMD)
        Me.groupDMD.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupDMD.Location = New System.Drawing.Point(5, 4)
        Me.groupDMD.Name = "groupDMD"
        Me.groupDMD.Size = New System.Drawing.Size(398, 239)
        Me.groupDMD.TabIndex = 7
        Me.groupDMD.TabStop = False
        Me.groupDMD.Text = "DMD"
        '
        'chkDMDFlipY
        '
        Me.chkDMDFlipY.AutoSize = True
        Me.chkDMDFlipY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDMDFlipY.Location = New System.Drawing.Point(152, 207)
        Me.chkDMDFlipY.Name = "chkDMDFlipY"
        Me.chkDMDFlipY.Size = New System.Drawing.Size(89, 20)
        Me.chkDMDFlipY.TabIndex = 23
        Me.chkDMDFlipY.Text = "DMD Y-Flip"
        Me.chkDMDFlipY.UseVisualStyleBackColor = True
        '
        'lblDMDLocationY
        '
        Me.lblDMDLocationY.AutoSize = True
        Me.lblDMDLocationY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDMDLocationY.Location = New System.Drawing.Point(210, 127)
        Me.lblDMDLocationY.Name = "lblDMDLocationY"
        Me.lblDMDLocationY.Size = New System.Drawing.Size(12, 16)
        Me.lblDMDLocationY.TabIndex = 22
        Me.lblDMDLocationY.Text = ","
        Me.lblDMDLocationY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDMDLocationY
        '
        Me.txtDMDLocationY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMDLocationY.Location = New System.Drawing.Point(227, 123)
        Me.txtDMDLocationY.Name = "txtDMDLocationY"
        Me.txtDMDLocationY.Size = New System.Drawing.Size(54, 23)
        Me.txtDMDLocationY.TabIndex = 4
        '
        'txtDMDLocationX
        '
        Me.txtDMDLocationX.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMDLocationX.Location = New System.Drawing.Point(152, 123)
        Me.txtDMDLocationX.Name = "txtDMDLocationX"
        Me.txtDMDLocationX.Size = New System.Drawing.Size(54, 23)
        Me.txtDMDLocationX.TabIndex = 3
        '
        'lblDMDLocationX
        '
        Me.lblDMDLocationX.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDMDLocationX.Location = New System.Drawing.Point(12, 125)
        Me.lblDMDLocationX.Name = "lblDMDLocationX"
        Me.lblDMDLocationX.Size = New System.Drawing.Size(134, 20)
        Me.lblDMDLocationX.TabIndex = 19
        Me.lblDMDLocationX.Text = "DMD location:"
        Me.lblDMDLocationX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDMDScreenSizeHeight
        '
        Me.lblDMDScreenSizeHeight.AutoSize = True
        Me.lblDMDScreenSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDMDScreenSizeHeight.Location = New System.Drawing.Point(210, 98)
        Me.lblDMDScreenSizeHeight.Name = "lblDMDScreenSizeHeight"
        Me.lblDMDScreenSizeHeight.Size = New System.Drawing.Size(12, 16)
        Me.lblDMDScreenSizeHeight.TabIndex = 18
        Me.lblDMDScreenSizeHeight.Text = ","
        Me.lblDMDScreenSizeHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDMDScreenSizeHeight
        '
        Me.txtDMDScreenSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMDScreenSizeHeight.Location = New System.Drawing.Point(227, 94)
        Me.txtDMDScreenSizeHeight.Name = "txtDMDScreenSizeHeight"
        Me.txtDMDScreenSizeHeight.ReadOnly = True
        Me.txtDMDScreenSizeHeight.Size = New System.Drawing.Size(54, 23)
        Me.txtDMDScreenSizeHeight.TabIndex = 2
        '
        'txtDMDScreenSizeWidth
        '
        Me.txtDMDScreenSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMDScreenSizeWidth.Location = New System.Drawing.Point(152, 94)
        Me.txtDMDScreenSizeWidth.Name = "txtDMDScreenSizeWidth"
        Me.txtDMDScreenSizeWidth.ReadOnly = True
        Me.txtDMDScreenSizeWidth.Size = New System.Drawing.Size(54, 23)
        Me.txtDMDScreenSizeWidth.TabIndex = 1
        '
        'lblDMDScreenSizeWidth
        '
        Me.lblDMDScreenSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDMDScreenSizeWidth.Location = New System.Drawing.Point(12, 96)
        Me.lblDMDScreenSizeWidth.Name = "lblDMDScreenSizeWidth"
        Me.lblDMDScreenSizeWidth.Size = New System.Drawing.Size(134, 20)
        Me.lblDMDScreenSizeWidth.TabIndex = 15
        Me.lblDMDScreenSizeWidth.Text = "Current screen size:"
        Me.lblDMDScreenSizeWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkDMDAtDefaultLocation
        '
        Me.chkDMDAtDefaultLocation.AutoSize = True
        Me.chkDMDAtDefaultLocation.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDMDAtDefaultLocation.Location = New System.Drawing.Point(152, 181)
        Me.chkDMDAtDefaultLocation.Name = "chkDMDAtDefaultLocation"
        Me.chkDMDAtDefaultLocation.Size = New System.Drawing.Size(227, 20)
        Me.chkDMDAtDefaultLocation.TabIndex = 7
        Me.chkDMDAtDefaultLocation.Text = "DMD is at default location (for grill)"
        Me.chkDMDAtDefaultLocation.UseVisualStyleBackColor = True
        '
        'lblDMDSizeHeight
        '
        Me.lblDMDSizeHeight.AutoSize = True
        Me.lblDMDSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDMDSizeHeight.Location = New System.Drawing.Point(210, 156)
        Me.lblDMDSizeHeight.Name = "lblDMDSizeHeight"
        Me.lblDMDSizeHeight.Size = New System.Drawing.Size(12, 16)
        Me.lblDMDSizeHeight.TabIndex = 13
        Me.lblDMDSizeHeight.Text = ","
        Me.lblDMDSizeHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDMDSizeHeight
        '
        Me.txtDMDSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMDSizeHeight.Location = New System.Drawing.Point(227, 152)
        Me.txtDMDSizeHeight.Name = "txtDMDSizeHeight"
        Me.txtDMDSizeHeight.Size = New System.Drawing.Size(54, 23)
        Me.txtDMDSizeHeight.TabIndex = 6
        '
        'txtDMDSizeWidth
        '
        Me.txtDMDSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMDSizeWidth.Location = New System.Drawing.Point(152, 152)
        Me.txtDMDSizeWidth.Name = "txtDMDSizeWidth"
        Me.txtDMDSizeWidth.Size = New System.Drawing.Size(54, 23)
        Me.txtDMDSizeWidth.TabIndex = 5
        '
        'lblDMDSize
        '
        Me.lblDMDSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDMDSize.Location = New System.Drawing.Point(12, 154)
        Me.lblDMDSize.Name = "lblDMDSize"
        Me.lblDMDSize.Size = New System.Drawing.Size(134, 20)
        Me.lblDMDSize.TabIndex = 10
        Me.lblDMDSize.Text = "DMD size:"
        Me.lblDMDSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDMDScreen
        '
        Me.txtDMDScreen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMDScreen.Location = New System.Drawing.Point(152, 65)
        Me.txtDMDScreen.Name = "txtDMDScreen"
        Me.txtDMDScreen.ReadOnly = True
        Me.txtDMDScreen.Size = New System.Drawing.Size(217, 23)
        Me.txtDMDScreen.TabIndex = 0
        '
        'lblDMDScreen
        '
        Me.lblDMDScreen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDMDScreen.Location = New System.Drawing.Point(12, 65)
        Me.lblDMDScreen.Name = "lblDMDScreen"
        Me.lblDMDScreen.Size = New System.Drawing.Size(134, 20)
        Me.lblDMDScreen.TabIndex = 8
        Me.lblDMDScreen.Text = "Current screen:"
        Me.lblDMDScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDMD
        '
        Me.lblDMD.AutoSize = True
        Me.lblDMD.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDMD.Location = New System.Drawing.Point(14, 23)
        Me.lblDMD.Name = "lblDMD"
        Me.lblDMD.Size = New System.Drawing.Size(382, 32)
        Me.lblDMD.TabIndex = 5
        Me.lblDMD.Text = "This is the dummy window for the DMD. Drag and drop me" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "whereever you want to hav" & _
    "e me."
        '
        'formDMD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(410, 255)
        Me.Controls.Add(Me.groupDMD)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formDMD"
        Me.Text = "DMD - B2S Screen Resolution Identifier"
        Me.groupDMD.ResumeLayout(False)
        Me.groupDMD.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents groupDMD As System.Windows.Forms.GroupBox
    Friend WithEvents lblDMDLocationY As System.Windows.Forms.Label
    Friend WithEvents txtDMDLocationY As B2STextBox
    Friend WithEvents txtDMDLocationX As B2STextBox
    Friend WithEvents lblDMDLocationX As System.Windows.Forms.Label
    Friend WithEvents lblDMDScreenSizeHeight As System.Windows.Forms.Label
    Friend WithEvents txtDMDScreenSizeHeight As B2STextBox
    Friend WithEvents txtDMDScreenSizeWidth As B2STextBox
    Friend WithEvents lblDMDScreenSizeWidth As System.Windows.Forms.Label
    Friend WithEvents chkDMDAtDefaultLocation As System.Windows.Forms.CheckBox
    Friend WithEvents lblDMDSizeHeight As System.Windows.Forms.Label
    Friend WithEvents txtDMDSizeHeight As B2STextBox
    Friend WithEvents txtDMDSizeWidth As B2STextBox
    Friend WithEvents lblDMDSize As System.Windows.Forms.Label
    Friend WithEvents txtDMDScreen As System.Windows.Forms.TextBox
    Friend WithEvents lblDMDScreen As System.Windows.Forms.Label
    Friend WithEvents lblDMD As System.Windows.Forms.Label
    Friend WithEvents chkDMDFlipY As System.Windows.Forms.CheckBox
End Class
