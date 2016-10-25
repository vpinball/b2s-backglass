Imports System
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D

Namespace Illumination

    Public Class Create

        Private Const C_OVERLAYS As Integer = 2
        Private Const C_BULBRADIUS As Single = 0.15

        Private doDodge As Boolean = True

        Private Class BitmapToByteConverter

            Private bitmap As Bitmap
            Private data As BitmapData

            Public bytes As Byte()

            Public Sub New(ByVal bitmap As Bitmap)
                Me.bitmap = bitmap
            End Sub

            Public Sub BitmapToBytes()
                Dim bounds As Rectangle = New Rectangle(0, 0, bitmap.Width, bitmap.Height)
                data = bitmap.LockBits(bounds, Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format32bppArgb)
                Dim size As Integer = data.Stride * data.Height
                ReDim bytes(size)
                Marshal.Copy(data.Scan0, bytes, 0, size)
            End Sub

            Public Sub BytesToBitmap()
                Dim size As Integer = data.Stride * data.Height
                Marshal.Copy(bytes, 0, data.Scan0, size)
                bitmap.UnlockBits(data)
                bitmap = Nothing
                bytes = Nothing
                data = Nothing
            End Sub

        End Class

        Public Sub MergeLayers(ByVal layer1 As Bitmap,
                               ByVal layer2 As Bitmap,
                               Optional ByVal overlays As Integer = 0,
                               Optional ByVal lightcolor As Color = Nothing,
                               Optional ByVal dodgecolor As Color = Nothing)
            If lightcolor = Nothing Then lightcolor = DefaultLightColor
            Dim layer1bytes As BitmapToByteConverter = New BitmapToByteConverter(layer1)
            Dim layer2bytes As BitmapToByteConverter = New BitmapToByteConverter(layer2)
            layer1bytes.BitmapToBytes()
            layer2bytes.BitmapToBytes()
            For nPixel As Integer = 0 To (layer2.Height - 1) * (layer2.Width - 1) * 4 Step 4
                ' bgr (reverse rgb)
                Dim rgb(2) As Byte
                rgb(0) = layer1bytes.bytes(nPixel)
                rgb(1) = layer1bytes.bytes(nPixel + 1)
                rgb(2) = layer1bytes.bytes(nPixel + 2)
                For i As Integer = 1 To If(overlays > 0, overlays, C_OVERLAYS)
                    rgb(0) = ColorOverlay(rgb(0), lightcolor.B)
                    rgb(1) = ColorOverlay(rgb(1), lightcolor.G)
                    rgb(2) = ColorOverlay(rgb(2), lightcolor.R)
                Next
                If doDodge AndAlso dodgecolor <> Nothing Then
                    rgb(0) = ColorDodge(rgb(0), dodgecolor.B)
                    rgb(1) = ColorDodge(rgb(1), dodgecolor.G)
                    rgb(2) = ColorDodge(rgb(2), dodgecolor.R)
                    'rgb(0) = ColorDodge(rgb(0), 75) ' lightcolor.B)
                    'rgb(1) = ColorDodge(rgb(1), lightcolor.G)
                    'rgb(2) = ColorDodge(rgb(2), 100) ' lightcolor.R)
                End If
                layer2bytes.bytes(nPixel) = rgb(0)
                layer2bytes.bytes(nPixel + 1) = rgb(1)
                layer2bytes.bytes(nPixel + 2) = rgb(2)
            Next
            layer1bytes.BytesToBitmap()
            layer2bytes.BytesToBitmap()
        End Sub

        Private Function ColorDodge(ByVal a As Byte, ByVal b As Byte) As Byte
            If b = 255 Then Return b
            Dim c As Integer = (CInt(a) << 8) / (255 - CInt(b))
            Return CByte(If((c > 255), 255, c))
        End Function
        Private Function ColorOverlay(ByVal a As Byte, ByVal b As Byte) As Byte
            Dim c As Integer = If(CInt(a) < 128, (2 * CInt(b) * CInt(a) / 255), (255 - 2 * (255 - CInt(b)) * (255 - CInt(a)) / 255))
            Return CByte(If((c > 255), 255, c))
        End Function

        Public Sub DrawLight(ByVal gr As Graphics,
                             ByVal rectlight As Rectangle,
                             Optional ByVal text As String = "",
                             Optional ByVal font As Font = Nothing,
                             Optional ByVal textalignment As Illumination.eTextAlignment = eTextAlignment.Center,
                             Optional ByVal illumode As Illumination.eIlluMode = eIlluMode.Standard)

            Dim pathlight As GraphicsPath = New GraphicsPath()
            Dim pathlight2 As GraphicsPath = Nothing
            Select Case illumode
                Case eIlluMode.Flasher
                    ' star bulb: flasher
                    With rectlight
                        Dim f As Integer = 5
                        pathlight.AddPolygon(New Point() {New Point(.X, .Y), New Point(.X + .Width / f, .Y + .Height / 2), New Point(.X, .Y + .Height)})
                        pathlight.AddPolygon(New Point() {New Point(.X, .Y + .Height), New Point(.X + .Width / 2, .Y + .Height / f * (f - 1)), New Point(.X + .Width, .Y + .Height)})
                        pathlight.AddPolygon(New Point() {New Point(.X + .Width, .Y + .Height), New Point(.X + .Width / f * (f - 1), .Y + .Height / 2), New Point(.X + .Width, .Y)})
                        pathlight.AddPolygon(New Point() {New Point(.X + .Width, .Y), New Point(.X + .Width / 2, .Y + .Height / f), New Point(.X, .Y)})
                        pathlight.CloseFigure()
                        pathlight2 = New GraphicsPath()
                        pathlight2.AddEllipse(rectlight)
                    End With
                Case Else
                    ' standard bulb: ellipse
                    pathlight.AddEllipse(rectlight)
            End Select

            ' color bulb
            Dim grad As PathGradientBrush = New PathGradientBrush(pathlight)
            grad.WrapMode = WrapMode.Clamp
            grad.SurroundColors = New Color() {Color.FromArgb(0, 0, 0, 0)}
            ' color not important, because due to a ms round bug we can use only alpha of the color
            grad.CenterColor = Color.FromArgb(If(String.IsNullOrEmpty(text), 255, 30), 255, 255, 255)
            grad.CenterPoint = New PointF(rectlight.X + rectlight.Width / 2 - 5, rectlight.Y + rectlight.Height / 2)
            grad.FocusScales = New PointF(C_BULBRADIUS, C_BULBRADIUS)
            gr.FillRectangle(grad, rectlight)
            grad.Dispose()
            pathlight.Dispose()
            If pathlight2 IsNot Nothing Then
                grad = New PathGradientBrush(pathlight2)
                grad.WrapMode = WrapMode.Clamp
                grad.SurroundColors = New Color() {Color.FromArgb(0, 0, 0, 0)}
                ' color not important, because due to a ms round bug we can use only alpha of the color
                grad.CenterColor = Color.FromArgb(If(String.IsNullOrEmpty(text), 255, 30), 255, 255, 255)
                grad.CenterPoint = New PointF(rectlight.X + rectlight.Width / 2 - 5, rectlight.Y + rectlight.Height / 2)
                grad.FocusScales = New PointF(C_BULBRADIUS, C_BULBRADIUS)
                gr.FillRectangle(grad, rectlight)
                grad.Dispose()
                pathlight2.Dispose()
            End If

            ' maybe render text
            If Not String.IsNullOrEmpty(text) AndAlso font IsNot Nothing Then
                Dim size As Size = TextRenderer.MeasureText(gr, text, font, New Size(0, 0), TextFormatFlags.NoPrefix)
                Dim loc As Point = New Point(rectlight.X + CInt((rectlight.Width - size.Width) / 2) + 2, rectlight.Y + CInt((rectlight.Height - size.Height) / 2))
                Dim left As Point = New Point(0, 0)
                Dim right As Point = New Point(rectlight.Width - 1, rectlight.Height - 1)
                Dim blend As ColorBlend = New ColorBlend
                blend.Colors = New Color() {Color.Transparent, Color.White, Color.Transparent}
                blend.Positions = New Single() {0.0F, 0.5F, 1.0F}
                If text.Contains(vbCrLf) Then
                    Using brush As LinearGradientBrush = New LinearGradientBrush(left, right, Color.White, Color.White)
                        brush.InterpolationColors = blend
                        'Dim brush As Brush = New SolidBrush(Color.FromArgb(255, 255, 255, 255))
                        Dim lines As String() = text.Split(vbCrLf)
                        Dim y As Integer = rectlight.Y + CInt((rectlight.Height - size.Height) / 2)
                        For Each line As String In lines
                            line = line.Trim.Replace(Chr(13), "").Replace(Chr(10), "")
                            Dim linesize As Size = TextRenderer.MeasureText(gr, line, font)
                            Dim x As Integer = rectlight.X + CInt((rectlight.Width - linesize.Width) / 2) + 2
                            If textalignment = eTextAlignment.Left Then
                                x = 2
                            ElseIf textalignment = eTextAlignment.Right Then
                                x = rectlight.X + rectlight.Width - linesize.Width + 2
                            End If
                            gr.DrawString(line, font, brush, New Point(x, y))
                            'gr.DrawString(line, font, brush, New Rectangle(New Point(x, y), New Size(linesize.Width, linesize.Height)), Drawing.StringFormat.GenericTypographic)
                            y += CInt(size.Height / lines.Count)
                        Next
                        'brush.Dispose()
                    End Using
                Else
                    Using brush As LinearGradientBrush = New LinearGradientBrush(left, right, Color.White, Color.White)
                        brush.InterpolationColors = blend
                        Dim x As Integer = loc.X
                        If textalignment = eTextAlignment.Left Then
                            x = 2
                        ElseIf textalignment = eTextAlignment.Right Then
                            x = rectlight.X + rectlight.Width - size.Width + 2
                        End If
                        gr.DrawString(text, font, brush, New Point(x, loc.Y))
                        'gr.DrawString(text, font, brush, New Rectangle(New Point(x, loc.Y), size), Drawing.StringFormat.GenericTypographic)
                    End Using
                End If
            End If

        End Sub

        Public Function CreateOverlayImage(ByVal imageBackground As Bitmap,
                                           ByVal rect As Rectangle,
                                           ByVal rectX As Rectangle,
                                           ByVal intensity As Integer,
                                           ByVal lightcolor As Color,
                                           ByVal dodgecolor As Color,
                                           ByVal text As String,
                                           ByVal font As Font,
                                           ByVal textalignment As Illumination.eTextAlignment,
                                           ByVal illumode As Illumination.eIlluMode) As Image

            ' maybe create the scaled and illuminated image part
            If imageBackground IsNot Nothing Then
                Dim currentimage As Image = New Bitmap(CInt(rectX.Width), CInt(rectX.Height))
                currentimage = imageBackground.Clone(rectX, Imaging.PixelFormat.Format32bppArgb)
                Dim image As Bitmap = New Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Using gr As Graphics = Graphics.FromImage(image)
                    gr.Clear(Color.Transparent)
                    gr.SmoothingMode = SmoothingMode.HighQuality
                    gr.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                    If Not String.IsNullOrEmpty(text) AndAlso font IsNot Nothing Then
                        DrawLight(gr, New Rectangle(0, 0, image.Width, image.Height), text, font, textalignment, illumode)
                    Else
                        DrawLight(gr, New Rectangle(0, 0, image.Width, image.Height), , , , illumode)
                    End If
                    If Not rect.Equals(rectX) Then
                        Dim newrect As Rectangle = rect
                        If newrect.X + newrect.Width > imageBackground.Width Then
                            newrect.Width = imageBackground.Width - newrect.X
                        End If
                        If newrect.Y + newrect.Height > imageBackground.Height Then
                            newrect.Height = imageBackground.Height - newrect.Y
                        End If
                        If newrect.X < 0 Then
                            newrect.Width += newrect.X
                            newrect.X = Math.Abs(newrect.X)
                        Else
                            newrect.X = 0
                        End If
                        If newrect.Y < 0 Then
                            newrect.Height += newrect.Y
                            newrect.Y = Math.Abs(newrect.Y)
                        Else
                            newrect.Y = 0
                        End If
                        image = image.PartFromImage(newrect)
                    End If
                End Using
                MergeLayers(currentimage, image, intensity, lightcolor, dodgecolor)
                Return image
            Else
                Return Nothing
            End If

        End Function

        Public Function CreateImage(ByVal image As Bitmap,
                                    ByVal rect As Rectangle,
                                    ByVal rectX As Rectangle,
                                    ByVal intensity As Integer,
                                    ByVal lightcolor As Color,
                                    ByVal dodgecolor As Color,
                                    ByVal text As String,
                                    ByVal font As Font,
                                    ByVal textalignment As Illumination.eTextAlignment,
                                    ByVal illumode As Illumination.eIlluMode) As Image

            If image Is Nothing Then Return Nothing

            Dim ret As Image = image.Clone()
            Dim overlay = CreateOverlayImage(image, rect, rectX, intensity, lightcolor, dodgecolor, text, font, textalignment, illumode)
            Using gr As Graphics = Graphics.FromImage(ret)
                gr.DrawImage(overlay, rectX.X, rectX.Y)
            End Using
            overlay.Dispose()
            overlay = Nothing
            Return ret

        End Function

    End Class

End Namespace