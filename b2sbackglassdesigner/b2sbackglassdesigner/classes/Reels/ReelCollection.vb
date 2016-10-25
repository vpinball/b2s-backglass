Namespace ReelAndLED

    Public Class ReelCollection

        Inherits Generic.SortedList(Of Integer, ReelAndLED.ReelInfo)

        Public Shadows Sub Add(value As ReelAndLED.ReelInfo)
            Dim key As Integer = 1
            Dim id As Integer = 1
            For Each reel As KeyValuePair(Of Integer, ReelAndLED.ReelInfo) In Me
                If key <= reel.Key Then key = reel.Key + 1
                If id <= reel.Value.ID Then id = reel.Value.ID + 1
            Next
            value.ID = id
            MyBase.Add(key, value)
        End Sub

    End Class

End Namespace