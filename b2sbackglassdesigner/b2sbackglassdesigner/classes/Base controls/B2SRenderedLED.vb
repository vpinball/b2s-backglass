Imports System
Imports System.Drawing.Drawing2D

Public Class B2SRenderedLED
    Inherits Control

    Public Enum eLEDType
        Undefined = 0
        LED8 = 1
        LED10 = 2
        LED14 = 3
        LED16 = 4
    End Enum

    Private segments As Integer = 0

    Private brushgray As SolidBrush = New SolidBrush(Color.FromArgb(15, 15, 15))
    Private colorred As Color() = New Color() {Color.FromArgb(254, 50, 25),
                                               Color.FromArgb(247, 40, 30),
                                               Color.FromArgb(236, 32, 33)}

    Private Const toleft As Integer = 8

    Private currentseg As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    'Private currentglowingseg As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    Private led8seg As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    Private led10seg As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    Private led14seg As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    Private led16seg As Generic.List(Of PointF()) = New Generic.List(Of PointF())

    Protected Overrides Sub OnResize(e As System.EventArgs)
        MyBase.OnResize(e)
        PreparePainting(Me.Width, Me.Height)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

        ' back color
        Me.BackColor = Color.Black

        ' draw LED segments
        Dim width As Single = Me.Width / 103
        Dim height As Single = Me.Height / 103

        ' set graphics' options
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        ' generate a randomized brush
        Dim randomize1 As Integer = Random()
        Dim randomize2 As Integer = Random(randomize1)
        Dim brush As SolidBrush = Nothing
        'Dim glowingbrush As SolidBrush = Nothing
        If _ForeColor = Nothing Then
            brush = New SolidBrush(colorred(0))
        Else
            brush = New SolidBrush(_ForeColor)
            'glowingbrush = New SolidBrush(Color.FromArgb(Math.Min(_ForeColor.R + 60, 255), Math.Min(_ForeColor.G + 60, 255), Math.Min(_ForeColor.B + 60, 255)))
        End If

        ' draw LED segments
        For i As Integer = 0 To currentseg.Count - 1
            e.Graphics.FillPolygon(If((segments And 2 ^ i) <> 0, brush, brushgray), currentseg(i))
            'If glowingbrush IsNot Nothing Then
            '    If i = 0 Then
            '        e.Graphics.FillPolygon(glowingbrush, currentglowingseg(i))
            '    End If
            'End If
        Next
     
        ' free some resources
        brush.Dispose()

    End Sub

    Public Sub New()

        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.DoubleBuffered = True

        ' add led segments
        ' 8 segments
        led8seg.Add({New PointF(22, 5), New PointF(26, 2), New PointF(88, 2), New PointF(92, 5), New PointF(85, 11), New PointF(29, 11)})
        led8seg.Add({New PointF(93, 7), New PointF(96, 10), New PointF(96 - toleft, 46), New PointF(93 - toleft, 49), New PointF(87 - toleft, 43), New PointF(87, 12)})
        led8seg.Add({New PointF(92 - toleft, 51), New PointF(95 - toleft, 54), New PointF(96 - 2 * toleft, 90), New PointF(93 - 2 * toleft, 93), New PointF(87 - 2 * toleft, 88), New PointF(86 - toleft, 57)})
        led8seg.Add({New PointF(22 - 2 * toleft, 95), New PointF(29 - 2 * toleft, 89), New PointF(85 - 2 * toleft, 89), New PointF(92 - 2 * toleft, 95), New PointF(88 - 2 * toleft, 98), New PointF(26 - 2 * toleft, 98)})
        led8seg.Add({New PointF(20 - toleft, 51), New PointF(26 - toleft, 57), New PointF(27 - 2 * toleft, 88), New PointF(21 - 2 * toleft, 93), New PointF(17 - 2 * toleft, 90), New PointF(17 - toleft, 54)})
        led8seg.Add({New PointF(21, 7), New PointF(27, 12), New PointF(27 - toleft, 43), New PointF(21 - toleft, 49), New PointF(18 - toleft, 46), New PointF(18, 10)})
        led8seg.Add({New PointF(23 - toleft, 50), New PointF(27 - toleft, 46), New PointF(86 - toleft, 46), New PointF(90 - toleft, 50), New PointF(86 - toleft, 54), New PointF(27 - toleft, 54)})
        ' 10 segments
        led10seg.Add({New PointF(22, 5), New PointF(26, 2), New PointF(88, 2), New PointF(92, 5), New PointF(85, 11), New PointF(72, 11), New PointF(67, 6), New PointF(62, 11), New PointF(29, 11)})
        led10seg.Add({New PointF(93, 7), New PointF(96, 10), New PointF(96 - toleft, 46), New PointF(93 - toleft, 49), New PointF(87 - toleft, 43), New PointF(87, 12)})
        led10seg.Add({New PointF(92 - toleft, 51), New PointF(95 - toleft, 54), New PointF(96 - 2 * toleft, 90), New PointF(93 - 2 * toleft, 93), New PointF(87 - 2 * toleft, 88), New PointF(86 - toleft, 57)})
        led10seg.Add({New PointF(22 - 2 * toleft, 95), New PointF(29 - 2 * toleft, 89), New PointF(61 - 2 * toleft, 89), New PointF(66 - 2 * toleft, 94), New PointF(71 - 2 * toleft, 89), New PointF(85 - 2 * toleft, 89), New PointF(92 - 2 * toleft, 95), New PointF(88 - 2 * toleft, 98), New PointF(26 - 2 * toleft, 98)})
        led10seg.Add({New PointF(20 - toleft, 51), New PointF(26 - toleft, 57), New PointF(27 - 2 * toleft, 88), New PointF(21 - 2 * toleft, 93), New PointF(17 - 2 * toleft, 90), New PointF(17 - toleft, 54)})
        led10seg.Add({New PointF(21, 7), New PointF(27, 12), New PointF(27 - toleft, 43), New PointF(21 - toleft, 49), New PointF(18 - toleft, 46), New PointF(18, 10)})
        led10seg.Add({New PointF(23 - toleft, 50), New PointF(27 - toleft, 46), New PointF(63 - toleft, 46), New PointF(68 - toleft, 51), New PointF(73 - toleft, 46), New PointF(86 - toleft, 46), New PointF(90 - toleft, 50), New PointF(86 - toleft, 54), New PointF(72 - toleft, 54), New PointF(67 - toleft, 49), New PointF(62 - toleft, 54), New PointF(27 - toleft, 54)})
        led10seg.Add({New PointF(67, 9), New PointF(71, 13), New PointF(71 - toleft, 45), New PointF(67 - toleft, 49), New PointF(63 - toleft, 45), New PointF(63, 13)})
        led10seg.Add({New PointF(68, 7), New PointF(72, 11), New PointF(72 - toleft, 43), New PointF(68 - toleft, 47), New PointF(64 - toleft, 43), New PointF(64, 7)})
        led10seg.Add({New PointF(66 - toleft, 51), New PointF(70 - toleft, 55), New PointF(70 - 2 * toleft, 88), New PointF(66 - 2 * toleft, 92), New PointF(62 - 2 * toleft, 88), New PointF(62 - toleft, 51)})
        ' 14 segments
        led14seg.Add({New PointF(22, 5), New PointF(26, 2), New PointF(88, 2), New PointF(92, 5), New PointF(85, 11), New PointF(29, 11)})
        led14seg.Add({New PointF(93, 7), New PointF(96, 10), New PointF(96 - toleft, 46), New PointF(93 - toleft, 49), New PointF(87 - toleft, 43), New PointF(87, 12)})
        led14seg.Add({New PointF(92 - toleft, 51), New PointF(95 - toleft, 54), New PointF(96 - 2 * toleft, 90), New PointF(93 - 2 * toleft, 93), New PointF(87 - 2 * toleft, 88), New PointF(86 - toleft, 57)})
        led14seg.Add({New PointF(22 - 2 * toleft, 95), New PointF(29 - 2 * toleft, 89), New PointF(85 - 2 * toleft, 89), New PointF(92 - 2 * toleft, 95), New PointF(88 - 2 * toleft, 98), New PointF(26 - 2 * toleft, 98)})
        led14seg.Add({New PointF(20 - toleft, 51), New PointF(26 - toleft, 57), New PointF(27 - 2 * toleft, 88), New PointF(21 - 2 * toleft, 93), New PointF(17 - 2 * toleft, 90), New PointF(17 - toleft, 54)})
        led14seg.Add({New PointF(21, 7), New PointF(27, 12), New PointF(27 - toleft, 43), New PointF(21 - toleft, 49), New PointF(18 - toleft, 46), New PointF(18, 10)})
        led14seg.Add({New PointF(23 - toleft, 50), New PointF(27 - toleft, 46), New PointF(52 - toleft, 46), New PointF(55 - toleft, 50), New PointF(52 - toleft, 54), New PointF(27 - toleft, 54)})
        led14seg.Add({New PointF(104 - 2 * toleft, 87), New PointF(109 - 2 * toleft, 90), New PointF(109 - 2 * toleft, 95), New PointF(104 - 2 * toleft, 99), New PointF(100 - 2 * toleft, 95), New PointF(100 - 2 * toleft, 90)})
        led14seg.Add({New PointF(30, 13), New PointF(34, 17), New PointF(54 - toleft, 38), New PointF(51 - toleft, 43), New PointF(48 - toleft, 40), New PointF(27, 16)})
        led14seg.Add({New PointF(57, 13), New PointF(61, 13), New PointF(61 - toleft, 46), New PointF(57 - toleft, 48), New PointF(53 - toleft, 46), New PointF(53, 13)})
        led14seg.Add({New PointF(82, 13), New PointF(85, 16), New PointF(68 - toleft, 42), New PointF(65 - toleft, 44), New PointF(63 - toleft, 39), New PointF(77, 17)})
        led14seg.Add({New PointF(58 - toleft, 50), New PointF(62 - toleft, 46), New PointF(86 - toleft, 46), New PointF(90 - toleft, 50), New PointF(86 - toleft, 54), New PointF(62 - toleft, 54)})
        led14seg.Add({New PointF(82 - 2 * toleft, 85), New PointF(87 - 2 * toleft, 86), New PointF(67 - toleft, 57), New PointF(62 - toleft, 57), New PointF(62 - toleft, 60), New PointF(79 - 2 * toleft, 86)})
        led14seg.Add({New PointF(57 - toleft, 52), New PointF(61 - toleft, 54), New PointF(61 - 2 * toleft, 88), New PointF(57 - 2 * toleft, 88), New PointF(53 - 2 * toleft, 88), New PointF(53 - toleft, 54)})
        led14seg.Add({New PointF(30 - 2 * toleft, 83), New PointF(33 - 2 * toleft, 86), New PointF(50 - toleft, 60), New PointF(47 - toleft, 57), New PointF(42 - toleft, 61), New PointF(27 - 2 * toleft, 86)})
        led14seg.Add({New PointF(102 - 2 * toleft, 97), New PointF(107 - 2 * toleft, 100), New PointF(107 - 2 * toleft, 105), New PointF(102 - 2 * toleft, 109), New PointF(98 - 2 * toleft, 105), New PointF(98 - 2 * toleft, 100)})
        ' 16 segments
        led16seg.Add({New PointF(22, 5), New PointF(26, 2), New PointF(88, 2), New PointF(92, 5), New PointF(85, 11), New PointF(29, 11)})
        'led16seg.Add({New PointF(93, 7), New PointF(96, 10), New PointF(96 - toleft, 46), New PointF(93 - toleft, 49), New PointF(87 - toleft, 43), New PointF(87, 12)})
        'led16seg.Add({New PointF(92 - toleft, 51), New PointF(95 - toleft, 54), New PointF(96 - 2 * toleft, 90), New PointF(93 - 2 * toleft, 93), New PointF(87 - 2 * toleft, 88), New PointF(86 - toleft, 57)})
        'led16seg.Add({New PointF(22 - 2 * toleft, 95), New PointF(29 - 2 * toleft, 89), New PointF(85 - 2 * toleft, 89), New PointF(92 - 2 * toleft, 95), New PointF(88 - 2 * toleft, 98), New PointF(26 - 2 * toleft, 98)})
        'led16seg.Add({New PointF(20 - toleft, 51), New PointF(26 - toleft, 57), New PointF(27 - 2 * toleft, 88), New PointF(21 - 2 * toleft, 93), New PointF(17 - 2 * toleft, 90), New PointF(17 - toleft, 54)})
        'led16seg.Add({New PointF(21, 7), New PointF(27, 12), New PointF(27 - toleft, 43), New PointF(21 - toleft, 49), New PointF(18 - toleft, 46), New PointF(18, 10)})
        'led16seg.Add({New PointF(23 - toleft, 50), New PointF(27 - toleft, 46), New PointF(86 - toleft, 46), New PointF(90 - toleft, 50), New PointF(86 - toleft, 54), New PointF(27 - toleft, 54)})

    End Sub

    Private _ForeColor As Color = Nothing
    Public Shadows Property ForeColor() As Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As Color)
            _ForeColor = value
            Me.Invalidate()
        End Set
    End Property

    Private _LEDType As eLEDType = eLEDType.LED8
    Public Property LEDType() As eLEDType
        Get
            Return _LEDType
        End Get
        Set(ByVal value As eLEDType)
            If _LEDType <> value Then
                _LEDType = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Sub Clear()
        segments = 0
        Me.Invalidate()
    End Sub
    Public Sub Draw(ByVal key As Keys)
        Select Case key
            Case Keys.D0
                segments = 1 + 2 + 4 + 8 + 16 + 32
            Case Keys.D1
                If LEDType = eLEDType.LED10 Then
                    segments = 256 + 512
                Else
                    segments = 2 + 4
                End If
            Case Keys.D2
                segments = 1 + 2 + 8 + 16 + 64 + If(LEDType = eLEDType.LED14, 2048, 0)
            Case Keys.D3
                segments = 1 + 2 + 4 + 8 + 64 + If(LEDType = eLEDType.LED14, 2048 - 64, 0)
            Case Keys.D4
                segments = 2 + 4 + 32 + 64 + If(LEDType = eLEDType.LED14, 2048, 0)
            Case Keys.D5
                segments = 1 + 4 + 8 + 32 + 64 + If(LEDType = eLEDType.LED14, 2048, 0)
            Case Keys.D6
                segments = 1 + 4 + 8 + 16 + 32 + 64 + If(LEDType = eLEDType.LED14, 2048, 0)
            Case Keys.D7
                segments = 1 + 2 + 4
            Case Keys.D8
                segments = 1 + 2 + 4 + 8 + 16 + 32 + 64 + If(LEDType = eLEDType.LED14, 2048, 0)
            Case Keys.D9
                segments = 1 + 2 + 4 + 8 + 32 + 64 + If(LEDType = eLEDType.LED14, 2048, 0)
            Case Else
                segments = 0
        End Select
        Me.Invalidate()
    End Sub

    Private Sub PreparePainting(ByVal _width As Integer, ByVal _height As Integer)
        Dim width As Single = _width / 103
        Dim height As Single = _height / 103
        currentseg.Clear()
        'currentglowingseg.Clear()
        For Each entry As PointF() In If(LEDType = eLEDType.LED10, led10seg, If(LEDType = eLEDType.LED14, led14seg, If(LEDType = eLEDType.LED16, led16seg, led8seg)))
            Dim scaled(entry.Length - 1) As PointF
            For i As Integer = 0 To entry.Length - 1
                scaled(i) = New PointF(entry(i).X * width, entry(i).Y * height)
            Next
            currentseg.Add(scaled)
        Next

        'Dim xscaled(5) As PointF
        'Dim ii As Integer = 0
        'For Each entry As PointF In {New PointF(32, 5), New PointF(36, 3), New PointF(78, 3), New PointF(82, 5), New PointF(75, 10), New PointF(39, 9)}
        '    entry.X = entry.X * width
        '    entry.Y = entry.Y * height
        '    xscaled(ii) = entry
        '    ii += 1
        'Next
        'currentglowingseg.Add(xscaled)

    End Sub
    
    Private Function Random(Optional ByVal notequal As Integer = -1) As Integer
        Static r As Random = New Random()
        Dim ret As Integer = -1
        Do While ret < 0 OrElse ret > colorred.Length - 1 OrElse (ret.Equals(notequal) AndAlso colorred.Length > 1)
            ret = CInt((r.NextDouble * colorred.Length * 100).ToString().Substring(0, 1))
        Loop
        Return ret
    End Function

    Public ReadOnly Property Image(ByVal ledcolor As Color) As Image
        Get
            segments = 65535
            Dim ret As Image = New Bitmap(30, 40)
            PreparePainting(ret.Width, ret.Height)
            Using gr As Graphics = Graphics.FromImage(ret)
                gr.SmoothingMode = SmoothingMode.HighQuality
                gr.FillRectangle(Brushes.Black, 0, 0, ret.Width, ret.Height)
                ' draw LED segments
                Dim Brush As LinearGradientBrush = New LinearGradientBrush(New Rectangle(0, 0, ret.Width + ret.Height, 1), If(ledcolor = Nothing, colorred(0), ledcolor), If(ledcolor = Nothing, colorred(1), ledcolor), 45)
                For i As Integer = 0 To currentseg.Count - 1
                    gr.FillPolygon(If((segments And 2 ^ i) <> 0, Brush, brushgray), currentseg(i))
                Next
                'gr.FillEllipse(If(segments > 0, Brush, brushgray), New Rectangle(ret.Width - 3, ret.Height - 3, 3, 3))
            End Using
            Return ret
        End Get
    End Property

End Class
