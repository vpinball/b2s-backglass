Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Win32

Module B2S_ScreenResIdentifier
    Sub Main()
        If IsAdmin() Then
            MessageBox.Show("You should not start this as Administrator!", My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            ' Too much noice here if we would quit out...
            'Exit Sub
        End If

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New formPlayfield())
    End Sub
End Module