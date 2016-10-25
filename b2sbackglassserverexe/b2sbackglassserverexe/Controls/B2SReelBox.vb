Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class B2SReelBox

    Inherits B2SBaseBox

    Public Enum eScoreType
        NotUsed = 0
        Scores = 1
        Credits = 2
    End Enum

    Public Class ReelRollOverEventArgs
        Inherits EventArgs

        Public Digit As Integer = 0

        Public Sub New(ByVal _digit As Integer)
            Digit = _digit
        End Sub
    End Class
    Public Event ReelRollOver(ByVal sender As Object, ByVal e As ReelRollOverEventArgs)

    Private timer As Timer = Nothing
    Private cTimerInterval As Integer = 101

    Private isLED As Boolean = False

    Private length As Integer = 1
    Private initValue As String = "0"
    Private reelindex As String = String.Empty

    Private intermediates As Integer = -1
    Private intermediates2go As Integer = 0

    Protected Overrides Sub Dispose(disposing As Boolean)
        MyBase.Dispose(disposing)
        On Error Resume Next
        If disposing Then
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf ReelAnimationTimer_Tick
            timer.Dispose()
            timer = Nothing
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

        If Not String.IsNullOrEmpty(reelindex) Then
            Dim images As Generic.Dictionary(Of String, Image) = If(_Illuminated, B2SData.ReelIlluImages, B2SData.ReelImages)
            Dim intimages As Generic.Dictionary(Of String, Image) = If(_Illuminated, B2SData.ReelIntermediateIlluImages, B2SData.ReelIntermediateImages)
            Dim name As String = String.Empty
            If intermediates = -1 AndAlso timer.Enabled Then
                Static firstintermediatecount As Integer = 1
                name = _ReelType & "_" & reelindex & If(SetID > 0 AndAlso _Illuminated, "_" & SetID.ToString(), "") & "_" & firstintermediatecount.ToString()
                If intimages.ContainsKey(name) Then
                    e.Graphics.DrawImage(intimages(name), e.ClipRectangle)
                    firstintermediatecount += 1
                    intermediates2go = 2
                Else
                    name = _ReelType & "_" & ConvertText(_CurrentText + 1) & If(SetID > 0 AndAlso _Illuminated, "_" & SetID.ToString(), "")
                    If images.ContainsKey(name) Then e.Graphics.DrawImage(images(name), e.ClipRectangle)
                    intermediates = firstintermediatecount - 1
                    intermediates2go = 1
                End If
            ElseIf intermediates2go > 0 Then
                name = _ReelType & "_" & reelindex & If(SetID > 0 AndAlso _Illuminated, "_" & SetID.ToString(), "") & "_" & (intermediates - intermediates2go + 1).ToString()
                If intimages.ContainsKey(name) Then e.Graphics.DrawImage(intimages(name), e.ClipRectangle)
            Else
                name = _ReelType & "_" & reelindex & If(SetID > 0 AndAlso _Illuminated, "_" & SetID.ToString(), "")
                If images.ContainsKey(name) Then e.Graphics.DrawImage(images(name), e.ClipRectangle)
            End If
        End If

    End Sub
    'Protected Overrides Sub OnPaintBackground(pevent As System.Windows.Forms.PaintEventArgs)

    '    ' nothing to do but important

    'End Sub

    Public Sub New()

        ' set some styles
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)
        'Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.DoubleBuffered = True

        ' back color
        'Me.BackColor = Color.Transparent

        ' show control
        'Me.Visible = True

        ' create timer
        timer = New Timer()
        timer.Interval = CInt(_RollingInterval / (If((intermediates = -1), 3, intermediates) + 2))
        AddHandler timer.Tick, AddressOf ReelAnimationTimer_Tick

    End Sub

    Private Sub B2SReelBox_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        On Error Resume Next
        If timer IsNot Nothing Then
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf ReelAnimationTimer_Tick
            timer.Dispose()
            timer = Nothing
        End If
    End Sub

    Private Sub ReelAnimationTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs)

        If intermediates2go > 0 OrElse intermediates = -1 Then

            Me.Refresh()
            intermediates2go -= 1

        Else

            If intermediates2go = 0 Then
                ' add one reel step
                _CurrentText += 1
                If _CurrentText > 9 Then
                    _CurrentText = 0
                    RaiseEvent ReelRollOver(Me, New ReelRollOverEventArgs(ID))
                End If

                reelindex = ConvertText(_CurrentText)

                ' play sound and redraw reel
                Try
                    If Sound() IsNot Nothing Then
                        My.Computer.Audio.Play(Sound(), AudioPlayMode.Background)
                    ElseIf SoundName() = "stille" Then
                        ' no sound
                    Else
                        My.Computer.Audio.Play(My.Resources.EMReel, AudioPlayMode.Background)
                    End If
                Catch
                End Try
                Me.Refresh()

                intermediates2go -= 1
            ElseIf intermediates2go = -1 Then
                intermediates2go -= 1
            Else
                ' maybe stop timer
                intermediates2go = intermediates
                If _CurrentText = _Text OrElse _Text >= 10 Then
                    timer.Stop()
                    timer.Interval = CInt(_RollingInterval / (If((intermediates = -1), 3, intermediates) + 2))
                End If
            End If

        End If

    End Sub

    Public Property SetID() As Integer

    Private _ReelType As String
    Public Property ReelType() As String
        Get
            Return _ReelType
        End Get
        Set(ByVal value As String)
            reelindex = "0"
            If value.Substring(value.Length - 1, 1) = "_" Then
                length = 2
                reelindex = "00"
                value = value.Substring(0, value.Length - 1)
            End If
            If value.StartsWith("LED", StringComparison.CurrentCultureIgnoreCase) OrElse value.StartsWith("ImportedLED", StringComparison.CurrentCultureIgnoreCase) Then
                isLED = True
                reelindex = "Empty"
                initValue = "Empty"
                _Text = -1
            End If
            _ReelType = value
        End Set
    End Property

    Public Property SoundName() As String = String.Empty
    Public Property Sound() As Byte() = Nothing

    Public Property ScoreType() As eScoreType = eScoreType.NotUsed

    Public Property GroupName() As String = String.Empty

    Private _Illuminated As Boolean
    Public Property Illuminated() As Boolean
        Get
            Return _Illuminated
        End Get
        Set(ByVal value As Boolean)
            If _Illuminated <> value Then
                _Illuminated = value
                intermediates2go = 0
                Me.Refresh()
            End If
        End Set
    End Property

    Private _Value As Integer = 0
    Public Property Value(Optional ByVal refresh As Boolean = False) As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            If _Value <> value OrElse refresh Then
                _Value = value
                reelindex = ConvertValue(_Value)
                Me.Refresh()
            End If
        End Set
    End Property

    Private _CurrentText As Integer = 0
    Private _Text As Integer = 0
    Public Shadows Property Text(Optional ByVal AnimateReelChange As Boolean = True) As Integer
        Get
            Return _Text
        End Get
        Set(ByVal value As Integer)
            If value >= 0 Then
                If _Text <> value Then
                    _Text = value
                    If AnimateReelChange AndAlso Not isLED Then
                        timer.Stop()
                        intermediates2go = intermediates
                        timer.Start()
                    Else
                        reelindex = ConvertText(_Text)
                        Me.Refresh()
                    End If
                End If
            End If
        End Set
    End Property
    Public ReadOnly Property CurrentText() As Integer
        Get
            Return _CurrentText
        End Get
    End Property

    Private _RollingInterval As Integer = cTimerInterval
    Public Property RollingInterval() As Integer
        Get
            Return _RollingInterval
        End Get
        Set(ByVal value As Integer)
            If _RollingInterval <> value Then
                _RollingInterval = value
                If _RollingInterval < 10 Then _RollingInterval = cTimerInterval
            End If
        End Set
    End Property

    Public ReadOnly Property IsInReelRolling() As Boolean
        Get
            Return (intermediates2go <= 0)
        End Get
    End Property
    Public ReadOnly Property IsInAction() As Boolean
        Get
            Return timer.Enabled
        End Get
    End Property

    Private Function ConvertValue(ByVal value As Integer) As String
        Dim ret As String = initValue
        ' remove the "," from the 7-segmenter
        If value >= 128 AndAlso value <= 255 Then
            value -= 128
        End If
        ' map value
        If value > 0 Then
            Select Case value
                ' 7-segment stuff
                Case 63
                    ret = "0"
                Case 6
                    ret = "1"
                Case 91
                    ret = "2"
                Case 79
                    ret = "3"
                Case 102
                    ret = "4"
                Case 109
                    ret = "5"
                Case 125
                    ret = "6"
                Case 7
                    ret = "7"
                Case 127
                    ret = "8"
                Case 111
                    ret = "9"
                Case Else
                    'additional 10-segment stuff
                    Select Case value
                        Case 768
                            ret = "1"
                        Case 124
                            ret = "6"
                        Case 103
                            ret = "9"
                            'Case Else
                            '    Debug.WriteLine(_Value)
                    End Select
            End Select
        End If
        Return If(length = 2, "0", "") & ret
    End Function
    Private Function ConvertText(ByVal text As Integer) As String
        Dim ret As String = String.Empty
        ret = "00" & text.ToString()
        ret = ret.Substring(ret.Length - length, length)
        Return ret
    End Function

End Class
