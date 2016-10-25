Imports Microsoft.Win32
Imports System.Security.Principal

Public Class formBackglassServerRegApp


    Private Sub Form1_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

        Me.Visible = False

        'If Not CheckB2SServer(False) Then

        ' base vars
        Dim basepath As String = String.Empty
        Dim regasmpath As String = String.Empty
        Dim version As String = String.Empty

        ' get .NET framework base directoy
        Dim regkey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\.NetFramework", False)
        If regkey IsNot Nothing Then
            basepath = regkey.GetValue("InstallRoot")
        End If
        regkey.Close()

        ' get version info
        If Not String.IsNullOrEmpty(basepath) Then
            regkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\.NetFramework\Policy\v4.0", False)
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
            Else
                '    regkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\.NetFramework\policy\v2.0", False)
                '    If regkey IsNot Nothing Then
                '        version = "v2.0"

                '        regkey.Close()
                '    End If
            End If
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

        'End If

        'Me.Close()
        End

    End Sub

    Private Function CheckB2SServer(ByVal showerrormessage As Boolean) As Boolean

        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)
        Dim isElevated As Boolean = principal.IsInRole(WindowsBuiltInRole.Administrator)

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
            MessageBox.Show("Everything is fine, the 'B2S backglass server' is registered.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf showerrormessage Then
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
            process = Nothing

        End If

    End Sub

End Class
