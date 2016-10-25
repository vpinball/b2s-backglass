Imports System

Public Class formToolReelsAndLEDs

    Public ignoreChange As Boolean = False

    Private RenderedLED As B2SRenderedLED = New B2SRenderedLED()

    Public Enum eScoreDataType
        NotDefined = 0
        B2SStartDigit = 1
        B2SScoreType = 2
        B2SPlayerNo = 3
        NumberOfPlayers = 4
        NumberOfDigits = 5
        Spacing = 6
        Location = 7
        Size = 8
        DisplayState = 9
        ReelType = 11
        UseDream7LEDs = 12
        PerfectScaleWidthFix = 21
        ChangeLEDColor = 22
        ReelIllumination = 23
    End Enum

    Public Event DataChanged(ByVal sender As Object, ByVal e As ScoreEventArgs)
    Public Class ScoreEventArgs
        Inherits EventArgs

        Public TypeOfData As eScoreDataType = eScoreDataType.NotDefined
        Public Data As Object = Nothing
        'Public Data2 As Object = Nothing

        Public Sub New(ByVal _typeofdata As eScoreDataType)
            TypeOfData = _typeofdata
        End Sub
        Public Sub New(ByVal _typeofdata As eScoreDataType, ByVal _data As Object)
            TypeOfData = _typeofdata
            Data = _data
        End Sub
        'Public Sub New(ByVal _typeofdata As eScoreDataType, ByVal _data As Object, ByVal _data2 As Object)
        '    TypeOfData = _typeofdata
        '    Data = _data
        '    Data2 = _data2
        'End Sub
    End Class

    Public Sub ReloadReels()
        ' get images of reels and leds into the image list
        If Backglass.currentData IsNot Nothing Then
            FillReelListView(eImageSetType.ReelImages, ilReelsAndLEDs, lvReelsAndLEDs, DefaultEMReels, True, True, Backglass.currentData.ReelType)
            FillReelListView(eImageSetType.CreditReelImages, ilReelsAndLEDs, lvReelsAndLEDs, DefaultEMCreditReels, , True, Backglass.currentData.ReelType)
            FillReelListView(eImageSetType.LEDImages, ilReelsAndLEDs, lvReelsAndLEDs, DefaultLEDs, , True, Backglass.currentData.ReelType)
            AddRenderedLEDs()
        End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MyBase.SaveName = Me.Name
        MyBase.DefaultLocation = eDefaultLocation.SW

    End Sub

    Private Sub formToolReelsAndLEDs_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ReloadReels()

        cmbNumberOfPlayers.SelectionLength = 0

    End Sub

    Private Sub NumberOfPlayers_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbNumberOfPlayers.SelectedIndexChanged
        If IsNumeric(cmbNumberOfPlayers.Text) Then
            Dim numberofplayers As Integer = CInt(cmbNumberOfPlayers.Text)
            If numberofplayers >= 1 AndAlso numberofplayers <= 4 Then
                RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.NumberOfPlayers, numberofplayers))
            End If
        End If
    End Sub

    Private Sub Digits_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numericDigits.ValueChanged
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.NumberOfDigits, numericDigits.Value))
    End Sub
    Private Sub Spacing_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numericSpacing.ValueChanged
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.Spacing, numericSpacing.Value))
    End Sub

    Private Sub InitState_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbInitState.SelectedIndexChanged
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.DisplayState, cmbInitState.SelectedIndex))
    End Sub

    Private Sub B2SScoreType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbB2SScoreType.SelectedIndexChanged
        If ignoreChange Then Return
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.B2SScoreType, cmbB2SScoreType.SelectedIndex))
        SetScoreType()
    End Sub
    Private Sub cmbB2SPlayerNo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbB2SPlayerNo.SelectedIndexChanged
        If ignoreChange Then Return
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.B2SPlayerNo, cmbB2SPlayerNo.SelectedIndex))
        SetPlayerNo()
        SetScoreType()
    End Sub
    Private Sub B2SStartID_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtB2SStartID.TextChanged
        If Not String.IsNullOrEmpty(txtB2SStartID.Text) Then
            If (Not IsNumeric(txtB2SStartID.Text) OrElse txtB2SStartID.Text = "0") Then txtB2SStartID.Text = ""
        End If
        If ignoreChange Then Return
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.B2SStartDigit, txtB2SStartID.Text))
        If IsOverlappingB2SStartDigit() Then
            If MessageBox.Show(My.Resources.MSG_CheckB2SStartDigit2, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                txtB2SStartID.Text = GetScoreStartDigit(CInt(txtID.Text), False)
            End If
        End If

    End Sub
    Private Sub SetScoreType()
        Select Case cmbB2SScoreType.SelectedIndex
            Case 0
                cmbB2SPlayerNo.SelectedIndex = 0
            Case 1
                'If String.IsNullOrEmpty(txtB2SStartID.Text) Then
                If IsOverlappingB2SStartDigit() Then
                    txtB2SStartID.Text = GetScoreStartDigit(CInt(txtID.Text), True)
                End If
                'End If
            Case 2
                cmbB2SPlayerNo.SelectedIndex = 0
                'If String.IsNullOrEmpty(txtB2SStartID.Text) Then
                txtB2SStartID.Text = GetCreditsStartDigit()
                'End If
        End Select
    End Sub
    Private Sub SetPlayerNo()
        If cmbB2SPlayerNo.SelectedIndex <> 0 AndAlso cmbB2SScoreType.SelectedIndex <> 1 Then cmbB2SScoreType.SelectedIndex = 1
    End Sub

    Private Sub ReelsAndLEDs_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lvReelsAndLEDs.MouseClick
        If lvReelsAndLEDs.SelectedItems IsNot Nothing AndAlso lvReelsAndLEDs.SelectedItems.Count > 0 AndAlso lvReelsAndLEDs.SelectedItems(0) IsNot Nothing Then
            Dim name As String = lvReelsAndLEDs.SelectedItems(0).Name
            If Not String.IsNullOrEmpty(name) Then
                RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.ReelType, name))
            End If
        End If
    End Sub
    Private Sub Dream7_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDream7.CheckedChanged
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.UseDream7LEDs, chkDream7.Checked))
    End Sub

    Private Sub LocationX_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLocationX.TextChanged
        If ignoreChange Then Return
        If Not IsNumeric(txtLocationX.Text) Then txtLocationX.Text = 0
        If Not IsNumeric(txtLocationY.Text) Then Return
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.Location, New Point(CInt(txtLocationX.Text), CInt(txtLocationY.Text))))
    End Sub
    Private Sub LocationY_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLocationY.TextChanged
        If ignoreChange Then Return
        If Not IsNumeric(txtLocationY.Text) Then txtLocationY.Text = 0
        If Not IsNumeric(txtLocationX.Text) Then Return
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.Location, New Point(CInt(txtLocationX.Text), CInt(txtLocationY.Text))))
    End Sub
    Private Sub SizeWidth_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSizeWidth.TextChanged
        If ignoreChange Then Return
        If Not IsNumeric(txtSizeWidth.Text) Then txtSizeWidth.Text = 0
        If Not IsNumeric(txtSizeHeight.Text) Then Return
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.Size, New Size(CInt(txtSizeWidth.Text), CInt(txtSizeHeight.Text))))
    End Sub
    Private Sub SizeHeight_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSizeHeight.TextChanged
        If ignoreChange Then Return
        If Not IsNumeric(txtSizeHeight.Text) Then txtSizeHeight.Text = 0
        If Not IsNumeric(txtSizeWidth.Text) Then Return
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.Size, New Size(CInt(txtSizeWidth.Text), CInt(txtSizeHeight.Text))))
    End Sub
    Private Sub Size_Validated(sender As Object, e As System.EventArgs) Handles txtSizeWidth.Validated, txtSizeHeight.Validated
        If Not IsNumeric(txtSizeWidth.Text) Then txtSizeWidth.Text = "10"
        If Not IsNumeric(txtSizeHeight.Text) Then txtSizeHeight.Text = "10"
        If CInt(txtSizeWidth.Text) < 10 Then txtSizeWidth.Text = "10"
        If CInt(txtSizeHeight.Text) < 10 Then txtSizeHeight.Text = "10"
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.Size, New Size(CInt(txtSizeWidth.Text), CInt(txtSizeHeight.Text))))
    End Sub

    Private Sub PerfectScaleWidthFix_Click(sender As System.Object, e As System.EventArgs) Handles btnPerfectScaleWidthFix.Click
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.PerfectScaleWidthFix))
    End Sub

    Private Sub ChangeLEDColor_Click(sender As System.Object, e As System.EventArgs) Handles btnChangeLEDColor.Click
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.ChangeLEDColor))
    End Sub
    Private Sub ReelIllumination_Click(sender As System.Object, e As System.EventArgs) Handles btnReelIllumination.Click
        RaiseEvent DataChanged(Me, New ScoreEventArgs(eScoreDataType.ReelIllumination))
    End Sub

    Private Sub AddRenderedLEDs()
        For Each item As String In Backglass.currentData.ReelType.Split(",")
            If IsReelImageRendered(item) Then
                Select Case item.Substring(11)
                    Case "7", "8"
                        RenderedLED.LEDType = B2SRenderedLED.eLEDType.LED8
                    Case "9", "10"
                        RenderedLED.LEDType = B2SRenderedLED.eLEDType.LED10
                    Case "14"
                        RenderedLED.LEDType = B2SRenderedLED.eLEDType.LED14
                    Case "16"
                        RenderedLED.LEDType = B2SRenderedLED.eLEDType.LED16
                End Select
                Dim index As Integer = ilReelsAndLEDs.Images.Count
                ilReelsAndLEDs.Images.Add(RenderedLED.Image(Backglass.currentData.ReelColor))
                lvReelsAndLEDs.Items.Add(item, "Rendered LED", index)
            End If
        Next
    End Sub

    Private Function GetScoreStartDigit(ByVal scoreid As Integer, ByVal message4Updated As Boolean) As Integer
        Dim ret As Integer = 1
        For i As Integer = 1 To 2
            For Each score As ReelAndLED.ScoreInfo In If(i = 1, Backglass.currentData.Scores, Backglass.currentData.DMDScores)
                If score.B2SScoreType = eB2SScoreType.Credits_29 Then
                    score.numbered = True
                Else
                    score.numbered = False
                End If
            Next
        Next
        Dim startAt As Integer = 1
        Dim changesDone As Boolean = False
        Do While IsThereAnyScoreNotNumbered()
            Dim plno As Integer = 999999
            Dim plscore As ReelAndLED.ScoreInfo = Nothing
            Dim idno As Integer = 999999
            Dim idscore As ReelAndLED.ScoreInfo = Nothing
            For i As Integer = 1 To 2
                For Each score As ReelAndLED.ScoreInfo In If(i = 1, Backglass.currentData.Scores, Backglass.currentData.DMDScores)
                    If Not score.numbered Then
                        If score.B2SPlayerNo > 0 Then
                            If score.B2SPlayerNo < plno Then
                                plno = score.B2SPlayerNo
                                plscore = score
                            End If
                        ElseIf score.ID < idno Then
                            idno = score.ID
                            idscore = score
                        End If
                    End If
                Next
            Next
            If plscore IsNot Nothing Then
                plscore.numbered = True
                If plscore.B2SStartDigit <> startAt Then
                    plscore.B2SStartDigit = startAt
                    changesDone = True
                End If
                If plscore.ID = scoreid Then ret = startAt
                startAt += plscore.Digits
            ElseIf idscore IsNot Nothing Then
                idscore.numbered = True
                If idscore.B2SStartDigit <> startAt Then
                    idscore.B2SStartDigit = startAt
                    changesDone = True
                End If
                If idscore.ID = scoreid Then ret = startAt
                startAt += idscore.Digits
            End If
        Loop
        If changesDone AndAlso message4Updated Then
            MessageBox.Show(My.Resources.MSG_CheckB2SStartDigit3, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        'If IsThereAnyScoreWithPlayerNo() Then
        '    'Dim currentplayerstartsat As Integer = 0
        '    Dim currentplayerendsat As Integer = 0
        '    For i As Integer = 1 To 2
        '        For Each score As ReelAndLED.ScoreInfo In If(i = 1, Backglass.currentData.Scores, Backglass.currentData.DMDScores)
        '            If score.B2SPlayerNo <> eB2SPlayerNo.NotUsed AndAlso (score.B2SPlayerNo <= playerno OrElse playerno = eB2SPlayerNo.NotUsed) AndAlso score.ID <> scoreid Then
        '                'If currentplayerstartsat = 0 OrElse currentplayerstartsat > score.B2SStartDigit Then currentplayerstartsat = score.B2SStartDigit
        '                If currentplayerendsat = 0 OrElse currentplayerendsat < score.B2SStartDigit + score.Digits Then currentplayerendsat = score.B2SStartDigit + score.Digits
        '            End If
        '        Next
        '    Next
        '    If currentplayerendsat > 0 Then
        '        ret = currentplayerendsat
        '    Else
        '        Select Case playerno
        '            Case eB2SPlayerNo.Player1 : ret = 1
        '            Case eB2SPlayerNo.Player2 : ret = 2
        '            Case eB2SPlayerNo.Player3 : ret = 3
        '            Case eB2SPlayerNo.Player4 : ret = 4
        '            Case Else : ret = 1
        '        End Select
        '    End If
        '    'For i As Integer = 1 To 2
        '    '    For Each score As ReelAndLED.ScoreInfo In If(i = 1, Backglass.currentData.Scores, Backglass.currentData.DMDScores)

        '    '    Next
        '    'Next
        'Else
        '    For i As Integer = 1 To 2
        '        For Each score As ReelAndLED.ScoreInfo In If(i = 1, Backglass.currentData.Scores, Backglass.currentData.DMDScores)
        '            If score.ID < scoreid AndAlso score.B2SScoreType = eB2SScoreType.Scores_01 Then
        '                ret += 6
        '            End If
        '        Next
        '    Next
        '    If ret > 24 Then ret = 1
        'End If
        Return ret
    End Function
    Private Function GetCreditsStartDigit() As Integer
        Dim ret As Integer = 29
        Return ret
    End Function
    Private Function IsThereAnyScoreWithPlayerNo() As Boolean
        Dim ret As Boolean = False
        For i As Integer = 1 To 2
            For Each score As ReelAndLED.ScoreInfo In If(i = 1, Backglass.currentData.Scores, Backglass.currentData.DMDScores)
                If score.B2SPlayerNo <> eB2SPlayerNo.NotUsed Then
                    ret = True
                    Exit For
                End If
            Next
            If ret Then Exit For
        Next
        Return ret
    End Function
    Private Function IsThereAnyScoreNotNumbered() As Boolean
        Dim ret As Boolean = False
        For i As Integer = 1 To 2
            For Each score As ReelAndLED.ScoreInfo In If(i = 1, Backglass.currentData.Scores, Backglass.currentData.DMDScores)
                If Not score.numbered Then
                    ret = True
                    Exit For
                End If
            Next
            If ret Then Exit For
        Next
        Return ret
    End Function

End Class