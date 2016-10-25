Imports System

Public Class Mouse

    Private parent As B2SPictureBox = Nothing
    Private contextMenuItems As ContextMenuStrip = Nothing

    Public Event MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseMove(ByVal sender As Object, ByVal e As MouseMoveEventArgs)
    Public Event CopyDMDCopyArea(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectedItemClicked(ByVal sender As Object, ByVal e As MouseMoveEventArgs)
    Public Event SelectedItemMoving(ByVal sender As Object, ByVal e As MouseMoveEventArgs)
    Public Event SelectedItemRemoved(ByVal sender As Object, ByVal e As EventArgs)
    Public Event SelectedBulbMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event SelectedBulbEdited(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Class MouseMoveEventArgs
        Inherits EventArgs

        Public Enum eItemType
            Undefined = 0
            Score = 1
            Bulb = 2
        End Enum

        Public ItemType As eItemType = eItemType.Undefined
        Public Location As Point = Nothing
        Public Size As Size = Nothing

        Public Sub New(ByVal _location As Point)
            Location = _location
        End Sub
        Public Sub New(ByVal _itemtype As eItemType, ByVal _location As Point, ByVal _size As Size)
            ItemType = _itemtype
            Location = _location
            Size = _size
        End Sub
    End Class

    Private Property currentBulb() As Illumination.BulbInfo = Nothing
    Private Property currentScore() As ReelAndLED.ScoreInfo = Nothing
    Private Property currentDMDCopyArea() As InfoBase = Nothing

    Public ReadOnly Property SelectedItem() As InfoBase
        Get
            Return If(SelectedBulb IsNot Nothing, SelectedBulb, If(SelectedScore IsNot Nothing, SelectedScore, Nothing))
        End Get
    End Property
    Private _selectedScore As ReelAndLED.ScoreInfo
    Public Property SelectedScore() As ReelAndLED.ScoreInfo
        Get
            Return _selectedScore
        End Get
        Set(ByVal value As ReelAndLED.ScoreInfo)
            If _selectedScore IsNot value Then
                _selectedScore = value
                If value IsNot Nothing Then
                    RaiseEvent SelectedItemClicked(Me, New MouseMoveEventArgs(MouseMoveEventArgs.eItemType.Score, value.Location, value.Size))
                End If
            End If
        End Set
    End Property
    Private _selectedBulb As Illumination.BulbInfo
    Public Property SelectedBulb() As Illumination.BulbInfo
        Get
            Return _selectedBulb
        End Get
        Set(ByVal value As Illumination.BulbInfo)
            If _selectedBulb IsNot value Then
                _selectedBulb = value
                If value IsNot Nothing Then
                    RaiseEvent SelectedItemClicked(Me, New MouseMoveEventArgs(MouseMoveEventArgs.eItemType.Bulb, value.Location, value.Size))
                End If
            End If
        End Set
    End Property

    Public Property PreviewedScore() As ReelAndLED.ScoreInfo
    Public Property PreviewedBulb() As Illumination.BulbInfo

    Public Property ShowScoreFrames() As Boolean = False
    Public Property ShowIlluFrames() As Boolean = False

    Public Property SetGrillHeight() As Boolean = False
    Public Property SetSmallGrillHeight() As Boolean = False
    Public Property CopyDMDImageFromBackglass() As Boolean = False
    Public Property SetDMDLocation() As Boolean = False

    Public Property RomInfoFilter() As String

    Public Property LastScoreSize() As Size = Nothing
    Public Property LastScoreDigits() As Integer = Nothing
    Public Property LastScoreSpacing() As Integer = Nothing
    Public Property LastScoreReelType() As String = Nothing
    Public Property LastScoreReelColor() As Color = Nothing
    Public Property LastBulbSize() As Size = Nothing
    Public Property LastBulbIntensity() As Integer = Nothing
    Public Property LastBulbLightColor() As Color = Nothing
    Public Property LastBulbDodgeColor() As Color = Nothing
    Public Property LastBulbInitialState() As Integer = Nothing
    Public Property LastBulbFont() As Font = Nothing

    Private IsMatchingLeft As Boolean = False
    Private IsMatchingRight As Boolean = False
    Private IsMatchingTop As Boolean = False
    Private IsMatchingBottom As Boolean = False
    Private IsMatchingX As Boolean = False
    Private IsMatchingGrillHeightX As Boolean = False
    Private IsMatchingSmallGrillHeightX As Boolean = False
    Private IsMatchingDMDLocationX As Boolean = False

    Private currentStartLocation As Point = Nothing
    Private currentStartSize As Size = Nothing
    Private currentMouseLocation As Point = Nothing

    Private moveStartLocation As Point = Nothing
    Private moveParentLocation As Point = Nothing

    Public Property factor() As Double = 1

    Public ReadOnly Property IsMouseOverGrillRemover() As Boolean
        Get
            Return IsMatchingGrillHeightX
        End Get
    End Property
    Public ReadOnly Property IsMouseOverSmallGrillRemover() As Boolean
        Get
            Return IsMatchingSmallGrillHeightX
        End Get
    End Property
    Public ReadOnly Property IsMouseOverDMDLocationRemover() As Boolean
        Get
            Return IsMatchingDMDLocationX
        End Get
    End Property

    Private ReadOnly Property scores() As ReelAndLED.ScoreCollection
        Get
            Return If(Backglass.currentData IsNot Nothing, If(Backglass.currentData.IsDMDImageShown, Backglass.currentData.DMDScores, Backglass.currentData.Scores), Nothing)
        End Get
    End Property
    Private ReadOnly Property bulbs() As Illumination.BulbCollection
        Get
            Return If(Backglass.currentData IsNot Nothing, If(Backglass.currentData.IsDMDImageShown, Backglass.currentData.DMDBulbs, Backglass.currentData.Bulbs), Nothing)
        End Get
    End Property
    Private ReadOnly Property dmdcopyimage() As InfoBase
        Get
            Return If(Backglass.currentData IsNot Nothing, If(Backglass.currentData.IsDMDImageShown, Nothing, Backglass.currentData.DMDCopyArea), Nothing)
        End Get
    End Property

    Public Sub New(ByRef _parent As B2SPictureBox)
        parent = _parent
        AddHandler parent.MouseMove, AddressOf Parent_MouseMove
        AddHandler parent.MouseDown, AddressOf Parent_MouseDown
        AddHandler parent.MouseUp, AddressOf Parent_MouseUp
    End Sub

    Public Sub KeyIsPressed(ByVal ctrl As Boolean, ByVal keycode As Keys)
        If SelectedItem IsNot Nothing Then
            With SelectedItem
                Dim currentLocation As Point = .Location
                Dim currentSize As Size = .Size
                Select Case keycode
                    Case Keys.Delete
                        If TypeOf SelectedItem Is Illumination.BulbInfo Then
                            Undo.AddEntry(New Undo.UndoEntry(Undo.Type.BulbRemoved, SelectedItem))
                            bulbs.Remove(SelectedItem)
                            SelectedBulb = Nothing
                        Else
                            Undo.AddEntry(New Undo.UndoEntry(Undo.Type.ScoreRemoved, SelectedItem))
                            scores.Remove(SelectedItem)
                            SelectedScore = Nothing
                        End If
                        RaiseEvent SelectedItemRemoved(Me, New EventArgs())
                    Case Keys.Up
                        If .Location.Y > .Size.Height / 2 * (-1) Then
                            If ctrl Then
                                .Size.Height -= 1
                            Else
                                .Location.Y -= 1
                            End If
                        End If
                    Case Keys.Down
                        If .Location.Y < Backglass.currentData.Image.Size.Height - .Size.Height / 2 Then
                            If ctrl Then
                                .Size.Height += 1
                            Else
                                .Location.Y += 1
                            End If
                        End If
                    Case Keys.Left
                        If .Location.X > .Size.Width / 2 * (-1) Then
                            If ctrl Then
                                .Size.Width -= 1
                            Else
                                .Location.X -= 1
                            End If
                        End If
                    Case Keys.Right
                        If .Location.X < Backglass.currentData.Image.Size.Width - .Size.Width / 2 Then
                            If ctrl Then
                                .Size.Width += 1
                            Else
                                .Location.X += 1
                            End If
                        End If
                    Case Else
                        If SelectedBulb IsNot Nothing Then
                            If keycode >= Keys.D1 AndAlso keycode <= Keys.D5 Then
                                SelectedBulb.Intensity = keycode - Keys.D0
                                RaiseEvent SelectedBulbEdited(Me, New EventArgs())
                            Else
                                Dim index As Integer = -1
                                Select Case keycode
                                    Case Keys.O
                                        index = 0
                                    Case Keys.R
                                        index = 1
                                    Case Keys.G
                                        index = 2
                                    Case Keys.B
                                        index = 3
                                    Case Keys.Y
                                        index = 4
                                    Case Keys.M
                                        index = 5
                                    Case Keys.P
                                        index = 6
                                    Case Keys.W
                                        index = 7
                                End Select
                                If index >= 0 Then
                                    If TranslateDodgeColor2Index(SelectedBulb.DodgeColor) = index Then SelectedBulb.DodgeColor = Nothing Else SelectedBulb.DodgeColor = TranslateIndex2DodgeColor(index)
                                    RaiseEvent SelectedBulbEdited(Me, New EventArgs())
                                End If
                            End If
                        End If
                End Select
                If keycode = Keys.Up OrElse keycode = Keys.Down OrElse keycode = Keys.Left OrElse keycode = Keys.Right Then
                    ' maybe store bulb or score settings
                    StoreCurrentSettings(SelectedItem)
                    ' undo entry
                    Undo.AddEntry(New Undo.UndoEntry(Undo.Type.BulbOrScoreMoved, SelectedItem, currentLocation, currentSize, .Location, .Size))
                    RaiseEvent SelectedItemMoving(Me, New MouseMoveEventArgs(If(SelectedBulb IsNot Nothing, MouseMoveEventArgs.eItemType.Bulb, MouseMoveEventArgs.eItemType.Score), .Location, .Size))
                End If
            End With
            parent.Refresh()
        End If
    End Sub

    Private Sub Parent_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Const minsize As Integer = 10
        If currentBulb Is Nothing AndAlso currentScore Is Nothing AndAlso currentDMDCopyArea Is Nothing Then
            If ShowIlluFrames OrElse ShowScoreFrames OrElse SetGrillHeight OrElse SetSmallGrillHeight OrElse CopyDMDImageFromBackglass OrElse SetDMDLocation Then
                parent.Cursor = CalcMouseLocation(e.X, e.Y)
                'ElseIf moveStartLocation <> Nothing Then
                '    Dim loc As Point = moveParentLocation - moveStartLocation + New Point(e.X, e.Y)
                '    If Not loc.Equals(parent.Location) Then
                '        parent.Location = loc
                '        'parent.Refresh()
                '    End If
            End If
        Else
            Backglass.currentData.IsDirty = True
            Dim current As InfoBase = If(currentBulb IsNot Nothing, currentBulb, If(currentScore IsNot Nothing, currentScore, currentDMDCopyArea))
            If IsMatchingLeft OrElse IsMatchingRight OrElse IsMatchingTop OrElse IsMatchingBottom Then
                If IsMatchingLeft AndAlso IsMatchingTop Then
                    current.Location = New Point(currentStartLocation.X - (currentMouseLocation.X - e.X) / factor, currentStartLocation.Y - (currentMouseLocation.Y - e.Y) / factor)
                    current.Size = New Size(currentStartSize.Width + (currentMouseLocation.X - e.X) / factor, currentStartSize.Height + (currentMouseLocation.Y - e.Y) / factor)
                ElseIf IsMatchingLeft AndAlso IsMatchingBottom Then
                    current.Location = New Point(currentStartLocation.X - (currentMouseLocation.X - e.X) / factor, currentStartLocation.Y)
                    current.Size = New Size(currentStartSize.Width + (currentMouseLocation.X - e.X) / factor, currentStartSize.Height - (currentMouseLocation.Y - e.Y) / factor)
                ElseIf IsMatchingRight AndAlso IsMatchingTop Then
                    current.Location = New Point(currentStartLocation.X, currentStartLocation.Y - (currentMouseLocation.Y - e.Y) / factor)
                    current.Size = New Size(currentStartSize.Width - (currentMouseLocation.X - e.X) / factor, currentStartSize.Height + (currentMouseLocation.Y - e.Y) / factor)
                ElseIf IsMatchingRight AndAlso IsMatchingBottom Then
                    current.Size = New Size(currentStartSize.Width - (currentMouseLocation.X - e.X) / factor, currentStartSize.Height - (currentMouseLocation.Y - e.Y) / factor)
                ElseIf IsMatchingLeft Then
                    current.Location = New Point(currentStartLocation.X - (currentMouseLocation.X - e.X) / factor, currentStartLocation.Y)
                    current.Size = New Size(currentStartSize.Width + (currentMouseLocation.X - e.X) / factor, currentStartSize.Height)
                ElseIf IsMatchingRight Then
                    current.Size = New Size(currentStartSize.Width - (currentMouseLocation.X - e.X) / factor, currentStartSize.Height)
                ElseIf IsMatchingTop Then
                    current.Location = New Point(currentStartLocation.X, currentStartLocation.Y - (currentMouseLocation.Y - e.Y) / factor)
                    current.Size = New Size(currentStartSize.Width, currentStartSize.Height + (currentMouseLocation.Y - e.Y) / factor)
                ElseIf IsMatchingBottom Then
                    current.Size = New Size(currentStartSize.Width, currentStartSize.Height - (currentMouseLocation.Y - e.Y) / factor)
                End If
                If current.Location.X > currentStartLocation.X + currentStartSize.Width - minsize Then
                    current.Location.X = currentStartLocation.X + currentStartSize.Width - minsize
                End If
                If current.Location.Y > currentStartLocation.Y + currentStartSize.Height - minsize Then
                    current.Location.Y = currentStartLocation.Y + currentStartSize.Height - minsize
                End If
                If current.Size.Width < minsize OrElse current.Size.Height < minsize Then
                    current.Size = New Size(Math.Max(current.Size.Width, minsize), Math.Max(current.Size.Height, minsize))
                End If
                ' maybe store bulb or score size
                StoreCurrentSettings(current)
            Else
                current.Location = New Point(currentStartLocation.X - (currentMouseLocation.X - e.X) / factor, currentStartLocation.Y - (currentMouseLocation.Y - e.Y) / factor)
                ' check the location to avoid moving into the not visible area
                Dim image As Image = If(Backglass.currentData.IsDMDImageShown, Backglass.currentData.DMDImage, Backglass.currentData.Image)
                If current.Location.X < current.Size.Width / 2 * (-1) Then current.Location.X = current.Size.Width / 2 * (-1)
                If current.Location.Y < current.Size.Height / 2 * (-1) Then current.Location.Y = current.Size.Height / 2 * (-1)
                If image IsNot Nothing Then
                    If current.Location.X > image.Size.Width - current.Size.Width / 2 Then current.Location.X = image.Size.Width - current.Size.Width / 2
                    If current.Location.Y > image.Size.Height - current.Size.Height / 2 Then current.Location.Y = image.Size.Height - current.Size.Height / 2
                End If
            End If
            parent.Refresh()
            RaiseEvent SelectedItemMoving(sender, New MouseMoveEventArgs(
                                          If(currentBulb Is Nothing, MouseMoveEventArgs.eItemType.Score, MouseMoveEventArgs.eItemType.Bulb),
                                          current.Location,
                                          current.Size))
        End If
        ' raise event
        RaiseEvent MouseMove(Me, New MouseMoveEventArgs(New Point(CInt(e.X / factor), CInt(e.Y / factor))))
        ' reset remove click when moving is done
        IsMatchingX = False
        IsMatchingGrillHeightX = False
        IsMatchingSmallGrillHeightX = False
        IsMatchingDMDLocationX = False
    End Sub
    Private Sub Parent_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            parent.Cursor = CalcMouseLocation(e.X, e.Y, currentBulb, currentScore, currentDMDCopyArea)
            ' set current bulb at the first position in the list
            SelectedBulb = currentBulb
            If currentBulb IsNot Nothing Then
                bulbs.Remove(currentBulb)
                bulbs.Insert(0, currentBulb)
            End If
            ' set current score at the first position in the list
            SelectedScore = currentScore
            If currentScore IsNot Nothing Then
                scores.Remove(currentScore)
                scores.Insert(0, currentScore)
            End If
            ' store location
            Dim current As InfoBase = If(currentBulb IsNot Nothing, currentBulb, If(currentScore IsNot Nothing, currentScore, currentDMDCopyArea))
            If current IsNot Nothing Then
                currentStartLocation = current.Location
                currentStartSize = current.Size
                currentMouseLocation = New Point(e.X, e.Y)
                RaiseEvent SelectedItemMoving(sender, New MouseMoveEventArgs(
                                              If(currentBulb Is Nothing, MouseMoveEventArgs.eItemType.Score, MouseMoveEventArgs.eItemType.Bulb),
                                              current.Location,
                                              current.Size))
                ' raise click event
                RaiseEvent SelectedItemClicked(sender, New MouseMoveEventArgs(
                                               If(currentBulb Is Nothing, MouseMoveEventArgs.eItemType.Score, MouseMoveEventArgs.eItemType.Bulb),
                                               current.Location,
                                               current.Size))
            Else
                moveStartLocation = New Point(e.X, e.Y)
                moveParentLocation = parent.Location
            End If
        ElseIf e.Button = MouseButtons.Right Then
            Dim itemList As Generic.List(Of InfoBase) = New Generic.List(Of InfoBase)
            Dim sorteditemList As Generic.SortedList(Of String, InfoBase) = New Generic.SortedList(Of String, InfoBase)
            parent.Cursor = CalcMouseLocation(e.X, e.Y, , , , , itemList)
            For Each item As InfoBase In itemList
                If TypeOf item Is Illumination.BulbInfo Then
                    With DirectCast(item, Illumination.BulbInfo)
                        If String.IsNullOrEmpty(.Name) Then
                            sorteditemList.Add("ZZZZZZZZ" & .ID, item)
                        Else
                            sorteditemList.Add(.Name & .ID.ToString(), item)
                        End If
                    End With
                ElseIf TypeOf item Is ReelAndLED.ScoreInfo Then
                    With DirectCast(item, ReelAndLED.ScoreInfo)
                        sorteditemList.Add(" " & .ID.ToString(), item)
                    End With
                End If
            Next
            itemList.Clear()
            If sorteditemList.Count >= 1 Then
                If contextMenuItems Is Nothing Then
                    contextMenuItems = New ContextMenuStrip()
                    AddHandler contextMenuItems.ItemClicked, AddressOf ContextMenuItems_ItemClicked
                    AddHandler contextMenuItems.MouseLeave, AddressOf ContextMenuItems_ItemsLeft
                End If
                For i As Integer = contextMenuItems.Items.Count - 1 To 0 Step -1
                    If contextMenuItems.Items(i).Image IsNot Nothing Then
                        contextMenuItems.Items(i).Image.Dispose()
                        contextMenuItems.Items(i).Image = Nothing
                    End If
                    RemoveHandler contextMenuItems.Items(i).MouseEnter, AddressOf ContextMenuItem_ItemEntered
                    RemoveHandler contextMenuItems.Items(i).MouseLeave, AddressOf ContextMenuItem_ItemLeft
                    contextMenuItems.Items(i).Dispose()
                Next
                contextMenuItems.Items.Clear()
                For Each item As KeyValuePair(Of String, InfoBase) In sorteditemList
                    If TypeOf item.Value Is Illumination.BulbInfo Then
                        With DirectCast(item.Value, Illumination.BulbInfo)
                            With contextMenuItems.Items.Add("Name='" & .Name & "', ID='" & If(Not String.IsNullOrEmpty(.RomInfo2String), .RomInfo2String, .B2SInfo2String) & If(.B2SValue > 0, "/" & .B2SValue, "") & "', text='" & .Text & "', location='" & .Location.X & ", " & .Location.Y & "', size='" & .Size.Width & "x" & .Size.Height & "'")
                                .Tag = item.Value
                                .Image = My.Resources.illumination2
                                AddHandler .MouseEnter, AddressOf ContextMenuItem_ItemEntered
                                AddHandler .MouseLeave, AddressOf ContextMenuItem_ItemLeft
                            End With
                        End With
                    ElseIf TypeOf item.Value Is ReelAndLED.ScoreInfo Then
                        With DirectCast(item.Value, ReelAndLED.ScoreInfo)
                            With contextMenuItems.Items.Add("Index='" & .ID & "', startdigit='" & .B2SStartDigit & "', location='" & .Location.X & ", " & .Location.Y & "', size='" & .Size.Width & "x" & .Size.Height & "'")
                                .Tag = item.Value
                                .Image = My.Resources.led_small
                                AddHandler .MouseEnter, AddressOf ContextMenuItem_ItemEntered
                                AddHandler .MouseLeave, AddressOf ContextMenuItem_ItemLeft
                            End With
                        End With
                    End If
                Next
                contextMenuItems.Show(Cursor.Position)
            End If
        End If
        RaiseEvent MouseDown(Me, e)
    End Sub
    Private Sub Parent_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        ' maybe store undo
        If SelectedItem IsNot Nothing AndAlso (Not currentStartLocation.Equals(SelectedItem.Location) OrElse Not currentStartSize.Equals(SelectedItem.Size)) Then
            Undo.AddEntry(New Undo.UndoEntry(Undo.Type.BulbOrScoreMoved, SelectedItem, currentStartLocation, currentStartSize, SelectedItem.Location, SelectedItem.Size))
        End If
        ' do mouse up stuff
        If currentBulb IsNot Nothing Then
            StoreCurrentSettings(currentBulb)
            RaiseEvent SelectedBulbMoved(Me, e)
        ElseIf currentScore IsNot Nothing Then
            StoreCurrentSettings(currentScore)
        End If
        currentBulb = Nothing
        currentScore = Nothing
        currentDMDCopyArea = Nothing
        ' maybe send bulb edited
        RaiseEvent SelectedBulbEdited(Me, New EventArgs())
        ' maybe remove selected item
        parent.Cursor = CalcMouseLocation(e.X, e.Y, , , , False)
        If IsMatchingX AndAlso (SelectedItem IsNot Nothing OrElse CopyDMDImageFromBackglass) Then
            If TypeOf SelectedItem Is Illumination.BulbInfo Then
                Undo.AddEntry(New Undo.UndoEntry(Undo.Type.BulbRemoved, SelectedItem))
                bulbs.Remove(SelectedItem)
                SelectedBulb = Nothing
                RaiseEvent SelectedItemRemoved(Me, New EventArgs())
            ElseIf TypeOf SelectedItem Is ReelAndLED.ScoreInfo Then
                Undo.AddEntry(New Undo.UndoEntry(Undo.Type.ScoreRemoved, SelectedItem))
                scores.Remove(SelectedItem)
                SelectedScore = Nothing
                RaiseEvent SelectedItemRemoved(Me, New EventArgs())
            ElseIf CopyDMDImageFromBackglass Then
                If e.Button = MouseButtons.Left Then
                    RaiseEvent CopyDMDCopyArea(Me, New EventArgs())
                End If
            End If
        End If
        moveStartLocation = Nothing
        RaiseEvent MouseUp(Me, e)
        parent.Refresh()
    End Sub

    Private Sub ContextMenuItems_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        If e.ClickedItem.Tag IsNot Nothing Then
            If TypeOf e.ClickedItem.Tag Is Illumination.BulbInfo Then
                SelectedBulb = e.ClickedItem.Tag
                bulbs.Remove(SelectedBulb)
                bulbs.Insert(0, SelectedBulb)
            ElseIf TypeOf e.ClickedItem.Tag Is ReelAndLED.ScoreInfo Then
                SelectedScore = e.ClickedItem.Tag
                scores.Remove(SelectedScore)
                scores.Insert(0, SelectedScore)
            End If
        End If
        parent.Invalidate()
    End Sub
    Private Sub ContextMenuItems_ItemsLeft(ByVal sender As Object, ByVal e As EventArgs)
        PreviewedBulb = Nothing
        PreviewedScore = Nothing
        parent.Invalidate()
    End Sub
    Private Sub ContextMenuItem_ItemEntered(ByVal sender As Object, ByVal e As EventArgs)
        PreviewedBulb = Nothing
        PreviewedScore = Nothing
        If TypeOf sender.tag Is Illumination.BulbInfo Then
            PreviewedBulb = sender.tag
        ElseIf TypeOf sender.tag Is ReelAndLED.ScoreInfo Then
            PreviewedScore = sender.tag
        End If
        parent.Invalidate()
    End Sub
    Private Sub ContextMenuItem_ItemLeft(ByVal sender As Object, ByVal e As EventArgs)
        PreviewedBulb = Nothing
        PreviewedScore = Nothing
        parent.Invalidate()
    End Sub

    Private Function CalcMouseLocation(ByVal x As Integer,
                                       ByVal y As Integer,
                                       Optional ByRef currentBulbWithMouseOver As Illumination.BulbInfo = Nothing,
                                       Optional ByRef currentScoreWithMouseOver As ReelAndLED.ScoreInfo = Nothing,
                                       Optional ByRef currentDMDCopyAreaWithMouseOver As InfoBase = Nothing,
                                       Optional ByVal lookforMatchingX As Boolean = True,
                                       Optional ByRef allBulbsAndScores As Generic.List(Of InfoBase) = Nothing) As Cursor
        Dim look4secondone As Integer = If(My.Computer.Keyboard.CtrlKeyDown AndAlso My.Computer.Keyboard.ShiftKeyDown, 2, 1)
        Dim startwithbulbs As Boolean = (SelectedScore Is Nothing)
        Dim ret As Cursor = Cursors.Default
        IsMatchingLeft = False
        IsMatchingRight = False
        IsMatchingTop = False
        IsMatchingBottom = False
        If lookforMatchingX Then
            IsMatchingX = False
            IsMatchingGrillHeightX = False
            IsMatchingSmallGrillHeightX = False
            IsMatchingDMDLocationX = False
        End If
        currentBulbWithMouseOver = Nothing
        currentScoreWithMouseOver = Nothing
        currentDMDCopyAreaWithMouseOver = Nothing
        For i As Integer = 0 To 1
            If look4secondone > 0 Then
                currentScoreWithMouseOver = Nothing
            End If
            If (i = 0 AndAlso startwithbulbs) OrElse (i = 1 AndAlso Not startwithbulbs) Then
                If ShowIlluFrames AndAlso (currentScoreWithMouseOver Is Nothing OrElse allBulbsAndScores IsNot Nothing) Then
                    For Each bulb As Illumination.BulbInfo In bulbs
                        ret = CalcItem(bulb, x, y, currentBulbWithMouseOver, lookforMatchingX)
                        If ret <> Cursors.Default Then
                            If allBulbsAndScores IsNot Nothing Then
                                allBulbsAndScores.Add(bulb)
                            End If
                            look4secondone -= 1
                            If look4secondone <= 0 AndAlso allBulbsAndScores Is Nothing Then Exit For
                        End If
                    Next
                End If
            End If
            If look4secondone > 0 Then
                currentBulbWithMouseOver = Nothing
            End If
            If (i = 0 AndAlso Not startwithbulbs) OrElse (i = 1 AndAlso startwithbulbs) Then
                If ShowScoreFrames AndAlso (currentBulbWithMouseOver Is Nothing OrElse allBulbsAndScores IsNot Nothing) Then
                    For Each score As ReelAndLED.ScoreInfo In scores
                        ret = CalcItem(score, x, y, currentScoreWithMouseOver)
                        If ret <> Cursors.Default Then
                            If allBulbsAndScores IsNot Nothing Then
                                allBulbsAndScores.Add(score)
                            End If
                            look4secondone -= 1
                            If look4secondone <= 0 AndAlso allBulbsAndScores Is Nothing Then Exit For
                        End If
                    Next
                End If
            End If
        Next
        ' look for DMD image copy frame
        If ret = Cursors.Default AndAlso CopyDMDImageFromBackglass Then
            ret = CalcItem(dmdcopyimage, x, y, currentDMDCopyAreaWithMouseOver, lookforMatchingX)
        End If
        ' look for grills and DMD
        If ret = Cursors.Default Then
            ret = CalcItem(Nothing, x, y, Nothing)
        End If
        ' get out
        Return ret
    End Function
    Private Function CalcItem(ByVal item As InfoBase,
                              ByVal x As Integer,
                              ByVal y As Integer,
                              ByRef currentWithMouseOver As InfoBase,
                              Optional ByVal lookforMatchingX As Boolean = True) As Cursor
        Const thick As Integer = 6
        Dim ret As Cursor = Cursors.Default
        If item IsNot Nothing Then
            With item
                Dim getin As Boolean = True
                If TypeOf item Is Illumination.BulbInfo Then
                    getin = (String.IsNullOrEmpty(RomInfoFilter) OrElse
                             RomInfoFilter.Equals(.B2SInfo2String) OrElse
                             RomInfoFilter.Equals(.RomInfo2String) OrElse
                             (RomInfoFilter.Equals("withoutid") AndAlso ((Backglass.currentData.CommType = eCommType.B2S AndAlso String.IsNullOrEmpty(.B2SInfo2String)) OrElse (Backglass.currentData.CommType = eCommType.Rom AndAlso String.IsNullOrEmpty(.RomInfo2String)))))
                    If Not getin Then
                        With DirectCast(item, Illumination.BulbInfo)
                            getin = (RomInfoFilter.Equals("off") AndAlso .InitialState = 0) OrElse
                                    (RomInfoFilter.Equals("on") AndAlso .InitialState = 1) OrElse
                                    (RomInfoFilter.Equals("alwayson") AndAlso .InitialState = 2) OrElse
                                    (RomInfoFilter.Equals("authentic") AndAlso .DualMode <> eDualMode.Fantasy) OrElse
                                    (RomInfoFilter.Equals("fantasy") AndAlso .DualMode <> eDualMode.Authentic) OrElse
                                    (RomInfoFilter.Equals("withname") AndAlso Not String.IsNullOrEmpty(.Name))
                        End With
                    End If
                End If
                If getin Then
                    If lookforMatchingX Then
                        Dim isSmallRect As Boolean = (item.Size.Width * factor < 25 OrElse item.Size.Height * factor < 25)
                        If x / factor >= .Location.X + .Size.Width - If(isSmallRect, 11, 15) / factor AndAlso x / factor <= .Location.X + .Size.Width - 5 / factor AndAlso y / factor >= .Location.Y + 5 / factor AndAlso y / factor <= .Location.Y + If(isSmallRect, 11, 15) / factor Then
                            If (SelectedItem IsNot Nothing AndAlso .Equals(SelectedItem)) OrElse CopyDMDImageFromBackglass Then
                                IsMatchingX = True
                                If isSmallRect Then IsMatchingX = False
                            End If
                        End If
                    End If
                    If (TypeOf item Is Illumination.BulbInfo AndAlso Not DirectCast(item, Illumination.BulbInfo).IsImageSnippit) OrElse Not TypeOf item Is Illumination.BulbInfo Then
                        IsMatchingLeft = False
                        IsMatchingRight = False
                        IsMatchingTop = False
                        IsMatchingBottom = False
                        If x / factor >= .Location.X AndAlso x / factor <= .Location.X + .Size.Width Then
                            IsMatchingTop = (y / factor >= .Location.Y AndAlso y / factor <= .Location.Y + thick)
                            IsMatchingBottom = (y / factor >= .Location.Y + .Size.Height - thick AndAlso y / factor <= .Location.Y + .Size.Height)
                        End If
                        If y / factor >= .Location.Y AndAlso y / factor <= .Location.Y + .Size.Height Then
                            IsMatchingLeft = (x / factor >= .Location.X AndAlso x / factor <= .Location.X + thick)
                            IsMatchingRight = (x / factor >= .Location.X + .Size.Width - thick AndAlso x / factor <= .Location.X + .Size.Width)
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
                    End If
                    If x / factor >= .Location.X AndAlso x / factor <= .Location.X + .Size.Width AndAlso y / factor >= .Location.Y AndAlso y / factor <= .Location.Y + .Size.Height Then
                        currentWithMouseOver = item
                        If ret = Cursors.Default Then ret = Cursors.Hand
                    End If
                End If
            End With
        Else
            If parent IsNot Nothing Then
                Dim grillX As Integer = parent.Width - 15
                Dim grillY As Integer = (Backglass.currentData.Image.Height - Backglass.currentData.GrillHeight) * factor - 15
                Dim smallgrillY As Integer = (Backglass.currentData.Image.Height - Backglass.currentData.SmallGrillHeight) * factor - 15
                If (SetGrillHeight OrElse SetSmallGrillHeight) Then
                    ' grills
                    If x >= grillX AndAlso x <= grillX + 12 AndAlso y >= grillY AndAlso y <= grillY + 12 Then
                        IsMatchingGrillHeightX = True
                        ret = Cursors.Hand
                    ElseIf x >= grillX AndAlso x <= grillX + 12 AndAlso y >= smallgrillY AndAlso y <= smallgrillY + 12 Then
                        IsMatchingSmallGrillHeightX = True
                        ret = Cursors.Hand
                    End If
                ElseIf SetDMDLocation Then
                    ' DMD
                    If Backglass.currentData.DMDImage IsNot Nothing Then
                        Dim dmdsize As Size = New Size(Backglass.currentData.DMDImage.Width * factor, Backglass.currentData.DMDImage.Height * factor)
                        If Backglass.currentData.DMDDefaultLocation <> Nothing Then
                            Dim dmdloc As Point = New Point(Backglass.currentData.DMDDefaultLocation.X * factor, Backglass.currentData.DMDDefaultLocation.Y * factor)
                            grillX = dmdloc.X + dmdsize.Width + 5
                            grillY = dmdloc.Y
                            If x >= grillX AndAlso x <= grillX + 12 AndAlso y >= grillY AndAlso y <= grillY + 12 Then
                                IsMatchingDMDLocationX = True
                                ret = Cursors.Hand
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return ret
    End Function

    'Private Function CreateMouseMoveEventArgs(current As InfoBase) As MouseMoveEventArgs
    '    Return New MouseMoveEventArgs(If(currentBulb Is Nothing, MouseMoveEventArgs.eItemType.Score, MouseMoveEventArgs.eItemType.Bulb),
    '                                  current.Location,
    '                                  current.Size)
    'End Function

    Private Sub StoreCurrentSettings(ByVal current As InfoBase)
        ' maybe store bulb or score settings
        If TypeOf current Is Illumination.BulbInfo Then
            With DirectCast(current, Illumination.BulbInfo)
                LastBulbSize = .Size
                LastBulbIntensity = .Intensity
                LastBulbInitialState = .InitialState
                LastBulbLightColor = .LightColor
                LastBulbDodgeColor = .DodgeColor
                LastBulbFont = If(Not String.IsNullOrEmpty(.FontName), New Font(.FontName, .FontSize, .FontStyle), Nothing)
            End With
        ElseIf TypeOf current Is ReelAndLED.ScoreInfo Then
            With DirectCast(current, ReelAndLED.ScoreInfo)
                LastScoreSize = .Size
                LastScoreDigits = .Digits
                LastScoreSpacing = .Spacing
                LastScoreReelType = .ReelType
                LastScoreReelColor = .ReelColor
                ' maybe set dirty flag for a reel recalculation
                .IsSingleReelSizeDirty = True
            End With
        End If
    End Sub

End Class