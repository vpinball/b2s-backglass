Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class Dream7Display
    Inherits Control
    Private SegmentNumbers As New Generic.List(Of SegmentNumber)
    Private ExtraSpacings As New Generic.Dictionary(Of Integer, Single)

    Public Shared SegmentPaintCounter As Long

#Region "constructor"

    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.Opaque, False)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            If TransparentBackground Then cp.ExStyle = cp.ExStyle Or &H20 ' WS_EX_TRANSPARENT 
            Return cp
        End Get
    End Property

#End Region

#Region "public properties"

    Public Property Hidden() As Boolean = False

    Private bMirrored As Boolean
    <DefaultValue(False)> _
    Public Property Mirrored As Boolean
        Get
            Return bMirrored
        End Get
        Set(ByVal value As Boolean)
            If bMirrored <> value Then
                bMirrored = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

    Private sText As String = Nothing
    Public Overrides Property Text As String
        Get
            Return sText
        End Get
        Set(ByVal value As String)
            sText = value
            Dim nLen As Integer = 0
            If Not String.IsNullOrEmpty(value) Then nLen = value.Length - 1
            Dim nIndex As Integer = 0
            For nSegment As Integer = 0 To nLen
                If nIndex >= SegmentNumbers.Count Then Exit For
                Dim sChar As Char = " "
                If Not String.IsNullOrEmpty(sText) AndAlso sText.Length > nSegment Then
                    sChar = sText.Substring(nSegment, 1)
                End If
                If sChar = "." AndAlso nIndex > 0 AndAlso Not SegmentNumbers(nIndex - 1).Character.EndsWith(".") Then
                    SegmentNumbers(nIndex - 1).Character &= "."
                Else
                    SegmentNumbers(nIndex).Character = sChar
                    nIndex += 1
                End If
            Next
        End Set
    End Property

    Private nDigits As Integer
    <DefaultValue(0)> _
    Public Property Digits As Integer
        Get
            Return nDigits
        End Get
        Set(ByVal value As Integer)
            If value <> nDigits Then
                nDigits = value
                InitSegements()
            End If
        End Set
    End Property

    Private nScaleMode As ScaleMode = ScaleMode.Stretch
    Public Property ScaleMode As ScaleMode
        Get
            Return nScaleMode
        End Get
        Set(ByVal value As ScaleMode)
            If nScaleMode <> value Then
                nScaleMode = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

    Private nSpacing As Single = 20.0
    <DefaultValue(CSng(20.0))> _
    Public Property Spacing As Single
        Get
            Return nSpacing
        End Get
        Set(ByVal value As Single)
            If nSpacing <> value Then
                nSpacing = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

    Private bTransparentBackground As Boolean = False
    <DefaultValue(False)> _
    Public Property TransparentBackground As Boolean
        Get
            Return bTransparentBackground
        End Get
        Set(ByVal value As Boolean)
            bTransparentBackground = value
            If Not value Then
                Me.DoubleBuffered = True
                Me.SetStyle(ControlStyles.Opaque, False)
                Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            Else
                Me.DoubleBuffered = False
                Me.SetStyle(ControlStyles.Opaque, True)
                Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, False)
            End If
        End Set
    End Property

    Private nType As SegmentNumberType = SegmentNumberType.SevenSegment
    <DefaultValue(GetType(SegmentNumberType), "SevenSegment")> _
    Public Property Type As SegmentNumberType
        Get
            Return nType
        End Get
        Set(ByVal value As SegmentNumberType)
            If value <> nType Then
                nType = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

    <DefaultValue(GetType(System.Drawing.Color), "20, 20, 20")> _
    Public WriteOnly Property OffColor As Color
        Set(ByVal value As Color)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.OffColor = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Public WriteOnly Property LightColor As Color
        Set(ByVal value As Color)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.LightColor = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Public WriteOnly Property GlassColor As Color
        Set(ByVal value As Color)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.GlassColor = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Public WriteOnly Property GlassColorCenter As Color
        Set(ByVal value As Color)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.GlassColorCenter = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Public WriteOnly Property GlassAlpha As Integer
        Set(ByVal value As Integer)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.GlassAlpha = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Public WriteOnly Property GlassAlphaCenter As Integer
        Set(ByVal value As Integer)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.GlassAlphaCenter = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Public WriteOnly Property Glow As Single
        Set(ByVal value As Single)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.Glow = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Public WriteOnly Property BulbSize As SizeF
        Set(ByVal value As SizeF)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.BulbSize = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Public WriteOnly Property WireFrame As Boolean
        Set(ByVal value As Boolean)
            For Each oSegment As SegmentNumber In SegmentNumbers
                oSegment.Style.WireFrame = value
                oSegment.AssignStyle()
            Next
        End Set
    End Property

    Private nShear As Single = 0.1
    <DefaultValue(CSng(0.1))> _
    Public Property Shear As Single
        Get
            Return nShear
        End Get
        Set(ByVal value As Single)
            If nShear <> value Then
                nShear = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

    Private nThickness As Single = 16.0
    <DefaultValue(CSng(16.0))> _
    Public Property Thickness As Single
        Get
            Return nThickness
        End Get
        Set(ByVal value As Single)
            If nThickness <> value Then
                nThickness = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

    Private nScaleFactor As Single = 0.5
    <DefaultValue(CSng(0.5))> _
    Public Property ScaleFactor As Single
        Get
            Return nScaleFactor
        End Get
        Set(ByVal value As Single)
            If nScaleFactor <> value Then
                nScaleFactor = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

    Private nOffsetWidth As Integer = 0
    <DefaultValue(CInt(0))> _
    Public Property OffsetWidth As Integer
        Get
            Return nOffsetWidth
        End Get
        Set(ByVal value As Integer)
            If value <> nOffsetWidth Then
                nOffsetWidth = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

    Private nAngle As Single = 0.0
    <DefaultValue(CSng(0.0))> _
    Public Property Angle As Single
        Get
            Return nAngle
        End Get
        Set(ByVal value As Single)
            If nAngle <> value Then
                nAngle = value
                InitSegmentsStyle()
            End If
        End Set
    End Property

