Imports System
Imports Microsoft.Win32
Imports System.Text

Public Class formVPM

    Private b2sserver As Object

    Private noChanges As Boolean = False

    Private ReadOnly Property isVPinMAMEBackglass() As Boolean
        Get
            Return (Backglass.currentData IsNot Nothing AndAlso Backglass.currentData.CommType <> eCommType.B2S)
        End Get
    End Property

    Private Class statistic
        Public Count0 As Integer = 0
        Public Count1 As Integer = 0
        Public Count2 As Integer = 0
        Public Count3 As Integer = 0
        Public Count4 As Integer = 0
        Public Count5 As Integer = 0
        Public Count6 As Integer = 0
        Public Count7 As Integer = 0
        Public Count8 As Integer = 0
    End Class
    Private stats As Generic.SortedList(Of String, statistic) = New Generic.SortedList(Of String, statistic)

    Private Sub formVPM_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If isVPinMAMEBackglass Then
            LoadROMs()
            cmbROMName.Text = Backglass.currentData.GameName
        Else
            cmbROMName.Text = String.Empty
        End If
        cmbROMName.Enabled = isVPinMAMEBackglass
        chkDoubleSize.Enabled = isVPinMAMEBackglass
        chkShowDMDOnly.Enabled = isVPinMAMEBackglass
        chkShowFrame.Enabled = isVPinMAMEBackglass
        chkShowTitle.Enabled = isVPinMAMEBackglass

        txtVPTablesFolder.Text = DefaultVPTablesFolder
        chkCopyBackglassFileToVPTablesFolder.Checked = True

        If Registry.CurrentUser.OpenSubKey("Software") Is Nothing Then Registry.CurrentUser.CreateSubKey("Software")
        If Registry.CurrentUser.OpenSubKey("Software\B2S") Is Nothing Then Registry.CurrentUser.CreateSubKey("Software\B2S")
        If Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME") Is Nothing Then Registry.CurrentUser.CreateSubKey("Software\B2S\VPinMAME")
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
            regkey.SetValue("Info", "")
        End Using

        TimerInfos.Interval = 97
        TimerInfos.Start()

    End Sub
    Private Sub formVPM_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

        If isVPinMAMEBackglass Then
            cmbROMName.Focus()
        Else
            btnStart.Focus()
        End If

    End Sub
    Private Sub formVPM_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        TimerInfos.Stop()

        For i As Integer = Process.GetProcessesByName("B2SVPinMAMEStarter").Count - 1 To 0 Step -1
            Process.GetProcessesByName("B2SVPinMAMEStarter")(i).Kill()
        Next

        btnStop.PerformClick()

        'btnStart.Enabled = True
        'btnStop.Enabled = False
        'btnPause.Enabled = False
        'btnResume.Enabled = False

        Me.Dispose()

    End Sub

    Private Sub TimerInfos_Tick(sender As System.Object, e As System.EventArgs) Handles TimerInfos.Tick

        If isVPinMAMEBackglass Then

            Dim regdata As String = String.Empty
            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
                regdata = regkey.GetValue("Info")
                regkey.SetValue("Info", "")
            End Using

            Me.SuspendLayout()

            If Not String.IsNullOrEmpty(regdata) AndAlso txtInfo IsNot Nothing Then
                txtInfo.SuspendLayout()
                Dim sb As StringBuilder = New StringBuilder()
                sb.Append(txtInfo.Text)
                Dim infolines As String() = regdata.Split(";")
                For Each info As String In infolines
                    Dim infos As String() = info.Split(",")

                    If infos.Length = 3 Then

                        ' add text info
                        Select Case infos(0)
                            Case "L" : sb.Append("Lamp ")
                            Case "S" : sb.Append("Solenoid ")
                            Case "G" : sb.Append("G.I. string ")
                        End Select
                        sb.Append(infos(1))
                        sb.Append(" is set to ")
                        sb.AppendLine(infos(2))

                        ' collect data
                        Dim key As String = infos(0) & CInt(infos(1)).ToString("D4")
                        Dim statistic As statistic = Nothing
                        If Not stats.ContainsKey(key) Then
                            statistic = New statistic()
                            stats.Add(key, statistic)
                        Else
                            statistic = stats(key)
                        End If
                        Select Case infos(2)
                            Case "0" : statistic.Count0 += 1
                            Case "1" : statistic.Count1 += 1
                            Case "2" : statistic.Count2 += 1
                            Case "3" : statistic.Count3 += 1
                            Case "4" : statistic.Count4 += 1
                            Case "5" : statistic.Count5 += 1
                            Case "6" : statistic.Count6 += 1
                            Case "7" : statistic.Count7 += 1
                            Case "8" : statistic.Count8 += 1
                        End Select

                    End If

                Next
                txtInfo.Text = sb.ToString()
                txtInfo.SelectionStart = txtInfo.Text.Length
                txtInfo.ScrollToCaret()
                txtInfo.ResumeLayout()

                ' show stats
                Dim topitem As ListViewItem = lvwStats.TopItem
                lvwStats.SuspendLayout()
                'lvwStats.Items.Clear()
                For Each statistic As KeyValuePair(Of String, statistic) In stats
                    With statistic
                        Dim found As Boolean = False
                        For Each item As ListViewItem In lvwStats.Items
                            If item.SubItems(0).Text.Equals(.Key) Then
                                found = True
                                item.SubItems(3).Text = .Value.Count0.ToString()
                                item.SubItems(4).Text = .Value.Count1.ToString()
                                Exit For
                            End If
                        Next
                        If Not found Then
                            lvwStats.Items.Add(New ListViewItem(New String() {.Key, CheckType(.Key.Substring(0, 1)), CInt(.Key.Substring(1)).ToString(), .Value.Count0.ToString(), .Value.Count1.ToString()}))
                        End If

                        'lvwStats.Items.Add(New ListViewItem(New String() {.Key, CheckType(.Key.Substring(0, 1)), CInt(.Key.Substring(1)).ToString(), .Value.Count0.ToString(), .Value.Count1.ToString()}))

                    End With
                Next
                lvwStats.TopItem = topitem
                lvwStats.ResumeLayout()

            End If

            Me.ResumeLayout()

        End If

    End Sub

    Private Sub ROMName_TextChanged(sender As Object, e As System.EventArgs) Handles cmbROMName.TextChanged
        Static currentROM As String = String.Empty
        If cmbROMName.Items.Contains(cmbROMName.Text) AndAlso Not currentROM.Equals(cmbROMName.Text) Then
            currentROM = cmbROMName.Text
            Dim controller As Object = CreateObject("VPinMAME.Controller")
            controller.GameName = cmbROMName.Text
            noChanges = True
            chkShowTitle.Checked = controller.ShowTitle
            chkShowFrame.Checked = controller.ShowFrame
            chkShowDMDOnly.Checked = controller.ShowDMDOnly
            chkDoubleSize.Checked = controller.DoubleSize
            noChanges = False
            controller = Nothing
        End If
    End Sub
    Private Sub Start_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click

        If isVPinMAMEBackglass AndAlso String.IsNullOrEmpty(cmbROMName.Text) Then

            MessageBox.Show(My.Resources.MSG_SelectRomName, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbROMName.Focus()

        ElseIf String.IsNullOrEmpty(txtVPTablesFolder.Text) OrElse Not IO.Directory.Exists(txtVPTablesFolder.Text) Then

            MessageBox.Show(My.Resources.MSG_EnterValidVPTablesFolder, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtVPTablesFolder.Focus()

        Else

            Backglass.currentData.GameName = cmbROMName.Text

            If chkCopyBackglassFileToVPTablesFolder.Checked Then
                Try
                    Dim filename As String = IO.Path.Combine(ProjectPath, Backglass.currentData.VSName & ".directB2S")
                    IO.File.Copy(filename, IO.Path.Combine(txtVPTablesFolder.Text, Backglass.currentData.VSName & ".directB2S"), True)
                Catch ex As Exception
                    MessageBox.Show(String.Format(My.Resources.MSG_CopyError, ex.Message), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            If isVPinMAMEBackglass Then

                ' start the VPinMAME starter
                Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
                    regkey.SetValue("GameName", cmbROMName.Text)
                    regkey.SetValue("Mode", "")
                End Using

                Dim process As Process = New Process()
                process.StartInfo.FileName = "B2SVPinMAMEStarter.exe"
                process.StartInfo.Arguments = """" & Backglass.currentData.VSName & """ """ & txtVPTablesFolder.Text & """"
                process.Start()

                btnStart.Enabled = False
                btnStop.Enabled = True
                btnPause.Enabled = True
                btnResume.Enabled = True
                btnStop.Focus()

            Else

                ' start the backglass directly
                b2sserver = CreateObject("B2S.Server")
                b2sserver.TableName = Backglass.currentData.VSName
                b2sserver.WorkingDir = txtVPTablesFolder.Text
                b2sserver.Run()

                btnStart.Enabled = False
                btnStop.Enabled = True
                btnPause.Enabled = False
                btnResume.Enabled = False
                btnStop.Focus()

            End If

        End If

    End Sub
    Private Sub Stop_Click(sender As System.Object, e As System.EventArgs) Handles btnStop.Click

        If isVPinMAMEBackglass Then

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
                regkey.SetValue("Mode", "Unload")
            End Using

        Else

            Try
                b2sserver.Stop()
                b2sserver.Dispose()
            Catch
            Finally
                b2sserver = Nothing
            End Try

        End If

        btnStart.Enabled = True
        btnStop.Enabled = False
        btnPause.Enabled = False
        btnResume.Enabled = False
        btnStart.Focus()

    End Sub
    Private Sub Pause_Click(sender As System.Object, e As System.EventArgs) Handles btnPause.Click

        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
            regkey.SetValue("Mode", "Pause")
        End Using

        btnPause.Enabled = False
        btnResume.Focus()

    End Sub
    Private Sub Resume_Click(sender As System.Object, e As System.EventArgs) Handles btnResume.Click

        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
            regkey.SetValue("Mode", "Resume")
        End Using

        btnPause.Enabled = True
        btnPause.Focus()

    End Sub

    Private Sub ShowTitle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkShowTitle.CheckedChanged

        If noChanges Then Return
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
            regkey.SetValue("ShowTitle", If(chkShowTitle.Checked, "1", "0"))
        End Using

    End Sub
    Private Sub ShowFrame_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkShowFrame.CheckedChanged

        If noChanges Then Return
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
            regkey.SetValue("ShowFrame", If(chkShowFrame.Checked, "1", "0"))
        End Using

    End Sub
    Private Sub ShowDMDOnly_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkShowDMDOnly.CheckedChanged

        If noChanges Then Return
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
            regkey.SetValue("ShowDMDOnly", If(chkShowDMDOnly.Checked, "1", "0"))
        End Using

    End Sub
    Private Sub DoubleSize_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDoubleSize.CheckedChanged

        If noChanges Then Return
        Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
            regkey.SetValue("DoubleSize", If(chkDoubleSize.Checked, "1", "0"))
        End Using

    End Sub

    Private Sub VPTablesFolder_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVPTablesFolder.TextChanged

        DefaultVPTablesFolder = txtVPTablesFolder.Text

    End Sub
    Private Sub VPTablesFolder_Click(sender As System.Object, e As System.EventArgs) Handles btnVPTablesFolder.Click

        Using fbd As FolderBrowserDialog = New FolderBrowserDialog()
            fbd.SelectedPath = txtVPTablesFolder.Text
            If fbd.ShowDialog(Me) Then
                txtVPTablesFolder.Text = fbd.SelectedPath
                DefaultVPTablesFolder = txtVPTablesFolder.Text
            End If
        End Using

    End Sub

    Private Sub LoadROMs()

        Dim controller As Object = Nothing
        Try
            If cmbROMName.Items.Count = 0 Then
                controller = CreateObject("VPinMAME.Controller")
                For Each machine As String In controller.Machines
                    cmbROMName.Items.Add(machine)
                Next
            End If
        Catch ex As Exception
            ' nothing to do
        Finally
            controller = Nothing
        End Try

    End Sub

    Private Function CheckType(ByVal type As String) As String
        Return If(type = "G", "GI", type)
    End Function

End Class