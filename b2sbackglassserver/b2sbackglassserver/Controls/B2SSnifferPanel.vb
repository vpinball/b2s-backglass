Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class B2SSnifferPanel

    Inherits Panel

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)

        ' create some resources
        Const offset As Integer = 30
        Dim pen As Pen = New Pen(Brushes.DarkBlue)
        Dim cellwidth As Single = (Me.Width - offset) / Columns
        Dim cellheight As Single = (Me.Height - offset) / Rows
        Dim font4background As Font = New Font("Calibri", 8, FontStyle.Bold)
        Dim font4backgroundL As Font = New Font("Calibri", 14, FontStyle.Bold)

        ' set background numbers
        Dim number As Integer = 0
        For i As Integer = 1 To Rows
            Dim y As Integer = offset + (i - 1) * cellheight
            For j As Integer = 1 To Columns
                Dim x As Integer = offset + (j - 1) * cellwidth
                TextRenderer.DrawText(e.Graphics, number.ToString(), font4backgroundL, New Rectangle(x, y, cellwidth, cellheight), Color.LightGray, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                number += 1
            Next
            If Not OnOffMode Then number = 0
        Next

        ' draw text
        TextRenderer.DrawText(e.Graphics, Me.Text, font4background, New Rectangle(0, 0, offset, offset), Color.LightGray, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

        ' set title numbers
        For i As Integer = 0 To Columns - 1
            TextRenderer.DrawText(e.Graphics, i.ToString(), font4background, New Rectangle(offset + i * cellwidth, 0, cellwidth, offset), Color.LightGray, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        Next
        For i As Integer = 0 To Rows - 1
            If i > 0 OrElse Not OnOffMode Then
                TextRenderer.DrawText(e.Graphics, If(Not OnOffMode, i + 1, i * 10).ToString(), font4background, New Rectangle(0, offset + i * cellheight, offset, cellheight), Color.LightGray, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            End If
        Next

        ' show data
        If Data IsNot Nothing AndAlso Data.Count > 0 Then
            For Each stats As KeyValuePair(Of Integer, B2SStatistics.StatsCollection) In Data
                If Me.OnOffMode Then
                    Dim row As Integer = Math.Truncate(stats.Key / Columns)
                    Dim col As Integer = stats.Key - row * Columns
                    Dim rect As Rectangle() = New Rectangle() {New Rectangle(offset + col * cellwidth + 1, offset + row * cellheight + 1, cellwidth / 2 - 1, cellheight - 1),
                                                               New Rectangle(offset + col * cellwidth + cellwidth / 2 + 1, offset + row * cellheight + 1, cellwidth / 2 - 1, cellheight - 1)}
                    If stats.Value.SetCount IsNot Nothing Then
                        For i As Integer = 0 To Math.Min(1, stats.Value.SetCount.Length - 1)
                            If i = stats.Value.CurrentState Then e.Graphics.FillRectangle(Brushes.LightGray, rect(i))
                            rect(i).Height = rect(i).Height / 5
                            If i = stats.Value.CurrentState Then e.Graphics.FillRectangle(Brushes.Gray, rect(i))
                            TextRenderer.DrawText(e.Graphics, If(i = 0, "Off", "On"), font4background, rect(i), pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            rect(i).Offset(0, cellheight / 5)
                            TextRenderer.DrawText(e.Graphics, stats.Value.SetCount(i).ToString(), font4background, rect(i), pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            If stats.Value.AvgOn(i) <> Nothing Then
                                rect(i).Offset(0, cellheight / 5)
                                Dim ms As Integer = stats.Value.AvgOn(i).TotalMilliseconds
                                TextRenderer.DrawText(e.Graphics, If(ms >= 100, Format(ms / 1000, "0.00"), ms).ToString() + If(ms >= 100, "s", "ms"), font4background, rect(i), pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            End If
                            If stats.Value.MaxOn(i) <> Nothing Then
                                rect(i).Offset(0, cellheight / 5)
                                Dim ms As Integer = stats.Value.MaxOn(i).TotalMilliseconds
                                TextRenderer.DrawText(e.Graphics, If(ms >= 100, Format(ms / 1000, "0.00"), ms).ToString() + If(ms >= 100, "s", "ms"), font4background, rect(i), pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            End If
                            If stats.Value.MinOn(i) <> Nothing Then
                                rect(i).Offset(0, cellheight / 5)
                                Dim ms As Integer = stats.Value.MinOn(i).TotalMilliseconds
                                TextRenderer.DrawText(e.Graphics, If(ms >= 100, Format(ms / 1000, "0.00"), ms).ToString() + If(ms >= 100, "s", "ms"), font4background, rect(i), pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            End If
                        Next
                    End If
                Else
                    Dim row As Integer = stats.Key
                    If stats.Value.SetCount IsNot Nothing Then
                        For i As Integer = 0 To Math.Min(8, stats.Value.SetCount.Length - 1)
                            Dim rect As Rectangle = New Rectangle(offset + i * cellwidth + 1, offset + row * cellheight + 1, cellwidth - 1, cellheight - 1)
                            If i = stats.Value.CurrentState Then e.Graphics.FillRectangle(Brushes.LightGray, rect)
                            rect.Height = Math.Truncate(rect.Height / 5)
                            If i = stats.Value.CurrentState Then e.Graphics.FillRectangle(Brushes.Gray, rect)
                            rect.Offset(0, Math.Truncate(cellheight / 5))
                            TextRenderer.DrawText(e.Graphics, stats.Value.SetCount(i).ToString(), font4background, rect, pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            If stats.Value.AvgOn(i) <> Nothing Then
                                rect.Offset(0, Math.Truncate(cellheight / 5))
                                Dim ms As Integer = stats.Value.AvgOn(i).TotalMilliseconds
                                TextRenderer.DrawText(e.Graphics, If(ms >= 100, Format(ms / 1000, "0.00"), ms).ToString() + If(ms >= 100, "s", "ms"), font4background, rect, pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            End If
                            If stats.Value.MaxOn(i) <> Nothing Then
                                rect.Offset(0, Math.Truncate(cellheight / 5))
                                Dim ms As Integer = stats.Value.MaxOn(i).TotalMilliseconds
                                TextRenderer.DrawText(e.Graphics, If(ms >= 100, Format(ms / 1000, "0.00"), ms).ToString() + If(ms >= 100, "s", "ms"), font4background, rect, pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            End If
                            If stats.Value.MinOn(i) <> Nothing Then
                                rect.Offset(0, Math.Truncate(cellheight / 5))
                                Dim ms As Integer = stats.Value.MinOn(i).TotalMilliseconds
                                TextRenderer.DrawText(e.Graphics, If(ms >= 100, Format(ms / 1000, "0.00"), ms).ToString() + If(ms >= 100, "s", "ms"), font4background, rect, pen.Color, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
                            End If
                        Next
                    End If
                End If
            Next
        End If

        ' draw border frame
        e.Graphics.DrawRectangle(pen, New Rectangle(offset, offset, Me.Width - offset - 1, Me.Height - offset - 1))

        ' draw grid lines
        For i As Integer = 1 To Rows - 1
            e.Graphics.DrawLine(pen, offset, offset + CInt(i * cellheight), Me.Width - 1, offset + CInt(i * cellheight))
        Next
        For i As Integer = 1 To Columns - 1
            e.Graphics.DrawLine(pen, offset + CInt(i * cellwidth), offset, offset + CInt(i * cellwidth), Me.Height - 1)
        Next

        ' free up some resources
        font4backgroundL.Dispose()
        font4background.Dispose()
        pen.Dispose()

    End Sub

    Public Property Rows() As Integer = 10
    Public Property Columns() As Integer = 10
    Public Property OnOffMode() As Boolean = True

    Public Property Data() As Generic.SortedList(Of Integer, B2SStatistics.StatsCollection) = Nothing

    Public Sub New()

        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)

    End Sub

    Public Shadows Sub Invalidate()
        If Data IsNot Nothing AndAlso Data.Count > 0 AndAlso Data.Keys(Data.Count - 1) > Rows * Columns Then
            Rows = Math.Truncate(Data.Keys(Data.Count - 1) / Columns) + 1
        End If
        MyBase.Invalidate()
    End Sub

End Class
