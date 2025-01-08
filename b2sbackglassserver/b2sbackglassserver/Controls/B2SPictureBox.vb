Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class B2SPictureBox

    Inherits B2SBaseBox

    Public Enum ePictureBoxType
        StandardImage = 0
        SelfRotatingImage = 1
        MechRotatingImage = 2
    End Enum
    Public Enum eSnippitRotationDirection
        Clockwise = 0
        AntiClockwise = 1
    End Enum
    Public Enum eSnippitRotationStopBehaviour
        SpinOff = 0
        StopImmediatelly = 1
        RunAnimationTillEnd = 2
        RunAnimationToFirstStep = 3
    End Enum


    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        ' rectangle area for painting
        Dim rect As Rectangle = New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)

        ' draw dashed frame
        Dim pen As Pen = New Pen(Brushes.LightGray)
        pen.DashPattern = New Single() {3.0F, 3.0F}
        e.Graphics.DrawRectangle(pen, rect)
        pen.Dispose()

        ' draw text
        'If Not String.IsNullOrEmpty(Me.Text) Then
        '	TextRenderer.DrawText(e.Graphics, Me.Text, Me.Font, rect, Color.White, TextFormatFlags.WordBreak Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        'End If
    End Sub

    Public Sub New()
        ' set some drawing styles
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        'Me.SetStyle(ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        'Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)

        ' backcolor needs to be transparent
        Me.BackColor = Color.Transparent

        ' do not show the control
        MyBase.Visible = False
    End Sub

    Public Property PictureBoxType() As ePictureBoxType = ePictureBoxType.StandardImage

    Public Property GroupName() As String = String.Empty

    Public Property Intensity() As Integer = 1
    Public Property InitialState() As Integer = 0
    Public Property DualMode() As B2SData.eDualMode = B2SData.eDualMode.Both
    Public Property ZOrder() As Integer = 0

    Public Property IsImageSnippit() As Boolean = False
    Public Property SnippitRotationStopBehaviour() As eSnippitRotationStopBehaviour = eSnippitRotationStopBehaviour.SpinOff

    Private _Visible As Boolean
    Public Shadows Property Visible(Optional ByVal _SetThruAnimation As Boolean = False) As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            If _Visible <> value Then
                SetThruAnimation = _SetThruAnimation
                _Visible = value
                ' do a invalidate at the parent form
                If Me.Parent IsNot Nothing Then Me.Parent.Invalidate(Rectangle.Round(Me.RectangleF))
            End If
        End Set
    End Property
    Public Property SetThruAnimation() As Boolean = False

    Private _BackgroundImage As Image
    Public Shadows Property BackgroundImage() As Image
        Get
            Return _BackgroundImage
        End Get
        Set(ByVal value As Image)
            If _BackgroundImage Is Nothing OrElse Not _BackgroundImage.Equals(value) Then
                _BackgroundImage = value
                ' do a invalidate at the parent form
                If Me.Parent IsNot Nothing Then Me.Parent.Invalidate(Rectangle.Round(Me.RectangleF))
            End If
        End Set
    End Property

    Private _OffImage As Image
    Public Property OffImage() As Image
        Get
            Return _OffImage
        End Get
        Set(ByVal value As Image)
            _OffImage = value
        End Set
    End Property

End Class
