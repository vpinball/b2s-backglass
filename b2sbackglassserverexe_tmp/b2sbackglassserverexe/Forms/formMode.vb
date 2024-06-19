Imports System

Public Class formMode

    Private Const stayInSeconds As Integer = 3

    Private fadeIn As Boolean = True
    Private Const fadeStep As Single = 0.02
    Private shutDownCounter As Integer = 0

    Private Sub formMode_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ' set info label
        lblInfo.Text = "Backglass mode is set to '" & If(B2SSettings.CurrentDualMode = B2SSettings.eDualMode.Fantasy, "FANTASY", "AUTHENTIC") & "'."

        ' set and start timers
        TimerShutDown.Interval = 1000
        TimerOpacity.Interval = 10
        TimerOpacity.Start()

    End Sub

    Private Sub TimerOpacity_Tick(sender As System.Object, e As System.EventArgs) Handles TimerOpacity.Tick

        If fadeIn Then
            Me.Opacity += fadeStep
            If Me.Opacity >= 1 Then
                TimerOpacity.Stop()
                TimerShutDown.Start()
                fadeIn = False
            End If
        Else
            Me.Opacity -= fadeStep
            If Me.Opacity <= 0 Then
                TimerOpacity.Stop()
                Try
                    Me.Close()
                    Me.Dispose()
                Catch
                End Try
            End If
        End If

    End Sub

    Private Sub TimerShutDown_Tick(sender As System.Object, e As System.EventArgs) Handles TimerShutDown.Tick

        shutDownCounter += 1
        If shutDownCounter >= stayInSeconds Then
            TimerShutDown.Stop()
            TimerOpacity.Start()
        End If

    End Sub

End Class