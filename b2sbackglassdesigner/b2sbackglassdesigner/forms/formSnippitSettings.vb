Imports System

Public Class formSnippitSettings

    Public ignoreChange As Boolean = False

    Private IsDirty As Boolean = False
    Private bulbID As Integer = -1

    Public Shadows Function ShowDialog(ByVal owner As IWin32Window,
                                       ByVal id As Integer,
                                       ByRef name As String,
                                       ByRef snippittype As eSnippitType,
                                       ByRef zorder As Integer,
                                       ByRef mechid As Integer,
                                       ByRef rotatingsteps As Integer,
                                       ByRef rotatinginterval As Integer,
                                       ByRef rotatingdirection As eSnippitRotationDirection,
                                       ByRef rotationstopping As eSnippitRotationStopBehaviour) As DialogResult

        ignoreChange = True
        ' set starting values
        bulbID = id
        txtName.Text = name
        cmbType.SelectedIndex = snippittype
        If zorder < 0 Or zorder > 9 Then zorder = 0
        numericZOrder.Value = zorder
        If mechid < numericMechID.Minimum OrElse mechid > numericMechID.Maximum Then mechid = numericMechID.Minimum
        numericMechID.Value = mechid
        If rotatingsteps < numericRotatingSteps.Minimum Then rotatingsteps = numericRotatingSteps.Minimum
        If rotatingsteps > numericRotatingSteps.Maximum Then rotatingsteps = numericRotatingSteps.Maximum
        numericRotatingSteps.Value = rotatingsteps
        If rotatinginterval < numericRotatingInterval.Minimum Then rotatinginterval = numericRotatingInterval.Minimum
        If rotatinginterval > numericRotatingInterval.Maximum Then rotatinginterval = numericRotatingInterval.Maximum
        numericRotatingInterval.Value = rotatinginterval
        If rotatingdirection < eSnippitRotationDirection.Clockwise OrElse rotatingdirection > eSnippitRotationDirection.AntiClockwise Then rotatingdirection = eSnippitRotationDirection.Clockwise
        cmbRotatingDirection.SelectedIndex = rotatingdirection
        If rotationstopping < eSnippitRotationStopBehaviour.SpinOff OrElse rotationstopping > eSnippitRotationStopBehaviour.RunAnimationToFirstStep Then rotationstopping = eSnippitRotationStopBehaviour.SpinOff
        cmbRotationStopBehaviour.SelectedIndex = rotationstopping
        ignoreChange = False
        EnableDisable()
        
        ' open dialog
        Dim ret As DialogResult = MyBase.ShowDialog(owner)
        If ret = Windows.Forms.DialogResult.OK Then
            ' return some values
            name = txtName.Text
            snippittype = cmbType.SelectedIndex
            zorder = numericZOrder.Value
            mechid = numericMechID.Value
            rotatingsteps = numericRotatingSteps.Value
            rotatinginterval = numericRotatingInterval.Value
            rotatingdirection = cmbRotatingDirection.SelectedIndex
            rotationstopping = cmbRotationStopBehaviour.SelectedIndex
        End If

        Return ret

    End Function

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If String.IsNullOrEmpty(txtName.Text) OrElse String.IsNullOrEmpty(cmbType.Text) Then
            MessageBox.Show(My.Resources.MSG_EnterSnippitName, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtName.Focus()
            Return
        End If
        MyBase.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If IsDirty Then
            Dim ret As DialogResult = MessageBox.Show(My.Resources.MSG_IsDirty, AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If ret = Windows.Forms.DialogResult.Yes Then
                btnOk.PerformClick()
            ElseIf ret = Windows.Forms.DialogResult.No Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private currentType As eSnippitType = eSnippitType.StandardImage
    Private Sub Type_Enter(sender As Object, e As System.EventArgs) Handles cmbType.Enter
        currentType = cmbType.SelectedIndex
    End Sub
    Private Sub Type_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        If ignoreChange Then Return
        If cmbType.SelectedIndex = 1 Then
            If IsThereAlreadyOneSelfRotatingSnippit(bulbID) Then
                MessageBox.Show(My.Resources.MSG_ThereIsAlreadyOneSelfRotatingSnippit, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If currentType <> eSnippitType.SelfRotatingImage Then
                    cmbType.SelectedIndex = currentType
                Else
                    cmbType.SelectedIndex = eSnippitType.StandardImage
                End If
            End If
        End If
        EnableDisable()
    End Sub

    Private Sub formSnippitSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsDirty = False
    End Sub
    Private Sub formSnippitSettings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        IsDirty = False
        txtName.Focus()
    End Sub

    Private Sub EnableDisable()
        numericMechID.Enabled = (cmbType.SelectedIndex = 2)
        numericRotatingSteps.Enabled = (cmbType.SelectedIndex <> 0)
        numericRotatingInterval.Enabled = (cmbType.SelectedIndex = 1)
        cmbRotatingDirection.Enabled = (cmbType.SelectedIndex <> 0)
        cmbRotationStopBehaviour.Enabled = (cmbType.SelectedIndex = 1)
    End Sub

End Class