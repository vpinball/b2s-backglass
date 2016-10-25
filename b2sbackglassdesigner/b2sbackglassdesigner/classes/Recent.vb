Public Class Recent

    Inherits HelperBase

    Public recentEntries As Generic.SortedList(Of Integer, recentEntry) = New Generic.SortedList(Of Integer, recentEntry)
    Public Class recentEntry
        Public Name As String = String.Empty
        Public ThumbnailImage As Image = Nothing

        Public Sub New(ByVal _name As String, ByVal _thumbnailimage As Image)
            Name = _name
            ThumbnailImage = _thumbnailimage
        End Sub
    End Class

    Public Property IsDirty() As Boolean = True

    Public Sub New()
        LoadRecentList()
    End Sub

    Public Sub AddToRecentList(ByVal backglassdata As Backglass.Data)
        Dim newrecent As recentEntry = New recentEntry(backglassdata.Name, If(backglassdata.ThumbnailImage IsNot Nothing, backglassdata.ThumbnailImage, backglassdata.Image.Resized(New Size(32, 32))))
        ' check whether the current entry is already included in the list
        For Each recentEntry As KeyValuePair(Of Integer, recentEntry) In recentEntries
            If recentEntry.Value.Name.Equals(newrecent.Name, StringComparison.CurrentCultureIgnoreCase) Then
                recentEntries.Remove(recentEntry.Key)
                Exit For
            End If
        Next
        ' not more than 10 entries
        Do While recentEntries.Count > 9
            recentEntries.RemoveAt(0)
        Loop
        ' get the highest key
        Dim key As Integer = 1
        For Each recentEntry As KeyValuePair(Of Integer, recentEntry) In recentEntries
            If key < recentEntry.Key Then key = recentEntry.Key
        Next
        ' add the new entry
        recentEntries.Add(key + 1, newrecent)
        ' save data
        SaveRecentList()
        ' set dirty flag for menu
        IsDirty = True
    End Sub
    Public Sub ResetThumbnailImage(ByVal backglassdata As Backglass.Data)
        For Each recentEntry As KeyValuePair(Of Integer, recentEntry) In recentEntries
            If recentEntry.Value.Name.Equals(backglassdata.Name) Then
                recentEntry.Value.ThumbnailImage = backglassdata.Image.Resized(New Size(32, 32))
                Exit For
            End If
        Next
    End Sub
    Public Sub RenameRecentEntry(ByVal oldname As String, ByVal newname As String)
        ' rename entry
        For Each recentEntry As KeyValuePair(Of Integer, recentEntry) In recentEntries
            If recentEntry.Value.Name.Equals(oldname) Then
                recentEntry.Value.Name = newname
            End If
        Next
        ' save data
        SaveRecentList()
        ' set dirty flag for menu
        IsDirty = True
    End Sub
    Public Sub RemoveFromRecentList(ByVal key As Integer)
        ' remove the entry
        recentEntries.Remove(key)
        ' save data
        SaveRecentList()
        ' set dirty flag for menu
        IsDirty = True
    End Sub
    Public Sub RemoveAllFromRecentList()
        recentEntries.Clear()
        ' set dirty flag for menu
        IsDirty = True
    End Sub

    Private Sub LoadRecentList()
        If XmlSettings Is Nothing Then
            XmlSettings = New Xml.XmlDocument
        End If
        If IO.File.Exists(SettingsFileName) Then
            XmlSettings.Load(SettingsFileName)
            If XmlSettings IsNot Nothing AndAlso XmlSettings.SelectSingleNode("B2SBackglassDesignerSettings/RecentBackglasses") IsNot Nothing Then
                Dim topnode As Xml.XmlElement = XmlSettings.SelectSingleNode("B2SBackglassDesignerSettings/RecentBackglasses")
                Dim i As Integer = 1
                For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Recent")
                    Dim recentEntry As recentEntry = New recentEntry(innerNode.Attributes("Name").InnerText, Base64ToImage(innerNode.Attributes("ThumbnailImage").InnerText))
                    recentEntries.Add(i, recentEntry)
                    i += 1
                Next
            End If
        End If
    End Sub
    Private Sub SaveRecentList()
        If CheckSaveDir(BackglassProjectsPath) Then
            Dim nodeHeader As Xml.XmlElement = XmlSettings.SelectSingleNode("B2SBackglassDesignerSettings")
            If nodeHeader Is Nothing Then
                nodeHeader = XmlSettings.CreateElement("B2SBackglassDesignerSettings")
                XmlSettings.AppendChild(nodeHeader)
            End If
            Dim nodeRecentList As Xml.XmlElement = nodeHeader.SelectSingleNode("RecentBackglasses")
            If nodeRecentList Is Nothing Then
                nodeRecentList = XmlSettings.CreateElement("RecentBackglasses")
                nodeHeader.AppendChild(nodeRecentList)
            End If
            nodeRecentList.RemoveAll()
            For Each recentEntry As KeyValuePair(Of Integer, recentEntry) In recentEntries
                Dim nodeEntry As Xml.XmlElement = XmlSettings.CreateElement("Recent")
                nodeRecentList.AppendChild(nodeEntry)
                With recentEntry
                    nodeEntry.SetAttribute("ID", .Key)
                    nodeEntry.SetAttribute("Name", .Value.Name)
                    nodeEntry.SetAttribute("ThumbnailImage", ImageToBase64(.Value.ThumbnailImage))
                End With
            Next
            ' save
            XmlSettings.Save(SettingsFileName)
        End If
    End Sub

End Class
