Public Class InfoBase

    Public ID As Integer = 0

    Public Location As Point = Nothing
    Public Size As Size = Nothing

    Public ReadOnly Property LocationX() As Point
        Get
            Dim loc As Point = Me.Location
            Dim image As Image = If(Backglass.currentData IsNot Nothing, If(ParentForm = eParentForm.DMD, Backglass.currentData.DMDImage, Backglass.currentData.Image), Nothing)
            If loc.X < 0 Then
                loc.X = 0
            ElseIf image IsNot Nothing AndAlso loc.X > image.Width Then
                loc.X = image.Width
            End If
            If loc.Y < 0 Then
                loc.Y = 0
            ElseIf image IsNot Nothing AndAlso loc.Y > image.Height Then
                loc.Y = image.Height
            End If
            Return loc
        End Get
    End Property
    Public ReadOnly Property SizeX() As Size
        Get
            Dim size As Size = Me.Size
            Dim image As Image = If(Backglass.currentData IsNot Nothing, If(ParentForm = eParentForm.DMD, Backglass.currentData.DMDImage, Backglass.currentData.Image), Nothing)
            If Location.X < 0 Then
                size.Width = size.Width + Location.X
            ElseIf image IsNot Nothing AndAlso Location.X > image.Width Then
                size.Width = 0
            ElseIf image IsNot Nothing AndAlso Location.X + size.Width > image.Width Then
                size.Width = image.Width - Location.X
            End If
            If size.Width < 0 Then size.Width = 0
            If image IsNot Nothing AndAlso size.Width > image.Width Then size.Width = image.Width
            If Location.Y < 0 Then
                size.Height = size.Height + Location.Y
            ElseIf image IsNot Nothing AndAlso Location.Y > image.Height Then
                size.Height = 0
            ElseIf image IsNot Nothing AndAlso Location.Y + size.Height > image.Height Then
                size.Height = image.Height - Location.Y
            End If
            If size.Height < 0 Then size.Height = 0
            If image IsNot Nothing AndAlso size.Height > image.Height Then size.Height = image.Height
            Return size
        End Get
    End Property

    Public B2SID As Integer = 0
    Public B2SIDType As eB2SIDType = eB2SIDType.NotUsed
    Public B2SValue As Integer = 0

    Public RomID As Integer = 0
    Public RomIDType As eRomIDType = eRomIDType.NotUsed
    Public RomInverted As Boolean = False

    Public ReadOnly Property B2SInfo2String() As String
        Get
            If B2SID > 0 Then
                Return B2SID.ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property
    Public ReadOnly Property RomInfo2String() As String
        Get
            If RomID > 0 AndAlso RomIDType <> eRomIDType.NotUsed Then
                Dim ret As String = If(RomInverted, "I", String.Empty)
                Select Case RomIDType
                    Case eRomIDType.Solenoid
                        ret &= "S"
                    Case eRomIDType.GIString
                        ret &= "GI"
                    Case Else
                        ret &= "L"
                End Select
                Return ret & RomID.ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ParentForm As eParentForm = eParentForm.NotDefined

End Class
