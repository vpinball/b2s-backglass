Imports System

Namespace B2SIllumination

    Public Class Mouse

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

        Private parent As B2SPictureBox = Nothing
        Private bulbs As B2SIllumination.BulbCollection = Nothing

        Public Property currentBulbWithMouseOver() As B2SIllumination.BulbInfo = Nothing
        Public Property currentBulb() As B2SIllumination.BulbInfo = Nothing

        Public Property IsMatchingLeft() As Boolean = False
        Public Property IsMatchingRight() As Boolean = False
        Public Property IsMatchingTop() As Boolean = False
        Public Property IsMatchingBottom() As Boolean = False

        Public Property SelectedBulb() As B2SIllumination.BulbInfo = Nothing

        Private currentBulbLocation As Point = Nothing
        Private currentBulbSize As Size = Nothing
        Private currentBulbMouseLocation As Point = Nothing

        Public Property factor() As Double = 1

        Public Sub New(ByRef _parent As B2SPictureBox, ByRef _bulbs As B2SIllumination.BulbCollection)
            parent = _parent
            bulbs = _bulbs
            AddHandler parent.MouseMove, AddressOf Parent_MouseMove
            AddHandler parent.MouseDown, AddressOf Parent_MouseDown
            AddHandler parent.MouseUp, AddressOf Parent_MouseUp
        End Sub

        Private Sub Parent_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
            If currentBulb Is Nothing Then
                parent.Cursor = CalcMouseLocation(e.X, e.Y)
            Else
                If IsMatchingLeft OrElse IsMatchingRight OrElse IsMatchingTop OrElse IsMatchingBottom Then
                    If IsMatchingRight AndAlso IsMatchingBottom Then
                        currentBulb.Size = New Size(currentBulbSize.Width - (currentBulbMouseLocation.X - e.X) / factor, currentBulbSize.Height - (currentBulbMouseLocation.Y - e.Y) / factor)
                    ElseIf IsMatchingLeft Then
                        currentBulb.Location = New Point(currentBulbLocation.X - (currentBulbMouseLocation.X - e.X) / factor, currentBulbLocation.Y)
                        currentBulb.Size = New Size(currentBulbSize.Width + (currentBulbMouseLocation.X - e.X) / factor, currentBulbSize.Height)
                    ElseIf IsMatchingRight Then
                        currentBulb.Size = New Size(currentBulbSize.Width - (currentBulbMouseLocation.X - e.X) / factor, currentBulbSize.Height)
                    ElseIf IsMatchingTop Then
                        currentBulb.Location = New Point(currentBulbLocation.X, currentBulbLocation.Y - (currentBulbMouseLocation.Y - e.Y) / factor)
                        currentBulb.Size = New Size(currentBulbSize.Width, currentBulbSize.Height + (currentBulbMouseLocation.Y - e.Y) / factor)
                    ElseIf IsMatchingBottom Then
                        currentBulb.Size = New Size(currentBulbSize.Width, currentBulbSize.Height - (currentBulbMouseLocation.Y - e.Y) / factor)
                    End If
                    If currentBulb.Size.Width < 10 OrElse currentBulb.Size.Height < 10 Then
                        currentBulb.Size = New Size(Math.Max(currentBulb.Size.Width, 10), Math.Max(currentBulb.Size.Height, 10))
                    End If
                Else
                    currentBulb.Location = New Point(currentBulbLocation.X - (currentBulbMouseLocation.X - e.X) / factor, currentBulbLocation.Y - (currentBulbMouseLocation.Y - e.Y) / factor)
                End If
                parent.Invalidate()
            End If
            ' raise event
            RaiseEvent MouseMove(Me, New MouseMoveEventArgs(CInt(e.X / factor), CInt(e.Y / factor)))
        End Sub

        Private Sub Parent_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
            parent.Cursor = CalcMouseLocation(e.X, e.Y)
            SelectedBulb = currentBulbWithMouseOver
            currentBulb = currentBulbWithMouseOver
            If currentBulb IsNot Nothing Then
                currentBulbLocation = currentBulb.Location
                currentBulbSize = currentBulb.Size
                currentBulbMouseLocation = New Point(e.X, e.Y)
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
            For Each bulb As KeyValuePair(Of String, B2SIllumination.BulbInfo) In bulbs
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
                    End If
                End With
            Next
            Return ret
        End Function

    End Class

End Namespace
