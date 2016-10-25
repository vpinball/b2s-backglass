Public Class formBase

    Inherits System.Windows.Forms.Form

    Private IsLoadDone As Boolean = False

    Private StartLocation As Point = Nothing
    Private StartSize As Size = Nothing

    Public StartToolForms(8) As String

    Friend Enum eDefaultLocation
        NotDefined = 0
        NW = 1
        SW = 2
        SE = 3
        NE = 4
    End Enum
    Friend DefaultLocation As eDefaultLocation = eDefaultLocation.NotDefined
    Friend SaveName As String = String.Empty

    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean
        Get
            Return If(SaveName = "formDesigner", MyBase.ShowWithoutActivation, True)
        End Get
    End Property

    Private Sub formBase_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadSettings()
        IsLoadDone = True
    End Sub

    Private Sub formBase_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveWindowSettings()
    End Sub

    Private Sub formBase_LocationChanged(sender As Object, e As System.EventArgs) Handles Me.LocationChanged
        SaveSettings()
    End Sub
    Private Sub formBase_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        SaveSettings()
    End Sub

    Private Sub LoadSettings()
        If XmlSettings Is Nothing Then
            XmlSettings = New Xml.XmlDocument
        End If
        Dim formSettingsFound As Boolean = False
        If IO.File.Exists(SettingsFileName) Then
            XmlSettings.Load(SettingsFileName)
        Else
            XmlSettings.LoadXml(My.Resources.B2SBackglassDesigner_Settings_xml)
        End If
        If XmlSettings IsNot Nothing AndAlso Not String.IsNullOrEmpty(SaveName) AndAlso XmlSettings.SelectSingleNode("B2SBackglassDesignerSettings/FormSettings/" & SaveName) IsNot Nothing Then
            formSettingsFound = True
            Dim node As Xml.XmlElement = XmlSettings.SelectSingleNode("B2SBackglassDesignerSettings/FormSettings/" & SaveName)
            Dim x As Integer = CInt(node.SelectSingleNode("LocX").Attributes("Value").InnerText)
            Dim y As Integer = CInt(node.SelectSingleNode("LocY").Attributes("Value").InnerText)
            Dim width As Integer = CInt(node.SelectSingleNode("Width").Attributes("Value").InnerText)
            Dim height As Integer = CInt(node.SelectSingleNode("Height").Attributes("Value").InnerText)
            If x < 0 Then x = 0
            If x > Screen.PrimaryScreen.Bounds.Width Then x = Screen.PrimaryScreen.Bounds.Width - width
            If y < 0 Then y = 0
            If y > Screen.PrimaryScreen.Bounds.Height Then y = Screen.PrimaryScreen.Bounds.Height - height
            Dim maximized As Boolean = (node.SelectSingleNode("Maximized").Attributes("Value").InnerText = "1")
            If node.SelectSingleNode("Opacity") IsNot Nothing Then
                DefaultOpacity = CInt(node.SelectSingleNode("Opacity").Attributes("Value").InnerText) / 100
            End If
            If node.SelectSingleNode("VPTablesFolder") IsNot Nothing Then
                DefaultVPTablesFolder = node.SelectSingleNode("VPTablesFolder").Attributes("Value").InnerText
            End If
            Me.Location = New Point(x, y)
            Me.Size = New Size(width, height)
            Me.WindowState = If(maximized, FormWindowState.Maximized, FormWindowState.Normal)
        End If
        If Not formSettingsFound AndAlso DefaultLocation <> eDefaultLocation.NotDefined Then
            Dim x As Integer = 0
            Dim y As Integer = 0
            If DefaultLocation = eDefaultLocation.NE OrElse DefaultLocation = eDefaultLocation.SE Then
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
        If Not SaveName.Equals("formDesigner", StringComparison.CurrentCultureIgnoreCase) Then
            Me.Opacity = DefaultOpacity
        Else
            ' load tool window visibility
            Dim node As Xml.XmlElement = XmlSettings.SelectSingleNode("B2SBackglassDesignerSettings/OpenToolForms")
            If node IsNot Nothing Then
                For I As Integer = 1 To 9
                    If node.SelectSingleNode("Form" & I.ToString()) IsNot Nothing Then
                        StartToolForms(I - 1) = node.SelectSingleNode("Form" & I.ToString()).Attributes("Name").InnerText
                    Else
                        Exit For
                    End If
                Next
            End If
        End If
        If Me.WindowState <> FormWindowState.Maximized Then
            StartLocation = Me.Location
            StartSize = Me.Size
        End If
    End Sub
    Public Sub SaveSettings()

        If Not IsLoadDone OrElse String.IsNullOrEmpty(SaveName) Then Return

        Dim helper As HelperBase = New HelperBase()
        If helper.CheckSaveDir(BackglassProjectsPath) Then

            ' settings XML file
            Dim nodeHeader As Xml.XmlElement = XmlSettings.SelectSingleNode("B2SBackglassDesignerSettings")
            If nodeHeader Is Nothing Then
                nodeHeader = XmlSettings.CreateElement("B2SBackglassDesignerSettings")
                XmlSettings.AppendChild(nodeHeader)
            End If
            Dim nodeForms As Xml.XmlElement = nodeHeader.SelectSingleNode("FormSettings")
            If nodeForms Is Nothing Then
                nodeForms = XmlSettings.CreateElement("FormSettings")
                nodeHeader.AppendChild(nodeForms)
            End If
            Dim nodeForm As Xml.XmlElement = nodeForms.SelectSingleNode(SaveName)
            If nodeForm Is Nothing Then
                nodeForm = XmlSettings.CreateElement(SaveName)
                nodeForms.AppendChild(nodeForm)
            End If
            If Me.WindowState = FormWindowState.Maximized Then
                helper.AddXMLAttribute(XmlSettings, nodeForm, "LocX", "Value", StartLocation.X.ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "LocY", "Value", StartLocation.Y.ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "Width", "Value", StartSize.Width.ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "Height", "Value", StartSize.Height.ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "Maximized", "Value", "1")
            Else
                helper.AddXMLAttribute(XmlSettings, nodeForm, "LocX", "Value", Me.Location.X.ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "LocY", "Value", Me.Location.Y.ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "Width", "Value", Me.Size.Width.ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "Height", "Value", Me.Size.Height.ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "Maximized", "Value", "0")
            End If
            If SaveName.Equals("formDesigner", StringComparison.CurrentCultureIgnoreCase) Then
                helper.AddXMLAttribute(XmlSettings, nodeForm, "Opacity", "Value", CInt(DefaultOpacity * 100).ToString())
                helper.AddXMLAttribute(XmlSettings, nodeForm, "VPTablesFolder", "Value", DefaultVPTablesFolder)
            End If
            ' save
            Try
                XmlSettings.Save(SettingsFileName)
            Catch ex As Exception
                MessageBox.Show(String.Format(My.Resources.MSG_StartupSaveError, ex.Message), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End If

    End Sub
    Private Sub SaveWindowSettings()
        If SaveName.Equals("formDesigner", StringComparison.CurrentCultureIgnoreCase) Then
            Dim helper As HelperBase = New HelperBase()
            If helper.CheckSaveDir(BackglassProjectsPath) Then
                Dim nodeHeader As Xml.XmlElement = XmlSettings.SelectSingleNode("B2SBackglassDesignerSettings")
                If nodeHeader Is Nothing Then
                    nodeHeader = XmlSettings.CreateElement("B2SBackglassDesignerSettings")
                    XmlSettings.AppendChild(nodeHeader)
                End If
                Dim nodeForms As Xml.XmlElement = nodeHeader.SelectSingleNode("OpenToolForms")
                If nodeForms Is Nothing Then
                    nodeForms = XmlSettings.CreateElement("OpenToolForms")
                    nodeHeader.AppendChild(nodeForms)
                End If
                If Me.OwnedForms.Count > 0 Then
                    nodeForms.RemoveAll()
                    Dim i As Integer = 1
                    For Each form As Form In Me.OwnedForms
                        If form.Name.StartsWith("formTool", StringComparison.CurrentCultureIgnoreCase) Then
                            helper.AddXMLAttribute(XmlSettings, nodeForms, "Form" & i.ToString(), "Name", form.Name)
                            i += 1
                        End If
                    Next
                Else
                    nodeForms.RemoveAll()
                End If
                ' save
                Try
                    XmlSettings.Save(SettingsFileName)
                Catch ex As Exception
                    MessageBox.Show(String.Format(My.Resources.MSG_StartupSaveError, ex.Message), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            End If
        End If
    End Sub

End Class
