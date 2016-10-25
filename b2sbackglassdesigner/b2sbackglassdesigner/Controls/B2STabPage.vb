Imports System

Public Class B2STabPage

    Inherits Panel

    Private PictureBox As B2SPictureBox = Nothing

    Public Class B2STabPageScrollEventArgs
        Inherits EventArgs

        Public Location As Point

        Public Sub New(ByVal _location As Point)
            Location = _location
        End Sub
    End Class
    Public Event Scrolled(ByVal sender As Object, ByVal e As B2STabPageScrollEventArgs)

    Public BackglassData As BackglassData = Nothing

    Public TabLocation As Point = Nothing
    Public TabSize As Size = Nothing

    Private Sub B2STabPage_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        Dim x As Integer = Math.Max(0, CInt((Me.Width - PictureBox.Width) / 2))
        Dim y As Integer = Math.Max(0, CInt((Me.Height - PictureBox.Height) / 2))
        PictureBox.Location = New Point(x, y)
    End Sub

    Private Sub PictureBox_LocationChanged(sender As Object, e As EventArgs)
        RaiseEvent Scrolled(Me, New B2STabPageScrollEventArgs(PictureBox.Location))
    End Sub

    Public Property Image(Optional ByVal _filename As String = "") As Image
        Get
            Return PictureBox.Image
        End Get
        Set(ByVal value As Image)
            ' set backglass data
            If BackglassData IsNot Nothing Then
                BackglassData.Image = value
                BackglassData.Zoom = 100
                If Not String.IsNullOrEmpty(_filename) Then
                    BackglassData.ImageFileName = _filename
                End If
            End If
            ' create thumbnail
            Dim smallimage As Image = New Bitmap(16, 16)
            Using gr As Graphics = Graphics.FromImage(smallimage)
                gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                gr.DrawImage(value, New Rectangle(0, 0, smallimage.Width, smallimage.Height))
            End Using
            ThumbnailImage = smallimage
            ' set picture box
            PictureBox.Image = value
            PictureBox.Focus()
            ' refresh
            Me.Invalidate()
        End Set
    End Property
    Public Property ThumbnailImage() As Image = Nothing

    Public ReadOnly Property Mouse() As B2SIllumination.Mouse
        Get
            Return If(PictureBox IsNot Nothing, PictureBox.Mouse, Nothing)
        End Get
    End Property

    Public Property ShowBulbMarkers() As Boolean
        Get
            Return PictureBox.ShowBulbMarkers
        End Get
        Set(ByVal value As Boolean)
            PictureBox.ShowBulbMarkers = value
        End Set
    End Property
    Public Property ShowIlluminaten() As Boolean
        Get
            Return PictureBox.ShowIlluminaten
        End Get
        Set(ByVal value As Boolean)
            PictureBox.ShowIlluminaten = value
        End Set
    End Property

    Public Sub New(ByVal _backglassdata As BackglassData)
        ' backglass
        BackglassData = _backglassdata
        ' standard new
        MyNew(_backglassdata.Text)
    End Sub
    Public Sub New(ByVal _text As String, ByVal _tabletype As TableType)
        ' backglass
        BackglassData = New BackglassData()
        With BackglassData
            .Text = _text
            .TableType = _tabletype
        End With
        ' standard new
        MyNew(_text)
    End Sub
    Private Sub MyNew(text As String)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        ' some global settings
        Me.Name = "tabpage"
        Me.Text = text
        Me.AutoScroll = True
        Me.DoubleBuffered = True
        ' create picture box
        PictureBox = New B2SPictureBox()
        AddHandler PictureBox.LocationChanged, AddressOf PictureBox_LocationChanged
        PictureBox.Visible = False
        Me.Controls.Add(PictureBox)
        PictureBox.BringToFront()
        ' maybe load picture
        If BackglassData IsNot Nothing AndAlso BackglassData.Image IsNot Nothing Then
            Image = BackglassData.Image
        End If
        ' give the last ressources to the picture box
        PictureBox.BackglassData = BackglassData
    End Sub

    Public Shadows Sub Invalidate()
        PictureBox.Invalidate()
        MyBase.Invalidate()
    End Sub

    Public Sub Zoom(ByVal zoom As String)
        If BackglassData IsNot Nothing AndAlso BackglassData.Image IsNot Nothing Then
            If zoom.Equals("Window", StringComparison.CurrentCultureIgnoreCase) Then
                If Not PictureBox.Image.Equals(BackglassData.Image) Then
                    PictureBox.Image.Dispose()
                End If
                Dim factor As Double = BackglassData.Image.Width / Me.Width
                Dim factor2 As Double = BackglassData.Image.Height / Me.Height
                If factor < factor2 Then factor = factor2
                Dim width As Integer = (BackglassData.Image.Width - 4) / factor
                Dim height As Integer = (BackglassData.Image.Height - 4) / factor
                Dim newimage As Image = New Bitmap(width, height)
                Using gr As Graphics = Graphics.FromImage(newimage)
                    gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                    gr.DrawImage(BackglassData.Image, New Rectangle(0, 0, newimage.Width, newimage.Height))
                End Using
                PictureBox.Image = newimage
                BackglassData.Zoom = CInt(PictureBox.Size.Width / BackglassData.Image.Width * 100)
            End If
        End If
    End Sub
    Public Sub Zoom(ByVal zoom As Integer)
        If BackglassData IsNot Nothing AndAlso BackglassData.Image IsNot Nothing Then
            If zoom <> 100 Then
                If Not PictureBox.Image.Equals(BackglassData.Image) Then
                    PictureBox.Image.Dispose()
                End If
                Dim width As Integer = CInt(BackglassData.Image.Width * zoom / 100)
                Dim height As Integer = CInt(BackglassData.Image.Height * zoom / 100)
                Dim newimage As Image = New Bitmap(width, height)
                Using gr As Graphics = Graphics.FromImage(newimage)
                    gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                    gr.DrawImage(BackglassData.Image, New Rectangle(0, 0, newimage.Width, newimage.Height))
                End Using
                PictureBox.Image = newimage
            Else
                PictureBox.Size = BackglassData.Image.Size
                PictureBox.Image = BackglassData.Image
            End If
            BackglassData.Zoom = zoom
        End If
    End Sub

End Class