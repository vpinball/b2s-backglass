Public Class HelperBase

    Friend Function CheckSaveDir(path As String) As Boolean
        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        Return (IO.Directory.Exists(path))
    End Function

    Friend Sub AddXMLAttribute(XML As Xml.XmlDocument, nodeHeader As Xml.XmlElement, element As String, attribut As String, value As String)
        Dim node As Xml.XmlElement = nodeHeader.SelectSingleNode(element)
        If node Is Nothing Then
            node = XML.CreateElement(element)
        End If
        node.SetAttribute(attribut, value)
        nodeHeader.AppendChild(node)
    End Sub

End Class
