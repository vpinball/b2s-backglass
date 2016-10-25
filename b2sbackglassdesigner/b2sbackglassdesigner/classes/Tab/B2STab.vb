Imports System
Imports System.Drawing.Drawing2D

Public Class B2STab

    Inherits ContainerControl

    Public Shadows Event MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Shadows Event MouseMove(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event CopyDMDCopyArea(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event SelectedItemClicked(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event SelectedItemMoving(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event SelectedBulbMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event SelectedBulbEdited(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event LightsReportProgress(ByVal sender As Object, ByVal e As Illumination.Lights.LightsProgressEventArgs)
    Public Event LightColorChanged(ByVal sedner As Object, ByVal e As Illumination.Lights.LightColorChangedEventArgs)

    Private Const tabheight As Integer = 25

    Private fontstd As Font = New Font("Segoe UI", 9, FontStyle.Regular)
    Private fontbold As Font = New Font("Segoe UI", 9, FontStyle.Bold)

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        ' draw background
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality
        Dim brush As LinearGradientBrush = New LinearGradientBrush(e.ClipRectangle, Color.FromArgb(190, 206, 220), Color.FromArgb(215, 228, 242), LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(brush, e.ClipRectangle)
        brush.Dispose()
        Dim pen As Pen = New Pen(Color.DarkGray)
        e.Graphics.DrawLine(pen, 0, Me.Height - 1, Me.Width - 1, Me.Height - 1)
        pen.Dispose()
        ' draw tabs
        If TabPages.Count > 0 Then
            Dim i As Integer = 0
            Dim x As Integer = 3
            Dim y As Integer = Me.Height - tabheight + 3
            e.Graphics.DrawLine(Pens.Black, 0, y, x, y)
            For Each tabpage As B2STabPage In TabPages
                With tabpage
                    Dim size As Size = TextRenderer.MeasureText(.Text, If(i = SelectedIndex, fontbold, fontstd))
                    Dim thumbnailwidth As Integer = If(.ThumbnailImage IsNot Nothing, .ThumbnailImage.Width + 2, 0)
                    .TabLocation = New Point(x, y)
                    .TabSize = New Size(size.Width + 10 + thumbnailwidth, tabheight - 8 + If(i = SelectedIndex, 2, 0))
                    If thumbnailwidth > 0 Then
                        e.Graphics.DrawImage(.ThumbnailImage, New Point(x + 6, y))
                        If i = SelectedIndex Then
                            e.Graphics.DrawLine(Pens.White, x + 6 + 1, y + 16, x + 6 + 16, y + 16)
                            e.Graphics.DrawLine(Pens.White, x + 6 + 16, y + 1, x + 6 + 16, y + 16)
                        End If
                    End If
                    If i = SelectedIndex Then
                        'e.Graphics.DrawString(.Text, If(i = SelectedIndex, fontbold, fontstd), Brushes.White, New Point(x + 6 + thumbnailwidth + 1, y + 1))
                        TextRenderer.DrawText(e.Graphics, .Text, If(i = SelectedIndex, fontbold, fontstd), New Point(x + 6 + thumbnailwidth + 1, y + 1), Color.White)
                    End If
                    'e.Graphics.DrawString(.Text, If(i = SelectedIndex, fontbold, fontstd), Brushes.Black, New Point(x + 6 + thumbnailwidth, y))
                    TextRenderer.DrawText(e.Graphics, .Text, If(i = SelectedIndex, fontbold, fontstd), New Point(x + 6 + thumbnailwidth, y), Color.Black)
                    If i = SelectedIndex Then
                        e.Graphics.DrawLine(Pens.White, .TabLocation.X + 1, .TabLocation.Y, .TabLocation.X + 1, .TabLocation.Y + .TabSize.Height - 1)
                        e.Graphics.DrawLine(Pens.White, .TabLocation.X + 1, .TabLocation.Y + .TabSize.Height - 1, .TabLocation.X + .TabSize.Width - 1, .TabLocation.Y + .TabSize.Height - 1)
                        e.Graphics.DrawLine(Pens.White, .TabLocation.X + .TabSize.Width - 1, .TabLocation.Y + .TabSize.Height - 1, .TabLocation.X + .TabSize.Width - 1, .TabLocation.Y)
                    End If
                    e.Graphics.DrawLine(Pens.Black, .TabLocation.X, .TabLocation.Y, .TabLocation.X, .TabLocation.Y + .TabSize.Height - 1)
                    e.Graphics.DrawLine(Pens.Black, .TabLocation.X + 1, .TabLocation.Y + .TabSize.Height, .TabLocation.X + .TabSize.Width, .TabLocation.Y + .TabSize.Height)
                    e.Graphics.DrawLine(Pens.Black, .TabLocation.X + .TabSize.Width, .TabLocation.Y + .TabSize.Height, .TabLocation.X + .TabSize.Width, .TabLocation.Y)
                    x += .TabSize.Width
                End With
                i += 1
            Next
        End If
    End Sub
    Protected Overrides Sub OnPaintBackground(e As System.Windows.Forms.PaintEventArgs)
        ' nothing to do        
    End Sub

    Private Sub B2STab_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown
        If e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
            e.KeyCode = Keys.Delete OrElse
            (e.KeyCode >= Keys.D1 AndAlso e.KeyCode <= Keys.D5) OrElse
            e.KeyCode = Keys.O OrElse e.KeyCode = Keys.R OrElse e.KeyCode = Keys.G OrElse e.KeyCode = Keys.B OrElse e.KeyCode = Keys.Y OrElse e.KeyCode = Keys.M OrElse e.KeyCode = Keys.P OrElse e.KeyCode = Keys.W Then
            If SelectedTabPage IsNot Nothing Then
                SelectedTabPage.Mouse.KeyIsPressed(e.Control, e.KeyCode)
            End If
        End If
    End Sub
    Private Sub B2STab_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        Dim I As Integer = 0
        For Each tabpage As B2STabPage In TabPages
            With tabpage
                If e.X >= .TabLocation.X AndAlso e.X <= .TabLocation.X + .TabSize.Width AndAlso e.Y >= .TabLocation.Y AndAlso e.Y <= .TabLocation.Y + .TabSize.Height Then
                    SelectedIndex = I
                    Exit For
                End If
            End With
            I += 1
        Next
    End Sub

    Private Sub B2STabPage_Scrolled(ByVal sender As Object, ByVal e As B2STabPage.B2STabPageScrollEventArgs)
        _offsetlocation = e.Location
    End Sub
    Private Sub B2STabPage_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent MouseDown(sender, e)
    End Sub
    Private Sub B2STabPage_MouseMove(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
        RaiseEvent MouseMove(sender, e)
    End Sub
    Private Sub B2STabPage_CopyDMDCopyArea(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent CopyDMDCopyArea(sender, e)
    End Sub
    Private Sub B2STabPage_SelectedItemMoving(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
        RaiseEvent SelectedItemMoving(sender, e)
    End Sub
    Private Sub B2STabPage_SelectedBulbMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent SelectedBulbMoved(sender, e)
    End Sub
    Private Sub B2STabPage_SelectedBulbEdited(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent SelectedBulbEdited(sender, e)
    End Sub
    Private Sub B2STabPage_SelectedItemClicked(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
        RaiseEvent SelectedItemClicked(sender, e)
    End Sub

    Private Sub B2STabPage_LightsReportProgress(ByVal sender As Object, ByVal e As Illumination.Lights.LightsProgressEventArgs)
        RaiseEvent LightsReportProgress(sender, e)
    End Sub
    Private Sub B2STabPage_LightColorChanged(ByVal sender As Object, ByVal e As Illumination.Lights.LightColorChangedEventArgs)
        RaiseEvent LightColorChanged(sender, e)
    End Sub

    Private _selectedIndex As Integer = -1
    Public Property SelectedIndex() As Integer
        Get
            Return _selectedIndex
        End Get
        Set(ByVal value As Integer)
            If value >= 0 AndAlso value < TabPages.Count Then
                _selectedIndex = value
                For Each tabpage As B2STabPage In TabPages
                    tabpage.Visible = False
                Next
                Backglass.currentData = TabPages(_selectedIndex).BackglassData
                Backglass.currentTabPage = TabPages(_selectedIndex)
                TabPages(_selectedIndex).Visible = True
                Me.Invalidate()
            Else
                _selectedIndex = -1
                Backglass.currentData = Nothing
                Backglass.currentTabPage = Nothing
            End If
            RaiseEvent SelectedIndexChanged(Me, New EventArgs())
        End Set
    End Property
    Public ReadOnly Property SelectedTabPage() As B2STabPage
        Get
            Return If(SelectedIndex > -1, TabPages(SelectedIndex), Nothing)
        End Get
    End Property

    Private _tabpages As Generic.List(Of B2STabPage) = New Generic.List(Of B2STabPage)
    Public ReadOnly Property TabPages() As Generic.List(Of B2STabPage)
        Get
            Return _tabpages
        End Get
    End Property

    Private _offsetlocation As Point = New Point(0, 0)
    Public ReadOnly Property OffsetLocation() As Point
        Get
            Return _offsetlocation
        End Get
    End Property

    Public Sub New()
        ' set some styles
        Me.SetStyle(ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint, True)
        Me.UpdateStyles()
        Me.DoubleBuffered = True
        ' set padding
        Me.Padding = New Padding(0, 0, 0, tabheight)
    End Sub

    Public Sub AddBackglass(ByVal tabpage As B2STabPage)
        tabpage.Location = New Point(0, 0)
        tabpage.Dock = DockStyle.Fill
        tabpage.Visible = False
        AddHandler tabpage.Scrolled, AddressOf B2STabPage_Scrolled
        AddHandler tabpage.MyMouseDown, AddressOf B2STabPage_MouseDown
        AddHandler tabpage.MyMouseMove, AddressOf B2STabPage_MouseMove
        AddHandler tabpage.CopyDMDCopyArea, AddressOf B2STabPage_CopyDMDCopyArea
        AddHandler tabpage.SelectedItemClicked, AddressOf B2STabPage_SelectedItemClicked
        AddHandler tabpage.SelectedItemMoving, AddressOf B2STabPage_SelectedItemMoving
        AddHandler tabpage.LightsReportProgress, AddressOf B2STabPage_LightsReportProgress
        AddHandler tabpage.LightColorChanged, AddressOf B2STabPage_LightColorChanged
        AddHandler tabpage.SelectedBulbMoved, AddressOf B2STabPage_SelectedBulbMoved
        AddHandler tabpage.SelectedBulbEdited, AddressOf B2STabPage_SelectedBulbEdited
        TabPages.Add(tabpage)
        Me.Controls.Add(tabpage)
        OnResize(New EventArgs())
    End Sub
    Public Function RemoveBackglass(ByVal index As Integer) As DialogResult
        Dim ret As DialogResult = DialogResult.No
        If TabPages.Count > 0 AndAlso TabPages.Count > index Then
            Dim tabpage As B2STabPage = TabPages(index)
            If tabpage.BackglassData.IsDirty Then
                ret = MessageBox.Show(My.Resources.MSG_IsDirty, AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If ret = DialogResult.Yes Then
                    Dim helper As Save = New Save()
                    helper.SaveData(tabpage.BackglassData)
                End If
            End If
            If ret = DialogResult.No Then
                RemoveHandler tabpage.Scrolled, AddressOf B2STabPage_Scrolled
                Me.Controls.Remove(tabpage)
                tabpage.Dispose()
                TabPages.RemoveAt(index)
                If TabPages.Count > 0 Then
                    Do While SelectedIndex >= TabPages.Count
                        SelectedIndex -= 1
                    Loop
                    If SelectedIndex = -1 AndAlso TabPages.Count > 0 Then
                        SelectedIndex = 0
                    End If
                    SelectedIndex = SelectedIndex
                Else
                    SelectedIndex = -1
                End If
                OnResize(New EventArgs())
            End If
        End If
        Return ret
    End Function

End Class
