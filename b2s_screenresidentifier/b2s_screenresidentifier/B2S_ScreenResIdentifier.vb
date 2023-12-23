Imports System
Imports System.Drawing
Imports System.IO
Imports System.Security.Principal
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports MessageBox = System.Windows.MessageBox

Module B2S_ScreenResIdentifier
    Sub Main()
        If IsAdmin() Then
            MessageBox.Show("You should not start this as Administrator!", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New formPlayfield())
    End Sub
    Private Function IsAdmin() As Boolean
        Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
        Dim principal As New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function
End Module