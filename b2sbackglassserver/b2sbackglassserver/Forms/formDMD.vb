Imports System
Imports System.Windows.Forms

Public Class formDMD

#Region "constructor"

    Public Sub New()

        InitializeComponent()

        ' set some styles
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.DoubleBuffered = True

    End Sub

#End Region

#Region "painting"

    Private Sub formDMD_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        If B2SData.DMDIlluminations.Count > 0 Then

            If Not B2SData.UseDMDZOrder Then

                ' draw all standard images
                For Each illu As KeyValuePair(Of String, B2SPictureBox) In B2SData.DMDIlluminations
                    If illu.Value.Visible Then
                        e.Graphics.DrawImage(illu.Value.BackgroundImage, illu.Value.RectangleF)
                    End If
                Next

            Else

                ' first of all draw zorderd images
                For Each illus As KeyValuePair(Of Integer, B2SPictureBox()) In B2SData.ZOrderDMDImages
                    For Each illu As B2SPictureBox In illus.Value
                        If illu.Visible Then
                            e.Graphics.DrawImage(illu.BackgroundImage, illu.RectangleF)
                        End If
                    Next
                Next
                ' now draw all standard images
                For Each illu As KeyValuePair(Of String, B2SPictureBox) In B2SData.DMDIlluminations
                    If illu.Value.Visible AndAlso illu.Value.ZOrder = 0 Then
                        e.Graphics.DrawImage(illu.Value.BackgroundImage, illu.Value.RectangleF)
                    End If
                Next

            End If

        End If

    End Sub

#End Region

End Class