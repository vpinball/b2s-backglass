Imports System

Public Class B2SDream7LEDPanel

    Inherits Panel

    Public Shadows Event Click(ByVal sender As Object, ByVal e As EventArgs)

    Private WithEvents panel As Panel = Nothing
    Private WithEvents label As Label = Nothing
    Private WithEvents dream7LED As Dream7Display = Nothing

    Private Sub PanelLED_Click(sender As Object, e As System.EventArgs) Handles panel.Click, label.Click, dream7LED.Click
        Selected = Not Selected
        RaiseEvent Click(Me, New EventArgs())
    End Sub

    Public Shadows Property BackColor() As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As Color)
            label.BackColor = value
            MyBase.BackColor = value
        End Set
    End Property

    Private _LEDColor As Color = Nothing
    Public Property LEDColor() As Color
        Get
            Return _LEDColor
        End Get
        Set(ByVal value As Color)
            dream7LED.GlassColor = value
            dream7LED.LightColor = Color.FromArgb(Math.Min(value.R + 35, 255), Math.Min(value.G + 35, 255), Math.Min(value.B + 25, 255))
            dream7LED.GlassColorCenter = value 'Color.FromArgb(Math.Min(value.R + 50, 255), Math.Min(value.G + 50, 255), Math.Min(value.B + 50, 255))
            'dream7LED.OffColor = reeldarkcolor
            dream7LED.GlassAlpha = 70
            dream7LED.GlassAlphaCenter = 155
            dream7LED.OffsetWidth = -2
        End Set
    End Property

    Public ReadOnly Property LED() As Dream7Display
        Get
            Return dream7LED
        End Get
    End Property

    Private _LEDType As SegmentNumberType = SegmentNumberType.SevenSegment
    Public Property LEDType() As SegmentNumberType
        Get
            Return _LEDType
        End Get
        Set(ByVal value As SegmentNumberType)
            If _LEDType <> value Then
                _LEDType = value
                dream7LED.Type = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _Selected As Boolean
    Public Property Selected() As Boolean
        Get
            Return _Selected
        End Get
        Set(ByVal value As Boolean)
            If _Selected <> value Then
                _Selected = value
                If value Then Me.BackColor = Color.FromKnownColor(KnownColor.Highlight) Else Me.BackColor = Me.Parent.BackColor
            End If
        End Set
    End Property

    Public Sub New()

        ' set some flags
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.DoubleBuffered = True

        Me.Padding = New Padding(5, 5, 5, 0)

        ' create controls
        label = New Label
        label.Height = 13
        label.TextAlign = ContentAlignment.TopCenter
        label.Dock = DockStyle.Bottom
        Me.Controls.Add(label)
        label.BringToFront()

        panel = New Panel
        panel.BackColor = Color.Black
        panel.Padding = New Padding(3)
        panel.Dock = DockStyle.Fill
        Me.Controls.Add(panel)
        panel.BringToFront()

        dream7LED = New Dream7Display
        dream7LED.Digits = 1
        dream7LED.ScaleMode = ScaleMode.Stretch
        dream7LED.Dock = DockStyle.Fill
        panel.Controls.Add(dream7LED)
        dream7LED.BringToFront()

    End Sub

    Public Property LabelText() As String
        Get
            Return Label.Text
        End Get
        Set(ByVal value As String)
            label.Text = value
        End Set
    End Property

    Public Shadows Sub Invalidate()
        dream7LED.Invalidate()
        MyBase.Invalidate()
    End Sub

End Class
