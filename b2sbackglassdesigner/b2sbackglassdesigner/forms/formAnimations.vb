Imports System
Imports System.Text

Public Class formAnimations

    Public Event ResetAnimationLights(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ShowAnimation(ByVal sender As Object, ByVal e As ShowAnimationEventArgs)
    Public Class ShowAnimationEventArgs

        Inherits EventArgs

        Public Name As String = String.Empty

        Public Sub New(ByVal _name As String)
            Name = _name
        End Sub

    End Class

    Private currentAnimationHeader As Animation.AnimationHeader = Nothing
    Private currentAnimationStep As Animation.AnimationStep = Nothing

    Public Sub BulbClicked(ByVal name As String, ByVal id As Integer)
        If currentAnimationStep IsNot Nothing AndAlso Not String.IsNullOrEmpty(name) Then
            If Not String.IsNullOrEmpty(txtLampsOn.Text) Then txtLampsOn.Text &= ","
            txtLampsOn.Text &= name
            If Not String.IsNullOrEmpty(txtLampsOff.Text) Then txtLampsOff.Text &= ","
            txtLampsOff.Text &= name
        End If
    End Sub

    Private Sub formAnimations_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        RaiseEvent ResetAnimationLights(Me, New System.EventArgs)
        PopulateAnimations()
    End Sub

    Private Sub Animations_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbAnimations.SelectedIndexChanged
        If cmbAnimations.Items(cmbAnimations.SelectedIndex) IsNot Nothing Then
            EnableAll()
            currentAnimationHeader = DirectCast(cmbAnimations.Items(cmbAnimations.SelectedIndex), Animation.AnimationHeader)
            currentAnimationStep = Nothing
            With currentAnimationHeader
                txtName.Text = .Name
                If Backglass.currentData.DualBackglass Then
                    cmbDualMode.SelectedIndex = .DualMode
                Else
                    cmbDualMode.Text = String.Empty
                End If
                txtInterval.Text = .Interval.ToString()
                txtLoops.Text = .Loops.ToString()
                txtIDJoin.Text = .IDJoin
                chkAutostart.Checked = .StartAnimationAtBackglassStartup
                cmbLightsStateAtStart.SelectedIndex = .LightsStateAtAnimationStart - 1
                If cmbLightsStateAtStart.SelectedIndex < 0 OrElse cmbLightsStateAtStart.SelectedIndex > cmbLightsStateAtStart.Items.Count - 1 Then cmbLightsStateAtStart.SelectedIndex = cmbLightsStateAtStart.Items.Count - 1
                cmbLightsStateAtEnd.SelectedIndex = .LightsStateAtAnimationEnd - 1
                If cmbLightsStateAtEnd.SelectedIndex < 0 OrElse cmbLightsStateAtEnd.SelectedIndex > cmbLightsStateAtEnd.Items.Count - 1 Then cmbLightsStateAtEnd.SelectedIndex = 0
                cmbAnimationStopBehaviour.SelectedIndex = .AnimationStopBehaviour - 1
                If cmbAnimationStopBehaviour.SelectedIndex < 0 OrElse cmbAnimationStopBehaviour.SelectedIndex > cmbAnimationStopBehaviour.Items.Count - 1 Then cmbAnimationStopBehaviour.SelectedIndex = 0
                chkLockInvolvedLamps.Checked = .LockInvolvedLamps
                chkHideScoreDisplays.Checked = .HideScoreDisplays
                chkBringToFront.Checked = .BringToFront
                chkRandomStart.Checked = .RandomStart
                numRandomQuality.Value = If(.RandomQuality > 0, .RandomQuality, Nothing)
                ResetStepsFields()
                PopulateSteps()
            End With
            EnableAll()
        End If
    End Sub
    Private Sub NewAnimation_Click(sender As System.Object, e As System.EventArgs) Handles btnNewAnimation.Click
        currentAnimationStep = Nothing
        currentAnimationHeader = New Animation.AnimationHeader()
        Backglass.currentAnimations.Add(currentAnimationHeader)
        PopulateAnimations()
        PopulateSteps()
        cmbAnimations.SelectedIndex = 0
        cmbLightsStateAtStart.SelectedIndex = cmbLightsStateAtStart.Items.Count - 1
        cmbLightsStateAtEnd.SelectedIndex = 0
        cmbAnimationStopBehaviour.SelectedIndex = 0
        ResetStepsFields()
        txtName.Focus()
        Backglass.currentData.IsDirty = True
    End Sub
    Private Sub RemoveAnimation_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveAnimation.Click
        If currentAnimationHeader IsNot Nothing AndAlso cmbAnimations.SelectedIndex >= 0 Then
            If MessageBox.Show(String.Format(My.Resources.MSG_RemoveAnimation, txtName.Text), AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim index As Integer = cmbAnimations.SelectedIndex
                Backglass.currentAnimations.Remove(currentAnimationHeader)
                currentAnimationHeader = Nothing
                currentAnimationStep = Nothing
                PopulateAnimations()
                PopulateSteps()
                cmbAnimations.SelectedIndex = If(index < cmbAnimations.Items.Count, index, index - 1)
                ResetStepsFields()
                Backglass.currentData.IsDirty = True
            End If
        End If
    End Sub

    Private Sub Name_Validated(sender As System.Object, e As System.EventArgs) Handles txtName.Validated
        Dim currentName As String = txtName.Text
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.Name = txtName.Text
            PopulateAnimations()
            Backglass.currentData.IsDirty = True
        End If
        cmbAnimations.SelectedItem = cmbGetItem(currentName)
    End Sub
    Private Sub DualMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDualMode.SelectedIndexChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.DualMode = cmbDualMode.SelectedIndex
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub Interval_Validated(sender As System.Object, e As System.EventArgs) Handles txtInterval.Validated
        If currentAnimationHeader IsNot Nothing Then
            If Not IsNumeric(txtInterval.Text) Then txtInterval.Text = "50"
            currentAnimationHeader.Interval = CInt(txtInterval.Text)
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub Loops_Validated(sender As System.Object, e As System.EventArgs) Handles txtLoops.Validated
        If currentAnimationHeader IsNot Nothing Then
            If Not IsNumeric(txtLoops.Text) Then txtLoops.Text = "1"
            currentAnimationHeader.Loops = CInt(txtLoops.Text)
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub IDJoin_Validated(sender As System.Object, e As System.EventArgs) Handles txtIDJoin.Validated
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.IDJoin = txtIDJoin.Text.Trim
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub Autostart_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAutostart.CheckedChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.StartAnimationAtBackglassStartup = chkAutostart.Checked
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub LightsStateAtStart_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbLightsStateAtStart.SelectedIndexChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.LightsStateAtAnimationStart = cmbLightsStateAtStart.SelectedIndex + 1
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub LightsStateAtEnd_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbLightsStateAtEnd.SelectedIndexChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.LightsStateAtAnimationEnd = cmbLightsStateAtEnd.SelectedIndex + 1
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub AnimationStopBehaviour_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbAnimationStopBehaviour.SelectedIndexChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.AnimationStopBehaviour = cmbAnimationStopBehaviour.SelectedIndex + 1
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub LockInvolvedLamps_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLockInvolvedLamps.CheckedChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.LockInvolvedLamps = chkLockInvolvedLamps.Checked
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub HideScoreDisplays_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkHideScoreDisplays.CheckedChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.HideScoreDisplays = chkHideScoreDisplays.Checked
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub BringToFront_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBringToFront.CheckedChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.BringToFront = chkBringToFront.Checked
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub RandomStart_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRandomStart.CheckedChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.RandomStart = chkRandomStart.Checked
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub RandomQuality_ValueChanged(sender As Object, e As System.EventArgs) Handles numRandomQuality.ValueChanged
        If currentAnimationHeader IsNot Nothing Then
            currentAnimationHeader.RandomQuality = numRandomQuality.Value
            Backglass.currentData.IsDirty = True
        End If
    End Sub

    Private Sub btnShowAnimation_Click(sender As System.Object, e As System.EventArgs) Handles btnShowAnimation.Click
        RaiseEvent ShowAnimation(Me, New ShowAnimationEventArgs(txtName.Text))
    End Sub

    Private Sub Steps_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvwSteps.SelectedIndexChanged
        If lvwSteps.SelectedItems.Count > 0 Then
            With lvwSteps.SelectedItems(0)
                currentAnimationStep = lvwGetItem(CInt(.SubItems(0).Text))
                txtLampsOn.Text = .SubItems(1).Text
                txtWaitLoopsAfterOn.Text = .SubItems(2).Text
                txtLampsOff.Text = .SubItems(3).Text
                txtWaitLoopsAfterOff.Text = .SubItems(4).Text
                txtPulseSwitch.Text = If(IsNumeric(.SubItems(5).Text) AndAlso CInt(.SubItems(5).Text) > 0, .SubItems(5).Text, String.Empty)
            End With
        End If
    End Sub

    Private Sub NewStep_Click(sender As System.Object, e As System.EventArgs) Handles btnNewStep.Click
        Dim waiton As Integer = 0
        Dim waitoff As Integer = 0
        If currentAnimationStep IsNot Nothing Then
            waiton = currentAnimationStep.WaitLoopsAfterOn
            waitoff = currentAnimationStep.WaitLoopsAfterOff
        End If
        currentAnimationStep = New Animation.AnimationStep
        currentAnimationStep.Step = lvwSteps.Items.Count + 1
        currentAnimationStep.WaitLoopsAfterOn = waiton
        currentAnimationStep.WaitLoopsAfterOff = waitoff
        currentAnimationHeader.AnimationSteps.Add(currentAnimationStep)
        PopulateSteps()
        On Error Resume Next
        lvwSteps.Items(lvwSteps.Items.Count - 1).Selected = True
        If lvwSteps.SelectedItems.Count = 1 Then lvwSteps.SelectedItems(0).EnsureVisible()
        txtLampsOn.Focus()
        Backglass.currentData.IsDirty = True
    End Sub
    Private Sub RemoveStep_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveStep.Click
        If currentAnimationStep IsNot Nothing AndAlso lvwSteps.SelectedItems.Count > 0 Then
            currentAnimationHeader.AnimationSteps.Remove(currentAnimationStep)
            currentAnimationStep = Nothing
            Dim currentStep As Integer = lvwSteps.SelectedIndices(0)
            lvwSteps.Items.Remove(lvwSteps.SelectedItems(0))
            RenumberSteps()
            PopulateSteps()
            On Error Resume Next
            lvwSteps.Items(currentStep - 1).Selected = True
            lvwSteps.Items(currentStep).Selected = True
            If lvwSteps.SelectedItems.Count = 1 Then lvwSteps.SelectedItems(0).EnsureVisible()
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub StepUp_Click(sender As System.Object, e As System.EventArgs) Handles btnStepUp.Click
        lvwItemUp()
        Backglass.currentData.IsDirty = True
    End Sub
    Private Sub StepDown_Click(sender As System.Object, e As System.EventArgs) Handles btnStepDown.Click
        lvwItemDown()
        Backglass.currentData.IsDirty = True
    End Sub

    Private Sub LampsOn_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLampsOn.TextChanged
        If currentAnimationStep IsNot Nothing Then
            lvwSetSubItem(1, txtLampsOn.Text)
            currentAnimationStep.On = txtLampsOn.Text
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub AllNames4LampsOn_Click(sender As System.Object, e As System.EventArgs) Handles btnAllNames4LampsOn.Click
        txtLampsOn.Text = GetAllNamedBulbs()
    End Sub
    Private Sub WaitLoopsAfterOn_Validated(sender As System.Object, e As System.EventArgs) Handles txtWaitLoopsAfterOn.Validated
        If currentAnimationStep IsNot Nothing Then
            If Not IsNumeric(txtWaitLoopsAfterOn.Text) Then txtWaitLoopsAfterOn.Text = "0"
        End If
    End Sub
    Private Sub WaitLoopsAfterOn_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtWaitLoopsAfterOn.TextChanged
        If currentAnimationStep IsNot Nothing Then
            lvwSetSubItem(2, txtWaitLoopsAfterOn.Text)
            currentAnimationStep.WaitLoopsAfterOn = If(IsNumeric(txtWaitLoopsAfterOn.Text), CInt(txtWaitLoopsAfterOn.Text), 1)
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub LampsOff_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLampsOff.TextChanged
        If currentAnimationStep IsNot Nothing Then
            lvwSetSubItem(3, txtLampsOff.Text)
            currentAnimationStep.Off = txtLampsOff.Text
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub AllLamps4LampsOff_Click(sender As System.Object, e As System.EventArgs) Handles btnAllLamps4LampsOff.Click
        txtLampsOff.Text = GetAllNamedBulbs()
    End Sub
    Private Sub WaitLoopsAfterOff_Validated(sender As System.Object, e As System.EventArgs) Handles txtWaitLoopsAfterOff.Validated
        If currentAnimationStep IsNot Nothing Then
            If Not IsNumeric(txtWaitLoopsAfterOff.Text) Then txtWaitLoopsAfterOff.Text = "0"
        End If
    End Sub
    Private Sub WaitLoopsAfterOff_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtWaitLoopsAfterOff.TextChanged
        If currentAnimationStep IsNot Nothing Then
            lvwSetSubItem(4, txtWaitLoopsAfterOff.Text)
            currentAnimationStep.WaitLoopsAfterOff = If(IsNumeric(txtWaitLoopsAfterOff.Text), CInt(txtWaitLoopsAfterOff.Text), 1)
            Backglass.currentData.IsDirty = True
        End If
    End Sub
    Private Sub PulseSwitch_Validated(sender As System.Object, e As System.EventArgs) Handles txtPulseSwitch.Validated
        If currentAnimationStep IsNot Nothing Then
            If Not IsNumeric(txtPulseSwitch.Text) OrElse CInt(txtPulseSwitch.Text) <= 0 Then txtPulseSwitch.Text = String.Empty
        End If
    End Sub
    Private Sub PulseSwitch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPulseSwitch.TextChanged
        If currentAnimationStep IsNot Nothing Then
            lvwSetSubItem(5, txtPulseSwitch.Text)
            currentAnimationStep.PulseSwitch = If(IsNumeric(txtPulseSwitch.Text) AndAlso CInt(txtPulseSwitch.Text) > 0, CInt(txtPulseSwitch.Text), Nothing)
            Backglass.currentData.IsDirty = True
        End If
    End Sub

    Private Sub PopulateAnimations()
        cmbAnimations.Items.Clear()
        ' add animations
        For Each item As Animation.AnimationHeader In Backglass.currentAnimations
            cmbAnimations.Items.Add(item)
        Next
    End Sub
    Private Sub PopulateSteps()
        lvwSteps.Items.Clear()
        ' add steps
        If currentAnimationHeader IsNot Nothing Then
            For Each item As Animation.AnimationStep In currentAnimationHeader.AnimationSteps
                With item
                    lvwAddItem(New String() {.Step.ToString(), .On, .WaitLoopsAfterOn.ToString(), .Off, .WaitLoopsAfterOff.ToString(), If((.PulseSwitch > 0), .PulseSwitch.ToString(), String.Empty)})
                End With
            Next
        End If
    End Sub

    Private Function cmbGetItem(ByVal text As String) As Animation.AnimationHeader
        Dim ret As Animation.AnimationHeader = Nothing
        For Each item As Animation.AnimationHeader In cmbAnimations.Items
            If item.Name.Equals(text) Then
                ret = item
                Exit For
            End If
        Next
        Return ret
    End Function

    Private Sub lvwAddItem(ByVal ParamArray Text() As String)
        With lvwSteps.Items
            .Add(New ListViewItem(Text))
        End With
    End Sub
    Private Function lvwGetItem(ByVal stepno As Integer) As Animation.AnimationStep
        Dim ret As Animation.AnimationStep = Nothing
        For Each item As Animation.AnimationStep In currentAnimationHeader.AnimationSteps
            If item.Step.Equals(stepno) Then
                ret = item
                Exit For
            End If
        Next
        Return ret
    End Function
    Private Sub lvwSetSubItem(ByVal subitemindex As Integer, ByVal text As String)
        If lvwSteps.SelectedItems.Count > 0 Then
            lvwSteps.SelectedItems(0).SubItems(subitemindex).Text = text
        End If
    End Sub
    Private Sub lvwItemUp()
        If lvwSteps.SelectedItems.Count > 0 Then
            Dim index As Integer = lvwSteps.SelectedIndices(0)
            If index > 0 Then
                'Dim item As ListViewItem = lvwSteps.SelectedItems(0)
                'lvwSteps.Items.Remove(item)
                'lvwSteps.Items.Insert(index - 1, item)
                'item.Selected = True
                'item.SubItems(0).Text = index.ToString()
                'lvwSteps.Items(index).SubItems(0).Text = (index + 1).ToString()
                Dim item As Animation.AnimationStep = currentAnimationHeader.AnimationSteps(index)
                currentAnimationHeader.AnimationSteps.RemoveAt(index)
                currentAnimationHeader.AnimationSteps.Insert(index - 1, item)
                RenumberSteps()
                PopulateSteps()
                On Error Resume Next
                lvwSteps.Items(index - 1).Selected = True
                If lvwSteps.SelectedItems.Count = 1 Then lvwSteps.SelectedItems(0).EnsureVisible()
            End If
        End If
    End Sub
    Private Sub lvwItemDown()
        If lvwSteps.SelectedItems.Count > 0 Then
            Dim index As Integer = lvwSteps.SelectedIndices(0)
            If index < lvwSteps.Items.Count - 1 Then
                'Dim item As ListViewItem = lvwSteps.SelectedItems(0)
                'lvwSteps.Items.Remove(item)
                'lvwSteps.Items.Insert(index + 1, item)
                'item.Selected = True
                'item.SubItems(0).Text = (index + 2).ToString()
                'lvwSteps.Items(index).SubItems(0).Text = (index + 1).ToString()
                Dim item As Animation.AnimationStep = currentAnimationHeader.AnimationSteps(index)
                currentAnimationHeader.AnimationSteps.RemoveAt(index)
                currentAnimationHeader.AnimationSteps.Insert(index + 1, item)
                RenumberSteps()
                PopulateSteps()
                On Error Resume Next
                lvwSteps.Items(index + 1).Selected = True
                If lvwSteps.SelectedItems.Count = 1 Then lvwSteps.SelectedItems(0).EnsureVisible()
            End If
        End If
    End Sub

    Private Sub RenumberSteps()
        Dim i As Integer = 1
        For Each animationstep As Animation.AnimationStep In currentAnimationHeader.AnimationSteps
            animationstep.Step = i
            i += 1
        Next
    End Sub

    Private Sub EnableAll()
        txtName.Enabled = True
        cmbDualMode.Enabled = Backglass.currentData.DualBackglass
        txtInterval.Enabled = True
        txtLoops.Enabled = True
        txtIDJoin.Enabled = True
        chkAutostart.Enabled = True
        cmbLightsStateAtStart.Enabled = True
        cmbLightsStateAtEnd.Enabled = True
        cmbAnimationStopBehaviour.Enabled = True
        chkLockInvolvedLamps.Enabled = True
        chkHideScoreDisplays.Enabled = True
        chkBringToFront.Enabled = True
        chkRandomStart.Enabled = True
        numRandomQuality.Enabled = True
        btnShowAnimation.Enabled = True
        btnNewStep.Enabled = True
        btnRemoveStep.Enabled = True
        btnStepUp.Enabled = True
        btnStepDown.Enabled = True
    End Sub

    Private Sub ResetStepsFields()
        txtLampsOn.Text = String.Empty
        txtWaitLoopsAfterOn.Text = String.Empty
        txtLampsOff.Text = String.Empty
        txtWaitLoopsAfterOff.Text = String.Empty
        txtPulseSwitch.Text = String.Empty
    End Sub

    Private Function GetAllNamedBulbs() As String
        Dim ret As String = String.Empty
        Dim list As Generic.SortedList(Of String, Boolean) = New Generic.SortedList(Of String, Boolean)
        For index As Integer = Backglass.currentBulbs.Count - 1 To 0 Step -1
            With Backglass.currentBulbs(index)
                If String.IsNullOrEmpty(Backglass.currentTabPage.RomInfoFilter) OrElse
                    Backglass.currentTabPage.RomInfoFilter.Equals(.B2SInfo2String) OrElse
                    Backglass.currentTabPage.RomInfoFilter.Equals(.RomInfo2String) OrElse
                    (Backglass.currentTabPage.RomInfoFilter.Equals("withoutid") AndAlso ((Backglass.currentData.CommType = eCommType.B2S AndAlso String.IsNullOrEmpty(.B2SInfo2String)) OrElse (Backglass.currentData.CommType = eCommType.Rom AndAlso String.IsNullOrEmpty(.RomInfo2String)))) OrElse
                    (Backglass.currentTabPage.RomInfoFilter.Equals("withname") AndAlso Not String.IsNullOrEmpty(.Name)) OrElse
                    (Backglass.currentTabPage.RomInfoFilter.Equals("off") AndAlso .InitialState = 0) OrElse
                    (Backglass.currentTabPage.RomInfoFilter.Equals("on") AndAlso .InitialState = 1) OrElse
                    (Backglass.currentTabPage.RomInfoFilter.Equals("alwayson") AndAlso .InitialState = 2) OrElse
                    (Backglass.currentTabPage.RomInfoFilter.Equals("authentic") AndAlso .DualMode <> eDualMode.Fantasy) OrElse
                    (Backglass.currentTabPage.RomInfoFilter.Equals("fantasy") AndAlso .DualMode <> eDualMode.Authentic) Then
                    If Not String.IsNullOrEmpty(.Name) AndAlso Not list.ContainsKey(.Name) Then
                        list.Add(.Name, True)
                    End If
                End If
            End With
        Next
        If list.Count > 0 Then
            Dim sb As StringBuilder = New StringBuilder()
            For Each name As KeyValuePair(Of String, Boolean) In list
                sb.Append(",")
                sb.Append(name.Key)
            Next
            If sb.Length > 0 Then
                sb.Remove(0, 1)
                ret = sb.ToString()
            End If
        End If
        Return ret
    End Function

End Class