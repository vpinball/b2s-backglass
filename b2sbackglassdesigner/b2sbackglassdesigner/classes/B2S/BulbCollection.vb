Namespace Illumination

    Public Class BulbCollection

        Inherits Generic.SortedList(Of Integer, Illumination.BulbInfo)

        Public Shadows Sub Add(value As Illumination.BulbInfo)
            Dim key As Integer = 1
            Dim id As Integer = 1
            For Each bulb As KeyValuePair(Of Integer, BulbInfo) In Me
                If key <= bulb.Key Then key = bulb.Key + 1
                If id <= bulb.Value.ID Then id = bulb.Value.ID + 1
            Next
            value.ID = id
            MyBase.Add(key, value)
        End Sub

    End Class

End Namespace