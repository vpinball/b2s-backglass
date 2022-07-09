Module ModuleGeneral

    Public Const AppTitle As String = "B2S Backglass Designer"

    Public DefaultReels As String() = New String() {"EMR_T1", "LED"}

    Public EXEDir As String = String.Empty
    Public Const ProjectDir As String = "Projects"

    Public Property CurrentB2S() As String = String.Empty

    Public Property DefaultOpacity() As Single = 1

    Public ReadOnly Property Headline() As String
        Get
            Return If(Not String.IsNullOrEmpty(CurrentB2S), CurrentB2S & " - ", "") & AppTitle
        End Get
    End Property

    Public Sub UpdateStatusBar(form As formDesigner, backglass As B2STabPage)
        ' update zoom factor
        form.tscmbZoomInPercent.Text = backglass.BackglassData.Zoom.ToString() & "%"
        ' update file info
        form.tsLabelFileInfo.Padding = New Padding(10, 0, 10, 0)
        If Not String.IsNullOrEmpty(backglass.BackglassData.ImageFileName) Then
            form.tsLabelFileInfo.BorderSides = ToolStripStatusLabelBorderSides.Left
            form.tsLabelFileInfo.ImageAlign = ContentAlignment.MiddleRight
            form.tsLabelFileInfo.Image = My.Resources.imagefile
            form.tsLabelFileInfo.TextAlign = ContentAlignment.MiddleCenter
            form.tsLabelFileInfo.Text = backglass.BackglassData.ImageFileName
        Else
            form.tsLabelFileInfo.BorderSides = ToolStripStatusLabelBorderSides.None
            form.tsLabelFileInfo.Image = Nothing
            form.tsLabelFileInfo.Text = String.Empty
        End If
        ' update file size info
        form.tsLabelFileSize.Padding = New Padding(10, 0, 10, 0)
        If backglass IsNot Nothing AndAlso backglass.Image IsNot Nothing Then
            form.tsLabelFileSize.BorderSides = ToolStripStatusLabelBorderSides.Left
            form.tsLabelFileSize.ImageAlign = ContentAlignment.MiddleRight
            form.tsLabelFileSize.Image = My.Resources.imagesize
            form.tsLabelFileSize.TextAlign = ContentAlignment.MiddleCenter
            form.tsLabelFileSize.Text = backglass.Image.Width.ToString() & " x " & backglass.Image.Height.ToString()
        Else
            form.tsLabelFileSize.BorderSides = ToolStripStatusLabelBorderSides.None
            form.tsLabelFileSize.Image = Nothing
            form.tsLabelFileSize.Text = String.Empty
        End If
    End Sub

    Public Sub UpdateStatusBar4Mouse(form As formDesigner, backglass As B2STabPage, loc As Point)
        form.tsLabelMarker.Padding = New Padding(10, 0, 10, 0)
        If backglass IsNot Nothing AndAlso backglass.Image IsNot Nothing Then
            form.tsLabelMarker.BorderSides = ToolStripStatusLabelBorderSides.Left
            form.tsLabelMarker.ImageAlign = ContentAlignment.MiddleRight
            form.tsLabelMarker.Image = My.Resources.imagemarker
            form.tsLabelMarker.TextAlign = ContentAlignment.MiddleCenter
            form.tsLabelMarker.Text = loc.X.ToString() & ", " & loc.Y.ToString()
        Else
            form.tsLabelMarker.BorderSides = ToolStripStatusLabelBorderSides.None
            form.tsLabelMarker.Image = Nothing
            form.tsLabelMarker.Text = String.Empty
        End If
    End Sub

End Module
