Imports System

Public Class formBrightness

    Private sourceimage As Image = Nothing

    Public Shadows Function ShowDialog(ByVal owner As IWin32Window,
                                       ByRef image As Image) As DialogResult
        ' show dialog
        sourceimage = image
        PictureBoxPreview.Image = sourceimage.Resized(PictureBoxPreview.Size)
        NumericUpDownBrightness.Value = 0
        chkIgnoreGrill.Enabled = (Backglass.currentData.GrillHeight > 0 AndAlso Not Backglass.currentData.IsDMDImageShown)
        If chkIgnoreGrill.Enabled Then chkIgnoreGrill.Checked = True Else chkIgnoreGrill.Checked = False
        ' now show the dialog
        Dim nRet As DialogResult = MyBase.ShowDialog(owner)
        If nRet = Windows.Forms.DialogResult.OK Then
            ' return new image
            Dim newimage As Bitmap = New Bitmap(sourceimage.Width, sourceimage.Height)
            If chkIgnoreGrill.Checked Then
                Using backglassimage As Bitmap = sourceimage.PartFromImage(New Rectangle(0, 0, sourceimage.Width, sourceimage.Height - Backglass.currentData.GrillHeight))
                    backglassimage.Filters.Brightness(CSng(NumericUpDownBrightness.Value / 100))
                    Using grillimage As Bitmap = sourceimage.PartFromImage(New Rectangle(0, sourceimage.Height - Backglass.currentData.GrillHeight, sourceimage.Width, Backglass.currentData.GrillHeight))
                        Using gr As Graphics = Graphics.FromImage(newimage)
                            gr.PageUnit = GraphicsUnit.Pixel
                            gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                            gr.DrawImage(backglassimage, New Rectangle(0, 0, backglassimage.Width, backglassimage.Height))
                            gr.DrawImage(grillimage, New Rectangle(0, sourceimage.Height - Backglass.currentData.GrillHeight, sourceimage.Width, Backglass.currentData.GrillHeight))
                        End Using
                    End Using
                End Using
            Else
                newimage = sourceimage.Copy() '.Resized(sourceimage.Size)
                newimage.Filters.Brightness(CSng(NumericUpDownBrightness.Value / 100))
            End If
            image = newimage
        End If
        Return nRet
    End Function

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        MyBase.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub formBrightness_ResizeBegin(sender As Object, e As System.EventArgs) Handles Me.ResizeBegin
        PictureBoxPreview.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub
    Private Sub formBrightness_ResizeEnd(sender As Object, e As System.EventArgs) Handles Me.ResizeEnd
        ChangeBrightness()
        PictureBoxPreview.SizeMode = PictureBoxSizeMode.Normal
    End Sub

    Private Sub TrackBarBrightness_Scroll(sender As System.Object, e As System.EventArgs) Handles TrackBarBrightness.Scroll
        NumericUpDownBrightness.Value = TrackBarBrightness.Value
    End Sub
    Private Sub NumericUpDownBrightness_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDownBrightness.ValueChanged
        TrackBarBrightness.Value = NumericUpDownBrightness.Value
        ChangeBrightness()
    End Sub

    Private Sub IgnoreGrill_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkIgnoreGrill.CheckedChanged
        ChangeBrightness()
    End Sub

    Private Sub ChangeBrightness()

        PictureBoxPreview.Image.Dispose()

        Dim factor As Single = sourceimage.Height / PictureBoxPreview.Height
        Dim size As Size = PictureBoxPreview.Size

        Dim newimage As Bitmap = New Bitmap(size.Width, size.Height)

        If chkIgnoreGrill.Enabled AndAlso chkIgnoreGrill.Checked Then
            Using backglassimage As Bitmap = sourceimage.Resized(PictureBoxPreview.Size).PartFromImage(New Rectangle(0, 0, size.Width, size.Height - Backglass.currentData.GrillHeight / factor))
                backglassimage.Filters.Brightness(CSng(NumericUpDownBrightness.Value / 100))
                Using grillimage As Bitmap = sourceimage.Resized(PictureBoxPreview.Size).PartFromImage(New Rectangle(0, size.Height - Backglass.currentData.GrillHeight / factor, size.Width, Backglass.currentData.GrillHeight / factor))
                    Using gr As Graphics = Graphics.FromImage(newimage)
                        gr.PageUnit = GraphicsUnit.Pixel
                        gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                        gr.DrawImage(backglassimage, New Rectangle(0, 0, backglassimage.Width, backglassimage.Height))
                        gr.DrawImage(grillimage, New Rectangle(0, size.Height - Backglass.currentData.GrillHeight / factor, size.Width, Backglass.currentData.GrillHeight / factor))
                    End Using
                End Using
            End Using
        Else
            newimage = sourceimage.Resized(size)
            newimage.Filters.Brightness(CSng(NumericUpDownBrightness.Value / 100))
        End If

        PictureBoxPreview.Image = newimage

    End Sub

End Class