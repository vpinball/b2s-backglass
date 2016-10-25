Namespace Images

    Public Class ImageCollection

        Inherits Generic.List(Of Images.ImageInfo)

        Public Sub Init()
            Me.Add(New Images.ImageInfo(Images.eImageInfoType.Title4BackgroundImages, My.Resources.IMAGES_BackgroundImages))
            Me.Add(New Images.ImageInfo(Images.eImageInfoType.Title4IlluminationImages, My.Resources.IMAGES_IlluminationImages))
            Me.Add(New Images.ImageInfo(Images.eImageInfoType.Title4DMDImages, My.Resources.IMAGES_DMDImages))
            Me.Add(New Images.ImageInfo(Images.eImageInfoType.Title4IlluminationSnippits, My.Resources.IMAGES_IlluminationSnippits))
        End Sub

        Public Shadows Sub Insert(ByVal titletype As Images.eImageInfoType, ByVal item As Images.ImageInfo)
            MyBase.Insert(indexInImageList(titletype), item)
        End Sub

        Public Sub Resize(ByVal _type As eImageInfoType, ByVal newimagesize As Size)
            For Each imageinfo As Images.ImageInfo In Me
                If imageinfo.Type = _type Then
                    imageinfo.Image = imageinfo.Image.Resized(newimagesize)
                End If
            Next
        End Sub

        Public Function CurrentDMDImageInfo() As Images.ImageInfo
            Dim ret As Images.ImageInfo = Nothing
            For Each info As Images.ImageInfo In Me
                If info.Type = eImageInfoType.DMDImage Then
                    ret = info
                    Exit For
                End If
            Next
            Return ret
        End Function

        Private Function indexInImageList(ByVal titleType As Images.eImageInfoType) As Integer
            Dim ret As Integer = 0
            If titleType = Images.eImageInfoType.Title4IlluminationSnippits Then
                ret = Me.Count
            Else
                For i As Integer = 0 To Me.Count - 1
                    If (titleType = Images.eImageInfoType.Title4BackgroundImages AndAlso Me(i).Type = Images.eImageInfoType.Title4IlluminationImages) OrElse
                       (titleType = Images.eImageInfoType.Title4IlluminationImages AndAlso Me(i).Type = Images.eImageInfoType.Title4DMDImages) OrElse
                       (titleType = Images.eImageInfoType.Title4DMDImages AndAlso Me(i).Type = Images.eImageInfoType.Title4IlluminationSnippits) Then
                        ret = i
                        Exit For
                    End If
                Next
            End If
            Return ret
        End Function

    End Class

End Namespace