Imports System

'Namespace My
'    Partial Friend Class MyApplication
'        Protected Overrides Sub OnRun()
'            Try
'                ApplicationStartup.Main()
'                MyBase.OnRun()
'            Catch ex As Exception
'                ' ignore crashes after closing
'                Throw ex
'            End Try
'        End Sub
'    End Class
'End Namespace

'Module ApplicationStartup
'    Public Sub Main()
'        Try
'            Windows.Forms.Application.EnableVisualStyles()
'            'MsgBox(Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory())
'        Catch ex As Exception
'            'MsgBox(String.Format(My.Resources.MSG_StartupError, ex.Message), MsgBoxStyle.Critical, My.Resources.TXT_Caption)
'        End Try
'    End Sub
'End Module