Imports System
Imports System.Drawing
Imports System.Text

Public Class B2SColorBar

    Inherits Control

    Public Event ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Shadows Event TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Const indent As Integer = 5
    Private Const numericWidth As Integer = 50
    Private Const trackerHeight As Integer = 13

    Private WithEvents numericRed As NumericUpDown = Nothing
    Private WithEvents numericGreen As NumericUpDown = Nothing
    Private WithEvents numericBlue As NumericUpDown = Nothing
    Private WithEvents txtHex As TextBox = Nothing

    Private trackerInfoRed As TrackerInfo = New TrackerInfo()
    Private trackerInfoGreen As TrackerInfo = New TrackerInfo()
    Private trackerInfoBlue As TrackerInfo = New TrackerInfo()

    Private mouseDownRed As Boolean = False
    Private mouseDownGreen As Boolean = False
    Private mouseDownBlue As Boolean = False

    Private mouseDownX As Integer = 0
    Private mouseDownTrackerX As Single = 0

    Private Class TrackerInfo
        Public Rect As RectangleF = New RectangleF(New PointF(0, 0), New SizeF(My.Resources.arrow_up_orange.Width, My.Resources.arrow_up_orange.Height))
    End Class

    Public Sub New()
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.DoubleBuffered = True
        ' create controls
        numericRed = New NumericUpDown
        Me.Controls.Add(numericRed)
        numericGreen = New NumericUpDown
        Me.Controls.Add(numericGreen)
        numericBlue = New NumericUpDown
        Me.Controls.Add(numericBlue)
        txtHex = New TextBox()
        With txtHex
            .Width = 2 * numericWidth
            .TextAlign = HorizontalAlignment.Right
            .TabIndex = 3
            Me.Controls.Add(txtHex)
            .BringToFront()
        End With
        Dim i As Integer = 0
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is NumericUpDown Then
                With DirectCast(ctrl, NumericUpDown)
                    .Width = numericWidth
                    .TextAlign = HorizontalAlignment.Right
                    .Minimum = 0
                    .Maximum = 255
                    .TabIndex = i
                    .BringToFront()
                End With
                i += 1
            End If
        Next
    End Sub

    Private _currentColor As Color = Color.Black
    Public Property CurrentColor() As Color
        Get
            Return _currentColor
        End Get
        Set(ByVal value As Color)
            If _currentColor <> value Then
                _currentColor = value
                numericRed.Value = value.R
                numericGreen.Value = value.G
                numericBlue.Value = value.B
                Me.Invalidate()
                RaiseEvent ColorChanged(Me, New EventArgs())
            End If
        End Set
    End Property

    Protected Overrides Sub OnResize(e As System.EventArgs)
        Dim space As Integer = (Me.Height - 3 * numericRed.Height - txtHex.Height) / 3
        numericRed.Location = New Point(Me.Width - 1 - numericRed.Width, 0)
        numericGreen.Location = New Point(Me.Width - 1 - numericGreen.Width, numericRed.Height + space)
        numericBlue.Location = New Point(Me.Width - 1 - numericBlue.Width, numericRed.Height + numericGreen.Height + 2 * space)
        txtHex.Location = New Point(Me.Width - 1 - txtHex.Width, numericRed.Height + numericGreen.Height + numericBlue.Height + 3 * space)
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        If numericRed Is Nothing Then Exit Sub
        Dim unit As Single = (Me.Width - numericWidth - 10 - indent * 2) / 256
        Dim trackerWidth As Single = My.Resources.arrow_up_orange.Width / 2
        Dim R As Integer = CurrentColor.R
        Dim G As Integer = CurrentColor.G
        Dim B As Integer = CurrentColor.B
        ' paint red track bar
        For i As Integer = 0 To 255
            Dim color As Color = color.FromArgb(i, CurrentColor.G, CurrentColor.B)
            Dim rect As RectangleF = New RectangleF(3 * indent + i * unit, numericRed.Location.Y + (numericRed.Height - trackerHeight) / 2, unit, trackerHeight)
            Dim brush As SolidBrush = New SolidBrush(color)
            e.Graphics.FillRectangle(brush, rect)
            brush.Dispose()
        Next
        ' draw red tracker
        trackerInfoRed.Rect.X = 3 * indent - trackerWidth + unit * R
        trackerInfoRed.Rect.Y = numericRed.Location.Y + 14
        e.Graphics.DrawImage(My.Resources.arrow_up_orange, trackerInfoRed.Rect.Location)
        ' paint green track bar
        For i As Integer = 0 To 255
            Dim color As Color = color.FromArgb(CurrentColor.R, i, CurrentColor.B)
            Dim rect As RectangleF = New RectangleF(3 * indent + i * unit, numericGreen.Location.Y + (numericGreen.Height - trackerHeight) / 2, unit, trackerHeight)
            Dim brush As SolidBrush = New SolidBrush(color)
            e.Graphics.FillRectangle(brush, rect)
            brush.Dispose()
        Next
        ' draw green tracker
        trackerInfoGreen.Rect.X = 3 * indent - trackerWidth + unit * G
        trackerInfoGreen.Rect.Y = numericGreen.Location.Y + 14
        e.Graphics.DrawImage(My.Resources.arrow_up_orange, trackerInfoGreen.Rect.Location)
        ' paint blue track bar
        For i As Integer = 0 To 255
            Dim color As Color = color.FromArgb(CurrentColor.R, CurrentColor.G, i)
            Dim rect As RectangleF = New RectangleF(3 * indent + i * unit, numericBlue.Location.Y + (numericBlue.Height - trackerHeight) / 2, unit, trackerHeight)
            Dim brush As SolidBrush = New SolidBrush(color)
            e.Graphics.FillRectangle(brush, rect)
            brush.Dispose()
        Next
        ' draw blue tracker
        trackerInfoBlue.Rect.X = 3 * indent - trackerWidth + unit * B
        trackerInfoBlue.Rect.Y = numericBlue.Location.Y + 14
        e.Graphics.DrawImage(My.Resources.arrow_up_orange, trackerInfoBlue.Rect.Location)
        ' render letters
        e.Graphics.DrawString("R:", Me.Font, Brushes.Black, New Point(0, trackerInfoRed.Rect.Y - 11))
        e.Graphics.DrawString("G:", Me.Font, Brushes.Black, New Point(0, trackerInfoGreen.Rect.Y - 11))
        e.Graphics.DrawString("B:", Me.Font, Brushes.Black, New Point(0, trackerInfoBlue.Rect.Y - 11))
        e.Graphics.DrawString("Hex:", Me.Font, Brushes.Black, New Point(0, txtHex.Location.Y + 4))
    End Sub

    Private Sub B2SColorBar_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If trackerInfoRed.Rect.Contains(e.X, e.Y) Then
            Cursor.Current = Cursors.Hand
            mouseDownRed = True
            mouseDownX = e.X
            mouseDownTrackerX = trackerInfoRed.Rect.X
        ElseIf trackerInfoGreen.Rect.Contains(e.X, e.Y) Then
            Cursor.Current = Cursors.Hand
            mouseDownGreen = True
            mouseDownX = e.X
            mouseDownTrackerX = trackerInfoGreen.Rect.X
        ElseIf trackerInfoBlue.Rect.Contains(e.X, e.Y) Then
            Cursor.Current = Cursors.Hand
            mouseDownBlue = True
            mouseDownX = e.X
            mouseDownTrackerX = trackerInfoBlue.Rect.X
        Else
            Dim rectRed As RectangleF = New RectangleF(3 * indent, numericRed.Location.Y + (numericRed.Height - trackerHeight) / 2, Me.Width - numericWidth - 10 - indent * 2, trackerHeight)
            Dim rectGreen As RectangleF = New RectangleF(3 * indent, numericGreen.Location.Y + (numericGreen.Height - trackerHeight) / 2, Me.Width - numericWidth - 10 - indent * 2, trackerHeight)
            Dim rectBlue As RectangleF = New RectangleF(3 * indent, numericBlue.Location.Y + (numericBlue.Height - trackerHeight) / 2, Me.Width - numericWidth - 10 - indent * 2, trackerHeight)
            If rectRed.Contains(e.X, e.Y) Then
                Dim unit As Single = (Me.Width - numericWidth - 10 - indent * 2) / 256
                trackerInfoRed.Rect.X = (e.X - 2 * indent) / unit
                If trackerInfoRed.Rect.X < 0 Then trackerInfoRed.Rect.X = 0
                If trackerInfoRed.Rect.X > 255 Then trackerInfoRed.Rect.X = 255
                numericRed.Value = trackerInfoRed.Rect.X
                CurrentColor = Color.FromArgb(trackerInfoRed.Rect.X, CurrentColor.G, CurrentColor.B)
            ElseIf rectGreen.Contains(e.X, e.Y) Then
                Dim unit As Single = (Me.Width - numericWidth - 10 - indent * 2) / 256
                trackerInfoGreen.Rect.X = (e.X - 2 * indent) / unit
                If trackerInfoGreen.Rect.X < 0 Then trackerInfoGreen.Rect.X = 0
                If trackerInfoGreen.Rect.X > 255 Then trackerInfoGreen.Rect.X = 255
                numericGreen.Value = trackerInfoGreen.Rect.X
                CurrentColor = Color.FromArgb(CurrentColor.R, trackerInfoGreen.Rect.X, CurrentColor.B)
            ElseIf rectBlue.Contains(e.X, e.Y) Then
                Dim unit As Single = (Me.Width - numericWidth - 10 - indent * 2) / 256
                trackerInfoBlue.Rect.X = (e.X - 2 * indent) / unit
                If trackerInfoBlue.Rect.X < 0 Then trackerInfoBlue.Rect.X = 0
                If trackerInfoBlue.Rect.X > 255 Then trackerInfoBlue.Rect.X = 255
                numericBlue.Value = trackerInfoBlue.Rect.X
                CurrentColor = Color.FromArgb(CurrentColor.R, CurrentColor.G, trackerInfoBlue.Rect.X)
            End If
        End If
    End Sub
    Private Sub B2SColorBar_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If mouseDownRed OrElse mouseDownGreen OrElse mouseDownBlue Then
            Dim unit As Single = (Me.Width - numericWidth - 10 - indent * 2) / 256
            If mouseDownRed Then
                trackerInfoRed.Rect.X = (mouseDownTrackerX + e.X - mouseDownX - 2 * indent) / unit
                If trackerInfoRed.Rect.X < 0 Then trackerInfoRed.Rect.X = 0
                If trackerInfoRed.Rect.X > 255 Then trackerInfoRed.Rect.X = 255
                numericRed.Value = trackerInfoRed.Rect.X
                CurrentColor = Color.FromArgb(trackerInfoRed.Rect.X, CurrentColor.G, CurrentColor.B)
            ElseIf mouseDownGreen Then
                trackerInfoGreen.Rect.X = (mouseDownTrackerX + e.X - mouseDownX - 2 * indent) / unit
                If trackerInfoGreen.Rect.X < 0 Then trackerInfoGreen.Rect.X = 0
                If trackerInfoGreen.Rect.X > 255 Then trackerInfoGreen.Rect.X = 255
                numericGreen.Value = trackerInfoGreen.Rect.X
                CurrentColor = Color.FromArgb(CurrentColor.R, trackerInfoGreen.Rect.X, CurrentColor.B)
            ElseIf mouseDownBlue Then
                trackerInfoBlue.Rect.X = (mouseDownTrackerX + e.X - mouseDownX - 2 * indent) / unit
                If trackerInfoBlue.Rect.X < 0 Then trackerInfoBlue.Rect.X = 0
                If trackerInfoBlue.Rect.X > 255 Then trackerInfoBlue.Rect.X = 255
                numericBlue.Value = trackerInfoBlue.Rect.X
                CurrentColor = Color.FromArgb(CurrentColor.R, CurrentColor.G, trackerInfoBlue.Rect.X)
            End If
            Me.Invalidate()
            RaiseEvent ColorChanged(Me, New System.EventArgs())
        ElseIf trackerInfoRed.Rect.Contains(e.X, e.Y) OrElse trackerInfoGreen.Rect.Contains(e.X, e.Y) OrElse trackerInfoBlue.Rect.Contains(e.X, e.Y) Then
            Cursor.Current = Cursors.Hand
        End If
    End Sub
    Private Sub B2SColorBar_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        mouseDownRed = False
        mouseDownGreen = False
        mouseDownBlue = False
    End Sub
    Private Sub B2SColorBar_MouseLeave(sender As Object, e As System.EventArgs) Handles Me.MouseLeave
        mouseDownRed = False
        mouseDownGreen = False
        mouseDownBlue = False
    End Sub

    Private stopEvents As Boolean = False

    Private Sub Red_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles numericRed.KeyUp
        If Not IsNumeric(numericRed.Value) Then numericRed.Value = 0
        Red_ValueChanged(Me, New EventArgs())
    End Sub
    Private Sub Red_ValueChanged(sender As Object, e As System.EventArgs) Handles numericRed.ValueChanged
        If stopEvents Then Exit Sub
        trackerInfoRed.Rect.X = numericRed.Value
        CurrentColor = Color.FromArgb(trackerInfoRed.Rect.X, CurrentColor.G, CurrentColor.B)
        RGB2Hex()
        Me.Invalidate()
        RaiseEvent ColorChanged(Me, New System.EventArgs())
    End Sub
    Private Sub Green_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles numericGreen.KeyUp
        If Not IsNumeric(numericGreen.Value) Then numericGreen.Value = 0
        Green_ValueChanged(Me, New EventArgs())
    End Sub
    Private Sub Green_ValueChanged(sender As Object, e As System.EventArgs) Handles numericGreen.ValueChanged
        If stopEvents Then Exit Sub
        trackerInfoGreen.Rect.X = numericGreen.Value
        CurrentColor = Color.FromArgb(CurrentColor.R, trackerInfoGreen.Rect.X, CurrentColor.B)
        RGB2Hex()
        Me.Invalidate()
        RaiseEvent ColorChanged(Me, New System.EventArgs())
    End Sub
    Private Sub Blue_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles numericBlue.KeyUp
        If Not IsNumeric(numericBlue.Value) Then numericBlue.Value = 0
        Blue_ValueChanged(Me, New EventArgs())
    End Sub
    Private Sub Blue_ValueChanged(sender As Object, e As System.EventArgs) Handles numericBlue.ValueChanged
        If stopEvents Then Exit Sub
        trackerInfoBlue.Rect.X = numericBlue.Value
        CurrentColor = Color.FromArgb(CurrentColor.R, CurrentColor.G, trackerInfoBlue.Rect.X)
        RGB2Hex()
        Me.Invalidate()
        RaiseEvent ColorChanged(Me, New System.EventArgs())
    End Sub
    Private Sub Hex_TextChanged(sender As Object, e As System.EventArgs) Handles txtHex.TextChanged
        If stopEvents Then Exit Sub
        On Error Resume Next
        Dim hex As String = "000000" & txtHex.Text.Trim
        hex = hex.Substring(hex.Length - 6, 6)
        stopEvents = True
        numericRed.Value = Convert.ToInt32(hex.Substring(0, 2), 16)
        numericGreen.Value = Convert.ToInt32(hex.Substring(2, 2), 16)
        numericBlue.Value = Convert.ToInt32(hex.Substring(4, 2), 16)
        CurrentColor = Color.FromArgb(numericRed.Value, numericGreen.Value, numericBlue.Value)
        RaiseEvent TextChanged(Me, New EventArgs())
        stopEvents = False
    End Sub

    Private Sub RGB2Hex()
        stopEvents = True
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append("0")
        sb.Append(Hex(CurrentColor.R))
        If sb.Length > 2 Then sb.Remove(0, 1)
        sb.Append("0")
        sb.Append(Hex(CurrentColor.G))
        If sb.Length > 4 Then sb.Remove(2, 1)
        sb.Append("0")
        sb.Append(Hex(CurrentColor.B))
        If sb.Length > 6 Then sb.Remove(4, 1)
        txtHex.Text = sb.ToString()
        stopEvents = False
    End Sub

End Class