#End Region

#Region "public functions"

    Public Sub SetValue(ByVal nSegment As Integer, ByVal sValue As String)
        If SegmentNumbers.Count <= nSegment Then Return
        SegmentNumbers(nSegment).DisplayCharacter(sValue)
    End Sub

    Public Sub SetValue(ByVal nSegment As Integer, ByVal nValue As Long)
        If SegmentNumbers.Count <= nSegment Then Return
        SegmentNumbers(nSegment).DisplayBitCode(nValue)
    End Sub

    Public Sub SetExtraSpacing(ByVal nSegment As Integer, ByVal nValue As Long)
        If ExtraSpacings.ContainsKey(nSegment) Then ExtraSpacings.Remove(nSegment)
        If nValue > 0 Then
            ExtraSpacings.Add(nSegment, nValue)
        End If
        InitSegmentsStyle()
    End Sub

    <Obsolete()> _
    Public Sub Repaint()
    End Sub

#End Region

#Region "transformation functions"

    Private oMatrix As Matrix
    Private Sub InitMatrix(ByVal nShear As Single, ByVal nScaleFactor As Single, ByVal bMirrored As Boolean)
        If oMatrix IsNot Nothing Then oMatrix.Dispose()
        oMatrix = New Matrix
        If nShear < 0 Then nShear = 0
        If nShear > 2 Then nShear = 2
        If nScaleFactor <= 0.01 Then nScaleFactor = 0.01
        If nScaleFactor > 10 Then nScaleFactor = 10
        ' try the simple matrix
        Dim oStyleMatrix As New Matrix
        If bMirrored Then oStyleMatrix.Multiply(New Matrix(1, 0, 0, -1, 0, 0))
        oStyleMatrix.Shear(-nShear, 0)
        oStyleMatrix.Rotate(nAngle)
        oStyleMatrix.Scale(nScaleFactor, nScaleFactor)
        oStyleMatrix.Translate(10, 10)
        If Me.IsHandleCreated Then
            ' correct the location and scale the whole display with a new matrix
            Dim nBounds As RectangleF = GetBounds(oStyleMatrix)
            Dim oCorrectedMatrix As New Matrix
            If nScaleMode <> ScaleMode.Manual Then
                Dim nScaleX As Single = (Me.Width + 3 - nOffsetWidth) / nBounds.Width
                Dim nScaleY As Single = (Me.Height - 1) / nBounds.Height
                If nScaleMode = ScaleMode.Zoom Then
                    nScaleY = Math.Min(nScaleX, nScaleY)
                    nScaleX = nScaleY
                End If
                If nScaleX > 0 AndAlso nScaleY > 0 Then oMatrix.Scale(nScaleX, nScaleY)
            End If
            oMatrix.Translate(-nBounds.X, -nBounds.Y)
        End If
        oMatrix.Multiply(oStyleMatrix)
        oStyleMatrix.Dispose()
    End Sub

    Private nBounds As RectangleF
    Private Function GetBounds(ByVal oMatrix As Matrix) As RectangleF
        ' determine the bounds of the whole display
        Dim nPoints(3) As PointF
        Dim nExtraSpacings As Single = 0
        For Each nSpacing As Single In ExtraSpacings.Values
            nExtraSpacings += nSpacing
        Next
        nBounds = New RectangleF(-10.0, -10.0, CSng(SegmentNumbers.Count) * (154.0 + Spacing) + 15.0 - Spacing + nExtraSpacings, 264.0)
        nPoints(0) = New PointF(nBounds.Left, nBounds.Top)
        nPoints(1) = New PointF(nBounds.Right, nBounds.Top)
        nPoints(2) = New PointF(nBounds.Right, nBounds.Bottom)
        nPoints(3) = New PointF(nBounds.Left, nBounds.Bottom)
        oMatrix.TransformPoints(nPoints)
        Dim nMinX As Single = Single.MaxValue, nMinY As Single = Single.MaxValue
        Dim nMaxX As Single = Single.MinValue, nMaxY As Single = Single.MinValue
        For Each nPoint As PointF In nPoints
            If nPoint.X < nMinX Then nMinX = nPoint.X
            If nPoint.X > nMaxX Then nMaxX = nPoint.X
            If nPoint.Y < nMinY Then nMinY = nPoint.Y
            If nPoint.Y > nMaxY Then nMaxY = nPoint.Y
        Next
        Return New RectangleF(nMinX, nMinY, nMaxX - nMinX, nMaxY - nMinY)
    End Function

