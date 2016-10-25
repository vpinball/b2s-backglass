Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class B2SAnimation

    Private Shared runningAnimations As RunningAnimationsCollection = New RunningAnimationsCollection()
    Private Class RunningAnimationsCollection

        Inherits Generic.List(Of String)

        Public Shadows Sub Add(item As String)
            If Not Me.Contains(item) Then
                MyBase.Add(item)
            End If
        End Sub
        Public Shadows Function Remove(item As String) As Boolean
            If Me.Contains(item) Then
                Return MyBase.Remove(item)
            End If
            Return True
        End Function

    End Class

    Public Enum eType
        Undefined = 0
        ImageCollectionAtForm = 1
        ImageCollectionAtPictureBox = 2
        PictureBoxCollection = 3
    End Enum
    Public Enum eLightsStateAtAnimationStart
        Undefined = 0
        InvolvedLightsOff = 1
        InvolvedLightsOn = 2
        LightsOff = 3
        NoChange = 4
    End Enum
    Public Enum eLightsStateAtAnimationEnd
        Undefined = 0
        InvolvedLightsOff = 1
        InvolvedLightsOn = 2
        LightsReseted = 3
        NoChange = 4
    End Enum
    Public Enum eAnimationStopBehaviour
        Undefined = 0
        StopImmediatelly = 1
        RunAnimationTillEnd = 2
        RunAnimationToFirstStep = 3
    End Enum

    Public MustInherit Class B2SAnimationBase

        Inherits Timers.Timer

        Friend Name As String = String.Empty
        Friend Type As eType = eType.Undefined
        Friend DualMode As B2SData.eDualMode = B2SData.eDualMode.Both
        Friend Loops As Integer = -1
        Friend PlayReverse As Boolean = False
        Friend StartMeAtVPActivate As Boolean = True
        Friend LightsStateAtAnimationStart As eLightsStateAtAnimationStart = eLightsStateAtAnimationStart.NoChange
        Friend LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.InvolvedLightsOff
        Friend AnimationStopBehaviour As eAnimationStopBehaviour = eAnimationStopBehaviour.StopImmediatelly
        Friend LockInvolvedLamps As Boolean = False
        Friend HideScoreDisplays As Boolean = False
        Friend BringToFront As Boolean = False
        Friend RandomStart As Boolean = False
        Friend RandomQuality As Integer = 1
        Friend BaseInterval As Integer = 0
        Friend SlowDown As Integer = 1

        Friend WouldBeStarted As Boolean = False
        Friend StopMeLater As Boolean = False

        Friend Sub New(ByVal _DualMode As B2SData.eDualMode,
                       ByVal _Interval As Integer,
                       ByVal _Type As eType,
                       ByVal _Loops As Integer,
                       ByVal _PlayReverse As Boolean,
                       ByVal _StartMeAtVPActivate As Boolean,
                       ByVal _LightsStateAtAnimationStart As eLightsStateAtAnimationStart,
                       ByVal _LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd,
                       ByVal _AnimationStopBehaviour As eAnimationStopBehaviour,
                       ByVal _LockInvolvedLamps As Boolean,
                       ByVal _HideScoreDisplays As Boolean,
                       ByVal _BringToFront As Boolean,
                       ByVal _RandomStart As Boolean,
                       ByVal _RandomQuality As Integer)
            Me.DualMode = _DualMode
            Me.Interval = _Interval
            Me.Loops = _Loops
            Me.PlayReverse = _PlayReverse
            Me.BaseInterval = _Interval
            Me.Type = _Type
            Me.StartMeAtVPActivate = _StartMeAtVPActivate
            Me.LightsStateAtAnimationStart = _LightsStateAtAnimationStart
            If Me.LightsStateAtAnimationStart = eLightsStateAtAnimationStart.Undefined Then Me.LightsStateAtAnimationStart = eLightsStateAtAnimationStart.NoChange
            Me.LightsStateAtAnimationEnd = _LightsStateAtAnimationEnd
            If Me.LightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.Undefined Then Me.LightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.InvolvedLightsOff
            Me.AnimationStopBehaviour = _AnimationStopBehaviour
            If Me.AnimationStopBehaviour = eAnimationStopBehaviour.Undefined Then Me.AnimationStopBehaviour = eAnimationStopBehaviour.StopImmediatelly
            Me.LockInvolvedLamps = _LockInvolvedLamps
            Me.HideScoreDisplays = _HideScoreDisplays
            Me.BringToFront = _BringToFront
            Me.RandomStart = _RandomStart
            Me.RandomQuality = _RandomQuality
            Me.Enabled = False
        End Sub

    End Class

    Public Class TimerAnimation

        Inherits B2SAnimationBase

        Public Event Finished()

        Private Form As Form = Nothing
        Private FormDMD As Form = Nothing
        Private PictureBox As PictureBox = Nothing
        Private ImageCollection As ImageList.ImageCollection = Nothing

        Public Sub New(ByVal _Form As Form,
                       ByVal _FormDMD As Form,
                       ByVal _Name As String,
                       ByVal _DualMode As B2SData.eDualMode,
                       ByVal _Interval As Integer,
                       ByVal _Loops As Integer,
                       ByVal _StartTimerAtVPActivate As Boolean,
                       ByVal _LightsStateAtAnimationStart As eLightsStateAtAnimationStart,
                       ByVal _LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd,
                       ByVal _AnimationStopBehaviour As eAnimationStopBehaviour,
                       ByVal _LockInvolvedLamps As Boolean,
                       ByVal _HideScoreDisplays As Boolean,
                       ByVal _BringToFront As Boolean,
                       ByVal _RandomStart As Boolean,
                       ByVal _RandomQuality As Integer,
                       ByVal _ImageCollection As ImageList.ImageCollection)
            MyBase.New(_DualMode, _Interval, eType.ImageCollectionAtForm, _Loops, False, _StartTimerAtVPActivate, _LightsStateAtAnimationStart, _LightsStateAtAnimationEnd, _AnimationStopBehaviour, _LockInvolvedLamps, _HideScoreDisplays, _BringToFront, _RandomStart, _RandomQuality)
            Me.SynchronizingObject = _Form
            Me.Form = _Form
            Me.FormDMD = _FormDMD
            Me.Name = _Name
            Me.ImageCollection = _ImageCollection
        End Sub
        Public Sub New(ByVal _Form As Form,
                       ByVal _Picturebox As PictureBox,
                       ByVal _Name As String,
                       ByVal _DualMode As B2SData.eDualMode,
                       ByVal _Interval As Integer,
                       ByVal _Loops As Integer,
                       ByVal _StartTimerAtVPActivate As Boolean,
                       ByVal _LightsStateAtAnimationStart As eLightsStateAtAnimationStart,
                       ByVal _LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd,
                       ByVal _AnimationStopBehaviour As eAnimationStopBehaviour,
                       ByVal _LockInvolvedLamps As Boolean,
                       ByVal _HideScoreDisplays As Boolean,
                       ByVal _BringToFront As Boolean,
                       ByVal _RandomStart As Boolean,
                       ByVal _RandomQuality As Integer,
                       ByVal _ImageCollection As ImageList.ImageCollection)
            MyBase.New(_DualMode, _Interval, eType.ImageCollectionAtPictureBox, _Loops, False, _StartTimerAtVPActivate, _LightsStateAtAnimationStart, _LightsStateAtAnimationEnd, _AnimationStopBehaviour, _LockInvolvedLamps, _HideScoreDisplays, _BringToFront, _RandomStart, _RandomQuality)
            Me.SynchronizingObject = _Form
            Me.PictureBox = _Picturebox
            Me.Name = _Name
            Me.ImageCollection = _ImageCollection
        End Sub

        Private loopticker As Integer = 0
        Private ticker As Integer = 0
        Private reachedThe0Point As Boolean = False

        Public Shadows Sub Start()

            ' set the start flag
            Me.WouldBeStarted = True
            Me.StopMeLater = False

            ' maybe get out here because animation is not allowed to start
            If B2SSettings.AllAnimationSlowDown = 0 OrElse Me.SlowDown = 0 Then
                Return
            End If

            ' maybe get out here because of not matching dual mode
            If B2SData.DualBackglass Then
                If Not (Me.DualMode = B2SData.eDualMode.Both OrElse Me.DualMode = B2SSettings.CurrentDualMode) Then
                    Return
                End If
            End If

            ' start the base timer
            MyBase.Start()
            runningAnimations.Add(Me.Name)

        End Sub
        Public Shadows Sub [Stop]()

            ' remove the start flag
            Me.WouldBeStarted = False

            ' maybe stop the animation a bit later to do the last animation steps
            If (Me.AnimationStopBehaviour = eAnimationStopBehaviour.RunAnimationTillEnd OrElse Me.AnimationStopBehaviour = eAnimationStopBehaviour.RunAnimationToFirstStep) Then
                If Not Me.StopMeLater Then
                    reachedThe0Point = False
                End If
                Me.StopMeLater = True
                Return
            End If

            ' stop the base timer
            runningAnimations.Remove(Me.Name)
            MyBase.Stop()
            Me.Enabled = False
            MyBase.Enabled = False

        End Sub

        Private Sub TimerAnimation_Tick(sender As Object, e As System.EventArgs) Handles Me.Elapsed

            ' first of all stop the timer
            MyBase.Stop()

            ' maybe get out since the timer is already stopped
            If Not runningAnimations.Contains(Me.Name) Then
                Me.Enabled = False
                MyBase.Enabled = False
                Return
            End If

            ' show image
            If Me.Type = eType.ImageCollectionAtForm Then
                Me.Form.BackgroundImage = Me.ImageCollection(ticker)
            ElseIf eType.ImageCollectionAtPictureBox Then
                Me.PictureBox.Image = Me.ImageCollection(ticker)
            End If

            ' count on and maybe restart the timer
            Dim restart As Boolean = True
            ticker += 1
            If ticker >= Me.ImageCollection.Count Then
                reachedThe0Point = True
                loopticker += 1
                ticker = 0
                If Me.Loops > 0 AndAlso loopticker >= Me.Loops Then
                    loopticker = 0
                    restart = False
                    RaiseEvent Finished()
                End If
            End If
            If restart Then
                If B2SSettings.AllAnimationSlowDown = 0 OrElse Me.SlowDown = 0 Then
                    Me.Stop()
                ElseIf Me.StopMeLater AndAlso Me.AnimationStopBehaviour = eAnimationStopBehaviour.RunAnimationTillEnd AndAlso ticker = 0 Then
                    Me.Stop()
                ElseIf Me.StopMeLater AndAlso Me.AnimationStopBehaviour = eAnimationStopBehaviour.RunAnimationToFirstStep AndAlso (ticker = 1 OrElse ticker = 2) AndAlso reachedThe0Point Then
                    Me.Stop()
                Else
                    MyBase.Start()
                End If
            Else
                Me.Stop()
            End If

        End Sub

    End Class

    Public Class PictureBoxAnimation

        Inherits B2SAnimationBase

        Public Event Finished()

        Private Class EntryAction
            Public Bulbs As String() = Nothing
            Public IntervalMultiplier As Integer = 0
            Public Visible As Boolean = True
            Public Corrector As Integer = 0
            Public PulseSwitch As Integer = 0

            Public Sub New(ByVal _Bulbs As String(), ByVal _IntervalMultiplier As Integer, ByVal _Visible As Boolean, ByVal _Corrector As Integer, ByVal _PulseSwitch As Integer)
                Bulbs = _Bulbs
                IntervalMultiplier = _IntervalMultiplier
                Visible = _Visible
                Corrector = _Corrector
                PulseSwitch = _PulseSwitch
            End Sub
        End Class

        Private Form As Form = Nothing
        Private FormDMD As Form = Nothing
        Private Entries As Generic.SortedList(Of Integer, EntryAction) = New Generic.SortedList(Of Integer, EntryAction)

        Private LightsInvolved As Generic.List(Of String) = New Generic.List(Of String)
        Private LightsStateAtStartup As Generic.Dictionary(Of String, Boolean) = New Generic.Dictionary(Of String, Boolean)
        Private MainFormBackgroundImage As Image = Nothing

        Private SelectedLEDType As B2SSettings.eLEDTypes = B2SSettings.eLEDTypes.Undefined

        Public Sub New(ByVal _Form As Form,
                       ByVal _FormDMD As Form,
                       ByVal _Name As String,
                       ByVal _DualMode As B2SData.eDualMode,
                       ByVal _Interval As Integer,
                       ByVal _Loops As Integer,
                       ByVal _StartTimerAtVPActivate As Boolean,
                       ByVal _LightsStateAtAnimationStart As eLightsStateAtAnimationStart,
                       ByVal _LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd,
                       ByVal _AnimationStopBehaviour As eAnimationStopBehaviour,
                       ByVal _LockInvolvedLamps As Boolean,
                       ByVal _HideScoreDisplays As Boolean,
                       ByVal _BringToFront As Boolean,
                       ByVal _RandomStart As Boolean,
                       ByVal _RandomQuality As Integer,
                       ByVal ParamArray _Entries As PictureBoxAnimationEntry())

            MyBase.New(_DualMode, _Interval, eType.PictureBoxCollection, _Loops, False, _StartTimerAtVPActivate, _LightsStateAtAnimationStart, _LightsStateAtAnimationEnd, _AnimationStopBehaviour, _LockInvolvedLamps, _HideScoreDisplays, _BringToFront, _RandomStart, _RandomQuality)
            Me.SynchronizingObject = _Form
            Me.Form = _Form
            Me.FormDMD = _FormDMD
            Me.Name = _Name

            ' collect all the entries in a nice collection
            For Each entry As PictureBoxAnimationEntry In _Entries
                Dim isOn1Valid As Boolean = (Not String.IsNullOrEmpty(entry.On1(0)) OrElse entry.WaitAfterOn1 > 0)
                Dim isOff1Valid As Boolean = (Not String.IsNullOrEmpty(entry.Off1(0)) OrElse entry.WaitAfterOff1 > 0)
                Dim isOn2Valid As Boolean = (Not String.IsNullOrEmpty(entry.On2(0)) OrElse entry.WaitAfterOn2 > 0)
                Dim isOff2Valid As Boolean = (Not String.IsNullOrEmpty(entry.Off2(0)) OrElse entry.WaitAfterOff2 > 0)
                Dim pulseswitch As Integer = entry.PulseSwitch
                If isOn1Valid Then
                    Entries.Add(Entries.Count + 1, New EntryAction(entry.On1, entry.WaitAfterOn1, True, If(isOff1Valid, 1, 0), pulseswitch))
                    pulseswitch = 0
                End If
                If isOff1Valid Then
                    Entries.Add(Entries.Count + 1, New EntryAction(entry.Off1, entry.WaitAfterOff1, False, If(isOn1Valid, -1, 0), pulseswitch))
                    pulseswitch = 0
                End If
                If isOn2Valid Then
                    Entries.Add(Entries.Count + 1, New EntryAction(entry.On2, entry.WaitAfterOn2, True, If(isOff2Valid, 1, 0), pulseswitch))
                    pulseswitch = 0
                End If
                If isOff2Valid Then
                    Entries.Add(Entries.Count + 1, New EntryAction(entry.Off2, entry.WaitAfterOff2, False, If(isOn2Valid, -1, 0), pulseswitch))
                    pulseswitch = 0
                End If
                If pulseswitch > 0 Then
                    Entries.Add(Entries.Count + 1, New EntryAction(New String() {""}, 0, True, 0, pulseswitch))
                    pulseswitch = 0
                End If
            Next

            ' pick up all involved lights
            LightsInvolved.Clear()
            For Each entry As KeyValuePair(Of Integer, EntryAction) In Entries
                For Each bulb As String In entry.Value.Bulbs
                    If Not String.IsNullOrEmpty(bulb) AndAlso Not LightsInvolved.Contains(bulb) Then
                        LightsInvolved.Add(bulb)
                    End If
                Next
            Next

        End Sub

        Private loopticker As Integer = 0
        Private ticker As Integer = 0
        Private reachedThe0Point As Boolean = False

        Public Shadows Sub Start()

            ' set the start flag
            Me.WouldBeStarted = True
            Me.StopMeLater = False

            ' maybe get out here because animation is not allowed to start
            If B2SSettings.AllAnimationSlowDown = 0 OrElse Me.SlowDown = 0 Then
                Return
            End If

            ' maybe get out here because of not matching dual mode
            If B2SData.DualBackglass Then
                If Not (Me.DualMode = B2SData.eDualMode.Both OrElse Me.DualMode = B2SSettings.CurrentDualMode) Then
                    Return
                End If
            End If

            ' maybe switch off all lights but pick them up
            LightsStateAtStartup.Clear()
            For i As Integer = 1 To 2
                Dim currentForm As Form = If(i = 2, Me.FormDMD, Me.Form)
                If currentForm IsNot Nothing Then
                    For Each picbox As B2SPictureBox In currentForm.Controls.OfType(Of B2SPictureBox)()
                        If Me.LightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.LightsReseted AndAlso Not LightsStateAtStartup.ContainsKey(picbox.Name) Then
                            LightsStateAtStartup.Add(picbox.Name, picbox.Visible)
                        End If
                        If Me.LightsStateAtAnimationStart = eLightsStateAtAnimationStart.LightsOff Then
                            picbox.Visible = False
                        End If
                    Next
                    If Me.LightsStateAtAnimationStart = eLightsStateAtAnimationStart.LightsOff Then
                        If TypeOf currentForm Is formBackglass Then
                            With DirectCast(currentForm, formBackglass)
                                MainFormBackgroundImage = .BackgroundImage
                                .BackgroundImage = .DarkImage
                                .Refresh()
                            End With
                        Else
                            currentForm.Refresh()
                        End If
                    End If
                End If
            Next

            ' maybe switch on or off all involved lights
            If Me.LightsStateAtAnimationStart = eLightsStateAtAnimationStart.InvolvedLightsOff OrElse Me.LightsStateAtAnimationStart = eLightsStateAtAnimationStart.InvolvedLightsOn Then
                For Each groupname As String In LightsInvolved
                    LightGroup(groupname, (Me.LightsStateAtAnimationStart = eLightsStateAtAnimationStart.InvolvedLightsOn))
                Next
            End If

            ' maybe lock some illu
            If Me.LockInvolvedLamps Then
                For Each groupname As String In LightsInvolved
                    If B2SData.IlluminationLocks.ContainsKey(groupname) Then
                        B2SData.IlluminationLocks.Item(groupname) += 1
                    Else
                        B2SData.IlluminationLocks.Add(groupname, 1)
                    End If
                    B2SData.UseIlluminationLocks = True
                Next
            End If

            ' maybe hide score displays
            If Me.HideScoreDisplays Then
                SelectedLEDType = GetLEDType()
                If SelectedLEDType = B2SSettings.eLEDTypes.Dream7 Then
                    For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
                        leddisplay.Value.Visible = False
                    Next
                ElseIf SelectedLEDType = B2SSettings.eLEDTypes.Rendered Then
                    For Each led As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
                        led.Value.Visible = False
                    Next
                End If
            End If

            ' maybe set a new timer interval
            Me.Interval = Me.BaseInterval * If(B2SSettings.AllAnimationSlowDown > 1, B2SSettings.AllAnimationSlowDown, If(Me.SlowDown > 1, Me.SlowDown, 1))

            ' start the base timer
            loopticker = 0
            ticker = 0
            MyBase.Start()
            runningAnimations.Add(Me.Name)

        End Sub
        Public Shadows Sub [Stop]()

            ' remove the start flag
            Me.WouldBeStarted = False

            ' maybe stop the animation a bit later to do the last animation steps
            If (Me.AnimationStopBehaviour = eAnimationStopBehaviour.RunAnimationTillEnd OrElse Me.AnimationStopBehaviour = eAnimationStopBehaviour.RunAnimationToFirstStep) Then
                If Not Me.StopMeLater Then
                    reachedThe0Point = False
                End If
                Me.StopMeLater = True
                Return
            End If

            ' stop the base timer
            runningAnimations.Remove(Me.Name)
            MyBase.Stop()
            Me.Enabled = False
            MyBase.Enabled = False

            ' maybe show score displays
            If HideScoreDisplays Then
                If SelectedLEDType = B2SSettings.eLEDTypes.Dream7 Then
                    For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
                        leddisplay.Value.Visible = True
                    Next
                ElseIf SelectedLEDType = B2SSettings.eLEDTypes.Rendered Then
                    For Each led As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
                        led.Value.Visible = True
                    Next
                End If
            End If

            ' maybe unlock all illu
            If LockInvolvedLamps Then
                For Each groupname As String In LightsInvolved
                    If B2SData.IlluminationLocks.ContainsKey(groupname) Then
                        If B2SData.IlluminationLocks.Item(groupname) > 1 Then
                            B2SData.IlluminationLocks.Item(groupname) -= 1
                        Else
                            B2SData.IlluminationLocks.Remove(groupname)
                        End If
                    End If
                Next
            End If
            B2SData.UseIlluminationLocks = (B2SData.IlluminationLocks.Count > 0)

            ' maybe switch all involved lights on/off or set some lights to initial state
            If LightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.InvolvedLightsOff OrElse LightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.InvolvedLightsOn Then
                For Each groupname As String In LightsInvolved
                    LightGroup(groupname, (LightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.InvolvedLightsOn))
                Next
            ElseIf LightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.LightsReseted Then
                For Each picbox As KeyValuePair(Of String, Boolean) In LightsStateAtStartup
                    LightBulb(picbox.Key, picbox.Value)
                Next
                If MainFormBackgroundImage IsNot Nothing Then
                    If TypeOf Form Is formBackglass Then
                        DirectCast(Form, formBackglass).BackgroundImage = MainFormBackgroundImage
                    End If
                End If
            End If

        End Sub

        Private Sub PictureBoxAnimation_Tick(sender As Object, e As System.EventArgs) Handles Me.Elapsed

            ' first of all stop the timer
            MyBase.Stop()

            ' maybe get out since the timer is already stopped
            If Not runningAnimations.Contains(Me.Name) Then
                Me.Enabled = False
                MyBase.Enabled = False
                Return
            End If

            ' show animation stuff
            If Me.Entries IsNot Nothing AndAlso Me.Entries.Count > 0 Then
                Do While True
                    Dim index As Integer = If(Not PlayReverse, ticker + 1, Me.Entries.Count - ticker)
                    If Me.Entries.ContainsKey(index) Then
                        Dim currentEntryAction As EntryAction = Me.Entries(index)
                        If currentEntryAction.Corrector <> 0 AndAlso PlayReverse Then currentEntryAction = Me.Entries(index + currentEntryAction.Corrector)
                        ' light or unlight bulbs
                        For Each groupname As String In currentEntryAction.Bulbs
                            If Not String.IsNullOrEmpty(groupname) Then
                                LightGroup(groupname, currentEntryAction.Visible)
                            End If
                        Next
                        ' maybe set switch
                        If currentEntryAction.PulseSwitch > 0 Then
                            SetSwitch(currentEntryAction.PulseSwitch)
                        End If
                        ' set next interval
                        If currentEntryAction.IntervalMultiplier > 0 Then
                            Me.Interval = currentEntryAction.IntervalMultiplier * Me.BaseInterval * If(B2SSettings.AllAnimationSlowDown > 1, B2SSettings.AllAnimationSlowDown, If(Me.SlowDown > 1, Me.SlowDown, 1))
                            Exit Do
                        Else
                            ticker += 1
                            If ticker >= Me.Entries.Count Then
                                Exit Do
                            End If
                        End If
                    Else
                        Exit Do
                    End If
                Loop
            End If

            ' count on and maybe restart the timer
            Dim restart As Boolean = True
            ticker += 1
            If ticker >= Me.Entries.Count Then
                reachedThe0Point = True
                loopticker += 1
                ticker = 0
                If Me.Loops > 0 AndAlso loopticker >= Me.Loops Then
                    loopticker = 0
                    restart = False
                    RaiseEvent Finished()
                End If
            End If
            If restart Then
                If B2SSettings.AllAnimationSlowDown = 0 OrElse Me.SlowDown = 0 Then
                    Me.Stop()
                ElseIf Me.StopMeLater AndAlso Me.AnimationStopBehaviour = eAnimationStopBehaviour.RunAnimationTillEnd AndAlso ticker = 0 Then
                    Me.Stop()
                ElseIf Me.StopMeLater AndAlso Me.AnimationStopBehaviour = eAnimationStopBehaviour.RunAnimationToFirstStep AndAlso (ticker = 1 OrElse ticker = 2) AndAlso reachedThe0Point Then
                    Me.Stop()
                Else
                    MyBase.Start()
                End If
            Else
                Me.Stop()
            End If

        End Sub

        Private Sub LightGroup(ByVal _GroupName As String, ByVal _Visible As Boolean)

            ' only do the lightning stuff if the group has a name
            If Not String.IsNullOrEmpty(_GroupName) AndAlso B2SData.IlluminationGroups.ContainsKey(_GroupName) Then

                ' get all matching picture boxes
                For Each picbox As B2SPictureBox In B2SData.IlluminationGroups(_GroupName)
                    Try
                        picbox.Visible(True) = _Visible
                        If _Visible AndAlso BringToFront Then
                            picbox.BringToFront()
                        End If
                    Catch
                    End Try
                Next

            End If

        End Sub
        Private Sub LightBulb(ByVal _Bulb As String, ByVal _Visible As Boolean)

            ' only do the lightning stuff if the bulb has a name
            If Not String.IsNullOrEmpty(_Bulb) Then
                Dim currentForm As Form = Nothing
                If Form.Controls.ContainsKey(_Bulb) Then
                    currentForm = Form
                ElseIf FormDMD.Controls.ContainsKey(_Bulb) Then
                    currentForm = FormDMD
                End If
                If currentForm IsNot Nothing Then
                    DirectCast(currentForm.Controls(_Bulb), B2SPictureBox).Visible(True) = _Visible
                    If _Visible AndAlso BringToFront Then
                        DirectCast(currentForm.Controls(_Bulb), B2SPictureBox).BringToFront()
                    End If
                End If
            End If

        End Sub

        Private Function GetLEDType() As B2SSettings.eLEDTypes
            Dim ret As B2SSettings.eLEDTypes = B2SSettings.eLEDTypes.Undefined
            If B2SData.LEDDisplays.Count > 0 Then
                For Each leddisplay As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
                    If leddisplay.Value.Visible Then ret = B2SSettings.eLEDTypes.Dream7
                    Exit For
                Next
            ElseIf B2SData.LEDs.Count > 0 Then
                For Each led As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
                    If led.Value.Visible Then ret = B2SSettings.eLEDTypes.Rendered
                    Exit For
                Next
            End If
            Return ret
        End Function

    End Class
    Public Class PictureBoxAnimationEntry

        Public On1 As String() = New String() {""}
        Public Off1 As String() = New String() {""}
        Public On2 As String() = New String() {""}
        Public Off2 As String() = New String() {""}

        Public WaitAfterOn1 As Integer = 0
        Public WaitAfterOff1 As Integer = 0
        Public WaitAfterOn2 As Integer = 0
        Public WaitAfterOff2 As Integer = 0

        Public PulseSwitch As Integer = 0

        Public Sub New(ByVal _On1 As Object, ByVal _WaitAfterOn1 As Integer, Optional ByVal _Off1 As Object = 0, Optional ByVal _WaitAfterOff1 As Integer = 0,
                       Optional ByVal _On2 As Object = Nothing, Optional ByVal _WaitAfterOn2 As Integer = 0, Optional ByVal _Off2 As Object = Nothing, Optional ByVal _WaitAfterOff2 As Integer = 0,
                       Optional ByVal _PulseSwitch As Integer = 0)
            On1 = Check4StringOrArray(_On1)
            WaitAfterOn1 = _WaitAfterOn1
            Off1 = Check4StringOrArray(_Off1)
            WaitAfterOff1 = _WaitAfterOff1
            On2 = Check4StringOrArray(_On2)
            WaitAfterOn2 = _WaitAfterOn2
            Off2 = Check4StringOrArray(_Off2)
            WaitAfterOff2 = _WaitAfterOff2
            PulseSwitch = _PulseSwitch
        End Sub

        Private Function Check4StringOrArray(ByVal _Str As Object) As Object
            Dim ret As String() = New String() {""}
            If _Str IsNot Nothing Then
                If IsArray(_Str) Then
                    ret = _Str
                ElseIf _Str.ToString.Contains(",") Then
                    ret = _Str.ToString.Split(",")
                Else
                    ret(0) = _Str
                End If
            End If
            Return ret
        End Function

    End Class

    Public Class PictureBoxAnimationCollection

        Public Entries As PictureBoxAnimationCollectionEntry() = Nothing

        Public StartMeAtVPActivate As Boolean = True

        Public Sub New(ByVal _StartTimerAtVPActivate As Boolean, ByVal ParamArray _Entries As PictureBoxAnimationCollectionEntry())
            StartMeAtVPActivate = _StartTimerAtVPActivate
            Entries = _Entries
        End Sub

    End Class
    Public Class PictureBoxAnimationCollectionEntry

        Public AnimationName As String = String.Empty
        Public Loops As Integer = 1
        Public PlayReverse As Boolean = False

        Public Sub New(ByVal _AnimationName As String, ByVal _Loops As Integer, Optional ByVal _PlayReverse As Boolean = False)
            AnimationName = _AnimationName
            Loops = _Loops
            PlayReverse = _PlayReverse
        End Sub

    End Class

    Private TimerAnimations As Generic.Dictionary(Of String, TimerAnimation) = New Generic.Dictionary(Of String, TimerAnimation)
    Private PictureBoxAnimations As Generic.Dictionary(Of String, PictureBoxAnimation) = New Generic.Dictionary(Of String, PictureBoxAnimation)
    Private PictureBoxAnimationCollections As Generic.Dictionary(Of String, PictureBoxAnimationCollection) = New Generic.Dictionary(Of String, PictureBoxAnimationCollection)

