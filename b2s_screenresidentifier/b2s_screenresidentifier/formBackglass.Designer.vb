﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formBackglass
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formBackglass))
        Me.groupBackglass = New System.Windows.Forms.GroupBox()
        Me.chkBackgroundActive = New System.Windows.Forms.CheckBox()
        Me.txtBackglassScreenScale = New System.Windows.Forms.TextBox()
        Me.chkBackglassGrillVisible = New System.Windows.Forms.CheckBox()
        Me.lblBackglassLocationY = New System.Windows.Forms.Label()
        Me.txtBackglassLocationY = New B2STextBox()
        Me.txtBackglassLocationX = New B2STextBox()
        Me.lblBackglassLocationX = New System.Windows.Forms.Label()
        Me.lblBackglassScreenSizeHeight = New System.Windows.Forms.Label()
        Me.txtBackglassScreenSizeHeight = New B2STextBox()
        Me.txtBackglassScreenSizeWidth = New B2STextBox()
        Me.lblBackglassScreenSizeWidth = New System.Windows.Forms.Label()
        Me.chkBackglassFullSize = New System.Windows.Forms.CheckBox()
        Me.lblBackglassSizeHeight = New System.Windows.Forms.Label()
        Me.txtBackglassSizeHeight = New B2STextBox()
        Me.txtBackglassSizeWidth = New B2STextBox()
        Me.lblBackglassSize = New System.Windows.Forms.Label()
        Me.txtBackglassScreen = New System.Windows.Forms.TextBox()
        Me.lblBackglassScreen = New System.Windows.Forms.Label()
        Me.lblBackglass = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.button_ScreenSwitch = New System.Windows.Forms.Button()
        Me.groupBackglass.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBackglass
        '
        Me.groupBackglass.Controls.Add(Me.Label1)
        Me.groupBackglass.Controls.Add(Me.chkBackgroundActive)
        Me.groupBackglass.Controls.Add(Me.button_ScreenSwitch)
        Me.groupBackglass.Controls.Add(Me.txtBackglassScreenScale)
        Me.groupBackglass.Controls.Add(Me.chkBackglassGrillVisible)
        Me.groupBackglass.Controls.Add(Me.lblBackglassLocationY)
        Me.groupBackglass.Controls.Add(Me.txtBackglassLocationY)
        Me.groupBackglass.Controls.Add(Me.txtBackglassLocationX)
        Me.groupBackglass.Controls.Add(Me.lblBackglassLocationX)
        Me.groupBackglass.Controls.Add(Me.lblBackglassScreenSizeHeight)
        Me.groupBackglass.Controls.Add(Me.txtBackglassScreenSizeHeight)
        Me.groupBackglass.Controls.Add(Me.txtBackglassScreenSizeWidth)
        Me.groupBackglass.Controls.Add(Me.lblBackglassScreenSizeWidth)
        Me.groupBackglass.Controls.Add(Me.chkBackglassFullSize)
        Me.groupBackglass.Controls.Add(Me.lblBackglassSizeHeight)
        Me.groupBackglass.Controls.Add(Me.txtBackglassSizeHeight)
        Me.groupBackglass.Controls.Add(Me.txtBackglassSizeWidth)
        Me.groupBackglass.Controls.Add(Me.lblBackglassSize)
        Me.groupBackglass.Controls.Add(Me.txtBackglassScreen)
        Me.groupBackglass.Controls.Add(Me.lblBackglassScreen)
        Me.groupBackglass.Controls.Add(Me.lblBackglass)
        Me.groupBackglass.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupBackglass.Location = New System.Drawing.Point(7, 5)
        Me.groupBackglass.Name = "groupBackglass"
        Me.groupBackglass.Size = New System.Drawing.Size(443, 321)
        Me.groupBackglass.TabIndex = 6
        Me.groupBackglass.TabStop = False
        Me.groupBackglass.Text = "Backglass"
        '
        'chkBackgroundActive
        '
        Me.chkBackgroundActive.AutoSize = True
        Me.chkBackgroundActive.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBackgroundActive.Location = New System.Drawing.Point(147, 295)
        Me.chkBackgroundActive.Name = "chkBackgroundActive"
        Me.chkBackgroundActive.Size = New System.Drawing.Size(141, 20)
        Me.chkBackgroundActive.TabIndex = 26
        Me.chkBackgroundActive.Text = "Activate background"
        Me.chkBackgroundActive.UseVisualStyleBackColor = True
        '
        'txtBackglassScreenScale
        '
        Me.txtBackglassScreenScale.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackglassScreenScale.Location = New System.Drawing.Point(282, 141)
        Me.txtBackglassScreenScale.Name = "txtBackglassScreenScale"
        Me.txtBackglassScreenScale.ReadOnly = True
        Me.txtBackglassScreenScale.Size = New System.Drawing.Size(93, 23)
        Me.txtBackglassScreenScale.TabIndex = 25
        '
        'chkBackglassGrillVisible
        '
        Me.chkBackglassGrillVisible.AutoSize = True
        Me.chkBackglassGrillVisible.Enabled = False
        Me.chkBackglassGrillVisible.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBackglassGrillVisible.Location = New System.Drawing.Point(147, 254)
        Me.chkBackglassGrillVisible.Name = "chkBackglassGrillVisible"
        Me.chkBackglassGrillVisible.Size = New System.Drawing.Size(254, 20)
        Me.chkBackglassGrillVisible.TabIndex = 8
        Me.chkBackglassGrillVisible.Text = "Backglass grill is visible because of DMD"
        Me.chkBackglassGrillVisible.UseVisualStyleBackColor = True
        '
        'lblBackglassLocationY
        '
        Me.lblBackglassLocationY.AutoSize = True
        Me.lblBackglassLocationY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackglassLocationY.Location = New System.Drawing.Point(205, 174)
        Me.lblBackglassLocationY.Name = "lblBackglassLocationY"
        Me.lblBackglassLocationY.Size = New System.Drawing.Size(11, 16)
        Me.lblBackglassLocationY.TabIndex = 22
        Me.lblBackglassLocationY.Text = ","
        Me.lblBackglassLocationY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackglassLocationY
        '
        Me.txtBackglassLocationY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackglassLocationY.Location = New System.Drawing.Point(222, 170)
        Me.txtBackglassLocationY.Name = "txtBackglassLocationY"
        Me.txtBackglassLocationY.Size = New System.Drawing.Size(54, 23)
        Me.txtBackglassLocationY.TabIndex = 4
        '
        'txtBackglassLocationX
        '
        Me.txtBackglassLocationX.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackglassLocationX.Location = New System.Drawing.Point(147, 170)
        Me.txtBackglassLocationX.Name = "txtBackglassLocationX"
        Me.txtBackglassLocationX.Size = New System.Drawing.Size(54, 23)
        Me.txtBackglassLocationX.TabIndex = 3
        '
        'lblBackglassLocationX
        '
        Me.lblBackglassLocationX.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackglassLocationX.Location = New System.Drawing.Point(7, 170)
        Me.lblBackglassLocationX.Name = "lblBackglassLocationX"
        Me.lblBackglassLocationX.Size = New System.Drawing.Size(134, 20)
        Me.lblBackglassLocationX.TabIndex = 19
        Me.lblBackglassLocationX.Text = "Backglass location:"
        Me.lblBackglassLocationX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBackglassScreenSizeHeight
        '
        Me.lblBackglassScreenSizeHeight.AutoSize = True
        Me.lblBackglassScreenSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackglassScreenSizeHeight.Location = New System.Drawing.Point(205, 145)
        Me.lblBackglassScreenSizeHeight.Name = "lblBackglassScreenSizeHeight"
        Me.lblBackglassScreenSizeHeight.Size = New System.Drawing.Size(11, 16)
        Me.lblBackglassScreenSizeHeight.TabIndex = 18
        Me.lblBackglassScreenSizeHeight.Text = ","
        Me.lblBackglassScreenSizeHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackglassScreenSizeHeight
        '
        Me.txtBackglassScreenSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackglassScreenSizeHeight.Location = New System.Drawing.Point(222, 141)
        Me.txtBackglassScreenSizeHeight.Name = "txtBackglassScreenSizeHeight"
        Me.txtBackglassScreenSizeHeight.ReadOnly = True
        Me.txtBackglassScreenSizeHeight.Size = New System.Drawing.Size(54, 23)
        Me.txtBackglassScreenSizeHeight.TabIndex = 2
        '
        'txtBackglassScreenSizeWidth
        '
        Me.txtBackglassScreenSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackglassScreenSizeWidth.Location = New System.Drawing.Point(147, 141)
        Me.txtBackglassScreenSizeWidth.Name = "txtBackglassScreenSizeWidth"
        Me.txtBackglassScreenSizeWidth.ReadOnly = True
        Me.txtBackglassScreenSizeWidth.Size = New System.Drawing.Size(54, 23)
        Me.txtBackglassScreenSizeWidth.TabIndex = 1
        '
        'lblBackglassScreenSizeWidth
        '
        Me.lblBackglassScreenSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackglassScreenSizeWidth.Location = New System.Drawing.Point(7, 141)
        Me.lblBackglassScreenSizeWidth.Name = "lblBackglassScreenSizeWidth"
        Me.lblBackglassScreenSizeWidth.Size = New System.Drawing.Size(134, 20)
        Me.lblBackglassScreenSizeWidth.TabIndex = 15
        Me.lblBackglassScreenSizeWidth.Text = "Current screen size:"
        Me.lblBackglassScreenSizeWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkBackglassFullSize
        '
        Me.chkBackglassFullSize.AutoSize = True
        Me.chkBackglassFullSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBackglassFullSize.Location = New System.Drawing.Point(147, 228)
        Me.chkBackglassFullSize.Name = "chkBackglassFullSize"
        Me.chkBackglassFullSize.Size = New System.Drawing.Size(144, 20)
        Me.chkBackglassFullSize.TabIndex = 7
        Me.chkBackglassFullSize.Text = "Backglass is fullsized"
        Me.chkBackglassFullSize.UseVisualStyleBackColor = True
        '
        'lblBackglassSizeHeight
        '
        Me.lblBackglassSizeHeight.AutoSize = True
        Me.lblBackglassSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackglassSizeHeight.Location = New System.Drawing.Point(205, 203)
        Me.lblBackglassSizeHeight.Name = "lblBackglassSizeHeight"
        Me.lblBackglassSizeHeight.Size = New System.Drawing.Size(11, 16)
        Me.lblBackglassSizeHeight.TabIndex = 13
        Me.lblBackglassSizeHeight.Text = ","
        Me.lblBackglassSizeHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackglassSizeHeight
        '
        Me.txtBackglassSizeHeight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackglassSizeHeight.Location = New System.Drawing.Point(222, 199)
        Me.txtBackglassSizeHeight.Name = "txtBackglassSizeHeight"
        Me.txtBackglassSizeHeight.Size = New System.Drawing.Size(54, 23)
        Me.txtBackglassSizeHeight.TabIndex = 6
        '
        'txtBackglassSizeWidth
        '
        Me.txtBackglassSizeWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackglassSizeWidth.Location = New System.Drawing.Point(147, 199)
        Me.txtBackglassSizeWidth.Name = "txtBackglassSizeWidth"
        Me.txtBackglassSizeWidth.Size = New System.Drawing.Size(54, 23)
        Me.txtBackglassSizeWidth.TabIndex = 5
        '
        'lblBackglassSize
        '
        Me.lblBackglassSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackglassSize.Location = New System.Drawing.Point(7, 199)
        Me.lblBackglassSize.Name = "lblBackglassSize"
        Me.lblBackglassSize.Size = New System.Drawing.Size(134, 20)
        Me.lblBackglassSize.TabIndex = 10
        Me.lblBackglassSize.Text = "Backglass size:"
        Me.lblBackglassSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBackglassScreen
        '
        Me.txtBackglassScreen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackglassScreen.Location = New System.Drawing.Point(147, 112)
        Me.txtBackglassScreen.Name = "txtBackglassScreen"
        Me.txtBackglassScreen.ReadOnly = True
        Me.txtBackglassScreen.Size = New System.Drawing.Size(217, 23)
        Me.txtBackglassScreen.TabIndex = 0
        '
        'lblBackglassScreen
        '
        Me.lblBackglassScreen.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackglassScreen.Location = New System.Drawing.Point(7, 112)
        Me.lblBackglassScreen.Name = "lblBackglassScreen"
        Me.lblBackglassScreen.Size = New System.Drawing.Size(134, 20)
        Me.lblBackglassScreen.TabIndex = 8
        Me.lblBackglassScreen.Text = "Current screen:"
        Me.lblBackglassScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBackglass
        '
        Me.lblBackglass.AutoSize = True
        Me.lblBackglass.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackglass.Location = New System.Drawing.Point(14, 23)
        Me.lblBackglass.Name = "lblBackglass"
        Me.lblBackglass.Size = New System.Drawing.Size(419, 80)
        Me.lblBackglass.TabIndex = 5
        Me.lblBackglass.Text = resources.GetString("lblBackglass.Text")
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(277, 165)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 55)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "}"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'button_ScreenSwitch
        '
        Me.button_ScreenSwitch.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button_ScreenSwitch.Location = New System.Drawing.Point(311, 184)
        Me.button_ScreenSwitch.Name = "button_ScreenSwitch"
        Me.button_ScreenSwitch.Size = New System.Drawing.Size(122, 25)
        Me.button_ScreenSwitch.TabIndex = 29
        Me.button_ScreenSwitch.Text = "<-> Background"
        Me.button_ScreenSwitch.UseVisualStyleBackColor = True
        '
        'formBackglass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(460, 338)
        Me.Controls.Add(Me.groupBackglass)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimizeBox = False
        Me.Name = "formBackglass"
        Me.Opacity = 0.8R
        Me.Text = "Backglass - B2S Screen Resolution Identifier"
        Me.TopMost = True
        Me.groupBackglass.ResumeLayout(False)
        Me.groupBackglass.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents groupBackglass As System.Windows.Forms.GroupBox
    Friend WithEvents lblBackglassLocationY As System.Windows.Forms.Label
    Friend WithEvents txtBackglassLocationY As B2STextBox
    Friend WithEvents txtBackglassLocationX As B2STextBox
    Friend WithEvents lblBackglassLocationX As System.Windows.Forms.Label
    Friend WithEvents lblBackglassScreenSizeHeight As System.Windows.Forms.Label
    Friend WithEvents txtBackglassScreenSizeHeight As B2STextBox
    Friend WithEvents txtBackglassScreenSizeWidth As B2STextBox
    Friend WithEvents lblBackglassScreenSizeWidth As System.Windows.Forms.Label
    Friend WithEvents chkBackglassFullSize As System.Windows.Forms.CheckBox
    Friend WithEvents lblBackglassSizeHeight As System.Windows.Forms.Label
    Friend WithEvents txtBackglassSizeHeight As B2STextBox
    Friend WithEvents txtBackglassSizeWidth As B2STextBox
    Friend WithEvents lblBackglassSize As System.Windows.Forms.Label
    Friend WithEvents txtBackglassScreen As System.Windows.Forms.TextBox
    Friend WithEvents lblBackglassScreen As System.Windows.Forms.Label
    Friend WithEvents lblBackglass As System.Windows.Forms.Label
    Friend WithEvents chkBackglassGrillVisible As System.Windows.Forms.CheckBox
    Friend WithEvents txtBackglassScreenScale As TextBox
    Friend WithEvents chkBackgroundActive As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents button_ScreenSwitch As Button
End Class
