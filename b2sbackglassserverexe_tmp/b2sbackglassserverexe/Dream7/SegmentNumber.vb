Imports System.Drawing.Drawing2D
Imports System.Drawing

Friend Class SegmentNumber
    Private oSegments As New SegmentList
    Private oSegmentStyle As New SegmentStyle

    Private Property Type As SegmentNumberType
    Private Property Thickness As Single = 16.0

#Region "invalidate handler for the parent control"

    Public Event Invalidated As EventHandler

    Public Sub OnInvalidated()
        RaiseEvent Invalidated(Me, New System.EventArgs)
    End Sub

#End Region

#Region "public methods"

    Public Sub Init(ByVal nLocation As PointF, ByVal nType As SegmentNumberType, ByVal oMatrix As Matrix, ByVal nThickness As Single)
        If Me.Type <> nType OrElse Me.Thickness <> nThickness Then
            InitSegments(nType, nThickness)
            DisplayCharacter(sCharacter)
        End If
        InitMatrix(nLocation, oMatrix)
    End Sub

    Public Sub AssignStyle()
        For Each oSegment As Segment In oSegments
            oSegment.AssignStyle()
        Next
        OnInvalidated()
    End Sub

    Public Sub Draw(ByVal oGraphics As Graphics)
        oGraphics.ResetTransform()
        oGraphics.CompositingMode = CompositingMode.SourceCopy
        oGraphics.ExcludeClip(GetSegmentRegions)
        oGraphics.FillPath(Brushes.Black, Me.Bounds)
        oGraphics.ResetClip()
        For Each oSegment As Segment In oSegments
            If oSegment.IsOn Then Continue For
            oSegment.Draw(oGraphics)
        Next
        If Style.Glow > 0 Then
            oGraphics.CompositingMode = CompositingMode.SourceOver
            For Each oSegment As Segment In oSegments
                If Not oSegment.IsOn Then Continue For
                oSegment.DrawLight(oGraphics)
            Next
        Else
            oGraphics.CompositingMode = CompositingMode.SourceCopy
        End If
        oGraphics.ResetClip()
        For Each oSegment As Segment In oSegments
            If Not oSegment.IsOn Then Continue For
            oSegment.Draw(oGraphics)
        Next
        oGraphics.ResetClip()
        oGraphics.ResetTransform()
    End Sub

#End Region

#Region "public properties"

    Public ReadOnly Property Bounds As GraphicsPath
        Get
            Dim oBounds As New RectangleF(-14, -14, 173, 272)
            Dim oRegion As New GraphicsPath()
            oRegion.AddRectangle(oBounds)
            oRegion.Transform(oNumberMatrix)
            Return oRegion
        End Get
    End Property

    Public ReadOnly Property Style As SegmentStyle
        Get
            Return oSegmentStyle
        End Get
    End Property

    Private sCharacter As String
    Public Property Character As String
        Get
            Return sCharacter
        End Get
        Set(ByVal value As String)
            sCharacter = value
            DisplayCharacter(sCharacter)
        End Set
    End Property

#End Region