#End Region

#Region "init methods"

    Private Sub InitSegements()
        InitSegements(nDigits, nType, nShear)
    End Sub

    Private Sub InitSegements(ByVal nNumbers As Integer, ByVal nType As SegmentNumberType, ByVal nShear As Single)
        If nNumbers >= 0 AndAlso nNumbers <= 80 Then
            For nNumber = SegmentNumbers.Count To nNumbers - 1
                Dim oNumber As New SegmentNumber()
                SegmentNumbers.Add(oNumber)
                AddHandler oNumber.Invalidated, AddressOf SegmentNumberInvalidated
            Next
            For nNumber = nNumbers To SegmentNumbers.Count - 1
                SegmentNumbers.RemoveAt(SegmentNumbers.Count - 1)
            Next
        End If
        nDigits = SegmentNumbers.Count
        InitSegmentsStyle()
    End Sub

    Private Sub SegmentNumberInvalidated(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oNumber As SegmentNumber = sender
        Me.Invalidate(New Region(oNumber.Bounds))
    End Sub
    Private Sub InitSegmentsStyle()
        InitMatrix(nShear, nScaleFactor, Mirrored)
        Dim nNumber As Integer = 0
        Dim nDistance As Single = 154 + Spacing
        Dim nXPos As Single = 0
        For Each oNumber As SegmentNumber In SegmentNumbers
            oNumber.Init(New PointF(nXPos, 0), nType, oMatrix, nThickness)
            nXPos += nDistance
            If ExtraSpacings.ContainsKey(nNumber) Then
                nXPos += ExtraSpacings(nNumber)
            End If
            nNumber += 1
        Next
        Me.Invalidate()
    End Sub

    Private Sub SegmentDisplay_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleCreated
        InitSegmentsStyle()
    End Sub

    Private Sub SegmentDisplay_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        InitSegmentsStyle()
    End Sub

#End Region

#Region "painting"

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality
        e.Graphics.CompositingMode = CompositingMode.SourceCopy
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear
        ' draw background
        If Me.DoubleBuffered Then
            e.Graphics.Clear(Color.Black)
        Else
            e.Graphics.Transform = oMatrix
            e.Graphics.FillRectangle(Brushes.Black, nBounds)
            e.Graphics.ResetTransform()
        End If
        For Each oNumber As SegmentNumber In SegmentNumbers
            Dim oRegion As New Region(e.ClipRectangle)
            oRegion.Intersect(oNumber.Bounds)
            If Not oRegion.IsEmpty(e.Graphics) Then
                oNumber.Draw(e.Graphics)
            End If
            'e.Graphics.DrawPath(Pens.Blue, oNumber.Bounds)
        Next
        e.Graphics.ResetTransform()
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        ' no background
    End Sub

#End Region

End Class

Public Enum ScaleMode
    Manual = 0
    Stretch = 1
    Zoom = 2
End Enum