#Region "constructor and startup"

    Public Sub New()

        ' nothing to do right now

    End Sub

#End Region

#Region "add animations and collections of animations"

    Public Sub AddAnimation(ByVal _Name As String,
                            ByVal _Form As Form,
                            ByVal _FormDMD As Form,
                            ByVal _DualMode As B2SData.eDualMode,
                            ByVal _Interval As Integer,
                            ByVal _Loops As Integer,
                            ByVal _StartTimerAtVPActivate As Boolean,
                            ByVal _LightsStateAtAnimationStart As eLightsStateAtAnimationStart,
                            ByVal _LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd,
                            ByVal _AnimationStopBehaviour As eAnimationStopBehaviour,
                            ByVal _LockInvolvedLamps As Boolean,
                            ByVal _HideScoreDisplays As Boolean,
                            ByVal _BringToFront As Boolean,
                            ByVal _RandomStart As Boolean,
                            ByVal _RandomQuality As Integer,
                            ByVal _ImageCollection As ImageList.ImageCollection)
        If Not TimerAnimations.ContainsKey(_Name) Then
            TimerAnimations.Add(_Name, New TimerAnimation(_Form, _FormDMD, _Name, _DualMode, _Interval, _Loops, _StartTimerAtVPActivate, _LightsStateAtAnimationStart, _LightsStateAtAnimationEnd, _AnimationStopBehaviour, _LockInvolvedLamps, _HideScoreDisplays, _BringToFront, _RandomStart, _RandomQuality, _ImageCollection))
        End If
    End Sub
    Public Sub AddAnimation(ByVal _Name As String,
                            ByVal _Form As Form,
                            ByVal _Picturebox As PictureBox,
                            ByVal _DualMode As B2SData.eDualMode,
                            ByVal _Interval As Integer,
                            ByVal _Loops As Integer,
                            ByVal _StartTimerAtVPActivate As Boolean,
                            ByVal _LightsStateAtAnimationStart As eLightsStateAtAnimationStart,
                            ByVal _LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd,
                            ByVal _AnimationStopBehaviour As eAnimationStopBehaviour,
                            ByVal _LockInvolvedLamps As Boolean,
                            ByVal _HideScoreDisplays As Boolean,
                            ByVal _BringToFront As Boolean,
                            ByVal _RandomStart As Boolean,
                            ByVal _RandomQuality As Integer,
                            ByVal _ImageCollection As ImageList.ImageCollection)
        If Not TimerAnimations.ContainsKey(_Name) Then
            TimerAnimations.Add(_Name, New TimerAnimation(_Form, _Picturebox, _Name, _DualMode, _Interval, _Loops, _StartTimerAtVPActivate, _LightsStateAtAnimationStart, _LightsStateAtAnimationEnd, _AnimationStopBehaviour, _LockInvolvedLamps, _HideScoreDisplays, _BringToFront, _RandomStart, _RandomQuality, _ImageCollection))
        End If
    End Sub
    Public Sub AddAnimation(ByVal _Name As String,
                            ByVal _Form As Form,
                            ByVal _FormDMD As Form,
                            ByVal _DualMode As B2SData.eDualMode,
                            ByVal _Interval As Integer,
                            ByVal _Loops As Integer,
                            ByVal _StartTimerAtVPActivate As Boolean,
                            ByVal _LightsStateAtAnimationStart As eLightsStateAtAnimationStart,
                            ByVal _LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd,
                            ByVal _AnimationStopBehaviour As eAnimationStopBehaviour,
                            ByVal _LockInvolvedLamps As Boolean,
                            ByVal _HideScoreDisplays As Boolean,
                            ByVal _BringToFront As Boolean,
                            ByVal _RandomStart As Boolean,
                            ByVal _RandomQuality As Integer,
                            ByVal ParamArray _entries As PictureBoxAnimationEntry())
        If Not PictureBoxAnimations.ContainsKey(_Name) Then
            PictureBoxAnimations.Add(_Name, New PictureBoxAnimation(_Form, _FormDMD, _Name, _DualMode, _Interval, _Loops, _StartTimerAtVPActivate, _LightsStateAtAnimationStart, _LightsStateAtAnimationEnd, _AnimationStopBehaviour, _LockInvolvedLamps, _HideScoreDisplays, _BringToFront, _RandomStart, _RandomQuality, _entries))
        End If
    End Sub

    Public Sub AddCollection(ByVal _Name As String,
                             ByVal _StartTimerAtVPActivate As Boolean,
                             ByVal ParamArray _Entries As PictureBoxAnimationCollectionEntry())
        If Not PictureBoxAnimationCollections.ContainsKey(_Name) Then
            PictureBoxAnimationCollections.Add(_Name, New PictureBoxAnimationCollection(_StartTimerAtVPActivate, _Entries))
        End If
    End Sub

