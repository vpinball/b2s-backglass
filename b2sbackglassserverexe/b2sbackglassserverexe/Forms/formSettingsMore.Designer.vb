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
        Me.GroupRegistrySettings = New System.Windows.Forms.GroupBox()
        Me.ChkB2SDebugLog = New System.Windows.Forms.CheckBox()
        Me.ChkB2STableSettingsExtendedPath = New System.Windows.Forms.CheckBox()
        Me.lblB2SScreenResFileNameOverride = New System.Windows.Forms.Label()
        Me.TxtB2SScreenResFileNameOverride = New System.Windows.Forms.TextBox()
        Me.ChkB2SWindowPunchActive = New System.Windows.Forms.CheckBox()
        Me.grpLogging.SuspendLayout()
        Me.grpPerfTests.SuspendLayout()
        Me.GroupRegistrySettings.SuspendLayout()
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
        Me.grpLogging.Size = New System.Drawing.Size(524, 84)
        Me.grpLogging.TabIndex = 10
        Me.grpLogging.TabStop = False
        Me.grpLogging.Text = "Logging"
        '
        'chkStatisticBackglass
        '
        Me.chkStatisticBackglass.AutoSize = True
        Me.chkStatisticBackglass.Location = New System.Drawing.Point(217, 25)
        Me.chkStatisticBackglass.Name = "chkStatisticBackglass"
        Me.chkStatisticBackglass.Size = New System.Drawing.Size(203, 17)
        Me.chkStatisticBackglass.TabIndex = 27
        Me.chkStatisticBackglass.Text = "Show statistics backglass after restart"
        Me.chkStatisticBackglass.UseVisualStyleBackColor = True
        '
        'chkLogLEDs
        '
        Me.chkLogLEDs.AutoSize = True
        Me.chkLogLEDs.Location = New System.Drawing.Point(411, 54)
        Me.chkLogLEDs.Name = "chkLogLEDs"
        Me.chkLogLEDs.Size = New System.Drawing.Size(73, 17)
        Me.chkLogLEDs.TabIndex = 31
        Me.chkLogLEDs.Text = "Log LEDs"
        Me.chkLogLEDs.UseVisualStyleBackColor = True
        '
        'btnLogPath
        '
        Me.btnLogPath.Location = New System.Drawing.Point(8, 20)
        Me.btnLogPath.Name = "btnLogPath"
        Me.btnLogPath.Size = New System.Drawing.Size(150, 24)
        Me.btnLogPath.TabIndex = 26
        Me.btnLogPath.Text = "Log path"
        Me.btnLogPath.UseVisualStyleBackColor = True
        '
        'chkLogSolenoids
        '
        Me.chkLogSolenoids.AutoSize = True
        Me.chkLogSolenoids.Location = New System.Drawing.Point(131, 54)
        Me.chkLogSolenoids.Name = "chkLogSolenoids"
        Me.chkLogSolenoids.Size = New System.Drawing.Size(91, 17)
        Me.chkLogSolenoids.TabIndex = 29
        Me.chkLogSolenoids.Text = "Log solenoids"
        Me.chkLogSolenoids.UseVisualStyleBackColor = True
        '
        'chkLogGIStrings
        '
        Me.chkLogGIStrings.AutoSize = True
        Me.chkLogGIStrings.Location = New System.Drawing.Point(271, 54)
        Me.chkLogGIStrings.Name = "chkLogGIStrings"
        Me.chkLogGIStrings.Size = New System.Drawing.Size(91, 17)
        Me.chkLogGIStrings.TabIndex = 30
        Me.chkLogGIStrings.Text = "Log GI strings"
        Me.chkLogGIStrings.UseVisualStyleBackColor = True
        '
        'chkLogLamps
        '
        Me.chkLogLamps.AutoSize = True
        Me.chkLogLamps.Location = New System.Drawing.Point(8, 54)
        Me.chkLogLamps.Name = "chkLogLamps"
        Me.chkLogLamps.Size = New System.Drawing.Size(74, 17)
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
        Me.grpPerfTests.Size = New System.Drawing.Size(524, 85)
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
        Me.chkAllOff.Location = New System.Drawing.Point(353, 19)
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
        Me.chkSolenoidsOff.Location = New System.Drawing.Point(139, 50)
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
        Me.chkGIStringsOff.Location = New System.Drawing.Point(272, 50)
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
        Me.chkLEDsOff.Location = New System.Drawing.Point(405, 50)
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
        Me.btnCloseSettings.Location = New System.Drawing.Point(440, 299)
        Me.btnCloseSettings.Name = "btnCloseSettings"
        Me.btnCloseSettings.Size = New System.Drawing.Size(92, 24)
        Me.btnCloseSettings.TabIndex = 35
        Me.btnCloseSettings.Text = "Close"
        Me.btnCloseSettings.UseVisualStyleBackColor = True
        '
        'GroupRegistrySettings
        '
        Me.GroupRegistrySettings.Controls.Add(Me.ChkB2SDebugLog)
        Me.GroupRegistrySettings.Controls.Add(Me.ChkB2STableSettingsExtendedPath)
        Me.GroupRegistrySettings.Controls.Add(Me.lblB2SScreenResFileNameOverride)
        Me.GroupRegistrySettings.Controls.Add(Me.TxtB2SScreenResFileNameOverride)
        Me.GroupRegistrySettings.Controls.Add(Me.ChkB2SWindowPunchActive)
        Me.GroupRegistrySettings.Location = New System.Drawing.Point(8, 185)
        Me.GroupRegistrySettings.Name = "GroupRegistrySettings"
        Me.GroupRegistrySettings.Size = New System.Drawing.Size(524, 110)
        Me.GroupRegistrySettings.TabIndex = 32
        Me.GroupRegistrySettings.TabStop = False
        Me.GroupRegistrySettings.Text = "Registry settings (HKCU\Software\B2S)"
        '
        'ChkB2SDebugLog
        '
        Me.ChkB2SDebugLog.AutoSize = True
        Me.ChkB2SDebugLog.Location = New System.Drawing.Point(392, 31)
        Me.ChkB2SDebugLog.Name = "ChkB2SDebugLog"
        Me.ChkB2SDebugLog.Size = New System.Drawing.Size(96, 17)
        Me.ChkB2SDebugLog.TabIndex = 36
        Me.ChkB2SDebugLog.Text = "B2SDebugLog"
        Me.ChkB2SDebugLog.UseVisualStyleBackColor = True
        '
        'ChkB2STableSettingsExtendedPath
        '
        Me.ChkB2STableSettingsExtendedPath.AutoSize = True
        Me.ChkB2STableSettingsExtendedPath.Location = New System.Drawing.Point(186, 31)
        Me.ChkB2STableSettingsExtendedPath.Name = "ChkB2STableSettingsExtendedPath"
        Me.ChkB2STableSettingsExtendedPath.Size = New System.Drawing.Size(178, 17)
        Me.ChkB2STableSettingsExtendedPath.TabIndex = 35
        Me.ChkB2STableSettingsExtendedPath.Text = "B2STableSettingsExtendedPath"
        Me.ChkB2STableSettingsExtendedPath.UseVisualStyleBackColor = True
        '
        'lblB2SScreenResFileNameOverride
        '
        Me.lblB2SScreenResFileNameOverride.AutoSize = True
        Me.lblB2SScreenResFileNameOverride.Location = New System.Drawing.Point(8, 63)
        Me.lblB2SScreenResFileNameOverride.Name = "lblB2SScreenResFileNameOverride"
        Me.lblB2SScreenResFileNameOverride.Size = New System.Drawing.Size(164, 13)
        Me.lblB2SScreenResFileNameOverride.TabIndex = 34
        Me.lblB2SScreenResFileNameOverride.Text = "B2SScreenResFileNameOverride"
        '
        'TxtB2SScreenResFileNameOverride
        '
        Me.TxtB2SScreenResFileNameOverride.Location = New System.Drawing.Point(187, 60)
        Me.TxtB2SScreenResFileNameOverride.Name = "TxtB2SScreenResFileNameOverride"
        Me.TxtB2SScreenResFileNameOverride.Size = New System.Drawing.Size(93, 20)
        Me.TxtB2SScreenResFileNameOverride.TabIndex = 33
        Me.TxtB2SScreenResFileNameOverride.TabStop = False
        Me.TxtB2SScreenResFileNameOverride.WordWrap = False
        '
        'ChkB2SWindowPunchActive
        '
        Me.ChkB2SWindowPunchActive.AutoSize = True
        Me.ChkB2SWindowPunchActive.Location = New System.Drawing.Point(12, 31)
        Me.ChkB2SWindowPunchActive.Name = "ChkB2SWindowPunchActive"
        Me.ChkB2SWindowPunchActive.Size = New System.Drawing.Size(146, 17)
        Me.ChkB2SWindowPunchActive.TabIndex = 32
        Me.ChkB2SWindowPunchActive.Text = "B2SWindowPunchActive"
        Me.ChkB2SWindowPunchActive.UseVisualStyleBackColor = True
        '
        'formSettingsMore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(539, 331)
        Me.Controls.Add(Me.GroupRegistrySettings)
        Me.Controls.Add(Me.btnCloseSettings)
        Me.Controls.Add(Me.grpLogging)
        Me.Controls.Add(Me.grpPerfTests)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formSettingsMore"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "More backglass settings ..."
        Me.TopMost = True
        Me.grpLogging.ResumeLayout(False)
        Me.grpLogging.PerformLayout()
        Me.grpPerfTests.ResumeLayout(False)
        Me.GroupRegistrySettings.ResumeLayout(False)
        Me.GroupRegistrySettings.PerformLayout()
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
    Friend WithEvents GroupRegistrySettings As Windows.Forms.GroupBox
    Friend WithEvents ChkB2SWindowPunchActive As Windows.Forms.CheckBox
    Friend WithEvents TxtB2SScreenResFileNameOverride As Windows.Forms.TextBox
    Friend WithEvents lblB2SScreenResFileNameOverride As Windows.Forms.Label
    Friend WithEvents ChkB2STableSettingsExtendedPath As Windows.Forms.CheckBox
    Friend WithEvents ChkB2SDebugLog As Windows.Forms.CheckBox
End Class
