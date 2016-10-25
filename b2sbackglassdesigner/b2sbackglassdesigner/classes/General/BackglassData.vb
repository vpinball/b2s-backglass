Public Class BackglassData

    Private _text As String = String.Empty
    Public Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            If Not _text.Equals(value) Then
                _text = value
                IsDirty = True
            End If
        End Set
    End Property

    Private _tabletype As TableType = B2SBackglassDesigner.TableType.NotDefined
    Public Property TableType() As TableType
        Get
            Return _tabletype
        End Get
        Set(ByVal value As TableType)
            If Not _tabletype.Equals(value) Then
                _tabletype = value
                IsDirty = True
            End If
        End Set
    End Property

    Private _image As Image = Nothing
    Private _imagefilename As String = String.Empty
    Public Property Image() As Image
        Get
            Return _image
        End Get
        Set(ByVal value As Image)
            If _image IsNot value Then
                _image = value
                IsDirty = True
            End If
        End Set
    End Property
    Public Property ImageFileName() As String
        Get
            Return _imagefilename
        End Get
        Set(ByVal value As String)
            If Not _imagefilename.Equals(value) Then
                _imagefilename = value
                IsDirty = True
            End If
        End Set
    End Property

    Private _NumberOfPlayers As Integer = 0
    Public Property NumberOfPlayers() As Integer
        Get
            Return _NumberOfPlayers
        End Get
        Set(ByVal value As Integer)
            If _NumberOfPlayers <> value Then
                _NumberOfPlayers = value
                NumberOfPlayers2Reels()
                IsDirty = True
            End If
        End Set
    End Property

    Public Property Reels() As ReelAndLED.ReelCollection = New ReelAndLED.ReelCollection

    Public Property Bulbs() As Illumination.BulbCollection = New Illumination.BulbCollection

    Public Property IsDirty() As Boolean = False

    Public Property Zoom() As Integer = 100

    Private Sub NumberOfPlayers2Reels()
        If NumberOfPlayers > Reels.Count Then
            Dim x As Integer = 300
            Dim y As Integer = 100
            Do While NumberOfPlayers > Reels.Count
                Dim newreel As ReelAndLED.ReelInfo = New ReelAndLED.ReelInfo()
                With newreel
                    .ReelType = TableType
                    .Location = New Point(x, y)
                    .Size = New Size(300, 100)
                End With
                Reels.Add(newreel)
                x += 40
                y += 30
            Loop
        ElseIf NumberOfPlayers < Reels.Count Then
            Do While NumberOfPlayers < Reels.Count
                Reels.RemoveAt(Reels.Count - 1)
            Loop
        End If
    End Sub

End Class
