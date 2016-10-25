Imports System
Imports System.Drawing.Drawing2D

Public Class B2STab

    Inherits ContainerControl

    Public Event SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event IlluMouseMove(ByVal sender As Object, ByVal e As B2SIllumination.Mouse.MouseMoveEventArgs)

    Private Const tabheight As Integer = 25

    Private fontstd As Font = New Font("Segoe UI", 9, FontStyle.Regular)
    Private fontbold As Font = New Font("Segoe UI", 9, FontStyle.Bold)

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        ' draw background
        Dim brush As LinearGradientBrush = New LinearGradientBrush(e.ClipRectangle, Color.FromKnownColor(KnownColor.InactiveCaption), Color.FromKnownColor(KnownColor.GradientInactiveCaption), LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(brush, e.ClipRectangle)
        brush.Dispose()
        ' draw tabs
        If Backglasses.Count > 0 Then
            Dim i As Integer = 0
            Dim x As Integer = 3
            Dim y As Integer = Me.Height - tabheight + 3
            e.Graphics.DrawLine(Pens.Black, 0, y, x, y)
            For Each backglass As B2STabPage In Backglasses
                With backglass
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
                        TextRenderer.DrawText(e.Graphics, .Text, If(i = SelectedIndex, fontbold, fontstd), New Point(x + 6 + thumbnailwidth + 1, y + 1), Color.White)
                    End If
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

    Private Sub B2STab_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        Dim I As Integer = 0
        For Each backglass As B2STabPage In Backglasses
            With backglass
                If e.X >= .TabLocation.X AndAlso e.X <= .TabLocation.X + .TabSize.Width AndAlso e.Y >= .TabLocation.Y AndAlso e.Y <= .TabLocation.Y + .TabSize.Height Then
                    SelectedIndex = I
                    Exit For
                End If
            End With
            I += 1
        Next
    End Sub
    Private Sub B2STab_Scrolled(sender As Object, e As B2STabPage.B2STabPageScrollEventArgs)
        _offsetlocation = e.Location
    End Sub

    Private Sub B2STabPage_MouseMove(ByVal sender As Object, ByVal e As B2SIllumination.Mouse.MouseMoveEventArgs)
        RaiseEvent IlluMouseMove(sender, e)
    End Sub

    Private _selectedIndex As Integer = -1
    Public Property SelectedIndex() As Integer
        Get
            Return _selectedIndex
        End Get
        Set(ByVal value As Integer)
            If value >= 0 AndAlso value < Backglasses.Count Then
                _selectedIndex = value
                For Each backglass As B2STabPage In Backglasses
                    backglass.Visible = False
                Next
                Backglasses(_selectedIndex).Visible = True
                Me.Invalidate()
                RaiseEvent SelectedIndexChanged(Me, New EventArgs())
            End If
        End Set
    End Property
    Public ReadOnly Property SelectedBackglass() As B2STabPage
        Get
            Return If(SelectedIndex > -1, Backglasses(SelectedIndex), Nothing)
        End Get
    End Property

    Private _backglasses As Generic.List(Of B2STabPage) = New Generic.List(Of B2STabPage)
    Public ReadOnly Property Backglasses() As Generic.List(Of B2STabPage)
        Get
            Return _backglasses
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

    Public Sub AddBackglass(ByVal backglass As B2STabPage)
        backglass.Location = New Point(0, 0)
        backglass.Dock = DockStyle.Fill
        backglass.Visible = False
        AddHandler backglass.Scrolled, AddressOf B2STab_Scrolled
        AddHandler backglass.Mouse.MouseMove, AddressOf B2STabPage_MouseMove
        Backglasses.Add(backglass)
        Me.Controls.Add(backglass)
        OnResize(New EventArgs())
    End Sub
    Public Sub RemoveBackglass(ByVal index As Integer)
        If Backglasses.Count > index Then
            Dim backglass As B2STabPage = Backglasses(index)
            Dim ret As DialogResult = DialogResult.No
            If backglass.BackglassData.IsDirty Then
                ret = MessageBox.Show(My.Resources.MSG_IsDirty, AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If ret = DialogResult.Yes Then
                    Dim helper As Helper = New Helper()
                    helper.SaveData(backglass.BackglassData)
                End If
            End If
            If ret = DialogResult.No Then
                RemoveHandler backglass.Scrolled, AddressOf B2STab_Scrolled
                Me.Controls.Remove(backglass)
                backglass.Dispose()
                Backglasses.RemoveAt(index)
                If Backglasses.Count > 0 Then
                    Do While SelectedIndex >= Backglasses.Count
                        SelectedIndex -= 1
                    Loop
                    If SelectedIndex = -1 AndAlso Backglasses.Count > 0 Then
                        SelectedIndex = 0
                    End If
                    SelectedIndex = SelectedIndex
                End If
                OnResize(New EventArgs())
            End If
        End If
    End Sub

End Class
