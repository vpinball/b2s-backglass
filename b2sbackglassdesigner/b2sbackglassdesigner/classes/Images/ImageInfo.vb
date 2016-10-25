Namespace Images

    Public Enum eBackgroundImageType
        NotUsed = 0
        Off = 1
        [On] = 2
    End Enum

    Public Enum eImageInfoType
        Undefined = 0
        Title4BackgroundImages = 1
        Title4IlluminationImages = 2
        Title4DMDImages = 3
        Title4IlluminationSnippits = 4
        BackgroundImage = 11
        IlluminationImage = 12
        DMDImage = 13
        IlluminationSnippits = 14
    End Enum

    Public Class ImageInfo

        Inherits InfoBase

        Public Type As eImageInfoType = eImageInfoType.Undefined
        Public Text As String = String.Empty
        Public Image As Image = Nothing

        Public BackgroundImageType As eBackgroundImageType = eBackgroundImageType.NotUsed

        Public Sub New(ByVal _type As eImageInfoType)
            Type = _type
        End Sub
        Public Sub New(ByVal _type As eImageInfoType, ByVal _text As String, ByVal _image As Image)
            Type = _type
            Text = _text
            Image = _image
        End Sub
        Public Sub New(ByVal _type As eImageInfoType, ByVal _text As String)
            Type = _type
            Text = _text
        End Sub

    End Class

End Namespace