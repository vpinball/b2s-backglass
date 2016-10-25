Imports System

Namespace B2SIllumination

    Public Class BulbInfo

        Public Name As String = String.Empty

        Public Visible As Boolean = True

        Public Location As Point = Nothing
        Public Size As Size = Nothing

        Public Dodge As Boolean = False
        Public Intensity As Integer = 1

    End Class

    Public Class BulbCollection

        Inherits Generic.Dictionary(Of String, BulbInfo)

    End Class

End Namespace