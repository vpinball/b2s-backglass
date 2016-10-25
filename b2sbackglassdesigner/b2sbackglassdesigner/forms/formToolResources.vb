Imports System

Public Class formToolResources

    Public Enum eImagesDataType
        NotDefined = 0
        BackgroundImageRemoved = 1
        IlluminationImageRemoved = 2
        DMDImageRemoved = 3
        BackgroundImageSelectionChanged = 6
        IlluminatedImageSelectionChanged = 7
        DMDImageSelectionChanged = 8
        BackgroundImageTypeChanged = 11
        BackgroundImageRomIDChanged = 12
        BackgroundImageRomIDTypeChanged = 13
    End Enum

    Public Event DataChanged(ByVal sender As Object, ByVal e As ImagesEventArgs)
    Public Class ImagesEventArgs
        Inherits EventArgs

        Public TypeOfData As eImagesDataType = eImagesDataType.NotDefined
        Public Data As Object = Nothing

        Public Sub New(ByVal _typeofdata As eImagesDataType)
            TypeOfData = _typeofdata
        End Sub
        Public Sub New(ByVal _typeofdata As eImagesDataType, ByVal _data As Object)
            TypeOfData = _typeofdata
            Data = _data
        End Sub
    End Class

    Private _imageInfoList As Images.ImageCollection = Nothing
    Public Property ImageInfoList() As Images.ImageCollection
        Get
            Return _imageInfoList
        End Get
        Set(ByVal value As Images.ImageCollection)
            _imageInfoList = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MyBase.SaveName = Me.Name
        MyBase.DefaultLocation = eDefaultLocation.NW

        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.DoubleBuffered = True

    End Sub

    Public Shadows Sub Invalidate()
        lbImages.DataSource = Nothing
        lbImages.DataSource = ImageInfoList
        MyBase.Invalidate()
    End Sub

    Private Sub formToolResources_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lbImages.ItemHeight = 54
        lbImages.DrawMode = DrawMode.OwnerDrawFixed

        lbImages.DataSource = ImageInfoList

        cmbImageType.SelectionLength = 0
        cmbROMIDType.SelectionLength = 0

    End Sub

    Private Sub Images_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbImages.Resize
        lbImages.Invalidate()
    End Sub
    Private Sub Images_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles lbImages.DrawItem

        If e.Index > -1 Then
            Dim item As Images.ImageInfo = CType(lbImages.Items(e.Index), Images.ImageInfo)
            e.DrawBackground()
            e.DrawFocusRectangle()
            'e.Graphics.FillRectangle(If(e.Index = lbImages.SelectedIndex, New SolidBrush(Color.FromKnownColor(KnownColor.Highlight)), Brushes.White), e.Bounds)
            If IsTitleItem(item) Then
                TextRenderer.DrawText(e.Graphics, item.Text, New Font(Me.Font.Name, Me.Font.Size + 1, FontStyle.Bold Or FontStyle.Underline), e.Bounds, Color.Black, TextFormatFlags.WordBreak Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            Else
                Dim imagewidth As Integer = 0
                ' maybe draw preview image
                Dim isSmallImage As Boolean = False
                If item.Image IsNot Nothing Then
                    ' maybe shrink image
                    isSmallImage = (item.Type = Images.eImageInfoType.IlluminationSnippits)
                    Dim factor As Single = item.Image.Height / If(isSmallImage, 32, 48) 'If(item.Image.Width > item.Image.Height, item.Image.Width / 48, item.Image.Height / 48)
                    Dim helper As HelperBase = New HelperBase()
                    Dim shrinkedimage As Image = item.Image.Resized(New Size(Math.Min(item.Image.Width / factor, e.Bounds.Width / 2), item.Image.Height / factor))
                    imagewidth = shrinkedimage.Width
                    ' draw image
                    e.Graphics.DrawImage(shrinkedimage, New Point(5, e.Bounds.Y + 3 + If(isSmallImage, 8, 0)))
                End If
                ' text 
                TextRenderer.DrawText(e.Graphics, item.Text.Replace("\", "\ "), Me.Font, New Rectangle(imagewidth + 10, e.Bounds.Y, e.Bounds.Width - imagewidth - 15, e.Bounds.Height - 2), Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.WordBreak)
            End If
        End If

    End Sub

    Private Sub Images_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbImages.Click
        SelectItem(sender, False)
    End Sub
    Private Sub Images_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbImages.DoubleClick
        SelectItem(sender, True)
    End Sub
    Private Sub Images_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lbImages.KeyDown
        If lbImages.SelectedItem IsNot Nothing Then
            Dim item As Images.ImageInfo = TryCast(sender.SelectedItem, Images.ImageInfo)
            If item IsNot Nothing Then
                If e.KeyCode = Keys.Delete Then
                    If item.Type = Images.eImageInfoType.BackgroundImage OrElse item.Type = Images.eImageInfoType.IlluminationImage OrElse item.Type = Images.eImageInfoType.DMDImage Then
                        ImageInfoList.Remove(item)
                        If item.Type = Images.eImageInfoType.BackgroundImage Then
                            RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.BackgroundImageRemoved, Nothing))
                        ElseIf item.Type = Images.eImageInfoType.IlluminationImage Then
                            RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.IlluminationImageRemoved, Nothing))
                        ElseIf item.Type = Images.eImageInfoType.DMDImage Then
                            RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.DMDImageRemoved, Nothing))
                        End If
                        Me.Invalidate()
                    End If
                ElseIf e.KeyCode = Keys.Space Then
                    SelectItem(sender.SelectedItem, True)
                End If
            End If
        End If
    End Sub
    Private Sub lbImages_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lbImages.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            lbImages.SelectedIndex = lbImages.IndexFromPoint(e.X, e.Y)
            If lbImages.SelectedItem IsNot Nothing Then
                Dim item As Images.ImageInfo = TryCast(sender.SelectedItem, Images.ImageInfo)
                If item.Image IsNot Nothing Then
                    Using sfd As SaveFileDialog = New SaveFileDialog
                        With sfd
                            .Filter = ImageFileExtensionFilter
                            .FileName = item.Text
                            If .ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Try
                                    item.Image.Save(.FileName)
                                Catch ex As Exception
                                    MessageBox.Show(String.Format(My.Resources.MSG_CannotSavePicFile, ex.Message), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End Try
                            End If
                        End With
                    End Using
                End If
            End If
        End If
    End Sub

    Private Sub ImageType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbImageType.SelectedIndexChanged
        If DoNoEvents Then Exit Sub
        If lbImages.SelectedItem IsNot Nothing Then
            Dim item As Images.ImageInfo = TryCast(lbImages.SelectedItem, Images.ImageInfo)
            If item IsNot Nothing AndAlso item.Type = Images.eImageInfoType.BackgroundImage Then
                item.BackgroundImageType = cmbImageType.SelectedIndex
            End If
        End If
        RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.BackgroundImageTypeChanged, cmbImageType.SelectedIndex))
    End Sub
    Private Sub RomID_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtRomID.TextChanged
        If DoNoEvents Then Exit Sub
        If Not String.IsNullOrEmpty(txtRomID.Text) Then
            If (Not IsNumeric(txtRomID.Text) OrElse txtRomID.Text = "0") Then txtRomID.Text = ""
            If Not String.IsNullOrEmpty(txtRomID.Text) AndAlso cmbROMIDType.SelectedIndex <= 0 Then cmbROMIDType.SelectedIndex = 1
        End If
        If lbImages.SelectedItem IsNot Nothing Then
            Dim item As Images.ImageInfo = TryCast(lbImages.SelectedItem, Images.ImageInfo)
            If item IsNot Nothing AndAlso item.Type = Images.eImageInfoType.BackgroundImage Then
                item.RomID = CInt(If(Not String.IsNullOrEmpty(txtRomID.Text), txtRomID.Text, "0"))
            End If
        End If
        RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.BackgroundImageRomIDChanged, txtRomID.Text))
    End Sub
    Private Sub ROMIDType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbROMIDType.SelectedIndexChanged
        If DoNoEvents Then Exit Sub
        If lbImages.SelectedItem IsNot Nothing Then
            Dim item As Images.ImageInfo = TryCast(lbImages.SelectedItem, Images.ImageInfo)
            If item IsNot Nothing AndAlso item.Type = Images.eImageInfoType.BackgroundImage Then
                item.RomIDType = cmbROMIDType.SelectedIndex
            End If
        End If
        RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.BackgroundImageRomIDTypeChanged, cmbROMIDType.SelectedIndex))
    End Sub
    Private Sub ROMIDType_TextChanged(sender As Object, e As System.EventArgs) Handles cmbROMIDType.TextChanged
        If String.IsNullOrEmpty(cmbROMIDType.Text) Then
            cmbROMIDType.SelectedIndex = 0
        End If
    End Sub

    Private DoNoEvents As Boolean = False
    Private Sub SelectItem(sender As Object, ByVal doubleClick As Boolean)
        If lbImages.SelectedItem IsNot Nothing Then
            Dim item As Images.ImageInfo = TryCast(sender.SelectedItem, Images.ImageInfo)
            If item IsNot Nothing AndAlso Not IsTitleItem(item) Then
                Select Case item.Type
                    Case Images.eImageInfoType.BackgroundImage
                        DoNoEvents = True
                        cmbImageType.SelectedIndex = item.BackgroundImageType
                        txtRomID.Text = item.RomID.ToString()
                        If txtRomID.Text = "0" Then txtRomID.Text = String.Empty
                        cmbROMIDType.SelectedIndex = item.RomIDType
                        DoNoEvents = False
                        PanelImages.Visible = True
                        If doubleClick Then
                            RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.BackgroundImageSelectionChanged, item))
                        End If
                    Case Images.eImageInfoType.IlluminationImage
                        PanelImages.Visible = False
                        If doubleClick Then
                            RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.IlluminatedImageSelectionChanged, item))
                        End If
                    Case Images.eImageInfoType.DMDImage
                        PanelImages.Visible = False
                        If doubleClick Then
                            RaiseEvent DataChanged(Me, New ImagesEventArgs(eImagesDataType.DMDImageSelectionChanged, item))
                        End If
                    Case Else
                        PanelImages.Visible = False
                End Select
            Else
                PanelImages.Visible = False
            End If
        End If
    End Sub

    Private ReadOnly Property IsTitleItem(item As Images.ImageInfo) As Boolean
        Get
            Return (item.Type = Images.eImageInfoType.Title4BackgroundImages OrElse item.Type = Images.eImageInfoType.Title4IlluminationImages OrElse item.Type = Images.eImageInfoType.Title4DMDImages OrElse item.Type = Images.eImageInfoType.Title4IlluminationSnippits)
        End Get
    End Property

End Class