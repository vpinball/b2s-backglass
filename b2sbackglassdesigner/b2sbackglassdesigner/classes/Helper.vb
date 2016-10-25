Imports System
Imports System.Text
Imports System.IO

Public Class Helper

    Private Const SaveVersion As String = "0.1"

    Public Sub LoadData(ByRef _backglassData As BackglassData, ByVal filename As String)

        Dim XML As Xml.XmlDocument = New Xml.XmlDocument
        XML.Load(filename)
        If XML IsNot Nothing AndAlso XML.SelectSingleNode("B2SBackglassData") IsNot Nothing Then
            Dim version As String = XML.SelectSingleNode("B2SBackglassData").Attributes("Version").InnerText
            If version = SaveVersion Then
                Dim topnode As Xml.XmlElement = XML.SelectNodes("B2SBackglassData")(0)
                _backglassData = New BackglassData()
                Dim bulbs As Illumination.BulbCollection = New Illumination.BulbCollection()
                _backglassData.Bulbs = bulbs
                With _backglassData
                    .Text = topnode.SelectSingleNode("Name").Attributes("Value").InnerText
                    .TableType = CInt(topnode.SelectSingleNode("Type").Attributes("Value").InnerText)
                    .ImageFileName = topnode.SelectSingleNode("BackgroundPicture").SelectSingleNode("FileName").Attributes("Value").InnerText
                    .Image = Base64ToImage(topnode.SelectSingleNode("BackgroundPicture").SelectSingleNode("Image").Attributes("Value").InnerText)
                    If topnode.SelectSingleNode("Illumination") IsNot Nothing AndAlso topnode.SelectNodes("Illumination/Bulb") IsNot Nothing Then
                        For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Illumination/Bulb")
                            Dim bulb As Illumination.BulbInfo = New Illumination.BulbInfo()
                            bulb.ID = innerNode.Attributes("ID").InnerText
                            bulb.Name = innerNode.Attributes("Name").InnerText
                            bulb.Text = innerNode.Attributes("Text").InnerText
                            bulb.Visible = (CInt(innerNode.Attributes("Visible").InnerText) = 1)
                            bulb.Location = New Point(CInt(innerNode.Attributes("LocX").InnerText), CInt(innerNode.Attributes("LocY").InnerText))
                            bulb.Size = New Size(CInt(innerNode.Attributes("Width").InnerText), CInt(innerNode.Attributes("Height").InnerText))
                            bulb.Intensity = CInt(innerNode.Attributes("Intensity").InnerText)
                            bulb.Dodge = (CInt(innerNode.Attributes("Dodge").InnerText) = 1)
                            bulbs.Add(bulb)
                        Next
                    End If
                End With
            End If
        End If

    End Sub
    Public Sub SaveData(ByRef _backglassData As BackglassData)

        Dim path As String = IO.Path.Combine(EXEDir, ProjectDir)
        If CheckSaveDir(path) Then
            Dim XML As Xml.XmlDocument = New Xml.XmlDocument
            Dim nodeHeader As Xml.XmlElement = XML.CreateElement("B2SBackglassData")
            Dim nodeImage As Xml.XmlElement = XML.CreateElement("BackgroundPicture")
            Dim nodeIllumination As Xml.XmlElement = XML.CreateElement("Illumination")
            XML.AppendChild(nodeHeader)
            nodeHeader.SetAttribute("Version", SaveVersion)
            With _backglassData
                AddXMLAttribute(XML, nodeHeader, "Name", "Value", .Text)
                AddXMLAttribute(XML, nodeHeader, "Type", "Value", CInt(.TableType).ToString())
                nodeHeader.AppendChild(nodeImage)
                AddXMLAttribute(XML, nodeImage, "FileName", "Value", .ImageFileName)
                AddXMLAttribute(XML, nodeImage, "Image", "Value", ImageToBase64(.Image))
                nodeHeader.AppendChild(nodeIllumination)
                For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In _backglassData.Bulbs
                    Dim nodeBulb As Xml.XmlElement = XML.CreateElement("Bulb")
                    nodeIllumination.AppendChild(nodeBulb)
                    With bulb.Value
                        nodeBulb.SetAttribute("ID", .ID)
                        nodeBulb.SetAttribute("Name", .Name)
                        nodeBulb.SetAttribute("Text", .Text)
                        nodeBulb.SetAttribute("Visible", If(.Visible, "1", "0"))
                        nodeBulb.SetAttribute("LocX", .Location.X)
                        nodeBulb.SetAttribute("LocY", .Location.Y)
                        nodeBulb.SetAttribute("Width", .Size.Width)
                        nodeBulb.SetAttribute("Height", .Size.Height)
                        nodeBulb.SetAttribute("Intensity", .Intensity.ToString())
                        nodeBulb.SetAttribute("Dodge", If(.Dodge, "1", "0"))
                    End With
                Next
                ' create path for file saving
                path = IO.Path.Combine(path, .Text & ".b2s")
            End With
            XML.Save(path)
        End If

        _backglassData.IsDirty = False

    End Sub

    Private Function CheckSaveDir(path As String) As Boolean
        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        Return (IO.Directory.Exists(path))
    End Function

    Private Sub AddXMLAttribute(ByRef XML As Xml.XmlDocument, ByRef nodeHeader As Xml.XmlElement, ByVal element As String, ByVal attribut As String, ByVal value As String)
        Dim node As Xml.XmlElement = XML.CreateElement(element)
        node.SetAttribute(attribut, value)
        nodeHeader.AppendChild(node)
    End Sub

    Private Function ImageToBase64(ByVal image As Image) As String
        If image IsNot Nothing Then
            With New System.Drawing.ImageConverter
                Dim bytes() As Byte = CType(.ConvertTo(image, GetType(Byte())), Byte())
                Return Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks)
            End With
        Else
            Return String.Empty
        End If
    End Function
    Private Function Base64ToImage(ByVal data As String) As Image
        Dim image As Image = Nothing
        If data.Length > 0 Then
            Dim bytes() As Byte = Convert.FromBase64String(data)
            If bytes IsNot Nothing AndAlso bytes.Length > 0 Then
                With New System.Drawing.ImageConverter
                    image = CType(.ConvertFrom(bytes), Image)
                End With
            End If
        End If
        Return image
    End Function

    Public Function ResizeImage(image As Image, size As Size) As Image
        Dim newimage As Image = New Bitmap(size.Width, size.Height)
        Using gr As Graphics = Graphics.FromImage(newimage)
            gr.DrawImage(image, New Rectangle(0, 0, newimage.Width, newimage.Height))
        End Using
        Return newimage
    End Function

End Class
