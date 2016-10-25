Imports System
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Namespace Illumination

    Public Class Lights

        Public Event ReportProgress(ByVal sender As Object, ByVal e As LightsProgressEventArgs)
        Public Class LightsProgressEventArgs
            Inherits System.EventArgs

            Public Progress As Integer = 0

            Public Sub New(ByVal _progress As Integer)
                Progress = _progress
            End Sub
        End Class
        Public Class LightColorChangedEventArgs
            Inherits System.EventArgs

            Public Color As Color = Nothing

            Public Sub New(ByVal _color As Color)
                Color = _color
            End Sub
        End Class

        Private lightcreation As Illumination.Create = New Illumination.Create()

        Private parent As B2SPictureBox = Nothing
        Private isDMD As Boolean = False

        Private ReadOnly Property bulbs() As Illumination.BulbCollection
            Get
                Return If(Backglass.currentData IsNot Nothing, If(isDMD, Backglass.currentData.DMDBulbs, Backglass.currentData.Bulbs), Nothing)
            End Get
        End Property

        Public Event ImageIsRendered(ByVal sender As Object, ByVal e As ImageEventArgs)
        Public Class ImageEventArgs
            Inherits EventArgs

            Public Image As Image = Nothing

            Public Sub New(ByVal _image As Image)
                Image = _image
            End Sub
        End Class

        Public Property factor() As Double = 1

        Private newimage As Image = Nothing
        Public ReadOnly Property Image() As Image
            Get
                Return newimage
            End Get
        End Property

        Public Sub PrepareImage(currentimage As Image)
            Static bulbcount As Integer = 0
            If bulbs.Count > 0 AndAlso bulbcount <> bulbs.Count Then
                bulbcount = bulbs.Count
                newimage = New Bitmap(currentimage.Width, currentimage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                lightcreation.MergeLayers(currentimage, newimage)
                ClearImage()
            End If
        End Sub
        Public Sub DrawImage(currentimage As Image, _
                             Optional ByVal rominfofilter As String = "")
            If bulbs.Count > 0 Then
                newimage = New Bitmap(currentimage.Width, currentimage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Using gr As Graphics = Graphics.FromImage(newimage)
                    gr.Clear(Color.Transparent)
                    gr.SmoothingMode = SmoothingMode.HighQuality
                    gr.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                    For Each bulb As Illumination.BulbInfo In bulbs
                        With bulb
                            Dim getin As Boolean = (String.IsNullOrEmpty(rominfofilter) OrElse .RomInfo2String.Equals(rominfofilter))
                            getin = getin AndAlso Not .IsImageSnippit
                            If getin Then
                                If Not String.IsNullOrEmpty(.Text) Then
                                    Dim font As Font = New Font(.FontName, .FontSize, .FontStyle)
                                    lightcreation.DrawLight(gr, New Rectangle(.Location, .Size), .Text, font, .TextAlignment, .IlluMode)
                                    font.Dispose()
                                Else
                                    lightcreation.DrawLight(gr, New Rectangle(.Location, .Size), , , , .IlluMode)
                                End If
                            End If
                        End With
                    Next
                End Using
                lightcreation.MergeLayers(currentimage, newimage)
                Using gr As Graphics = Graphics.FromImage(newimage)
                    For Each bulb As Illumination.BulbInfo In bulbs
                        With bulb
                            Dim getin As Boolean = (String.IsNullOrEmpty(rominfofilter) OrElse .RomInfo2String.Equals(rominfofilter))
                            getin = (getin AndAlso .IsImageSnippit AndAlso .Image IsNot Nothing)
                            If getin Then
                                gr.DrawImage(.Image, New Rectangle(.Location, .Size))
                            End If
                        End With
                    Next
                End Using
            Else
                newimage = Nothing
            End If
        End Sub
        Public Sub ClearImage()
            If newimage IsNot Nothing Then
                newimage.Dispose()
                newimage = Nothing
            End If
        End Sub

        Private newimages As Generic.SortedList(Of Integer, ImageInfo) = New Generic.SortedList(Of Integer, ImageInfo)
        Public Class ImageInfo
            Public BulbID As Integer = -1
            Public Name As String = String.Empty
            Public Image As Image = Nothing
            Public OffImage As Image = Nothing
            Public Rectangle As Rectangle = Nothing
            Public Disposable As Boolean = True

            Public Sub New(ByVal _bulbid As Integer, ByVal _name As String, ByVal _image As Image, ByVal _offimage As Image, ByVal _rectangle As Rectangle, ByVal _disposable As Boolean)
                BulbID = _bulbid
                Name = _name
                Image = _image
                OffImage = _offimage
                Rectangle = _rectangle
                Disposable = _disposable
            End Sub
        End Class
        Public ReadOnly Property Images() As Generic.SortedList(Of Integer, ImageInfo)
            Get
                Return newimages
            End Get
        End Property
        Private Function GetImageByID(ByVal id As Integer) As Image
            Dim ret As Image = Nothing
            For Each imageinfo As KeyValuePair(Of Integer, ImageInfo) In newimages
                If imageinfo.Value.BulbID = id Then
                    ret = imageinfo.Value.Image
                    Exit For
                End If
            Next
            Return ret
        End Function

        Public Sub DrawImages(ByVal currentimage As Image,
                              Optional ByVal currentoffimage As Image = Nothing,
                              Optional ByVal onlyrenderalwaysonlights As Boolean = False,
                              Optional ByVal onlyrenderonlights As Boolean = False,
                              Optional ByVal rominfofilter As String = "")
            Cursor.Current = Cursors.WaitCursor
            RaiseEvent ReportProgress(Me, New LightsProgressEventArgs(0))
            Dim progress As Integer = 0
            'ClearImages()
            If bulbs.Count > 0 Then
                progress += 1
                RaiseEvent ReportProgress(Me, New LightsProgressEventArgs(progress / bulbs.Count))
                For Each bulb As Illumination.BulbInfo In bulbs
                    With bulb
                        If Not newimages.ContainsKey(.ID) OrElse .IsIlluminatedImageDirty Then
                            If .IsIlluminatedImageDirty Then
                                newimages.Remove(.ID)
                            End If
                            Dim getin As Boolean = ((String.IsNullOrEmpty(rominfofilter) OrElse rominfofilter.Equals(.B2SInfo2String) OrElse rominfofilter.Equals(.RomInfo2String)) OrElse
                                                    (rominfofilter.Equals("withoutid") AndAlso ((Backglass.currentData.CommType = eCommType.B2S AndAlso String.IsNullOrEmpty(.B2SInfo2String)) OrElse (Backglass.currentData.CommType = eCommType.Rom AndAlso String.IsNullOrEmpty(.RomInfo2String)))) OrElse
                                                    (rominfofilter.Equals("withname") AndAlso Not String.IsNullOrEmpty(.Name)) OrElse
                                                    (rominfofilter.Equals("off") AndAlso .InitialState = 0) OrElse
                                                    (rominfofilter.Equals("on") AndAlso .InitialState = 1) OrElse
                                                    (rominfofilter.Equals("alwayson") AndAlso .InitialState = 2) OrElse
                                                    (rominfofilter.Equals("authentic") AndAlso .DualMode <> eDualMode.Fantasy) OrElse
                                                    (rominfofilter.Equals("fantasy") AndAlso .DualMode <> eDualMode.Authentic))
                            getin = (getin AndAlso ((Not onlyrenderalwaysonlights AndAlso Not onlyrenderonlights) OrElse
                                                    (onlyrenderalwaysonlights AndAlso .InitialState = 2) OrElse
                                                    (onlyrenderonlights AndAlso .InitialState >= 1)))
                            If getin Then
                                Dim imageinfo As ImageInfo = Nothing
                                Dim illuimage As Image = GetImageByID(.ID)
                                If illuimage IsNot Nothing AndAlso Not .IsImageSnippit AndAlso .IsIlluminatedImageDirty Then
                                    illuimage.Dispose()
                                    illuimage = Nothing
                                End If
                                If Not (.IsImageSnippit AndAlso .Image IsNot Nothing) Then
                                    Dim rect As Rectangle = New Rectangle(.Location, .Size)
                                    Dim rectX As Rectangle = New Rectangle(.LocationX, .SizeX)
                                    If rectX.Width > 0 AndAlso rectX.Height > 0 Then
                                        If Not String.IsNullOrEmpty(.Text) Then
                                            Dim font As Font = New Font(.FontName, .FontSize, .FontStyle)
                                            Dim lowerintensity As Integer = Math.Max(.Intensity - 2, 1)
                                            If .DodgeColor <> Nothing Then lowerintensity = 3
                                            'Dim lowerintensity As Integer = 1
                                            If illuimage Is Nothing Then illuimage = lightcreation.CreateOverlayImage(currentimage, rect, rectX, .Intensity, .LightColor, .DodgeColor, .Text, font, .TextAlignment, .IlluMode)
                                            imageinfo = New ImageInfo(.ID, .Name, illuimage,
                                                                             If(currentoffimage IsNot Nothing, lightcreation.CreateOverlayImage(currentoffimage, rect, rectX, lowerintensity, .LightColor, Nothing, .Text, font, .TextAlignment, .IlluMode), Nothing),
                                                                             rectX,
                                                                             True)
                                            font.Dispose()
                                        Else
                                            Dim lowerintensity As Integer = Math.Max(.Intensity - 2, 1)
                                            If .DodgeColor <> Nothing Then lowerintensity = 3
                                            'Dim lowerintensity As Integer = 1
                                            If illuimage Is Nothing Then illuimage = lightcreation.CreateOverlayImage(currentimage, rect, rectX, .Intensity, .LightColor, .DodgeColor, "", Nothing, eTextAlignment.Center, .IlluMode)
                                            imageinfo = New ImageInfo(.ID, .Name, illuimage,
                                                                             If(currentoffimage IsNot Nothing, lightcreation.CreateOverlayImage(currentoffimage, rect, rectX, lowerintensity, .LightColor, Nothing, "", Nothing, eTextAlignment.Center, .IlluMode), Nothing),
                                                                             rectX,
                                                                             True)
                                        End If
                                    End If
                                Else
                                    Dim rect As Rectangle = New Rectangle(.Location, .Size)
                                    If illuimage Is Nothing Then illuimage = .Image
                                    imageinfo = New ImageInfo(.ID, .Name, illuimage,
                                                                     Nothing,
                                                                     rect,
                                                                     False)
                                End If
                                ' maybe add newimages entry
                                If imageinfo IsNot Nothing Then
                                    newimages.Add(.ID, imageinfo)
                                    .IsIlluminatedImageDirty = False
                                End If
                            End If
                        End If
                    End With
                Next
            End If
            RaiseEvent ReportProgress(Me, New LightsProgressEventArgs(100))
            Cursor.Current = Cursors.Default
        End Sub
        Public Sub ClearImages()
            For i As Integer = newimages.Count - 1 To 0 Step -1
                If newimages(newimages.Keys(i)).Disposable AndAlso newimages(newimages.Keys(i)).Image IsNot Nothing Then
                    newimages(newimages.Keys(i)).Image.Dispose()
                    newimages(newimages.Keys(i)).Image = Nothing
                End If
            Next
            newimages.Clear()
        End Sub

        Public Function DrawIlluminatedReelImage(ByVal reelimage As Image, ByVal reelintensity As Integer, ByVal reelillulocation As eReelIlluminationLocation) As Image
            If reelillulocation = eReelIlluminationLocation.Off Then
                Return reelimage
            Else
                Dim intensity As Integer = 1
                Dim halfheight As Integer = 1
                If reelillulocation = eReelIlluminationLocation.Above OrElse reelillulocation = eReelIlluminationLocation.AboveAndBelow Then
                    halfheight = CInt(reelimage.Height / 2) + CInt(reelimage.Height / 10)
                Else
                    halfheight = CInt(reelimage.Height / 13)
                    If halfheight < 4 Then halfheight = 4
                End If
                Dim ret As Image = lightcreation.CreateImage(reelimage, New Rectangle(0, -1 * halfheight, reelimage.Width, 2 * halfheight), New Rectangle(0, 0, reelimage.Width, halfheight), reelintensity, Nothing, Nothing, "", Nothing, eTextAlignment.Center, eIlluMode.Standard)
                If reelillulocation = eReelIlluminationLocation.Below OrElse reelillulocation = eReelIlluminationLocation.AboveAndBelow Then
                    halfheight = CInt(reelimage.Height / 2) + CInt(reelimage.Height / 10)
                Else
                    halfheight = CInt(reelimage.Height / 13)
                    If halfheight < 4 Then halfheight = 4
                End If
                Return lightcreation.CreateImage(ret, New Rectangle(0, ret.Height - halfheight, ret.Width, 2 * halfheight), New Rectangle(0, ret.Height - halfheight, ret.Width, halfheight), reelintensity, Nothing, Nothing, "", Nothing, eTextAlignment.Center, eIlluMode.Standard)
            End If
        End Function

        Public Sub New(ByRef _parent As B2SPictureBox, ByVal _IsDMD As Boolean)
            parent = _parent
            isDMD = _IsDMD
        End Sub

    End Class

End Namespace