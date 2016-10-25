<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formSettingsMore
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
        Me.grpLogging = New System.Windows.Forms.GroupBox()
        Me.chkStatisticBackglass = New System.Windows.Forms.CheckBox()
        Me.chkLogLEDs = New System.Windows.Forms.CheckBox()
        Me.btnLogPath = New System.Windows.Forms.Button()
        Me.chkLogSolenoids = New System.Windows.Forms.CheckBox()
        Me.chkLogGIStrings = New System.Windows.Forms.CheckBox()
        Me.chkLogLamps = New System.Windows.Forms.CheckBox()
        Me.grpPerfTests = New System.Windows.Forms.GroupBox()
        Me.chkAllOut = New System.Windows.Forms.CheckBox()
        Me.chkAllOff = New System.Windows.Forms.CheckBox()
        Me.chkLampsOff = New System.Windows.Forms.CheckBox()
        Me.chkSolenoidsOff = New System.Windows.Forms.CheckBox()
        Me.chkGIStringsOff = New System.Windows.Forms.CheckBox()
        Me.chkLEDsOff = New System.Windows.Forms.CheckBox()
        Me.btnCloseSettings = New System.Windows.Forms.Button()
        Me.grpLogging.SuspendLayout()
        Me.grpPerfTests.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpLogging
        '
        Me.grpLogging.Controls.Add(Me.chkStatisticBackglass)
        Me.grpLogging.Controls.Add(Me.chkLogLEDs)
        Me.grpLogging.Controls.Add(Me.btnLogPath)
        Me.grpLogging.Controls.Add(Me.chkLogSolenoids)
        Me.grpLogging.Controls.Add(Me.chkLogGIStrings)
        Me.grpLogging.Controls.Add(Me.chkLogLamps)
        Me.grpLogging.Location = New System.Drawing.Point(8, 95)
        Me.grpLogging.Name = "grpLogging"
        Me.grpLogging.Size = New System.Drawing.Size(474, 74)
        Me.grpLogging.TabIndex = 10
        Me.grpLogging.TabStop = False
        Me.grpLogging.Text = "Logging"
        '
        'chkStatisticBackglass
        '
        Me.chkStatisticBackglass.AutoSize = True
        Me.chkStatisticBackglass.Location = New System.Drawing.Point(240, 25)
        Me.chkStatisticBackglass.Name = "chkStatisticBackglass"
        Me.chkStatisticBackglass.Size = New System.Drawing.Size(209, 17)
        Me.chkStatisticBackglass.TabIndex = 27
        Me.chkStatisticBackglass.Text = "Show statistics backglass after restart"
        Me.chkStatisticBackglass.UseVisualStyleBackColor = True
        '
        'chkLogLEDs
        '
        Me.chkLogLEDs.AutoSize = True
        Me.chkLogLEDs.Location = New System.Drawing.Point(357, 50)
        Me.chkLogLEDs.Name = "chkLogLEDs"
        Me.chkLogLEDs.Size = New System.Drawing.Size(69, 17)
        Me.chkLogLEDs.TabIndex = 31
        Me.chkLogLEDs.Text = "Log LEDs"
        Me.chkLogLEDs.UseVisualStyleBackColor = True
        '
        'btnLogPath
        '
        Me.btnLogPath.Location = New System.Drawing.Point(8, 20)
        Me.btnLogPath.Name = "btnLogPath"
        Me.btnLogPath.Size = New System.Drawing.Size(226, 24)
        Me.btnLogPath.TabIndex = 26
        Me.btnLogPath.Text = "Log path"
        Me.btnLogPath.UseVisualStyleBackColor = True
        '
        'chkLogSolenoids
        '
        Me.chkLogSolenoids.AutoSize = True
        Me.chkLogSolenoids.Location = New System.Drawing.Point(124, 50)
        Me.chkLogSolenoids.Name = "chkLogSolenoids"
        Me.chkLogSolenoids.Size = New System.Drawing.Size(90, 17)
        Me.chkLogSolenoids.TabIndex = 29
        Me.chkLogSolenoids.Text = "Log solenoids"
        Me.chkLogSolenoids.UseVisualStyleBackColor = True
        '
        'chkLogGIStrings
        '
        Me.chkLogGIStrings.AutoSize = True
        Me.chkLogGIStrings.Location = New System.Drawing.Point(240, 50)
        Me.chkLogGIStrings.Name = "chkLogGIStrings"
        Me.chkLogGIStrings.Size = New System.Drawing.Size(92, 17)
        Me.chkLogGIStrings.TabIndex = 30
        Me.chkLogGIStrings.Text = "Log GI strings"
        Me.chkLogGIStrings.UseVisualStyleBackColor = True
        '
        'chkLogLamps
        '
        Me.chkLogLamps.AutoSize = True
        Me.chkLogLamps.Location = New System.Drawing.Point(8, 50)
        Me.chkLogLamps.Name = "chkLogLamps"
        Me.chkLogLamps.Size = New System.Drawing.Size(73, 17)
        Me.chkLogLamps.TabIndex = 28
        Me.chkLogLamps.Text = "Log lamps"
        Me.chkLogLamps.UseVisualStyleBackColor = True
        '
        'grpPerfTests
        '
        Me.grpPerfTests.Controls.Add(Me.chkAllOut)
        Me.grpPerfTests.Controls.Add(Me.chkAllOff)
        Me.grpPerfTests.Controls.Add(Me.chkLampsOff)
        Me.grpPerfTests.Controls.Add(Me.chkSolenoidsOff)
        Me.grpPerfTests.Controls.Add(Me.chkGIStringsOff)
        Me.grpPerfTests.Controls.Add(Me.chkLEDsOff)
        Me.grpPerfTests.Location = New System.Drawing.Point(8, 7)
        Me.grpPerfTests.Name = "grpPerfTests"
        Me.grpPerfTests.Size = New System.Drawing.Size(474, 85)
        Me.grpPerfTests.TabIndex = 9
        Me.grpPerfTests.TabStop = False
        Me.grpPerfTests.Text = "Performance tests"
        '
        'chkAllOut
        '
        Me.chkAllOut.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkAllOut.Location = New System.Drawing.Point(6, 20)
        Me.chkAllOut.Name = "chkAllOut"
        Me.chkAllOut.Size = New System.Drawing.Size(163, 24)
        Me.chkAllOut.TabIndex = 20
        Me.chkAllOut.Text = "All off and &out"
        Me.chkAllOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkAllOut.UseVisualStyleBackColor = True
        '
        'chkAllOff
        '
        Me.chkAllOff.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkAllOff.Location = New System.Drawing.Point(305, 20)
        Me.chkAllOff.Name = "chkAllOff"
        Me.chkAllOff.Size = New System.Drawing.Size(163, 24)
        Me.chkAllOff.TabIndex = 21
        Me.chkAllOff.Text = "&All off"
        Me.chkAllOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkAllOff.UseVisualStyleBackColor = True
        '
        'chkLampsOff
        '
        Me.chkLampsOff.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkLampsOff.Location = New System.Drawing.Point(6, 50)
        Me.chkLampsOff.Name = "chkLampsOff"
        Me.chkLampsOff.Size = New System.Drawing.Size(111, 24)
        Me.chkLampsOff.TabIndex = 22
        Me.chkLampsOff.Text = "&Lamps off"
        Me.chkLampsOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkLampsOff.UseVisualStyleBackColor = True
        '
        'chkSolenoidsOff
        '
        Me.chkSolenoidsOff.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkSolenoidsOff.Location = New System.Drawing.Point(123, 50)
        Me.chkSolenoidsOff.Name = "chkSolenoidsOff"
        Me.chkSolenoidsOff.Size = New System.Drawing.Size(111, 24)
        Me.chkSolenoidsOff.TabIndex = 23
        Me.chkSolenoidsOff.Text = "&Solenoids off"
        Me.chkSolenoidsOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSolenoidsOff.UseVisualStyleBackColor = True
        '
        'chkGIStringsOff
        '
        Me.chkGIStringsOff.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkGIStringsOff.Location = New System.Drawing.Point(240, 50)
        Me.chkGIStringsOff.Name = "chkGIStringsOff"
        Me.chkGIStringsOff.Size = New System.Drawing.Size(111, 24)
        Me.chkGIStringsOff.TabIndex = 24
        Me.chkGIStringsOff.Text = "&GI strings off"
        Me.chkGIStringsOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkGIStringsOff.UseVisualStyleBackColor = True
        '
        'chkLEDsOff
        '
        Me.chkLEDsOff.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkLEDsOff.Location = New System.Drawing.Point(357, 50)
        Me.chkLEDsOff.Name = "chkLEDsOff"
        Me.chkLEDsOff.Size = New System.Drawing.Size(111, 24)
        Me.chkLEDsOff.TabIndex = 25
        Me.chkLEDsOff.Text = "L&EDs off"
        Me.chkLEDsOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkLEDsOff.UseVisualStyleBackColor = True
        '
        'btnCloseSettings
        '
        Me.btnCloseSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseSettings.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCloseSettings.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseSettings.Location = New System.Drawing.Point(390, 175)
        Me.btnCloseSettings.Name = "btnCloseSettings"
        Me.btnCloseSettings.Size = New System.Drawing.Size(92, 24)
        Me.btnCloseSettings.TabIndex = 35
        Me.btnCloseSettings.Text = "Close"
        Me.btnCloseSettings.UseVisualStyleBackColor = True
        '
        'formSettingsMore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 207)
        Me.Controls.Add(Me.btnCloseSettings)
        Me.Controls.Add(Me.grpLogging)
        Me.Controls.Add(Me.grpPerfTests)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formSettingsMore"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "More backglass settings ..."
        Me.grpLogging.ResumeLayout(False)
        Me.grpLogging.PerformLayout()
        Me.grpPerfTests.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpLogging As System.Windows.Forms.GroupBox
    Friend WithEvents chkStatisticBackglass As System.Windows.Forms.CheckBox
    Friend WithEvents chkLogLEDs As System.Windows.Forms.CheckBox
    Friend WithEvents btnLogPath As System.Windows.Forms.Button
    Friend WithEvents chkLogSolenoids As System.Windows.Forms.CheckBox
    Friend WithEvents chkLogGIStrings As System.Windows.Forms.CheckBox
    Friend WithEvents chkLogLamps As System.Windows.Forms.CheckBox
    Friend WithEvents grpPerfTests As System.Windows.Forms.GroupBox
    Friend WithEvents chkAllOut As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllOff As System.Windows.Forms.CheckBox
    Friend WithEvents chkLampsOff As System.Windows.Forms.CheckBox
    Friend WithEvents chkSolenoidsOff As System.Windows.Forms.CheckBox
    Friend WithEvents chkGIStringsOff As System.Windows.Forms.CheckBox
    Friend WithEvents chkLEDsOff As System.Windows.Forms.CheckBox
    Friend WithEvents btnCloseSettings As System.Windows.Forms.Button
End Class
