Imports System
Imports System.Text
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Public Class Coding

    Inherits HelperBase

    Private Const DirectB2SVersion As String = "1.26"
    Private Const DirectB2SVersionMaybeWithDataLost As String = "1.3"
    Private Const DirectB2SVersionWithImportFeature As String = "0.85" ' nicht ändern

    Public Event ReportProgress(ByVal sender As Object, ByVal e As CodingProgressEventArgs)
    Public Class CodingProgressEventArgs
        Inherits System.EventArgs

        Public Progress As Integer = 0

        Public Sub New(ByVal _progress As Integer)
            Progress = _progress
        End Sub
    End Class


#Region "directB2S stuff"

    ' main method(s)

    Public Function CreateDirectB2SFile() As Boolean

        If Not CheckData() Then Return False

        Dim ret As Boolean = True

        RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(0))

        Dim currentDir As String = IO.Directory.GetCurrentDirectory()

        Dim projectname As String = Backglass.currentData.Name
        Dim assemblyname As String = Backglass.currentData.VSName

        ' create direct B2S and project directory
        IO.Directory.CreateDirectory(ProjectPath)
        IO.Directory.SetCurrentDirectory(ProjectPath)

        ' create direct B2S file
        Dim XML As Xml.XmlDocument = New Xml.XmlDocument
        Dim nodeHeader As Xml.XmlElement = XML.CreateElement("DirectB2SData")
        Dim nodeAnimations As Xml.XmlElement = XML.CreateElement("Animations")
        Dim nodeScores As Xml.XmlElement = XML.CreateElement("Scores")
        Dim nodeReels As Xml.XmlElement = XML.CreateElement("Reels")
        Dim nodeIllumination As Xml.XmlElement = XML.CreateElement("Illumination")
        Dim nodeSounds As Xml.XmlElement = XML.CreateElement("Sounds")
        Dim nodeImages As Xml.XmlElement = XML.CreateElement("Images")
        XML.AppendChild(nodeHeader)
        nodeHeader.SetAttribute("Version", DirectB2SVersion)
        With Backglass.currentData

            ' get scores and bulbs
            Dim savescores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo) = New Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo)
            Dim savebulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo) = New Generic.SortedList(Of Integer, Illumination.BulbInfo)
            For Each score As ReelAndLED.ScoreInfo In .Scores
                savescores.Add(score.ID, score)
            Next
            For Each score As ReelAndLED.ScoreInfo In .DMDScores
                savescores.Add(score.ID, score)
            Next
            For Each bulb As Illumination.BulbInfo In .Bulbs
                savebulbs.Add(bulb.ID, bulb)
            Next
            For Each bulb As Illumination.BulbInfo In .DMDBulbs
                savebulbs.Add(bulb.ID + 1000000, bulb)
            Next

            ' add main data
            AddXMLAttribute(XML, nodeHeader, "Name", "Value", .Name)
            AddXMLAttribute(XML, nodeHeader, "TableType", "Value", CInt(.TableType).ToString())
            AddXMLAttribute(XML, nodeHeader, "DMDType", "Value", CInt(.DMDType).ToString())
            AddXMLAttribute(XML, nodeHeader, "DMDDefaultLocation", "LocX", Backglass.currentData.DMDDefaultLocation.X.ToString())
            AddXMLAttribute(XML, nodeHeader, "DMDDefaultLocation", "LocY", Backglass.currentData.DMDDefaultLocation.Y.ToString())
            AddXMLAttribute(XML, nodeHeader, "GrillHeight", "Value", CInt(.GrillHeight).ToString())
            If .SmallGrillHeight > 0 Then
                AddXMLAttribute(XML, nodeHeader, "GrillHeight", "Small", CInt(.SmallGrillHeight - .GrillHeight).ToString())
            End If

            ' add additional main data
            AddXMLAttribute(XML, nodeHeader, "ProjectGUID", "Value", .ProjectGUID)
            AddXMLAttribute(XML, nodeHeader, "ProjectGUID2", "Value", .ProjectGUID2)
            AddXMLAttribute(XML, nodeHeader, "AssemblyGUID", "Value", .AssemblyGUID)
            AddXMLAttribute(XML, nodeHeader, "VSName", "Value", .VSName)
            AddXMLAttribute(XML, nodeHeader, "DualBackglass", "Value", If(.DualBackglass, "1", "0"))
            AddXMLAttribute(XML, nodeHeader, "Author", "Value", .Author)
            AddXMLAttribute(XML, nodeHeader, "Artwork", "Value", .Artwork)
            AddXMLAttribute(XML, nodeHeader, "GameName", "Value", .GameName)
            AddXMLAttribute(XML, nodeHeader, "AddEMDefaults", "Value", If(.AddEMDefaults, "1", "0"))
            AddXMLAttribute(XML, nodeHeader, "CommType", "Value", CInt(.CommType).ToString())
            AddXMLAttribute(XML, nodeHeader, "DestType", "Value", CInt(.DestType).ToString())
            AddXMLAttribute(XML, nodeHeader, "NumberOfPlayers", "Value", .NumberOfPlayers.ToString())
            AddXMLAttribute(XML, nodeHeader, "B2SDataCount", "Value", .B2SDataCount.ToString())
            AddXMLAttribute(XML, nodeHeader, "ReelType", "Value", .ReelType.ToString())
            AddXMLAttribute(XML, nodeHeader, "UseDream7LEDs", "Value", If(.UseDream7LEDs, "1", "0"))
            AddXMLAttribute(XML, nodeHeader, "D7Glow", "Value", (.D7Glow * 100).ToString())
            AddXMLAttribute(XML, nodeHeader, "D7Thickness", "Value", (.D7Thickness * 100).ToString())
            AddXMLAttribute(XML, nodeHeader, "D7Shear", "Value", (.D7Shear * 100).ToString())
            If .ReelColor <> Nothing Then
                AddXMLAttribute(XML, nodeHeader, "ReelColor", "Value", Color2String(.ReelColor))
            End If
            AddXMLAttribute(XML, nodeHeader, "ReelRollingDirection", "Value", CInt(.ReelRollingDirection).ToString())
            AddXMLAttribute(XML, nodeHeader, "ReelRollingInterval", "Value", .ReelRollingInterval.ToString())
            AddXMLAttribute(XML, nodeHeader, "ReelIntermediateImageCount", "Value", .ReelIntermediateImageCount.ToString())

            ' add animation
            nodeHeader.AppendChild(nodeAnimations)
            For Each item As Animation.AnimationHeader In .Animations
                If Not String.IsNullOrEmpty(item.Name) Then
                    Dim nodeAnimation As Xml.XmlElement = XML.CreateElement("Animation")
                    nodeAnimations.AppendChild(nodeAnimation)
                    With item
                        nodeAnimation.SetAttribute("Name", .Name)
                        nodeAnimation.SetAttribute("Parent", "Backglass")
                        nodeAnimation.SetAttribute("DualMode", CInt(.DualMode).ToString())
                        nodeAnimation.SetAttribute("Interval", .Interval.ToString())
                        nodeAnimation.SetAttribute("Loops", .Loops.ToString())
                        nodeAnimation.SetAttribute("IDJoin", .IDJoin)
                        nodeAnimation.SetAttribute("StartAnimationAtBackglassStartup", If(.StartAnimationAtBackglassStartup, "1", "0"))
                        nodeAnimation.SetAttribute("LightsStateAtAnimationStart", CInt(.LightsStateAtAnimationStart).ToString())
                        nodeAnimation.SetAttribute("LightsStateAtAnimationEnd", CInt(.LightsStateAtAnimationEnd).ToString())
                        nodeAnimation.SetAttribute("AnimationStopBehaviour", CInt(.AnimationStopBehaviour).ToString())
                        nodeAnimation.SetAttribute("LockInvolvedLamps", If(.LockInvolvedLamps, "1", "0"))
                        nodeAnimation.SetAttribute("HideScoreDisplays", If(.HideScoreDisplays, "1", "0"))
                        nodeAnimation.SetAttribute("BringToFront", If(.BringToFront, "1", "0"))
                        If .RandomStart Then
                            nodeAnimation.SetAttribute("RandomStart", If(.RandomStart, "1", "0"))
                            nodeAnimation.SetAttribute("RandomQuality", .RandomQuality.ToString())
                        End If
                        ' add all steps to animation
                        For Each stepitem As Animation.AnimationStep In item.AnimationSteps
                            Dim nodeStep As Xml.XmlElement = XML.CreateElement("AnimationStep")
                            nodeAnimation.AppendChild(nodeStep)
                            With stepitem
                                nodeStep.SetAttribute("Step", .Step.ToString())
                                nodeStep.SetAttribute("On", .On)
                                nodeStep.SetAttribute("WaitLoopsAfterOn", .WaitLoopsAfterOn.ToString())
                                nodeStep.SetAttribute("Off", .Off)
                                nodeStep.SetAttribute("WaitLoopsAfterOff", .WaitLoopsAfterOff.ToString())
                                If .PulseSwitch > 0 Then
                                    nodeStep.SetAttribute("PulseSwitch", .PulseSwitch.ToString())
                                End If
                            End With
                        Next
                    End With
                End If
            Next

            ' collect all intensities of the score displays
            Dim scoreimagesets As Generic.Dictionary(Of String, Integer) = New Generic.Dictionary(Of String, Integer)

            ' add scores
            nodeHeader.AppendChild(nodeScores)
            nodeScores.SetAttribute("ReelCountOfIntermediates", Backglass.currentData.ReelIntermediateImageCount.ToString())
            nodeScores.SetAttribute("ReelRollingDirection", Backglass.currentData.ReelRollingDirection.ToString())
            nodeScores.SetAttribute("ReelRollingInterval", Backglass.currentData.ReelRollingInterval.ToString())
            For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In savescores
                Dim nodeScore As Xml.XmlElement = XML.CreateElement("Score")
                nodeScores.AppendChild(nodeScore)
                With score.Value
                    nodeScore.SetAttribute("Parent", If((.ParentForm = eParentForm.DMD), "DMD", "Backglass"))
                    nodeScore.SetAttribute("ID", .ID)
                    If Backglass.currentData.CommType = eCommType.B2S Then
                        nodeScore.SetAttribute("B2SStartDigit", .B2SStartDigit.ToString())
                        nodeScore.SetAttribute("B2SScoreType", CInt(.B2SScoreType).ToString())
                        nodeScore.SetAttribute("B2SPlayerNo", CInt(.B2SPlayerNo).ToString())
                    End If
                    If IsReelImageRendered(score.Value.ReelType) OrElse IsReelImageDream7(score.Value.ReelType) Then
                        If Backglass.currentData.UseDream7LEDs Then
                            nodeScore.SetAttribute("ReelType", "Dream7" & .ReelType.ToString().Substring(8))
                        Else
                            nodeScore.SetAttribute("ReelType", .ReelType.ToString())
                        End If
                    Else
                        nodeScore.SetAttribute("ReelType", .ReelType)
                        If .ReelIlluLocation <> eReelIlluminationLocation.Off Then
                            Dim key As String = .ReelType & Chr(1) & .ReelIlluIntensity.ToString() & Chr(1) & CInt(.ReelIlluLocation).ToString()
                            If Not scoreimagesets.ContainsKey(key) Then
                                scoreimagesets.Add(key, scoreimagesets.Count + 1)
                            End If
                            Dim setid As Integer = scoreimagesets(key)
                            nodeScore.SetAttribute("ReelIlluImageSet", setid.ToString())
                        End If
                        nodeScore.SetAttribute("ReelIlluLocation", CInt(.ReelIlluLocation).ToString())
                        If .ReelIlluIntensity <= 0 Then .ReelIlluIntensity = 1
                        nodeScore.SetAttribute("ReelIlluIntensity", .ReelIlluIntensity.ToString())
                        nodeScore.SetAttribute("ReelIlluB2SID", .ReelIlluB2SID.ToString())
                        nodeScore.SetAttribute("ReelIlluB2SIDType", CInt(.ReelIlluB2SIDType).ToString())
                        nodeScore.SetAttribute("ReelIlluB2SValue", .ReelIlluB2SValue.ToString())
                    End If
                    nodeScore.SetAttribute("ReelLitColor", Color2String(.ReelColor))
                    nodeScore.SetAttribute("ReelDarkColor", Color2String(Color.FromArgb(15, 15, 15)))
                    nodeScore.SetAttribute("Glow", CInt(Backglass.currentData.D7Glow * 100).ToString())
                    nodeScore.SetAttribute("Thickness", CInt(Backglass.currentData.D7Thickness * 100).ToString())
                    nodeScore.SetAttribute("Shear", CInt(Backglass.currentData.D7Shear * 100).ToString())
                    nodeScore.SetAttribute("Digits", .Digits.ToString())
                    nodeScore.SetAttribute("Spacing", .Spacing.ToString())
                    nodeScore.SetAttribute("DisplayState", CInt(.DisplayState).ToString())
                    nodeScore.SetAttribute("LocX", .Location.X)
                    nodeScore.SetAttribute("LocY", .Location.Y)
                    nodeScore.SetAttribute("Width", .Size.Width)
                    nodeScore.SetAttribute("Height", .Size.Height)
                    nodeScore.SetAttribute("Sound3", "10")
                    nodeScore.SetAttribute("Sound4", "100")
                    nodeScore.SetAttribute("Sound5", "1000")
                End With
            Next

            RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(10))

            ' add EM reel images
            If Not String.IsNullOrEmpty(Backglass.currentData.ReelType) Then

                ' collect all necessary reel images
                Dim reelimages As Generic.Dictionary(Of String, Generic.Dictionary(Of String, Image)) = New Generic.Dictionary(Of String, Generic.Dictionary(Of String, Image))
                For Each reeltype As String In Backglass.currentData.ReelType.Split(",")
                    If Not String.IsNullOrEmpty(reeltype) AndAlso reeltype.Length >= 2 AndAlso Not IsReelImageRendered(reeltype) AndAlso Not IsReelImageDream7(reeltype) Then
                        Dim length As Integer = 2
                        If reeltype.Substring(reeltype.Length - 3, 1) = "_" Then length = 3
                        Dim isLED As Boolean = (reeltype.StartsWith("LED") OrElse reeltype.StartsWith(ImportedStartString & "LED"))
                        Dim reelname As String = reeltype.Substring(0, reeltype.Length - length)
                        If Not reelimages.ContainsKey(reelname) Then
                            reelimages.Add(reelname, New Generic.Dictionary(Of String, Image))
                        End If
                        Dim index As Integer = 0
                        Do While True
                            Dim currentname As String = reelname & "_" & If(index >= 0, index.ToString("D" & length - 1), "Empty")
                            Dim reelimage As Image = Nothing
                            If reeltype.StartsWith(ImportedStartString) Then
                                Try
                                    Select Case reeltype.Substring(8, 5)
                                        Case "EMR_T" : reelimage = GeneralData.currentData.ImportedReelImageSets(CInt(reeltype.Substring(13).Replace("_0", "")))(index)
                                        Case "EMR_CT" : reelimage = GeneralData.currentData.ImportedCreditReelImageSets(CInt(reeltype.Substring(14).Replace("_0", "")))(index)
                                        Case "LED_T" : reelimage = GeneralData.currentData.ImportedLEDImageSets(CInt(reeltype.Substring(13).Replace("_0", "")))(index)
                                    End Select
                                Catch ex As IndexOutOfRangeException
                                    ' nothing to do for this error
                                Catch ex As Exception
                                    MessageBox.Show(String.Format(My.Resources.MSG_CreateDirectB2SError, ex.Message), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End Try
                            Else
                                reelimage = My.Resources.ResourceManager.GetObject(currentname)
                            End If
                            If reelimage IsNot Nothing Then
                                If Not reelimages(reelname).ContainsKey(currentname) Then
                                    ' add standard image
                                    reelimages(reelname).Add(currentname, reelimage)
                                End If
                                ' maybe get out here
                                If index < 0 Then
                                    Exit Do
                                End If
                            Else
                                ' do not add the empty image because it isn't needed
                                If isLED AndAlso index >= 0 Then
                                    index = -99
                                Else
                                    Exit Do
                                End If
                            End If
                            ' set to next index
                            index += 1
                        Loop
                    End If
                Next

                ' pick up all standard images into simple arrays
                Dim reelimagearrays As Generic.Dictionary(Of String, Image()) = New Generic.Dictionary(Of String, Image())
                For Each reeltype As KeyValuePair(Of String, Generic.Dictionary(Of String, Image)) In reelimages
                    reelimagearrays.Add(reeltype.Key, Nothing)
                    For Each reelimage As KeyValuePair(Of String, Image) In reeltype.Value
                        If reelimagearrays(reeltype.Key) Is Nothing Then
                            ReDim reelimagearrays(reeltype.Key)(0)
                            reelimagearrays(reeltype.Key)(0) = reelimage.Value
                        Else
                            ReDim Preserve reelimagearrays(reeltype.Key)(reelimagearrays(reeltype.Key).Length)
                            reelimagearrays(reeltype.Key)(reelimagearrays(reeltype.Key).Length - 1) = reelimage.Value
                        End If
                    Next
                Next

                ' create intermediate images
                Dim reelintermediates As Generic.Dictionary(Of String, Image()) = New Generic.Dictionary(Of String, Image())
                If Backglass.currentData.ReelIntermediateImageCount > 0 Then
                    For Each reeltype As KeyValuePair(Of String, Generic.Dictionary(Of String, Image)) In reelimages
                        Dim isLED As Boolean = (reeltype.Key.StartsWith("LED") OrElse reeltype.Key.StartsWith(ImportedStartString & "LED"))
                        If Not isLED Then
                            Dim i As Integer = 0
                            For Each reelimage As KeyValuePair(Of String, Image) In reeltype.Value
                                Dim currentimage As Image = reelimagearrays(reeltype.Key)(i)
                                Dim nextimage As Image = Nothing
                                If i >= reelimagearrays(reeltype.Key).Count - 1 Then
                                    nextimage = reelimagearrays(reeltype.Key)(0)
                                Else
                                    nextimage = reelimagearrays(reeltype.Key)(i + 1)
                                End If
                                reelintermediates.Add(reelimage.Key, CreateIntermediates(nextimage, currentimage, Backglass.currentData.ReelIntermediateImageCount, (Backglass.currentData.ReelRollingDirection = eReelRollingDirection.Up)))
                                i += 1
                            Next
                        End If
                    Next
                End If

                ' create and add base XML nodes
                nodeHeader.AppendChild(nodeReels)
                Dim nodeReelsImages As Xml.XmlElement = XML.CreateElement("Images")
                nodeReels.AppendChild(nodeReelsImages)
                Dim nodeReelsIlluImages As Xml.XmlElement = XML.CreateElement("IlluminatedImages")
                nodeReels.AppendChild(nodeReelsIlluImages)
                Dim nodeReelIlluImageSet As Xml.XmlElement() = Nothing
                ReDim nodeReelIlluImageSet(scoreimagesets.Count - 1)
                For i As Integer = 0 To scoreimagesets.Count - 1
                    nodeReelIlluImageSet(i) = XML.CreateElement("Set")
                    nodeReelsIlluImages.AppendChild(nodeReelIlluImageSet(i))
                    nodeReelIlluImageSet(i).SetAttribute("ID", (i + 1).ToString())
                Next

                ' add standard images with intermediates
                For Each reeltype As KeyValuePair(Of String, Generic.Dictionary(Of String, Image)) In reelimages
                    Dim isLED As Boolean = (reeltype.Key.StartsWith("LED") OrElse reeltype.Key.StartsWith(ImportedStartString & "LED"))
                    For Each reelimage As KeyValuePair(Of String, Image) In reeltype.Value
                        ' add new reel image node
                        Dim nodeReelImage As Xml.XmlElement = XML.CreateElement("Image")
                        nodeReelsImages.AppendChild(nodeReelImage)
                        nodeReelImage.SetAttribute("Name", reelimage.Key)
                        nodeReelImage.SetAttribute("CountOfIntermediates", If(isLED, "0", Backglass.currentData.ReelIntermediateImageCount.ToString()))
                        nodeReelImage.SetAttribute("Image", ImageToBase64(reelimage.Value))
                        If Not isLED Then
                            If reelintermediates.ContainsKey(reelimage.Key) Then
                                Dim i As Integer = 1
                                For Each intermediateimage As Image In reelintermediates(reelimage.Key)
                                    nodeReelImage.SetAttribute("IntermediateImage" & i.ToString(), ImageToBase64(intermediateimage))
                                    i += 1
                                Next
                            End If
                        End If
                    Next
                Next

                ' add all illuminated image sets
                For i As Integer = 0 To scoreimagesets.Count - 1
                    Dim keyinfo As String() = scoreimagesets.Keys(i).Split(Chr(1))
                    Dim setreeltype As String = keyinfo(0)
                    Dim length As Integer = 2
                    If setreeltype.Substring(setreeltype.Length - 3, 1) = "_" Then length = 3
                    setreeltype = setreeltype.Substring(0, setreeltype.Length - length)
                    Dim reelilluintensity As Integer = CInt(keyinfo(1))
                    Dim reelillulocation As eReelIlluminationLocation = CInt(keyinfo(2))
                    For Each reeltype As KeyValuePair(Of String, Generic.Dictionary(Of String, Image)) In reelimages
                        Dim isLED As Boolean = (reeltype.Key.StartsWith("LED") OrElse reeltype.Key.StartsWith(ImportedStartString & "LED"))
                        For Each reelimage As KeyValuePair(Of String, Image) In reeltype.Value
                            If reelimage.Key.StartsWith(setreeltype, StringComparison.CurrentCultureIgnoreCase) Then
                                Dim nodeReelIlluImage As Xml.XmlElement = XML.CreateElement("IlluminatedImage")
                                nodeReelIlluImageSet(i).AppendChild(nodeReelIlluImage)
                                nodeReelIlluImage.SetAttribute("Name", reelimage.Key)
                                nodeReelIlluImage.SetAttribute("CountOfIntermediates", If(isLED, "0", Backglass.currentData.ReelIntermediateImageCount.ToString()))
                                nodeReelIlluImage.SetAttribute("Image", ImageToBase64(Backglass.currentTabPage.DrawIlluminatedReelImage(reelimage.Value, reelilluintensity, reelillulocation)))
                                If Not isLED Then
                                    If reelintermediates.ContainsKey(reelimage.Key) Then
                                        Dim j As Integer = 1
                                        For Each intermediateimage As Image In reelintermediates(reelimage.Key)
                                            nodeReelIlluImage.SetAttribute("IntermediateImage" & j.ToString(), ImageToBase64(Backglass.currentTabPage.DrawIlluminatedReelImage(intermediateimage, reelilluintensity, reelillulocation)))
                                            j += 1
                                        Next
                                    End If
                                End If
                            End If
                        Next
                    Next
                Next

            End If

            RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(20))

            ' create some illumination stuff
            Dim image As Image = Backglass.currentTabPage.IlluminatedImageOnlyWithAlwaysOnLights()
            Dim dmdimage As Image = Backglass.currentTabPage.IlluminatedDMDImageOnlyWithAlwaysOnLights()
            Dim offimage As Image = Backglass.currentTabPage.OffImage()
            Dim illuminationimage As Image = Backglass.currentTabPage.FirstStoredIlluminationImage()
            Dim thumbnailimage As Image = Backglass.currentData.ThumbnailImage

            RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(40))

            Dim illuimages As Generic.SortedList(Of Integer, Illumination.Lights.ImageInfo) = Backglass.currentTabPage.IlluminatedImages(image, offimage)

            RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(60))

            Dim illudmdimages As Generic.SortedList(Of Integer, Illumination.Lights.ImageInfo) = Backglass.currentTabPage.IlluminatedDMDImages(dmdimage)

            RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(80))

            ' add illumination
            nodeHeader.AppendChild(nodeIllumination)
            For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In savebulbs
                With bulb.Value
                    If .InitialState <> 2 Then
                        Dim imageinfo As Illumination.Lights.ImageInfo = Nothing
                        If bulb.Key >= 1000000 Then
                            If illudmdimages.ContainsKey(.ID) Then
                                imageinfo = illudmdimages(.ID)
                            End If
                        Else
                            If illuimages.ContainsKey(.ID) Then
                                imageinfo = illuimages(.ID)
                            End If
                        End If
                        If imageinfo IsNot Nothing Then
                            Dim nodeBulb As Xml.XmlElement = XML.CreateElement("Bulb")
                            nodeIllumination.AppendChild(nodeBulb)
                            ' add main illu info
                            nodeBulb.SetAttribute("Parent", If((.ParentForm = eParentForm.DMD), "DMD", "Backglass"))
                            nodeBulb.SetAttribute("ID", .ID)
                            nodeBulb.SetAttribute("Name", .Name)
                            If Backglass.currentData.CommType = eCommType.B2S Then
                                nodeBulb.SetAttribute("B2SID", .B2SID.ToString())
                                nodeBulb.SetAttribute("B2SIDType", CInt(.B2SIDType).ToString())
                                nodeBulb.SetAttribute("B2SValue", .B2SValue.ToString())
                            ElseIf .IsImageSnippit AndAlso .SnippitInfo.SnippitType = eSnippitType.MechRotatingImage Then
                                nodeBulb.SetAttribute("RomID", .SnippitInfo.SnippitMechID.ToString())
                                nodeBulb.SetAttribute("RomIDType", 4)
                            Else
                                nodeBulb.SetAttribute("RomID", .RomID.ToString())
                                nodeBulb.SetAttribute("RomIDType", CInt(.RomIDType).ToString())
                                nodeBulb.SetAttribute("RomInverted", If(.RomInverted, "1", "0"))
                            End If
                            nodeBulb.SetAttribute("InitialState", .InitialState.ToString())
                            nodeBulb.SetAttribute("DualMode", CInt(.DualMode).ToString())
                            If .Intensity <= 0 Then .Intensity = 1
                            nodeBulb.SetAttribute("Intensity", .Intensity.ToString())
                            nodeBulb.SetAttribute("LightColor", Color2String(.LightColor))
                            nodeBulb.SetAttribute("DodgeColor", Color2String(.DodgeColor))
                            nodeBulb.SetAttribute("IlluMode", CInt(.IlluMode).ToString())
                            nodeBulb.SetAttribute("Visible", If(.Visible, "1", "0"))
                            nodeBulb.SetAttribute("LocX", .LocationX.X)
                            nodeBulb.SetAttribute("LocY", .LocationX.Y)
                            nodeBulb.SetAttribute("Width", .SizeX.Width)
                            nodeBulb.SetAttribute("Height", .SizeX.Height)
                            ' snippit stuff
                            If .ZOrder > 0 Then
                                nodeBulb.SetAttribute("ZOrder", .ZOrder.ToString())
                            End If
                            nodeBulb.SetAttribute("IsImageSnippit", If(.IsImageSnippit, "1", "0"))
                            If .SnippitInfo.SnippitType <> eSnippitType.StandardImage Then
                                nodeBulb.SetAttribute("SnippitType", CInt(.SnippitInfo.SnippitType).ToString())
                                nodeBulb.SetAttribute("SnippitRotatingSteps", .SnippitInfo.SnippitRotatingSteps.ToString())
                                nodeBulb.SetAttribute("SnippitRotatingDirection", CInt(.SnippitInfo.SnippitRotatingDirection).ToString())
                                nodeBulb.SetAttribute("SnippitRotatingStopBehaviour", CInt(.SnippitInfo.SnippitRotatingStopBehaviour).ToString())
                                If .SnippitInfo.SnippitType = eSnippitType.SelfRotatingImage Then
                                    nodeBulb.SetAttribute("SnippitRotatingInterval", .SnippitInfo.SnippitRotatingInterval.ToString())
                                End If
                            End If
                            ' image and maybe off image
                            nodeBulb.SetAttribute("Image", If(imageinfo.Image IsNot Nothing, ImageToBase64(imageinfo.Image), ""))
                            If imageinfo.OffImage IsNot Nothing Then
                                nodeBulb.SetAttribute("OffImage", ImageToBase64(imageinfo.OffImage))
                            End If

                            ' add additional illu info for reimport
                            nodeBulb.SetAttribute("Text", .Text)
                            nodeBulb.SetAttribute("TextAlignment", CInt(.TextAlignment).ToString())
                            nodeBulb.SetAttribute("FontName", .FontName)
                            nodeBulb.SetAttribute("FontSize", CInt(.FontSize * 100).ToString())
                            nodeBulb.SetAttribute("FontStyle", CInt(.FontStyle).ToString())
                        End If
                    End If
                End With
            Next

            RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(90))

            ' add sounds
            nodeHeader.AppendChild(nodeSounds)
            For Each name As String In New String() {"10", "100", "1000"}
                Using stream As IO.FileStream = New IO.FileStream("C:\Users\Stefan\Dropbox\WIP Tables\Resources\Sound Resources\" & name & ".wav", IO.FileMode.Open)
                    If stream IsNot Nothing Then
                        Dim nodeSound As Xml.XmlElement = XML.CreateElement("Sound")
                        nodeSounds.AppendChild(nodeSound)
                        nodeSound.SetAttribute("Name", name)
                        nodeSound.SetAttribute("Type", "wav")
                        nodeSound.SetAttribute("Stream", WavToBase64(stream))
                    End If
                End Using
            Next

            ' add images
            nodeHeader.AppendChild(nodeImages)
            If offimage IsNot Nothing Then
                AddXMLAttribute(XML, nodeImages, "BackglassOffImage", "Value", ImageToBase64(offimage))
                AddXMLAttribute(XML, nodeImages, "BackglassOnImage", "Value", ImageToBase64(image))
                AddXMLAttribute(XML, nodeImages, "BackglassOnImage", "FileName", .ImageFileName)
                If Backglass.currentTabPage.OnImageRomID > 0 Then
                    AddXMLAttribute(XML, nodeImages, "BackglassOnImage", "RomID", Backglass.currentTabPage.OnImageRomID.ToString())
                    AddXMLAttribute(XML, nodeImages, "BackglassOnImage", "RomIDType", CInt(Backglass.currentTabPage.OnImageRomIDType).ToString())
                End If
            Else
                AddXMLAttribute(XML, nodeImages, "BackglassImage", "Value", ImageToBase64(image))
                AddXMLAttribute(XML, nodeImages, "BackglassImage", "FileName", .ImageFileName)
            End If
            If dmdimage IsNot Nothing Then
                AddXMLAttribute(XML, nodeImages, "DMDImage", "Value", ImageToBase64(dmdimage))
                AddXMLAttribute(XML, nodeImages, "DMDImage", "FileName", .DMDImageFileName)
            End If
            If illuminationimage IsNot Nothing Then
                AddXMLAttribute(XML, nodeImages, "IlluminationImage", "Value", ImageToBase64(illuminationimage))
            End If
            If thumbnailimage IsNot Nothing Then
                AddXMLAttribute(XML, nodeImages, "ThumbnailImage", "Value", ImageToBase64(thumbnailimage))
            End If

        End With

        ' save XML file
        XML.Save(assemblyname & ".directb2s")

        ' get to the starting working dir
        IO.Directory.SetCurrentDirectory(currentDir)

        RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(100))

        Return ret

    End Function

    Public Function ImportDirectB2SFile(ByRef _backglassData As Backglass.Data, ByVal filename As String) As Boolean

        Dim ret As Boolean = True

        If Not IO.File.Exists(filename) Then
            Return False
        End If

        Dim XML As Xml.XmlDocument = New Xml.XmlDocument
        Try
            XML.Load(filename)
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.MSG_ImportError, ex.Message), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If XML IsNot Nothing AndAlso XML.SelectSingleNode("DirectB2SData") IsNot Nothing Then

            Dim version As String = XML.SelectSingleNode("DirectB2SData").Attributes("Version").InnerText
            If version < DirectB2SVersionWithImportFeature Then

                MessageBox.Show(My.Resources.MSG_CannotImport, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else

                If version >= DirectB2SVersionMaybeWithDataLost Then
                    MessageBox.Show(My.Resources.MSG_ImportWarning, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                Dim topnode As Xml.XmlElement = XML.SelectNodes("DirectB2SData")(0)
                _backglassData = New Backglass.Data()
                Dim myanimations As Animation.AnimationHeaderCollection = New Animation.AnimationHeaderCollection()
                _backglassData.Animations = myanimations
                'Dim mydmdanimations As Animation.AnimationHeaderCollection = New Animation.AnimationHeaderCollection()
                '_backglassData.DMDAnimations = mydmdanimations
                Dim myscores As ReelAndLED.ScoreCollection = New ReelAndLED.ScoreCollection()
                _backglassData.Scores = myscores
                Dim mydmdscores As ReelAndLED.ScoreCollection = New ReelAndLED.ScoreCollection()
                _backglassData.DMDScores = mydmdscores
                Dim mybulbs As Illumination.BulbCollection = New Illumination.BulbCollection()
                _backglassData.Bulbs = mybulbs
                Dim mydmdbulbs As Illumination.BulbCollection = New Illumination.BulbCollection()
                _backglassData.DMDBulbs = mydmdbulbs
                Dim myimages As Images.ImageCollection = New Images.ImageCollection()
                _backglassData.Images = myimages
                With _backglassData
                    .BackupName = String.Empty
                    .ProjectGUID = topnode.SelectSingleNode("ProjectGUID").Attributes("Value").InnerText
                    .ProjectGUID2 = topnode.SelectSingleNode("ProjectGUID2").Attributes("Value").InnerText
                    .AssemblyGUID = topnode.SelectSingleNode("AssemblyGUID").Attributes("Value").InnerText
                    .Name = topnode.SelectSingleNode("Name").Attributes("Value").InnerText
                    .LoadedName = .Name
                    .VSName = IO.Path.GetFileNameWithoutExtension(filename) ' topnode.SelectSingleNode("VSName").Attributes("Value").InnerText
                    If topnode.SelectSingleNode("DualBackglass") IsNot Nothing Then
                        .DualBackglass = (topnode.SelectSingleNode("DualBackglass").Attributes("Value").InnerText = "1")
                    End If
                    .Author = topnode.SelectSingleNode("Author").Attributes("Value").InnerText
                    If topnode.SelectSingleNode("Artwork") IsNot Nothing Then
                        .Artwork = topnode.SelectSingleNode("Artwork").Attributes("Value").InnerText
                    End If
                    .GameName = topnode.SelectSingleNode("GameName").Attributes("Value").InnerText
                    .TableType = CInt(topnode.SelectSingleNode("TableType").Attributes("Value").InnerText)
                    .AddEMDefaults = (topnode.SelectSingleNode("AddEMDefaults").Attributes("Value").InnerText = "1")
                    .DMDType = CInt(topnode.SelectSingleNode("DMDType").Attributes("Value").InnerText)
                    .CommType = CInt(topnode.SelectSingleNode("CommType").Attributes("Value").InnerText)
                    .DestType = CInt(topnode.SelectSingleNode("DestType").Attributes("Value").InnerText)
                    .NumberOfPlayers = CInt(topnode.SelectSingleNode("NumberOfPlayers").Attributes("Value").InnerText)
                    .B2SDataCount = CInt(topnode.SelectSingleNode("B2SDataCount").Attributes("Value").InnerText)
                    .ReelType = topnode.SelectSingleNode("ReelType").Attributes("Value").InnerText
                    .UseDream7LEDs = (topnode.SelectSingleNode("UseDream7LEDs").Attributes("Value").InnerText = "1")
                    .D7Glow = CSng(topnode.SelectSingleNode("D7Glow").Attributes("Value").InnerText) / 100
                    .D7Thickness = CSng(topnode.SelectSingleNode("D7Thickness").Attributes("Value").InnerText) / 100
                    .D7Shear = CSng(topnode.SelectSingleNode("D7Shear").Attributes("Value").InnerText) / 100
                    Try
                        .ReelColor = String2Color(topnode.SelectSingleNode("ReelColor").Attributes("Value").InnerText.Replace(";", "."))
                    Catch
                        .ReelColor = Color.OrangeRed
                    End Try
                    .ReelRollingDirection = CInt(topnode.SelectSingleNode("ReelRollingDirection").Attributes("Value").InnerText)
                    .ReelRollingInterval = CInt(topnode.SelectSingleNode("ReelRollingInterval").Attributes("Value").InnerText)
                    .ReelIntermediateImageCount = CInt(topnode.SelectSingleNode("ReelIntermediateImageCount").Attributes("Value").InnerText)
                    .GrillHeight = CInt(topnode.SelectSingleNode("GrillHeight").Attributes("Value").InnerText)
                    If topnode.SelectSingleNode("GrillHeight").Attributes("Small") IsNot Nothing Then
                        .SmallGrillHeight = CInt(topnode.SelectSingleNode("GrillHeight").Attributes("Small").InnerText)
                    End If
                    .DMDDefaultLocation = New Point(CInt(topnode.SelectSingleNode("DMDDefaultLocation").Attributes("LocX").InnerText), CInt(topnode.SelectSingleNode("DMDDefaultLocation").Attributes("LocY").InnerText))

                    ' get all animations
                    If topnode.SelectSingleNode("Animations") IsNot Nothing AndAlso topnode.SelectNodes("Animations/Animation") IsNot Nothing Then
                        For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Animations/Animation")
                            Dim ani As Animation.AnimationHeader = New Animation.AnimationHeader()
                            ani.Name = innerNode.Attributes("Name").InnerText
                            If innerNode.Attributes("DualMode") IsNot Nothing Then
                                ani.DualMode = CInt(innerNode.Attributes("DualMode").InnerText)
                            End If
                            ani.Interval = CInt(innerNode.Attributes("Interval").InnerText)
                            ani.Loops = CInt(innerNode.Attributes("Loops").InnerText)
                            If innerNode.Attributes("B2SJoin") IsNot Nothing Then
                                ani.IDJoin = innerNode.Attributes("B2SJoin").InnerText
                            Else
                                ani.IDJoin = innerNode.Attributes("IDJoin").InnerText
                            End If
                            If ani.IDJoin = "0" Then ani.IDJoin = String.Empty
                            ani.StartAnimationAtBackglassStartup = (innerNode.Attributes("StartAnimationAtBackglassStartup").InnerText = "1")
                            If innerNode.Attributes("LightsStateAtAnimationStart") IsNot Nothing Then
                                ani.LightsStateAtAnimationStart = CInt(innerNode.Attributes("LightsStateAtAnimationStart").InnerText)
                            Else
                                ani.LightsStateAtAnimationStart = If((innerNode.Attributes("AllLightsOffAtAnimationStart").InnerText = "1"), Animation.AnimationHeader.eLightsStateAtAnimationStart.LightsOff, Animation.AnimationHeader.eLightsStateAtAnimationStart.NoChange)
                            End If
                            If innerNode.Attributes("LightsStateAtAnimationEnd") IsNot Nothing Then
                                ani.LightsStateAtAnimationEnd = CInt(innerNode.Attributes("LightsStateAtAnimationEnd").InnerText)
                            ElseIf innerNode.Attributes("ResetLightsAtAnimationEnd") IsNot Nothing Then
                                ani.LightsStateAtAnimationEnd = If((innerNode.Attributes("ResetLightsAtAnimationEnd").InnerText = "1"), Animation.AnimationHeader.eLightsStateAtAnimationEnd.LightsReseted, Animation.AnimationHeader.eLightsStateAtAnimationEnd.Undefined)
                            End If
                            If innerNode.Attributes("RunAnimationTilEnd") IsNot Nothing Then
                                ani.AnimationStopBehaviour = If((innerNode.Attributes("RunAnimationTilEnd").InnerText = "1"), Animation.AnimationHeader.eAnimationStopBehaviour.RunAnimationTillEnd, Animation.AnimationHeader.eAnimationStopBehaviour.StopImmediatelly)
                            ElseIf innerNode.Attributes("AnimationStopBehaviour") IsNot Nothing Then
                                ani.AnimationStopBehaviour = CInt(innerNode.Attributes("AnimationStopBehaviour").InnerText)
                            End If
                            ani.LockInvolvedLamps = (innerNode.Attributes("LockInvolvedLamps").InnerText = "1")
                            If innerNode.Attributes("HideScoreDisplays") IsNot Nothing Then
                                ani.HideScoreDisplays = (innerNode.Attributes("HideScoreDisplays").InnerText = "1")
                            End If
                            If innerNode.Attributes("BringToFront") IsNot Nothing Then
                                ani.BringToFront = (innerNode.Attributes("BringToFront").InnerText = "1")
                            End If
                            If innerNode.Attributes("RandomStart") IsNot Nothing Then
                                ani.RandomStart = (innerNode.Attributes("RandomStart").InnerText = "1")
                                ani.RandomQuality = CInt(innerNode.Attributes("RandomQuality").InnerText)
                            End If
                            For Each stepnode As Xml.XmlElement In innerNode.SelectNodes("AnimationStep")
                                Dim animationstep As Animation.AnimationStep = New Animation.AnimationStep()
                                animationstep.Step = CInt(stepnode.Attributes("Step").InnerText)
                                animationstep.On = stepnode.Attributes("On").InnerText
                                animationstep.WaitLoopsAfterOn = CInt(stepnode.Attributes("WaitLoopsAfterOn").InnerText)
                                animationstep.Off = stepnode.Attributes("Off").InnerText
                                animationstep.WaitLoopsAfterOff = CInt(stepnode.Attributes("WaitLoopsAfterOff").InnerText)
                                If stepnode.Attributes("PulseSwitch") IsNot Nothing Then
                                    animationstep.PulseSwitch = CInt(stepnode.Attributes("PulseSwitch").InnerText)
                                End If
                                ani.AnimationSteps.Add(animationstep)
                            Next
                            myanimations.Add(ani)
                        Next
                    End If

                    ' get all score info
                    If topnode.SelectSingleNode("Scores") IsNot Nothing AndAlso topnode.SelectNodes("Scores/Score") IsNot Nothing Then
                        For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Scores/Score")
                            Dim score As ReelAndLED.ScoreInfo = New ReelAndLED.ScoreInfo()
                            score.ID = CInt(innerNode.Attributes("ID").InnerText)
                            score.ReelType = innerNode.Attributes("ReelType").InnerText.Replace("Dream7", "Rendered")
                            score.ReelColor = _backglassData.ReelColor
                            score.Digits = CInt(innerNode.Attributes("Digits").InnerText)
                            score.Spacing = CInt(innerNode.Attributes("Spacing").InnerText)
                            If innerNode.Attributes("DisplayState") IsNot Nothing Then score.DisplayState = CInt(innerNode.Attributes("DisplayState").InnerText)
                            If innerNode.Attributes("ReelColor") IsNot Nothing Then score.ReelColor = String2Color(innerNode.Attributes("ReelColor").InnerText.Replace(";", "."))
                            If innerNode.Attributes("B2SStartDigit") IsNot Nothing Then score.B2SStartDigit = CInt(innerNode.Attributes("B2SStartDigit").InnerText)
                            If innerNode.Attributes("B2SScoreType") IsNot Nothing Then score.B2SScoreType = CInt(innerNode.Attributes("B2SScoreType").InnerText)
                            If innerNode.Attributes("B2SPlayerNo") IsNot Nothing Then score.B2SPlayerNo = CInt(innerNode.Attributes("B2SPlayerNo").InnerText)
                            If innerNode.Attributes("ReelIlluLocation") IsNot Nothing Then score.ReelIlluLocation = CInt(innerNode.Attributes("ReelIlluLocation").InnerText)
                            If innerNode.Attributes("ReelIlluB2SID") IsNot Nothing Then score.ReelIlluB2SID = CInt(innerNode.Attributes("ReelIlluB2SID").InnerText)
                            If innerNode.Attributes("ReelIlluB2SIDType") IsNot Nothing Then score.ReelIlluB2SIDType = CInt(innerNode.Attributes("ReelIlluB2SIDType").InnerText)
                            If innerNode.Attributes("ReelIlluB2SValue") IsNot Nothing Then score.ReelIlluB2SValue = CInt(innerNode.Attributes("ReelIlluB2SValue").InnerText)
                            If innerNode.Attributes("ReelIlluIntensity") IsNot Nothing Then score.ReelIlluIntensity = CInt(innerNode.Attributes("ReelIlluIntensity").InnerText)
                            score.Location = New Point(CInt(innerNode.Attributes("LocX").InnerText), CInt(innerNode.Attributes("LocY").InnerText))
                            score.Size = New Size(CInt(innerNode.Attributes("Width").InnerText), CInt(innerNode.Attributes("Height").InnerText))
                            If score.Size.Width < 10 Then score.Size.Width = 10
                            If score.Size.Height < 10 Then score.Size.Height = 10
                            If innerNode.Attributes("Parent") IsNot Nothing AndAlso innerNode.Attributes("Parent").InnerText.Equals("DMD") Then
                                score.ParentForm = eParentForm.DMD
                                mydmdscores.Add(score.ID, score)
                            Else
                                score.ParentForm = eParentForm.Backglass
                                myscores.Add(score.ID, score)
                            End If
                        Next
                    End If

                    ' get all illumination info
                    If topnode.SelectSingleNode("Illumination") IsNot Nothing AndAlso topnode.SelectNodes("Illumination/Bulb") IsNot Nothing Then
                        Dim usedfonts As String = String.Empty
                        For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Illumination/Bulb")
                            Dim bulb As Illumination.BulbInfo = New Illumination.BulbInfo()
                            bulb.ID = CInt(innerNode.Attributes("ID").InnerText)
                            bulb.Name = innerNode.Attributes("Name").InnerText
                            bulb.Text = innerNode.Attributes("Text").InnerText
                            bulb.TextAlignment = innerNode.Attributes("TextAlignment").InnerText
                            bulb.FontName = innerNode.Attributes("FontName").InnerText
                            If Not String.IsNullOrEmpty(bulb.FontName) Then If Not usedfonts.Contains(bulb.FontName & ",") Then usedfonts &= bulb.FontName & ","
                            bulb.FontSize = CSng(innerNode.Attributes("FontSize").InnerText)
                            If bulb.FontSize >= 100 Then bulb.FontSize = bulb.FontSize / 100
                            bulb.FontStyle = CInt(innerNode.Attributes("FontStyle").InnerText)
                            bulb.Visible = (CInt(innerNode.Attributes("Visible").InnerText) = 1)
                            bulb.Location = New Point(CInt(innerNode.Attributes("LocX").InnerText), CInt(innerNode.Attributes("LocY").InnerText))
                            bulb.Size = New Size(CInt(innerNode.Attributes("Width").InnerText), CInt(innerNode.Attributes("Height").InnerText))
                            If bulb.Size.Width < 10 Then bulb.Size.Width = 10
                            If bulb.Size.Height < 10 Then bulb.Size.Height = 10
                            bulb.InitialState = CInt(innerNode.Attributes("InitialState").InnerText)
                            If innerNode.Attributes("DualMode") IsNot Nothing Then
                                bulb.DualMode = CInt(innerNode.Attributes("DualMode").InnerText)
                            End If
                            bulb.Intensity = CInt(innerNode.Attributes("Intensity").InnerText)
                            If innerNode.Attributes("LightColor") IsNot Nothing Then bulb.LightColor = String2Color(innerNode.Attributes("LightColor").InnerText)
                            If innerNode.Attributes("DodgeColor") IsNot Nothing Then bulb.DodgeColor = String2Color(innerNode.Attributes("DodgeColor").InnerText)
                            If innerNode.Attributes("ZOrder") IsNot Nothing Then bulb.ZOrder = CInt(innerNode.Attributes("ZOrder").InnerText)
                            bulb.IsImageSnippit = (innerNode.Attributes("IsImageSnippit").InnerText = "1")
                            If bulb.IsImageSnippit Then bulb.Image = Base64ToImage(innerNode.Attributes("Image").InnerText)
                            If innerNode.Attributes("SnippitType") IsNot Nothing Then
                                bulb.SnippitInfo.SnippitType = CInt(innerNode.Attributes("SnippitType").InnerText)
                                If bulb.SnippitInfo.SnippitType <> eSnippitType.StandardImage Then
                                    If innerNode.Attributes("SnippitRotatingSteps") IsNot Nothing Then
                                        bulb.SnippitInfo.SnippitRotatingSteps = CInt(innerNode.Attributes("SnippitRotatingSteps").InnerText)
                                    ElseIf innerNode.Attributes("SnippitRotatingAngle") IsNot Nothing Then
                                        bulb.SnippitInfo.SnippitRotatingSteps = CInt(360 / CInt(innerNode.Attributes("SnippitRotatingAngle").InnerText))
                                    End If
                                    If innerNode.Attributes("SnippitRotatingDirection") IsNot Nothing Then
                                        bulb.SnippitInfo.SnippitRotatingDirection = CInt(innerNode.Attributes("SnippitRotatingDirection").InnerText)
                                    End If
                                    If innerNode.Attributes("SnippitRotatingStopBehaviour") IsNot Nothing Then
                                        bulb.SnippitInfo.SnippitRotatingStopBehaviour = CInt(innerNode.Attributes("SnippitRotatingStopBehaviour").InnerText)
                                    End If
                                    If innerNode.Attributes("SnippitRotatingInterval") IsNot Nothing Then
                                        bulb.SnippitInfo.SnippitRotatingInterval = CInt(innerNode.Attributes("SnippitRotatingInterval").InnerText)
                                    End If
                                End If
                            End If
                            If innerNode.Attributes("B2SID") IsNot Nothing Then bulb.B2SID = CInt(innerNode.Attributes("B2SID").InnerText)
                            If innerNode.Attributes("B2SIDType") IsNot Nothing Then bulb.B2SIDType = CInt(innerNode.Attributes("B2SIDType").InnerText)
                            If innerNode.Attributes("B2SValue") IsNot Nothing Then bulb.B2SValue = CInt(innerNode.Attributes("B2SValue").InnerText)
                            If bulb.IsImageSnippit AndAlso bulb.SnippitInfo.SnippitType = eSnippitType.MechRotatingImage Then
                                If innerNode.Attributes("RomID") IsNot Nothing Then bulb.SnippitInfo.SnippitMechID = CInt(innerNode.Attributes("RomID").InnerText)
                            Else
                                If innerNode.Attributes("RomID") IsNot Nothing Then bulb.RomID = CInt(innerNode.Attributes("RomID").InnerText)
                                If innerNode.Attributes("RomIDType") IsNot Nothing Then bulb.RomIDType = CInt(innerNode.Attributes("RomIDType").InnerText)
                            End If
                            If innerNode.Attributes("RomInverted") IsNot Nothing Then bulb.RomInverted = (innerNode.Attributes("RomInverted").InnerText = "1")
                            If innerNode.Attributes("Parent") IsNot Nothing AndAlso innerNode.Attributes("Parent").InnerText.Equals("DMD") Then
                                bulb.ParentForm = eParentForm.DMD
                                mydmdbulbs.Insert(mydmdbulbs.Count, bulb)
                            Else
                                bulb.ParentForm = eParentForm.Backglass
                                mybulbs.Insert(mybulbs.Count, bulb)
                            End If
                        Next
                        ' check fonts
                        If Not String.IsNullOrEmpty(usedfonts) Then
                            Dim notfound As String = String.Empty
                            Dim fonts As InstalledFontCollection = New InstalledFontCollection()
                            For Each usedfont As String In usedfonts.Split(",")
                                If Not String.IsNullOrEmpty(usedfont) Then
                                    If Not IsFontInstalled(usedfont) Then
                                        notfound &= ", '" & usedfont & "'"
                                    End If
                                    'Dim found As Boolean = False
                                    'For Each fontfamily As FontFamily In fonts.Families
                                    '    If usedfont.Equals(fontfamily.Name, StringComparison.CurrentCultureIgnoreCase) Then
                                    '        found = True
                                    '        Exit For
                                    '    End If
                                    'Next
                                    'If Not found Then
                                    '    If Not String.IsNullOrEmpty(notfound) Then notfound &= ","
                                    '    notfound &= usedfont
                                    'End If
                                End If
                            Next
                            fonts.Dispose()
                            ' msg box for all not found fonts
                            If Not String.IsNullOrEmpty(notfound) Then
                                MessageBox.Show(String.Format(My.Resources.MSG_FontNotFound, notfound.Substring(2)), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                        End If
                    End If

                    ' get standard thumbnail image
                    If topnode.SelectSingleNode("Images/ThumbnailImage") IsNot Nothing Then
                        .ThumbnailImage = Base64ToImage(topnode.SelectSingleNode("Images/ThumbnailImage").Attributes("Value").InnerText)
                    End If

                    ' get main images
                    Dim offImage As Image = Nothing
                    Dim offImageRomID As Integer = 0
                    Dim offImageRomIDType As eRomIDType = eRomIDType.NotUsed
                    Dim onImageRomID As Integer = 0
                    Dim onImageRomIDType As eRomIDType = eRomIDType.NotUsed
                    If topnode.SelectSingleNode("Images/BackglassImage") IsNot Nothing Then
                        .Image = Base64ToImage(topnode.SelectSingleNode("Images/BackglassImage").Attributes("Value").InnerText)
                        .ImageFileName = topnode.SelectSingleNode("Images/BackglassImage").Attributes("FileName").InnerText
                    ElseIf topnode.SelectSingleNode("Images/BackglassOnImage") IsNot Nothing Then
                        offImage = Base64ToImage(topnode.SelectSingleNode("Images/BackglassOffImage").Attributes("Value").InnerText)
                        If topnode.SelectSingleNode("Images/BackglassOffImage").Attributes("RomID") IsNot Nothing Then
                            offImageRomID = CInt(topnode.SelectSingleNode("Images/BackglassOffImage").Attributes("RomID").InnerText)
                            offImageRomIDType = CInt(topnode.SelectSingleNode("Images/BackglassOffImage").Attributes("RomIDType").InnerText)
                        End If
                        .Image = Base64ToImage(topnode.SelectSingleNode("Images/BackglassOnImage").Attributes("Value").InnerText)
                        .ImageFileName = topnode.SelectSingleNode("Images/BackglassOnImage").Attributes("FileName").InnerText
                        If topnode.SelectSingleNode("Images/BackglassOnImage").Attributes("RomID") IsNot Nothing Then
                            onImageRomID = CInt(topnode.SelectSingleNode("Images/BackglassOnImage").Attributes("RomID").InnerText)
                            onImageRomIDType = CInt(topnode.SelectSingleNode("Images/BackglassOnImage").Attributes("RomIDType").InnerText)
                        End If
                    End If
                    If topnode.SelectSingleNode("Images/DMDImage") IsNot Nothing Then
                        .DMDImage = Base64ToImage(topnode.SelectSingleNode("Images/DMDImage").Attributes("Value").InnerText)
                        .DMDImageFileName = topnode.SelectSingleNode("Images/DMDImage").Attributes("FileName").InnerText
                    End If
                    ' maybe set off image ROM values to on values
                    If offImageRomIDType = eRomIDType.NotUsed Then
                        offImageRomID = onImageRomID
                        offImageRomIDType = onImageRomIDType
                    End If
                    ' images to image collection
                    myimages.Init()
                    If .Image IsNot Nothing Then
                        Dim mainimageinfo As Images.ImageInfo = New Images.ImageInfo(Images.eImageInfoType.BackgroundImage, .ImageFileName, .Image)
                        mainimageinfo.BackgroundImageType = Images.eBackgroundImageType.On
                        mainimageinfo.RomID = onImageRomID
                        mainimageinfo.RomIDType = onImageRomIDType
                        myimages.Insert(Images.eImageInfoType.Title4BackgroundImages, mainimageinfo)
                    End If
                    If offImage IsNot Nothing Then
                        Dim offimageinfo As Images.ImageInfo = New Images.ImageInfo(Images.eImageInfoType.BackgroundImage, "", offImage)
                        offimageinfo.BackgroundImageType = Images.eBackgroundImageType.Off
                        offimageinfo.RomID = offImageRomID
                        offimageinfo.RomIDType = offImageRomIDType
                        myimages.Insert(Images.eImageInfoType.Title4BackgroundImages, offimageinfo)
                    End If
                    If topnode.SelectSingleNode("Images/IlluminationImage") IsNot Nothing Then
                        myimages.Insert(Images.eImageInfoType.Title4IlluminationImages, New Images.ImageInfo(Images.eImageInfoType.IlluminationImage, "", Base64ToImage(topnode.SelectSingleNode("Images/IlluminationImage").Attributes("Value").InnerText)))
                    End If
                    If .DMDImage IsNot Nothing Then
                        myimages.Insert(Images.eImageInfoType.Title4DMDImages, New Images.ImageInfo(Images.eImageInfoType.DMDImage, .DMDImageFileName, .DMDImage))
                    End If
                    For Each bulb As Illumination.BulbInfo In mybulbs
                        With bulb
                            If .IsImageSnippit AndAlso .Image IsNot Nothing Then
                                Dim imageinfo As Images.ImageInfo = New Images.ImageInfo(Images.eImageInfoType.IlluminationSnippits)
                                imageinfo.Text = .Name
                                imageinfo.Image = .Image
                                myimages.Insert(Images.eImageInfoType.Title4IlluminationSnippits, imageinfo)
                            End If
                        End With
                    Next
                End With

            End If

        End If

        Return ret

    End Function


    ' private methods

    Private Function CheckData() As Boolean
        Dim ret As Boolean = True

        ' check B2S start digits of score displays
        If ret Then
            ret = Not IsOverlappingB2SStartDigit()
            If Not ret Then
                MessageBox.Show(My.Resources.MSG_CheckB2SStartDigit, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        ' check LED types
        If ret Then
            For Each score As ReelAndLED.ScoreInfo In Backglass.currentScores
                If String.IsNullOrEmpty(score.ReelType) Then
                    Backglass.currentTabPage.ShowScoreFrames = True
                    MessageBox.Show(My.Resources.MSG_CheckB2SScoreTypes, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ret = False
                    Exit For
                End If
            Next
        End If

        ' check grill height
        If ret Then
            If Backglass.currentData.DMDType = eDMDType.B2SAlwaysOnSecondMonitor OrElse Backglass.currentData.DMDType = eDMDType.B2SOnSecondOrThirdMonitor Then
                If Backglass.currentData.GrillHeight <= 0 Then
                    If MessageBox.Show(My.Resources.MSG_CheckNoGrillHeight, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        ret = False
                    End If
                End If
            End If
        End If

        Return ret
    End Function

    Private Function CreateIntermediates(ByVal currentReelImage As Image,
                                         ByVal lastReelImage As Image,
                                         ByVal countOfIntermediates As Integer,
                                         ByVal rollingUp As Boolean) As Image()
        Dim ret As Image() = Nothing
        ReDim ret(countOfIntermediates - 1)
        Dim intermediatesize As Size = New Size(Math.Max(currentReelImage.Size.Width, lastReelImage.Size.Width), Math.Max(currentReelImage.Size.Height, lastReelImage.Size.Height))
        Dim currentcut As Single = currentReelImage.Size.Height / (countOfIntermediates + 1)
        Dim currentcutlocation As Single = currentcut
        Dim lastcut As Single = lastReelImage.Size.Height / (countOfIntermediates + 1)
        Dim lastcutlocation As Single = lastcut
        For i As Integer = 0 To countOfIntermediates - 1
            Dim lastimage As Image = Nothing
            Dim currentimage As Image = Nothing
            Dim intermediateimage As Image = New Bitmap(intermediatesize.Width, intermediatesize.Height)
            Dim currentcutlocationI As Integer = Math.Truncate(currentcutlocation)
            Dim lastcutlocationI As Integer = Math.Truncate(lastcutlocation)
            If rollingUp Then
                currentimage = currentReelImage.PartFromImage(New Rectangle(New Point(0, 0), New Size(currentReelImage.Size.Width, currentcutlocationI)))
                lastimage = lastReelImage.PartFromImage(New Rectangle(New Point(0, lastcutlocationI), New Size(lastReelImage.Size.Width, lastReelImage.Size.Height - lastcutlocationI)))
                Using gr As Graphics = Graphics.FromImage(intermediateimage)
                    gr.PageUnit = GraphicsUnit.Pixel
                    gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    gr.DrawImage(lastimage, New Rectangle(0, 0, lastimage.Width, lastimage.Height))
                    gr.DrawImage(currentimage, New Rectangle(0, lastimage.Height, currentimage.Width, currentimage.Height))
                End Using
            Else
                currentimage = currentReelImage.PartFromImage(New Rectangle(New Point(0, currentReelImage.Size.Height - currentcutlocationI), New Size(currentReelImage.Size.Width, currentcutlocationI)))
                lastimage = lastReelImage.PartFromImage(New Rectangle(New Point(0, 0), New Size(lastReelImage.Size.Width, lastReelImage.Size.Height - lastcutlocationI)))
                Using gr As Graphics = Graphics.FromImage(intermediateimage)
                    gr.PageUnit = GraphicsUnit.Pixel
                    gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    gr.DrawImage(currentimage, New Rectangle(0, 0, currentimage.Width, currentimage.Height))
                    gr.DrawImage(lastimage, New Rectangle(0, currentimage.Height, lastimage.Width, lastimage.Height))
                End Using
            End If
            ret(i) = intermediateimage
            lastimage.Dispose()
            currentimage.Dispose()
            lastimage = Nothing
            currentimage = Nothing
            currentcutlocation += currentcut
            lastcutlocation += lastcut
        Next
        Return ret
    End Function
    Private Sub AddIntermediates(ByVal nodeReelImage As Xml.XmlElement,
                                 ByVal nodeReelIlluImage As Xml.XmlElement(),
                                 ByVal reelImage As Image,
                                 ByVal lastReelImage As Image,
                                 ByVal countOfIntermediates As Integer,
                                 ByVal rollingUp As Boolean,
                                 ByVal scoreimagesets As Generic.Dictionary(Of String, Integer))
        Dim size As Size = reelImage.Size
        Dim cutat As Integer = CInt(size.Height / (countOfIntermediates + 1))
        Dim currentcutat As Integer = cutat
        For i As Integer = 0 To (countOfIntermediates - 1)
            Dim lastimage As Image = Nothing
            Dim currentimage As Image = Nothing
            Dim intermediateimage As Image = New Bitmap(size.Width, size.Height)
            If rollingUp Then
                lastimage = lastReelImage.PartFromImage(New Rectangle(New Point(0, currentcutat), New Size(size.Width, size.Height - currentcutat)))
                currentimage = reelImage.PartFromImage(New Rectangle(New Point(0, 0), New Size(size.Width, currentcutat)))
                Using gr As Graphics = Graphics.FromImage(intermediateimage)
                    gr.PageUnit = GraphicsUnit.Pixel
                    gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    gr.DrawImage(lastimage, New Rectangle(0, 0, lastimage.Width, lastimage.Height))
                    gr.DrawImage(currentimage, New Rectangle(0, lastimage.Height, currentimage.Width, currentimage.Height))
                End Using
            Else
                currentimage = reelImage.PartFromImage(New Rectangle(New Point(0, size.Height - currentcutat), New Size(size.Width, currentcutat)))
                lastimage = lastReelImage.PartFromImage(New Rectangle(New Point(0, 0), New Size(size.Width, size.Height - currentcutat)))
                Using gr As Graphics = Graphics.FromImage(intermediateimage)
                    gr.PageUnit = GraphicsUnit.Pixel
                    gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    gr.DrawImage(currentimage, New Rectangle(0, 0, currentimage.Width, currentimage.Height))
                    gr.DrawImage(lastimage, New Rectangle(0, currentimage.Height, lastimage.Width, lastimage.Height))
                End Using
            End If
            nodeReelImage.SetAttribute("IntermediateImage" & (i + 1).ToString(), ImageToBase64(intermediateimage))
            For j As Integer = 0 To nodeReelIlluImage.Count - 1
                Dim key As String = scoreimagesets.Keys(j)
                Dim keyinfo As String() = key.Split(Chr(1))
                Dim reelintensity As Integer = CInt(keyinfo(keyinfo.Length - 2))
                Dim reelillulocation As Integer = CInt(keyinfo(keyinfo.Length - 1))
                nodeReelIlluImage(j).SetAttribute("IntermediateImage" & (i + 1).ToString(), ImageToBase64(Backglass.currentTabPage.DrawIlluminatedReelImage(intermediateimage, reelintensity, reelillulocation)))
            Next
            intermediateimage.Dispose()
            lastimage.Dispose()
            currentimage.Dispose()
            intermediateimage = Nothing
            lastimage = Nothing
            currentimage = Nothing
            currentcutat += cutat
        Next
    End Sub

#End Region


#Region "Visual Studio stuff"

    ' main method(s)

    Public Function CreateVisualStudioCode() As Boolean

        Dim ret As Boolean = True

        RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(0))

        Dim currentDir As String = IO.Directory.GetCurrentDirectory()

        Dim projectname As String = Backglass.currentData.Name
        Dim rootnamespace As String = Backglass.currentData.SecuredName
        Dim assemblyname As String = Backglass.currentData.VSName
        Dim sortedScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo) = New Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo)
        Dim sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo) = New Generic.SortedList(Of Integer, Illumination.BulbInfo)
        Dim sortedDMDScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo) = New Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo)
        Dim sortedDMDBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo) = New Generic.SortedList(Of Integer, Illumination.BulbInfo)

        ' add all score display and blub info to the sorted lists
        SortScoresAndBulbs(sortedScores, sortedBulbs, sortedDMDScores, sortedDMDBulbs)

        ' directories for backglass VS files
        Dim pathSolution As String = ProjectPath
        Dim pathVBProj As String = IO.Path.Combine(pathSolution, projectname)
        Dim pathMyProject As String = IO.Path.Combine(pathVBProj, "My Project")
        Dim pathClasses As String = IO.Path.Combine(pathVBProj, "Classes")
        Dim pathControls As String = IO.Path.Combine(pathVBProj, "Controls")
        Dim pathResources As String = IO.Path.Combine(pathVBProj, "Resources")
        Dim pathDream7 As String = IO.Path.Combine(pathVBProj, "Dream7")

        ' maybe reengineer the already existing visual studio code
        Dim codelines As Generic.SortedList(Of Integer, String) = Nothing
        If IO.Directory.Exists(pathVBProj) Then
            IO.Directory.SetCurrentDirectory(pathVBProj)
            If IO.File.Exists("formBackglass.vb") Then
                codelines = ReengineerCode()
            End If
        End If
        CheckDataCodeLines(codelines)

        Try

            ' create directories for backglass VS files
            IO.Directory.CreateDirectory(pathMyProject)
            IO.Directory.CreateDirectory(pathClasses)
            IO.Directory.CreateDirectory(pathControls)
            IO.Directory.CreateDirectory(pathResources)
            IO.Directory.CreateDirectory(pathDream7)

            ' create solution file
            IO.Directory.SetCurrentDirectory(pathSolution)
            ResCopy(My.Resources.B2SSolution_sln, projectname & ".sln")
            ResRename("<<projectguid1>>", Backglass.currentData.ProjectGUID, projectname & ".sln")
            ResRename("<<projectguid2>>", Backglass.currentData.ProjectGUID2, projectname & ".sln")
            ResRename("<<name>>", projectname, projectname & ".sln")

            ' create project file
            IO.Directory.SetCurrentDirectory(pathVBProj)
            ResCopy(My.Resources.B2SSolution_vbproj, projectname & ".vbproj")
            ResRename("<<projectguid1>>", Backglass.currentData.ProjectGUID, projectname & ".vbproj")
            ResRename("<<rootnamespace>>", rootnamespace, projectname & ".vbproj")
            ResRename("<<assemblyname>>", assemblyname, projectname & ".vbproj")
            ResRename("<<formDMD1>>", DMD2Code1(projectname), projectname & ".vbproj")
            ResRename("<<formDMD2>>", DMD2Code2(projectname), projectname & ".vbproj")
            ResRename("<<Main>>", MainImage2Code1(), projectname & ".vbproj")
            ResRename("<<Resources>>", ResourceImage2Code1(), projectname & ".vbproj")
            ' create main form files
            ResCopy(My.Resources.formBackglass_vb, "formBackglass.vb")
            ResRename("<<BSVD>>", EngineerCode(codelines, sortedScores, sortedBulbs, sortedDMDScores, sortedDMDBulbs, , True), "formBackglass.vb")
            ResRename("<<BSLC>>", EngineerCode(codelines, sortedScores, sortedBulbs, sortedDMDScores, sortedDMDBulbs, , True), "formBackglass.vb")
            ResRename("<<BSC>>", EngineerCode(codelines, sortedScores, sortedBulbs, sortedDMDScores, sortedDMDBulbs, , , True, Backglass.currentData.AddEMDefaults), "formBackglass.vb")
            ResRename("<<formDMD>>", If(Backglass.currentData.IsDMDImageAvailable, "Private formDMD As formDMD = New formDMD()" & vbCrLf & vbCrLf, ""), "formBackglass.vb")
            ResRename("<<B2SSystem>>", Backglass.currentData.B2SDataCount.ToString(), "formBackglass.vb")
            ResRename("<<B2SScreen>>", B2SScreenCode(), "formBackglass.vb")
            ResRename("<<B2SLED>>", "B2SLED.Start()", "formBackglass.vb")
            ResRename("<<B2SAnimation>>", B2SAnimationCode(), "formBackglass.vb")
            ResRename("<<B2SSystem_DataIsSent>>", GenerateDataCode(sortedScores, sortedBulbs, sortedDMDScores, sortedDMDBulbs), "formBackglass.vb")
            ResCopy(My.Resources.formBackglass_Designer_vb, "formBackglass.Designer.vb")
            ResRename("<<code1>>", PictureBox2DesignerCode1(rootnamespace, sortedScores, sortedDMDScores, sortedBulbs), "formBackglass.Designer.vb")
            ResRename("<<code2>>", PictureBox2DesignerCode2(rootnamespace, sortedScores, sortedDMDScores, sortedBulbs), "formBackglass.Designer.vb")
            ResRename("<<code3>>", PictureBox2DesignerCode3(rootnamespace, sortedScores, sortedDMDScores, sortedBulbs), "formBackglass.Designer.vb")
            ResRename("<<code4>>", PictureBox2DesignerCode4(rootnamespace, sortedScores, sortedDMDScores, sortedBulbs), "formBackglass.Designer.vb")
            ResRename("<<backgroundimage>>", PictureBox2DesignerCode5(rootnamespace, False), "formBackglass.Designer.vb")
            ResRename("<<SizeWidth>>", Backglass.currentData.Image.Size.Width.ToString(), "formBackglass.Designer.vb")
            ResRename("<<SizeHeight>>", Backglass.currentData.Image.Size.Height.ToString(), "formBackglass.Designer.vb")
            ResCopy(My.Resources.formBackglass_resx, "formBackglass.resx")
            ResRename("<<code>>", Image2Base64(sortedBulbs, False), "formBackglass.resx")
            ' maybe create DMD form files
            If Backglass.currentData.IsDMDImageAvailable Then
                ResCopy(My.Resources.formDMD_vb, "formDMD.vb")
                ResCopy(My.Resources.formDMD_Designer_vb, "formDMD.Designer.vb")
                ResRename("<<code1>>", PictureBox2DesignerCode1(rootnamespace, sortedDMDScores, sortedScores, sortedDMDBulbs), "formDMD.Designer.vb")
                ResRename("<<code2>>", PictureBox2DesignerCode2(rootnamespace, sortedDMDScores, sortedScores, sortedDMDBulbs), "formDMD.Designer.vb")
                ResRename("<<code3>>", PictureBox2DesignerCode3(rootnamespace, sortedDMDScores, sortedScores, sortedDMDBulbs), "formDMD.Designer.vb")
                ResRename("<<code4>>", PictureBox2DesignerCode4(rootnamespace, sortedDMDScores, sortedScores, sortedDMDBulbs), "formDMD.Designer.vb")
                ResRename("<<backgroundimage>>", PictureBox2DesignerCode5(rootnamespace, True), "formDMD.Designer.vb")
                ResRename("<<SizeWidth>>", Backglass.currentData.DMDImage.Size.Width.ToString(), "formDMD.Designer.vb")
                ResRename("<<SizeHeight>>", Backglass.currentData.DMDImage.Size.Height.ToString(), "formDMD.Designer.vb")
                ResCopy(My.Resources.formDMD_resx, "formDMD.resx")
                ResRename("<<code>>", Image2Base64(sortedDMDBulbs, True), "formDMD.resx")
            End If

            ' create 'My Project' files
            IO.Directory.SetCurrentDirectory(pathMyProject)
            ResCopy(My.Resources.Application_myapp, "Application.myapp")
            ResCopy(My.Resources.Resources_resx, "Resources.resx")
            ResRename("<<Main>>", MainImage2Code2(), "Resources.resx")
            ResRename("<<Resources>>", ResourceImage2Code2(), "Resources.resx")
            ResCopy(My.Resources.Application_Designer_vb, "Application.Designer.vb")
            ResRename("<<rootnamespace>>", rootnamespace, "Application.Designer.vb")
            ResCopy(My.Resources.AssemblyInfo_vb, "AssemblyInfo.vb")
            ResRename("<<assemblyguid>>", Backglass.currentData.AssemblyGUID, "AssemblyInfo.vb")
            ResRename("<<rootnamespace>>", rootnamespace, "AssemblyInfo.vb")
            ResCopy(My.Resources.Resources_Designer_vb, "Resources.Designer.vb")
            ResRename("<<rootnamespace>>", rootnamespace, "Resources.Designer.vb")
            ResRename("<<Main>>", MainImage2Code3, "Resources.Designer.vb")
            ResRename("<<Resources>>", ResourceImage2Code3, "Resources.Designer.vb")
            ResCopy(My.Resources.Settings_Designer_vb, "Settings.Designer.vb")
            ResRename("<<rootnamespace>>", rootnamespace, "Settings.Designer.vb")
            ResCopy(My.Resources.Settings_settings, "Settings.settings")

            ' create 'Classes' files
            IO.Directory.SetCurrentDirectory(pathClasses)
            ResCopy(My.Resources.B2SData_vb, "B2SData.vb")
            ResCopy(My.Resources.B2SSystem_vb, "B2SSystem.vb")
            ResCopy(My.Resources.B2SScreen_vb, "B2SScreen.vb")
            ResCopy(My.Resources.B2SLED_vb, "B2SLED.vb")
            ResCopy(My.Resources.B2SAnimation_vb, "B2SAnimation.vb")

            ' create 'Controls' files
            IO.Directory.SetCurrentDirectory(pathControls)
            ResCopy(My.Resources.B2SBaseBox_vb, "B2SBaseBox.vb")
            ResCopy(My.Resources.B2SPictureBox_vb, "B2SPictureBox.vb")
            ResCopy(My.Resources.B2SLEDBox_vb, "B2SLEDBox.vb")
            ResCopy(My.Resources.B2SReelBox_vb, "B2SReelBox.vb")

            ' maybe create resources' files
            If Backglass.currentData.IsResourceFileNeeded Then
                IO.Directory.SetCurrentDirectory(pathResources)
                ResResourceImagesCopy()
                ResResourceSoundsCopy()
            End If

            ' create dream 7 LED files
            IO.Directory.SetCurrentDirectory(pathDream7)
            ResCopy(My.Resources.Segment_vb, "Segment.vb")
            ResCopy(My.Resources.SegmentDisplay_vb, "SegmentDisplay.vb")
            ResCopy(My.Resources.SegmentNumber_vb, "SegmentNumber.vb")

            ' get to the starting working dir
            IO.Directory.SetCurrentDirectory(currentDir)

            RaiseEvent ReportProgress(Me, New CodingProgressEventArgs(100))

        Catch ex As Exception

            MessageBox.Show(ex.Message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Return ret

    End Function


    ' private methods

    Private Sub SortScoresAndBulbs(ByRef sortedScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                   ByRef sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo),
                                   ByRef sortedDMDScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                   ByRef sortedDMDBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo))
        sortedScores.Clear()
        sortedBulbs.Clear()
        sortedDMDScores.Clear()
        sortedDMDBulbs.Clear()
        For Each score As ReelAndLED.ScoreInfo In Backglass.currentData.Scores
            sortedScores.Add(score.ID, score)
        Next
        For Each bulb As Illumination.BulbInfo In Backglass.currentData.Bulbs
            sortedBulbs.Add(bulb.ID, bulb)
        Next
        For Each score As ReelAndLED.ScoreInfo In Backglass.currentData.DMDScores
            sortedDMDScores.Add(score.ID, score)
        Next
        For Each bulb As Illumination.BulbInfo In Backglass.currentData.DMDBulbs
            sortedDMDBulbs.Add(bulb.ID, bulb)
        Next
    End Sub

    Private Function ReengineerCode() As Generic.SortedList(Of Integer, String)
        Dim codelines As Generic.SortedList(Of Integer, String) = New Generic.SortedList(Of Integer, String)
        Dim inVariableBlock As Boolean = False
        Dim inLoadBlock As Boolean = False
        Dim inDataBlock As Boolean = False
        Dim i As Integer = 0
        Using sr As IO.StreamReader = IO.File.OpenText("formBackglass.vb")
            Do While Not sr.EndOfStream
                Dim line As String = sr.ReadLine()
                If line.Trim.StartsWith("' ***BSVDS*") Then
                    inVariableBlock = True
                    i = 1
                ElseIf line.Trim.StartsWith("' ***BSVDE*") Then
                    inVariableBlock = False
                ElseIf line.Trim.StartsWith("' ***BSLCS*") Then
                    inLoadBlock = True
                    i = 100001
                ElseIf line.Trim.StartsWith("' ***BSLCE*") Then
                    inLoadBlock = False
                ElseIf line.Trim.StartsWith("' ***BSCS*") Then
                    inDataBlock = True
                    i = 200001
                ElseIf line.Trim.StartsWith("' ***BSCE*") Then
                    inDataBlock = False
                ElseIf inVariableBlock OrElse inLoadBlock OrElse inDataBlock Then
                    codelines.Add(i, line)
                    i += 1
                End If
            Loop
        End Using
        Return codelines
    End Function
    Private Function EngineerCode(ByRef codelines As Generic.SortedList(Of Integer, String),
                                  ByVal sortedScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                  ByVal sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo),
                                  ByVal sortedDMDScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                  ByVal sortedDMDBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo),
                                  Optional ByVal giveVariableBlock As Boolean = False,
                                  Optional ByVal giveLoadBlock As Boolean = False,
                                  Optional ByVal giveDataBlock As Boolean = False,
                                  Optional ByVal giveEMDefaultCode As Boolean = False) As String
        Dim sb As StringBuilder = New StringBuilder()
        If codelines IsNot Nothing Then
            For Each line As KeyValuePair(Of Integer, String) In codelines
                If line.Key >= 1 AndAlso line.Key <= 99999 AndAlso giveVariableBlock Then
                    sb.AppendLine(line.Value)
                ElseIf line.Key >= 100001 AndAlso line.Key <= 199999 AndAlso giveLoadBlock Then
                    sb.AppendLine(line.Value)
                ElseIf line.Key >= 200001 AndAlso line.Key <= 299999 AndAlso giveDataBlock Then
                    sb.AppendLine(line.Value)
                End If
            Next
        End If
        If giveDataBlock AndAlso Not sb.ToString.Contains("Private Sub B2SDataIsSent(e As B2SSystem.B2SSystemEventArgs)") Then
            If giveEMDefaultCode Then
                sb.AppendLine()
                sb.AppendLine("    Private Sub B2SDataIsSent(e As B2SSystem.B2SSystemEventArgs)")
                sb.AppendLine()
                sb.AppendLine("        ' 1-24: score reels")
                For i As Integer = 1 To 4
                    For j = 1 To 6
                        Dim index As Integer = (i - 1) * 6 + j
                        sb.AppendLine("        If e.IsValueChanged(" & index.ToString() & ") Then")
                        sb.Append("            ' code for score reel " & j.ToString() & " for player " & i.ToString() & If(IsScoreIndexExisting(index, sortedScores, sortedDMDScores), "; normally no need to do anything here since this score reel is already handled in the autogenerated code", ""))
                        sb.AppendLine()
                        sb.AppendLine("        End If")
                    Next
                Next
                sb.AppendLine()
                sb.AppendLine("        ' 25-28: rollover for each player")
                For i As Integer = 1 To 4
                    Dim index As Integer = 24 + i
                    sb.AppendLine("        If e.IsValueChanged(" & index.ToString() & ") Then")
                    sb.Append("            ' code for rollover for player " & i.ToString() & If(IsBulbIndexExisting(index, sortedBulbs, sortedDMDBulbs), "; normally no need to do anything here since this bulb is already handled in the autogenerated code", ""))
                    sb.AppendLine()
                    sb.AppendLine("        End If")
                    sb.AppendLine()
                Next
                For i As Integer = 29 To 36
                    Dim index As Integer = i
                    Dim text As String = String.Empty
                    Select Case index
                        Case 29
                            text = "credits reel"
                        Case 30
                            text = "player up"
                        Case 31
                            text = "can play"
                        Case 32
                            text = "ball in play"
                        Case 33
                            text = "tilt"
                        Case 34
                            text = "match"
                        Case 35
                            text = "game over"
                        Case 36
                            text = "shoot again"
                    End Select
                    sb.AppendLine("        ' " & index.ToString() & ": " & text)
                    sb.AppendLine("        If e.IsValueChanged(" & index.ToString() & ") Then")
                    sb.Append("            ' code for " & text)
                    If index = 29 Then
                        If IsScoreIndexExisting(index, sortedScores, sortedDMDScores) Then
                            sb.Append("; normally no need to do anything here since this credit reel is already handled in the autogenerated code")
                        End If
                    Else
                        If IsBulbIndexExisting(index, sortedBulbs, sortedDMDBulbs) Then
                            sb.Append("; normally no need to do anything here since this bulb is already handled in the autogenerated code")
                        End If
                    End If
                    sb.AppendLine()
                    sb.AppendLine("        End If")
                    sb.AppendLine()
                Next
                sb.AppendLine("    End Sub")
                sb.AppendLine()
            Else
                sb.AppendLine()
                sb.AppendLine("    Private Sub B2SDataIsSent(e As B2SSystem.B2SSystemEventArgs)")
                sb.AppendLine()
                sb.AppendLine("    End Sub")
                sb.AppendLine()
            End If
        End If
        If sb.Length = 0 Then
            sb.AppendLine()
        End If
        If sb.Length >= 2 Then
            sb.Length -= 2
        End If
        Return sb.ToString()
    End Function
    Private Sub CheckDataCodeLines(ByRef codelines As Generic.SortedList(Of Integer, String))
        Dim sb As StringBuilder = New StringBuilder()
        If codelines IsNot Nothing Then
            For Each line As KeyValuePair(Of Integer, String) In codelines
                If line.Key >= 200001 AndAlso line.Key <= 299999 Then
                    sb.Append(line.Value)
                End If
            Next
        End If
        sb.Replace(" ", "")
        If sb.ToString.ToLower.Equals("privatesubb2sdataissent(easb2ssystem.b2ssystemeventargs)endsub") Then
            ' remove all
            For i As Integer = codelines.Keys.Count - 1 To 0 Step -1
                Dim key As Integer = codelines.Keys(i)
                If key >= 200001 AndAlso key <= 299999 Then
                    codelines.Remove(key)
                End If
            Next
        End If
    End Sub

    Private Function GenerateDataCode(ByVal sortedScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                      ByVal sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo),
                                      ByVal sortedDMDScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                      ByVal sortedDMDBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo)) As String

        Dim sb As StringBuilder = New StringBuilder()

        ' set main on or off code
        sb.AppendLine()
        sb.AppendLine("            ' main on or off image stuff")
        sb.AppendLine("            If e.IsValueChanged(0) Then")
        sb.AppendLine("                ' on or off")
        sb.AppendLine("                If e.Value(0) = 0 Then")
        sb.AppendLine("                    Me.BackgroundImage = B2SScreen.BackgroundImageOff")
        sb.AppendLine("                ElseIf e.Value(0) = 1 Then")
        sb.AppendLine("                    Me.BackgroundImage = B2SScreen.BackgroundImageOn")
        sb.AppendLine("                End If")
        sb.AppendLine()
        sb.AppendLine("                ' main on or off flashing animations")
        sb.AppendLine("            End If")

        ' add score stuff
        Dim scoreCommentAdded As Boolean = False
        For i As Integer = 0 To 1
            For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In If((i = 1), sortedDMDScores, sortedScores)
                With score.Value
                    Dim index As Integer = GetMatchingScoreIndex(.ID, sortedScores, sortedDMDScores)
                    For J As Integer = 1 To .Digits
                        If Not IsReelImageRendered(score.Value.ReelType) Then
                            If Not scoreCommentAdded Then
                                sb.AppendLine()
                                sb.AppendLine("            ' score reel stuff")
                                scoreCommentAdded = True
                            End If
                            sb.AppendLine("            If e.IsValueChanged(" & index.ToString() & ") Then")
                            sb.AppendLine("                " & If((i = 1), "formDMD", "Me") & ".ReelBox" & index.ToString("D3") & ".Text(" & If((.B2SScoreType = eB2SScoreType.Credits_29), "False", "True") & ") = e.Value(" & index.ToString() & ")")
                            sb.AppendLine("            End If")
                        End If
                        index += 1
                    Next
                End With
            Next
        Next

        ' get all illumination B2S ids sorted
        Dim b2sbulbs As SortedList(Of Integer, Integer) = New SortedList(Of Integer, Integer)
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            If bulb.Value.B2SID > 0 AndAlso Not b2sbulbs.ContainsKey(bulb.Value.B2SID) Then
                b2sbulbs.Add(bulb.Value.B2SID, 0)
            End If
        Next
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedDMDBulbs
            If bulb.Value.B2SID > 0 AndAlso Not b2sbulbs.ContainsKey(bulb.Value.B2SID) Then
                b2sbulbs.Add(bulb.Value.B2SID, 1)
            End If
        Next
        ' add illumination stuff
        Dim bulbCommentAdded As Boolean = False
        For Each b2sbulb As KeyValuePair(Of Integer, Integer) In b2sbulbs
            Dim firstentry As Boolean = True
            For i As Integer = 0 To 1
                For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In If((i = 1), sortedDMDBulbs, sortedBulbs)
                    With bulb.Value
                        If .B2SID > 0 AndAlso b2sbulb.Key = .B2SID Then
                            If Not bulbCommentAdded Then
                                sb.AppendLine()
                                sb.AppendLine("            ' illumination stuff")
                                bulbCommentAdded = True
                            End If
                            If firstentry Then
                                firstentry = False
                                If .B2SIDType <> eB2SIDType.NotUsed Then
                                    Dim idtype As String = GetB2SIDType(.B2SIDType)
                                    If Not String.IsNullOrEmpty(idtype) Then
                                        sb.AppendLine("            ' " & idtype)
                                    End If
                                End If
                                sb.AppendLine("            If e.IsValueChanged(" & .B2SID.ToString() & ") Then")
                            End If
                            If i = 1 Then
                                sb.Append("                formDMD.PictureBox")
                            Else
                                sb.Append("                Me.PictureBox")
                            End If
                            sb.Append(.ID.ToString("D3"))
                            sb.Append(".Visible = (e.Value(" & .B2SID.ToString() & ") " & If(.B2SValue > 0, "=", "<>") & " " & .B2SValue.ToString() & ")")
                            sb.AppendLine()
                        End If
                    End With
                Next
            Next
            If Not firstentry Then
                sb.AppendLine("            End If")
            End If
        Next

        ' add animation stuff
        Dim animationCommentAdded As Boolean = False
        For Each animationheader As Animation.AnimationHeader In Backglass.currentAnimations
            If Not String.IsNullOrEmpty(animationheader.IDJoin) Then
                If Not animationCommentAdded Then
                    sb.AppendLine()
                    sb.AppendLine("            ' animations")
                    animationCommentAdded = True
                End If
                For Each idJoin As String In animationheader.IDJoin.Split(",")
                    Dim inverted As Boolean = (idJoin.Substring(0, 1).ToUpper = "I")
                    If inverted Then idJoin = idJoin.Substring(1)
                    If IsNumeric(idJoin) Then
                        sb.AppendLine("            If e.IsValueChanged(" & idJoin & ") Then")
                        sb.AppendLine("                If e.Value(" & idJoin & ") " & If(inverted, "=", "<>") & " 0 Then B2SAnimation.StartAnimation(""" & animationheader.Name & """) Else B2SAnimation.StopAnimation(""" & animationheader.Name & """)")
                        sb.AppendLine("            End If")
                    End If
                Next
            End If
        Next

        Return sb.ToString()

    End Function
    Private Function GetB2SIDType(ByVal type As eB2SIDType) As String
        Dim ret As String = String.Empty
        Select Case type
            Case eB2SIDType.ScoreRolloverPlayer1_25 : ret = "score rollover for player 1"
            Case eB2SIDType.ScoreRolloverPlayer2_26 : ret = "score rollover for player 2"
            Case eB2SIDType.ScoreRolloverPlayer3_27 : ret = "score rollover for player 3"
            Case eB2SIDType.ScoreRolloverPlayer4_28 : ret = "score rollover for player 4"
            Case eB2SIDType.PlayerUp_30 : ret = "player up"
            Case eB2SIDType.CanPlay_31 : ret = "can play"
            Case eB2SIDType.BallInPlay_32 : ret = "ball in play"
            Case eB2SIDType.Tilt_33 : ret = "tilt"
            Case eB2SIDType.Match_34 : ret = "match"
            Case eB2SIDType.GameOver_35 : ret = "game over"
            Case eB2SIDType.ShootAgain_36 : ret = "shoot again"
        End Select
        Return ret
    End Function

    Private Function B2SScreenCode() As String
        Dim ret As String = String.Empty
        If Backglass.currentData.DMDImage IsNot Nothing Then
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim grillheight As Integer = Backglass.currentData.GrillHeight
            Dim defaultlocation As String = Backglass.currentData.DMDDefaultLocation.X.ToString() & ", " & Backglass.currentData.DMDDefaultLocation.Y.ToString()
            'With Backglass.currentData
            '    If grillheight <= 0 Then grillheight = .DMDImage.Height
            '    x = CInt((.Image.Width - .DMDImage.Width) / 2)
            '    y = .Image.Height - grillheight + CInt((grillheight - .DMDImage.Height) / 2)
            '    defaultlocation = x.ToString() & ", " & y.ToString()
            'End With
            Select Case Backglass.currentData.DMDType
                Case eDMDType.B2SAlwaysOnSecondMonitor
                    ret = ", formDMD, New Point(" & defaultlocation & "), B2SScreen.eDMDViewMode.ShowDMDOnlyAtDefaultLocation, " & Backglass.currentData.GrillHeight.ToString()
                Case eDMDType.B2SAlwaysOnThirdMonitor
                    ret = ", formDMD, New Point(" & defaultlocation & "), B2SScreen.eDMDViewMode.DoNotShowDMDAtDefaultLocation, " & Backglass.currentData.GrillHeight.ToString()
                Case eDMDType.B2SOnSecondOrThirdMonitor
                    ret = ", formDMD, New Point(" & defaultlocation & "), B2SScreen.eDMDViewMode.ShowDMD, " & Backglass.currentData.GrillHeight.ToString()
                Case Else
                    If Backglass.currentData.GrillHeight > 0 Then
                        ret = ", " & Backglass.currentData.GrillHeight.ToString()
                    End If
            End Select
        End If
        Return ret
    End Function

    Private Function B2SAnimationCode() As String
        Dim sb As StringBuilder = New StringBuilder()
        For Each animationheader As Animation.AnimationHeader In Backglass.currentAnimations
            sb.Append("        B2SAnimation.AddAnimation(""")
            sb.Append(animationheader.Name)
            sb.Append(""", Me, ")
            sb.Append(animationheader.Interval)
            sb.Append(", ")
            sb.Append(animationheader.Loops)
            sb.Append(", ")
            sb.Append(If(animationheader.StartAnimationAtBackglassStartup, "True", "False"))
            sb.Append(", ")
            sb.Append(If((animationheader.LightsStateAtAnimationStart = Animation.AnimationHeader.eLightsStateAtAnimationStart.LightsOff), "True", "False"))
            sb.Append(", ")
            sb.Append(CInt(animationheader.LightsStateAtAnimationEnd).ToString())
            sb.Append(", ")
            sb.Append(If(animationheader.AnimationStopBehaviour = Animation.AnimationHeader.eAnimationStopBehaviour.RunAnimationTillEnd, "True", "False"))
            sb.Append(", ")
            sb.Append(If(animationheader.LockInvolvedLamps, "True", "False"))
            sb.Append(", ")
            sb.Append(If(animationheader.HideScoreDisplays, "True", "False"))
            sb.Append(", ")
            sb.Append(If(animationheader.BringToFront, "True", "False"))
            sb.Append(", ")
            For Each animationstep As Animation.AnimationStep In animationheader.AnimationSteps
                sb.AppendLine()
                sb.Append("                                  New B2SAnimation.PictureBoxAnimationEntry(""")
                sb.Append(animationstep.On)
                sb.Append(""", ")
                sb.Append(animationstep.WaitLoopsAfterOn)
                sb.Append(", """)
                sb.Append(animationstep.Off)
                sb.Append(""", ")
                sb.Append(animationstep.WaitLoopsAfterOff)
                sb.Append("), ")
            Next
            sb.Length -= 2
            sb.AppendLine(")")
        Next
        Return sb.ToString()
    End Function

    Private Sub ResCopy(file As String, filename As String)
        If IO.File.Exists(filename) Then
            IO.File.Delete(filename)
        End If
        IO.File.WriteAllText(filename, file)
    End Sub
    Private Sub ResRename(ByVal replacefrom As String, ByVal replaceto As String, ByVal filename As String)
        Try
            ' read file
            Dim input As IO.StreamReader = IO.File.OpenText(filename)
            Dim sb As StringBuilder = New StringBuilder(input.ReadToEnd)
            sb.Replace(replacefrom, replaceto)
            input.Close()
            ' write file
            Dim output As IO.StreamWriter = New IO.StreamWriter(filename)
            output.Write(sb.ToString())
            output.Flush()
            output.Close()
        Catch ex As IO.IOException
            Throw New Exception(My.Resources.MSG_IOError)
        End Try
    End Sub

    Private Function DMD2Code1(ByVal name As String) As String
        Dim sb As StringBuilder = New StringBuilder()
        If Backglass.currentData.IsDMDImageAvailable Then
            sb.AppendLine("    <Compile Include=""formDMD.vb"">")
            sb.AppendLine("      <SubType>Form</SubType>")
            sb.AppendLine("    </Compile>")
            sb.AppendLine("    <Compile Include=""formDMD.Designer.vb"">")
            sb.AppendLine("      <DependentUpon>formDMD.vb</DependentUpon>")
            sb.AppendLine("      <SubType>Form</SubType>")
            sb.AppendLine("    </Compile>")
        End If
        Return sb.ToString()
    End Function
    Private Function DMD2Code2(ByVal name As String) As String
        Dim sb As StringBuilder = New StringBuilder()
        If Backglass.currentData.IsDMDImageAvailable Then
            sb.AppendLine("    <EmbeddedResource Include=""formDMD.resx"">")
            sb.AppendLine("      <DependentUpon>formDMD.vb</DependentUpon>")
            sb.AppendLine("    </EmbeddedResource>")
        End If
        Return sb.ToString()
    End Function

    Private Function MainImage2Code1() As String
        Dim sb As StringBuilder = New StringBuilder()
        Dim extension As String = ".png"
        If Not String.IsNullOrEmpty(Backglass.currentData.ImageFileName) Then extension = IO.Path.GetExtension(Backglass.currentData.ImageFileName)
        sb.AppendLine("  <ItemGroup>")
        sb.AppendLine("    <None Include=""Resources\BackgroundOff" & extension & """ />")
        sb.AppendLine("  </ItemGroup>")
        sb.AppendLine("  <ItemGroup>")
        sb.AppendLine("    <None Include=""Resources\BackgroundOn" & extension & """ />")
        sb.AppendLine("  </ItemGroup>")
        Return sb.ToString()
    End Function
    Private Function MainImage2Code2() As String
        Dim sb As StringBuilder = New StringBuilder()
        Dim extension As String = ".png"
        If Not String.IsNullOrEmpty(Backglass.currentData.ImageFileName) Then extension = IO.Path.GetExtension(Backglass.currentData.ImageFileName)
        sb.AppendLine("  <data name=""BackgroundOff"" type=""System.Resources.ResXFileRef, System.Windows.Forms"">")
        sb.AppendLine("    <value>..\Resources\BackgroundOff" & extension & ";System.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</value>")
        sb.AppendLine("  </data>")
        sb.AppendLine("  <data name=""BackgroundOn"" type=""System.Resources.ResXFileRef, System.Windows.Forms"">")
        sb.AppendLine("    <value>..\Resources\BackgroundOn" & extension & ";System.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</value>")
        sb.AppendLine("  </data>")
        Return sb.ToString()
    End Function
    Private Function MainImage2Code3() As String
        Dim sb As StringBuilder = New StringBuilder()
        sb.AppendLine("  Friend ReadOnly Property BackgroundOff() As System.Drawing.Bitmap")
        sb.AppendLine("    Get")
        sb.AppendLine("      Dim obj As Object = ResourceManager.GetObject(""BackgroundOff"", resourceCulture)")
        sb.AppendLine("      Return CType(obj, System.Drawing.Bitmap)")
        sb.AppendLine("    End Get")
        sb.AppendLine("  End Property")
        sb.AppendLine()
        sb.AppendLine("  Friend ReadOnly Property BackgroundOn() As System.Drawing.Bitmap")
        sb.AppendLine("    Get")
        sb.AppendLine("      Dim obj As Object = ResourceManager.GetObject(""BackgroundOn"", resourceCulture)")
        sb.AppendLine("      Return CType(obj, System.Drawing.Bitmap)")
        sb.AppendLine("    End Get")
        sb.AppendLine("  End Property")
        sb.AppendLine()
        Return sb.ToString()
    End Function

    Private Function ResourceImage2Code1() As String
        Dim sb As StringBuilder = New StringBuilder()
        If Backglass.currentData.IsResourceFileNeeded Then
            For Each resourceimage As KeyValuePair(Of String, Image) In Backglass.currentData.ResourceImages
                sb.AppendLine("  <ItemGroup>")
                sb.AppendLine("    <None Include=""Resources\" & resourceimage.Key & ".jpg"" />")
                sb.AppendLine("  </ItemGroup>")
            Next
            sb.AppendLine("  <ItemGroup>")
            sb.AppendLine("    <None Include=""Resources\EMReel.wav"" />")
            sb.AppendLine("  </ItemGroup>")
        End If
        Return sb.ToString()
    End Function
    Private Function ResourceImage2Code2() As String
        Dim sb As StringBuilder = New StringBuilder()
        If Backglass.currentData.IsResourceFileNeeded Then
            For Each resourceimage As KeyValuePair(Of String, Image) In Backglass.currentData.ResourceImages
                sb.AppendLine("  <data name=""" & resourceimage.Key & """ type=""System.Resources.ResXFileRef, System.Windows.Forms"">")
                sb.AppendLine("    <value>..\Resources\" & resourceimage.Key & ".jpg;System.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</value>")
                sb.AppendLine("  </data>")
            Next
            sb.AppendLine("  <data name=""EMReel"" type=""System.Resources.ResXFileRef, System.Windows.Forms"">")
            sb.AppendLine("    <value>..\Resources\EMReel.wav;System.IO.MemoryStream, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>")
            sb.AppendLine("  </data>")
        End If
        Return sb.ToString()
    End Function
    Private Function ResourceImage2Code3() As String
        Dim sb As StringBuilder = New StringBuilder()
        If Backglass.currentData.IsResourceFileNeeded Then
            For Each resourceimage As KeyValuePair(Of String, Image) In Backglass.currentData.ResourceImages
                sb.AppendLine("  Friend ReadOnly Property " & resourceimage.Key & "() As System.Drawing.Bitmap")
                sb.AppendLine("    Get")
                sb.AppendLine("      Dim obj As Object = ResourceManager.GetObject(""" & resourceimage.Key & """, resourceCulture)")
                sb.AppendLine("      Return CType(obj, System.Drawing.Bitmap)")
                sb.AppendLine("    End Get")
                sb.AppendLine("  End Property")
                sb.AppendLine()
            Next
            sb.AppendLine("  Friend ReadOnly Property EMReel() As System.IO.UnmanagedMemoryStream")
            sb.AppendLine("    Get")
            sb.AppendLine("      Return ResourceManager.GetStream(""EMReel"", resourceCulture)")
            sb.AppendLine("    End Get")
            sb.AppendLine("  End Property")
        End If
        Return sb.ToString()
    End Function
    Private Sub ResResourceImagesCopy()
        If Backglass.currentData.IsResourceFileNeeded Then
            For Each resourceimage As KeyValuePair(Of String, Image) In Backglass.currentData.ResourceImages
                resourceimage.Value.Save(resourceimage.Key & ".jpg")
            Next
        End If
    End Sub
    Private Sub ResResourceSoundsCopy()
        If Backglass.currentData.IsResourceFileNeeded Then
            Dim wavFile(My.Resources.EMReel.Length - 1) As Byte
            My.Resources.EMReel.Read(wavFile, 0, UBound(wavFile) + 1)
            IO.File.WriteAllBytes("EMReel.wav", wavFile)
        End If
    End Sub

    Private Function Image2Base64(ByVal sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo), ByVal IsDMD As Boolean) As String
        ' some inits
        Dim sb As StringBuilder = New StringBuilder()
        Dim illumination As Illumination.Create = Nothing
        Dim imageBackground As Image = Nothing
        Dim imageOnBackground As Image = Nothing
        Dim bitmapBackground As Bitmap = Nothing
        Dim imageIlluminated As Image = Nothing
        If IsDMD Then
            illumination = New Illumination.Create()
            imageBackground = Backglass.currentTabPage.IlluminatedDMDImageOnlyWithAlwaysOnLights()
            bitmapBackground = CType(imageBackground, Bitmap)
        Else
            If Backglass.currentData.IsExternalIlluminationImageSelected Then
                imageBackground = Backglass.currentData.Image()
                imageIlluminated = Backglass.currentData.FirstSelectedExternalIlluminationImage()
            Else
                illumination = New Illumination.Create()
                imageBackground = Backglass.currentTabPage.IlluminatedImageOnlyWithAlwaysOnLights()
                bitmapBackground = CType(imageBackground, Bitmap)
                imageOnBackground = Backglass.currentTabPage.IlluminatedImageOnlyWithOnLights()
            End If
        End If
        ' add main background pic
        If IsDMD Then
            sb.AppendLine("  <data name=""$this.BackgroundImage"" type=""System.Drawing.Bitmap, System.Drawing"" mimetype=""application/x-microsoft.net.object.bytearray.base64"">")
            sb.AppendLine("    <value>")
            sb.AppendLine("    " & ImageToBase64(imageBackground))
            sb.AppendLine("    </value>")
            sb.AppendLine("  </data>")
        Else
            Dim extension As String = ".png"
            If Not String.IsNullOrEmpty(Backglass.currentData.ImageFileName) Then extension = IO.Path.GetExtension(Backglass.currentData.ImageFileName)
            imageBackground.Save("Resources\BackgroundOff" & extension)
            If imageOnBackground IsNot Nothing Then
                imageOnBackground.Save("Resources\BackgroundOn" & extension)
            End If
        End If
        ' add all picture box images
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            With bulb.Value
                Dim name As String = String.Empty
                name = "PictureBox" & .ID.ToString("D3")
                sb.AppendLine("  <data name=""" & name & ".BackgroundImage"" type=""System.Drawing.Bitmap, System.Drawing"" mimetype=""application/x-microsoft.net.object.bytearray.base64"">")
                sb.AppendLine("    <value>")
                If Not IsDMD AndAlso Backglass.currentData.IsExternalIlluminationImageSelected Then
                    sb.AppendLine("    " & ImageToBase64(imageIlluminated.PartFromImage(New Rectangle(.LocationX, .SizeX))))
                Else
                    If .IsImageSnippit Then
                        If .Image IsNot Nothing Then
                            sb.AppendLine("    " & ImageToBase64(.Image))
                        End If
                    Else
                        Dim font As Font = Nothing
                        If Not String.IsNullOrEmpty(.Text) AndAlso Not String.IsNullOrEmpty(.FontName) Then
                            font = New Font(.FontName, .FontSize, .FontStyle)
                        End If
                        sb.AppendLine("    " & ImageToBase64(illumination.CreateOverlayImage(bitmapBackground, New Rectangle(.Location, .Size), New Rectangle(.LocationX, .SizeX), .Intensity, .LightColor, .DodgeColor, .Text, font, .TextAlignment, .IlluMode)))
                        If font IsNot Nothing Then font.Dispose()
                    End If
                End If
                sb.AppendLine("    </value>")
                sb.AppendLine("  </data>")
            End With
        Next
        ' finished
        Return sb.ToString()
    End Function
    Private Function PictureBox2DesignerCode1(ByVal securedname As String,
                                              ByVal sortedScores1 As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                              ByVal sortedScores2 As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                              ByVal sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo)) As String
        Dim sb As StringBuilder = New StringBuilder()
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            With bulb.Value
                Dim name As String = "Me.PictureBox" & .ID.ToString("D3")
                sb.AppendLine(name & " = New " & securedname & ".B2SPictureBox()")
            End With
        Next
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedScores1
            With score.Value
                Dim index As Integer = GetMatchingScoreIndex(.ID, sortedScores1, sortedScores2)
                For J As Integer = 1 To .Digits
                    If IsReelImageRendered(.ReelType) Then
                        If Backglass.currentData.UseDream7LEDs Then
                            Dim name As String = "Me.LEDDisplay" & .ID.ToString("D2")
                            sb.AppendLine(name & " = New " & securedname & ".Dream7Display()")
                            Exit For
                        Else
                            Dim name As String = "Me.LEDBox" & index.ToString("D3")
                            sb.AppendLine(name & " = New " & securedname & ".B2SLEDBox()")
                        End If
                    Else
                        Dim name As String = "Me.ReelBox" & index.ToString("D3")
                        sb.AppendLine(name & " = New " & securedname & ".B2SReelBox()")
                    End If
                    index += 1
                Next
            End With
        Next
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            With bulb.Value
                Dim name As String = "Me.PictureBox" & .ID.ToString("D3")
                sb.AppendLine("CType(" & name & ", System.ComponentModel.ISupportInitialize).BeginInit()")
            End With
        Next
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedScores1
            With score.Value
                Dim index As Integer = GetMatchingScoreIndex(.ID, sortedScores1, sortedScores2)
                For J As Integer = 1 To .Digits
                    If IsReelImageRendered(.ReelType) Then
                        If Backglass.currentData.UseDream7LEDs Then
                            Exit For
                        Else
                            Dim name As String = "Me.LEDBox" & index.ToString("D3")
                            sb.AppendLine("CType(" & name & ", System.ComponentModel.ISupportInitialize).BeginInit()")
                        End If
                    Else
                        Dim name As String = "Me.ReelBox" & index.ToString("D3")
                        sb.AppendLine("CType(" & name & ", System.ComponentModel.ISupportInitialize).BeginInit()")
                    End If
                    index += 1
                Next
            End With
        Next
        Return sb.ToString()
    End Function
    Private Function PictureBox2DesignerCode2(ByVal securedname As String,
                                              ByVal sortedScores1 As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                              ByVal sortedScores2 As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                              ByVal sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo)) As String
        Dim sb As StringBuilder = New StringBuilder()
        Dim tabindex As Integer = 0
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            With bulb.Value
                If Backglass.currentData.IsExternalIlluminationImageSelected Then
                    Dim name As String = "Me.PictureBox" & .ID.ToString("D3")
                    sb.AppendLine("'")
                    sb.Append("'")
                    sb.AppendLine(name.Substring(3))
                    sb.AppendLine("'")
                    sb.AppendLine(name & ".Name = """ & name.Substring(3) & """")
                    sb.AppendLine(name & ".GroupName = """ & .Name & """")
                    sb.AppendLine(name & ".Location = New System.Drawing.Point(" & .LocationX.X.ToString() & ", " & .LocationX.Y.ToString() & ")")
                    sb.AppendLine(name & ".Size = New System.Drawing.Size(" & .SizeX.Width.ToString() & ", " & .SizeX.Height.ToString() & ")")
                    sb.AppendLine(name & ".BackgroundImage = CType(Resources.GetObject(""" & name.Substring(3) & ".BackgroundImage""), System.Drawing.Image)")
                    'sb.AppendLine(name & ".BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch")
                    sb.AppendLine(name & ".Margin = New System.Windows.Forms.Padding(0)")
                    'sb.AppendLine(name & ".SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage")
                    sb.AppendLine(name & ".RomID = " & .RomID.ToString())
                    sb.AppendLine(name & ".RomIDType = B2SPictureBox.eRomIDType." & Choose(.RomIDType + 1, "NotUsed", "Lamp", "Solenoid", "GIString"))
                    sb.AppendLine(name & ".RomInverted = " & If(.RomInverted, "True", "False"))
                    sb.AppendLine(name & ".TabIndex = " & tabindex.ToString())
                    sb.AppendLine(name & ".TabStop = False")
                    sb.AppendLine(name & ".Visible = False") ' & If(.InitialState = 1, "True", "False"))
                Else
                    Dim name As String = "Me.PictureBox" & .ID.ToString("D3")
                    sb.AppendLine("'")
                    sb.Append("'")
                    sb.AppendLine(name.Substring(3))
                    sb.AppendLine("'")
                    sb.AppendLine(name & ".Name = """ & name.Substring(3) & """")
                    sb.AppendLine(name & ".GroupName = """ & .Name & """")
                    sb.AppendLine(name & ".Location = New System.Drawing.Point(" & .LocationX.X.ToString() & ", " & .LocationX.Y.ToString() & ")")
                    sb.AppendLine(name & ".Size = New System.Drawing.Size(" & .SizeX.Width.ToString() & ", " & .SizeX.Height.ToString() & ")")
                    sb.AppendLine(name & ".BackgroundImage = CType(Resources.GetObject(""" & name.Substring(3) & ".BackgroundImage""), System.Drawing.Image)")
                    sb.AppendLine(name & ".BackColor = System.Drawing.Color.Transparent")
                    If Not String.IsNullOrEmpty(.FontName) AndAlso Not String.IsNullOrEmpty(.Text) Then
                        sb.AppendLine(name & ".Font = New System.Drawing.Font(""" & .FontName & """, " & Math.Round(.FontSize) & "!, " & FontStyle2String(.FontStyle) & ", System.Drawing.GraphicsUnit.Point, CType(0, Byte))") ' System.Drawing.FontStyle.Bold
                        sb.AppendLine(name & ".Text = """ & .Text.Replace(vbCrLf, """ & vbCrLf & """) & """")
                    End If
                    sb.AppendLine(name & ".Intensity = " & .Intensity.ToString())
                    sb.AppendLine(name & ".InitialState = " & .InitialState.ToString())
                    sb.AppendLine(name & ".RomID = " & .RomID.ToString())
                    sb.AppendLine(name & ".RomIDType = B2SPictureBox.eRomIDType." & Choose(.RomIDType + 1, "NotUsed", "Lamp", "Solenoid", "GIString"))
                    sb.AppendLine(name & ".RomInverted = " & If(.RomInverted, "True", "False"))
                    sb.AppendLine(name & ".TabIndex = " & tabindex.ToString())
                    sb.AppendLine(name & ".TabStop = False")
                    sb.AppendLine(name & ".Visible = False") '& If(.InitialState = 1, "True", "False"))
                End If
            End With
            tabindex += 1
        Next
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedScores1
            With score.Value
                Dim color As Color = .ReelColor
                If color = Nothing Then color = Drawing.Color.OrangeRed
                Dim index As Integer = GetMatchingScoreIndex(.ID, sortedScores1, sortedScores2)
                Dim width As Integer = CInt((.SizeX.Width - (.Digits - 1) * .Spacing) / .Digits)
                For J As Integer = 1 To .Digits
                    Dim x As Integer = .LocationX.X + ((J - 1) * (width + .Spacing))
                    If IsReelImageRendered(score.Value.ReelType) Then
                        If Backglass.currentData.UseDream7LEDs Then
                            Dim name As String = "Me.LEDDisplay" & score.Value.ID.ToString("D2")
                            sb.AppendLine("'")
                            sb.Append("'")
                            sb.AppendLine(name.Substring(3))
                            sb.AppendLine("'")
                            sb.AppendLine(name & ".Name = """ & name.Substring(3) & """")
                            sb.AppendLine(name & ".Location = New System.Drawing.Point(" & .LocationX.X.ToString() & ", " & .LocationX.Y.ToString() & ")")
                            sb.AppendLine(name & ".Size = New System.Drawing.Size(" & .SizeX.Width.ToString() & ", " & .SizeX.Height.ToString() & ")")
                            sb.AppendLine(name & ".BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))")
                            sb.AppendLine(name & ".LightColor = System.Drawing.Color.FromArgb(CType(CType(" & color.R.ToString() & ", Byte), Integer), CType(CType(" & color.G.ToString() & ", Byte), Integer), CType(CType(" & color.B.ToString() & ", Byte), Integer))")
                            sb.AppendLine(name & ".OffColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))")
                            sb.AppendLine(name & ".Angle = 0.0!")
                            sb.AppendLine(name & ".Digits = " & .Digits.ToString())
                            sb.AppendLine(name & ".ScaleFactor = 0.5!")
                            sb.AppendLine(name & ".ScaleMode = ScaleMode.Stretch")
                            sb.AppendLine(name & ".Shear = 0." & CInt(Backglass.currentData.D7Shear * 10).ToString() & "!")
                            sb.AppendLine(name & ".Spacing = " & (.Spacing * 6).ToString() & ".0!")
                            sb.AppendLine(name & ".TabIndex = " & tabindex.ToString())
                            sb.AppendLine(name & ".Text = Nothing")
                            sb.AppendLine(name & ".Type = SegmentNumberType." & GetDream7LEDType(.ReelType.Substring(11)))
                            Exit For
                        Else
                            Dim name As String = "Me.LEDBox" & index.ToString("D3")
                            sb.AppendLine("'")
                            sb.Append("'")
                            sb.AppendLine(name.Substring(3))
                            sb.AppendLine("'")
                            sb.AppendLine(name & ".Name = """ & name.Substring(3) & """")
                            sb.AppendLine(name & ".Location = New System.Drawing.Point(" & x.ToString() & ", " & .LocationX.Y.ToString() & ")")
                            sb.AppendLine(name & ".Size = New System.Drawing.Size(" & width.ToString() & ", " & .SizeX.Height.ToString() & ")")
                            sb.AppendLine(name & ".BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))")
                            sb.AppendLine(name & ".BackgroundImage = CType(Resources.GetObject(""" & name.Substring(3) & ".BackgroundImage""), System.Drawing.Image)")
                            sb.AppendLine(name & ".LitLEDSegmentColor = System.Drawing.Color.FromArgb(CType(CType(" & color.R.ToString() & ", Byte), Integer), CType(CType(" & color.G.ToString() & ", Byte), Integer), CType(CType(" & color.B.ToString() & ", Byte), Integer))")
                            sb.AppendLine(name & ".DarkLEDSegmentColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))")
                            sb.AppendLine(name & ".Margin = New System.Windows.Forms.Padding(0)")
                            sb.AppendLine(name & ".ID = " & index.ToString())
                            sb.AppendLine(name & ".LEDType = B2SLED.eLEDType.LED" & .ReelType.Substring(11))
                            sb.AppendLine(name & ".TabIndex = " & tabindex.ToString())
                            sb.AppendLine(name & ".TabStop = False")
                            sb.AppendLine(name & ".Visible = True")
                        End If
                    Else
                        Dim name As String = "Me.ReelBox" & index.ToString("D3")
                        sb.AppendLine("'")
                        sb.Append("'")
                        sb.AppendLine(name.Substring(3))
                        sb.AppendLine("'")
                        sb.AppendLine(name & ".Name = """ & name.Substring(3) & """")
                        sb.AppendLine(name & ".Location = New System.Drawing.Point(" & x.ToString() & ", " & .LocationX.Y.ToString() & ")")
                        sb.AppendLine(name & ".Size = New System.Drawing.Size(" & width.ToString() & ", " & .SizeX.Height.ToString() & ")")
                        sb.AppendLine(name & ".BackColor = System.Drawing.Color.White")
                        sb.AppendLine(name & ".BackgroundImage = Global." & securedname & ".My.Resources.Resources." & .ReelType)
                        sb.AppendLine(name & ".BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch")
                        sb.AppendLine(name & ".ImeMode = System.Windows.Forms.ImeMode.NoControl")
                        sb.AppendLine(name & ".Margin = New System.Windows.Forms.Padding(0)")
                        sb.AppendLine(name & ".ID = " & index.ToString())
                        sb.AppendLine(name & ".TabIndex = " & tabindex.ToString())
                        sb.AppendLine(name & ".TabStop = False")
                        sb.AppendLine(name & ".Visible = True")
                    End If
                    tabindex += 1
                    index += 1
                Next
            End With
        Next
        Return sb.ToString()
    End Function
    Private Function PictureBox2DesignerCode3(ByVal securedname As String,
                                              ByVal sortedScores1 As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                              ByVal sortedScores2 As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                              ByVal sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo)) As String
        Dim sb As StringBuilder = New StringBuilder()
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            With bulb.Value
                Dim name As String = "Me.PictureBox" & .ID.ToString("D3")
                sb.AppendLine("Me.Controls.Add(" & name & ")")
            End With
        Next
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedScores1
            With score.Value
                Dim index As Integer = GetMatchingScoreIndex(.ID, sortedScores1, sortedScores2)
                For J As Integer = 1 To .Digits
                    If IsReelImageRendered(.ReelType) Then
                        If Backglass.currentData.UseDream7LEDs Then
                            Dim name As String = "Me.LEDDisplay" & .ID.ToString("D2")
                            sb.AppendLine("Me.Controls.Add(" & name & ")")
                            Exit For
                        Else
                            Dim name As String = "Me.LEDBox" & index.ToString("D3")
                            sb.AppendLine("Me.Controls.Add(" & name & ")")
                        End If
                    Else
                        Dim name As String = "Me.ReelBox" & index.ToString("D3")
                        sb.AppendLine("Me.Controls.Add(" & name & ")")
                    End If
                    index += 1
                Next
            End With
        Next
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            With bulb.Value
                Dim name As String = "Me.PictureBox" & .ID.ToString("D3")
                sb.AppendLine("CType(" & name & ", System.ComponentModel.ISupportInitialize).EndInit()")
            End With
        Next
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedScores1
            With score.Value
                Dim index As Integer = GetMatchingScoreIndex(.ID, sortedScores1, sortedScores2)
                For J As Integer = 1 To .Digits
                    If IsReelImageRendered(.ReelType) Then
                        If Backglass.currentData.UseDream7LEDs Then
                            Exit For
                        Else
                            Dim name As String = "Me.LEDBox" & index.ToString("D3")
                            sb.AppendLine("CType(" & name & ", System.ComponentModel.ISupportInitialize).EndInit()")
                        End If
                    Else
                        Dim name As String = "Me.ReelBox" & index.ToString("D3")
                        sb.AppendLine("CType(" & name & ", System.ComponentModel.ISupportInitialize).EndInit()")
                    End If
                    index += 1
                Next
            End With
        Next
        Return sb.ToString()
    End Function
    Private Function PictureBox2DesignerCode4(ByVal securedname As String,
                                              ByVal sortedScores1 As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                              ByVal sortedScores2 As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                              ByVal sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo)) As String
        Dim sb As StringBuilder = New StringBuilder()
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            With bulb.Value
                Dim name As String = "PictureBox" & .ID.ToString("D3")
                sb.AppendLine("Friend WithEvents " & name & " As " & securedname & ".B2SPictureBox")
            End With
        Next
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedScores1
            With score.Value
                Dim index As Integer = GetMatchingScoreIndex(.ID, sortedScores1, sortedScores2)
                For J As Integer = 1 To .Digits
                    If IsReelImageRendered(.ReelType) Then
                        If Backglass.currentData.UseDream7LEDs Then
                            Dim name As String = "LEDDisplay" & .ID.ToString("D2")
                            sb.AppendLine("Friend WithEvents " & name & " As " & securedname & ".Dream7Display")
                            Exit For
                        Else
                            Dim name As String = "LEDBox" & index.ToString("D3")
                            sb.AppendLine("Friend WithEvents " & name & " As " & securedname & ".B2SLEDBox")
                        End If
                    Else
                        Dim name As String = "ReelBox" & index.ToString("D3")
                        sb.AppendLine("Friend WithEvents " & name & " As " & securedname & ".B2SReelBox")
                    End If
                    index += 1
                Next
            End With
        Next
        Return sb.ToString()
    End Function
    Private Function PictureBox2DesignerCode5(ByVal securedname As String, ByVal IsDMD As Boolean) As String
        Return "Global." & securedname & ".My.Resources.Resources." & If(IsDMD, "DMD", "") & "BackgroundOff"
    End Function

    Private Function GetMatchingScoreIndex(ByVal id As Integer,
                                           ByVal sortedScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                           ByVal sortedDMDScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo)) As Integer
        Dim ret As Integer = 1
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedScores
            If score.Value.ID < id Then ret += score.Value.Digits
            If score.Value.ID = id AndAlso score.Value.B2SStartDigit > 0 Then ret = score.Value.B2SStartDigit : Exit For
        Next
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedDMDScores
            If score.Value.ID < id Then ret += score.Value.Digits
            If score.Value.ID = id AndAlso score.Value.B2SStartDigit > 0 Then ret = score.Value.B2SStartDigit : Exit For
        Next
        Return ret
    End Function

    Private Function IsScoreIndexExisting(ByVal digit As Integer,
                                          ByVal sortedScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo),
                                          ByVal sortedDMDScores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo)) As Boolean
        Dim ret As Boolean = False
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedScores
            If score.Value.B2SStartDigit <= digit AndAlso score.Value.B2SStartDigit + score.Value.Digits - 1 >= digit Then ret = True : Exit For
        Next
        For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In sortedDMDScores
            If score.Value.B2SStartDigit <= digit AndAlso score.Value.B2SStartDigit + score.Value.Digits - 1 >= digit Then ret = True : Exit For
        Next
        Return ret
    End Function
    Private Function IsBulbIndexExisting(ByVal bulbid As Integer,
                                         ByVal sortedBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo),
                                         ByVal sortedDMDBulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo))
        Dim ret As Boolean = False
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedBulbs
            If bulb.Value.B2SID = bulbid Then ret = True : Exit For
        Next
        For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In sortedDMDBulbs
            If bulb.Value.B2SID = bulbid Then ret = True : Exit For
        Next
        Return ret
    End Function

    Private Function FontStyle2String(ByVal style As FontStyle) As String
        Dim ret As String = String.Empty
        If style = 0 Then
            ret = "System.Drawing.FontStyle.Regular"
        Else
            If (style And FontStyle.Bold) <> 0 Then
                ret = "System.Drawing.FontStyle.Bold"
            End If
            If (style And FontStyle.Italic) <> 0 Then
                ret = If(Not String.IsNullOrEmpty(ret), " Or ", "") & "System.Drawing.FontStyle.Italic"
            End If
            If (style And FontStyle.Underline) <> 0 Then
                ret = If(Not String.IsNullOrEmpty(ret), " Or ", "") & "System.Drawing.FontStyle.Underline"
            End If
            If (style And FontStyle.Strikeout) <> 0 Then
                ret = If(Not String.IsNullOrEmpty(ret), " Or ", "") & "System.Drawing.FontStyle.Strikeout"
            End If
        End If
        Return ret
    End Function

#End Region


End Class