#Region "segment coordinates"

    Private Sub InitSegments(ByVal nType As SegmentNumberType, ByVal nThickness As Single)
        oSegments.Clear()
        Dim TH As Single = nThickness
        Dim T4 As Single = TH / 4.0
        Dim T2 As Single = TH / 2.0
        Me.Type = nType
        Me.Thickness = TH
        Select Case nType
            Case SegmentNumberType.SevenSegment
                oSegments.AddRange(New Segment() { _
                    New Segment("a", T4 + 2, TH, TH, 120 - T2, -90, SegmentCap.MoreRight, SegmentCap.MoreRight), _
                    New Segment("b", 124 - TH, T4 + 2, TH, 110 - T4, 0, SegmentCap.MoreRight, SegmentCap.Standard), _
                    New Segment("c", 124 - TH, 116, TH, 110 - T4, 0, SegmentCap.Standard, SegmentCap.MoreRight), _
                    New Segment("d", T4 + 2, 228, TH, 120 - T2, -90, SegmentCap.MoreLeft, SegmentCap.MoreLeft), _
                    New Segment("e", 0, 116, TH, 110 - T4, 0, SegmentCap.Standard, SegmentCap.MoreLeft), _
                    New Segment("f", 0, T4 + 2, TH, 110 - T4, 0, SegmentCap.MoreLeft, SegmentCap.Standard), _
                    New Segment("g", T2 + 2, 114 + T2, TH, 120 - TH, -90), _
                    New Segment(135 - T4 * 5, 228 - T4 * 5, TH) _
                })
            Case SegmentNumberType.FourteenSegment
                Dim nAngleDiag As Single = 26.5 + T4
                Dim MT As Single = TH - T4
                Dim nSinA As Single = Math.Sin(nAngleDiag / 180.0 * Math.PI)
                Dim nCosA As Single = Math.Cos(nAngleDiag / 180.0 * Math.PI)
                Dim nTanA As Single = Math.Tan(nAngleDiag / 180.0 * Math.PI)
                Dim nDiagX As Single = nCosA * MT
                Dim nDiagY As Single = nSinA * MT
                Dim nDiagHeight As Single = (58 - TH - MT / 2) / nSinA + MT / nTanA
                oSegments.AddRange(New Segment() { _
                    New Segment("a", T4 + 2, TH, TH, 120 - T2, -90, SegmentCap.MoreRight, SegmentCap.MoreRight), _
                    New Segment("b", 124 - TH, T4 + 2, TH, 110 - T4, 0, SegmentCap.MoreRight, SegmentCap.Standard), _
                    New Segment("c", 124 - TH, 116, TH, 110 - T4, 0, SegmentCap.Standard, SegmentCap.MoreRight), _
                    New Segment("d", T4 + 2, 228, TH, 120 - T2, -90, SegmentCap.MoreLeft, SegmentCap.MoreLeft), _
                    New Segment("e", 0, 116, TH, 110 - T4, 0, SegmentCap.Standard, SegmentCap.MoreLeft), _
                    New Segment("f", 0, T4 + 2, TH, 110 - T4, 0, SegmentCap.MoreLeft, SegmentCap.Standard), _
                    New Segment("g1", T2 + 2, 114 + T2, TH, 59 - T2, -90, SegmentCap.Standard, SegmentCap.Flat), _
                    New Segment(135 - T4 * 5, 224 - T4 * 6, TH), _
                    New Segment("h", TH - nDiagX + 2, TH + 2 + nDiagY, MT, nDiagHeight, -nAngleDiag, SegmentCap.Right, SegmentCap.Left, 90 - nAngleDiag),
                    New Segment("i", 62 - MT / 2, TH + 2, MT, 110 - TH - T2, 0, SegmentCap.Flat, SegmentCap.Flat), _
                    New Segment("j", 122 - TH, TH + 2, MT, nDiagHeight, nAngleDiag, SegmentCap.Left, SegmentCap.Right, 90 - nAngleDiag),
                    New Segment("g2", 63, 114 + T2, TH, 59 - T2, -90, SegmentCap.Flat, SegmentCap.Standard), _
                    New Segment("m", MT / 2.0 - nDiagX + 64, 116 + T2 + nDiagY, MT, nDiagHeight, -nAngleDiag, SegmentCap.Right, SegmentCap.Left, 90 - nAngleDiag), _
                    New Segment("l", 62 - MT / 2, 116 + T2, MT, 110 - TH - T2, 0, SegmentCap.Flat, SegmentCap.Flat), _
                    New Segment("k", 60 - MT / 2, 116 + TH / 2, MT, nDiagHeight, nAngleDiag, SegmentCap.Left, SegmentCap.Right, 90 - nAngleDiag), _
                    New Segment(131 - T4 * 5, 230 - T4 * 4, TH) _
                })
            Case SegmentNumberType.TenSegment
                oSegments.AddRange(New Segment() { _
                    New Segment("a", T4 + 2, TH, TH, 120 - T2, -90, SegmentCap.MoreRight, SegmentCap.MoreRight), _
                    New Segment("b", 124 - TH, T4 + 2, TH, 110 - T4, 0, SegmentCap.MoreRight, SegmentCap.Standard), _
                    New Segment("c", 124 - TH, 116, TH, 110 - T4, 0, SegmentCap.Standard, SegmentCap.MoreRight), _
                    New Segment("d", T4 + 2, 228, TH, 120 - T2, -90, SegmentCap.MoreLeft, SegmentCap.MoreLeft), _
                    New Segment("e", 0, 116, TH, 110 - T4, 0, SegmentCap.Standard, SegmentCap.MoreLeft), _
                    New Segment("f", 0, T4 + 2, TH, 110 - T4, 0, SegmentCap.MoreLeft, SegmentCap.Standard), _
                    New Segment("g1", T2 + 2, 114 + T2, TH, 69 - T2, -90, SegmentCap.Standard, SegmentCap.Standard), _
                    New Segment("g2", 73, 114 + T2, TH, 49 - T2, -90, SegmentCap.Standard, SegmentCap.Standard), _
                    New Segment("i", 72 - T2, T2 + 2, TH, 110 - T2, 0, SegmentCap.Standard, SegmentCap.Standard), _
                    New Segment("l", 72 - T2, 116, TH, 110 - T2, 0, SegmentCap.Standard, SegmentCap.Standard)
                })
        End Select
        For Each oSegment As Segment In oSegments
            oSegment.Style = oSegmentStyle
        Next
    End Sub

