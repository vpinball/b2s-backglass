Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.ComponentModel

Friend Class Segment
    Private FocusScales As PointF
    Private Lights(5) As PointF
    Private LightDot As RectangleF
    Private Points(5) As PointF

    Public Property Style As SegmentStyle
    Public Property IsOn As Boolean
    Public Property Name As String

#Region "constructors"

    Public Sub New(ByVal sName As String, ByVal x As Single, ByVal y As Single, ByVal width As Single, ByVal height As Single, _
                   ByVal angle As Single)
        InitSegment(sName, x, y, width, height, angle, SegmentCap.Standard, SegmentCap.Standard, 45)
    End Sub

    Public Sub New(ByVal sName As String, ByVal x As Single, ByVal y As Single, ByVal width As Single, ByVal height As Single, _
                   ByVal angle As Single, ByVal topcap As SegmentCap, ByVal bottomcap As SegmentCap)
        InitSegment(sName, x, y, width, height, angle, topcap, bottomcap, 45)
    End Sub

    Public Sub New(ByVal sName As String, ByVal x As Single, ByVal y As Single, ByVal width As Single, ByVal height As Single, _
                   ByVal angle As Single, ByVal topcap As SegmentCap, ByVal bottomcap As SegmentCap, ByVal capangle As Single)
        InitSegment(sName, x, y, width, height, angle, topcap, bottomcap, capangle)
    End Sub

    Public Sub New(ByVal x As Single, ByVal y As Single, ByVal radius As Single)
        InitSegmentDot(x, y, radius)
    End Sub

#End Region

#Region "public properties"

    Private oGlassPath As GraphicsPath
    Public ReadOnly Property GlassPath As GraphicsPath
        Get
            GetGlassData()
            Return oGlassPath
        End Get
    End Property
    Public ReadOnly Property GlassPathTransformed As GraphicsPath
        Get
            Dim oPath As GraphicsPath = GlassPath.Clone
            oPath.Transform(oOwnMatrix)
            Return oPath
        End Get
    End Property

#End Region

#Region "public functions"

    Public Sub AssignStyle()
        ResetCacheData()
    End Sub

    Public Sub Draw(ByVal oGraphics As Graphics)
        SetTransform(oGraphics)
        GetGlassData()
        Dim oBrush As Brush
        If IsOn Then
            oBrush = New PathGradientBrush(oGlassPath)
            With DirectCast(oBrush, PathGradientBrush)
                .CenterColor = (Color.FromArgb(Style.GlassAlphaCenter, Style.GlassColorCenter))
                .SurroundColors = New Color() {Color.FromArgb(Style.GlassAlpha, Style.GlassColor)}
                .FocusScales = FocusScales
            End With
            PaintSegment(oGraphics, oBrush, Pens.Red, oGlassPath)
        Else
            oBrush = New SolidBrush(Style.OffColor)
            PaintSegment(oGraphics, oBrush, Pens.DarkGray, oGlassPath)
        End If
        oGraphics.ResetTransform()
        oBrush.Dispose()
    End Sub

    Public Sub DrawLight(ByVal oGraphics As Graphics)
        If Not IsOn Then Return
        SetTransform(oGraphics)
        GetLightData()
        PaintSegment(oGraphics, oLightBrush, Pens.Yellow, oLightPath)
        oGraphics.ResetTransform()
    End Sub

#End Region

