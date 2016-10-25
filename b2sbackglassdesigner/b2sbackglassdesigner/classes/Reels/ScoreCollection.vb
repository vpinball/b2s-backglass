Namespace ReelAndLED

    Public Class ScoreCollection

        Inherits Generic.List(Of ReelAndLED.ScoreInfo)

        Public Shadows Sub Add(value As ReelAndLED.ScoreInfo)
            Dim id As Integer = 1
            For Each score As ReelAndLED.ScoreInfo In Me
                If id <= score.ID Then id = score.ID + 1
            Next
            value.ID = id
            MyBase.Insert(0, value)
        End Sub
        Public Shadows Sub Add(ByVal id As Integer, value As ReelAndLED.ScoreInfo)
            value.ID = id
            MyBase.Insert(0, value)
        End Sub

        Public Sub Resize(ByVal currentimagesize As Size, ByVal newimagesize As Size)
            Dim x As Single = currentimagesize.Width / newimagesize.Width
            Dim y As Single = currentimagesize.Height / newimagesize.Height
            For Each score As ReelAndLED.ScoreInfo In Me
                score.Location.X = Math.Round(score.Location.X / x)
                score.Location.Y = Math.Round(score.Location.Y / y)
                score.Size.Width = Math.Round(score.Size.Width / x)
                score.Size.Height = Math.Round(score.Size.Height / y)
            Next
        End Sub

    End Class

End Namespace