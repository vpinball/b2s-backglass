Imports Microsoft.Win32
Imports System.IO
Imports System.Security.Principal

Public Class formBackglassServerRegApp


    Private Sub Form1_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        If Not IsAdmin() Then
            MessageBox.Show("You have to start this app as Administrator!", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If

        Dim CommandSilent As Boolean = False
        If My.Application.CommandLineArgs.Count > 0 Then
            CommandSilent = InStr(My.Application.CommandLineArgs(0).ToString().ToLower(), "silent", CompareMethod.Text) > 0
        End If
        Me.Visible = False

        Dim basepath As String = String.Empty
        Dim regasmpath As String = String.Empty
        Dim version As String = String.Empty
        Dim dialogResult As DialogResult
        Dim clsID As String = String.Empty

        If Not CommandSilent Then
            If CheckB2SServer(False) Then
                Dim dllURI As String = "file://Unknown"
                Try
                    Using regRoot As RegistryKey = Registry.ClassesRoot
                        Using openKey As RegistryKey = regRoot.OpenSubKey("B2S.Server\CLSID", False)
                            If openKey IsNot Nothing Then
                                clsID = openKey.GetValue("")
                            End If
                        End Using
                        If clsID IsNot String.Empty Then
                            Using openKey As RegistryKey = regRoot.OpenSubKey(IO.Path.Combine("CLSID", clsID, "InprocServer32"), False)
                                If openKey IsNot Nothing Then
                                    dllURI = openKey.GetValue("CodeBase")
                                End If
                            End Using
                        End If
                    End Using
                Catch
                End Try
                Dim filepath As String = New Uri(dllURI).LocalPath
                dialogResult = MessageBox.Show("The 'B2S Server' is already registered here:" & vbCrLf & vbCrLf & filepath & vbCrLf & vbCrLf & "Do you want to (try to) re-register it?", My.Application.Info.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            Else
                dialogResult = MessageBox.Show("The 'B2S Server' is not registered yet." & vbCrLf & vbCrLf & "Do you want to register it?", My.Application.Info.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            End If
        End If

        ' get .NET framework base directoy
        If CommandSilent Or dialogResult = DialogResult.Yes Then
            Using regkey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\.NetFramework", False)
                If regkey IsNot Nothing Then
                    basepath = regkey.GetValue("InstallRoot")
                End If
            End Using

            ' get version info
            If Not String.IsNullOrEmpty(basepath) Then
                Using regkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\.NetFramework\Policy\v4.0", False)
                    If regkey IsNot Nothing Then
                        version = "v4.0"
                        For Each valuename As String In regkey.GetValueNames()
                            Dim path As String = IO.Path.Combine(basepath, version & "." & valuename)
                            If IO.Directory.Exists(path) Then
                                regasmpath = path
                                Exit For
                            End If
                        Next
                        regkey.Close()
                    End If
                End Using
            End If

            ' check whether everything is fine
            If String.IsNullOrEmpty(basepath) Then
                MessageBox.Show("Error, no basepath found.")
            ElseIf String.IsNullOrEmpty(regasmpath) Then
                MessageBox.Show("Error, no regasmpath found.")
            Else
                ' cleanup earlier B2S.Server entries
                If clsID IsNot String.Empty Then
                    Using regRoot As RegistryKey = Registry.ClassesRoot
                        'Computer\HKEY_CLASSES_ROOT\B2S.Server
                        regRoot.DeleteSubKeyTree("B2S.Server", False)
                        'Computer\HKEY_CLASSES_ROOT\CLSID\<id>
                        regRoot.OpenSubKey("CLSID", True).DeleteSubKeyTree(clsID, False)
                        'Computer\HKEY_CLASSES_ROOT\WOW6432Node\CLSID\<id>
                        regRoot.OpenSubKey("WOW6432Node\CLSID", True).DeleteSubKeyTree(clsID, False)
                    End Using
                End If
                ' do the register operation
                ShellAndWait(regasmpath, "B2SBackglassServer.DLL")
                If IO.File.Exists("B2SBackglassServer64.DLL") Then
                    ShellAndWait(regasmpath.Replace("\Framework\", "\Framework64\"), "B2SBackglassServer64.DLL")
                Else
                    ShellAndWait(regasmpath.Replace("\Framework\", "\Framework64\"), "B2SBackglassServer.DLL")
                End If
            End If
            If Not CommandSilent Then CheckB2SServer(True) 'Make sure no window is opened on silent option
        End If

        If Not CommandSilent Then
            dialogResult = MessageBox.Show("Do you want to (re-)register the context menu (Yes)" & vbCrLf & vbCrLf & "Unregister the context menu (No)" & vbCrLf & vbCrLf & "Or please just press cancel", My.Application.Info.AssemblyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        End If

        If CommandSilent Or (dialogResult = DialogResult.Yes) Or (dialogResult = DialogResult.No) Then
            Dim rkReg As RegistryKey = Registry.ClassesRoot


            ' Clean old registry for the ScreenRes path and only if Yes is choosen it is regenerated.

            If rkReg.OpenSubKey("b2sserver.directb2s\ShellEx") Is Nothing Then
                rkReg.DeleteSubKeyTree(".directb2s", False)
                rkReg.DeleteSubKeyTree("b2sserver.directb2s", False)
            Else
                'keep ".directb2s" for VP preview handler!
                rkReg.DeleteSubKeyTree("b2sserver.directb2s\shell", False)
            End If

            rkReg.DeleteSubKeyTree(".res", False) ' Do not delete this one?
            rkReg.DeleteSubKeyTree("b2sserver.res", False)

            Using sysFileKey As RegistryKey = rkReg.OpenSubKey("SystemFileAssociations", True)
                Try
                    'Remove the old .VPX right click... Computer\HKEY_CLASSES_ROOT\SystemFileAssociations\.vpx\shell\B2SServer\
                    sysFileKey.DeleteSubKeyTree(".vpx\shell\B2SServer", False)
                    sysFileKey.DeleteSubKeyTree(".directb2s", False)

                    If CommandSilent Or (dialogResult = DialogResult.Yes) Then

                        ' Add directb2s file context menu for double click and right click -> Edit ScreenRes file
                        rkReg.CreateSubKey(".directb2s").SetValue("", "b2sserver.directb2s")
                        Using b2sReg As RegistryKey = rkReg.CreateSubKey("b2sserver.directb2s")

                            b2sReg.SetValue("", "B2S Server backglass file")
                            b2sReg.CreateSubKey("DefaultIcon").SetValue("", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2SBackglassServerEXE.exe") & """,0")
                            b2sReg.CreateSubKey("shell\open\command").SetValue("", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2SBackglassServerEXE.exe") & """ ""%1""")
                            b2sReg.CreateSubKey("shell\Edit ScreenRes file\command").SetValue("", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2S_ScreenResIdentifier.exe") & """ ""%1""")
                        End Using

                        ' Add res file context menu for double click and right click -> Edit ScreenRes file
                        rkReg.CreateSubKey(".res").SetValue("", "b2sserver.res")
                        ' Add New -> B2S Server ScreenRes file (new).res  Context menu
                        rkReg.CreateSubKey(".res\b2sserver.res\ShellNew").SetValue("Command", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2S_ScreenResIdentifier.exe") & """ ""%1""")
                        Using resReg As RegistryKey = rkReg.CreateSubKey("b2sserver.res")
                            resReg.SetValue("", "B2S Server ScreenRes file")
                            resReg.CreateSubKey("shell\open\command").SetValue("", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2S_ScreenResIdentifier.exe") & """ ""%1""")
                        End Using

                        If Directory.Exists("ScreenResTemplates") Then
                            Dim sFiles() As String = Directory.GetFiles("ScreenResTemplates", "*.res")
                            'And then add it in a Label in the way you want
                            If sFiles.Length > 0 Then
                                Using b2stoolstoplevel As RegistryKey = sysFileKey.CreateSubKey(".directb2s\shell\B2SServer")
                                    b2stoolstoplevel.SetValue("MUIVerb", "B2S Server copy Screenres template")
                                    b2stoolstoplevel.SetValue("subcommands", "")
                                    b2stoolstoplevel.SetValue("Icon", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2SBackglassServerEXE.exe") & """")
                                    For Each resFileName As String In sFiles
                                        '           "D:\vPinball\VisualPinball\B2SServer\ScreenResTemplates.cmd" "ScreenResTemplates\Full Screen.res" "%L"
                                        Dim shellText As String = """" + IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "ScreenResTemplates.cmd") + """ """ + resFileName + """ ""%L"""
                                        b2stoolstoplevel.CreateSubKey("shell\" + Path.GetFileNameWithoutExtension(resFileName) + "\command").SetValue("", shellText)
                                    Next
                                End Using
                            End If
                        End If
                    End If

                Catch ex As UnauthorizedAccessException
                    MessageBox.Show("UnauthorizedAccessException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using

        End If


        End

    End Sub
    Private Function IsAdmin() As Boolean
        Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
        Dim principal As New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function
    Private Function CheckB2SServer(ByVal showmessages As Boolean) As Boolean

        'Dim identity = WindowsIdentity.GetCurrent()
        'Dim principal = New WindowsPrincipal(identity)
        'Dim isElevated As Boolean = principal.IsInRole(WindowsBuiltInRole.Administrator)

        Dim ret As Boolean = True

        ' check object creation
        Dim err As Boolean = False
        Dim errmessage As String = String.Empty
        Try
            Dim obj As Object = CreateObject("B2S.Server")
            'obj.Stop()
            obj = Nothing
        Catch ex As Exception
            errmessage = ex.Message
            err = True
            ret = False
        End Try
        If Not err Then
            If showmessages Then MessageBox.Show("Everything is fine, the 'B2S backglass server' is registered.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf showmessages Then
            MessageBox.Show("Oops, the 'B2S backglass server' is NOT registered. Have you started this app as 'Administrator'?" & vbCrLf & vbCrLf & "(" & errmessage & ")", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Return ret

    End Function

    Private Sub ShellAndWait(ByVal regasmpath As String, ByVal dllpath As String)

        If IO.File.Exists(IO.Path.Combine(regasmpath, "regasm.exe")) Then

            ' do the register operation
            Dim process As New System.Diagnostics.Process()
            process.StartInfo.FileName = IO.Path.Combine(regasmpath, "regasm.exe")
            process.StartInfo.Arguments = dllpath + " /codebase"
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            process.Start()

            ' wait until the process passes back an exit code
            process.WaitForExit()

            ' free resources associated with this process
            process.Close()
            process.Dispose()
            'process = Nothing
        Else
            MessageBox.Show("Oops, the regasm.exe could not be found. Have you started this app as 'Administrator'?", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

End Class
