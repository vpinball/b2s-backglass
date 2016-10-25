Public Class GeneralData

    Public Shared Property currentData() As Data = New Data()

    Public Class Data

        Private Const imageimportprefix As String = "ImageImport"

        Public ImportedReelImageSets As ImageSetCollection = New ImageSetCollection
        Public ImportedCreditReelImageSets As ImageSetCollection = New ImageSetCollection
        Public ImportedLEDImageSets As ImageSetCollection = New ImageSetCollection
        Public Class ImageSetCollection
            Inherits Generic.SortedList(Of Integer, Image())

            Public Shadows Sub Add(ByVal value As Image())
                Dim newkey As Integer = 1
                For Each key As Integer In Me.Keys
                    If key >= newkey Then newkey = key + 1
                Next
                MyBase.Add(newkey, value)
            End Sub
            Public Shadows Sub Add(ByVal key As Integer, ByVal value As Image())
                MyBase.Add(key, value)
            End Sub
        End Class

        Public Property IsDirty() As Boolean = False

        Public Sub New()
            Load()
        End Sub

        Public Sub AddImageSet(ByVal value As Image(), ByVal type As eImageSetType)
            Select Case type
                Case eImageSetType.ReelImages
                    ImportedReelImageSets.Add(value)
                Case eImageSetType.CreditReelImages
                    ImportedCreditReelImageSets.Add(value)
                Case eImageSetType.LEDImages
                    ImportedLEDImageSets.Add(value)
            End Select
            IsDirty = True
            Save()
        End Sub
        Public Sub RemoveImageSet(ByVal index As Integer)

            IsDirty = True
            Save()
        End Sub


        ' private stuff ***************************************************************************************************************************************************

        Private Sub Load()

            ' load import stuff
            If IO.File.Exists(ImportFileName) Then
                'Dim helper As HelperBase = New HelperBase()
                Dim XmlImport As Xml.XmlDocument = New Xml.XmlDocument()
                XmlImport.Load(ImportFileName)
                If XmlImport IsNot Nothing Then
                    GetImageSetsFromXML(XmlImport, "ReelSets", ImportedReelImageSets)
                    GetImageSetsFromXML(XmlImport, "CreditReelSets", ImportedCreditReelImageSets)
                    GetImageSetsFromXML(XmlImport, "LEDSets", ImportedLEDImageSets)
                End If
            End If

        End Sub
        Private Sub Save()

            ' save import stuff
            If IsDirty Then
                IsDirty = False
                ' XML
                Dim helper As HelperBase = New HelperBase()
                Dim XMLImport As Xml.XmlDocument = New Xml.XmlDocument()
                Dim nodeImportHeader As Xml.XmlElement = XmlSettings.SelectSingleNode("B2SBackglassDesignerImport")
                If nodeImportHeader Is Nothing Then
                    nodeImportHeader = XMLImport.CreateElement("B2SBackglassDesignerImport")
                    XMLImport.AppendChild(nodeImportHeader)
                End If
                Dim nodeImageSetsHeader As Xml.XmlElement = nodeImportHeader.SelectSingleNode("ImageSets")
                If nodeImageSetsHeader Is Nothing Then
                    nodeImageSetsHeader = XMLImport.CreateElement("ImageSets")
                    nodeImportHeader.AppendChild(nodeImageSetsHeader)
                End If
                AddImageSets2XML(XMLImport, nodeImageSetsHeader, "ReelSets", ImportedReelImageSets, helper)
                AddImageSets2XML(XMLImport, nodeImageSetsHeader, "CreditReelSets", ImportedCreditReelImageSets, helper)
                AddImageSets2XML(XMLImport, nodeImageSetsHeader, "LEDSets", ImportedLEDImageSets, helper)
                ' save
                XMLImport.Save(ImportFileName)
            End If

        End Sub

        Private Sub GetImageSetsFromXML(ByVal XML As Xml.XmlDocument, ByVal settypename As String, ByVal imageset As ImageSetCollection)

            If XML.SelectSingleNode("B2SBackglassDesignerImport/ImageSets/" & settypename) IsNot Nothing Then
                For Each node As Xml.XmlElement In XML.SelectNodes("B2SBackglassDesignerImport/ImageSets/" & settypename)
                    For Each setnode As Xml.XmlElement In node.ChildNodes
                        Dim key As Integer = CInt(setnode.Name.Substring(imageimportprefix.Length))
                        Dim images As Image() = Nothing
                        For Each childnode As Xml.XmlElement In setnode.ChildNodes
                            Dim image As Image = Base64ToImage(childnode.Attributes("Value").InnerText)
                            If images Is Nothing Then
                                ReDim images(0)
                                images(0) = image
                            Else
                                ReDim Preserve images(images.Length)
                                images(images.Length - 1) = image
                            End If
                        Next
                        imageset.Add(key, images)
                    Next
                Next
            End If

        End Sub
        Private Sub AddImageSets2XML(ByVal XML As Xml.XmlDocument, ByVal nodeSetsHeader As Xml.XmlElement, ByVal settypename As String, ByVal imageset As ImageSetCollection, ByVal helper As HelperBase)

            ' check set node
            Dim nodeImageSetType As Xml.XmlElement = nodeSetsHeader.SelectSingleNode(settypename)
            If nodeImageSetType Is Nothing Then
                nodeImageSetType = XML.CreateElement(settypename)
                nodeSetsHeader.AppendChild(nodeImageSetType)
            End If

            ' write nodes
            For Each setinfo As KeyValuePair(Of Integer, Image()) In imageset
                Dim nodeSet As Xml.XmlElement = nodeImageSetType.SelectSingleNode(imageimportprefix & setinfo.Key.ToString())
                If nodeSet Is Nothing Then
                    nodeSet = XML.CreateElement(imageimportprefix & setinfo.Key.ToString())
                    nodeImageSetType.AppendChild(nodeSet)
                End If

                Dim i As Integer = 1
                For Each image As Image In setinfo.Value
                    helper.AddXMLAttribute(XML, nodeSet, "Image" & i, "Value", ImageToBase64(image))
                    i += 1
                Next
            Next

        End Sub

    End Class

End Class