#End Region

#Region "private methods"

    Private oNumberMatrix As Matrix
    Private Sub InitMatrix(ByVal nLocation As PointF, ByVal oMatrix As Matrix)
        oMatrix = oMatrix.Clone
        oMatrix.Translate(nLocation.X, nLocation.Y)
        oNumberMatrix = oMatrix
        oSegments.Transform(oMatrix)
    End Sub

    Private Function SetSegmentState(ByVal oSegment As Segment, ByVal bIsOn As Boolean) As Boolean
        If oSegment.IsOn = bIsOn Then Return False
        oSegment.IsOn = bIsOn
        Return True
    End Function

    Private Function GetSegmentRegions() As Region
        Dim oRegion As New Region(RectangleF.Empty)
        For Each oSegment As Segment In oSegments
            Dim oPath As GraphicsPath = oSegment.GlassPathTransformed
            oPath.Transform(oNumberMatrix)
            oRegion.Union(oPath)
        Next
        Return oRegion
    End Function

#End Region

#Region "character and bitflag display"

    Friend Sub DisplayCharacter(ByVal sCharacter As String)
        Dim sSegments As String = String.Empty
        If Not String.IsNullOrEmpty(sCharacter) Then
            Select Case Type
                Case SegmentNumberType.SevenSegment
                    Select Case sCharacter(0)
                        Case "0"c : sSegments = "abcdef"
                        Case "1"c : sSegments = "bc"
                        Case "2"c : sSegments = "abdeg"
                        Case "3"c : sSegments = "abcdg"
                        Case "4"c : sSegments = "bcfg"
                        Case "5"c : sSegments = "acdfg"
                        Case "6"c : sSegments = "acdefg"
                        Case "7"c : sSegments = "abc"
                        Case "8"c : sSegments = "abcdefg"
                        Case "9"c : sSegments = "abcdfg"
                        Case "A"c, "a"c : sSegments = "abcefg"
                        Case "B"c, "b"c : sSegments = "cdefg"
                        Case "C"c, "c"c : sSegments = "adef"
                        Case "D"c, "d"c : sSegments = "bcdeg"
                        Case "E"c, "e"c : sSegments = "adefg"
                        Case "F"c, "f"c : sSegments = "aefg"
                    End Select
                Case SegmentNumberType.TenSegment
                    Select Case sCharacter(0)
                        Case "0"c : sSegments = "abcdefjk"
                        Case "1"c : sSegments = "il"
                        Case "2"c : sSegments = "abdeg1g2"
                        Case "3"c : sSegments = "abcdg2"
                        Case "4"c : sSegments = "bcfg1g2"
                        Case "5"c : sSegments = "acdfg1g2"
                        Case "6"c : sSegments = "acdefg1g2"
                        Case "7"c : sSegments = "abc"
                        Case "8"c : sSegments = "abcdefg1g2"
                        Case "9"c : sSegments = "abcdfg1g2"
                        Case "A"c, "a"c : sSegments = "abcefg"
                        Case "B"c, "b"c : sSegments = "cdefg"
                        Case "C"c, "c"c : sSegments = "adef"
                        Case "D"c, "d"c : sSegments = "bcdeg"
                        Case "E"c, "e"c : sSegments = "adefg"
                        Case "F"c, "f"c : sSegments = "aefg"
                    End Select
                Case SegmentNumberType.FourteenSegment
                    Select Case sCharacter(0)
                        Case "0"c : sSegments = "abcdefjk"
                        Case "1"c : sSegments = "bcj"
                        Case "2"c : sSegments = "abdeg1g2"
                        Case "3"c : sSegments = "abcdg2"
                        Case "4"c : sSegments = "bcfg1g2"
                        Case "5"c : sSegments = "acdfg1g2"
                        Case "6"c : sSegments = "acdefg1g2"
                        Case "7"c : sSegments = "abc"
                        Case "8"c : sSegments = "abcdefg1g2"
                        Case "9"c : sSegments = "abcdfg1g2"
                        Case "A"c, "a"c : sSegments = "abcefg1g2"
                        Case "B"c, "b"c : sSegments = "abcdg2il"
                        Case "C"c, "c"c : sSegments = "adef"
                        Case "D"c, "d"c : sSegments = "abcdil"
                        Case "E"c, "e"c : sSegments = "adefg1"
                        Case "F"c, "f"c : sSegments = "aefg1"
                        Case "G"c, "g"c : sSegments = "acdefg2"
                        Case "H"c, "h"c : sSegments = "bcefg1g2"
                        Case "I"c, "i"c : sSegments = "adil"
                        Case "J"c, "j"c : sSegments = "bcde"
                        Case "K"c, "k"c : sSegments = "efg1jm"
                        Case "L"c, "l"c : sSegments = "def"
                        Case "M"c, "m"c : sSegments = "bcefhj"
                        Case "N"c, "n"c : sSegments = "bcefhm"
                        Case "O"c, "o"c : sSegments = "abcdef"
                        Case "P"c, "p"c : sSegments = "abefg1g2"
                        Case "Q"c, "q"c : sSegments = "abcdefm"
                        Case "R"c, "r"c : sSegments = "abefg1g2m"
                        Case "S"c, "s"c : sSegments = "acdfg1g2"
                        Case "T"c, "t"c : sSegments = "ail"
                        Case "U"c, "u"c : sSegments = "bcdef"
                        Case "V"c, "v"c : sSegments = "efjk"
                        Case "W"c, "w"c : sSegments = "bcefkm"
                        Case "X"c, "x"c : sSegments = "hjkm"
                        Case "Y"c, "y"c : sSegments = "hjl" '"bfg1g2l"
                        Case "Z"c, "z"c : sSegments = "adjk"
                    End Select
            End Select
            If sCharacter.EndsWith(".") Then sSegments &= "."
        End If
        Dim bAnyChange As Boolean = False
        For Each oSegment As Segment In oSegments
            bAnyChange = bAnyChange Or SetSegmentState(oSegment, sSegments.Contains(oSegment.Name))
        Next
        If bAnyChange Then OnInvalidated()
    End Sub

    Friend Sub DisplayBitCode(ByVal nValue As Long)
        Dim nSegment As Long
        Dim bAnyChange As Boolean = False
        For Each oSegment As Segment In oSegments
            If Type = SegmentNumberType.TenSegment AndAlso oSegment.Name = "g2" Then
                bAnyChange = bAnyChange Or SetSegmentState(oSegment, (nValue And (2 ^ (nSegment - 1))) > 0)
            Else
                bAnyChange = bAnyChange Or SetSegmentState(oSegment, (nValue And (2 ^ nSegment)) > 0)
            End If
            nSegment += 1
        Next
        If bAnyChange Then OnInvalidated()
    End Sub

#End Region

End Class

#Region "helper classes"

Public Enum SegmentNumberType
    SevenSegment = 7
    TenSegment = 10
    FourteenSegment = 14
End Enum

Friend Class SegmentStyle
    Public Property OffColor As Color = Color.FromArgb(255, 20, 20, 20)
    Public Property LightColor As Color = Color.FromArgb(254, 90, 50)
    Public Property GlassColor As Color = Color.FromArgb(254, 50, 25)
    Public Property GlassColorCenter As Color = Color.FromArgb(254, 50, 25)
    Public Property GlassAlpha As Integer = 140
    Public Property GlassAlphaCenter As Integer = 255
    Public Property Glow As Single = 10
    Public Property BulbSize As SizeF = SizeF.Empty
    Public Property WireFrame As Boolean = False
End Class

#End Region