#Region "private functions"

    Private Sub InitSegmentDot(ByVal x As Single, ByVal y As Single, ByVal radius As Single)
        Name = "."
        Points(0) = New PointF(radius, radius)
        nRadius = radius

        CreateLightData()

        oOwnMatrix = New Matrix
        oOwnMatrix.Translate(x, y)
    End Sub

    Private Sub InitSegment(ByVal sName As String, ByVal x As Single, ByVal y As Single, ByVal width As Single, ByVal height As Single, _
                           ByVal angle As Single, ByVal topcap As SegmentCap, ByVal bottomcap As SegmentCap, ByVal capangle As Single)
        Name = sName
        Dim nBounds As New RectangleF(0, 0, width, height)
        Dim topleft As Single, topright As Single, topdelta As Single
        Dim bottomleft As Single, bottomright As Single, bottomdelta As Single
        LeftRightFromCap(topcap, width, capangle, topleft, topright, topdelta)
        LeftRightFromCap(bottomcap, width, capangle, bottomleft, bottomright, bottomdelta)

        Points(0) = New PointF(nBounds.Left + topdelta, nBounds.Top)
        Points(1) = New PointF(nBounds.Right, nBounds.Top + If(topcap = SegmentCap.Flat, 0, topright))
        Points(2) = New PointF(nBounds.Right, nBounds.Bottom - If(bottomcap = SegmentCap.Flat, 0, bottomright))
        Points(3) = New PointF(nBounds.Left + bottomdelta, nBounds.Bottom)
        Points(4) = New PointF(nBounds.Left, nBounds.Bottom - If(bottomcap = SegmentCap.Flat, 0, bottomleft))
        Points(5) = New PointF(nBounds.Left, nBounds.Top + If(topcap = SegmentCap.Flat, 0, topleft))

        nAngle = angle
        CreateLightData()

        oOwnMatrix = New Matrix
        oOwnMatrix.Translate(x, y)
        oOwnMatrix.Rotate(angle)
    End Sub

    Private nGlow As Single = -1
    Private Sub CreateLightData()
        If Style Is Nothing OrElse Me.nGlow = Style.Glow Then Return
        Me.nGlow = Style.Glow
        If nRadius > 0 Then
            ' extra light for the dot
            LightDot = New RectangleF(Points(0).X - nGlow, Points(0).Y - nGlow, nRadius + nGlow * 2, nRadius + nGlow * 2)
        Else
            Lights(0) = PointF.Add(Points(0), New SizeF(0, -nGlow))
            Lights(1) = PointF.Add(Points(1), New SizeF(nGlow, 0))
            Lights(2) = PointF.Add(Points(2), New SizeF(nGlow, 0))
            Lights(3) = PointF.Add(Points(3), New SizeF(0, nGlow))
            Lights(4) = PointF.Add(Points(4), New SizeF(-nGlow, 0))
            Lights(5) = PointF.Add(Points(5), New SizeF(-nGlow, 0))
            ' bulb size
        End If
    End Sub

    Private Sub SetBulbSize()
        If Style.BulbSize.IsEmpty Then
            FocusScales = PointF.Empty
        Else
            With Style.BulbSize
                If nAngle = -90 Then
                    FocusScales = New PointF(.Height, .Width)
                ElseIf nAngle = 0 Then
                    FocusScales = New PointF(.Width, .Height)
                Else
                    Dim nDiag As Single = (.Width + .Height) / 2
                    FocusScales = New PointF(nDiag, nDiag)
                End If
            End With
        End If
    End Sub

    Private nAngle As Single
    Private nRadius As Single

    Private Sub LeftRightFromCap(ByVal nCap As SegmentCap, ByVal nWidth As Single, ByVal nCapangle As Single, _
                                 ByRef nLeft As Single, ByRef nRight As Single, ByRef nDelta As Single)
        Select Case nCap
            Case SegmentCap.Standard, SegmentCap.Flat
                nLeft = nWidth / 2.0
                nRight = nWidth / 2.0
            Case SegmentCap.Left
                nLeft = 0
                nRight = nWidth
            Case SegmentCap.Right
                nLeft = nWidth
                nRight = 0
            Case SegmentCap.MoreLeft
                nLeft = nWidth / 4.0
                nRight = nWidth * 3.0 / 4.0
            Case SegmentCap.MoreRight
                nLeft = nWidth * 3.0 / 4.0
                nRight = nWidth / 4.0
        End Select
        nDelta = nLeft
        nLeft = Math.Tan(nCapangle / 180 * Math.PI) * nLeft
        nRight = Math.Tan(nCapangle / 180 * Math.PI) * nRight
    End Sub

    Private Sub PaintSegment(ByVal oGraphics As Graphics, ByVal oBrush As Brush, ByVal oPen As Pen, ByVal oPath As GraphicsPath)
        If Style.WireFrame Then
            oGraphics.DrawPath(oPen, oPath)
        Else
            oGraphics.FillPath(oBrush, oPath)
        End If
        Dream7Display.SegmentPaintCounter += 1
    End Sub

    Private Sub GetGlassData()
        If oGlassPath Is Nothing Then
            oGlassPath = New GraphicsPath()
            If nRadius > 0 Then
                oGlassPath.AddEllipse(Points(0).X, Points(0).Y, nRadius, nRadius)
            Else
                oGlassPath.AddPolygon(Points)
            End If
            SetBulbSize()
        End If
    End Sub

    Private oLightPath As GraphicsPath
    Private oLightBrush As PathGradientBrush
    Private Sub GetLightData()
        If oLightPath Is Nothing Then
            CreateLightData()
            oLightPath = New GraphicsPath()
            If nRadius > 0 Then
                oLightPath.AddEllipse(LightDot)
            Else
                oLightPath.AddClosedCurve(Lights, 0.5)
            End If
            SetBulbSize()
        End If
        If oLightBrush Is Nothing Then
            ' create light brush
            oLightBrush = New PathGradientBrush(oLightPath)
            oLightBrush.SurroundColors = New Color() {Color.Transparent}
            oLightBrush.CenterColor = Color.FromArgb(255, Style.LightColor)
            ' huge performance impact
            If Not FocusScales.IsEmpty Then oLightBrush.FocusScales = FocusScales
            'oLightBrush.SetSigmaBellShape(1, 0.6)
        End If
    End Sub

    Private Sub ResetCacheData()
        If oGlassPath IsNot Nothing Then oGlassPath.Dispose()
        oGlassPath = Nothing
        If oLightPath IsNot Nothing Then oLightPath.Dispose()
        oLightPath = Nothing
        If oLightBrush IsNot Nothing Then oLightBrush.Dispose()
        oLightBrush = Nothing
    End Sub

    Private Sub SetTransform(ByVal oGraphics As Graphics)
        Dim oMatrix As Matrix
        If oExternMatrix Is Nothing Then oMatrix = New Matrix Else oMatrix = oExternMatrix.Clone
        oMatrix.Multiply(oOwnMatrix)
        oGraphics.Transform = oMatrix
    End Sub

    Private oOwnMatrix As Matrix
    Private oExternMatrix As Matrix
    Public Sub Transform(ByVal oMatrix As Matrix)
        Me.oExternMatrix = oMatrix
    End Sub

#End Region

End Class

#Region "helper classes"

Friend Class SegmentList
    Inherits Generic.List(Of Segment)

    Public Sub Transform(ByVal oMatrix As Matrix)
        For Each oSegment As Segment In Me
            oSegment.Transform(oMatrix)
        Next
    End Sub
End Class

Friend Enum SegmentCap
    Standard = 0
    Flat = 1
    MoreLeft = 2
    Left = 3
    MoreRight = 4
    Right = 5
End Enum

#End Region
