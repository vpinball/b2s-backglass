Imports System
Imports System.Drawing

Public Class B2SData

    Private Declare Function GetShortPathName Lib "kernel32" Alias "GetShortPathNameA" (ByVal LongName As String, ShortName As String, ByVal bufsize As Integer) As Long

    Public Enum eDMDType
        NotDefined = 0
        NoB2SDMD = 1
        B2SAlwaysOnSecondMonitor = 2
        B2SAlwaysOnThirdMonitor = 3
        B2SOnSecondOrThirdMonitor = 4
    End Enum
    Public Enum eDualMode
        Both = 0
        Authentic = 1
        Fantasy = 2
    End Enum

    Public Shared Property OnAndOffImage() As Boolean = False
    Public Shared Property IsOffImageVisible() As Boolean = False

    Public Shared ReadOnly Property UsedRomLampIDs() As Generic.SortedList(Of Integer, B2SBaseBox())
        Get
            Return If(B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy, UsedRomLampIDs4Fantasy, UsedRomLampIDs4Authentic)
        End Get
    End Property
    Public Shared ReadOnly Property UsedRomSolenoidIDs() As Generic.SortedList(Of Integer, B2SBaseBox())
        Get
            Return If(B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy, UsedRomSolenoidIDs4Fantasy, UsedRomSolenoidIDs4Authentic)
        End Get
    End Property
    Public Shared ReadOnly Property UsedRomGIStringIDs() As Generic.SortedList(Of Integer, B2SBaseBox())
        Get
            Return If(B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy, UsedRomGIStringIDs4Fantasy, UsedRomGIStringIDs4Authentic)
        End Get
    End Property
    Public Shared ReadOnly Property UsedRomMechIDs() As Generic.SortedList(Of Integer, B2SBaseBox())
        Get
            Return If(B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy, UsedRomMechIDs4Fantasy, UsedRomMechIDs4Authentic)
        End Get
    End Property
    Public Shared Property UsedRomLampIDs4Authentic() As Generic.SortedList(Of Integer, B2SBaseBox()) = New Generic.SortedList(Of Integer, B2SBaseBox())
    Public Shared Property UsedRomSolenoidIDs4Authentic() As Generic.SortedList(Of Integer, B2SBaseBox()) = New Generic.SortedList(Of Integer, B2SBaseBox())
    Public Shared Property UsedRomGIStringIDs4Authentic() As Generic.SortedList(Of Integer, B2SBaseBox()) = New Generic.SortedList(Of Integer, B2SBaseBox())
    Public Shared Property UsedRomMechIDs4Authentic() As Generic.SortedList(Of Integer, B2SBaseBox()) = New Generic.SortedList(Of Integer, B2SBaseBox())
    Public Shared Property UsedRomLampIDs4Fantasy() As Generic.SortedList(Of Integer, B2SBaseBox()) = New Generic.SortedList(Of Integer, B2SBaseBox())
    Public Shared Property UsedRomSolenoidIDs4Fantasy() As Generic.SortedList(Of Integer, B2SBaseBox()) = New Generic.SortedList(Of Integer, B2SBaseBox())
    Public Shared Property UsedRomGIStringIDs4Fantasy() As Generic.SortedList(Of Integer, B2SBaseBox()) = New Generic.SortedList(Of Integer, B2SBaseBox())
    Public Shared Property UsedRomMechIDs4Fantasy() As Generic.SortedList(Of Integer, B2SBaseBox()) = New Generic.SortedList(Of Integer, B2SBaseBox())

    Public Shared ReadOnly Property UsedTopRomIDType() As B2SBaseBox.eRomIDType
        Get
            Return If(B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy, UsedTopRomIDType4Fantasy, UsedTopRomIDType4Authentic)
        End Get
    End Property
    Public Shared ReadOnly Property UsedSecondRomIDType() As B2SBaseBox.eRomIDType
        Get
            Return If(B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy, UsedSecondRomIDType4Fantasy, UsedSecondRomIDType4Authentic)
        End Get
    End Property
    Public Shared Property UsedTopRomIDType4Authentic() As B2SBaseBox.eRomIDType = B2SBaseBox.eRomIDType.NotDefined
    Public Shared Property UsedSecondRomIDType4Authentic() As B2SBaseBox.eRomIDType = B2SBaseBox.eRomIDType.NotDefined
    Public Shared Property UsedTopRomIDType4Fantasy() As B2SBaseBox.eRomIDType = B2SBaseBox.eRomIDType.NotDefined
    Public Shared Property UsedSecondRomIDType4Fantasy() As B2SBaseBox.eRomIDType = B2SBaseBox.eRomIDType.NotDefined

    Public Shared Property UsedRomReelLampIDs() As Generic.SortedList(Of Integer, B2SReelBox()) = New Generic.SortedList(Of Integer, B2SReelBox())

    Public Shared Property UsedAnimationLampIDs() As AnimationCollection = New AnimationCollection()
    Public Shared Property UsedRandomAnimationLampIDs() As AnimationCollection = New AnimationCollection()
    Public Shared Property UsedAnimationSolenoidIDs() As AnimationCollection = New AnimationCollection()
    Public Shared Property UsedRandomAnimationSolenoidIDs() As AnimationCollection = New AnimationCollection()
    Public Shared Property UsedAnimationGIStringIDs() As AnimationCollection = New AnimationCollection()
    Public Shared Property UsedRandomAnimationGIStringIDs() As AnimationCollection = New AnimationCollection()

    Public Shared Property TableName() As String = String.Empty
    Public Shared Property TableFileName() As String = String.Empty
    Public Shared Property BackglassFileName() As String = String.Empty
    Public Shared Property TableType() As Integer = 0
    Public Shared Property DMDType() As Integer = 0
    Public Shared Property GrillHeight() As Integer = 0
    Public Shared Property SmallGrillHeight() As Integer = 0
    Public Shared Property DMDDefaultLocation() As Point = New Point(0, 0)
    Public Shared Property DualBackglass() As Boolean = False

    Public Class PictureBoxCollection
        Inherits Generic.SortedList(Of String, B2SPictureBox)

        Public Sub New()
            MyBase.New(StringComparer.OrdinalIgnoreCase)
        End Sub

        Public Shadows Sub Add(ByVal value As B2SPictureBox, Optional ByVal dualmode As B2SData.eDualMode = eDualMode.Both)
            If Not MyBase.ContainsKey(value.Name) Then MyBase.Add(value.Name, value)
            If value.RomID > 0 Then
                Dim UsedRomIDs4Authentic As Generic.SortedList(Of Integer, B2SBaseBox()) = Nothing
                Dim UsedRomIDs4Fantasy As Generic.SortedList(Of Integer, B2SBaseBox()) = Nothing
                If value.RomIDType = B2SBaseBox.eRomIDType.Lamp Then
                    UsedRomIDs4Authentic = UsedRomLampIDs4Authentic
                    UsedRomIDs4Fantasy = UsedRomLampIDs4Fantasy
                ElseIf value.RomIDType = B2SBaseBox.eRomIDType.Solenoid Then
                    UsedRomIDs4Authentic = UsedRomSolenoidIDs4Authentic
                    UsedRomIDs4Fantasy = UsedRomSolenoidIDs4Fantasy
                ElseIf value.RomIDType = B2SBaseBox.eRomIDType.GIString Then
                    UsedRomIDs4Authentic = UsedRomGIStringIDs4Authentic
                    UsedRomIDs4Fantasy = UsedRomGIStringIDs4Fantasy
                ElseIf value.RomIDType = B2SBaseBox.eRomIDType.Mech Then
                    UsedRomIDs4Authentic = UsedRomMechIDs4Authentic
                    UsedRomIDs4Fantasy = UsedRomMechIDs4Fantasy
                End If
                If UsedRomIDs4Authentic IsNot Nothing AndAlso (dualmode = eDualMode.Both OrElse dualmode = eDualMode.Authentic) Then
                    If UsedRomIDs4Authentic.ContainsKey(value.RomID) Then
                        Dim baseboxes As B2SBaseBox() = UsedRomIDs4Authentic(value.RomID)
                        ReDim Preserve baseboxes(baseboxes.Length)
                        baseboxes(baseboxes.Length - 1) = value
                        UsedRomIDs4Authentic(value.RomID) = baseboxes
                    Else
                        Dim baseboxes As B2SBaseBox()
                        ReDim baseboxes(0)
                        baseboxes(0) = value
                        UsedRomIDs4Authentic.Add(value.RomID, baseboxes)
                    End If
                End If
                If UsedRomIDs4Fantasy IsNot Nothing AndAlso (dualmode = eDualMode.Both OrElse dualmode = eDualMode.Fantasy) Then
                    If UsedRomIDs4Fantasy.ContainsKey(value.RomID) Then
                        Dim baseboxes As B2SBaseBox() = UsedRomIDs4Fantasy(value.RomID)
                        ReDim Preserve baseboxes(baseboxes.Length)
                        baseboxes(baseboxes.Length - 1) = value
                        UsedRomIDs4Fantasy(value.RomID) = baseboxes
                    Else
                        Dim baseboxes As B2SBaseBox()
                        ReDim baseboxes(0)
                        baseboxes(0) = value
                        UsedRomIDs4Fantasy.Add(value.RomID, baseboxes)
                    End If
                End If
            End If
        End Sub
    End Class
    Public Class ReelBoxCollection
        Inherits Generic.Dictionary(Of String, B2SReelBox)

        Public Sub New()
            MyBase.New(StringComparer.OrdinalIgnoreCase)
        End Sub

        Public Shadows Sub Add(value As B2SReelBox)
            If Not MyBase.ContainsKey(value.Name) Then MyBase.Add(value.Name, value)
            If value.RomID > 0 Then
                If UsedRomReelLampIDs.ContainsKey(value.RomID) Then
                    Dim reelboxes As B2SReelBox() = UsedRomReelLampIDs(value.RomID)
                    ReDim Preserve reelboxes(reelboxes.Length)
                    reelboxes(reelboxes.Length - 1) = value
                    UsedRomReelLampIDs(value.RomID) = reelboxes
                Else
                    Dim reelboxes As B2SReelBox()
                    ReDim reelboxes(0)
                    reelboxes(0) = value
                    UsedRomReelLampIDs.Add(value.RomID, reelboxes)
                End If
            End If
        End Sub
    End Class
    Public Class ZOrderCollection
        Inherits Generic.SortedList(Of Integer, B2SPictureBox())

        Public Shadows Sub Add(value As B2SPictureBox)
            If value.ZOrder > 0 Then
                If MyBase.ContainsKey(value.ZOrder) Then
                    Dim pictureboxes As B2SPictureBox() = MyBase.Item(value.ZOrder)
                    ReDim Preserve pictureboxes(pictureboxes.Length)
                    pictureboxes(pictureboxes.Length - 1) = value
                    MyBase.Item(value.ZOrder) = pictureboxes
                Else
                    Dim pictureboxes As B2SPictureBox()
                    ReDim pictureboxes(0)
                    pictureboxes(0) = value
                    MyBase.Add(value.ZOrder, pictureboxes)
                End If
            End If
        End Sub
    End Class

    Public Class AnimationInfo
        Public AnimationName As String = String.Empty
        Public Inverted As Boolean = False

        Public Sub New(ByVal _name As String, ByVal _inverted As Boolean)
            AnimationName = _name
            Inverted = _inverted
        End Sub
    End Class
    Public Class AnimationCollection
        Inherits Generic.Dictionary(Of Integer, AnimationInfo())

        Public Shadows Sub Add(key As Integer, value As AnimationInfo)
            If Not Me.ContainsKey(key) Then
                MyBase.Add(key, New AnimationInfo() {value})
            Else
                Dim infos As AnimationInfo() = Me(key)
                ReDim Preserve infos(infos.Length)
                infos(infos.Length - 1) = value
                Me(key) = infos
            End If
        End Sub
    End Class

    Public Class IlluminationGroupCollection
        Inherits Generic.Dictionary(Of String, B2SPictureBox())

        Public Sub New()
            MyBase.New(StringComparer.OrdinalIgnoreCase)
        End Sub

        Public Shadows Sub Add(ByVal value As B2SPictureBox)
            If Not String.IsNullOrEmpty(value.GroupName) Then
                If Not Me.ContainsKey(value.GroupName) Then
                    MyBase.Add(value.GroupName, New B2SPictureBox() {value})
                Else
                    Dim picboxes As B2SPictureBox() = Me(value.GroupName)
                    ReDim Preserve picboxes(picboxes.Length)
                    picboxes(picboxes.Length - 1) = value
                    Me(value.GroupName) = picboxes
                End If
            End If
        End Sub
    End Class

    Public Shared ReadOnly Property UseRomLamps() As Boolean
        Get
            If B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy Then
                Return (UsedRomLampIDs4Fantasy.Count > 0 OrElse UsedTopRomIDType4Fantasy = B2SBaseBox.eRomIDType.Lamp OrElse UsedSecondRomIDType4Fantasy = B2SBaseBox.eRomIDType.Lamp)
            Else
                Return (UsedRomLampIDs4Authentic.Count > 0 OrElse UsedTopRomIDType4Authentic = B2SBaseBox.eRomIDType.Lamp OrElse UsedSecondRomIDType4Authentic = B2SBaseBox.eRomIDType.Lamp)
            End If
        End Get
    End Property
    Public Shared ReadOnly Property UseRomSolenoids() As Boolean
        Get
            If B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy Then
                Return (UsedRomSolenoidIDs4Fantasy.Count > 0 OrElse UsedTopRomIDType4Fantasy = B2SBaseBox.eRomIDType.Solenoid OrElse UsedSecondRomIDType4Fantasy = B2SBaseBox.eRomIDType.Solenoid)
            Else
                Return (UsedRomSolenoidIDs4Authentic.Count > 0 OrElse UsedTopRomIDType4Authentic = B2SBaseBox.eRomIDType.Solenoid OrElse UsedSecondRomIDType4Authentic = B2SBaseBox.eRomIDType.Solenoid)
            End If
        End Get
    End Property
    Public Shared ReadOnly Property UseRomGIStrings() As Boolean
        Get
            If B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy Then
                Return (UsedRomGIStringIDs4Fantasy.Count > 0 OrElse UsedTopRomIDType4Fantasy = B2SBaseBox.eRomIDType.GIString OrElse UsedSecondRomIDType4Fantasy = B2SBaseBox.eRomIDType.GIString)
            Else
                Return (UsedRomGIStringIDs4Authentic.Count > 0 OrElse UsedTopRomIDType4Authentic = B2SBaseBox.eRomIDType.GIString OrElse UsedSecondRomIDType4Authentic = B2SBaseBox.eRomIDType.GIString)
            End If
        End Get
    End Property
    Public Shared ReadOnly Property UseRomMechs() As Boolean
        Get
            If B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy Then
                Return (UsedRomMechIDs4Fantasy.Count > 0)
            Else
                Return (UsedRomMechIDs4Authentic.Count > 0)
            End If
        End Get
    End Property

    Public Shared ReadOnly Property UseAnimationLamps() As Boolean
        Get
            Return (UsedAnimationLampIDs.Count > 0 OrElse UsedRandomAnimationLampIDs.Count > 0)
        End Get
    End Property
    Public Shared ReadOnly Property UseAnimationSolenoids() As Boolean
        Get
            Return (UsedAnimationSolenoidIDs.Count > 0 OrElse UsedRandomAnimationSolenoidIDs.Count > 0)
        End Get
    End Property
    Public Shared ReadOnly Property UseAnimationGIStrings() As Boolean
        Get
            Return (UsedAnimationGIStringIDs.Count > 0 OrElse UsedRandomAnimationGIStringIDs.Count > 0)
        End Get
    End Property

    Public Shared ReadOnly Property UseRomReelLamps() As Boolean
        Get
            Return (UsedRomReelLampIDs.Count > 0)
        End Get
    End Property

    Public Shared ReadOnly Property UseLEDs() As Boolean
        Get
            Return (LEDs.Count > 0)
        End Get
    End Property
    Public Shared ReadOnly Property UseLEDDisplays() As Boolean
        Get
            Return (LEDDisplays.Count > 0)
        End Get
    End Property

    Public Shared ReadOnly Property UseReels() As Boolean
        Get
            Return (Reels.Count > 0)
        End Get
    End Property

    Private Shared _ScoreMaxDigit As Integer = 0
    Public Shared Property ScoreMaxDigit() As Integer
        Get
            Return _ScoreMaxDigit
        End Get
        Set(value As Integer)
            If _ScoreMaxDigit < value Then
                _ScoreMaxDigit = value
            End If
        End Set
    End Property

    Public Shared Property Players() As B2SPlayer = New B2SPlayer()
    Public Shared Property IsAPlayerAdded() As Boolean = False

    Public Shared Property Reels() As ReelBoxCollection = New ReelBoxCollection
    Public Shared Property ReelDisplays() As Generic.Dictionary(Of Integer, B2SReelDisplay) = New Generic.Dictionary(Of Integer, B2SReelDisplay)
    Public Shared Property ReelImages() As Generic.Dictionary(Of String, Image) = New Generic.Dictionary(Of String, Image)(StringComparer.OrdinalIgnoreCase)
    Public Shared Property ReelIntermediateImages() As Generic.Dictionary(Of String, Image) = New Generic.Dictionary(Of String, Image)(StringComparer.OrdinalIgnoreCase)
    Public Shared Property ReelIlluImages() As Generic.Dictionary(Of String, Image) = New Generic.Dictionary(Of String, Image)(StringComparer.OrdinalIgnoreCase)
    Public Shared Property ReelIntermediateIlluImages() As Generic.Dictionary(Of String, Image) = New Generic.Dictionary(Of String, Image)(StringComparer.OrdinalIgnoreCase)

    Public Shared Property Sounds() As Generic.Dictionary(Of String, Byte()) = New Generic.Dictionary(Of String, Byte())(StringComparer.OrdinalIgnoreCase)

    Public Shared Property LEDs() As Generic.Dictionary(Of String, B2SLEDBox) = New Generic.Dictionary(Of String, B2SLEDBox)(StringComparer.OrdinalIgnoreCase)
    Public Shared Property LEDAreas() As Generic.Dictionary(Of String, LEDAreaInfo) = New Generic.Dictionary(Of String, LEDAreaInfo)(StringComparer.OrdinalIgnoreCase)
    Public Class LEDAreaInfo
        Public Rect As Rectangle = Nothing
        Public IsOnDMD As Boolean = False

        Public Sub New(ByVal _rect As Rectangle, ByVal _isOnDMD As Boolean)
            Rect = _rect
            IsOnDMD = _isOnDMD
        End Sub
    End Class

    Public Shared Property LEDDisplays() As Generic.Dictionary(Of String, Dream7Display) = New Generic.Dictionary(Of String, Dream7Display)(StringComparer.OrdinalIgnoreCase)
    Public Shared Property LEDDisplayDigits() As Generic.Dictionary(Of Integer, LEDDisplayDigitLocation) = New Generic.Dictionary(Of Integer, LEDDisplayDigitLocation)
    Public Class LEDDisplayDigitLocation
        Public LEDDisplay As Dream7Display = Nothing
        Public Digit As Integer = 0
        Public LEDDisplayID As Integer = 0

        Public Sub New(ByRef _leddisplay As Dream7Display, ByVal _digit As Integer, ByVal _ledDisplayID As Integer)
            LEDDisplay = _leddisplay
            Digit = _digit
            LEDDisplayID = _ledDisplayID
        End Sub
    End Class

    Public Shared Property Illuminations() As PictureBoxCollection = New PictureBoxCollection()
    Public Shared Property DMDIlluminations() As PictureBoxCollection = New PictureBoxCollection()

    Public Shared Property UseIlluminationLocks() As Boolean = False
    Public Shared Property IlluminationGroups() As IlluminationGroupCollection = New IlluminationGroupCollection()
    Public Shared Property IlluminationLocks() As Generic.Dictionary(Of String, Integer) = New Generic.Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

    Public Shared Property UseZOrder() As Boolean = False
    Public Shared Property UseDMDZOrder() As Boolean = False
    Public Shared Property ZOrderImages() As ZOrderCollection = New ZOrderCollection()
    Public Shared Property ZOrderDMDImages() As ZOrderCollection = New ZOrderCollection()

    Public Shared Property UseRotatingImage() As Boolean = False
    Public Shared Property UseMechRotatingImage() As Boolean = False
    Public Shared Property RotatingImages() As Generic.Dictionary(Of Integer, Generic.Dictionary(Of Integer, Image)) = New Generic.Dictionary(Of Integer, Generic.Dictionary(Of Integer, Image))
    Public Shared Property RotatingPictureBox() As Generic.Dictionary(Of Integer, B2SPictureBox) = New Generic.Dictionary(Of Integer, B2SPictureBox)

    Public Shared Property led8Seg() As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    Public Shared Property led10Seg() As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    Public Shared Property led14Seg() As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    Public Shared Property led16Seg() As Generic.List(Of PointF()) = New Generic.List(Of PointF())
    Public Shared Property ledCoordMax() As Integer

    Public Shared Sub ClearAll(Optional ByVal donotclearnames As Boolean = False)
        If Not donotclearnames Then
            TableName = String.Empty
            TableFileName = String.Empty
            BackglassFileName = String.Empty
        End If
        TableType = 0
        DMDType = 0
        GrillHeight = 0
        SmallGrillHeight = 0
        DMDDefaultLocation = New Point(0, 0)
        DualBackglass = False
        UsedTopRomIDType4Authentic = B2SBaseBox.eRomIDType.NotDefined
        UsedTopRomIDType4Fantasy = B2SBaseBox.eRomIDType.NotDefined
        UsedSecondRomIDType4Authentic = B2SBaseBox.eRomIDType.NotDefined
        UsedSecondRomIDType4Fantasy = B2SBaseBox.eRomIDType.NotDefined
        UsedRomLampIDs4Authentic.Clear()
        UsedRomSolenoidIDs4Authentic.Clear()
        UsedRomGIStringIDs4Authentic.Clear()
        UsedRomMechIDs4Authentic.Clear()
        UsedRomLampIDs4Fantasy.Clear()
        UsedRomSolenoidIDs4Fantasy.Clear()
        UsedRomGIStringIDs4Fantasy.Clear()
        UsedRomMechIDs4Fantasy.Clear()
        UsedRomReelLampIDs.Clear()
        UsedAnimationLampIDs.Clear()
        UsedRandomAnimationLampIDs.Clear()
        UsedAnimationSolenoidIDs.Clear()
        UsedRandomAnimationSolenoidIDs.Clear()
        UsedAnimationGIStringIDs.Clear()
        UsedRandomAnimationGIStringIDs.Clear()
        IsAPlayerAdded = False
        Players.Clear()
        For Each r In Reels : r.Value.Dispose() : Next
        Reels.Clear()
        For Each rd In ReelDisplays : rd.Value.Dispose() : Next
        ReelDisplays.Clear()
        ReelImages.Clear()
        ReelIntermediateImages.Clear()
        ReelIlluImages.Clear()
        ReelIntermediateIlluImages.Clear()
        'For Each s In Sounds : s.Value.Dispose() : Next
        Sounds.Clear()
        LEDs.Clear()
        LEDAreas.Clear()
        LEDDisplays.Clear()
        LEDDisplayDigits.Clear()
        Illuminations.Clear()
        DMDIlluminations.Clear()
        UseIlluminationLocks = False
        IlluminationGroups.Clear()
        IlluminationLocks.Clear()
        UseZOrder = False
        UseDMDZOrder = False
        ZOrderImages.Clear()
        ZOrderDMDImages.Clear()
        UseRotatingImage = False
        UseMechRotatingImage = False
        For Each r As KeyValuePair(Of Integer, Generic.Dictionary(Of Integer, Image)) In RotatingImages : r.Value.Clear() : Next
        RotatingImages.Clear()
        RotatingPictureBox.Clear()
    End Sub

    Shared Sub New()

        ' set coordinates maximum
        ledCoordMax = 103

        ' add led segments
        Const toleft As Integer = 8
        ' 8 segments
        led8Seg.Add({New PointF(22, 5), New PointF(26, 2), New PointF(88, 2), New PointF(92, 5), New PointF(85, 11), New PointF(29, 11)})
        led8Seg.Add({New PointF(93, 7), New PointF(96, 10), New PointF(96 - toleft, 46), New PointF(93 - toleft, 49), New PointF(87 - toleft, 43), New PointF(87, 12)})
        led8Seg.Add({New PointF(92 - toleft, 51), New PointF(95 - toleft, 54), New PointF(96 - 2 * toleft, 90), New PointF(93 - 2 * toleft, 93), New PointF(87 - 2 * toleft, 88), New PointF(86 - toleft, 57)})
        led8Seg.Add({New PointF(22 - 2 * toleft, 95), New PointF(29 - 2 * toleft, 89), New PointF(85 - 2 * toleft, 89), New PointF(92 - 2 * toleft, 95), New PointF(88 - 2 * toleft, 98), New PointF(26 - 2 * toleft, 98)})
        led8Seg.Add({New PointF(20 - toleft, 51), New PointF(26 - toleft, 57), New PointF(27 - 2 * toleft, 88), New PointF(21 - 2 * toleft, 93), New PointF(17 - 2 * toleft, 90), New PointF(17 - toleft, 54)})
        led8Seg.Add({New PointF(21, 7), New PointF(27, 12), New PointF(27 - toleft, 43), New PointF(21 - toleft, 49), New PointF(18 - toleft, 46), New PointF(18, 10)})
        led8Seg.Add({New PointF(23 - toleft, 50), New PointF(27 - toleft, 46), New PointF(86 - toleft, 46), New PointF(90 - toleft, 50), New PointF(86 - toleft, 54), New PointF(27 - toleft, 54)})
        ' 10 segments
        led10Seg.Add({New PointF(22, 5), New PointF(26, 2), New PointF(88, 2), New PointF(92, 5), New PointF(85, 11), New PointF(72, 11), New PointF(67, 6), New PointF(62, 11), New PointF(29, 11)})
        led10Seg.Add({New PointF(93, 7), New PointF(96, 10), New PointF(96 - toleft, 46), New PointF(93 - toleft, 49), New PointF(87 - toleft, 43), New PointF(87, 12)})
        led10Seg.Add({New PointF(92 - toleft, 51), New PointF(95 - toleft, 54), New PointF(96 - 2 * toleft, 90), New PointF(93 - 2 * toleft, 93), New PointF(87 - 2 * toleft, 88), New PointF(86 - toleft, 57)})
        led10Seg.Add({New PointF(22 - 2 * toleft, 95), New PointF(29 - 2 * toleft, 89), New PointF(61 - 2 * toleft, 89), New PointF(66 - 2 * toleft, 94), New PointF(71 - 2 * toleft, 89), New PointF(85 - 2 * toleft, 89), New PointF(92 - 2 * toleft, 95), New PointF(88 - 2 * toleft, 98), New PointF(26 - 2 * toleft, 98)})
        led10Seg.Add({New PointF(20 - toleft, 51), New PointF(26 - toleft, 57), New PointF(27 - 2 * toleft, 88), New PointF(21 - 2 * toleft, 93), New PointF(17 - 2 * toleft, 90), New PointF(17 - toleft, 54)})
        led10Seg.Add({New PointF(21, 7), New PointF(27, 12), New PointF(27 - toleft, 43), New PointF(21 - toleft, 49), New PointF(18 - toleft, 46), New PointF(18, 10)})
        led10Seg.Add({New PointF(23 - toleft, 50), New PointF(27 - toleft, 46), New PointF(63 - toleft, 46), New PointF(68 - toleft, 51), New PointF(73 - toleft, 46), New PointF(86 - toleft, 46), New PointF(90 - toleft, 50), New PointF(86 - toleft, 54), New PointF(72 - toleft, 54), New PointF(67 - toleft, 49), New PointF(62 - toleft, 54), New PointF(27 - toleft, 54)})
        led10Seg.Add({New PointF(67, 9), New PointF(71, 13), New PointF(71 - toleft, 45), New PointF(67 - toleft, 49), New PointF(63 - toleft, 45), New PointF(63, 13)})
        led10Seg.Add({New PointF(68, 7), New PointF(72, 11), New PointF(72 - toleft, 43), New PointF(68 - toleft, 47), New PointF(64 - toleft, 43), New PointF(64, 7)})
        led10Seg.Add({New PointF(66 - toleft, 51), New PointF(70 - toleft, 55), New PointF(70 - 2 * toleft, 88), New PointF(66 - 2 * toleft, 92), New PointF(62 - 2 * toleft, 88), New PointF(62 - toleft, 51)})
        ' 14 segments
        led14Seg.Add({New PointF(22, 5), New PointF(26, 2), New PointF(88, 2), New PointF(92, 5), New PointF(85, 11), New PointF(29, 11)})
        led14Seg.Add({New PointF(93, 7), New PointF(96, 10), New PointF(96 - toleft, 46), New PointF(93 - toleft, 49), New PointF(87 - toleft, 43), New PointF(87, 12)})
        led14Seg.Add({New PointF(92 - toleft, 51), New PointF(95 - toleft, 54), New PointF(96 - 2 * toleft, 90), New PointF(93 - 2 * toleft, 93), New PointF(87 - 2 * toleft, 88), New PointF(86 - toleft, 57)})
        led14Seg.Add({New PointF(22 - 2 * toleft, 95), New PointF(29 - 2 * toleft, 89), New PointF(85 - 2 * toleft, 89), New PointF(92 - 2 * toleft, 95), New PointF(88 - 2 * toleft, 98), New PointF(26 - 2 * toleft, 98)})
        led14Seg.Add({New PointF(20 - toleft, 51), New PointF(26 - toleft, 57), New PointF(27 - 2 * toleft, 88), New PointF(21 - 2 * toleft, 93), New PointF(17 - 2 * toleft, 90), New PointF(17 - toleft, 54)})
        led14Seg.Add({New PointF(21, 7), New PointF(27, 12), New PointF(27 - toleft, 43), New PointF(21 - toleft, 49), New PointF(18 - toleft, 46), New PointF(18, 10)})
        led14Seg.Add({New PointF(23 - toleft, 50), New PointF(27 - toleft, 46), New PointF(52 - toleft, 46), New PointF(55 - toleft, 50), New PointF(52 - toleft, 54), New PointF(27 - toleft, 54)})
        led14Seg.Add({New PointF(104 - 2 * toleft, 87), New PointF(109 - 2 * toleft, 90), New PointF(109 - 2 * toleft, 95), New PointF(104 - 2 * toleft, 99), New PointF(100 - 2 * toleft, 95), New PointF(100 - 2 * toleft, 90)})
        led14Seg.Add({New PointF(30, 13), New PointF(34, 17), New PointF(54 - toleft, 38), New PointF(51 - toleft, 43), New PointF(48 - toleft, 40), New PointF(27, 16)})
        led14Seg.Add({New PointF(57, 13), New PointF(61, 13), New PointF(61 - toleft, 46), New PointF(57 - toleft, 48), New PointF(53 - toleft, 46), New PointF(53, 13)})
        led14Seg.Add({New PointF(82, 13), New PointF(85, 16), New PointF(68 - toleft, 42), New PointF(65 - toleft, 44), New PointF(63 - toleft, 39), New PointF(77, 17)})
        led14Seg.Add({New PointF(58 - toleft, 50), New PointF(62 - toleft, 46), New PointF(86 - toleft, 46), New PointF(90 - toleft, 50), New PointF(86 - toleft, 54), New PointF(62 - toleft, 54)})
        led14Seg.Add({New PointF(82 - 2 * toleft, 85), New PointF(87 - 2 * toleft, 86), New PointF(67 - toleft, 57), New PointF(62 - toleft, 57), New PointF(62 - toleft, 60), New PointF(79 - 2 * toleft, 86)})
        led14Seg.Add({New PointF(57 - toleft, 52), New PointF(61 - toleft, 54), New PointF(61 - 2 * toleft, 88), New PointF(57 - 2 * toleft, 88), New PointF(53 - 2 * toleft, 88), New PointF(53 - toleft, 54)})
        led14Seg.Add({New PointF(30 - 2 * toleft, 83), New PointF(33 - 2 * toleft, 86), New PointF(50 - toleft, 60), New PointF(47 - toleft, 57), New PointF(42 - toleft, 61), New PointF(27 - 2 * toleft, 86)})
        led14Seg.Add({New PointF(102 - 2 * toleft, 97), New PointF(107 - 2 * toleft, 100), New PointF(107 - 2 * toleft, 105), New PointF(102 - 2 * toleft, 109), New PointF(98 - 2 * toleft, 105), New PointF(98 - 2 * toleft, 100)})
        ' 16 segments
        led16Seg.Add({New PointF(22, 5), New PointF(26, 2), New PointF(88, 2), New PointF(92, 5), New PointF(85, 11), New PointF(29, 11)})
        'led16Seg.Add({New PointF(93, 7), New PointF(96, 10), New PointF(96 - toleft, 46), New PointF(93 - toleft, 49), New PointF(87 - toleft, 43), New PointF(87, 12)})
        'led16Seg.Add({New PointF(92 - toleft, 51), New PointF(95 - toleft, 54), New PointF(96 - 2 * toleft, 90), New PointF(93 - 2 * toleft, 93), New PointF(87 - 2 * toleft, 88), New PointF(86 - toleft, 57)})
        'led16Seg.Add({New PointF(22 - 2 * toleft, 95), New PointF(29 - 2 * toleft, 89), New PointF(85 - 2 * toleft, 89), New PointF(92 - 2 * toleft, 95), New PointF(88 - 2 * toleft, 98), New PointF(26 - 2 * toleft, 98)})
        'led16Seg.Add({New PointF(20 - toleft, 51), New PointF(26 - toleft, 57), New PointF(27 - 2 * toleft, 88), New PointF(21 - 2 * toleft, 93), New PointF(17 - 2 * toleft, 90), New PointF(17 - toleft, 54)})
        'led16Seg.Add({New PointF(21, 7), New PointF(27, 12), New PointF(27 - toleft, 43), New PointF(21 - toleft, 49), New PointF(18 - toleft, 46), New PointF(18, 10)})
        'led16Seg.Add({New PointF(23 - toleft, 50), New PointF(27 - toleft, 46), New PointF(86 - toleft, 46), New PointF(90 - toleft, 50), New PointF(86 - toleft, 54), New PointF(27 - toleft, 54)})

    End Sub

    Public Shared Function ShortFileName(ByVal longFileName As String) As String

        On Error Resume Next
        Dim ret As String = Space(255)
        GetShortPathName(longFileName, ret, Len(ret))
        If Not String.IsNullOrEmpty(ret) AndAlso InStr(ret, Chr(0)) > 0 Then
            Return ret.Substring(0, InStr(ret, Chr(0)) - 1)
        Else
            Return String.Empty
        End If

    End Function

End Class
