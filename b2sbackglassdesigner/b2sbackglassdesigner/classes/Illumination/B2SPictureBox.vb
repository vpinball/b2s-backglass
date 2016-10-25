Imports System

Public Class B2SPictureBox

    Inherits PictureBox

    Private helper As HelperBase = New HelperBase()

    Public WithEvents Mouse As Illumination.Mouse = Nothing
    Public WithEvents Lights As Illumination.Lights = Nothing

    Protected Overrides Sub OnPaint(pe As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(pe)
        ' draw the overlay illumination picture
        pe.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        If ShowIllumination AndAlso Lights IsNot Nothing AndAlso Lights.Image IsNot Nothing Then
            pe.Graphics.DrawImage(Lights.Image, New Rectangle(0, 0, Me.Image.Width, Me.Image.Height))
        End If
        ' show illumination markers
        If ShowIlluMarkers AndAlso Mouse IsNot Nothing AndAlso BackglassData IsNot Nothing AndAlso BackglassData.Bulbs.Count > 0 Then
            Dim pen As Pen = New Pen(Color.White)
            Dim pendashed As Pen = New Pen(Brushes.Black)
            pendashed.DashPattern = New Single() {2.0F, 20.0F}
            Dim factor As Double = Mouse.factor
            For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In BackglassData.Bulbs
                With bulb.Value
                    Dim rect As Rectangle = New Rectangle(CInt(.Location.X * factor), CInt(.Location.Y * factor), CInt(.Size.Width * factor), CInt(.Size.Height * factor))
                    pe.Graphics.DrawRectangle(pen, rect)
                    Dim x As Integer = CInt(rect.X + rect.Width / 2)
                    Dim y As Integer = CInt(rect.Y + rect.Height / 2)
                    If .Equals(Mouse.SelectedBulb) Then
                        pe.Graphics.DrawRectangle(pen, rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2)
                        pe.Graphics.DrawRectangle(pen, rect.X - 2, rect.Y - 2, rect.Width + 4, rect.Height + 4)
                        pe.Graphics.DrawLine(pen, x - 3, y, x + 3, y)
                        pe.Graphics.DrawLine(pen, x, y - 3, x, y + 3)
                    Else
                        pe.Graphics.DrawRectangle(pendashed, rect)
                    End If
                End With
            Next
            pendashed.Dispose()
            pen.Dispose()
        End If
        ' show score markers
        If (ShowScoreMarkers OrElse ShowScoring) AndAlso Mouse IsNot Nothing AndAlso BackglassData IsNot Nothing AndAlso BackglassData.Scores.Count > 0 Then
            Dim font As Font = New Font("Segoe UI", 9, FontStyle.Bold)
            Dim pen As Pen = New Pen(Color.OrangeRed)
            Dim pendashed As Pen = New Pen(Brushes.White)
            pendashed.DashPattern = New Single() {5.0F, 5.0F}
            Dim brush As SolidBrush = New SolidBrush(Color.OrangeRed)
            Dim factor As Double = Mouse.factor
            For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In BackglassData.Scores
                With score.Value
                    Dim rect As Rectangle = New Rectangle(CInt(.Location.X * factor), CInt(.Location.Y * factor), CInt(.Size.Width * factor), CInt(.Size.Height * factor))
                    ' draw marker frame
                    If ShowScoreMarkers Then
                        pe.Graphics.DrawRectangle(pen, rect)
                        If .Equals(Mouse.SelectedScore) Then
                            pe.Graphics.DrawRectangle(pen, rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2)
                            pe.Graphics.DrawRectangle(pen, rect.X - 2, rect.Y - 2, rect.Width + 4, rect.Height + 4)
                        Else
                            pe.Graphics.DrawRectangle(pendashed, rect)
                        End If
                    End If
                    ' draw images for score reels
                    If .IsSingleReelSizeDirty OrElse .SingleReelFactor <> factor Then
                        Dim width As Integer = CInt(rect.Width / 6)
                        Do While width * 6 > rect.Width - 1 : width -= 1 : Loop
                        Dim height As Integer = rect.Height - 1
                        If .PerfectScaleWidthFix Then
                            height = CInt(My.Resources.LED_0.Height / My.Resources.LED_0.Width * width)
                            .Size.Height = CInt(height / factor) + 1
                            .PerfectScaleWidthFix = False
                            Me.Invalidate()
                        End If
                        .SingleReelSize = New Size(width, height)
                        .IsSingleReelSizeDirty = False
                        .SingleReelFactor = factor
                    End If
                    Dim x As Integer = rect.X + 1
                    Dim y As Integer = rect.Y + 1
                    Dim image As Image = helper.ResizeImage(My.Resources.LED_0, New Size(.SingleReelSize.Width, .SingleReelSize.Height))
                    For i As Integer = 1 To .NumberOfReels
                        pe.Graphics.DrawImage(image, New Point(x, y))
                        x += image.Width
                    Next
                    image.Dispose()
                    ' draw player number
                    If ShowScoreMarkers Then
                        pe.Graphics.DrawString(.ID.ToString(), font, brush, New Point(rect.X + 3, rect.Y + 3))
                    End If
                End With
            Next
            brush.Dispose()
            pendashed.Dispose()
            pen.Dispose()
            font.Dispose()
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
            Mouse = New Illumination.Mouse(Me, value.Bulbs, value.Scores)
            Lights = New Illumination.Lights(Me, value.Bulbs)
        End Set
    End Property

    Private _SetGrillHeight As Boolean = False
    Public Property SetGrillHeight() As Boolean
        Get
            Return _SetGrillHeight
        End Get
        Set(ByVal value As Boolean)
            If _SetGrillHeight <> value Then
                _SetGrillHeight = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowScoreMarkers As Boolean = False
    Public Property ShowScoreMarkers() As Boolean
        Get
            Return _ShowScoreMarkers
        End Get
        Set(ByVal value As Boolean)
            If _ShowScoreMarkers <> value Then
                _ShowScoreMarkers = value
                Mouse.ShowScoreMarkers = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowScoring As Boolean = False
    Public Property ShowScoring() As Boolean
        Get
            Return _ShowScoring
        End Get
        Set(ByVal value As Boolean)
            If _ShowScoring <> value Then
                _ShowScoring = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowIlluMarkers As Boolean = False
    Public Property ShowIlluMarkers() As Boolean
        Get
            Return _ShowIlluMarkers
        End Get
        Set(ByVal value As Boolean)
            If _ShowIlluMarkers <> value Then
                _ShowIlluMarkers = value
                Mouse.ShowIlluMarkers = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowIllumination As Boolean = False
    Public Property ShowIllumination() As Boolean
        Get
            Return _ShowIllumination
        End Get
        Set(ByVal value As Boolean)
            If _ShowIllumination <> value Then
                _ShowIllumination = value
                If _ShowIllumination Then
                    Lights.DrawImage(BackglassData.Image)
                Else
                    Lights.ClearImage()
                End If
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
                Lights.factor = Mouse.factor
            End If
            ' maybe show picture box
            If Not Me.Visible Then
                Me.Visible = True
            End If
        End Set
    End Property

End Class
