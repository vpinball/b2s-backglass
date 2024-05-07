Imports B2S
Public Module Main
    Sub Main()

        Dim myServer As New B2S.Server

        If My.Application.CommandLineArgs.Count > 0 Then
            B2SData.TableFileName = My.Application.CommandLineArgs(0).ToString

            If B2SData.TableFileName.EndsWith(".directb2s") Then
                B2SData.TableFileName = System.IO.Path.GetFileNameWithoutExtension(B2SData.TableFileName)
                B2SSettings.StartAsEXE = False
            End If

            If My.Application.CommandLineArgs.Count > 1 Then
                If My.Application.CommandLineArgs(1).ToString = "1" Then
                    'Me.TopMost = True
                End If
            End If
        Else
            MessageBox.Show("Please do not start the EXE this way.", "B2SWrapper", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New formBackglass)
    End Sub

End Module
