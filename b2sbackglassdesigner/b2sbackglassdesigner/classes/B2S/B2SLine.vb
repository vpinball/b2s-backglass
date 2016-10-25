Imports System

Public Class B2SLine

    Inherits Control

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        ' draw the horizontal line with or without text
        Dim y As Integer = CInt(Me.Height / 2)
        If String.IsNullOrEmpty(Me.Text) Then
            e.Graphics.DrawLine(Pens.Black, 0, y, Me.Width - 1, y)
        Else
            Dim font As Font = New Font("Tahoma", 8)
            Dim size As Size = TextRenderer.MeasureText(e.Graphics, Me.Text, font)
            e.Graphics.DrawLine(Pens.Black, 0, y, 3, y)
            TextRenderer.DrawText(e.Graphics, Me.Text, font, New Point(5, y - 7), Color.Black)
            e.Graphics.DrawLine(Pens.Black, 7 + size.Width, y, Me.Width - 1, y)
            font.Dispose()
        End If
    End Sub

    Public Sub New()
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.DoubleBuffered = True

        Me.Enabled = False
    End Sub

End Class
