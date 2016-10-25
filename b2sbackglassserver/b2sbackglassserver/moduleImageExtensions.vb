Imports System
Imports System.Drawing

Module moduleImageExtensions

    <System.Runtime.CompilerServices.Extension()> _
    Public Function Resized(image As Image, size As Size, Optional ByVal disposeOriginal As Boolean = False) As Image
        If image Is Nothing Then Return Nothing
        If size.Width <= 0 OrElse size.Height <= 0 Then Return Nothing
        Dim ret As Bitmap = New Bitmap(size.Width, size.Height)
        Using gr As Graphics = Graphics.FromImage(ret)
            gr.PageUnit = GraphicsUnit.Pixel
            'gr.InterpolationMode = Drawing2D.InterpolationMode.High
            gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            gr.DrawImage(image, New Rectangle(0, 0, ret.Width, ret.Height))
        End Using
        If disposeOriginal Then
            image.Dispose()
            image = Nothing
        End If
        Return ret
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function Rotated(image As Image, angle As Integer) As Image
        If image Is Nothing Then Return Nothing
        Dim ret As Bitmap = New Bitmap(image.Width, image.Height)
        Dim matrix As New System.Drawing.Drawing2D.Matrix
        Using gr As Graphics = Graphics.FromImage(ret)
            gr.PageUnit = GraphicsUnit.Pixel
            'gr.InterpolationMode = Drawing2D.InterpolationMode.High
            gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            matrix.RotateAt(angle * -1, New Point(CInt(image.Width / 2), CInt(image.Height / 2)))
            gr.Transform = matrix
            gr.DrawImage(image, New Rectangle(0, 0, image.Width, image.Height))
        End Using
        Return ret
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ResizedF(image As Image, sizeF As SizeF, Optional ByVal disposeOriginal As Boolean = False) As Image
        If image Is Nothing Then Return Nothing
        If sizeF.Width <= 0 OrElse sizeF.Height <= 0 Then Return Nothing
        Dim largesize As Size = New Size(CInt(sizeF.Width) + 1, CInt(sizeF.Height) + 1)
        Dim ret As Bitmap = New Bitmap(largesize.Width, largesize.Height)
        Using gr As Graphics = Graphics.FromImage(ret)
            gr.PageUnit = GraphicsUnit.Pixel
            'gr.InterpolationMode = Drawing2D.InterpolationMode.High
            gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            gr.DrawImage(image, New Rectangle(0, 0, sizeF.Width, sizeF.Height))
        End Using
        If disposeOriginal Then
            image.Dispose()
            image = Nothing
        End If
        Return ret
    End Function

    '<System.Runtime.CompilerServices.Extension()> _
    'Public Function PartFromImage(image As Image, area As Rectangle) As Image
    '    If image Is Nothing Then Return Nothing
    '    Dim imageBackground As Bitmap = CType(image, Bitmap)
    '    Dim imagePart As Image = New Bitmap(area.Width, area.Height)
    '    imagePart = imageBackground.Clone(area, Imaging.PixelFormat.Format32bppArgb)
    '    Return imagePart
    'End Function

End Module
