Imports System

Public MustInherit Class formToolBase

    Inherits Form

    Private XML As Xml.XmlDocument = Nothing

    Private path As String = IO.Path.Combine(EXEDir, ProjectDir)
    Private filename As String = IO.Path.Combine(path, Application.ProductName & ".Settings.xml")

    Private IsLoadDone As Boolean = False

    Friend Enum eDefaultLocation
        NotDefined = 0
        NW = 1
        SW = 2
        SE = 3
        NE = 4
    End Enum
    Friend DefaultLocation As eDefaultLocation = eDefaultLocation.NotDefined
    Friend SaveName As String = String.Empty

    Private Sub formToolBase_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadSettings()
        IsLoadDone = True
    End Sub

    Private Sub formToolBase_LocationChanged(sender As Object, e As System.EventArgs) Handles Me.LocationChanged
        SaveSettings()
    End Sub
    Private Sub formToolBase_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        SaveSettings()
    End Sub

    Private Sub LoadSettings()
        If IO.File.Exists(filename) Then
            XML = New Xml.XmlDocument
            XML.Load(filename)
            If XML IsNot Nothing AndAlso Not String.IsNullOrEmpty(SaveName) AndAlso XML.SelectSingleNode("B2SBackglassDesignerSettings/FormSettings/" & SaveName) IsNot Nothing Then
                Dim node As Xml.XmlElement = XML.SelectSingleNode("B2SBackglassDesignerSettings/FormSettings/" & SaveName)
                Dim x As Integer = CInt(node.SelectSingleNode("LocX").Attributes("Value").InnerText)
                Dim y As Integer = CInt(node.SelectSingleNode("LocY").Attributes("Value").InnerText)
                Dim width As Integer = CInt(node.SelectSingleNode("Width").Attributes("Value").InnerText)
                Dim height As Integer = CInt(node.SelectSingleNode("Height").Attributes("Value").InnerText)
                Dim maximized As Boolean = (node.SelectSingleNode("Maximized").Attributes("Value").InnerText = "1")
                Me.Location = New Point(x, y)
                Me.Size = New Size(width, height)
                Me.WindowState = If(maximized, FormWindowState.Maximized, FormWindowState.Normal)
            End If
        Else
            If DefaultLocation <> eDefaultLocation.NotDefined Then
                Dim x As Integer = 0
                Dim y As Integer = 0
                If DefaultLocation = eDefaultLocation.NW OrElse DefaultLocation = eDefaultLocation.SW Then
                    x = Me.Owner.Location.X + Me.Owner.Width - Me.Size.Width - 20
                Else
                    x = Me.Owner.Location.X + 20
                End If
                If DefaultLocation = eDefaultLocation.SW OrElse DefaultLocation = eDefaultLocation.SE Then
                    y = Me.Owner.Location.Y + Me.Owner.Height - Me.Size.Height - 20
                Else
                    y = Me.Owner.Location.Y + 20
                End If
                Me.Location = New Point(x, y)
            End If
        End If
    End Sub

    Private Sub SaveSettings()
        If Not IsLoadDone OrElse String.IsNullOrEmpty(SaveName) Then Return
        If CheckSaveDir() Then
            If XML Is Nothing Then
                XML = New Xml.XmlDocument
            End If
            Dim nodeHeader As Xml.XmlElement = XML.SelectSingleNode("B2SBackglassDesignerSettings")
            If nodeHeader Is Nothing Then
                nodeHeader = XML.CreateElement("B2SBackglassDesignerSettings")
                XML.AppendChild(nodeHeader)
            End If
            Dim nodeForms As Xml.XmlElement = nodeHeader.SelectSingleNode("FormSettings")
            If nodeForms Is Nothing Then
                nodeForms = XML.CreateElement("FormSettings")
                nodeHeader.AppendChild(nodeForms)
            End If
            Dim nodeForm As Xml.XmlElement = nodeForms.SelectSingleNode(SaveName)
            If nodeForm Is Nothing Then
                nodeForm = XML.CreateElement(SaveName)
                nodeForms.AppendChild(nodeForm)
            End If
            AddXMLAttribute(XML, nodeForm, "LocX", "Value", Me.Location.X.ToString())
            AddXMLAttribute(XML, nodeForm, "LocY", "Value", Me.Location.Y.ToString())
            AddXMLAttribute(XML, nodeForm, "Width", "Value", Me.Size.Width.ToString())
            AddXMLAttribute(XML, nodeForm, "Height", "Value", Me.Size.Height.ToString())
            AddXMLAttribute(XML, nodeForm, "Maximized", "Value", If(Me.WindowState = FormWindowState.Maximized, "1", "0"))
            ' save
            XML.Save(filename)
        End If
    End Sub

    Private Function CheckSaveDir() As Boolean
        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        Return (IO.Directory.Exists(path))
    End Function

    Private Sub AddXMLAttribute(ByRef XML As Xml.XmlDocument, ByRef nodeHeader As Xml.XmlElement, ByVal element As String, ByVal attribut As String, ByVal value As String)
        Dim node As Xml.XmlElement = nodeHeader.SelectSingleNode(element)
        If node Is Nothing Then
            node = XML.CreateElement(element)
        End If
        node.SetAttribute(attribut, value)
        nodeHeader.AppendChild(node)
    End Sub

End Class
