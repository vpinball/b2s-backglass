Imports System

Public Class B2SRenderedLEDPanel

    Inherits Panel

    Public Shadows Event Click(ByVal sender As Object, ByVal e As EventArgs)

    Private WithEvents panel As Panel = Nothing
    Private WithEvents label As Label = Nothing
    Private WithEvents renderedLED As B2SRenderedLED = Nothing

    Private Sub PanelLED_Click(sender As Object, e As System.EventArgs) Handles panel.Click, label.Click, renderedLED.Click
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

    Public ReadOnly Property LED() As B2SRenderedLED
        Get
            Return renderedLED
        End Get
    End Property

    Private _LEDType As B2SRenderedLED.eLEDType = B2SRenderedLED.eLEDType.LED8
    Public Property LEDType() As B2SRenderedLED.eLEDType
        Get
            Return _LEDType
        End Get
        Set(ByVal value As B2SRenderedLED.eLEDType)
            If _LEDType <> value Then
                _LEDType = value
                renderedLED.LEDType = value
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
                'If value Then UnselectAll()
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

        renderedLED = New B2SRenderedLED
        renderedLED.Dock = DockStyle.Fill
        panel.Controls.Add(renderedLED)
        renderedLED.BringToFront()

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
        renderedLED.Invalidate()
        MyBase.Invalidate()
    End Sub

    'Private Sub UnselectAll()
    '    For Each cntrl As Control In Me.Parent.Controls.OfType(Of B2SRenderedLEDPanel)()
    '        'If TypeOf cntrl Is B2SRenderedLEDPanel Then
    '        DirectCast(cntrl, B2SRenderedLEDPanel).Selected = False
    '        'End If
    '    Next
    'End Sub

End Class