#End Region

#Region "start and stop animations and collections of animations"

    Public ReadOnly Property AreThereAnimations() As Boolean
        Get
            Return (TimerAnimations.Count > 0 OrElse PictureBoxAnimations.Count > 0)
        End Get
    End Property
    Public ReadOnly Property AreThereTimerAnimations() As Boolean
        Get
            Return (TimerAnimations.Count > 0)
        End Get
    End Property
    Public ReadOnly Property AreTherePictureBoxAnimations() As Boolean
        Get
            Return (PictureBoxAnimations.Count > 0)
        End Get
    End Property

    Public Sub AutoStart()

        ' start all autostart timer animations
        For Each timer As KeyValuePair(Of String, TimerAnimation) In TimerAnimations
            With timer.Value
                If .StartMeAtVPActivate AndAlso Not .Enabled Then
                    .Start()
                End If
            End With
        Next

        ' start all autostart picture box animations
        For Each pictimer As KeyValuePair(Of String, PictureBoxAnimation) In PictureBoxAnimations
            With pictimer.Value
                If .StartMeAtVPActivate AndAlso Not .Enabled Then
                    .Start()
                End If
            End With
        Next

        ' and now start all autostart picture box animations' collections
        For Each pictimer As KeyValuePair(Of String, PictureBoxAnimationCollection) In PictureBoxAnimationCollections
            If pictimer.Value.StartMeAtVPActivate Then
                StartCollection(pictimer.Key)
            End If
        Next

    End Sub

    Public Sub StartAnimation(ByVal _Name As String,
                              Optional ByVal _PlayReverse As Boolean = False)
        If TimerAnimations.ContainsKey(_Name) AndAlso Not TimerAnimations(_Name).Enabled Then
            TimerAnimations(_Name).PlayReverse = _PlayReverse
            TimerAnimations(_Name).Start()
        End If
        If PictureBoxAnimations.ContainsKey(_Name) AndAlso Not PictureBoxAnimations(_Name).Enabled Then
            PictureBoxAnimations(_Name).PlayReverse = _PlayReverse
            PictureBoxAnimations(_Name).Start()
        End If
    End Sub
    Public Sub RestartAnimations()
        For Each timer As KeyValuePair(Of String, TimerAnimation) In TimerAnimations
            If timer.Value.WouldBeStarted Then
                timer.Value.Stop()
                timer.Value.Start()
            Else
                timer.Value.Stop()
            End If
        Next
        For Each timer As KeyValuePair(Of String, PictureBoxAnimation) In PictureBoxAnimations
            If timer.Value.WouldBeStarted Then
                timer.Value.Stop()
                timer.Value.Start()
            Else
                timer.Value.Stop()
            End If
        Next
    End Sub
    Public Sub StopAnimation(ByVal _Name As String)
        If TimerAnimations.ContainsKey(_Name) Then
            TimerAnimations(_Name).Stop()
        End If
        If PictureBoxAnimations.ContainsKey(_Name) Then
            PictureBoxAnimations(_Name).Stop()
        End If
    End Sub
    Public Sub StopAllAnimations()
        For Each timer As KeyValuePair(Of String, TimerAnimation) In TimerAnimations
            timer.Value.Stop()
        Next
        For Each timer As KeyValuePair(Of String, PictureBoxAnimation) In PictureBoxAnimations
            timer.Value.Stop()
        Next
    End Sub

    Public Property AnimationSlowDown(ByVal _Name As String) As Integer
        Get
            If TimerAnimations.ContainsKey(_Name) Then Return TimerAnimations(_Name).SlowDown
            If PictureBoxAnimations.ContainsKey(_Name) Then Return PictureBoxAnimations(_Name).SlowDown
            Return 1
        End Get
        Set(ByVal value As Integer)
            If TimerAnimations.ContainsKey(_Name) Then
                TimerAnimations(_Name).SlowDown = value
            End If
            If PictureBoxAnimations.ContainsKey(_Name) Then
                PictureBoxAnimations(_Name).SlowDown = value
            End If
        End Set
    End Property

    Public Function IsAnimationRunning(ByVal _Name As String) As Boolean
        If TimerAnimations.ContainsKey(_Name) Then
            Return TimerAnimations(_Name).Enabled
        End If
        If PictureBoxAnimations.ContainsKey(_Name) Then
            Return PictureBoxAnimations(_Name).Enabled
        End If
        Return False
    End Function

    Public ReadOnly Property Animations() As Generic.List(Of String)
        Get
            Dim ret As Generic.List(Of String) = New Generic.List(Of String)
            For Each timer As KeyValuePair(Of String, TimerAnimation) In TimerAnimations
                If Not ret.Contains(timer.Key) Then ret.Add(timer.Key)
            Next
            For Each timer As KeyValuePair(Of String, PictureBoxAnimation) In PictureBoxAnimations
                If Not ret.Contains(timer.Key) Then ret.Add(timer.Key) ' & If(timer.Value.SlowDown = 1, "", " (" & If(timer.Value.SlowDown = 0, "Off", timer.Value.SlowDown.ToString & "x") & ")"))
            Next
            Return ret
        End Get
    End Property

    Private WithEvents CurrentCollectionAnimation As PictureBoxAnimation = Nothing
    Private CurrentCollectionEntries As PictureBoxAnimationCollectionEntry() = Nothing
    Private CurrentCollectionIndex As Integer = 0
    Public Sub StartCollection(ByVal _Name As String)
        If PictureBoxAnimationCollections.ContainsKey(_Name) Then
            CurrentCollectionEntries = PictureBoxAnimationCollections(_Name).Entries
            CurrentCollectionIndex = 0
            If IsArray(CurrentCollectionEntries) AndAlso CurrentCollectionEntries.Length > 0 Then
                Dim firstEntry As PictureBoxAnimationCollectionEntry = CurrentCollectionEntries(0)
                If Not String.IsNullOrEmpty(firstEntry.AnimationName) AndAlso PictureBoxAnimations.ContainsKey(firstEntry.AnimationName) AndAlso firstEntry.Loops > 0 Then
                    CurrentCollectionAnimation = PictureBoxAnimations(firstEntry.AnimationName)
                    CurrentCollectionAnimation.Loops = firstEntry.Loops
                    CurrentCollectionAnimation.PlayReverse = firstEntry.PlayReverse
                    CurrentCollectionAnimation.Start()
                End If
            End If
        End If
    End Sub
    Public Sub StopCollection(ByVal _Name As String)
        If CurrentCollectionAnimation IsNot Nothing Then
            CurrentCollectionAnimation.Stop()
        End If
    End Sub

    Private Sub CurrentCollectionAnimation_Finished() Handles CurrentCollectionAnimation.Finished
        CurrentCollectionIndex += 1
        If CurrentCollectionEntries.Length > CurrentCollectionIndex Then
            Dim nextEntry As PictureBoxAnimationCollectionEntry = CurrentCollectionEntries(CurrentCollectionIndex)
            If Not String.IsNullOrEmpty(nextEntry.AnimationName) AndAlso PictureBoxAnimations.ContainsKey(nextEntry.AnimationName) AndAlso nextEntry.Loops > 0 Then
                CurrentCollectionAnimation = PictureBoxAnimations(nextEntry.AnimationName)
                CurrentCollectionAnimation.Loops = nextEntry.Loops
                CurrentCollectionAnimation.PlayReverse = nextEntry.PlayReverse
                CurrentCollectionAnimation.Start()
            End If
        End If
    End Sub

#End Region

#Region "switch timer stuff"

    Private Shared switches As Generic.SortedList(Of Integer, Boolean) = New Generic.SortedList(Of Integer, Boolean)

    Private Shared SwitchTimer As Timers.Timer = Nothing

    Public Shared Sub SetSwitch(ByVal switchid As Integer)

        B2SData.VPinMAME.Switch(switchid) = True

        If Not switches.ContainsKey(switchid) Then
            switches.Add(switchid, True)
        End If

        If SwitchTimer Is Nothing Then
            SwitchTimer = New Timers.Timer
            AddHandler SwitchTimer.Elapsed, AddressOf SwitchTimer_Elasped
            SwitchTimer.Interval = 200
        End If
        SwitchTimer.Stop()
        SwitchTimer.Start()

    End Sub

    Private Shared Sub SwitchTimer_Elasped(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs)

        SwitchTimer.Stop()

        For Each switch As KeyValuePair(Of Integer, Boolean) In switches
            B2SData.VPinMAME.Switch(switch.Key) = False
        Next
        switches.Clear()

    End Sub

#End Region

End Class