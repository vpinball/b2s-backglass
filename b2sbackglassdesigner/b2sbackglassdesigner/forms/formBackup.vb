Imports System

Public Class formBackup

    Private IsDirty As Boolean = False

    Public Shadows Function ShowDialog(ByVal owner As IWin32Window, _
                                       ByRef name As String) As DialogResult
        ' open dialog
        Dim ret As DialogResult = MyBase.ShowDialog(owner)
        If ret = Windows.Forms.DialogResult.OK Then
            ' return some values
            name = txtName.Text
        End If
        Return ret
    End Function

    Private Sub formBackup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        IsDirty = False
    End Sub
    Private Sub formBackup_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        IsDirty = False
        txtName.Focus()
    End Sub

    Private Sub txtName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtName.TextChanged
        IsDirty = True
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If String.IsNullOrEmpty(txtName.Text) Then
            MessageBox.Show(My.Resources.MSG_EnterBackglassName, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

End Class