Imports System

Namespace Illumination

    Public Class Mouse

        Private parent As B2SPictureBox = Nothing
        Private bulbs As Illumination.BulbCollection = Nothing

        Public Event MouseMove(ByVal sender As Object, ByVal e As MouseMoveEventArgs)
        Public Class MouseMoveEventArgs
            Inherits EventArgs

            Public X As Integer
            Public Y As Integer

            Public Sub New(ByVal _x As Integer, ByVal _y As Integer)
                X = _x
                Y = _y
            End Sub
        End Class

        Public Property currentBulbWithMouseOver() As Illumination.BulbInfo = Nothing
        Public Property currentBulb() As Illumination.BulbInfo = Nothing

        Public Property IsMatchingLeft() As Boolean = False
        Public Property IsMatchingRight() As Boolean = False
        Public Property IsMatchingTop() As Boolean = False
        Public Property IsMatchingBottom() As Boolean = False

        Public Property SelectedBulb() As Illumination.BulbInfo = Nothing

        Private currentBulbStartLocation As Point = Nothing
        Private currentBulbStartSize As Size = Nothing
        Private currentBulbMouseLocation As Point = Nothing

        Public Property factor() As Double = 1

        Public Sub New(ByRef _parent As B2SPictureBox, ByRef _bulbs As Illumination.BulbCollection)
            parent = _parent
            bulbs = _bulbs
            AddHandler parent.MouseMove, AddressOf Parent_MouseMove
            AddHandler parent.MouseDown, AddressOf Parent_MouseDown
            AddHandler parent.MouseUp, AddressOf Parent_MouseUp
        End Sub

        Private Sub Parent_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
            Const minsize As Integer = 10
            If currentBulb Is Nothing Then
                parent.Cursor = CalcMouseLocation(e.X, e.Y)
            Else
                If IsMatchingLeft OrElse IsMatchingRight OrElse IsMatchingTop OrElse IsMatchingBottom Then
                    If IsMatchingRight AndAlso IsMatchingBottom Then
                        currentBulb.Size = New Size(currentBulbStartSize.Width - (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartSize.Height - (currentBulbMouseLocation.Y - e.Y) / factor)
                    ElseIf IsMatchingRight AndAlso IsMatchingTop Then
                        currentBulb.Location = New Point(currentBulbStartLocation.X, currentBulbStartLocation.Y - (currentBulbMouseLocation.Y - e.Y) / factor)
                        currentBulb.Size = New Size(currentBulbStartSize.Width - (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartSize.Height + (currentBulbMouseLocation.Y - e.Y) / factor)
                    ElseIf IsMatchingLeft AndAlso IsMatchingBottom Then
                        currentBulb.Location = New Point(currentBulbStartLocation.X - (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartLocation.Y)
                        currentBulb.Size = New Size(currentBulbStartSize.Width + (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartSize.Height - (currentBulbMouseLocation.Y - e.Y) / factor)
                    ElseIf IsMatchingLeft AndAlso IsMatchingTop Then
                        currentBulb.Location = New Point(currentBulbStartLocation.X - (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartLocation.Y - (currentBulbMouseLocation.Y - e.Y) / factor)
                        currentBulb.Size = New Size(currentBulbStartSize.Width + (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartSize.Height + (currentBulbMouseLocation.Y - e.Y) / factor)
                    ElseIf IsMatchingLeft Then
                        currentBulb.Location = New Point(currentBulbStartLocation.X - (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartLocation.Y)
                        currentBulb.Size = New Size(currentBulbStartSize.Width + (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartSize.Height)
                    ElseIf IsMatchingRight Then
                        currentBulb.Size = New Size(currentBulbStartSize.Width - (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartSize.Height)
                    ElseIf IsMatchingTop Then
                        currentBulb.Location = New Point(currentBulbStartLocation.X, currentBulbStartLocation.Y - (currentBulbMouseLocation.Y - e.Y) / factor)
                        currentBulb.Size = New Size(currentBulbStartSize.Width, currentBulbStartSize.Height + (currentBulbMouseLocation.Y - e.Y) / factor)
                    ElseIf IsMatchingBottom Then
                        currentBulb.Size = New Size(currentBulbStartSize.Width, currentBulbStartSize.Height - (currentBulbMouseLocation.Y - e.Y) / factor)
                    End If
                    If currentBulb.Location.X > currentBulbStartLocation.X + currentBulbStartSize.Width - minsize Then
                        currentBulb.Location.X = currentBulbStartLocation.X + currentBulbStartSize.Width - minsize
                    End If
                    If currentBulb.Location.Y > currentBulbStartLocation.Y + currentBulbStartSize.Height - minsize Then
                        currentBulb.Location.Y = currentBulbStartLocation.Y + currentBulbStartSize.Height - minsize
                    End If
                    If currentBulb.Size.Width < minsize OrElse currentBulb.Size.Height < minsize Then
                        currentBulb.Size = New Size(Math.Max(currentBulb.Size.Width, minsize), Math.Max(currentBulb.Size.Height, minsize))
                    End If
                Else
                    currentBulb.Location = New Point(currentBulbStartLocation.X - (currentBulbMouseLocation.X - e.X) / factor, currentBulbStartLocation.Y - (currentBulbMouseLocation.Y - e.Y) / factor)
                End If
                parent.Invalidate()
            End If
            ' raise event
            RaiseEvent MouseMove(Me, New MouseMoveEventArgs(CInt(e.X / factor), CInt(e.Y / factor)))
        End Sub

        Private Sub Parent_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                parent.Cursor = CalcMouseLocation(e.X, e.Y)
                SelectedBulb = currentBulbWithMouseOver
                currentBulb = currentBulbWithMouseOver
                If currentBulb IsNot Nothing Then
                    currentBulbStartLocation = currentBulb.Location
                    currentBulbStartSize = currentBulb.Size
                    currentBulbMouseLocation = New Point(e.X, e.Y)
                End If
            End If
        End Sub
        Private Sub Parent_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
            currentBulb = Nothing
            'SelectedBulb = currentBulbWithMouseOver

            parent.Invalidate()
        End Sub

        Private Function CalcMouseLocation(ByVal x As Integer, ByVal y As Integer) As Cursor
            Const thick As Integer = 4
            Dim ret As Cursor = Cursors.Default
            IsMatchingLeft = False
            IsMatchingRight = False
            IsMatchingTop = False
            IsMatchingBottom = False
            currentBulbWithMouseOver = Nothing
            For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In bulbs
                With bulb.Value
                    If y / factor >= .Location.Y AndAlso y / factor <= .Location.Y + .Size.Height Then
                        IsMatchingLeft = (x / factor >= .Location.X AndAlso x / factor <= .Location.X + thick)
                        IsMatchingRight = (x / factor >= .Location.X + .Size.Width - thick AndAlso x / factor <= .Location.X + .Size.Width)
                    End If
                    If x / factor >= .Location.X AndAlso x / factor <= .Location.X + .Size.Width Then
                        IsMatchingTop = (y / factor >= .Location.Y AndAlso y / factor <= .Location.Y + 4)
                        IsMatchingBottom = (y / factor >= .Location.Y + .Size.Height - thick AndAlso y / factor <= .Location.Y + .Size.Height)
                    End If
                    If IsMatchingLeft AndAlso IsMatchingTop Then
                        ret = Cursors.SizeNWSE
                    ElseIf IsMatchingLeft AndAlso IsMatchingBottom Then
                        ret = Cursors.SizeNESW
                    ElseIf IsMatchingRight AndAlso IsMatchingTop Then
                        ret = Cursors.SizeNESW
                    ElseIf IsMatchingRight AndAlso IsMatchingBottom Then
                        ret = Cursors.SizeNWSE
                    ElseIf IsMatchingLeft OrElse IsMatchingRight Then
                        ret = Cursors.SizeWE
                    ElseIf IsMatchingTop OrElse IsMatchingBottom Then
                        ret = Cursors.SizeNS
                    End If
                    If x / factor >= .Location.X AndAlso x / factor <= .Location.X + .Size.Width AndAlso y / factor >= .Location.Y AndAlso y / factor <= .Location.Y + .Size.Height Then
                        currentBulbWithMouseOver = bulb.Value
                        If ret = Cursors.Default Then ret = Cursors.Hand
                        Exit For
                    End If
                End With
            Next
            Return ret
        End Function

    End Class

End Namespace
