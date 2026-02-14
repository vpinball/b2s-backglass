Imports Microsoft.Win32
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Principal

Public Class formBackglassServerRegApp
    Public Shared Function SafeReadRegistry(ByVal keyname As String, ByVal valuename As String, ByVal defaultvalue As String) As String
        '    Public Property GlobalFileName As String = SafeReadRegistry("Software\B2S", "B2SScreenResFileNameOverride", "ScreenRes.txt")

        Try
            Return CStr(Registry.CurrentUser.OpenSubKey(keyname).GetValue(valuename, defaultvalue))
        Catch ex As Exception
            Return defaultvalue
        End Try
    End Function

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
        Dim alreadyInstalledfilepath As String = String.Empty

        If Not CommandSilent Then
            If CheckB2SServer(False) Then
                Dim dllURI As String = "file://Unknown"
                Dim dllVersion As String = ""
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
                                    Try
                                        '"Assembly"="B2SBackglassServer, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null"
                                        dllVersion = openKey.GetValue("Assembly").Split(",")(1).ToString()
                                    Catch
                                        dllVersion = "(not available)"
                                    End Try
                                End If
                            End Using
                        End If
                    End Using
                Catch
                End Try

                alreadyInstalledfilepath = New Uri(dllURI).LocalPath
                dialogResult = MessageBox.Show($"The 'B2S Server'{dllVersion} is already registered here:" & vbCrLf & vbCrLf & alreadyInstalledfilepath & vbCrLf & vbCrLf & "Do you want to (try to) re-register it?", My.Application.Info.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            Else
                dialogResult = MessageBox.Show("The 'B2S Server' is not registered yet." & vbCrLf & vbCrLf & "Do you want to register it?", My.Application.Info.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            End If
        End If

        ' get .NET framework base directory
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
                        'Computer\HKEY_CLASSES_ROOT\CLSID\<id>\InprocServer32
                        regRoot.OpenSubKey("CLSID", True).DeleteSubKeyTree($"{clsID}\InprocServer32", False)
                        'Computer\HKEY_CLASSES_ROOT\CLSID\<id>
                        regRoot.OpenSubKey("CLSID", True).DeleteSubKeyTree(clsID, False)
                        'Computer\HKEY_CLASSES_ROOT\WOW6432Node\CLSID\<id>
                        regRoot.OpenSubKey("WOW6432Node\CLSID", True).DeleteSubKeyTree(clsID, False)
                    End Using
                End If

                ' Clean up ALL old B2S.* ProgIDs from legacy VB.NET DLL (these were accidentally COM-visible)
                ' The new C# B2S.ComServer.dll only exposes B2S.Server, so these are no longer needed
                CleanupLegacyB2SEntries(CommandSilent)

                ' Default to the COM server which is already installed in the earlier check, but if both are present ask the user which one to register
                Dim dllToRegister As String = If(String.IsNullOrEmpty(alreadyInstalledfilepath) Or alreadyInstalledfilepath.EndsWith("B2SBackglassServer.DLL", StringComparison.OrdinalIgnoreCase), "B2SBackglassServer.DLL", "B2S.ComServer.dll")
                If Not CommandSilent Then
                    ' Ask which DLL to register
                    Dim hasComServer As Boolean = File.Exists(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "B2S.ComServer.dll"))
                    Dim hasLegacyServer As Boolean = File.Exists(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "B2SBackglassServer.DLL"))
                    If hasComServer AndAlso hasLegacyServer Then
                        Dim dllChoice As DialogResult = MessageBox.Show(
                            "Which COM server do you want to register?" & vbCrLf & vbCrLf &
                            "YES = VB B2SBackglassServer.dll" & vbCrLf &
                            "NO  = C# B2S.ComServer.dll" & vbCrLf & vbCrLf &
                            "Both DLLs were found in the installation directory.",
                            "Select COM Server to Register",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question)

                        If dllChoice = DialogResult.Yes Then
                            dllToRegister = "B2SBackglassServer.DLL"
                        ElseIf dllChoice = DialogResult.No Then
                            dllToRegister = "B2S.ComServer.dll"
                        Else
                            ' Cancel - don't register anything
                            GoTo SkipRegistration
                        End If
                    ElseIf Not hasComServer AndAlso Not hasLegacyServer Then
                        MessageBox.Show("Neither B2S.ComServer.dll nor B2SBackglassServer.dll found in the installation directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        GoTo SkipRegistration
                    End If
                End If

                ' do the register operation
                ShellAndWait(regasmpath, dllToRegister)
                ShellAndWait(regasmpath.Replace("\Framework\", "\Framework64\"), dllToRegister)
SkipRegistration:
            End If
            If Not CommandSilent Then CheckB2SServer(True) 'Make sure no window is opened on silent option
        End If

        If Not CommandSilent Then
            dialogResult = MessageBox.Show("Do you want to (re-)register the context menu (Yes)" & vbCrLf & vbCrLf & "Unregister the context menu (No)" & vbCrLf & vbCrLf & "Or please just press cancel", My.Application.Info.AssemblyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        End If

        If CommandSilent Or (dialogResult = DialogResult.Yes) Or (dialogResult = DialogResult.No) Then
            Dim rkReg As RegistryKey = Registry.ClassesRoot
            Dim B2SResFileEnding As String = SafeReadRegistry("Software\B2S", "B2SResFileEndingOverride", ".res")

            ' Clean old registry for the ScreenRes path and only if Yes is choosen it is regenerated.

            If rkReg.OpenSubKey("b2sserver.directb2s\ShellEx") Is Nothing Then
                rkReg.DeleteSubKeyTree(".directb2s", False)
                rkReg.DeleteSubKeyTree("b2sserver.directb2s", False)
            Else
                'keep ".directb2s" for VP preview handler!
                rkReg.DeleteSubKeyTree("b2sserver.directb2s\shell", False)
            End If

            rkReg.DeleteSubKeyTree(B2SResFileEnding, False) ' Do not delete this one?
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
                        rkReg.CreateSubKey(B2SResFileEnding).SetValue("", "b2sserver.res")
                        ' Add New -> B2S Server ScreenRes file (new).res  Context menu
                        rkReg.CreateSubKey(".res\b2sserver.res\ShellNew").SetValue("Command", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2S_ScreenResIdentifier.exe") & """ ""%1""")
                        Using resReg As RegistryKey = rkReg.CreateSubKey("b2sserver.res")
                            resReg.SetValue("", "B2S Server ScreenRes file")
                            resReg.CreateSubKey("shell\open\command").SetValue("", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2S_ScreenResIdentifier.exe") & """ ""%1""")
                        End Using

                        If Directory.Exists("ScreenResTemplates") Then
                            Dim sFiles() As String = Directory.GetFiles("ScreenResTemplates", "*" + B2SResFileEnding)
                            'And then add it in a Label in the way you want
                            If sFiles.Length > 0 Then
                                Using b2stoolstoplevel As RegistryKey = sysFileKey.CreateSubKey(".directb2s\shell\B2SServer"),
                                    vpxtoolstoplevel As RegistryKey = sysFileKey.CreateSubKey(".vpx\shell\B2SServer")

                                    b2stoolstoplevel.SetValue("MUIVerb", "B2S Server copy Screenres template")
                                    b2stoolstoplevel.SetValue("subcommands", "")
                                    b2stoolstoplevel.SetValue("Icon", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2SBackglassServerEXE.exe") & """")

                                    vpxtoolstoplevel.SetValue("MUIVerb", "B2S Server copy Screenres template")
                                    vpxtoolstoplevel.SetValue("subcommands", "")
                                    vpxtoolstoplevel.SetValue("Icon", """" & IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "B2SBackglassServerEXE.exe") & """")

                                    For Each resFileName As String In sFiles
                                        '           "D:\vPinball\VisualPinball\B2SServer\ScreenResTemplates.cmd" "ScreenResTemplates\Full Screen.res" "%L"
                                        Dim shellText As String = """" + IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath()), "ScreenResTemplates.cmd") + """ """ + resFileName + """ ""%L"""
                                        b2stoolstoplevel.CreateSubKey("shell\" + Path.GetFileNameWithoutExtension(resFileName) + "\command").SetValue("", shellText)

                                        vpxtoolstoplevel.CreateSubKey("shell\" + Path.GetFileNameWithoutExtension(resFileName) + "\command").SetValue("", shellText)
                                    Next
                                End Using

                            End If
                        End If
                    End If

                Catch ex As UnauthorizedAccessException
                    MessageBox.Show("UnauthorizedAccessException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Stop
                End Try
            End Using

        End If
        If Not CommandSilent Then MessageBox.Show("Everything is fine, the 'B2S backglass server' is registered.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

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
            Dim serverType As Type = Type.GetTypeFromProgID("B2S.Server", throwOnError:=False)
            If serverType Is Nothing Then
                err = True
                ret = False
                errmessage = "ProgID not registered"
            End If
        Catch ex As Exception
            errmessage = ex.Message
            err = True
            ret = False
        End Try
        If err And showmessages Then
            MessageBox.Show("Oops, the 'B2S Server' is NOT registered. Have you started this app as 'Administrator'?" & vbCrLf & vbCrLf & "(" & errmessage & ")", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Dim installPath As String = IO.Path.GetDirectoryName(Application.ExecutablePath())
            UnblockDeploymentFiles(installPath)
            UnblockFile(IO.Path.Combine(installPath, dllpath))
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

    <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Shared Function DeleteFile(lpFileName As String) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Private Sub UnblockFile(filePath As String)
        Try
            If File.Exists(filePath) Then
                DeleteFile(filePath & ":Zone.Identifier")
            End If
        Catch
            ' ignore failures
        End Try
    End Sub

    Private Sub UnblockDeploymentFiles(rootPath As String)
        Try
            If Directory.Exists(rootPath) Then
                For Each pattern As String In {"*.dll", "*.exe", "*.cmd"}
                    For Each file As String In Directory.GetFiles(rootPath, pattern, SearchOption.TopDirectoryOnly)
                        UnblockFile(file)
                    Next
                Next
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Dynamically finds and cleans up all B2S.* ProgIDs and orphaned CLSIDs in HKEY_CLASSES_ROOT.
    ''' This catches all legacy entries from old VB.NET DLL versions that were accidentally COM-visible.
    ''' The new version only exposes B2S.Server, so these orphaned entries cause conflicts.
    ''' Also scans CLSID registry directly to catch orphaned entries without ProgID references.
    ''' </summary>
    Private Sub CleanupLegacyB2SEntries(CommandSilent As Boolean)
        Try
            ' First, find all B2S.* entries dynamically across registry views
            Dim viewsToCheck As New List(Of RegistryView) From {RegistryView.Registry32}
            If Environment.Is64BitOperatingSystem Then
                viewsToCheck.Add(RegistryView.Registry64)
            End If

            Dim viewProgIds As New Dictionary(Of RegistryView, List(Of String))()
            Dim viewClsids As New Dictionary(Of RegistryView, HashSet(Of String))()

            For Each view As RegistryView In viewsToCheck
                Dim foundEntries As New List(Of String)
                Dim foundClsids As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

                Using regRoot As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, view)
                    ' Enumerate all subkeys looking for B2S.* ProgIDs (but not B2S.Server)
                    For Each subKeyName As String In regRoot.GetSubKeyNames()
                        If subKeyName.StartsWith("B2S.", StringComparison.OrdinalIgnoreCase) AndAlso
                           Not subKeyName.Equals("B2S.Server", StringComparison.OrdinalIgnoreCase) Then
                            foundEntries.Add(subKeyName)

                            ' Try to get the CLSID for this ProgID
                            Try
                                Using progIdKey As RegistryKey = regRoot.OpenSubKey(subKeyName & "\CLSID", False)
                                    If progIdKey IsNot Nothing Then
                                        Dim clsidValue As String = CStr(progIdKey.GetValue(""))
                                        If Not String.IsNullOrEmpty(clsidValue) Then
                                            foundClsids.Add(clsidValue)
                                        End If
                                    End If
                                End Using
                            Catch
                            End Try
                        End If
                    Next

                    ' Also scan CLSID registry directly to find orphaned CLSIDs pointing to B2S assemblies
                    ' This catches legacy entries that may not have ProgID wrappers
                    ' .NET assemblies are referenced via "Class" and "Assembly" values in InprocServer32
                    Try
                        Using clsidBaseKey As RegistryKey = regRoot.OpenSubKey("CLSID", False)
                            If clsidBaseKey IsNot Nothing Then
                                For Each clsidGuid As String In clsidBaseKey.GetSubKeyNames()
                                    if (foundClsids.Contains(clsidGuid)) Then
                                        ' Already found via ProgID reference, skip
                                        Continue For
                                    End If
                                    Try
                                        Using inProcKey As RegistryKey = clsidBaseKey.OpenSubKey(clsidGuid & "\InprocServer32", False)
                                            If inProcKey IsNot Nothing Then
                                                ' Check multiple registry values for B2S references
                                                Dim dllPath As String = CStr(If(inProcKey.GetValue(""), ""))
                                                Dim codeBase As String = CStr(If(inProcKey.GetValue("CodeBase"), ""))
                                                Dim classValue As String = CStr(If(inProcKey.GetValue("Class"), ""))
                                                Dim assemblyValue As String = CStr(If(inProcKey.GetValue("Assembly"), ""))

                                                ' Check if this CLSID points to a B2S Server (check all possible locations where B2S is referenced)
                                                Dim isB2SEntry As Boolean = False
                                                If (Not String.IsNullOrEmpty(dllPath) AndAlso dllPath.Contains("B2SBackglassServer.dll") ) Then
                                                    isB2SEntry = True
                                                ElseIf (Not String.IsNullOrEmpty(codeBase) AndAlso codeBase.Contains("B2SBackglassServer.dll") ) Then
                                                    isB2SEntry = True
                                                ElseIf (Not String.IsNullOrEmpty(classValue) AndAlso classValue.StartsWith("B2S.", StringComparison.OrdinalIgnoreCase)) Then
                                                    ' .NET class like "B2S.formMode"
                                                    isB2SEntry = True
                                                ElseIf (Not String.IsNullOrEmpty(assemblyValue) AndAlso assemblyValue.StartsWith("B2SBackglassServer", StringComparison.OrdinalIgnoreCase)) Then
                                                    ' Assembly like "B2SBackglassServer, Version=2.0.4.0, ..."
                                                    isB2SEntry = True
                                                End If

                                                If isB2SEntry Then
                                                    foundClsids.Add(clsidGuid)
                                                End If
                                            End If
                                        End Using
                                    Catch
                                    End Try
                                Next
                            End If
                        End Using
                    Catch
                    End Try
                End Using

                If foundEntries.Count > 0 OrElse foundClsids.Count > 0 Then
                    viewProgIds(view) = foundEntries
                    viewClsids(view) = foundClsids
                End If
            Next

            Dim totalProgIds As Integer = 0
            Dim totalClsids As Integer = 0
            For Each progIds As List(Of String) In viewProgIds.Values
                totalProgIds += progIds.Count
            Next
            For Each clsids As HashSet(Of String) In viewClsids.Values
                totalClsids += clsids.Count
            Next

            ' If nothing found, we're done
            If totalProgIds = 0 AndAlso totalClsids = 0 Then
                Return
            End If

            ' Ask user for confirmation
            Dim confirmResult As DialogResult
            If CommandSilent Then
                confirmResult = DialogResult.Yes
            Else
                confirmResult = MessageBox.Show(
                    $"Found {totalProgIds} legacy B2S.* ProgID entries and {totalClsids} associated CLSIDs." & vbCrLf & vbCrLf &
                    "These are leftover entries from old B2SBackglassServer.dll versions." & vbCrLf &
                    "The new version only uses 'B2S.Server'." & vbCrLf & vbCrLf &
                    "Do you want to remove these legacy entries?" & vbCrLf & vbCrLf &
                    "YES = Remove all legacy entries (recommended)" & vbCrLf &
                    "NO = Keep legacy entries",
                    "Clean Up Legacy Registry Entries",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
            End If
            
            If confirmResult <> DialogResult.Yes Then
                Return
            End If
            
            ' Now delete all found entries
            Dim deletedProgIds As Integer = 0
            Dim deletedClsids As Integer = 0

            For Each view As RegistryView In viewProgIds.Keys
                Using regRoot As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, view)
                    ' Delete ProgID entries
                    For Each progId As String In viewProgIds(view)
                        Try
                            regRoot.DeleteSubKeyTree(progId, False)
                            deletedProgIds += 1
                        Catch
                        End Try
                    Next

                    ' Delete CLSID entries
                    Using clsidKey As RegistryKey = regRoot.OpenSubKey("CLSID", True)
                        If clsidKey IsNot Nothing Then
                            For Each clsid As String In viewClsids(view)
                                Try
                                    clsidKey.DeleteSubKeyTree(clsid, False)
                                    deletedClsids += 1
                                Catch
                                End Try
                            Next
                        End If
                    End Using
                End Using
            Next
            
            MessageBox.Show(
                $"Cleanup completed:" & vbCrLf &
                $"  Removed {deletedProgIds} of {totalProgIds} ProgID entries" & vbCrLf &
                $"  Removed {deletedClsids} of {totalClsids} CLSID entries",
                "Registry Cleanup Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
                
        Catch ex As Exception
            MessageBox.Show(
                "Error during registry cleanup: " & ex.Message & vbCrLf & vbCrLf &
                "Make sure you are running as Administrator.",
                "Registry Cleanup Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
        End Try
    End Sub

End Class
