Imports System

Public Class B2STabPage

    Inherits Panel

    Public Event MyMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MyMouseMove(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event CopyDMDCopyArea(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectedItemClicked(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event SelectedItemMoving(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event SelectedBulbMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event SelectedBulbEdited(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event LightsReportProgress(ByVal sender As Object, ByVal e As Illumination.Lights.LightsProgressEventArgs)
    Public Event LightColorChanged(ByVal sender As Object, ByVal e As Illumination.Lights.LightColorChangedEventArgs)

    Private WithEvents formSetLEDColor As formSetLEDColor = New formSetLEDColor()
    Private WithEvents formSetLightColor As formSetLEDColor = New formSetLEDColor()
    Private WithEvents formSetReelIllumination As formSetReelIllumination = New formSetReelIllumination()

    Private PictureBox As B2SPictureBox = Nothing
    Private DMDPictureBox As B2SPictureBox = Nothing
    Private ReadOnly Property CurrentPictureBox() As B2SPictureBox
        Get
            Return If(BackglassData IsNot Nothing, If(BackglassData.IsDMDImageShown, DMDPictureBox, PictureBox), Nothing)
        End Get
    End Property

    Public Class B2STabPageScrollEventArgs
        Inherits EventArgs

        Public Location As Point

        Public Sub New(ByVal _location As Point)
            Location = _location
        End Sub
    End Class
    Public Event Scrolled(ByVal sender As Object, ByVal e As B2STabPageScrollEventArgs)

    Public BackglassData As Backglass.Data = Nothing

    Public TabLocation As Point = Nothing
    Public TabSize As Size = Nothing

    Private Sub B2STabPage_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim location As Point = PictureBox.Location
        If Me.Width > PictureBox.Width Then
            location.X = Math.Max(0, CInt((Me.Width - PictureBox.Width) / 2))
        ElseIf Me.HorizontalScroll.Value = 0 Then
            location.X = 0
        End If
        If Me.Height > PictureBox.Height Then
            location.Y = Math.Max(0, CInt((Me.Height - PictureBox.Height) / 2))
        ElseIf Me.VerticalScroll.Value = 0 Then
            location.Y = 0
        End If
        PictureBox.Location = location
        ' locate and size the DMD picture box
        location = DMDPictureBox.Location
        If Me.Width > DMDPictureBox.Width Then
            location.X = Math.Max(0, CInt((Me.Width - DMDPictureBox.Width) / 2))
        ElseIf Me.HorizontalScroll.Value = 0 Then
            location.X = 0
        End If
        If Me.Height > DMDPictureBox.Height Then
            location.Y = Math.Max(0, CInt((Me.Height - DMDPictureBox.Height) / 2))
        ElseIf Me.VerticalScroll.Value = 0 Then
            location.Y = 0
        End If
        DMDPictureBox.Location = location
    End Sub

    Private Sub PictureBox_LocationChanged(sender As Object, e As EventArgs)
        RaiseEvent Scrolled(Me, New B2STabPageScrollEventArgs(PictureBox.Location))
    End Sub
    Private Sub DMDPictureBox_LocationChanged(sender As Object, e As EventArgs)
        RaiseEvent Scrolled(Me, New B2STabPageScrollEventArgs(DMDPictureBox.Location))
    End Sub
    Private Sub PictureBox_MyMouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent MyMouseDown(sender, e)
    End Sub
    Private Sub DMDPictureBox_MyMouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent MyMouseDown(sender, e)
    End Sub
    Private Sub PictureBox_MyMouseMove(sender As Object, e As Mouse.MouseMoveEventArgs)
        RaiseEvent MyMouseMove(sender, e)
    End Sub
    Private Sub DMDPictureBox_MyMouseMove(sender As Object, e As Mouse.MouseMoveEventArgs)
        RaiseEvent MyMouseMove(sender, e)
    End Sub
    Private Sub PictureBox_CopyDMDCopyArea(sender As Object, e As EventArgs)
        RaiseEvent CopyDMDCopyArea(sender, e)
    End Sub
    Private Sub PictureBox_SelectedItemClicked(sender As Object, e As Mouse.MouseMoveEventArgs)
        RaiseEvent SelectedItemClicked(sender, e)
    End Sub
    Private Sub DMDPictureBox_SelectedItemClicked(sender As Object, e As Mouse.MouseMoveEventArgs)
        RaiseEvent SelectedItemClicked(sender, e)
    End Sub
    Private Sub PictureBox_SelectedItemMoving(sender As Object, e As Mouse.MouseMoveEventArgs)
        RaiseEvent SelectedItemMoving(sender, e)
    End Sub
    Private Sub DMDPictureBox_SelectedItemMoving(sender As Object, e As Mouse.MouseMoveEventArgs)
        RaiseEvent SelectedItemMoving(sender, e)
    End Sub
    Private Sub PictureBox_SelectedBulbMoved(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent SelectedBulbMoved(sender, e)
    End Sub
    Private Sub DMDPictureBox_SelectedBulbMoved(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent SelectedBulbMoved(sender, e)
    End Sub
    Private Sub PictureBox_SelectedBulbEdited(sender As Object, e As System.EventArgs)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.IsIlluminatedImageDirty = True
            RefreshIllumination()
        End If
        RaiseEvent SelectedBulbEdited(sender, e)
    End Sub
    Private Sub DMDPictureBox_SelectedBulbEdited(sender As Object, e As System.EventArgs)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.IsIlluminatedImageDirty = True
            RefreshIllumination()
        End If
        RaiseEvent SelectedBulbEdited(sender, e)
    End Sub
    Private Sub PictureBox_SelectedItemRemoved(sender As Object, e As System.EventArgs)
        RefreshIllumination()
    End Sub
    Private Sub DMDPictureBox_SelectedItemRemoved(sender As Object, e As System.EventArgs)
        RefreshIllumination()
    End Sub
    Private Sub PictureBox_LightsReportProgress(sender As Object, e As Illumination.Lights.LightsProgressEventArgs)
        RaiseEvent LightsReportProgress(sender, e)
    End Sub
    Private Sub DMDPictureBox_LightsReportProgress(sender As Object, e As Illumination.Lights.LightsProgressEventArgs)
        RaiseEvent LightsReportProgress(sender, e)
    End Sub

    Public Sub ShowBackglassImage()
        PictureBox.Visible = True
        DMDPictureBox.Visible = False
        BackglassData.IsDMDImageShown = False
    End Sub
    Public Sub ShowDMDImage()
        PictureBox.Visible = False
        If DMDPictureBox.Image IsNot Nothing Then
            DMDPictureBox.Visible = True
        End If
        BackglassData.IsDMDImageShown = True
    End Sub

    Public Property Image(Optional ByVal _filename As String = "") As Image
        Get
            Return PictureBox.Image
        End Get
        Set(ByVal value As Image)
            ' set backglass data
            If BackglassData IsNot Nothing Then
                BackglassData.Image = value
                BackglassData.IsDMDImageShown = False
                BackglassData.Zoom = 100
                If Not String.IsNullOrEmpty(_filename) Then
                    BackglassData.ImageFileName = _filename
                End If
                ' create thumbnail
                If BackglassData.ThumbnailImage Is Nothing AndAlso value IsNot Nothing Then
                    BackglassData.ThumbnailImage = value.Resized(New Size(32, 32))
                End If
            End If
            '' create thumbnail
            'Dim smallimage As Image = New Bitmap(16, 16)
            'If value IsNot Nothing Then
            '    Using gr As Graphics = Graphics.FromImage(smallimage)
            '        gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            '        gr.DrawImage(value, New Rectangle(0, 0, smallimage.Width, smallimage.Height))
            '    End Using
            'End If
            'ThumbnailImage = smallimage
            ' set picture box
            PictureBox.Image = value
            DMDPictureBox.Visible = False
            'PictureBox.Focus()
            ' refresh
            Me.Invalidate()
        End Set
    End Property
    Public ReadOnly Property ThumbnailImage() As Image
        Get
            Static thumbnail As Image = Nothing
            If thumbnail Is Nothing AndAlso BackglassData IsNot Nothing AndAlso BackglassData.ThumbnailImage IsNot Nothing Then
                thumbnail = BackglassData.ThumbnailImage.Resized(New Size(16, 16))
            End If
            Return If(BackglassData IsNot Nothing, thumbnail, Nothing)
        End Get
    End Property

    Public Property DMDImage(Optional ByVal _filename As String = "") As Image
        Get
            Return DMDPictureBox.Image
        End Get
        Set(ByVal value As Image)
            If BackglassData IsNot Nothing Then
                BackglassData.DMDImage = value
                BackglassData.IsDMDImageShown = True
                If Not String.IsNullOrEmpty(_filename) Then
                    BackglassData.DMDImageFileName = _filename
                End If
                If value Is Nothing Then
                    BackglassData.DMDImageFileName = String.Empty
                End If
            End If
            ' set DMD picture box
            DMDPictureBox.Image = value
            PictureBox.Visible = False
            ' refresh
            Me.Invalidate()
        End Set
    End Property

    Public ReadOnly Property OffImage() As Image
        Get
            Return PictureBox.OffImage()
        End Get
    End Property
    Public ReadOnly Property OnImageRomID() As Integer
        Get
            Return PictureBox.OnImageRomID()
        End Get
    End Property
    Public ReadOnly Property OnImageRomIDType() As eRomIDType
        Get
            Return PictureBox.OnImageRomIDType()
        End Get
    End Property
    Public ReadOnly Property DarkImage() As Image
        Get
            Return PictureBox.DarkImage()
        End Get
    End Property
    Public ReadOnly Property IlluminatedImage() As Image
        Get
            Return PictureBox.IlluminatedImage()
        End Get
    End Property
    Public ReadOnly Property IlluminatedImageOnlyWithAlwaysOnLights() As Image
        Get
            Return PictureBox.IlluminatedImageOnlyWithAlwaysOnLights()
        End Get
    End Property
    Public ReadOnly Property IlluminatedImageOnlyWithOnLights() As Image
        Get
            Return PictureBox.IlluminatedImageOnlyWithOnLights()
        End Get
    End Property
    Public ReadOnly Property FirstStoredIlluminationImage() As Image
        Get
            Return PictureBox.FirstStoredIlluminationImage()
        End Get
    End Property
    Public ReadOnly Property IlluminatedImages(ByVal currentimage As Image,
                                               Optional ByVal currentoffimage As Image = Nothing) As Generic.SortedList(Of Integer, Illumination.Lights.ImageInfo)
        Get
            Return PictureBox.IlluminatedImages(currentimage, currentoffimage)
        End Get
    End Property

    Public ReadOnly Property IlluminatedDMDImage() As Image
        Get
            Return DMDPictureBox.IlluminatedImage()
        End Get
    End Property
    Public ReadOnly Property IlluminatedDMDImageOnlyWithAlwaysOnLights() As Image
        Get
            Return DMDPictureBox.IlluminatedImageOnlyWithAlwaysOnLights()
        End Get
    End Property
    Public ReadOnly Property IlluminatedDMDImages(ByVal currentdmdimage As Image) As Generic.SortedList(Of Integer, Illumination.Lights.ImageInfo)
        Get
            Return DMDPictureBox.IlluminatedImages(currentdmdimage)
        End Get
    End Property

    Public ReadOnly Property Mouse() As Mouse
        Get
            Return If(CurrentPictureBox IsNot Nothing, CurrentPictureBox.Mouse, Nothing)
        End Get
    End Property
    Public ReadOnly Property SelectedItem() As InfoBase
        Get
            Return If(Mouse IsNot Nothing, Mouse.SelectedItem, Nothing)
        End Get
    End Property
    Public ReadOnly Property SelectedScore() As ReelAndLED.ScoreInfo
        Get
            Return If(Mouse IsNot Nothing, Mouse.SelectedScore, Nothing)
        End Get
    End Property
    Public ReadOnly Property SelectedBulb() As Illumination.BulbInfo
        Get
            Return If(Mouse IsNot Nothing, Mouse.SelectedBulb, Nothing)
        End Get
    End Property

    Public Property SetGrillHeight() As Boolean
        Get
            Return PictureBox.SetGrillHeight
        End Get
        Set(ByVal value As Boolean)
            PictureBox.SetGrillHeight = value
        End Set
    End Property
    Public Property SetSmallGrillHeight() As Boolean
        Get
            Return PictureBox.SetSmallGrillHeight
        End Get
        Set(ByVal value As Boolean)
            PictureBox.SetSmallGrillHeight = value
        End Set
    End Property

    Public Property CopyDMDImageFromBackglass() As Boolean
        Get
            Return PictureBox.CopyDMDImageFromBackglass
        End Get
        Set(ByVal value As Boolean)
            PictureBox.CopyDMDImageFromBackglass = value
        End Set
    End Property
    Public Property SetDMDDefaultLocation() As Boolean
        Get
            Return PictureBox.SetDMDDefaultLocation
        End Get
        Set(ByVal value As Boolean)
            PictureBox.SetDMDDefaultLocation = value
        End Set
    End Property

    Public Property ShowScoreFrames() As Boolean
        Get
            Return PictureBox.ShowScoreFrames
        End Get
        Set(ByVal value As Boolean)
            PictureBox.ShowScoreFrames = value
            DMDPictureBox.ShowScoreFrames = value
            If BackglassData IsNot Nothing Then BackglassData.ShowScoreFrames = value
        End Set
    End Property
    Public Property ShowScoring() As Boolean
        Get
            Return PictureBox.ShowScoring
        End Get
        Set(ByVal value As Boolean)
            PictureBox.ShowScoring = value
            DMDPictureBox.ShowScoring = value
            If BackglassData IsNot Nothing Then BackglassData.ShowScoring = value
        End Set
    End Property

    Public Property ShowIlluFrames() As Boolean
        Get
            Return PictureBox.ShowIlluFrames
        End Get
        Set(ByVal value As Boolean)
            PictureBox.ShowIlluFrames = value
            DMDPictureBox.ShowIlluFrames = value
            If BackglassData IsNot Nothing Then BackglassData.ShowIlluFrames = value
        End Set
    End Property
    Public Property ShowIllumination() As Boolean
        Get
            Return PictureBox.ShowIllumination
        End Get
        Set(ByVal value As Boolean)
            PictureBox.ShowIllumination = value
            DMDPictureBox.ShowIllumination = value
            If BackglassData IsNot Nothing Then BackglassData.ShowIllumination = value
        End Set
    End Property
    Public Property ShowIntensityIllumination() As Boolean
        Get
            Return PictureBox.ShowIntensityIllumination
        End Get
        Set(ByVal value As Boolean)
            PictureBox.ShowIntensityIllumination = value
            DMDPictureBox.ShowIntensityIllumination = value
            If BackglassData IsNot Nothing Then BackglassData.ShowIllumination = value
        End Set
    End Property

    Public ReadOnly Property RomInfoFilter() As String
        Get
            Return If(PictureBox IsNot Nothing, PictureBox.RomInfoFilter, String.Empty)
        End Get
    End Property

    Public Sub RefreshIllumination()
        If ShowIllumination Then
            ShowIllumination = False
            ShowIllumination = True
        End If
    End Sub

    Public Function DrawIlluminatedReelImage(ByVal reelimage As Image, ByVal reelintensity As Integer, ByVal reelillulocation As eReelIlluminationLocation) As Image
        Return PictureBox.DrawIlluminatedReelImage(reelimage, reelintensity, reelillulocation)
    End Function

    Public Sub ResetAnimationLights()
        If DMDPictureBox.Visible Then
            DMDPictureBox.ResetAnimationLights()
        Else
            PictureBox.ResetAnimationLights()
        End If
    End Sub
    Public Sub ShowAnimation(ByVal animationname As String)
        If DMDPictureBox.Visible Then
            DMDPictureBox.ShowAnimation(animationname)
        Else
            PictureBox.ShowAnimation(animationname)
        End If
    End Sub

    Public Sub New(ByVal _backglassdata As Backglass.Data)
        ' backglass
        BackglassData = _backglassdata
        ' standard new
        MyNew(_backglassdata.Name)
    End Sub
    Public Sub New(ByVal _name As String,
                   ByVal _vsname As String,
                   ByVal _dualbackglass As Boolean,
                   ByVal _author As String,
                   ByVal _artwork As String,
                   ByVal _tabletype As eTableType,
                   ByVal _addemdefaults As Boolean,
                   ByVal _numberofplayers As Integer,
                   ByVal _b2sdatacount As Integer,
                   ByVal _dmdtype As eDMDType,
                   ByVal _commtype As eCommType,
                   ByVal _desttype As eDestType)
        ' backglass
        BackglassData = New Backglass.Data()
        With BackglassData
            .Name = _name
            .VSName = _vsname
            .DualBackglass = _dualbackglass
            .Author = _author
            .Artwork = _artwork
            .TableType = _tabletype
            .AddEMDefaults = _addemdefaults
            .NumberOfPlayers = _numberofplayers
            .B2SDataCount = _b2sdatacount
            .DMDType = _dmdtype
            .CommType = _commtype
            .DestType = _desttype
        End With
        ' standard new
        MyNew(_name)
    End Sub
    Private Sub MyNew(ByVal _name As String)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        ' some global settings
        Me.Name = "tabpage"
        Me.Text = _name & If(BackglassData.IsBackup, " (Backup: '" & BackglassData.BackupName & "')", "")
        Me.AutoScroll = True
        Me.DoubleBuffered = True
        ' create picture box
        PictureBox = New B2SPictureBox(False)
        AddHandler PictureBox.LocationChanged, AddressOf PictureBox_LocationChanged
        AddHandler PictureBox.MyMouseDown, AddressOf PictureBox_MyMouseDown
        AddHandler PictureBox.MyMouseMove, AddressOf PictureBox_MyMouseMove
        AddHandler PictureBox.CopyDMDCopyArea, AddressOf PictureBox_CopyDMDCopyArea
        AddHandler PictureBox.SelectedItemClicked, AddressOf PictureBox_SelectedItemClicked
        AddHandler PictureBox.SelectedItemMoving, AddressOf PictureBox_SelectedItemMoving
        AddHandler PictureBox.SelectedBulbMoved, AddressOf PictureBox_SelectedBulbMoved
        AddHandler PictureBox.SelectedBulbEdited, AddressOf PictureBox_SelectedBulbEdited
        AddHandler PictureBox.SelectedItemRemoved, AddressOf PictureBox_SelectedItemRemoved
        AddHandler PictureBox.LightsReportProgress, AddressOf PictureBox_LightsReportProgress
        PictureBox.Visible = False
        Me.Controls.Add(PictureBox)
        PictureBox.BringToFront()
        ' create DMD picture box
        DMDPictureBox = New B2SPictureBox(True)
        AddHandler DMDPictureBox.LocationChanged, AddressOf DMDPictureBox_LocationChanged
        AddHandler DMDPictureBox.MyMouseDown, AddressOf DMDPictureBox_MyMouseDown
        AddHandler DMDPictureBox.MyMouseMove, AddressOf DMDPictureBox_MyMouseMove
        AddHandler DMDPictureBox.SelectedItemClicked, AddressOf DMDPictureBox_SelectedItemClicked
        AddHandler DMDPictureBox.SelectedItemMoving, AddressOf DMDPictureBox_SelectedItemMoving
        AddHandler DMDPictureBox.SelectedBulbMoved, AddressOf DMDPictureBox_SelectedBulbMoved
        AddHandler DMDPictureBox.SelectedBulbEdited, AddressOf DMDPictureBox_SelectedBulbEdited
        AddHandler DMDPictureBox.SelectedItemRemoved, AddressOf DMDPictureBox_SelectedItemRemoved
        AddHandler DMDPictureBox.LightsReportProgress, AddressOf DMDPictureBox_LightsReportProgress
        DMDPictureBox.Visible = False
        Me.Controls.Add(DMDPictureBox)
        DMDPictureBox.BringToFront()
        ' maybe load picture
        If BackglassData IsNot Nothing Then
            If BackglassData.DMDImage IsNot Nothing Then
                DMDImage = BackglassData.DMDImage
            End If
            If BackglassData.Image IsNot Nothing Then
                Image = BackglassData.Image
            End If
        End If
    End Sub

    Public Shadows Sub Invalidate()
        PictureBox.Invalidate()
        DMDPictureBox.Invalidate()
        Me.OnResize(New EventArgs())
        MyBase.Invalidate()
    End Sub

    Public Sub Zoom(ByVal zoomtext As String)
        If BackglassData IsNot Nothing AndAlso BackglassData.Image IsNot Nothing Then
            If zoomtext.Equals("Window", StringComparison.CurrentCultureIgnoreCase) Then
                Dim currentimage As Image = If(BackglassData.IsDMDImageShown, BackglassData.DMDImage, BackglassData.Image)
                Dim factor As Double = currentimage.Width / Me.Width
                Dim factor2 As Double = currentimage.Height / Me.Height
                If factor < factor2 Then factor = factor2
                Dim width As Integer = (BackglassData.Image.Width - 4) / factor
                Dim height As Integer = (BackglassData.Image.Height - 4) / factor
                PictureBox.Size = New Size(width, height)
                If BackglassData.DMDImage IsNot Nothing Then
                    width = (BackglassData.DMDImage.Width - 4) / factor
                    height = (BackglassData.DMDImage.Height - 4) / factor
                    DMDPictureBox.Size = New Size(width, height)
                End If
                BackglassData.Zoom = CInt(PictureBox.Size.Width / BackglassData.Image.Width * 100)
                Me.Invalidate()
            ElseIf zoomtext.Contains("%") AndAlso IsNumeric(zoomtext.Replace("%", "")) Then
                Zoom(CInt(zoomtext.Replace("%", "")))
            End If
        End If
    End Sub
    Public Sub Zoom(ByVal zoomvalue As Integer)
        If BackglassData IsNot Nothing AndAlso BackglassData.Image IsNot Nothing Then
            Dim width As Integer = CInt(BackglassData.Image.Width * zoomvalue / 100)
            Dim height As Integer = CInt(BackglassData.Image.Height * zoomvalue / 100)
            PictureBox.Size = New Size(width, height)
            If BackglassData.DMDImage IsNot Nothing Then
                width = CInt(BackglassData.DMDImage.Width * zoomvalue / 100)
                height = CInt(BackglassData.DMDImage.Height * zoomvalue / 100)
                DMDPictureBox.Size = New Size(width, height)
            End If
            BackglassData.Zoom = zoomvalue
            Me.Invalidate()
        End If
    End Sub

    Public Sub ReelsAndLEDs_SetNumberOfPlayers(ByVal numberofplayers As Integer)
        BackglassData.NumberOfPlayers = numberofplayers
        Me.Invalidate()
    End Sub
    Public Sub ReelsAndLEDs_SetB2SStartDigit(ByVal startdigit As String)
        If SelectedScore IsNot Nothing Then
            If Not IsNumeric(startdigit) Then startdigit = "0"
            SelectedScore.B2SStartDigit = CInt(startdigit)
            SelectedScore.IsSingleReelSizeDirty = True
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetB2SScoreType(ByVal scoretype As eB2SScoreType)
        If SelectedScore IsNot Nothing Then
            SelectedScore.B2SScoreType = scoretype
            SelectedScore.IsSingleReelSizeDirty = True
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetB2SPlayerNo(ByVal playerno As eB2SPlayerNo)
        If SelectedScore IsNot Nothing Then
            SelectedScore.B2SPlayerNo = playerno
            SelectedScore.IsSingleReelSizeDirty = True
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetDigits(ByVal digits As Integer)
        If SelectedScore IsNot Nothing Then
            SelectedScore.Digits = digits
            SelectedScore.IsSingleReelSizeDirty = True
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetSpacing(ByVal spacing As Integer)
        If SelectedScore IsNot Nothing Then
            SelectedScore.Spacing = spacing
            SelectedScore.IsSingleReelSizeDirty = True
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetLocation(ByVal loc As Point)
        If SelectedScore IsNot Nothing Then
            SelectedScore.Location = loc
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetSize(ByVal size As Size)
        If SelectedScore IsNot Nothing Then
            SelectedScore.Size = size
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetState(ByVal state As eScoreDisplayState)
        If SelectedScore IsNot Nothing Then
            SelectedScore.DisplayState = state
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetReelType(ByVal name As String)
        If SelectedScore IsNot Nothing Then
            SelectedScore.ReelType = name
            SelectedScore.ReelColor = Backglass.currentData.ReelColor
            SelectedScore.IsSingleReelSizeDirty = True
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_SetDream7LEDs(ByVal usethem As Boolean)
        BackglassData.UseDream7LEDs = usethem
        Me.Invalidate()
    End Sub
    Public Sub ReelsAndLEDs_PerfectScaleWidthFix()
        If SelectedScore IsNot Nothing Then
            SelectedScore.PerfectScaleWidthFix = True
            SelectedScore.IsSingleReelSizeDirty = True
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_ChangeLEDColor()
        If SelectedScore IsNot Nothing AndAlso (IsReelImageRendered(SelectedScore.ReelType) OrElse IsReelImageDream7(SelectedScore.ReelType)) Then
            If formSetLEDColor IsNot Nothing Then
                Try
                    formSetLEDColor.Dispose()
                    formSetLEDColor = Nothing
                Catch
                End Try
            End If
            formSetLEDColor = New formSetLEDColor()
            Static lastchoosencolor As Color = Nothing
            Dim startingcolor As Color = SelectedScore.ReelColor
            Dim reelcolor As Color = If((lastchoosencolor <> Nothing), lastchoosencolor, startingcolor)
            If formSetLEDColor.ShowDialog(Me, reelcolor) = DialogResult.OK Then
                SelectedScore.ReelColor = reelcolor
                BackglassData.IsDirty = True
                lastchoosencolor = reelcolor
            Else
                SelectedScore.ReelColor = startingcolor
            End If
            Me.Invalidate()
        End If
    End Sub
    Private Sub formSetLEDColor_ColorChanged(sender As Object, e As formSetLEDColor.LEDColorEventArgs) Handles formSetLEDColor.ColorChanged
        If SelectedScore IsNot Nothing Then
            SelectedScore.ReelColor = e.Color
            Me.Invalidate()
        End If
    End Sub
    Public Sub ReelsAndLEDs_ReelIllumination()
        If SelectedScore IsNot Nothing AndAlso Not (IsReelImageRendered(SelectedScore.ReelType) OrElse IsReelImageDream7(SelectedScore.ReelType)) Then
            If formSetReelIllumination IsNot Nothing Then
                Try
                    formSetReelIllumination.Dispose()
                    formSetReelIllumination = Nothing
                Catch
                End Try
            End If
            formSetReelIllumination = New formSetReelIllumination()
            Dim reelillulocation As eReelIlluminationLocation = SelectedScore.ReelIlluLocation
            Dim reelillub2sid As Integer = SelectedScore.ReelIlluB2SID
            Dim reelillub2sidtype As eB2SIDType = SelectedScore.ReelIlluB2SIDType
            Dim reelillub2svalue As Integer = SelectedScore.ReelIlluB2SValue
            Dim reelilluintensity As Integer = SelectedScore.ReelIlluIntensity
            If formSetReelIllumination.ShowDialog(Me, reelillulocation, reelillub2sid, reelillub2sidtype, reelillub2svalue, reelilluintensity) Then
                SelectedScore.ReelIlluLocation = reelillulocation
                SelectedScore.ReelIlluB2SID = reelillub2sid
                SelectedScore.ReelIlluB2SIDType = reelillub2sidtype
                SelectedScore.ReelIlluB2SValue = reelillub2svalue
                SelectedScore.ReelIlluIntensity = reelilluintensity
                BackglassData.IsDirty = True
            End If
        End If
    End Sub

    Public Sub ReelsAndLEDs_AddScore()
        BackglassData.IsDirty = True
        ' add new score display
        Dim newScore As ReelAndLED.ScoreInfo = New ReelAndLED.ScoreInfo
        With newScore
            '.ReelType = BackglassData.ReelType
            'If String.IsNullOrEmpty(.ReelType) Then .ReelType = "LED"
            .Digits = 6
            .Spacing = 5
            If Mouse.LastScoreSize <> Nothing Then
                .Size = Mouse.LastScoreSize
            Else
                .Size = New Size(300, 100)
            End If
            If Mouse.LastScoreDigits <> Nothing Then .Digits = Mouse.LastScoreDigits
            If Mouse.LastScoreSpacing <> Nothing Then .Spacing = Mouse.LastScoreSpacing
            If Mouse.LastScoreReelType <> Nothing Then .ReelType = Mouse.LastScoreReelType
            If Mouse.LastScoreReelColor <> Nothing Then .ReelColor = Mouse.LastScoreReelColor
            If String.IsNullOrEmpty(.ReelType) Then
                .ReelType = GetFirstReelType()
            End If
            If String.IsNullOrEmpty(.ReelType) Then
                MessageBox.Show(My.Resources.MSG_CheckB2SScoreTypes, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            If CurrentPictureBox IsNot Nothing Then
                Dim x As Integer = CurrentPictureBox.Left * -1
                If CurrentPictureBox.Parent IsNot Nothing Then x += (CurrentPictureBox.Parent.Width - 50) / 2 - .Size.Width
                Dim y As Integer = CurrentPictureBox.Top * -1
                If CurrentPictureBox.Parent IsNot Nothing Then y += (CurrentPictureBox.Parent.Height - 50) / 2 - .Size.Height
                .Location = New Point(x / Mouse.factor, y / Mouse.factor)
            Else
                .Location = New Point(10, 10)
            End If
        End With
        If BackglassData.IsDMDImageShown Then
            newScore.ParentForm = eParentForm.DMD
            BackglassData.DMDScores.Add(BackglassData.DMDScores.Count + BackglassData.Scores.Count + 1, newScore)
        Else
            newScore.ParentForm = eParentForm.Backglass
            BackglassData.Scores.Add(BackglassData.DMDScores.Count + BackglassData.Scores.Count + 1, newScore)
        End If
        Mouse.SelectedScore = newScore
        Undo.AddEntry(New Undo.UndoEntry(Undo.Type.ScoreAdded, newScore))
        Me.Invalidate()
    End Sub

    Public Sub Illumination_SetName(ByVal name As String)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.Name = name
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetID(ByVal id As String)
        If SelectedBulb IsNot Nothing Then
            If Not IsNumeric(id) Then id = "0"
            SelectedBulb.ID = CInt(id)
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetB2SID(ByVal b2sid As String)
        If SelectedBulb IsNot Nothing Then
            If Not IsNumeric(b2sid) Then b2sid = "0"
            SelectedBulb.B2SID = CInt(b2sid)
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetB2SIDType(ByVal b2sidtype As eB2SIDType)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.B2SIDType = b2sidtype
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetB2SValue(ByVal b2svalue As String)
        If SelectedBulb IsNot Nothing Then
            If Not IsNumeric(b2svalue) Then b2svalue = "0"
            SelectedBulb.B2SValue = CInt(b2svalue)
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetRomID(ByVal romid As String)
        If SelectedBulb IsNot Nothing Then
            If Not IsNumeric(romid) Then romid = "0"
            SelectedBulb.RomID = CInt(romid)
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetRomIDType(ByVal romidtype As eRomIDType)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.RomIDType = romidtype
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetRomInverted(ByVal rominverted As Boolean)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.RomInverted = rominverted
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetInitialState(ByVal initialState As Integer)
        If SelectedBulb IsNot Nothing Then
            If initialState < 0 OrElse initialState > 3 Then initialState = 0
            SelectedBulb.InitialState = initialState
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetDualMode(ByVal dualMode As eDualMode)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.DualMode = dualMode
            BackglassData.IsDirty = True
        End If
    End Sub
    Public Sub Illumination_SetIntensity(ByVal intensity As Integer)
        If SelectedBulb IsNot Nothing Then
            If intensity < 1 OrElse intensity > MaxBulbIntensity Then intensity = 1
            SelectedBulb.Intensity = intensity
            SelectedBulb.IsIlluminatedImageDirty = True
            BackglassData.IsDirty = True
            RefreshIllumination()
        End If
    End Sub
    Public Sub Illumination_DodgeColor(ByVal color As Color)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.DodgeColor = color
            SelectedBulb.IsIlluminatedImageDirty = True
            BackglassData.IsDirty = True
            RefreshIllumination()
        End If
    End Sub
    Public Sub Illumination_IlluMode(ByVal illumode As Illumination.eIlluMode)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.IlluMode = illumode
            SelectedBulb.IsIlluminatedImageDirty = True
            BackglassData.IsDirty = True
            RefreshIllumination()
        End If
    End Sub
    Public Sub Illumination_SetText(ByVal text As String)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.Text = text
            Me.Invalidate()
            SelectedBulb.IsIlluminatedImageDirty = True
            BackglassData.IsDirty = True
            RefreshIllumination()
        End If
    End Sub
    Public Sub Illumination_SetTextAlignment(ByVal alignment As Illumination.eTextAlignment)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.TextAlignment = alignment
            Me.Invalidate()
            SelectedBulb.IsIlluminatedImageDirty = True
            BackglassData.IsDirty = True
            RefreshIllumination()
        End If
    End Sub
    Public Sub Illumination_SetTextFont(ByVal font As Font)
        If SelectedBulb IsNot Nothing Then
            With SelectedBulb
                .FontName = font.Name
                .FontSize = font.Size
                .FontStyle = font.Style
            End With
            Me.Invalidate()
            SelectedBulb.IsIlluminatedImageDirty = True
            BackglassData.IsDirty = True
            RefreshIllumination()
        End If
    End Sub
    Public Sub Illumination_SetLocation(ByVal loc As Point)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.Location = loc
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub Illumination_SetSize(ByVal size As Size)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.Size = size
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub Illumination_SetZOrder(ByVal zorder As Integer)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.ZOrder = zorder
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub Illumination_SetSnippitInfo(ByVal snippitinfo As Illumination.SnippitInfo)
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.SnippitInfo = snippitinfo
            BackglassData.IsDirty = True
            Me.Invalidate()
        End If
    End Sub
    Public Sub Illumination_ChangeLightColor()
        If SelectedBulb IsNot Nothing Then
            If formSetLightColor IsNot Nothing Then
                Try
                    formSetLightColor.Dispose()
                    formSetLightColor = Nothing
                Catch
                End Try
            End If
            formSetLightColor = New formSetLEDColor()
            Static lastchoosencolor As Color = Nothing
            Dim startingcolor As Color = SelectedBulb.LightColor
            Dim lightcolor As Color = If((lastchoosencolor <> Nothing), lastchoosencolor, startingcolor)
            If formSetLightColor.ShowDialog(Me, lightcolor, True) = DialogResult.OK Then
                SelectedBulb.LightColor = lightcolor
                BackglassData.IsDirty = True
                lastchoosencolor = Nothing 'lightcolor (do not store the last used color)
            Else
                SelectedBulb.LightColor = startingcolor
            End If
            SelectedBulb.IsIlluminatedImageDirty = True
            RaiseEvent LightColorChanged(Me, New Illumination.Lights.LightColorChangedEventArgs(SelectedBulb.LightColor))
            RefreshIllumination()
        End If
    End Sub
    Private Sub formSetLightColor_ColorChanged(sender As Object, e As formSetLEDColor.LEDColorEventArgs) Handles formSetLightColor.ColorChanged
        If SelectedBulb IsNot Nothing Then
            SelectedBulb.LightColor = e.Color
            SelectedBulb.IsIlluminatedImageDirty = True
            RaiseEvent LightColorChanged(Me, New Illumination.Lights.LightColorChangedEventArgs(SelectedBulb.LightColor))
            RefreshIllumination()
        End If
    End Sub

    Public Sub Illumination_SetRomFilter(ByVal rominfo As String)
        PictureBox.RomInfoFilter = rominfo
        DMDPictureBox.RomInfoFilter = rominfo
        RefreshIllumination()
    End Sub

    Public Sub Illumination_AddBulb()
        BackglassData.IsDirty = True
        ' add new bulb
        Dim newBulb As Illumination.BulbInfo = New Illumination.BulbInfo
        With newBulb
            If Mouse.LastBulbSize <> Nothing Then
                .Size = Mouse.LastBulbSize
            Else
                .Size = New Size(300, 100)
            End If
            If Mouse.LastBulbIntensity <> Nothing Then .Intensity = Mouse.LastBulbIntensity
            If Mouse.LastBulbLightColor <> Nothing Then .LightColor = Mouse.LastBulbLightColor
            If Mouse.LastBulbDodgeColor <> Nothing Then .DodgeColor = Mouse.LastBulbDodgeColor
            If Mouse.LastBulbInitialState <> Nothing Then .InitialState = Mouse.LastBulbInitialState
            If Mouse.LastBulbFont IsNot Nothing Then
                .FontName = Mouse.LastBulbFont.Name
                .FontSize = Mouse.LastBulbFont.Size
                .FontStyle = Mouse.LastBulbFont.Style
            End If
            If Not String.IsNullOrEmpty(PictureBox.RomInfoFilter) Then
                Select Case PictureBox.RomInfoFilter.ToLower
                    Case "off"
                        .InitialState = 0
                    Case "on"
                        .InitialState = 1
                    Case "alwayson"
                        .InitialState = 2
                    Case "authentic"
                        .DualMode = eDualMode.Authentic
                    Case "fantasy"
                        .DualMode = eDualMode.Fantasy
                    Case Else
                        Dim str As String = PictureBox.RomInfoFilter.ToLower.Substring(0, 1)
                        If Not String.IsNullOrEmpty(str) Then
                            Dim inverted As Boolean = (str = "i")
                            If inverted Then
                                str = PictureBox.RomInfoFilter.ToLower.Substring(1, 1)
                            End If
                            Select Case str
                                Case "l"
                                    .RomIDType = eRomIDType.Lamp
                                    .RomID = CInt(PictureBox.RomInfoFilter.Substring(1 + If(inverted, 1, 0)))
                                Case "s"
                                    .RomIDType = eRomIDType.Solenoid
                                    .RomID = CInt(PictureBox.RomInfoFilter.Substring(1 + If(inverted, 1, 0)))
                                Case "g"
                                    .RomIDType = eRomIDType.GIString
                                    .RomID = CInt(PictureBox.RomInfoFilter.Substring(2 + If(inverted, 1, 0)))
                                Case Else
                                    str = PictureBox.RomInfoFilter
                                    If IsNumeric(str) Then
                                        .B2SID = CInt(str)
                                        Select Case str
                                            Case "25"
                                                .B2SIDType = eB2SIDType.ScoreRolloverPlayer1_25
                                            Case "26"
                                                .B2SIDType = eB2SIDType.ScoreRolloverPlayer2_26
                                            Case "27"
                                                .B2SIDType = eB2SIDType.ScoreRolloverPlayer3_27
                                            Case "28"
                                                .B2SIDType = eB2SIDType.ScoreRolloverPlayer4_28
                                            Case "30"
                                                .B2SIDType = eB2SIDType.PlayerUp_30
                                            Case "31"
                                                .B2SIDType = eB2SIDType.CanPlay_31
                                            Case "32"
                                                .B2SIDType = eB2SIDType.BallInPlay_32
                                            Case "33"
                                                .B2SIDType = eB2SIDType.Tilt_33
                                            Case "34"
                                                .B2SIDType = eB2SIDType.Match_34
                                            Case "35"
                                                .B2SIDType = eB2SIDType.GameOver_35
                                            Case "36"
                                                .B2SIDType = eB2SIDType.ShootAgain_36
                                        End Select
                                    End If
                            End Select
                            .RomInverted = inverted
                        End If
                End Select
            End If
            If CurrentPictureBox IsNot Nothing Then
                Dim x As Integer = CurrentPictureBox.Left * -1
                If CurrentPictureBox.Parent IsNot Nothing Then x += (CurrentPictureBox.Parent.Width - 50) / 2 - .Size.Width
                Dim y As Integer = CurrentPictureBox.Top * -1
                If CurrentPictureBox.Parent IsNot Nothing Then y += (CurrentPictureBox.Parent.Height - 50) / 2 - .Size.Height
                .Location = New Point(x / Mouse.factor, y / Mouse.factor)
            Else
                .Location = New Point(10, 10)
            End If
        End With
        If BackglassData.IsDMDImageShown Then
            newBulb.ParentForm = eParentForm.DMD
            BackglassData.DMDBulbs.Add(newBulb)
        Else
            newBulb.ParentForm = eParentForm.Backglass
            BackglassData.Bulbs.Add(newBulb)
        End If
        Mouse.SelectedBulb = newBulb
        Undo.AddEntry(New Undo.UndoEntry(Undo.Type.BulbAdded, newBulb, BackglassData.IsDMDImageShown))
        Me.Invalidate()
        RefreshIllumination()
    End Sub
    Public Sub Illumination_AddSnippit(ByVal name As String, ByVal image As Image, ByVal loc As Point)
        If image IsNot Nothing Then
            BackglassData.IsDirty = True
            ' add new snippit
            Dim newBulb As Illumination.BulbInfo = New Illumination.BulbInfo
            With newBulb
                .Name = name
                .Size = New Size(image.Width, image.Height)
                .Location = loc
                .Image = image
                .IsImageSnippit = True
            End With
            If BackglassData.IsDMDImageShown Then
                newBulb.ParentForm = eParentForm.DMD
                BackglassData.DMDBulbs.Add(newBulb)
            Else
                newBulb.ParentForm = eParentForm.Backglass
                BackglassData.Bulbs.Add(newBulb)
            End If
            Mouse.SelectedBulb = newBulb
            Undo.AddEntry(New Undo.UndoEntry(Undo.Type.BulbAdded, newBulb, BackglassData.IsDMDImageShown))
            Me.Invalidate()
        End If
    End Sub

End Class