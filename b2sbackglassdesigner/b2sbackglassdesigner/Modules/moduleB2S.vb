Imports System.Text

Module moduleB2S

    Public Const AppTitle As String = "B2S Backglass Designer"

    Public Const MaxBulbIntensity As Integer = 5

    Public DefaultEMReels As String() = New String() {"EMR_T1_0", "EMR_T2_0", "EMR_T3_0", "EMR_T4_0", "EMR_T5_0", "EMR_T6_0"}
    Public DefaultEMCreditReels As String() = New String() {"EMR_CT1_00", "EMR_CT2_00", "EMR_CT3_00"}
    Public DefaultLEDs As String() = New String() {"LED_0", "LED_Blue_0"}
    Public ImportedStartString As String = "Imported"

    Public ImageFileExtensionFilter As String = My.Resources.TXT_AllImages & "|*.png; *.jpg; *.jpeg; *.gif; *.bmp|PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|BITMAP (*.bmp)|*.bmp|ALL (*.*)|*.*"

    Private EXEDir As String = Application.StartupPath
    Private Const ProjectDir As String = "Projects"

    Public ReadOnly Property BackglassProjectsPath() As String
        Get
            Return IO.Path.Combine(EXEDir, ProjectDir)
        End Get
    End Property
    Public ReadOnly Property ProjectPath() As String
        Get
            Return IO.Path.Combine(BackglassProjectsPath, Backglass.currentData.Name)
        End Get
    End Property
    Public ReadOnly Property ProjectImagesPath() As String
        Get
            Return IO.Path.Combine(ProjectPath, "My Resources")
        End Get
    End Property
    Public ReadOnly Property SettingsFileName As String
        Get
            Return IO.Path.Combine(BackglassProjectsPath, Application.ProductName.Replace(" ", "") & ".Settings.xml")
        End Get
    End Property
    Public ReadOnly Property ImportFileName() As String
        Get
            Return IO.Path.Combine(BackglassProjectsPath, Application.ProductName.Replace(" ", "") & ".Import.xml")
        End Get
    End Property

    Public XmlSettings As Xml.XmlDocument = Nothing

    Public Property CurrentB2S() As String = String.Empty

    Public Property DefaultLightColor() As Color = Color.FromArgb(&HFF, &HFF, &HFE, &HE8)

    Public Property DefaultOpacity() As Single = 1
    Public Property DefaultVPTablesFolder() As String = String.Empty

    Public Property NoToolEvents() As Boolean = False

    Public ReadOnly Property Headline() As String
        Get
            Return If(Not String.IsNullOrEmpty(CurrentB2S), CurrentB2S & " - ", "") & AppTitle
        End Get
    End Property

    Public Sub UpdateStatusBar(form As formDesigner, tab As B2STabPage)
        If tab IsNot Nothing Then
            ' update zoom factor
            form.tscmbZoomInPercent.Text = tab.BackglassData.Zoom.ToString() & "%"
        End If
        ' update file info
        form.tsLabelFileInfo.Padding = New Padding(10, 0, 10, 0)
        If tab IsNot Nothing AndAlso tab.Image IsNot Nothing AndAlso Not String.IsNullOrEmpty(tab.BackglassData.ImageFileName) Then
            form.tsLabelFileInfo.BorderSides = ToolStripStatusLabelBorderSides.Left
            form.tsLabelFileInfo.ImageAlign = ContentAlignment.MiddleRight
            form.tsLabelFileInfo.Image = My.Resources.chooseback3
            form.tsLabelFileInfo.TextAlign = ContentAlignment.MiddleCenter
            form.tsLabelFileInfo.Text = tab.BackglassData.ImageFileName
        Else
            form.tsLabelFileInfo.BorderSides = ToolStripStatusLabelBorderSides.None
            form.tsLabelFileInfo.Image = Nothing
            form.tsLabelFileInfo.Text = String.Empty
        End If
        ' update file size info
        form.tsLabelFileSize.Padding = New Padding(10, 0, 10, 0)
        If tab IsNot Nothing AndAlso tab.Image IsNot Nothing Then
            form.tsLabelFileSize.BorderSides = ToolStripStatusLabelBorderSides.Left
            form.tsLabelFileSize.ImageAlign = ContentAlignment.MiddleRight
            form.tsLabelFileSize.Image = My.Resources.imagesize
            form.tsLabelFileSize.TextAlign = ContentAlignment.MiddleCenter
            If Backglass.currentData.IsDMDImageShown Then
                If tab.DMDImage IsNot Nothing Then
                    form.tsLabelFileSize.Text = tab.DMDImage.Width.ToString() & " x " & tab.DMDImage.Height.ToString()
                Else
                    form.tsLabelFileSize.Text = String.Empty
                End If
            Else
                form.tsLabelFileSize.Text = tab.Image.Width.ToString() & " x " & tab.Image.Height.ToString()
            End If
        Else
            form.tsLabelFileSize.BorderSides = ToolStripStatusLabelBorderSides.None
            form.tsLabelFileSize.Image = Nothing
            form.tsLabelFileSize.Text = String.Empty
        End If
    End Sub

    Public Sub UpdateStatusBar4Mouse(form As formDesigner, tab As B2STabPage, loc As Point)
        form.tsLabelMarker.Padding = New Padding(10, 0, 10, 0)
        If tab IsNot Nothing AndAlso tab.Image IsNot Nothing Then
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

    Public Sub FillReelListView(ByVal type As eImageSetType,
                                ByVal ilReelsAndLEDs As ImageList,
                                ByVal lvReelsAndLEDs As ListView,
                                ByVal ReelLEDList As String(),
                                Optional ByVal clearList As Boolean = False,
                                Optional ByVal showselected As Boolean = False,
                                Optional ByVal selected As String = "")

        If clearList Then
            lvReelsAndLEDs.Items.Clear()
            ilReelsAndLEDs.Images.Clear()
        End If

        ' set list view's image list
        lvReelsAndLEDs.LargeImageList = ilReelsAndLEDs
        lvReelsAndLEDs.SmallImageList = ilReelsAndLEDs

        ' get images of reels and leds into the image list
        Dim i As Integer = ilReelsAndLEDs.Images.Count
        For Each item As String In ReelLEDList
            If Not showselected OrElse selected.Contains("," & item & ",") Then
                ilReelsAndLEDs.Images.Add(item, My.Resources.ResourceManager.GetObject(item))
                lvReelsAndLEDs.Items.Add(item, If(item.Substring(0, 3) = "LED", My.Resources.ReelsAndLEDs_TypeLED, If(item.Substring(0, 5) = "EMR_C", My.Resources.ReelsAndLEDs_TypeEMCreditReel, My.Resources.ReelsAndLEDs_TypeEMReel)) & If(Not showselected, " " & (i + 1).ToString(), ""), i)
                i += 1
            End If
        Next

        Dim imagesets As GeneralData.Data.ImageSetCollection = Nothing
        Dim key As String = String.Empty
        Dim name As String = My.Resources.ReelsAndLEDs_TypeImported & " "
        Select Case type
            Case eImageSetType.ReelImages
                imagesets = GeneralData.currentData.ImportedReelImageSets
                key = ImportedStartString & "EMR_T{0}_0"
                name &= My.Resources.ReelsAndLEDs_TypeEMReel
            Case eImageSetType.CreditReelImages
                imagesets = GeneralData.currentData.ImportedCreditReelImageSets
                key = ImportedStartString & "EMR_CT{0}_00"
                name &= My.Resources.ReelsAndLEDs_TypeEMCreditReel
            Case eImageSetType.LEDImages
                imagesets = GeneralData.currentData.ImportedLEDImageSets
                key = ImportedStartString & "LED_T{0}_0"
                name &= My.Resources.ReelsAndLEDs_TypeLED
        End Select
        If imagesets IsNot Nothing Then
            For Each imageset As KeyValuePair(Of Integer, Image()) In imagesets
                Dim item As String = String.Format(key, imageset.Key.ToString())
                If Not showselected OrElse selected.Contains("," & item & ",") Then
                    ilReelsAndLEDs.Images.Add(item, imageset.Value(0))
                    lvReelsAndLEDs.Items.Add(item, name & " " & (i + 1).ToString(), i)
                    i += 1
                End If
            Next
        End If

    End Sub

    Public Function GetRenderedLEDName(ByVal reel As String) As String
        Dim ret As String = String.Empty
        reel = reel.Replace("RenderedLED0", "RenderedLED").Replace("Dream7LED0", "Dream7LED")
        If reel.StartsWith("RenderedLED", StringComparison.CurrentCultureIgnoreCase) Then
            ret = reel
        ElseIf reel.StartsWith("Dream7LED", StringComparison.CurrentCultureIgnoreCase) Then
            ret = "RenderedLED" & reel.Substring(9)
        End If
        Return ret
    End Function
    Public Function IsReelImageRendered(ByVal reel As String) As Boolean
        Return reel.StartsWith("RenderedLED", StringComparison.CurrentCultureIgnoreCase)
    End Function
    Public Function IsReelImageDream7(ByVal reel As String) As Boolean
        Return reel.StartsWith("Dream7", StringComparison.CurrentCultureIgnoreCase)
    End Function
    Public Function GetReelImage(ByVal reeltype As String,
                                 ByVal reelcolor As Color,
                                 Optional ByVal dream7 As Boolean = False,
                                 Optional ByVal d7thickness As Single = 1,
                                 Optional ByVal d7shear As Single = 1,
                                 Optional ByVal d7glow As Single = 1,
                                 Optional ByVal newsize As Size = Nothing) As Image
        If (String.IsNullOrEmpty(reeltype) OrElse reeltype.Equals("0") OrElse reeltype.Equals("1") OrElse reeltype.Equals("2")) AndAlso Not String.IsNullOrEmpty(Backglass.currentData.ReelType) Then
            reeltype = Backglass.currentData.ReelType
        End If
        If IsReelImageRendered(reeltype) Then
            Static image As Image = Nothing
            Static type As String = String.Empty
            Static color As Color = Nothing
            Static d7 As Boolean = False
            'If Not type.Equals(reeltype.Substring(11)) OrElse Not reelcolor.Equals(color) OrElse Not dream7.Equals(d7) Then
            type = reeltype.Substring(11)
            color = reelcolor
            d7 = dream7
            If Not dream7 Then
                Dim led As B2SRenderedLED = New B2SRenderedLED
                Select Case type
                    Case "7", "8"
                        led.LEDType = B2SRenderedLED.eLEDType.LED8
                    Case "9", "10"
                        led.LEDType = B2SRenderedLED.eLEDType.LED10
                    Case "14"
                        led.LEDType = B2SRenderedLED.eLEDType.LED14
                    Case "16"
                        led.LEDType = B2SRenderedLED.eLEDType.LED16
                End Select
                image = led.Image(reelcolor)
            Else
                Dim led As Dream7Display = New Dream7Display
                led.Size = newsize
                If led.Width <= 0 OrElse led.Height <= 0 Then led.Size = New Size(90, 120)
                led.Digits = 1
                Select Case type
                    Case "7", "8"
                        led.Type = SegmentNumberType.SevenSegment
                    Case "9", "10"
                        led.Type = SegmentNumberType.TenSegment
                    Case "14"
                        led.Type = SegmentNumberType.FourteenSegment
                    Case "16"
                        'led.Type = 
                End Select
                led.SetValue(0, 65535)
                led.ScaleMode = ScaleMode.Stretch
                'led.ForeColor = reelcolor
                If Not reelcolor.Equals(color.FromArgb(0, 0, 0)) Then
                    led.LightColor = reelcolor
                    led.GlassColor = color.FromArgb(Math.Min(reelcolor.R + 50, 255), Math.Min(reelcolor.G + 50, 255), Math.Min(reelcolor.B + 50, 255))
                    led.GlassColorCenter = color.FromArgb(Math.Min(reelcolor.R + 70, 255), Math.Min(reelcolor.G + 70, 255), Math.Min(reelcolor.B + 70, 255))
                Else
                    led.LightColor = color.FromArgb(15, 15, 15)
                    led.GlassColor = color.FromArgb(15, 15, 15)
                    led.GlassColorCenter = color.FromArgb(15, 15, 15)
                End If
                led.OffColor = color.FromArgb(15, 15, 15)
                'led.BackColor = Color.FromArgb(5, 5, 5)
                led.BackColor = color.FromArgb(15, 15, 15)
                led.GlassAlpha = 140
                led.GlassAlphaCenter = 255
                'led.Thickness = d7thickness * 1.2
                led.Shear = d7shear
                led.Glow = d7glow
                Try
                    Dim bmp As Bitmap = New Bitmap(led.Width, led.Height)
                    led.DrawToBitmap(bmp, New Rectangle(New Point(0, 0), led.Size))
                    'bmp.Save("c:\tmp\bmp.png")
                    image = bmp
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
            'End If
            Return image
        ElseIf reeltype.StartsWith(ImportedStartString) Then
            Try
                Select Case reeltype.Substring(8, 5)
                    Case "EMR_T" : Return GeneralData.currentData.ImportedReelImageSets(CInt(reeltype.Substring(13).Replace("_0", "")))(0)
                    Case "EMR_CT" : Return GeneralData.currentData.ImportedCreditReelImageSets(CInt(reeltype.Substring(14).Replace("_0", "")))(0)
                    Case "LED_T" : Return GeneralData.currentData.ImportedLEDImageSets(CInt(reeltype.Substring(13).Replace("_0", "")))(0)
                    Case Else : Return Nothing
                End Select
            Catch
                Return Nothing
            End Try
        Else
            Return My.Resources.ResourceManager.GetObject(If(reeltype.EndsWith("0"), reeltype, reeltype & "_0"))
        End If
    End Function
    Public Function GetDream7LEDType(ByVal type As String) As String
        Select Case type
            Case "7", "8"
                Return "SevenSegment"
            Case "9", "10"
                Return "TenSegment"
            Case "14"
                Return "FourteenSegment"
            Case "16"
                Return "SixteenSegment"
            Case Else
                Return "SevenSegment"
        End Select
    End Function
    Public Function GetFirstReelType() As String
        Dim ret As String = String.Empty
        For Each reeltype As String In Backglass.currentData.ReelType.Split(",")
            If Not String.IsNullOrEmpty(reeltype) Then
                ret = reeltype
                Exit For
            End If
        Next
        Return ret
    End Function

    Public Function ImageToBase64(image As Image) As String
        If image IsNot Nothing Then
            With New System.Drawing.ImageConverter
                Dim bytes() As Byte = CType(.ConvertTo(image, GetType(Byte())), Byte())
                Return Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks)
            End With
        Else
            Return String.Empty
        End If
    End Function
    Public Function Base64ToImage(data As String) As Image
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

    Public Function WavToBase64(stream As IO.Stream) As String
        If stream IsNot Nothing Then
            Dim bytes() As Byte
            ReDim bytes(stream.Length - 1)
            Using reader As IO.BinaryReader = New IO.BinaryReader(stream)
                Dim length As Integer = reader.Read(bytes, 0, stream.Length)
            End Using
            Return Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks)
        Else
            Return String.Empty
        End If
    End Function
    Public Function Base64ToWav(data As String) As IO.Stream
        If data.Length > 0 Then
            Dim bytes() As Byte = Convert.FromBase64String(data)
            Return New IO.MemoryStream(bytes)
        Else
            Return Nothing
        End If
    End Function

    Public Function Color2String(ByVal color As Color) As String
        Return color.R.ToString() & "." & color.G.ToString() & "." & color.B.ToString()
    End Function
    Public Function String2Color(ByVal color As String) As Color
        Dim colorvalues As String() = color.Split(".")
        Return Drawing.Color.FromArgb(CInt(colorvalues(0)), CInt(colorvalues(1)), CInt(colorvalues(2)))
    End Function

    Public Function TranslateIndex2DodgeColor(ByVal index As Integer) As Color
        Select Case index
            Case 1 : Return Color.Red
            Case 2 : Return Color.FromArgb(0, 255, 0)
            Case 3 : Return Color.Blue
            Case 4 : Return Color.Yellow
            Case 5 : Return Color.FromArgb(0, 255, 255)
            Case 6 : Return Color.FromArgb(255, 0, 255)
            Case 7 : Return Color.White
            Case Else : Return Nothing
        End Select
    End Function
    Public Function TranslateDodgeColor2Index(ByVal col As Color) As Integer
        If col.Equals(Color.FromArgb(255, 0, 0)) OrElse col.Equals(Color.Red) Then
            Return 1
        ElseIf col.Equals(Color.FromArgb(0, 255, 0)) OrElse col.Equals(Color.Green) Then
            Return 2
        ElseIf col.Equals(Color.FromArgb(0, 0, 255)) OrElse col.Equals(Color.Blue) Then
            Return 3
        ElseIf col.Equals(Color.FromArgb(255, 255, 0)) OrElse col.Equals(Color.Yellow) Then
            Return 4
        ElseIf col.Equals(Color.FromArgb(0, 255, 255)) OrElse col.Equals(Color.Magenta) Then
            Return 5
        ElseIf col.Equals(Color.FromArgb(255, 0, 255)) Then
            Return 6
        ElseIf col.Equals(Color.FromArgb(255, 255, 255)) OrElse col.Equals(Color.White) Then
            Return 7
        Else
            Return 0
        End If
    End Function

    Public Function Secured(ByVal text As String) As String
        Dim sb As StringBuilder = New StringBuilder()
        text = text.Replace(" ", "")
        For Each letter As String In text.ToCharArray()
            Dim a As Integer = Asc(letter)
            If (a < Asc("a") OrElse a > Asc("z")) AndAlso (a < Asc("A") OrElse a > Asc("Z")) AndAlso (a < Asc("0") OrElse a > Asc("9")) Then
                letter = "_"
            End If
            sb.Append(letter)
        Next
        Return sb.ToString()
    End Function

    Public Function IsFontInstalled(fontName As String) As Boolean
        Using testFont = New Font(fontName, 8)
            Return 0 = String.Compare(fontName, testFont.Name, StringComparison.InvariantCultureIgnoreCase)
        End Using
    End Function

    Public Function IsOverlappingB2SStartDigit() As Boolean
        Dim ret As Boolean = False
        Dim usedDigits As Generic.List(Of Integer) = New Generic.List(Of Integer)
        For Each score As ReelAndLED.ScoreInfo In Backglass.currentScores
            If score.B2SStartDigit > 0 Then
                For i As Integer = 0 To score.Digits - 1
                    If usedDigits.Contains(score.B2SStartDigit + i) Then
                        ret = True
                        Exit For
                    End If
                    usedDigits.Add(score.B2SStartDigit + i)
                Next
            End If
        Next
        Return ret
    End Function

    Public Function IsThereAlreadyOneSelfRotatingSnippit(ByVal bulbid As Integer) As Boolean
        Dim ret As Boolean = False
        For Each illu As Illumination.BulbInfo In Backglass.currentBulbs
            If illu.IsImageSnippit AndAlso illu.ID <> bulbid AndAlso illu.SnippitInfo.SnippitType = eSnippitType.SelfRotatingImage Then
                ret = True
                Exit For
            End If
        Next
        Return ret
    End Function

End Module
