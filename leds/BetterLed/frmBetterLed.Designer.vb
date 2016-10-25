<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBetterLed
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
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.TrackBar2 = New System.Windows.Forms.TrackBar()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TrackBar3 = New System.Windows.Forms.TrackBar()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TrackBar4 = New System.Windows.Forms.TrackBar()
        Me.TrackBar5 = New System.Windows.Forms.TrackBar()
        Me.TrackBar6 = New System.Windows.Forms.TrackBar()
        Me.TrackBar7 = New System.Windows.Forms.TrackBar()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TrackBar8 = New System.Windows.Forms.TrackBar()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.SegmentDisplay3 = New Dream7Display.Dream7Display()
        Me.SegmentDisplay1 = New Dream7Display.Dream7Display()
        Me.SegmentDisplay2 = New Dream7Display.Dream7Display()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(114, 335)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(69, 21)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Licht Farbe"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(203, 336)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(121, 20)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Text = "08912345678"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(330, 325)
        Me.TrackBar1.Maximum = 100
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(351, 45)
        Me.TrackBar1.TabIndex = 3
        Me.TrackBar1.Value = 10
        '
        'TrackBar2
        '
        Me.TrackBar2.Location = New System.Drawing.Point(330, 376)
        Me.TrackBar2.Maximum = 100
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(351, 45)
        Me.TrackBar2.TabIndex = 4
        Me.TrackBar2.Value = 50
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"7", "10", "14"})
        Me.ComboBox1.Location = New System.Drawing.Point(203, 376)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 5
        Me.ComboBox1.Text = "7"
        '
        'TrackBar3
        '
        Me.TrackBar3.Location = New System.Drawing.Point(330, 427)
        Me.TrackBar3.Maximum = 100
        Me.TrackBar3.Name = "TrackBar3"
        Me.TrackBar3.Size = New System.Drawing.Size(351, 45)
        Me.TrackBar3.TabIndex = 6
        Me.TrackBar3.Value = 20
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(114, 362)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(69, 21)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Glas Farbe"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TrackBar4
        '
        Me.TrackBar4.Location = New System.Drawing.Point(23, 185)
        Me.TrackBar4.Maximum = 255
        Me.TrackBar4.Name = "TrackBar4"
        Me.TrackBar4.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar4.Size = New System.Drawing.Size(45, 287)
        Me.TrackBar4.TabIndex = 8
        Me.TrackBar4.Value = 140
        '
        'TrackBar5
        '
        Me.TrackBar5.Location = New System.Drawing.Point(23, 12)
        Me.TrackBar5.Maximum = 30
        Me.TrackBar5.Name = "TrackBar5"
        Me.TrackBar5.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar5.Size = New System.Drawing.Size(45, 167)
        Me.TrackBar5.TabIndex = 9
        Me.TrackBar5.Value = 15
        '
        'TrackBar6
        '
        Me.TrackBar6.Location = New System.Drawing.Point(63, 185)
        Me.TrackBar6.Maximum = 255
        Me.TrackBar6.Name = "TrackBar6"
        Me.TrackBar6.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar6.Size = New System.Drawing.Size(45, 287)
        Me.TrackBar6.TabIndex = 10
        Me.TrackBar6.Value = 255
        '
        'TrackBar7
        '
        Me.TrackBar7.Location = New System.Drawing.Point(741, 427)
        Me.TrackBar7.Maximum = 180
        Me.TrackBar7.Minimum = -180
        Me.TrackBar7.Name = "TrackBar7"
        Me.TrackBar7.Size = New System.Drawing.Size(351, 45)
        Me.TrackBar7.TabIndex = 11
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"Manual", "Stretch", "Zoom"})
        Me.ComboBox2.Location = New System.Drawing.Point(774, 349)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox2.TabIndex = 12
        Me.ComboBox2.Text = "Stretch"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(207, 414)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(117, 20)
        Me.TextBox2.TabIndex = 14
        Me.TextBox2.Text = "20534"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(207, 440)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(64, 17)
        Me.CheckBox1.TabIndex = 16
        Me.CheckBox1.Text = "Mirrored"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(114, 410)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(53, 47)
        Me.Button3.TabIndex = 17
        Me.Button3.Text = "Test"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TrackBar8
        '
        Me.TrackBar8.Location = New System.Drawing.Point(712, 387)
        Me.TrackBar8.Maximum = 25
        Me.TrackBar8.Minimum = 10
        Me.TrackBar8.Name = "TrackBar8"
        Me.TrackBar8.Size = New System.Drawing.Size(351, 45)
        Me.TrackBar8.TabIndex = 23
        Me.TrackBar8.Value = 10
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(107, 486)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(76, 20)
        Me.TextBox3.TabIndex = 24
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(107, 462)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(76, 20)
        Me.TextBox4.TabIndex = 25
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(207, 465)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(69, 17)
        Me.CheckBox2.TabIndex = 27
        Me.CheckBox2.Text = "Use Bulb"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(207, 489)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(74, 17)
        Me.CheckBox3.TabIndex = 28
        Me.CheckBox3.Text = "Wireframe"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        '
        'SegmentDisplay3
        '
        Me.SegmentDisplay3.Angle = 15.0!
        Me.SegmentDisplay3.Digits = 1
        Me.SegmentDisplay3.Location = New System.Drawing.Point(1136, 335)
        Me.SegmentDisplay3.Name = "SegmentDisplay3"
        Me.SegmentDisplay3.ScaleMode = Dream7Display.ScaleMode.Stretch
        Me.SegmentDisplay3.Size = New System.Drawing.Size(282, 407)
        Me.SegmentDisplay3.TabIndex = 26
        Me.SegmentDisplay3.Text = " "
        Me.SegmentDisplay3.TransparentBackground = True
        Me.SegmentDisplay3.Type = Dream7Display.SegmentNumberType.FourteenSegment
        '
        'SegmentDisplay1
        '
        Me.SegmentDisplay1.BackColor = System.Drawing.Color.White
        Me.SegmentDisplay1.Digits = 15
        Me.SegmentDisplay1.Location = New System.Drawing.Point(97, 26)
        Me.SegmentDisplay1.Name = "SegmentDisplay1"
        Me.SegmentDisplay1.OffsetWidth = -10
        Me.SegmentDisplay1.ScaleMode = Dream7Display.ScaleMode.Stretch
        Me.SegmentDisplay1.Size = New System.Drawing.Size(1321, 143)
        Me.SegmentDisplay1.TabIndex = 21
        Me.SegmentDisplay1.Text = "089.12"
        '
        'SegmentDisplay2
        '
        Me.SegmentDisplay2.BackColor = System.Drawing.Color.White
        Me.SegmentDisplay2.Digits = 15
        Me.SegmentDisplay2.Location = New System.Drawing.Point(97, 183)
        Me.SegmentDisplay2.Name = "SegmentDisplay2"
        Me.SegmentDisplay2.OffsetWidth = 2
        Me.SegmentDisplay2.ScaleMode = Dream7Display.ScaleMode.Stretch
        Me.SegmentDisplay2.Shear = 0.3!
        Me.SegmentDisplay2.Size = New System.Drawing.Size(1127, 136)
        Me.SegmentDisplay2.TabIndex = 18
        Me.SegmentDisplay2.Text = "089.12"
        '
        'frmBetterLed
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1443, 754)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.SegmentDisplay3)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TrackBar8)
        Me.Controls.Add(Me.SegmentDisplay1)
        Me.Controls.Add(Me.SegmentDisplay2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.TrackBar7)
        Me.Controls.Add(Me.TrackBar6)
        Me.Controls.Add(Me.TrackBar5)
        Me.Controls.Add(Me.TrackBar4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TrackBar3)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TrackBar2)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.KeyPreview = True
        Me.Name = "frmBetterLed"
        Me.Text = "BetterLed"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TrackBar3 As System.Windows.Forms.TrackBar
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TrackBar4 As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar5 As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar6 As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar7 As System.Windows.Forms.TrackBar
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents SegmentDisplay1 As Dream7Display.Dream7Display
    Friend WithEvents TrackBar8 As System.Windows.Forms.TrackBar
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents SegmentDisplay2 As Dream7Display.Dream7Display
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents SegmentDisplay3 As Dream7Display.Dream7Display
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
End Class
