Public Class Undo

    Public Shared Property SelectedBackglass() As B2STabPage = Nothing
    Public Shared Property ListBox() As ListBox = Nothing

    Public Enum Type
        Undefined = 0
        BulbAdded = 1
        ScoreAdded = 2
        BulbOrScoreMoved = 3
        BulbRemoved = 4
        ScoreRemoved = 5
        ImageImported = 51
        ImageReloaded = 52
        ImageChanged = 53
        ImageResized = 54
        ImageBrightnessChanged = 55
        ImageRemoved = 61
        IlluminationImageImported = 71
        IlluminationImageChanged = 73
        DMDImageImported = 81
        DMDImageChanged = 83
    End Enum
    Public Shared UndoList As UndoCollection = New UndoCollection
    Public Class UndoCollection

        Inherits Generic.List(Of UndoEntry)

    End Class
    Public Class UndoEntry

        Public Type As Type = Type.Undefined
        Public Item As Object = Nothing
        Public Data1 As Object = Nothing
        Public Data2 As Object = Nothing
        Public Data3 As Object = Nothing
        Public Data4 As Object = Nothing

        Public Sub New(ByVal _type As Type, ByVal _item As Object)
            Type = _type
            Item = _item
        End Sub
        Public Sub New(ByVal _type As Type, ByVal _item As Object, ByVal _data1 As Object)
            Type = _type
            Item = _item
            Data1 = _data1
        End Sub
        Public Sub New(ByVal _type As Type, ByVal _item As Object, ByVal _data1 As Object, ByVal _data2 As Object, ByVal _data3 As Object, ByVal _data4 As Object)
            Type = _type
            Item = _item
            Data1 = _data1
            Data2 = _data2
            Data3 = _data3
            Data4 = _data4
        End Sub

        Public ReadOnly Property Image() As Image
            Get
                Select Case Type
                    Case B2SBackglassDesigner.Undo.Type.BulbAdded
                        Return My.Resources.marker_newbulb
                    Case B2SBackglassDesigner.Undo.Type.ScoreAdded
                        Return My.Resources.marker_reel2
                    Case B2SBackglassDesigner.Undo.Type.BulbOrScoreMoved
                        Return My.Resources.marker_bulb
                    Case B2SBackglassDesigner.Undo.Type.BulbRemoved
                        Return My.Resources.delete_red
                    Case B2SBackglassDesigner.Undo.Type.ScoreRemoved
                        Return My.Resources.delete_red
                    Case B2SBackglassDesigner.Undo.Type.ImageImported
                        Return My.Resources.chooseback3
                    Case B2SBackglassDesigner.Undo.Type.ImageReloaded
                        Return My.Resources.chooseback2
                    Case B2SBackglassDesigner.Undo.Type.ImageChanged
                        Return My.Resources.chooseback1
                    Case B2SBackglassDesigner.Undo.Type.ImageResized
                        Return My.Resources.resize
                    Case B2SBackglassDesigner.Undo.Type.ImageRemoved

                    Case B2SBackglassDesigner.Undo.Type.ImageBrightnessChanged
                        Return My.Resources.brightness
                    Case B2SBackglassDesigner.Undo.Type.IlluminationImageImported

                    Case B2SBackglassDesigner.Undo.Type.IlluminationImageChanged

                    Case B2SBackglassDesigner.Undo.Type.DMDImageImported

                    Case B2SBackglassDesigner.Undo.Type.DMDImageChanged

                End Select
                Return My.Resources.designer
            End Get
        End Property

        Public Overrides Function ToString() As String
            Select Case Type
                Case B2SBackglassDesigner.Undo.Type.BulbAdded
                    Return My.Resources.UNDO_BulbAdded
                Case B2SBackglassDesigner.Undo.Type.ScoreAdded
                    Return My.Resources.UNDO_ScoreAdded
                Case B2SBackglassDesigner.Undo.Type.BulbOrScoreMoved
                    Return My.Resources.UNDO_BulbOrScoreMoved
                Case B2SBackglassDesigner.Undo.Type.BulbRemoved
                    Return My.Resources.UNDO_BulbRemoved
                Case B2SBackglassDesigner.Undo.Type.ScoreRemoved
                    Return My.Resources.UNDO_ScoreRemoved
                Case B2SBackglassDesigner.Undo.Type.ImageImported
                    Return My.Resources.UNDO_ImageImported
                Case B2SBackglassDesigner.Undo.Type.ImageReloaded
                    Return My.Resources.UNDO_ImageReloaded
                Case B2SBackglassDesigner.Undo.Type.ImageChanged
                    Return My.Resources.UNDO_ImageChanged
                Case B2SBackglassDesigner.Undo.Type.ImageResized
                    Return My.Resources.UNDO_ImageResized
                Case B2SBackglassDesigner.Undo.Type.ImageRemoved

                Case B2SBackglassDesigner.Undo.Type.ImageBrightnessChanged
                    Return My.Resources.UNDO_ImageBrightnessChanged
                Case B2SBackglassDesigner.Undo.Type.IlluminationImageImported

                Case B2SBackglassDesigner.Undo.Type.IlluminationImageChanged

                Case B2SBackglassDesigner.Undo.Type.DMDImageImported

                Case B2SBackglassDesigner.Undo.Type.DMDImageChanged

            End Select
            Return Type.ToString()
        End Function

    End Class

    Public Shared Sub AddEntry(item As UndoEntry)
        UndoList.Add(item)
        ' add to undo listbox
        ListBox.Items.Add(item)
        ListBox.SelectedItem = item
    End Sub
    Public Shared Sub Clear()
        UndoList.Clear()
        ListBox.Items.Clear()
        ListBox.SelectedItem = Nothing
    End Sub

    Public Shared Sub Undo()
        If UndoList.Count > 0 Then
            Dim index As Integer = UndoList.Count - 1
            If index > -1 Then
                Dim current As UndoEntry = UndoList(index)
                With current
                    Select Case .Type
                        Case Type.BulbAdded
                            SelectedBackglass.BackglassData.Bulbs.Remove(.Item)
                            SelectedBackglass.Mouse.SelectedBulb = Nothing
                        Case Type.ScoreAdded
                            SelectedBackglass.BackglassData.Scores.Remove(.Item)
                            SelectedBackglass.Mouse.SelectedScore = Nothing
                        Case Type.BulbOrScoreMoved
                            .Item.Location = .Data1
                            .Item.Size = .Data2
                        Case Type.BulbRemoved
                            SelectedBackglass.BackglassData.Bulbs.Add(.Item)
                            SelectedBackglass.Mouse.SelectedBulb = .Item
                        Case Type.ScoreRemoved
                            SelectedBackglass.BackglassData.Scores.Add(.Item.ID, .Item)
                            SelectedBackglass.Mouse.SelectedScore = .Item
                        Case Type.ImageImported
                            SelectedBackglass.Image = .Item
                        Case Type.ImageReloaded
                            SelectedBackglass.Image = .Item
                        Case Type.ImageChanged
                            SelectedBackglass.Image = .Item
                        Case Type.ImageResized
                            SelectedBackglass.Image = .Item
                        Case Type.ImageRemoved

                        Case Type.ImageBrightnessChanged
                            SelectedBackglass.Image = .Item
                        Case Type.IlluminationImageImported

                        Case Type.IlluminationImageChanged

                        Case Type.DMDImageImported

                        Case Type.DMDImageChanged

                    End Select
                End With
                ' remove last undo entry
                UndoList.RemoveAt(index)
                ' remove from undo listbox
                ListBox.Items.RemoveAt(index)
                ListBox.SelectedIndex = index - 1
                ' do an invalidate to the picture box
                If SelectedBackglass IsNot Nothing Then
                    SelectedBackglass.Invalidate()
                End If
            End If
        End If
    End Sub

    Public Shared Sub Redo()

    End Sub

End Class
