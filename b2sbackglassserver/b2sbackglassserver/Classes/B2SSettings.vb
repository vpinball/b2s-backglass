Imports System
Imports System.IO

Public Class B2SSettings

    Public Const DirectB2SVersion As String = "1.3.0.4"
    Public Const MinimumDirectB2SVersion As String = "1.0"
    Public Shared Property BackglassFileVersion() As String = String.Empty

    Public Enum eDefaultStartMode
        Standard = 1
        EXE = 2
    End Enum
    
    Public Enum eDMDTypes
        Standard = 0
        TwoMonitorSetup = 1
        ThreeMonitorSetup = 2
        Hidden = 3
    End Enum
    Public Enum eLEDTypes
        Undefined = 0
        Rendered = 1
        Dream7 = 2
    End Enum
    Public Enum eImageFileType
        PNG = 0
        JPG = 1
        GIF = 2
        BMP = 3
    End Enum
    Public Enum eDualMode
        NotSet = 0
        Authentic = 1
        Fantasy = 2
    End Enum

    Private Const filename As String = "B2STableSettings.xml"

    Public Shared Property MatchingFileName() As String = String.Empty
    Public Shared Property MatchingFileNames() As String() = Nothing

    Public Shared Property LogPath() As String = String.Empty
    Private Shared Property _IsLampsStateLogOn() As Boolean = False
    Public Shared Property IsLampsStateLogOn() As Boolean
        Get
            Return _IsLampsStateLogOn
        End Get
        Set(ByVal value As Boolean)
            _IsLampsStateLogOn = value
            B2SData.IsInfoDirty = True
        End Set
    End Property
    Private Shared Property _IsSolenoidsStateLogOn() As Boolean = False
    Public Shared Property IsSolenoidsStateLogOn() As Boolean
        Get
            Return _IsSolenoidsStateLogOn
        End Get
        Set(ByVal value As Boolean)
            _IsSolenoidsStateLogOn = value
            B2SData.IsInfoDirty = True
        End Set
    End Property
    Private Shared Property _IsGIStringsStateLogOn() As Boolean = False
    Public Shared Property IsGIStringsStateLogOn() As Boolean
        Get
            Return _IsGIStringsStateLogOn
        End Get
        Set(ByVal value As Boolean)
            _IsGIStringsStateLogOn = value
            B2SData.IsInfoDirty = True
        End Set
    End Property
    Private Shared Property _IsLEDsStateLogOn() As Boolean = False
    Public Shared Property IsLEDsStateLogOn() As Boolean
        Get
            Return _IsLEDsStateLogOn
        End Get
        Set(ByVal value As Boolean)
            _IsLEDsStateLogOn = value
            B2SData.IsInfoDirty = True
        End Set
    End Property
    Public Shared Property IsPaintingLogOn() As Boolean = True
    Public Shared Property IsStatisticsBackglassOn() As Boolean = True

    Public Shared Property ShowStartupError() As Boolean = False

    Public Shared Property ArePluginsOn() As Boolean = False

    Public Shared Property CPUAffinityMask() As Integer = 0

    Public Shared Property PluginHost() As PluginHost = Nothing

    Public Shared Property ScreenshotPath() As String = String.Empty
    Public Shared Property ScreenshotFileType() As eImageFileType = eImageFileType.PNG

    Public Shared Property DMDType() As eDMDTypes = eDMDTypes.Standard

    Public Shared Property AllOut() As Boolean = False
    Private Shared Property _AllOff() As Boolean = False
    Public Shared Property AllOff() As Boolean
        Get
            Return _AllOff
        End Get
        Set(ByVal value As Boolean)
            _AllOff = value
            B2SData.IsInfoDirty = True
        End Set
    End Property
    Private Shared Property _LampsOff() As Boolean = False
    Public Shared Property LampsOff() As Boolean
        Get
            Return _LampsOff
        End Get
        Set(ByVal value As Boolean)
            _LampsOff = value
            B2SData.IsInfoDirty = True
        End Set
    End Property
    Private Shared Property _SolenoidsOff() As Boolean = False
    Public Shared Property SolenoidsOff() As Boolean
        Get
            Return _SolenoidsOff
        End Get
        Set(ByVal value As Boolean)
            _SolenoidsOff = value
            B2SData.IsInfoDirty = True
        End Set
    End Property
    Private Shared Property _GIStringsOff() As Boolean = False
    Public Shared Property GIStringsOff() As Boolean
        Get
            Return _GIStringsOff
        End Get
        Set(ByVal value As Boolean)
            _GIStringsOff = value
            B2SData.IsInfoDirty = True
        End Set
    End Property
    Public Shared Property LEDsOff() As Boolean = False
    Public Shared Property StartAsEXE() As Boolean = True
    Public Shared Property DefaultStartMode() As eDefaultStartMode = eDefaultStartMode.EXE
    Public Shared Property DisableFuzzyMatching() As Boolean = False

    Public Shared Property LampsSkipFrames() As Integer = 0
    Public Shared Property SolenoidsSkipFrames() As Integer = 0
    Public Shared Property GIStringsSkipFrames() As Integer = 0
    Public Shared Property LEDsSkipFrames() As Integer = 0

    Public Shared Property UsedLEDType() As eLEDTypes = eLEDTypes.Undefined

    Public Shared Property IsGlowBulbOn() As Boolean = False
    Public Shared Property GlowIndex() As Integer = -1
    Public Shared Property DefaultGlow() As Integer = -1
    Public Shared Property FormToFront() As Boolean = True

    Public Shared Property HideGrill() As System.Windows.Forms.CheckState = Windows.Forms.CheckState.Indeterminate
    Public Shared Property HideB2SDMD() As Boolean = False
    Public Shared Property HideDMD() As System.Windows.Forms.CheckState = Windows.Forms.CheckState.Indeterminate

    Public Shared Property AnimationSlowDowns() As Generic.Dictionary(Of String, Integer) = New Generic.Dictionary(Of String, Integer)
    Public Shared Property AllAnimationSlowDown() As Integer = 1

    Public Shared Property HyperpinXMLFile() As String = String.Empty

    Public Shared Property CurrentDualMode() As B2SSettings.eDualMode = eDualMode.NotSet

    Public Shared Property StartBackground() As Boolean = False


    Public Shared ReadOnly Property IsROMControlled() As Boolean
        Get
            Return (GameName.Length > 0)
        End Get
    End Property

    Private Shared _GameName As String = String.Empty
    Public Shared Property GameName() As String
        Get
            Return _GameName
        End Get
        Set(ByVal value As String)
            _GameName = value
            Load(False)
        End Set
    End Property

    Private Shared _IsGameNameFound As Boolean = False
    Public Shared ReadOnly Property IsGameNameFound() As Boolean
        Get
            Return _IsGameNameFound
        End Get
    End Property

    Private Shared _B2SName As String = String.Empty
    Public Shared Property B2SName() As String
        Get
            Return _B2SName
        End Get
        Set(ByVal value As String)
            _B2SName = value
            Load(False)
        End Set
    End Property

    Public Shared Sub Load(Optional ByVal resetLogs As Boolean = True,
                           Optional ByVal justLoadPluginSetting As Boolean = False)
        ClearAll()
        ' load settings
        If IO.File.Exists(filename) Then
            Dim XML As Xml.XmlDocument = New Xml.XmlDocument
            XML.Load(filename)
            If XML IsNot Nothing AndAlso XML.SelectSingleNode("B2STableSettings") IsNot Nothing Then
                Dim nodeHeader As Xml.XmlNode = XML.SelectSingleNode("B2STableSettings")
                If justLoadPluginSetting Then
                    ' get plugin status
                    If nodeHeader.SelectSingleNode("ArePluginsOn") IsNot Nothing Then ArePluginsOn = (nodeHeader.SelectSingleNode("ArePluginsOn").InnerText = "1")
                Else
                    ' get default start mode
                    If nodeHeader.SelectSingleNode("DefaultStartMode") IsNot Nothing Then DefaultStartMode = CInt(nodeHeader.SelectSingleNode("DefaultStartMode").InnerText)
                    If DefaultStartMode <> eDefaultStartMode.Standard Then DefaultStartMode = eDefaultStartMode.EXE
                    If DefaultStartMode = eDefaultStartMode.Standard Then StartAsEXE = False
                    If nodeHeader.SelectSingleNode("DisableFuzzyMatching") IsNot Nothing Then DisableFuzzyMatching = (nodeHeader.SelectSingleNode("DisableFuzzyMatching").InnerText = "1")

                    ' get overall settings
                    If nodeHeader.SelectSingleNode("CPUAffinityMask") IsNot Nothing Then CPUAffinityMask = CInt(nodeHeader.SelectSingleNode("CPUAffinityMask").InnerText)
                        If nodeHeader.SelectSingleNode("LogPath") IsNot Nothing Then LogPath = nodeHeader.SelectSingleNode("LogPath").InnerText
                        If nodeHeader.SelectSingleNode("IsLampsStateLogOn") IsNot Nothing Then IsLampsStateLogOn = (nodeHeader.SelectSingleNode("IsLampsStateLogOn").InnerText = "1")
                        If nodeHeader.SelectSingleNode("IsSolenoidsStateLogOn") IsNot Nothing Then IsSolenoidsStateLogOn = (nodeHeader.SelectSingleNode("IsSolenoidsStateLogOn").InnerText = "1")
                        If nodeHeader.SelectSingleNode("IsGIStringsStateLogOn") IsNot Nothing Then IsGIStringsStateLogOn = (nodeHeader.SelectSingleNode("IsGIStringsStateLogOn").InnerText = "1")
                        If nodeHeader.SelectSingleNode("IsLEDsStateLogOn") IsNot Nothing Then IsLEDsStateLogOn = (nodeHeader.SelectSingleNode("IsLEDsStateLogOn").InnerText = "1")
                        If nodeHeader.SelectSingleNode("IsPaintingLogOn") IsNot Nothing Then IsPaintingLogOn = (nodeHeader.SelectSingleNode("IsPaintingLogOn").InnerText = "1")
                        If nodeHeader.SelectSingleNode("IsStatisticsBackglassOn") IsNot Nothing Then IsStatisticsBackglassOn = (nodeHeader.SelectSingleNode("IsStatisticsBackglassOn").InnerText = "1")
                        If nodeHeader.SelectSingleNode("ShowStartupError") IsNot Nothing Then ShowStartupError = (nodeHeader.SelectSingleNode("ShowStartupError").InnerText = "1")
                If nodeHeader.SelectSingleNode("FormToFront") IsNot Nothing Then FormToFront = (nodeHeader.SelectSingleNode("FormToFront").InnerText = "1")
                        If nodeHeader.SelectSingleNode("ScreenshotPath") IsNot Nothing Then
                            ScreenshotPath = nodeHeader.SelectSingleNode("ScreenshotPath").InnerText
                            ScreenshotFileType = CInt(nodeHeader.SelectSingleNode("ScreenshotFileType").InnerText)
                        End If
                        If nodeHeader.SelectSingleNode("HyperpinXMLFile") IsNot Nothing Then
                            HyperpinXMLFile = nodeHeader.SelectSingleNode("HyperpinXMLFile").InnerText
                        End If
                        If resetLogs AndAlso (IsLampsStateLogOn OrElse IsSolenoidsStateLogOn OrElse IsGIStringsStateLogOn OrElse IsLEDsStateLogOn OrElse IsPaintingLogOn) Then
                            AddNode(XML, nodeHeader, "IsLampsStateLogOn", "0")
                            AddNode(XML, nodeHeader, "IsSolenoidsStateLogOn", "0")
                            AddNode(XML, nodeHeader, "IsGIStringsStateLogOn", "0")
                            AddNode(XML, nodeHeader, "IsLEDsStateLogOn", "0")
                            AddNode(XML, nodeHeader, "IsPaintingLogOn", "0")
                            XML.Save(filename)
                        End If
                        ' set default dual mode
                        'If B2SData.DualBackglass Then
                        CurrentDualMode = eDualMode.Authentic
                        'End If
                        ' maybe get table specific settings
                        If Not String.IsNullOrEmpty(GameName) OrElse Not String.IsNullOrEmpty(B2SName) Then
                            Dim nodeTable As Xml.XmlElement = nodeHeader.SelectSingleNode(If(Not String.IsNullOrEmpty(GameName), GameName, B2SName))
                            If nodeTable IsNot Nothing Then
                                _IsGameNameFound = True
                                If nodeTable.SelectSingleNode("HideGrill") IsNot Nothing Then HideGrill = CInt(nodeTable.SelectSingleNode("HideGrill").InnerText)
                                If nodeTable.SelectSingleNode("HideB2SDMD") IsNot Nothing Then HideB2SDMD = (nodeTable.SelectSingleNode("HideB2SDMD").InnerText = "1")
                                If nodeTable.SelectSingleNode("HideDMD") IsNot Nothing Then HideDMD = CInt(nodeTable.SelectSingleNode("HideDMD").InnerText)
                                If nodeTable.SelectSingleNode("MatchingFileName") IsNot Nothing Then MatchingFileName = nodeTable.SelectSingleNode("MatchingFileName").InnerText
                                If nodeTable.SelectSingleNode("LampsBlackTurns") IsNot Nothing Then LampsSkipFrames = CInt(nodeTable.SelectSingleNode("LampsBlackTurns").InnerText)
                                If nodeTable.SelectSingleNode("SolenoidsBlackTurns") IsNot Nothing Then SolenoidsSkipFrames = CInt(nodeTable.SelectSingleNode("SolenoidsBlackTurns").InnerText)
                                If nodeTable.SelectSingleNode("GIStringsBlackTurns") IsNot Nothing Then GIStringsSkipFrames = CInt(nodeTable.SelectSingleNode("GIStringsBlackTurns").InnerText)
                                If nodeTable.SelectSingleNode("LEDsBlackTurns") IsNot Nothing Then LEDsSkipFrames = CInt(nodeTable.SelectSingleNode("LEDsBlackTurns").InnerText)
                                If nodeTable.SelectSingleNode("LampsSkipFrames") IsNot Nothing Then LampsSkipFrames = CInt(nodeTable.SelectSingleNode("LampsSkipFrames").InnerText)
                                If nodeTable.SelectSingleNode("SolenoidsSkipFrames") IsNot Nothing Then SolenoidsSkipFrames = CInt(nodeTable.SelectSingleNode("SolenoidsSkipFrames").InnerText)
                                If nodeTable.SelectSingleNode("GIStringsSkipFrames") IsNot Nothing Then GIStringsSkipFrames = CInt(nodeTable.SelectSingleNode("GIStringsSkipFrames").InnerText)
                                If nodeTable.SelectSingleNode("LEDsSkipFrames") IsNot Nothing Then LEDsSkipFrames = CInt(nodeTable.SelectSingleNode("LEDsSkipFrames").InnerText)
                                If nodeTable.SelectSingleNode("UsedLEDType") IsNot Nothing Then UsedLEDType = CInt(nodeTable.SelectSingleNode("UsedLEDType").InnerText)
                                If nodeTable.SelectSingleNode("IsGlowBulbOn") IsNot Nothing Then IsGlowBulbOn = (nodeTable.SelectSingleNode("IsGlowBulbOn").InnerText = "1")
                                If nodeTable.SelectSingleNode("GlowIndex") IsNot Nothing Then GlowIndex = CInt(nodeTable.SelectSingleNode("GlowIndex").InnerText)
                                If nodeTable.SelectSingleNode("StartAsEXE") IsNot Nothing Then StartAsEXE = (nodeTable.SelectSingleNode("StartAsEXE").InnerText = "1")
                                If nodeTable.SelectSingleNode("DualMode") IsNot Nothing Then CurrentDualMode = CInt(nodeTable.SelectSingleNode("DualMode").InnerText)
                        If nodeTable.SelectSingleNode("StartBackground") IsNot Nothing Then StartBackground = (nodeTable.SelectSingleNode("StartBackground").InnerText = "1")
                        If nodeTable.SelectSingleNode("FormToFront") IsNot Nothing Then FormToFront = (nodeTable.SelectSingleNode("FormToFront").InnerText = "1")

                                Dim nodeAnimations As Xml.XmlElement = nodeTable.SelectSingleNode("Animations")
                                If nodeAnimations IsNot Nothing Then
                                    For Each nodeAnimation As Xml.XmlElement In nodeAnimations.ChildNodes
                                        If nodeAnimation.Name.Equals("Animation") Then
                                            AnimationSlowDowns.Add(nodeAnimation.Attributes("Name").InnerText, CInt(nodeAnimation.Attributes("SlowDown").InnerText))
                                        ElseIf nodeAnimation.Name.Equals("AllAnimations") Then
                                            AllAnimationSlowDown = CInt(nodeAnimation.Attributes("SlowDown").InnerText)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If
        End If
    End Sub
    Public Shared Sub Save(Optional ByVal b2sanimation As B2SAnimation = Nothing,
                           Optional ByVal justSaveSnifferCheck As Boolean = False,
                           Optional ByVal justSaveDualMode As Boolean = False,
                           Optional ByVal justSaveHyperpinXMLFile As Boolean = False)
        ' save settings
        Dim XML As Xml.XmlDocument = New Xml.XmlDocument
        If IO.File.Exists(filename) Then XML.Load(filename)
        Dim nodeHeader As Xml.XmlElement = AddHeader(XML, XML, "B2STableSettings")
        If justSaveSnifferCheck Then
            AddNode(XML, nodeHeader, "IsStatisticsBackglassOn", If(IsStatisticsBackglassOn, "1", "0"))
        ElseIf justSaveDualMode Then
            If B2SData.DualBackglass AndAlso (Not String.IsNullOrEmpty(GameName) OrElse Not String.IsNullOrEmpty(B2SName)) Then
                Dim nodeTable As Xml.XmlElement = AddHeader(XML, nodeHeader, If(Not String.IsNullOrEmpty(GameName), GameName, B2SName))
                AddNode(XML, nodeTable, "DualMode", CInt(CurrentDualMode).ToString())
            End If
        ElseIf justSaveHyperpinXMLFile Then
            AddNode(XML, nodeHeader, "HyperpinXMLFile", HyperpinXMLFile)
        Else
            AddNode(XML, nodeHeader, "ArePluginsOn", If(ArePluginsOn, "1", "0"))
            AddNode(XML, nodeHeader, "DefaultStartMode", CInt(DefaultStartMode).ToString())
            AddNode(XML, nodeHeader, "DisableFuzzyMatching", If(DisableFuzzyMatching, "1", "0"))
            AddNode(XML, nodeHeader, "LogPath", LogPath)
            AddNode(XML, nodeHeader, "IsLampsStateLogOn", If(IsLampsStateLogOn, "1", "0"))
            AddNode(XML, nodeHeader, "IsSolenoidsStateLogOn", If(IsSolenoidsStateLogOn, "1", "0"))
            AddNode(XML, nodeHeader, "IsGIStringsStateLogOn", If(IsGIStringsStateLogOn, "1", "0"))
            AddNode(XML, nodeHeader, "IsLEDsStateLogOn", If(IsLEDsStateLogOn, "1", "0"))
            AddNode(XML, nodeHeader, "IsPaintingLogOn", If(IsPaintingLogOn, "1", "0"))
            AddNode(XML, nodeHeader, "IsStatisticsBackglassOn", If(IsStatisticsBackglassOn, "1", "0"))
            AddNode(XML, nodeHeader, "ShowStartupError", If(ShowStartupError, "1", "0"))
            AddNode(XML, nodeHeader, "ScreenshotPath", ScreenshotPath)
            AddNode(XML, nodeHeader, "ScreenshotFileType", CInt(ScreenshotFileType).ToString())
            If Not String.IsNullOrEmpty(GameName) OrElse Not String.IsNullOrEmpty(B2SName) Then
                Dim nodeTable As Xml.XmlElement = AddHeader(XML, nodeHeader, If(Not String.IsNullOrEmpty(GameName), GameName, B2SName))
                nodeTable.RemoveAll()
                AddNode(XML, nodeTable, "HideGrill", CInt(HideGrill).ToString())
                AddNode(XML, nodeTable, "HideB2SDMD", If(HideB2SDMD, "1", "0"))
                AddNode(XML, nodeTable, "HideDMD", CInt(HideDMD).ToString())
                If Not String.IsNullOrEmpty(MatchingFileName) Then
                    AddNode(XML, nodeTable, "MatchingFileName", MatchingFileName)
                End If
                AddNode(XML, nodeTable, "LampsSkipFrames", LampsSkipFrames.ToString())
                AddNode(XML, nodeTable, "SolenoidsSkipFrames", SolenoidsSkipFrames.ToString())
                AddNode(XML, nodeTable, "GIStringsSkipFrames", GIStringsSkipFrames.ToString())
                If Not B2SData.UseReels Then
                    AddNode(XML, nodeTable, "LEDsSkipFrames", LEDsSkipFrames.ToString())
                    AddNode(XML, nodeTable, "UsedLEDType", CInt(UsedLEDType).ToString())
                    AddNode(XML, nodeTable, "IsGlowBulbOn", If(IsGlowBulbOn, "1", "0"))
                    AddNode(XML, nodeTable, "GlowIndex", GlowIndex.ToString())
                End If
                AddNode(XML, nodeTable, "StartAsEXE", If(StartAsEXE, "1", "0"))
                AddNode(XML, nodeTable, "StartBackground", If(StartBackground, "1", "0"))
                AddNode(XML, nodeTable, "FormToFront", If(FormToFront, "1", "0"))

                If b2sanimation IsNot Nothing Then
                    Dim nodeAnimations As Xml.XmlElement = AddHeader(XML, nodeTable, "Animations")
                    nodeAnimations.RemoveAll()
                    For Each animationname As String In b2sanimation.Animations
                        Dim slowdown As Integer = b2sanimation.AnimationSlowDown(animationname)
                        If slowdown <> 1 Then
                            Dim nodeAnimation As Xml.XmlElement = XML.CreateElement("Animation")
                            nodeAnimations.AppendChild(nodeAnimation)
                            nodeAnimation.SetAttribute("Name", animationname)
                            nodeAnimation.SetAttribute("SlowDown", slowdown)
                        End If
                    Next
                    If AllAnimationSlowDown <> 1 Then
                        Dim nodeAnimation As Xml.XmlElement = XML.CreateElement("AllAnimations")
                        nodeAnimations.AppendChild(nodeAnimation)
                        nodeAnimation.SetAttribute("SlowDown", AllAnimationSlowDown)
                    End If
                End If
            End If
        End If
        XML.Save(filename)
    End Sub

    Public Shared Sub ClearAll()
        ' do not add GameName or B2SName here
        DefaultStartMode = eDefaultStartMode.EXE
        DisableFuzzyMatching = False
        LogPath = String.Empty
        IsLampsStateLogOn = False
        IsSolenoidsStateLogOn = False
        IsGIStringsStateLogOn = False
        IsLEDsStateLogOn = False
        IsPaintingLogOn = False
        IsStatisticsBackglassOn = False
        ScreenshotPath = String.Empty
        ScreenshotFileType = eImageFileType.PNG
        DMDType = eDMDTypes.Standard
        AllOut = False
        AllOff = False
        LampsOff = False
        SolenoidsOff = False
        GIStringsOff = False
        LEDsOff = False
        StartAsEXE = True
        LampsSkipFrames = 0
        SolenoidsSkipFrames = 0
        GIStringsSkipFrames = 0
        LEDsSkipFrames = 0
        UsedLEDType = eLEDTypes.Undefined
        IsGlowBulbOn = False
        GlowIndex = -1
        DefaultGlow = -1
        HideGrill = System.Windows.Forms.CheckState.Indeterminate
        HideB2SDMD = False
        HideDMD = System.Windows.Forms.CheckState.Indeterminate
        HyperpinXMLFile = String.Empty
        AnimationSlowDowns.Clear()
        AllAnimationSlowDown = 1
        CurrentDualMode = eDualMode.NotSet
    End Sub

    Private Shared Function AddHeader(XML As Xml.XmlDocument, parentnode As Xml.XmlNode, nodename As String) As Xml.XmlNode
        Dim node As Xml.XmlElement = parentnode.SelectSingleNode(nodename)
        If node Is Nothing Then
            node = XML.CreateElement(nodename)
            parentnode.AppendChild(node)
        End If
        Return node
    End Function
    Private Shared Sub AddNode(XML As Xml.XmlDocument, parentnode As Xml.XmlNode, nodename As String, nodevalue As String)
        Dim node As Xml.XmlElement = parentnode.SelectSingleNode(nodename)
        If node Is Nothing Then
            node = XML.CreateElement(nodename)
            parentnode.AppendChild(node)
        End If
        node.InnerText = nodevalue
    End Sub

    Public Shared Function LocateHyperpinXMLFile() As Boolean
        If Not String.IsNullOrEmpty(HyperpinXMLFile) AndAlso HyperpinXMLFile <> "Unknown" AndAlso Not IO.File.Exists(HyperpinXMLFile) Then
            HyperpinXMLFile = String.Empty
        End If
        If String.IsNullOrEmpty(HyperpinXMLFile) Then
            Dim vp As DirectoryInfo = Directory.GetParent(IO.Directory.GetCurrentDirectory())
            Dim vpparent As DirectoryInfo = vp.Parent()
            ' get thru all directories of the parent of visual pinball
            HyperpinXMLFile = CheckDir(vpparent.FullName)
            ' maybe check the next parent
            If String.IsNullOrEmpty(HyperpinXMLFile) Then
                Dim nextparent As DirectoryInfo = vpparent
                Do While Not vp.Root.FullName.Equals(nextparent.Parent.FullName)
                    nextparent = nextparent.Parent
                Loop
                If Not vpparent.Equals(nextparent) Then
                    HyperpinXMLFile = CheckDir(nextparent.FullName)
                End If
            End If
            If String.IsNullOrEmpty(HyperpinXMLFile) Then
                If IO.Directory.Exists(IO.Path.Combine(vp.Root.FullName, "Hyperpin")) Then
                    HyperpinXMLFile = CheckDir(IO.Path.Combine(vp.Root.FullName, "Hyperpin"))
                End If
            End If
            If String.IsNullOrEmpty(HyperpinXMLFile) Then
                If IO.Directory.Exists(IO.Path.Combine(vp.Root.FullName, "PinballX")) Then
                    HyperpinXMLFile = CheckDir(IO.Path.Combine(vp.Root.FullName, "PinballX"))
                End If
            End If
            If String.IsNullOrEmpty(HyperpinXMLFile) Then
                ' not found
                HyperpinXMLFile = "Unknown"
            End If
            Save(, , , True)
        End If
        Return (HyperpinXMLFile <> "Unknown")
    End Function
    Public Shared ReadOnly Property HyperpinName() As String
        Get
            Dim ret As String = String.Empty
            If HyperpinXMLFile <> "Unknown" Then
                If Not String.IsNullOrEmpty(HyperpinXMLFile) AndAlso IO.File.Exists(HyperpinXMLFile) Then
                    Dim Xml As Xml.XmlDocument = New Xml.XmlDocument
                    Xml.Load(HyperpinXMLFile)
                    If Xml.SelectNodes("menu") IsNot Nothing Then
                        For Each node As Xml.XmlNode In Xml.SelectNodes("menu")
                            For Each gamenode As Xml.XmlNode In node.SelectNodes("game")
                                If gamenode.Attributes("name") IsNot Nothing Then
                                    Dim name As String = gamenode.Attributes("name").InnerText
                                    If name.Equals(B2SData.TableFileName, StringComparison.CurrentCultureIgnoreCase) Then
                                        ret = gamenode.SelectSingleNode("description").InnerText
                                        Exit For
                                    End If
                                End If
                            Next
                            If Not String.IsNullOrEmpty(ret) Then Exit For
                        Next
                    End If
                End If
                If String.IsNullOrEmpty(ret) Then ret = B2SData.TableFileName
            End If
            ' get out
            Return ret
        End Get
    End Property

    Private Shared Function CheckDir(ByVal direc As String) As String
        Dim ret As String = String.Empty
        If File.Exists(Path.Combine(direc, "hyperpin.exe")) OrElse File.Exists(Path.Combine(direc, "pinballx.exe")) Then
            If File.Exists(Path.Combine(direc, "Databases", "Visual Pinball", "Visual Pinball.xml")) Then
                ret = Path.Combine(direc, "Databases", "Visual Pinball", "Visual Pinball.xml")
            End If
        End If
        If String.IsNullOrEmpty(ret) AndAlso Directory.Exists(direc) Then
            For Each d As String In Directory.GetDirectories(direc)
                Try
                    ret = CheckDir(d)
                Catch
                End Try
                If Not String.IsNullOrEmpty(ret) Then Exit For
            Next
        End If
        Return ret
    End Function

End Class
