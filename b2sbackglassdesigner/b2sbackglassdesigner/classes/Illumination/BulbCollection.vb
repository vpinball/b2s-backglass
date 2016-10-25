Namespace Illumination

    Public Class BulbCollection

        Inherits Generic.List(Of Illumination.BulbInfo)

        Public Shadows Sub Add(value As Illumination.BulbInfo)
            If value.ID <= 0 Then
                Dim id As Integer = 1
                For Each bulb As Illumination.BulbInfo In Me
                    If id <= bulb.ID Then id = bulb.ID + 1
                Next
                value.ID = id
            End If
            MyBase.Insert(0, value)
        End Sub

        Public Sub Resize(ByVal currentimagesize As Size, ByVal newimagesize As Size)
            Dim x As Single = currentimagesize.Width / newimagesize.Width
            Dim y As Single = currentimagesize.Height / newimagesize.Height
            For Each bulb As Illumination.BulbInfo In Me
                bulb.Location.X = Math.Round(bulb.Location.X / x)
                bulb.Location.Y = Math.Round(bulb.Location.Y / y)
                bulb.Size.Width = Math.Round(bulb.Size.Width / x)
                bulb.Size.Height = Math.Round(bulb.Size.Height / y)
            Next
        End Sub

    End Class

End Namespace