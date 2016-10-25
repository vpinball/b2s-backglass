Imports System

Public Class formResize

    Private newsizeofimage As Size = Nothing

    Private factor As Double = 1

    Private noChangeEvent As Boolean = True

    Public Shadows Function ShowDialog(ByVal owner As IWin32Window,
                                       ByRef newsize As Size) As DialogResult
        ' show dialog
        noChangeEvent = True
        numPercentage.Value = 100
        newsizeofimage = newsize
        numWidth.Maximum = newsize.Width * 2
        numWidth.Value = newsize.Width
        numWidth.Minimum = 1
        numHeight.Maximum = newsize.Height * 2
        numHeight.Value = newsize.Height
        numHeight.Minimum = 1
        rbByAbsoluteSize.Checked = True
        cbMaintainAspectRatio.Checked = True
        factor = newsize.Width / newsize.Height
        ' now show the dialog
        noChangeEvent = False
        Dim nRet As DialogResult = MyBase.ShowDialog(owner)
        If nRet = Windows.Forms.DialogResult.OK Then
            ' return some values
            newsize = newsizeofimage
        End If
        Return nRet
    End Function

    Private Sub Percentage_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numPercentage.ValueChanged
        numWidth.Value = CInt(newsizeofimage.Width * numPercentage.Value / 100)
        numHeight.Value = CInt(newsizeofimage.Height * numPercentage.Value / 100)
    End Sub

    Private Sub ByPercentage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbByPercentage.CheckedChanged
        numPercentage.Enabled = True
        cbMaintainAspectRatio.Enabled = False
        numWidth.Enabled = False
        numHeight.Enabled = False
        numPercentage.Focus()
    End Sub
    Private Sub ByAbsoluteSize_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbByAbsoluteSize.CheckedChanged
        numPercentage.Enabled = False
        cbMaintainAspectRatio.Enabled = True
        numWidth.Enabled = True
        numHeight.Enabled = True
        cbMaintainAspectRatio.Focus()
    End Sub

    Private Sub Width_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numWidth.ValueChanged
        If noChangeEvent OrElse Not cbMaintainAspectRatio.Checked Then Return
        noChangeEvent = True
        Dim height As Integer = numWidth.Value / factor
        If height <> numHeight.Value Then numHeight.Value = height
        noChangeEvent = False
    End Sub
    Private Sub Height_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numHeight.ValueChanged
        If noChangeEvent OrElse Not cbMaintainAspectRatio.Checked Then Return
        noChangeEvent = True
        Dim width As Integer = numHeight.Value * factor
        If width <> numWidth.Value Then numWidth.Value = width
        noChangeEvent = False
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If rbByPercentage.Checked Then
            newsizeofimage = New Size(CInt(newsizeofimage.Width * numPercentage.Value / 100), CInt(newsizeofimage.Height * numPercentage.Value / 100))
        Else
            newsizeofimage = New Size(numWidth.Value, numHeight.Value)
        End If
        MyBase.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class