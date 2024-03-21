Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Win32

Module B2SBackglassServerEXE
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New formBackglass())
    End Sub
End Module