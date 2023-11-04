<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formBackground
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formBackground))
        Me.groupBackground = New System.Windows.Forms.GroupBox()
        Me.chkBackgroundFullSize = New System.Windows.Forms.CheckBox()
        Me.TxtBackgroundPath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBackgroundScreenScale = New System.Windows.Forms.TextBox()
        Me.lblBackgroundLocationY = New System.Windows.Forms.Label()
        Me.txtBackgroundLocationY = New B2S.Module1.B2STextBox()
        Me.txtBackgroundLocationX = New B2S.Module1.B2STextBox()
        Me.lblBackgroundLocationX = New System.Windows.Forms.Label()
        Me.lblBackgroundScreenSizeHeight = New System.Windows.Forms.Label()
        Me.txtBackgroundScreenSizeHeight = New B2S.Module1.B2STextBox()
        Me.txtBackgroundScreenSizeWidth = New B2S.Module1.B2STextBox()
        Me.lblBackgroundScreenSizeWidth = New System.Windows.Forms.Label()
        Me.lblBackgroundSizeHeight = New System.Windows.Forms.Label()
        Me.txtBackgroundSizeHeight = New B2S.Module1.B2STextBox()
        Me.txtBackgroundSizeWidth = New B2S.Module1.B2STextBox()
        Me.lblBackgroundSize = New System.Windows.Forms.Label()
        Me.txtBackgroundScreen = New System.Windows.Forms.TextBox()
        Me.lblBackgroundScreen = New System.Windows.Forms.Label()
        Me.lblBackground = New System.Windows.Forms.Label()
        Me.groupBackground.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBackground
        '
        Me.groupBackground.Controls.Add(Me.chkBackgroundFullSize)
        Me.groupBackground.Controls.Add(Me.TxtBackgroundPath)
        Me.groupBackground.Controls.Add(Me.Label1)
        Me.groupBackground.Controls.Add(Me.txtBackgroundScreenScale)
        Me.groupBackground.Controls.Add(Me.lblBackgroundLocationY)
        Me.groupBackground.Controls.Add(Me.txtBackgroundLocationY)
        Me.groupBackground.Controls.Add(Me.txtBackgroundLocationX)
        Me.groupBackground.Controls.Add(Me.lblBackgroundLocationX)
        Me.groupBackground.Controls.Add(Me.lblBackgroundScreenSizeHeight)
        Me.groupBackground.Controls.Add(Me.txtBackgroundScreenSizeHeight)
        Me.groupBackground.Controls.Add(Me.txtBackgroundScreenSizeWidth)
        Me.groupBackground.Controls.Add(Me.lblBackgroundScreenSizeWidth)
        Me.groupBackground.Controls.Add(Me.lblBackgroundSizeHeight)
        Me.groupBackground.Controls.Add(Me.txtBackgroundSizeHeight)
        Me.groupBackground.Controls.Add(Me.txtBackgroundSizeWidth)
        Me.groupBackground.Controls.Add(Me.lblBackgroundSize)
        Me.groupBackground.Controls.Add(Me.txtBackgroundScreen)
        Me.groupBackground.Controls.Add(Me.lblBackgroundScreen)
        Me.groupBackground.Controls.Add(Me.lblBackground)
        Me.groupBackground.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupBackground.ForeColor = System.Drawing.Color.White
        Me.groupBackground.Location = New System.Drawing.Point(5, 4)
        Me.groupBackground.Name = "groupBackground"
        Me.groupBackground.Size = New System.Drawing.Size(451, 313)
        Me.groupBackground.TabIndex = 7
        Me.groupBackground.TabStop = False
        Me.groupBackground.Text = "Background"
        '
        'chkBackgroundFullSize
        '
        Me.chkBackgroundFullSize.AutoSize = True
        Me.chkBackgroundFullSize.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBackgroundFullSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBackgroundFullSize.Location = New System.Drawing.Point(17, 226)
        Me.chkBackgroundFullSize.Name = "chkBackgroundFullSize"
        Me.chkBackgroundFullSize.Size = New System.Drawing.Size(147, 20)
        Me.chkBackgroundFullSize.TabIndex = 28
        Me.chkBackgroundFullSize.Text = "Background fullsized:"
        Me.chkBackgroundFullSize.UseVisualStyleBackColor = True
        '
        'TxtBackgroundPath
        '
        Me.TxtBackgroundPath.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBackgroundPath.Location = New System.Drawing.Point(152, 186)
        Me.TxtBackgroundPath.Name = "TxtBackgroundPath"
        Me.TxtBackgroundPath.Size = New System.Drawing.Size(290, 23)
        Me.TxtBackgroundPath.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 186)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 20)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Background picture:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackgroundScreenScale
        '
        Me.txtBackgroundScreenScale.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackgroundScreenScale.Location = New System.Drawing.Point(287, 94)
        Me.txtBackgroundScreenScale.Name = "txtBackgroundScreenScale"
        Me.txtBackgroundScreenScale.ReadOnly = True
        Me.txtBackgroundScreenScale.Size = New System.Drawing.Size(93, 23)
        Me.txtBackgroundScreenScale.TabIndex = 25
        '
        'lblBackgroundLocationY
        '
        Me.lblBackgroundLocationY.AutoSize = True
        Me.lblBackgroundLocationY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackgroundLocationY.Location = New System.Drawing.Point(210, 127)
        Me.lblBackgroundLocationY.Name = "lblBackgroundLocationY"
        Me.lblBackgroundLocationY.Size = New System.Drawing.Size(11, 16)
        Me.lblBackgroundLocationY.TabIndex = 22
        Me.lblBackgroundLocationY.Text = ","
        Me.lblBackgroundLocationY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackgroundLocationY
        '
        Me.txtBackgroundLocationY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackgroundLocationY.Location = New System.Drawing.Point(227, 123)
        Me.txtBackgroundLocationY.Name = "txtBackgroundLocationY"
        Me.txtBackgroundLocationY.Size = New System.Drawing.Size(54, 23)
        Me.txtBackgroundLocationY.TabIndex = 4
        '
        'txtBackgroundLocationX
        '
        Me.txtBackgroundLocationX.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackgroundLocationX.Location = New System.Drawing.Point(152, 123)
        Me.txtBackgroundLocationX.Name = "txtBackgroundLocationX"
        Me.txtBackgroundLocationX.Size = New System.Drawing.Size(54, 23)
        Me.txtBackgroundLocationX.TabIndex = 3
        '
        'lblBackgroundLocationX
        '
        Me.lblBackgroundLocationX.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackgroundLocationX.Location = New System.Drawing.Point(12, 125)
        Me.lblBackgroundLocationX.Name = "lblBackgroundLocationX"
        Me.lblBackgroundLocationX.Size = New System.Drawing.Size(134, 20)
        Me.lblBackgroundLocationX.TabIndex = 19
        Me.lblBackgroundLocationX.Text = "Background location:"
        Me.lblBackgroundLocationX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBackgroundScreenSizeHeight
        '
        Me.lblBackgroundScreenSizeHeight.AutoSize = True
        Me.lblBackgroundScreenSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackgroundScreenSizeHeight.Location = New System.Drawing.Point(210, 98)
        Me.lblBackgroundScreenSizeHeight.Name = "lblBackgroundScreenSizeHeight"
        Me.lblBackgroundScreenSizeHeight.Size = New System.Drawing.Size(11, 16)
        Me.lblBackgroundScreenSizeHeight.TabIndex = 18
        Me.lblBackgroundScreenSizeHeight.Text = ","
        Me.lblBackgroundScreenSizeHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackgroundScreenSizeHeight
        '
        Me.txtBackgroundScreenSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackgroundScreenSizeHeight.Location = New System.Drawing.Point(227, 94)
        Me.txtBackgroundScreenSizeHeight.Name = "txtBackgroundScreenSizeHeight"
        Me.txtBackgroundScreenSizeHeight.ReadOnly = True
        Me.txtBackgroundScreenSizeHeight.Size = New System.Drawing.Size(54, 23)
        Me.txtBackgroundScreenSizeHeight.TabIndex = 2
        '
        'txtBackgroundScreenSizeWidth
        '
        Me.txtBackgroundScreenSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackgroundScreenSizeWidth.Location = New System.Drawing.Point(152, 94)
        Me.txtBackgroundScreenSizeWidth.Name = "txtBackgroundScreenSizeWidth"
        Me.txtBackgroundScreenSizeWidth.ReadOnly = True
        Me.txtBackgroundScreenSizeWidth.Size = New System.Drawing.Size(54, 23)
        Me.txtBackgroundScreenSizeWidth.TabIndex = 1
        '
        'lblBackgroundScreenSizeWidth
        '
        Me.lblBackgroundScreenSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackgroundScreenSizeWidth.Location = New System.Drawing.Point(12, 96)
        Me.lblBackgroundScreenSizeWidth.Name = "lblBackgroundScreenSizeWidth"
        Me.lblBackgroundScreenSizeWidth.Size = New System.Drawing.Size(134, 20)
        Me.lblBackgroundScreenSizeWidth.TabIndex = 15
        Me.lblBackgroundScreenSizeWidth.Text = "Current screen size:"
        Me.lblBackgroundScreenSizeWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBackgroundSizeHeight
        '
        Me.lblBackgroundSizeHeight.AutoSize = True
        Me.lblBackgroundSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackgroundSizeHeight.Location = New System.Drawing.Point(210, 156)
        Me.lblBackgroundSizeHeight.Name = "lblBackgroundSizeHeight"
        Me.lblBackgroundSizeHeight.Size = New System.Drawing.Size(11, 16)
        Me.lblBackgroundSizeHeight.TabIndex = 13
        Me.lblBackgroundSizeHeight.Text = ","
        Me.lblBackgroundSizeHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackgroundSizeHeight
        '
        Me.txtBackgroundSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackgroundSizeHeight.Location = New System.Drawing.Point(227, 152)
        Me.txtBackgroundSizeHeight.Name = "txtBackgroundSizeHeight"
        Me.txtBackgroundSizeHeight.Size = New System.Drawing.Size(54, 23)
        Me.txtBackgroundSizeHeight.TabIndex = 6
        '
        'txtBackgroundSizeWidth
        '
        Me.txtBackgroundSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackgroundSizeWidth.Location = New System.Drawing.Point(152, 152)
        Me.txtBackgroundSizeWidth.Name = "txtBackgroundSizeWidth"
        Me.txtBackgroundSizeWidth.Size = New System.Drawing.Size(54, 23)
        Me.txtBackgroundSizeWidth.TabIndex = 5
        '
        'lblBackgroundSize
        '
        Me.lblBackgroundSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackgroundSize.Location = New System.Drawing.Point(12, 154)
        Me.lblBackgroundSize.Name = "lblBackgroundSize"
        Me.lblBackgroundSize.Size = New System.Drawing.Size(134, 20)
        Me.lblBackgroundSize.TabIndex = 10
        Me.lblBackgroundSize.Text = "Background size:"
        Me.lblBackgroundSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackgroundScreen
        '
        Me.txtBackgroundScreen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackgroundScreen.Location = New System.Drawing.Point(152, 65)
        Me.txtBackgroundScreen.Name = "txtBackgroundScreen"
        Me.txtBackgroundScreen.ReadOnly = True
        Me.txtBackgroundScreen.Size = New System.Drawing.Size(217, 23)
        Me.txtBackgroundScreen.TabIndex = 0
        '
        'lblBackgroundScreen
        '
        Me.lblBackgroundScreen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackgroundScreen.Location = New System.Drawing.Point(12, 65)
        Me.lblBackgroundScreen.Name = "lblBackgroundScreen"
        Me.lblBackgroundScreen.Size = New System.Drawing.Size(134, 20)
        Me.lblBackgroundScreen.TabIndex = 8
        Me.lblBackgroundScreen.Text = "Current screen:"
        Me.lblBackgroundScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBackground
        '
        Me.lblBackground.AutoSize = True
        Me.lblBackground.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackground.Location = New System.Drawing.Point(14, 23)
        Me.lblBackground.Name = "lblBackground"
        Me.lblBackground.Size = New System.Drawing.Size(428, 32)
        Me.lblBackground.TabIndex = 5
        Me.lblBackground.Text = "This is the dummy window for the Background. Drag and drop me" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "whereever you want" &
    " to have me."
        '
        'formBackground
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(468, 329)
        Me.ControlBox = False
        Me.Controls.Add(Me.groupBackground)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "formBackground"
        Me.Opacity = 0.75R
        Me.Text = "Background - B2S Screen Resolution Identifier"
        Me.TopMost = True
        Me.groupBackground.ResumeLayout(False)
        Me.groupBackground.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents groupBackground As System.Windows.Forms.GroupBox
    Friend WithEvents lblBackgroundLocationY As System.Windows.Forms.Label
    Friend WithEvents txtBackgroundLocationY As B2STextBox
    Friend WithEvents txtBackgroundLocationX As B2STextBox
    Friend WithEvents lblBackgroundLocationX As System.Windows.Forms.Label
    Friend WithEvents lblBackgroundScreenSizeHeight As System.Windows.Forms.Label
    Friend WithEvents txtBackgroundScreenSizeHeight As B2STextBox
    Friend WithEvents txtBackgroundScreenSizeWidth As B2STextBox
    Friend WithEvents lblBackgroundScreenSizeWidth As System.Windows.Forms.Label
    Friend WithEvents lblBackgroundSizeHeight As System.Windows.Forms.Label
    Friend WithEvents txtBackgroundSizeHeight As B2STextBox
    Friend WithEvents txtBackgroundSizeWidth As B2STextBox
    Friend WithEvents lblBackgroundSize As System.Windows.Forms.Label
    Friend WithEvents txtBackgroundScreen As System.Windows.Forms.TextBox
    Friend WithEvents lblBackgroundScreen As System.Windows.Forms.Label
    Friend WithEvents lblBackground As System.Windows.Forms.Label
    Friend WithEvents txtBackgroundScreenScale As TextBox
    Friend WithEvents TxtBackgroundPath As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkBackgroundFullSize As CheckBox
End Class
