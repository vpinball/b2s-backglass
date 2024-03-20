Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Win32

Module B2S_ScreenResIdentifier
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New formPlayfield())
    End Sub
End Module