Public Class B2SCollectData

    Inherits Generic.Dictionary(Of Integer, CollectData)

    Private skipframes As Integer = 0

    Public Enum eCollectedDataType
        TopImage = 1
        SecondImage = 2
        Standard = 4
        Animation = 8
    End Enum

    Public Class CollectData
        Public State As Integer = 0
        Public Types As Integer = 0
        Public EarlyOffMode As Boolean = False

        Public Sub New(_state As Integer, _type As eCollectedDataType)
            State = _state
            Types = _type
        End Sub
        Public Sub New(_state As Integer, _type As eCollectedDataType, _earlyoffmode As Boolean)
            State = _state
            Types = _type
            EarlyOffMode = _earlyoffmode
        End Sub
    End Class

    Public Sub New(_skipframes As Integer)
        skipframes = _skipframes
    End Sub

    Public Shadows Function Add(key As Integer, value As CollectData) As Boolean
        Dim ret As Boolean = False
        If Me.ContainsKey(key) Then
            If value.EarlyOffMode AndAlso Me(key).State = 0 AndAlso value.State = 0 Then
                Me(key).State = 2
            Else
                Me(key).State = value.State
            End If
            Me(key).Types = Me(key).Types Or value.Types
            ret = True
        Else
            MyBase.Add(key, value)
        End If
        Return ret
    End Function

    Public Sub DataAdded()
        skipframes -= 1
    End Sub

    Public ReadOnly Property ShowData() As Boolean
        Get
            Return (skipframes < 0)
        End Get
    End Property

    Public Sub ClearData(_skipframes As Integer)
        MyBase.Clear()
        If skipframes <= 0 Then skipframes = _skipframes
    End Sub

End Class
