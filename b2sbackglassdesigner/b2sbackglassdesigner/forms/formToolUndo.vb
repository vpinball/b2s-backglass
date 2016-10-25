Imports System

Public Class formToolUndo

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MyBase.SaveName = Me.Name
        MyBase.DefaultLocation = eDefaultLocation.NE

    End Sub

    Private Sub formToolUndo_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        lbHistory.ItemHeight = 18
        lbHistory.DrawMode = DrawMode.OwnerDrawFixed

    End Sub

    Private Sub lbHistory_DrawItem(sender As Object, e As System.Windows.Forms.DrawItemEventArgs) Handles lbHistory.DrawItem

        If e.Index > -1 Then
            e.DrawBackground()
            e.DrawFocusRectangle()
            Dim item As Undo.UndoEntry = TryCast(lbHistory.Items(e.Index), Undo.UndoEntry)
            e.Graphics.FillRectangle(Brushes.White, e.Bounds)
            If item IsNot Nothing Then
                If item.Image IsNot Nothing Then
                    e.Graphics.DrawImage(item.Image, New Point(1, e.Bounds.Y + 1))
                End If
                'e.Graphics.DrawString(item.ToString(), Me.Font, Brushes.Black, New Point(18, e.Bounds.Y + 2))
                TextRenderer.DrawText(e.Graphics, item.ToString(), Me.Font, New Point(18, e.Bounds.Y + 2), Color.Black)
            End If
        End If

    End Sub

End Class