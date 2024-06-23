Imports System
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms

Public Class B2SLEDBox

    Inherits B2SBaseBox

    Private currentSeg As Generic.List(Of PointF()) = New Generic.List(Of PointF())

    Protected Overrides Sub OnResize(e As System.EventArgs)

        ' base call
        MyBase.OnResize(e)

        ' resize the led segments and load it into the current segment array
        Dim width As Single = Me.Width / B2SData.ledCoordMax
        Dim height As Single = Me.Height / B2SData.ledCoordMax
        currentSeg.Clear()
        For Each entry As PointF() In If(LEDType = B2SLED.eLEDType.LED10, B2SData.led10Seg, If(LEDType = B2SLED.eLEDType.LED14, B2SData.led14Seg, If(LEDType = B2SLED.eLEDType.LED16, B2SData.led16Seg, B2SData.led8Seg)))
            Dim scaled(entry.Length - 1) As PointF
            For i As Integer = 0 To entry.Length - 1
                scaled(i) = New PointF(entry(i).X * width, entry(i).Y * height)
            Next
            currentSeg.Add(scaled)
        Next

    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

        ' create or recreate brushes
        'Static glowingbrush As SolidBrush = New SolidBrush(Color.FromArgb(Math.Min(LitLEDSegmentColor.R + 20, 255), Math.Min(LitLEDSegmentColor.G + 20, 255), Math.Min(LitLEDSegmentColor.B + 20, 255)))
        Static litbrush As SolidBrush = New SolidBrush(LitLEDSegmentColor)
        Static litpen As Pen = New Pen(LitLEDSegmentColor)
        Static darkbrush As SolidBrush = New SolidBrush(DarkLEDSegmentColor)
        If Not litbrush.Color.Equals(LitLEDSegmentColor) Then
            'glowingbrush.Dispose()
            'glowingbrush = New SolidBrush(Color.FromArgb(Math.Min(LitLEDSegmentColor.R + 30, 255), Math.Max(LitLEDSegmentColor.G + 30, 255), Math.Max(LitLEDSegmentColor.B + 30, 255)))
            litbrush.Dispose()
            litbrush = New SolidBrush(LitLEDSegmentColor)
            litpen.Dispose()
            litpen = New Pen(LitLEDSegmentColor)
        End If
        If Not darkbrush.Color.Equals(DarkLEDSegmentColor) Then
            darkbrush.Dispose()
            darkbrush = New SolidBrush(DarkLEDSegmentColor)
        End If

        ' draw LED segments
        Dim width As Single = Me.Width / B2SData.ledCoordMax
        Dim height As Single = Me.Height / B2SData.ledCoordMax

        ' set graphics' options
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' draw LED segments
        For i As Integer = 0 To currentSeg.Count - 1
            If (_Value And 2 ^ i) <> 0 Then
                'e.Graphics.FillPolygon(glowingbrush, currentSeg(i))
                'e.Graphics.DrawPolygon(litpen, currentSeg(i))
                e.Graphics.FillPolygon(litbrush, currentSeg(i))
            Else
                e.Graphics.FillPolygon(darkbrush, currentSeg(i))
            End If
        Next
       
    End Sub

    Public Sub New()

        ' set some styles
        'Me.SetStyle(ControlStyles.ResizeRedraw, True) ' Or ControlStyles.SupportsTransparentBackColor
        'Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)

        ' back color
        Me.BackColor = Color.Black

        ' show control
        Me.Visible = True

    End Sub

    Public Property LitLEDSegmentColor() As Color = Nothing
    Public Property DarkLEDSegmentColor() As Color = Nothing

    Private _LEDType As B2SLED.eLEDType = B2SLED.eLEDType.Undefined
    Public Property LEDType() As B2SLED.eLEDType
        Get
            Return _LEDType
        End Get
        Set(ByVal value As B2SLED.eLEDType)
            If _LEDType <> value Then
                _LEDType = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _Value As Integer = 0
    Public Property Value(Optional ByVal refresh As Boolean = False) As Integer
        Get
            Return _Value
        End Get
        Set(ByVal newvalue As Integer)
            If _Value <> newvalue OrElse refresh Then
                _Value = newvalue
                Me.Refresh()
            End If
        End Set
    End Property

    Private _Text As String = String.Empty
    Public Shadows Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal newvalue As String)
            If Not String.IsNullOrEmpty(newvalue) Then
                If _Text <> newvalue Then
                    _Text = newvalue
                    Select Case LEDType
                        Case B2SLED.eLEDType.LED8, B2SLED.eLEDType.Undefined
                            Select Case newvalue
                                Case " " : Value = 0
                                Case "0" : Value = 63
                                Case "1" : Value = 6
                                Case "2" : Value = 91
                                Case "3" : Value = 79
                                Case "4" : Value = 102
                                Case "5" : Value = 109
                                Case "6" : Value = 125
                                Case "7" : Value = 7
                                Case "8" : Value = 127
                                Case "9" : Value = 111
                            End Select
                        Case B2SLED.eLEDType.LED10
                            Select Case newvalue
                                Case " " : Value = 0
                                Case "0" : Value = 63
                                Case "1" : Value = 768
                                Case "2" : Value = 91
                                Case "3" : Value = 79
                                Case "4" : Value = 102
                                Case "5" : Value = 109
                                Case "6" : Value = 124
                                Case "7" : Value = 7
                                Case "8" : Value = 127
                                Case "9" : Value = 103
                            End Select
                        Case B2SLED.eLEDType.LED14
                            ' TODO
                        Case B2SLED.eLEDType.LED16
                            ' not implemented right now
                    End Select
                End If
            End If
        End Set
    End Property

End Class
