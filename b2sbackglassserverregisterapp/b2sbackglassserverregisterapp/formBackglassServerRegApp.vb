﻿Imports Microsoft.Win32
Imports System.IO

Public Class formBackglassServerRegApp


    Private Sub Form1_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

        Me.Visible = False

        Dim basepath As String = String.Empty
        Dim regasmpath As String = String.Empty
        Dim version As String = String.Empty
        Dim dialogResult As DialogResult

        If CheckB2SServer(False) Then
            dialogResult = MessageBox.Show("The 'B2S Server' is already registered." & vbCrLf & vbCrLf & "Do you REALLY want to (try to) reregister it???", My.Application.Info.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        Else
            dialogResult = MessageBox.Show("The 'B2S Server' is not registered yet." & vbCrLf & vbCrLf & "Do you want to register it?", My.Application.Info.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        End If

        ' get .NET framework base directoy
        If dialogResult = DialogResult.Yes Then
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
                        'Else
                        '    regkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\.NetFramework\policy\v2.0", False)
                        '    If regkey IsNot Nothing Then version = "v2.0"; regkey.Close() End If
                    End If
                End Using
            End If

            ' check whether everything is fine
            If String.IsNullOrEmpty(basepath) Then
                MessageBox.Show("Error, no basepath found.")
            ElseIf String.IsNullOrEmpty(regasmpath) Then
                MessageBox.Show("Error, no regasmpath found.")
            Else
                ' do the register operation
                ShellAndWait(regasmpath)
                ShellAndWait(regasmpath.Replace("\Framework\", "\Framework64\"))
            End If
            CheckB2SServer(True)
        End If

        dialogResult = MessageBox.Show("Do you want to (re-)register the context menu (Yes)" & vbCrLf & vbCrLf & "Unregister the context menu (No)" & vbCrLf & vbCrLf & "Or please just press cancel", My.Application.Info.AssemblyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

        If (dialogResult = DialogResult.Yes) Or (dialogResult = DialogResult.No) Then
            Dim rkReg As RegistryKey = Registry.ClassesRoot
            Dim strKey As String = "Applications\VPinballX.exe\shell"

            Dim pinEditValue As String = ""

            Using openKey As RegistryKey = rkReg.OpenSubKey(strKey + "\edit\command", False)
                If openKey IsNot Nothing Then
                    Try
                        pinEditValue = openKey.GetValue("")
                    Catch ex As UnauthorizedAccessException
                        MessageBox.Show("UnauthorizedAccessException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End
                    End Try
                End If
            End Using
            If Not pinEditValue = "" Then
                Using openKey As RegistryKey = rkReg.OpenSubKey(strKey, True)
                    Try
                        ' Clean old registry for the ScreenRes path and only if Yes is choosen it is regenerated.
                        Try
                            openKey.DeleteSubKeyTree("B2SServer")
                        Catch ex As ArgumentException

                        End Try
                        If (dialogResult = DialogResult.Yes) Then

                            'Add VPX Pov export 
                            'Dim exportPOVKey As RegistryKey = openKey.CreateSubKey("export POV").CreateSubKey("command")
                            'exportPOVKey.SetValue("", pinEditValue.Replace(" -edit ", " -Pov "))

                            'Add VBS export 
                            'Dim exportVBSKey As RegistryKey = openKey.CreateSubKey("export VBS").CreateSubKey("command")
                            'exportVBSKey.SetValue("", pinEditValue.Replace(" -edit ", " -ExtractVBS "))

                            Dim sFiles() As String = Directory.GetFiles("ScreenResTemplates", "*.res")
                            'And then add it in a Label in the way you want
                            If sFiles.Length > 0 Then
                                Dim vpxtoolstoplevel As RegistryKey = openKey.CreateSubKey("B2SServer")
                                vpxtoolstoplevel.SetValue("MUIVerb", "B2S Server copy Screenres template")
                                vpxtoolstoplevel.SetValue("subcommands", "")
                                vpxtoolstoplevel.SetValue("Icon", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2SBackglassServerEXE.exe") & """")
                                For Each resFileName As String In sFiles
                                    '           "D:\vPinball\VisualPinball\B2SServer\ScreenResTemplates.cmd" "ScreenResTemplates\Full Screen.res" "%L"
                                    Dim shellText As String = """" + IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "ScreenResTemplates.cmd") + """ """ + resFileName + """ ""%L"""
                                    vpxtoolstoplevel.CreateSubKey("shell").CreateSubKey(Path.GetFileNameWithoutExtension(resFileName)).CreateSubKey("command").SetValue("", shellText)
                                Next
                                vpxtoolstoplevel.Close()
                            End If
                        End If
                    Catch ex As UnauthorizedAccessException
                            MessageBox.Show("UnauthorizedAccessException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End If
        End If


        End

    End Sub

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

    Private Sub ShellAndWait(ByVal regasmpath As String)

        If IO.File.Exists(IO.Path.Combine(regasmpath, "regasm.exe")) Then

            ' do the register operation
            Dim process As New System.Diagnostics.Process()
            process.StartInfo.FileName = IO.Path.Combine(regasmpath, "regasm.exe")
            process.StartInfo.Arguments = """B2SBackglassServer.DLL"" /codebase" ' /silent"
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
