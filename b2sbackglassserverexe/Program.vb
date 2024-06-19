Imports B2S

Public Module Main
    Sub Main()

        Dim myServer As New EXEServer

        Dim TableFileName As String = ""
        Dim topMost As Boolean = False

        If My.Application.CommandLineArgs.Count > 0 Then
            TableFileName = My.Application.CommandLineArgs(0).ToString
            If My.Application.CommandLineArgs.Count > 1 AndAlso My.Application.CommandLineArgs(1).ToString = "1" Then
                topMost = True
            End If
        Else
            MessageBox.Show("Please do not start the EXE this way.", "B2S.Server", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New formBackglass(TableFileName, topMost))
        'myServer.Run(New formBackglass(TableFileName, topMost))
    End Sub

End Module
