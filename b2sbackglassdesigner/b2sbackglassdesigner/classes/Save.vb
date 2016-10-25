Imports System
Imports System.Text
Imports System.IO

Public Class Save

    Inherits HelperBase

    Private Const SaveVersion As String = "1.26"

    Public Function LoadData(ByRef _backglassData As Backglass.Data, ByVal filename As String) As Boolean

        If Not IO.File.Exists(filename) Then
            Return False
        End If

        Dim XML As Xml.XmlDocument = New Xml.XmlDocument
        Try
            XML.Load(filename)
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.MSG_LoadError, ex.Message), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If XML IsNot Nothing AndAlso XML.SelectSingleNode("B2SBackglassData") IsNot Nothing Then
            Dim version As String = XML.SelectSingleNode("B2SBackglassData").Attributes("Version").InnerText
            Dim topnode As Xml.XmlElement = XML.SelectNodes("B2SBackglassData")(0)
            _backglassData = New Backglass.Data()
            Dim myanimations As Animation.AnimationHeaderCollection = New Animation.AnimationHeaderCollection()
            _backglassData.Animations = myanimations
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
                If topnode.SelectSingleNode("BackupName") IsNot Nothing Then
                    .BackupName = topnode.SelectSingleNode("BackupName").Attributes("Value").InnerText
                End If
                .ProjectGUID = topnode.SelectSingleNode("ProjectGUID").Attributes("Value").InnerText
                .ProjectGUID2 = topnode.SelectSingleNode("ProjectGUID2").Attributes("Value").InnerText
                .AssemblyGUID = topnode.SelectSingleNode("AssemblyGUID").Attributes("Value").InnerText
                .Name = topnode.SelectSingleNode("Name").Attributes("Value").InnerText
                .LoadedName = .Name
                If topnode.SelectSingleNode("VSName") IsNot Nothing Then
                    .VSName = topnode.SelectSingleNode("VSName").Attributes("Value").InnerText
                End If
                If topnode.SelectSingleNode("DualBackglass") IsNot Nothing Then
                    .DualBackglass = (topnode.SelectSingleNode("DualBackglass").Attributes("Value").InnerText = "1")
                End If
                If topnode.SelectSingleNode("Author") IsNot Nothing Then
                    .Author = topnode.SelectSingleNode("Author").Attributes("Value").InnerText
                End If
                If topnode.SelectSingleNode("Artwork") IsNot Nothing Then
                    .Artwork = topnode.SelectSingleNode("Artwork").Attributes("Value").InnerText
                End If
                If topnode.SelectSingleNode("GameName") IsNot Nothing Then
                    .GameName = topnode.SelectSingleNode("GameName").Attributes("Value").InnerText
                End If
                .TableType = CInt(topnode.SelectSingleNode("TableType").Attributes("Value").InnerText)
                If topnode.SelectSingleNode("AddEMDefaults") IsNot Nothing Then
                    .AddEMDefaults = (topnode.SelectSingleNode("AddEMDefaults").Attributes("Value").InnerText = "1")
                End If
                If topnode.SelectSingleNode("DMDType") IsNot Nothing Then
                    .DMDType = CInt(topnode.SelectSingleNode("DMDType").Attributes("Value").InnerText)
                End If
                If topnode.SelectSingleNode("CommType") IsNot Nothing Then
                    .CommType = CInt(topnode.SelectSingleNode("CommType").Attributes("Value").InnerText)
                End If
                If topnode.SelectSingleNode("DestType") IsNot Nothing Then
                    .DestType = CInt(topnode.SelectSingleNode("DestType").Attributes("Value").InnerText)
                ElseIf .CommType = eCommType.Rom Then
                    .DestType = eDestType.DirectB2S
                End If
                .NumberOfPlayers = CInt(topnode.SelectSingleNode("NumberOfPlayers").Attributes("Value").InnerText)
                If topnode.SelectSingleNode("B2SDataCount") IsNot Nothing Then
                    .B2SDataCount = CInt(topnode.SelectSingleNode("B2SDataCount").Attributes("Value").InnerText)
                End If
                If topnode.SelectSingleNode("ReelType") IsNot Nothing Then
                    .ReelType = topnode.SelectSingleNode("ReelType").Attributes("Value").InnerText
                End If
                If topnode.SelectSingleNode("UseDream7LEDs") IsNot Nothing Then
                    .UseDream7LEDs = (topnode.SelectSingleNode("UseDream7LEDs").Attributes("Value").InnerText = "1")
                End If
                If topnode.SelectSingleNode("D7Glow") IsNot Nothing Then
                    .D7Glow = CSng(topnode.SelectSingleNode("D7Glow").Attributes("Value").InnerText) / 100
                    .D7Thickness = CSng(topnode.SelectSingleNode("D7Thickness").Attributes("Value").InnerText) / 100
                    .D7Shear = CSng(topnode.SelectSingleNode("D7Shear").Attributes("Value").InnerText) / 100
                End If
                If topnode.SelectSingleNode("ReelColor") IsNot Nothing Then
                    Try
                        .ReelColor = String2Color(topnode.SelectSingleNode("ReelColor").Attributes("Value").InnerText.Replace(";", "."))
                    Catch
                        .ReelColor = Color.OrangeRed
                    End Try
                End If
                If topnode.SelectSingleNode("ReelRollingDirection") IsNot Nothing Then
                    .ReelRollingDirection = CInt(topnode.SelectSingleNode("ReelRollingDirection").Attributes("Value").InnerText)
                End If
                If topnode.SelectSingleNode("ReelRollingInterval") IsNot Nothing Then
                    .ReelRollingInterval = CInt(topnode.SelectSingleNode("ReelRollingInterval").Attributes("Value").InnerText)
                End If
                If topnode.SelectSingleNode("ReelIntermediateImageCount") IsNot Nothing Then
                    .ReelIntermediateImageCount = CInt(topnode.SelectSingleNode("ReelIntermediateImageCount").Attributes("Value").InnerText)
                End If
                .GrillHeight = CInt(topnode.SelectSingleNode("GrillHeight").Attributes("Value").InnerText)
                If topnode.SelectSingleNode("GrillHeight").Attributes("Small") IsNot Nothing Then
                    .SmallGrillHeight = CInt(topnode.SelectSingleNode("GrillHeight").Attributes("Small").InnerText)
                End If
                .DMDDefaultLocation = New Point(CInt(topnode.SelectSingleNode("DMDDefaultLocationX").Attributes("Value").InnerText), CInt(topnode.SelectSingleNode("DMDDefaultLocationY").Attributes("Value").InnerText))
                If topnode.SelectSingleNode("DMDCopyAreaX") IsNot Nothing Then
                    .DMDCopyArea.Location = New Point(CInt(topnode.SelectSingleNode("DMDCopyAreaX").Attributes("Value").InnerText), CInt(topnode.SelectSingleNode("DMDCopyAreaY").Attributes("Value").InnerText))
                    .DMDCopyArea.Size = New Size(CInt(topnode.SelectSingleNode("DMDCopyAreaWidth").Attributes("Value").InnerText), CInt(topnode.SelectSingleNode("DMDCopyAreaHeight").Attributes("Value").InnerText))
                End If

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
                        score.ReelType = innerNode.Attributes("ReelType").InnerText
                        score.ReelColor = _backglassData.ReelColor
                        If innerNode.Attributes("ReelColor") IsNot Nothing Then
                            score.ReelColor = String2Color(innerNode.Attributes("ReelColor").InnerText.Replace(";", "."))
                        End If
                        If innerNode.Attributes("B2SStartDigit") IsNot Nothing Then
                            score.B2SStartDigit = CInt(innerNode.Attributes("B2SStartDigit").InnerText)
                        End If
                        If innerNode.Attributes("B2SScoreType") IsNot Nothing Then
                            score.B2SScoreType = CInt(innerNode.Attributes("B2SScoreType").InnerText)
                        End If
                        If innerNode.Attributes("B2SPlayerNo") IsNot Nothing Then
                            score.B2SPlayerNo = CInt(innerNode.Attributes("B2SPlayerNo").InnerText)
                        End If
                        If innerNode.Attributes("NumberOfReels") IsNot Nothing Then
                            score.Digits = CInt(innerNode.Attributes("NumberOfReels").InnerText)
                        End If
                        If innerNode.Attributes("Digits") IsNot Nothing Then
                            score.Digits = CInt(innerNode.Attributes("Digits").InnerText)
                        End If
                        If innerNode.Attributes("SpaceBetweenReels") IsNot Nothing Then
                            score.Spacing = CInt(innerNode.Attributes("SpaceBetweenReels").InnerText)
                        End If
                        If innerNode.Attributes("Spacing") IsNot Nothing Then
                            score.Spacing = CInt(innerNode.Attributes("Spacing").InnerText)
                        End If
                        If innerNode.Attributes("DisplayState") IsNot Nothing Then
                            score.DisplayState = CInt(innerNode.Attributes("DisplayState").InnerText)
                        End If
                        If innerNode.Attributes("ReelIlluLocation") IsNot Nothing Then
                            score.ReelIlluLocation = CInt(innerNode.Attributes("ReelIlluLocation").InnerText)
                        End If
                        If innerNode.Attributes("ReelIlluB2SID") IsNot Nothing Then
                            score.ReelIlluB2SID = CInt(innerNode.Attributes("ReelIlluB2SID").InnerText)
                        End If
                        If innerNode.Attributes("ReelIlluB2SIDType") IsNot Nothing Then
                            score.ReelIlluB2SIDType = CInt(innerNode.Attributes("ReelIlluB2SIDType").InnerText)
                        End If
                        If innerNode.Attributes("ReelIlluB2SValue") IsNot Nothing Then
                            score.ReelIlluB2SValue = CInt(innerNode.Attributes("ReelIlluB2SValue").InnerText)
                        End If
                        If innerNode.Attributes("ReelIlluIntensity") IsNot Nothing Then
                            score.ReelIlluIntensity = CInt(innerNode.Attributes("ReelIlluIntensity").InnerText)
                        End If
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
                    For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Illumination/Bulb")
                        Dim bulb As Illumination.BulbInfo = New Illumination.BulbInfo()
                        bulb.ID = CInt(innerNode.Attributes("ID").InnerText)
                        If innerNode.Attributes("B2SID") IsNot Nothing Then
                            bulb.B2SID = CInt(innerNode.Attributes("B2SID").InnerText)
                        End If
                        If innerNode.Attributes("B2SIDType") IsNot Nothing Then
                            bulb.B2SIDType = CInt(innerNode.Attributes("B2SIDType").InnerText)
                        End If
                        If innerNode.Attributes("B2SValue") IsNot Nothing Then
                            bulb.B2SValue = CInt(innerNode.Attributes("B2SValue").InnerText)
                        End If
                        If innerNode.Attributes("RomID") IsNot Nothing Then
                            bulb.RomID = CInt(innerNode.Attributes("RomID").InnerText)
                        End If
                        If innerNode.Attributes("RomIDType") IsNot Nothing Then
                            bulb.RomIDType = CInt(innerNode.Attributes("RomIDType").InnerText)
                        End If
                        If innerNode.Attributes("RomInverted") IsNot Nothing Then
                            bulb.RomInverted = (innerNode.Attributes("RomInverted").InnerText = "1")
                        End If
                        bulb.Name = innerNode.Attributes("Name").InnerText
                        bulb.Text = innerNode.Attributes("Text").InnerText
                        If innerNode.Attributes("TextAlignment") IsNot Nothing Then
                            bulb.TextAlignment = CInt(innerNode.Attributes("TextAlignment").InnerText)
                        End If
                        bulb.FontName = innerNode.Attributes("FontName").InnerText
                        bulb.FontSize = CSng(innerNode.Attributes("FontSize").InnerText)
                        If bulb.FontSize >= 100 Then bulb.FontSize = bulb.FontSize / 100
                        bulb.FontStyle = CInt(innerNode.Attributes("FontStyle").InnerText)
                        bulb.Visible = (CInt(innerNode.Attributes("Visible").InnerText) = 1)
                        bulb.Location = New Point(CInt(innerNode.Attributes("LocX").InnerText), CInt(innerNode.Attributes("LocY").InnerText))
                        bulb.Size = New Size(CInt(innerNode.Attributes("Width").InnerText), CInt(innerNode.Attributes("Height").InnerText))
                        If bulb.Size.Width < 10 Then bulb.Size.Width = 10
                        If bulb.Size.Height < 10 Then bulb.Size.Height = 10
                        If innerNode.Attributes("InitialState") IsNot Nothing Then
                            bulb.InitialState = CInt(innerNode.Attributes("InitialState").InnerText)
                            If bulb.InitialState = 3 Then bulb.InitialState = 1
                        End If
                        If innerNode.Attributes("DualMode") IsNot Nothing Then
                            bulb.DualMode = CInt(innerNode.Attributes("DualMode").InnerText)
                        End If
                        bulb.Intensity = CInt(innerNode.Attributes("Intensity").InnerText)
                        If bulb.Intensity < 1 Then
                            bulb.Intensity = 1
                        ElseIf bulb.Intensity > MaxBulbIntensity Then
                            bulb.Intensity = MaxBulbIntensity
                        End If
                        If innerNode.Attributes("LightColor") IsNot Nothing Then
                            bulb.LightColor = String2Color(innerNode.Attributes("LightColor").InnerText)
                        End If
                        If innerNode.Attributes("DodgeColor") IsNot Nothing Then
                            bulb.DodgeColor = String2Color(innerNode.Attributes("DodgeColor").InnerText)
                        End If
                        If innerNode.Attributes("IlluMode") IsNot Nothing Then
                            bulb.IlluMode = CInt(innerNode.Attributes("IlluMode").InnerText)
                        End If
                        If innerNode.Attributes("ZOrder") IsNot Nothing Then
                            bulb.ZOrder = CInt(innerNode.Attributes("ZOrder").InnerText)
                        End If
                        If innerNode.Attributes("IsImageSnippit") IsNot Nothing Then
                            bulb.IsImageSnippit = (innerNode.Attributes("IsImageSnippit").InnerText = "1")
                            If bulb.IsImageSnippit Then
                                bulb.Image = Base64ToImage(innerNode.Attributes("Image").InnerText)
                            End If
                        End If
                        If innerNode.Attributes("SnippitType") IsNot Nothing Then
                            bulb.SnippitInfo.SnippitType = CInt(innerNode.Attributes("SnippitType").InnerText)
                            If bulb.SnippitInfo.SnippitType <> eSnippitType.StandardImage Then
                                If innerNode.Attributes("SnippitMechID") IsNot Nothing Then
                                    bulb.SnippitInfo.SnippitMechID = CInt(innerNode.Attributes("SnippitMechID").InnerText)
                                End If
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
                        If innerNode.Attributes("Parent") IsNot Nothing AndAlso innerNode.Attributes("Parent").InnerText.Equals("DMD") Then
                            bulb.ParentForm = eParentForm.DMD
                            mydmdbulbs.Insert(mydmdbulbs.Count, bulb)
                        Else
                            bulb.ParentForm = eParentForm.Backglass
                            mybulbs.Insert(mybulbs.Count, bulb)
                        End If
                    Next
                End If

                ' get standard thumbnail image
                If topnode.SelectSingleNode("Images/ThumbnailImages/MainImage") IsNot Nothing Then
                    .ThumbnailImage = Base64ToImage(topnode.SelectSingleNode("Images/ThumbnailImages/MainImage").Attributes("Image").InnerText)
                End If

                ' get main images
                .ImageFileName = CheckImageFileName(.Name, topnode.SelectSingleNode("Images/BackgroundImages/MainImage").Attributes("FileName").InnerText)
                .Image = Base64ToImage(topnode.SelectSingleNode("Images/BackgroundImages/MainImage").Attributes("Image").InnerText)
                If topnode.SelectSingleNode("Images/DMDImages/MainImage").Attributes("Image") IsNot Nothing Then
                    .DMDImageFileName = CheckImageFileName(.Name, topnode.SelectSingleNode("Images/DMDImages/MainImage").Attributes("FileName").InnerText)
                    .DMDImage = Base64ToImage(topnode.SelectSingleNode("Images/DMDImages/MainImage").Attributes("Image").InnerText)
                End If
                .IsSavedImageDirty = False
                .IsSavedDMDImageDirty = False
                ' get more background images
                myimages.Init()
                If .Image IsNot Nothing Then
                    Dim mainimageinfo As Images.ImageInfo = New Images.ImageInfo(Images.eImageInfoType.BackgroundImage, .ImageFileName, .Image)
                    mainimageinfo.BackgroundImageType = CInt(topnode.SelectSingleNode("Images/BackgroundImages/MainImage").Attributes("Type").InnerText)
                    mainimageinfo.RomID = CInt(topnode.SelectSingleNode("Images/BackgroundImages/MainImage").Attributes("RomID").InnerText)
                    mainimageinfo.RomIDType = CInt(topnode.SelectSingleNode("Images/BackgroundImages/MainImage").Attributes("RomIDType").InnerText)
                    myimages.Insert(Images.eImageInfoType.Title4BackgroundImages, mainimageinfo)
                End If
                For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Images/BackgroundImages/Image")
                    Dim imageinfo As Images.ImageInfo = New Images.ImageInfo(Images.eImageInfoType.BackgroundImage)
                    imageinfo.Text = CheckImageFileName(.Name, innerNode.Attributes("FileName").InnerText)
                    imageinfo.Image = Base64ToImage(innerNode.Attributes("Image").InnerText)
                    If innerNode.Attributes("Type") IsNot Nothing Then
                        imageinfo.BackgroundImageType = CInt(innerNode.Attributes("Type").InnerText)
                        imageinfo.RomID = CInt(innerNode.Attributes("RomID").InnerText)
                        imageinfo.RomIDType = CInt(innerNode.Attributes("RomIDType").InnerText)
                    End If
                    myimages.Insert(Images.eImageInfoType.Title4BackgroundImages, imageinfo)
                Next
                ' get more illuminated images
                For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Images/IlluminatedImages/Image")
                    Dim imageinfo As Images.ImageInfo = New Images.ImageInfo(Images.eImageInfoType.IlluminationImage)
                    imageinfo.Text = CheckImageFileName(.Name, innerNode.Attributes("FileName").InnerText)
                    imageinfo.Image = Base64ToImage(innerNode.Attributes("Image").InnerText)
                    myimages.Insert(Images.eImageInfoType.Title4IlluminationImages, imageinfo)
                Next
                ' get more DMD images
                If .DMDImage IsNot Nothing Then
                    myimages.Insert(Images.eImageInfoType.Title4DMDImages, New Images.ImageInfo(Images.eImageInfoType.DMDImage, .DMDImageFileName, .DMDImage))
                End If
                For Each innerNode As Xml.XmlElement In topnode.SelectNodes("Images/DMDImages/Image")
                    Dim imageinfo As Images.ImageInfo = New Images.ImageInfo(Images.eImageInfoType.DMDImage)
                    imageinfo.Text = CheckImageFileName(.Name, innerNode.Attributes("FileName").InnerText)
                    imageinfo.Image = Base64ToImage(innerNode.Attributes("Image").InnerText)
                    myimages.Insert(Images.eImageInfoType.Title4DMDImages, imageinfo)
                Next
                ' get illumination snippits
                For i As Integer = 1 To 2
                    For Each bulb As Illumination.BulbInfo In If((i = 2), mydmdbulbs, mybulbs)
                        With bulb
                            If .IsImageSnippit AndAlso .Image IsNot Nothing Then
                                Dim imageinfo As Images.ImageInfo = New Images.ImageInfo(Images.eImageInfoType.IlluminationSnippits)
                                imageinfo.Text = .Name
                                imageinfo.Image = .Image
                                myimages.Insert(Images.eImageInfoType.Title4IlluminationSnippits, imageinfo)
                            End If
                        End With
                    Next
                Next
            End With
        End If

        Return True

    End Function
    Public Sub SaveData(ByRef _backglassData As Backglass.Data, Optional ByVal backupname As String = "", Optional ByRef recent As Recent = Nothing)

        'Dim path As String = IO.Path.Combine(EXEDir, ProjectDir)
        If CheckSaveDir(BackglassProjectsPath) Then

            Dim filename As String = Backglass.currentData.Name & If(Not String.IsNullOrEmpty(backupname), "_" & Secured(backupname) & ".b2b", ".b2s")

            ' create or rename the project directory
            If Not String.IsNullOrEmpty(Backglass.currentData.Name) AndAlso Not String.IsNullOrEmpty(Backglass.currentData.LoadedName) AndAlso Not Backglass.currentData.Name.Equals(Backglass.currentData.LoadedName) Then
                If IO.Directory.Exists(IO.Path.Combine(BackglassProjectsPath, Backglass.currentData.LoadedName)) Then
                    FileIO.FileSystem.RenameDirectory(IO.Path.Combine(BackglassProjectsPath, Backglass.currentData.LoadedName), Backglass.currentData.Name)
                End If
                Dim oldfilename As String = Backglass.currentData.LoadedName & If(Not String.IsNullOrEmpty(backupname), "_" & Secured(backupname) & ".b2b", ".b2s")
                If IO.File.Exists(IO.Path.Combine(BackglassProjectsPath, oldfilename)) Then
                    FileIO.FileSystem.RenameFile(IO.Path.Combine(BackglassProjectsPath, oldfilename), filename)
                End If
                If recent IsNot Nothing Then
                    recent.RenameRecentEntry(Backglass.currentData.LoadedName, Backglass.currentData.Name)
                End If
            End If
            If Not IO.Directory.Exists(ProjectPath) Then
                IO.Directory.CreateDirectory(ProjectPath)
            End If
            If Not IO.Directory.Exists(ProjectImagesPath) Then
                IO.Directory.CreateDirectory(ProjectImagesPath)
            End If

            ' the current name becomes the loaded name too
            Backglass.currentData.LoadedName = Backglass.currentData.Name

            ' save data
            Dim XML As Xml.XmlDocument = New Xml.XmlDocument
            Dim nodeHeader As Xml.XmlElement = XML.CreateElement("B2SBackglassData")
            Dim nodeScores As Xml.XmlElement = XML.CreateElement("Scores")
            Dim nodeIllumination As Xml.XmlElement = XML.CreateElement("Illumination")
            Dim nodeAnimations As Xml.XmlElement = XML.CreateElement("Animations")
            Dim nodeImages As Xml.XmlElement = XML.CreateElement("Images")
            XML.AppendChild(nodeHeader)
            nodeHeader.SetAttribute("Version", SaveVersion)
            With _backglassData
                If Not String.IsNullOrEmpty(backupname) Then
                    AddXMLAttribute(XML, nodeHeader, "BackupName", "Value", backupname)
                End If
                AddXMLAttribute(XML, nodeHeader, "ProjectGUID", "Value", .ProjectGUID)
                AddXMLAttribute(XML, nodeHeader, "ProjectGUID2", "Value", .ProjectGUID2)
                AddXMLAttribute(XML, nodeHeader, "AssemblyGUID", "Value", .AssemblyGUID)
                AddXMLAttribute(XML, nodeHeader, "Name", "Value", .Name)
                AddXMLAttribute(XML, nodeHeader, "VSName", "Value", .VSName)
                AddXMLAttribute(XML, nodeHeader, "DualBackglass", "Value", If(.DualBackglass, "1", "0"))
                AddXMLAttribute(XML, nodeHeader, "Author", "Value", .Author)
                AddXMLAttribute(XML, nodeHeader, "Artwork", "Value", .Artwork)
                AddXMLAttribute(XML, nodeHeader, "GameName", "Value", .GameName)
                AddXMLAttribute(XML, nodeHeader, "TableType", "Value", CInt(.TableType).ToString())
                AddXMLAttribute(XML, nodeHeader, "AddEMDefaults", "Value", If(.AddEMDefaults, "1", "0"))
                AddXMLAttribute(XML, nodeHeader, "DMDType", "Value", CInt(.DMDType).ToString())
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
                AddXMLAttribute(XML, nodeHeader, "GrillHeight", "Value", CInt(.GrillHeight).ToString())
                If .SmallGrillHeight > 0 Then
                    AddXMLAttribute(XML, nodeHeader, "GrillHeight", "Small", CInt(.SmallGrillHeight).ToString())
                End If
                AddXMLAttribute(XML, nodeHeader, "DMDDefaultLocationX", "Value", CInt(.DMDDefaultLocation.X).ToString())
                AddXMLAttribute(XML, nodeHeader, "DMDDefaultLocationY", "Value", CInt(.DMDDefaultLocation.Y).ToString())
                If .DMDCopyArea IsNot Nothing AndAlso .DMDCopyArea.Location <> Nothing AndAlso .DMDCopyArea.Size <> Nothing Then
                    AddXMLAttribute(XML, nodeHeader, "DMDCopyAreaX", "Value", CInt(.DMDCopyArea.Location.X).ToString())
                    AddXMLAttribute(XML, nodeHeader, "DMDCopyAreaY", "Value", CInt(.DMDCopyArea.Location.Y).ToString())
                    AddXMLAttribute(XML, nodeHeader, "DMDCopyAreaWidth", "Value", CInt(.DMDCopyArea.Size.Width).ToString())
                    AddXMLAttribute(XML, nodeHeader, "DMDCopyAreaHeight", "Value", CInt(.DMDCopyArea.Size.Height).ToString())
                End If

                ' add animations
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

                ' add score details
                nodeHeader.AppendChild(nodeScores)
                Dim savescores As Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo) = New Generic.SortedList(Of Integer, ReelAndLED.ScoreInfo)
                For Each score As ReelAndLED.ScoreInfo In .Scores
                    savescores.Add(score.ID, score)
                Next
                For Each score As ReelAndLED.ScoreInfo In .DMDScores
                    savescores.Add(score.ID + 1000000, score)
                Next
                For Each score As KeyValuePair(Of Integer, ReelAndLED.ScoreInfo) In savescores
                    If score.Value.SizeX.Width > 0 AndAlso score.Value.SizeX.Height > 0 Then
                        Dim nodeScore As Xml.XmlElement = XML.CreateElement("Score")
                        nodeScores.AppendChild(nodeScore)
                        With score.Value
                            nodeScore.SetAttribute("ID", .ID)
                            nodeScore.SetAttribute("Parent", If((score.Key >= 1000000), "DMD", "Backglass"))
                            nodeScore.SetAttribute("ReelType", .ReelType.ToString())
                            If .ReelColor <> Nothing Then
                                nodeScore.SetAttribute("ReelColor", Color2String(.ReelColor))
                            End If
                            If .B2SStartDigit > 0 Then
                                nodeScore.SetAttribute("B2SStartDigit", .B2SStartDigit.ToString())
                            End If
                            If .B2SScoreType <> eB2SScoreType.NotUsed Then
                                nodeScore.SetAttribute("B2SScoreType", CInt(.B2SScoreType).ToString())
                            End If
                            If .B2SPlayerNo <> eB2SPlayerNo.NotUsed Then
                                nodeScore.SetAttribute("B2SPlayerNo", CInt(.B2SPlayerNo).ToString())
                            End If
                            nodeScore.SetAttribute("Digits", .Digits.ToString())
                            nodeScore.SetAttribute("Spacing", .Spacing.ToString())
                            nodeScore.SetAttribute("DisplayState", CInt(.DisplayState).ToString())
                            nodeScore.SetAttribute("ReelIlluLocation", CInt(.ReelIlluLocation).ToString())
                            nodeScore.SetAttribute("ReelIlluIntensity", .ReelIlluIntensity.ToString())
                            nodeScore.SetAttribute("ReelIlluB2SID", .ReelIlluB2SID.ToString())
                            nodeScore.SetAttribute("ReelIlluB2SIDType", CInt(.ReelIlluB2SIDType).ToString())
                            nodeScore.SetAttribute("ReelIlluB2SValue", .ReelIlluB2SValue.ToString())
                            nodeScore.SetAttribute("LocX", .Location.X)
                            nodeScore.SetAttribute("LocY", .Location.Y)
                            nodeScore.SetAttribute("Width", .Size.Width)
                            nodeScore.SetAttribute("Height", .Size.Height)
                        End With
                    End If
                Next

                ' add illumination
                nodeHeader.AppendChild(nodeIllumination)
                Dim savebulbs As Generic.SortedList(Of Integer, Illumination.BulbInfo) = New Generic.SortedList(Of Integer, Illumination.BulbInfo)
                For Each bulb As Illumination.BulbInfo In .Bulbs
                    savebulbs.Add(bulb.ID, bulb)
                Next
                For Each bulb As Illumination.BulbInfo In .DMDBulbs
                    savebulbs.Add(bulb.ID + 1000000, bulb)
                Next
                For Each bulb As KeyValuePair(Of Integer, Illumination.BulbInfo) In savebulbs
                    If bulb.Value.SizeX.Width > 0 AndAlso bulb.Value.SizeX.Height > 0 Then
                        Dim nodeBulb As Xml.XmlElement = XML.CreateElement("Bulb")
                        nodeIllumination.AppendChild(nodeBulb)
                        With bulb.Value
                            nodeBulb.SetAttribute("ID", .ID)
                            nodeBulb.SetAttribute("Parent", If((bulb.Value.ParentForm = eParentForm.DMD), "DMD", "Backglass"))
                            nodeBulb.SetAttribute("B2SID", .B2SID.ToString())
                            nodeBulb.SetAttribute("B2SIDType", CInt(.B2SIDType).ToString())
                            nodeBulb.SetAttribute("B2SValue", .B2SValue.ToString())
                            nodeBulb.SetAttribute("RomID", .RomID.ToString())
                            nodeBulb.SetAttribute("RomIDType", CInt(.RomIDType).ToString())
                            nodeBulb.SetAttribute("RomInverted", If(.RomInverted, "1", "0"))
                            nodeBulb.SetAttribute("Name", .Name)
                            nodeBulb.SetAttribute("Text", .Text)
                            nodeBulb.SetAttribute("TextAlignment", CInt(.TextAlignment).ToString())
                            nodeBulb.SetAttribute("FontName", .FontName)
                            nodeBulb.SetAttribute("FontSize", CInt(.FontSize * 100).ToString())
                            nodeBulb.SetAttribute("FontStyle", CInt(.FontStyle).ToString())
                            nodeBulb.SetAttribute("Visible", If(.Visible, "1", "0"))
                            nodeBulb.SetAttribute("LocX", .Location.X)
                            nodeBulb.SetAttribute("LocY", .Location.Y)
                            nodeBulb.SetAttribute("Width", .Size.Width)
                            nodeBulb.SetAttribute("Height", .Size.Height)
                            nodeBulb.SetAttribute("InitialState", .InitialState.ToString())
                            nodeBulb.SetAttribute("DualMode", CInt(.DualMode).ToString())
                            If .Intensity <= 0 Then .Intensity = 1
                            nodeBulb.SetAttribute("Intensity", .Intensity.ToString())
                            If .LightColor <> Nothing Then
                                nodeBulb.SetAttribute("LightColor", Color2String(.LightColor))
                            End If
                            If .DodgeColor <> Nothing Then
                                nodeBulb.SetAttribute("DodgeColor", Color2String(.DodgeColor))
                            End If
                            nodeBulb.SetAttribute("IlluMode", CInt(.IlluMode).ToString())
                            nodeBulb.SetAttribute("ZOrder", .ZOrder.ToString())
                            nodeBulb.SetAttribute("IsImageSnippit", If(.IsImageSnippit, "1", "0"))
                            nodeBulb.SetAttribute("SnippitType", CInt(.SnippitInfo.SnippitType).ToString())
                            If .SnippitInfo.SnippitType <> eSnippitType.StandardImage Then
                                If .SnippitInfo.SnippitType = eSnippitType.MechRotatingImage Then
                                    nodeBulb.SetAttribute("SnippitMechID", .SnippitInfo.SnippitMechID.ToString())
                                End If
                                nodeBulb.SetAttribute("SnippitRotatingSteps", .SnippitInfo.SnippitRotatingSteps.ToString())
                                nodeBulb.SetAttribute("SnippitRotatingDirection", CInt(.SnippitInfo.SnippitRotatingDirection).ToString())
                                nodeBulb.SetAttribute("SnippitRotatingStopBehaviour", CInt(.SnippitInfo.SnippitRotatingStopBehaviour).ToString())
                                If .SnippitInfo.SnippitType = eSnippitType.SelfRotatingImage Then
                                    nodeBulb.SetAttribute("SnippitRotatingInterval", .SnippitInfo.SnippitRotatingInterval.ToString())
                                End If
                            End If
                            nodeBulb.SetAttribute("Image", If(.Image IsNot Nothing, ImageToBase64(.Image), ""))
                        End With
                    End If
                Next

                ' add images
                nodeHeader.AppendChild(nodeImages)
                Dim nodeTI As Xml.XmlElement = XML.CreateElement("ThumbnailImages")
                Dim nodeBI As Xml.XmlElement = XML.CreateElement("BackgroundImages")
                Dim nodeII As Xml.XmlElement = XML.CreateElement("IlluminatedImages")
                Dim nodeDI As Xml.XmlElement = XML.CreateElement("DMDImages")
                nodeImages.AppendChild(nodeTI)
                nodeImages.AppendChild(nodeBI)
                nodeImages.AppendChild(nodeII)
                nodeImages.AppendChild(nodeDI)
                ' standard thumbnail image
                If .ThumbnailImage IsNot Nothing Then
                    Dim nodeTIMain As Xml.XmlElement = XML.CreateElement("MainImage")
                    nodeTI.AppendChild(nodeTIMain)
                    nodeTIMain.SetAttribute("Image", ImageToBase64(.ThumbnailImage))
                End If
                ' main background image with some data
                Dim nodeBIMain As Xml.XmlElement = XML.CreateElement("MainImage")
                nodeBI.AppendChild(nodeBIMain)
                nodeBIMain.SetAttribute("Type", "0")
                nodeBIMain.SetAttribute("RomID", "0")
                nodeBIMain.SetAttribute("RomIDType", "0")
                nodeBIMain.SetAttribute("FileName", .ImageFileName)
                nodeBIMain.SetAttribute("Image", ImageToBase64(.Image))
                If Not String.IsNullOrEmpty(.ImageFileName) Then
                    If Not .ImageFileName.StartsWith(".") AndAlso IO.File.Exists(.ImageFileName) Then
                        Try
                            IO.File.Copy(.ImageFileName, IO.Path.Combine(ProjectImagesPath, FileIO.FileSystem.GetFileInfo(.ImageFileName).Name), True)
                        Catch
                        End Try
                    ElseIf .IsSavedImageDirty OrElse Not IO.File.Exists(IO.Path.Combine(ProjectImagesPath, FileIO.FileSystem.GetFileInfo(.ImageFileName).Name)) Then
                        Try
                            .Image.Save(IO.Path.Combine(ProjectImagesPath, FileIO.FileSystem.GetFileInfo(.ImageFileName).Name))
                        Catch
                        End Try
                    End If
                End If
                .IsSavedImageDirty = False
                ' main DMD image with some data
                Dim nodeDIMain As Xml.XmlElement = XML.CreateElement("MainImage")
                nodeDI.AppendChild(nodeDIMain)
                If .DMDImage IsNot Nothing Then
                    nodeDIMain.SetAttribute("FileName", .DMDImageFileName)
                    nodeDIMain.SetAttribute("Image", ImageToBase64(.DMDImage))
                    If Not String.IsNullOrEmpty(.DMDImageFileName) Then
                        If Not .DMDImageFileName.StartsWith(".") AndAlso IO.File.Exists(.DMDImageFileName) Then
                            Try
                                IO.File.Copy(.DMDImageFileName, IO.Path.Combine(ProjectImagesPath, FileIO.FileSystem.GetFileInfo(.DMDImageFileName).Name), True)
                            Catch
                            End Try
                        ElseIf .IsSavedDMDImageDirty OrElse Not IO.File.Exists(IO.Path.Combine(ProjectImagesPath, FileIO.FileSystem.GetFileInfo(.DMDImageFileName).Name)) Then
                            Try
                                .DMDImage.Save(IO.Path.Combine(ProjectImagesPath, FileIO.FileSystem.GetFileInfo(.DMDImageFileName).Name))
                            Catch
                            End Try
                        End If
                    End If
                    .IsSavedDMDImageDirty = False
                End If
                ' get thru all images
                For Each image As Images.ImageInfo In .Images
                    If image.Image IsNot Nothing AndAlso Not image.Text.Equals(.ImageFileName, StringComparison.CurrentCultureIgnoreCase) AndAlso Not image.Text.Equals(.DMDImageFileName, StringComparison.CurrentCultureIgnoreCase) Then
                        Dim node As Xml.XmlElement = Nothing
                        Select Case image.Type
                            Case Images.eImageInfoType.BackgroundImage
                                node = nodeBI
                            Case Images.eImageInfoType.IlluminationImage
                                node = nodeII
                            Case Images.eImageInfoType.DMDImage
                                node = nodeDI
                        End Select
                        If node IsNot Nothing Then
                            Dim nodeImageDetails As Xml.XmlElement = XML.CreateElement("Image")
                            If node.ParentNode Is Nothing Then nodeImages.AppendChild(node)
                            node.AppendChild(nodeImageDetails)
                            With image
                                If .Type = Images.eImageInfoType.BackgroundImage Then
                                    nodeImageDetails.SetAttribute("Type", CInt(.BackgroundImageType).ToString())
                                    nodeImageDetails.SetAttribute("RomID", .RomID.ToString())
                                    nodeImageDetails.SetAttribute("RomIDType", CInt(.RomIDType).ToString())
                                End If
                                nodeImageDetails.SetAttribute("FileName", .Text)
                                nodeImageDetails.SetAttribute("Image", ImageToBase64(.Image))
                                If Not .Text.StartsWith(".") AndAlso IO.File.Exists(.Text) Then
                                    IO.File.Copy(.Text, IO.Path.Combine(ProjectImagesPath, FileIO.FileSystem.GetFileInfo(.Text).Name), True)
                                End If
                            End With
                        End If
                    ElseIf image.Image IsNot Nothing AndAlso image.Text.Equals(.ImageFileName, StringComparison.CurrentCultureIgnoreCase) AndAlso image.Type = Images.eImageInfoType.BackgroundImage Then
                        nodeBIMain.Attributes("Type").InnerText = image.BackgroundImageType
                        nodeBIMain.Attributes("RomID").InnerText = image.RomID.ToString()
                        nodeBIMain.Attributes("RomIDType").InnerText = CInt(image.RomIDType).ToString()
                    End If
                Next
            End With

            ' save data
            XML.Save(IO.Path.Combine(BackglassProjectsPath, filename))

        End If

        _backglassData.IsDirty = False

    End Sub

    Private Function CheckImageFileName(ByVal name As String, ByVal filename As String) As String
        Dim ret As String = filename
        If Not String.IsNullOrEmpty(filename) AndAlso Not IO.File.Exists(filename) Then
            ret = IO.Path.Combine(".\Projects", name, "My Resources", FileIO.FileSystem.GetFileInfo(filename).Name)
            'ret = IO.Path.Combine(BackglassProjectsPath, name, "My Resources", FileIO.FileSystem.GetFileInfo(filename).Name)
        End If
        Return ret
    End Function

End Class
