Imports System

Public Class B2SPictureBox

    Inherits PictureBox

    Public WithEvents Mouse As B2SIllumination.Mouse = Nothing

    Protected Overrides Sub OnPaint(pe As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(pe)
        If ShowBulbMarkers AndAlso Mouse IsNot Nothing AndAlso BackglassData IsNot Nothing AndAlso BackglassData.Bulbs.Count > 0 Then
            Dim pen As Pen = New Pen(Color.White)
            Dim factor As Double = Mouse.factor
            For Each bulb As KeyValuePair(Of String, B2SIllumination.BulbInfo) In BackglassData.Bulbs
                With bulb.Value
                    pe.Graphics.DrawRectangle(pen, CInt(.Location.X * factor), CInt(.Location.Y * factor), CInt(.Size.Width * factor), CInt(.Size.Height * factor))
                    If bulb.Value.Equals(Mouse.SelectedBulb) Then
                        pe.Graphics.DrawRectangle(pen, CInt(.Location.X * factor) + 1, CInt(.Location.Y * factor) + 1, CInt(.Size.Width * factor) - 2, CInt(.Size.Height * factor) - 2)
                        pe.Graphics.DrawRectangle(pen, CInt(.Location.X * factor) + 2, CInt(.Location.Y * factor) + 2, CInt(.Size.Width * factor) - 4, CInt(.Size.Height * factor) - 4)
                        pe.Graphics.DrawRectangle(pen, CInt(.Location.X * factor) + 3, CInt(.Location.Y * factor) + 3, CInt(.Size.Width * factor) - 6, CInt(.Size.Height * factor) - 6)
                    End If
                End With
            Next
            pen.Dispose()
        End If
    End Sub
    Protected Overrides Sub OnPaintBackground(pevent As System.Windows.Forms.PaintEventArgs)
        ' nothing to do
    End Sub

    Private _BackglassData As BackglassData
    Public Property BackglassData() As BackglassData
        Get
            Return _BackglassData
        End Get
        Set(ByVal value As BackglassData)
            _BackglassData = value
            Mouse = New B2SIllumination.Mouse(Me, value.Bulbs)
        End Set
    End Property

    Private _ShowBulbMarkers As Boolean = False
    Public Property ShowBulbMarkers() As Boolean
        Get
            Return _ShowBulbMarkers
        End Get
        Set(ByVal value As Boolean)
            If _ShowBulbMarkers <> value Then
                _ShowBulbMarkers = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _Illuminate As Boolean = False
    Public Property ShowIlluminaten() As Boolean
        Get
            Return _Illuminate
        End Get
        Set(ByVal value As Boolean)
            If _Illuminate <> value Then
                _Illuminate = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Shadows Property Image() As Image
        Get
            Return MyBase.Image
        End Get
        Set(ByVal value As Image)
            MyBase.Image = value
            ' set size
            Me.Size = value.Size
            ' set location
            Dim x As Integer = Math.Max(0, CInt((Me.Parent.Width - Me.Width) / 2))
            Dim y As Integer = Math.Max(0, CInt((Me.Parent.Height - Me.Height) / 2))
            Me.Location = New Point(x, y)
            ' recalc the factor
            If BackglassData IsNot Nothing AndAlso Mouse IsNot Nothing Then
                Mouse.factor = value.Width / BackglassData.Image.Width
            End If
            ' maybe show picture box
            If Not Me.Visible Then
                Me.Visible = True
            End If
        End Set
    End Property

End Class
