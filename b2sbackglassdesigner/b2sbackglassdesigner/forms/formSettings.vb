Imports System

Public Class formSettings

    Private IsDirty As Boolean = False

    Public Shadows Function ShowDialog(ByVal owner As IWin32Window,
                                       ByVal newtable As Boolean,
                                       ByRef name As String,
                                       ByRef filename As String,
                                       ByRef dualbackglass As Boolean,
                                       ByRef author As String,
                                       ByRef artwork As String,
                                       ByRef tabletype As eTableType,
                                       ByRef addemdefaults As Boolean,
                                       ByRef numberofplayers As Integer,
                                       ByRef b2sdatacount As Integer,
                                       ByRef dmdtype As eDMDType,
                                       ByRef commtype As eCommType,
                                       ByRef desttype As eDestType) As DialogResult
        ' set not null columns to red
        Name_TextChanged(txtName, New EventArgs())
        FileName_TextChanged(txtFileName, New EventArgs())
        TableType_SelectedIndexChanged(cmbTableType, New EventArgs())
        ' show dialog
        txtName.Text = name
        txtFileName.Text = filename
        chkDualBackglass.Checked = dualbackglass
        txtAuthor.Text = author
        txtArtwork.Text = artwork
        If tabletype = eTableType.NotDefined Then
            cmbTableType.Text = String.Empty
        Else
            cmbTableType.SelectedIndex = tabletype
        End If
        chkCreateEMDefaults.Checked = addemdefaults
        cmbNumberOfPlayers.SelectedIndex = numberofplayers
        cmbB2SDataCount.SelectedIndex = b2sdatacount
        cmbDMDLocation.SelectedIndex = dmdtype
        If commtype = eCommType.NotDefined Then
            cmbCommMode.Text = String.Empty
        Else
            cmbCommMode.SelectedIndex = commtype
        End If
        If desttype = eDestType.NotDefined Then
            cmbDestType.Text = String.Empty
            If Not cmbDestType.Enabled AndAlso cmbCommMode.SelectedIndex = 1 Then cmbDestType.SelectedIndex = 1
        Else
            cmbDestType.SelectedIndex = desttype
        End If
        ' maybe lock some fields
        'txtName.Enabled = newtable
        ' open dialog
        Dim ret As DialogResult = MyBase.ShowDialog(owner)
        If ret = Windows.Forms.DialogResult.OK Then
            ' return some values
            name = txtName.Text
            filename = txtFileName.Text
            dualbackglass = chkDualBackglass.Checked
            author = txtAuthor.Text
            artwork = txtArtwork.Text
            tabletype = cmbTableType.SelectedIndex
            If String.IsNullOrEmpty(cmbTableType.Text) Then tabletype = eTableType.NotDefined
            addemdefaults = chkCreateEMDefaults.Checked
            numberofplayers = CInt(cmbNumberOfPlayers.Text)
            b2sdatacount = CInt(cmbB2SDataCount.Text)
            dmdtype = cmbDMDLocation.SelectedIndex
            commtype = cmbCommMode.SelectedIndex
            desttype = cmbDestType.SelectedIndex
        End If
        Return ret
    End Function

    Private Sub formSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsDirty = False
    End Sub
    Private Sub formSettings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        IsDirty = False
        txtName.Focus()
    End Sub

    Private Sub Name_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        IsDirty = True
        lblName.ForeColor = If(String.IsNullOrEmpty(sender.Text), Color.Red, Color.FromKnownColor(KnownColor.ControlText))
    End Sub
    Private Sub Name_Validated(sender As Object, e As System.EventArgs) Handles txtName.Validated
        txtName.Text = txtName.Text.Replace("\", "")
        If (Backglass.currentData Is Nothing OrElse Not Backglass.currentData.Name.Equals(txtName.Text)) AndAlso IO.File.Exists(IO.Path.Combine(BackglassProjectsPath, txtName.Text & ".b2s")) Then
            btnOk.Enabled = False
            MessageBox.Show(My.Resources.MSG_ProjectNameIsAlreadyUsed, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtName.Focus()
            Exit Sub
        End If
        btnOk.Enabled = True
    End Sub
    Private Sub FileName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFileName.TextChanged
        IsDirty = True
        lblFileName.ForeColor = If(String.IsNullOrEmpty(sender.Text), Color.Red, Color.FromKnownColor(KnownColor.ControlText))
        If txtFileName.Text.ToUpper.EndsWith("B2S") Then
            btnOk.Enabled = False
            MessageBox.Show(My.Resources.MSG_ProjectNameEndsWithB2S, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtFileName.Focus()
            Exit Sub
        End If
        btnOk.Enabled = True
    End Sub
    Private Sub FileName_Click(sender As System.Object, e As System.EventArgs) Handles btnFileName.Click
        Dim ofd As OpenFileDialog = New OpenFileDialog()
        ofd.InitialDirectory = DefaultVPTablesFolder
        ofd.Filter = "Visual Pinball tables (*.vpt)|*.vpt"
        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim filename As String = FileIO.FileSystem.GetName(ofd.FileName)
            If filename.ToLower.EndsWith(".vpt") Then
                txtFileName.Text = filename.Substring(0, filename.Length - 4)
                DefaultVPTablesFolder = (New IO.FileInfo(ofd.FileName)).DirectoryName
            End If
        End If
    End Sub
    Private Sub TableType_TextChanged(sender As Object, e As System.EventArgs) Handles cmbTableType.TextChanged
        IsDirty = True
        lblTableType.ForeColor = If(String.IsNullOrEmpty(sender.Text), Color.Red, Color.FromKnownColor(KnownColor.ControlText))
    End Sub
    Private Sub TableType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTableType.SelectedIndexChanged
        IsDirty = True
        lblTableType.ForeColor = If(String.IsNullOrEmpty(sender.Text), Color.Red, Color.FromKnownColor(KnownColor.ControlText))
        chkCreateEMDefaults.Enabled = (cmbTableType.SelectedIndex = 1 AndAlso cmbDestType.SelectedIndex = 2)
        If Not chkCreateEMDefaults.Enabled Then chkCreateEMDefaults.Checked = False
        If cmbTableType.SelectedIndex = 0 Then
            cmbCommMode.SelectedIndex = 0
            cmbDestType.SelectedIndex = 0
        ElseIf cmbTableType.SelectedIndex = 1 OrElse cmbTableType.SelectedIndex = 4 Then
            cmbCommMode.SelectedIndex = 2
        Else
            cmbCommMode.SelectedIndex = 1
        End If
    End Sub

    Private Sub NumberOfPlayers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNumberOfPlayers.SelectedIndexChanged
        IsDirty = True
    End Sub

    Private Sub CommMode_TextChanged(sender As Object, e As System.EventArgs) Handles cmbCommMode.TextChanged
        IsDirty = True
        lblCommMode.ForeColor = If(String.IsNullOrEmpty(sender.Text), Color.Red, Color.FromKnownColor(KnownColor.ControlText))
    End Sub
    Private Sub CommMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbCommMode.SelectedIndexChanged
        IsDirty = True
        cmbB2SDataCount.Enabled = (cmbCommMode.SelectedIndex = 2 AndAlso cmbDestType.SelectedIndex = 2)
        If cmbCommMode.SelectedIndex = 1 Then
            cmbDestType.Enabled = False
            cmbDestType.SelectedIndex = 1
        Else
            cmbDestType.Enabled = True
            cmbDestType.SelectedIndex = 1
        End If
    End Sub
    Private Sub DestType_TextChanged(sender As Object, e As System.EventArgs) Handles cmbDestType.TextChanged
        IsDirty = True
        lblDestType.ForeColor = If(String.IsNullOrEmpty(sender.Text), Color.Red, Color.FromKnownColor(KnownColor.ControlText))
    End Sub
    Private Sub DestType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDestType.SelectedIndexChanged
        IsDirty = True
        chkCreateEMDefaults.Enabled = (cmbTableType.SelectedIndex = 1 AndAlso cmbDestType.SelectedIndex = 2)
        If Not chkCreateEMDefaults.Enabled Then chkCreateEMDefaults.Checked = False
        cmbB2SDataCount.Enabled = (cmbCommMode.SelectedIndex = 2 AndAlso cmbDestType.SelectedIndex = 2)
    End Sub
    Private Sub CreateEMDefaults_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCreateEMDefaults.CheckedChanged
        IsDirty = True
        If chkCreateEMDefaults.Checked Then
            cmbB2SDataCount.SelectedIndex = 36
        End If
    End Sub
    Private Sub B2SDataCount_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbB2SDataCount.SelectedIndexChanged
        IsDirty = True
    End Sub
    Private Sub DMDLocation_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDMDLocation.SelectedIndexChanged
        IsDirty = True
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If String.IsNullOrEmpty(txtName.Text) OrElse String.IsNullOrEmpty(txtFileName.Text) OrElse String.IsNullOrEmpty(cmbTableType.Text) OrElse String.IsNullOrEmpty(cmbCommMode.Text) OrElse String.IsNullOrEmpty(cmbDestType.Text) Then
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