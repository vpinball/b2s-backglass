Imports System
Imports System.Windows.Forms

Public Class formSettingsMore

    Private formSettings As formSettings = Nothing
    Private formBackglass As formBackglass = Nothing

    Public Sub New(ByVal _formSettings As formSettings, ByVal _formbackglass As formBackglass)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.formSettings = _formSettings
        Me.formBackglass = _formbackglass

    End Sub

    Public Shadows Function ShowDialog(ByVal owner As IWin32Window,
                                       ByVal _formbackglass As formBackglass) As DialogResult

        Me.formSettings = owner
        Me.formBackglass = _formbackglass

        Dim ret As DialogResult = MyBase.ShowDialog(owner)
        Return Windows.Forms.DialogResult.OK

    End Function

    Private Sub formSettings_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            btnCloseSettings.PerformClick()
        End If
    End Sub

    Private Sub btnCloseSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseSettings.Click
        Me.Close()
    End Sub

    Private Sub chkAllOut_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAllOut.CheckedChanged
        B2SSettings.AllOut = chkAllOut.Checked
        chkAllOff.Checked = chkAllOut.Checked
        formBackglass.BackgroundImage = formBackglass.DarkImage
        Me.Invalidate()
    End Sub
    Private Sub chkAllOff_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAllOff.CheckedChanged
        B2SSettings.AllOff = chkAllOff.Checked
        chkLampsOff.Checked = chkAllOff.Checked
        chkSolenoidsOff.Checked = chkAllOff.Checked
        chkGIStringsOff.Checked = chkAllOff.Checked
        chkLEDsOff.Checked = chkAllOff.Checked
    End Sub
    Private Sub chkLampsOff_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLampsOff.CheckedChanged
        B2SSettings.LampsOff = chkLampsOff.Checked
    End Sub
    Private Sub chkSolenoidsOff_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSolenoidsOff.CheckedChanged
        B2SSettings.SolenoidsOff = chkSolenoidsOff.Checked
    End Sub
    Private Sub chkGIStringsOff_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGIStringsOff.CheckedChanged
        B2SSettings.GIStringsOff = chkGIStringsOff.Checked
    End Sub
    Private Sub chkLEDsOff_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLEDsOff.CheckedChanged
        B2SSettings.LEDsOff = chkLEDsOff.Checked
        For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
            leddisplay.Value.Visible = Not B2SSettings.LEDsOff AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Dream7
        Next
        For Each leds As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
            leds.Value.Visible = Not B2SSettings.LEDsOff AndAlso B2SSettings.UsedLEDType = B2SSettings.eLEDTypes.Rendered
        Next
    End Sub

    Private Sub btnLogPath_Click(sender As System.Object, e As System.EventArgs) Handles btnLogPath.Click
        Using fbd As FolderBrowserDialog = New FolderBrowserDialog()
            fbd.SelectedPath = B2SSettings.LogPath
            If fbd.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                formSettings.isSettingsScreenDirty = True
                B2SSettings.LogPath = fbd.SelectedPath
                btnLogPath.Text = "Log path: " & B2SSettings.LogPath
            End If
        End Using
    End Sub
    Private Sub chkLogLamps_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLogLamps.CheckedChanged
        formSettings.isSettingsScreenDirty = True
        B2SSettings.IsLampsStateLogOn = chkLogLamps.Checked
    End Sub
    Private Sub chlLogSolenoids_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLogSolenoids.CheckedChanged
        formSettings.isSettingsScreenDirty = True
        B2SSettings.IsSolenoidsStateLogOn = chkLogSolenoids.Checked
    End Sub
    Private Sub chkLogGIStrings_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLogGIStrings.CheckedChanged
        formSettings.isSettingsScreenDirty = True
        B2SSettings.IsGIStringsStateLogOn = chkLogGIStrings.Checked
    End Sub
    Private Sub chkLogLEDs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLogLEDs.CheckedChanged
        formSettings.isSettingsScreenDirty = True
        B2SSettings.IsLEDsStateLogOn = chkLogLEDs.Checked
    End Sub
    Private Sub chkStatisticBackglass_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkStatisticBackglass.CheckedChanged
        formSettings.activateMsgBoxAtSaving = True
        formSettings.isSettingsScreenDirty = True
        B2SSettings.IsStatisticsBackglassOn = chkStatisticBackglass.Checked
    End Sub

End Class