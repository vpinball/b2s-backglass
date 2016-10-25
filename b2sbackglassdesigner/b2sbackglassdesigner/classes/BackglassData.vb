Public Class BackglassData

    Public Property Text() As String = String.Empty
    Public Property TableType() As TableType = TableType.NotDefined

    Public Property Image() As Image = Nothing
    Public Property ImageFileName As String = String.Empty

    Public Property Bulbs() As B2SIllumination.BulbCollection = New B2SIllumination.BulbCollection

    Public Property IsDirty() As Boolean = False

    Public Property Zoom() As Integer = 100

End Class
