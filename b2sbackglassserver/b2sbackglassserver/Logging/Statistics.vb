Imports System
Imports System.Text

Public Class Statistics

    Private Const cMinTicks4CoolDown As Integer = 150
    Private Const cMinTicks4GoodCalculation As Integer = 500
    Private Const cMinTicks4Chillout As Integer = 18
    Private Const cMaxTicks4BadChillout1 As Integer = 12
    Private Const cMaxTicks4BadChillout2 As Integer = 8
    Private Const cMaxTicks4BadChillout3 As Integer = 4
    Private Const cMaxTicks4BadChillout4 As Integer = 2

    Private log As Log = Nothing

    Private lastTick As Date = Nothing
    Private counter As Integer = 0
    Private sum As Integer = 0
    Private average As Single = 0
    Private chiller As Integer = 0
    Private waitonechill As Integer = 0

    Private sb As StringBuilder = New StringBuilder()

    Public Sub New(ByRef _log As Log)
        log = _log
    End Sub

    Public Function AddTick(ByVal currentCoolDownCounter As Integer, ByVal info As String) As Integer

        Dim ret As Integer = 0

        Dim nowTick As Date = DateTime.Now
        Dim diff As Integer = (nowTick - lastTick).Milliseconds
        lastTick = nowTick

        ' calc average tick duration
        If counter < cMinTicks4GoodCalculation Then
            counter += 1
            sum += diff
            average = sum / counter
        End If

        ' calc cool down level
        Dim chiller4logfile As Integer = chiller
        If counter > cMinTicks4CoolDown Then
            If currentCoolDownCounter <= 0 Then chiller += 1
            Dim cooldownfactor As Single = diff / average
            If cooldownfactor > 3 Then
                ret = 8
                chiller = 0
            ElseIf cooldownfactor >= 2 Then
                ret = 6
                chiller = 0
            ElseIf cooldownfactor >= 1.5 Then
                ' mode 5
                If chiller <= 12 Then
                    waitonechill += 1
                    If waitonechill > 1 Then
                        ret = Math.Round((12 - chiller) / 2) + 2
                        If ret >= 5 Then ret += 1
                    Else
                        ret = 1
                    End If

                    ' mode 4
                    'If chiller <= 14 Then
                    '    waitonechill += 1
                    '    If waitonechill > 1 Then
                    '        ret = Math.Round((14 - chiller) / 2) + 2
                    '        If ret >= 6 Then ret += 1
                    '        If ret >= 9 Then ret += 1
                    '    Else
                    '        ret = 1
                    '    End If

                    ' mode 3
                    'If chiller <= 16 Then
                    'If currentCoolDownCounter > 0 Then
                    '    If currentCoolDownCounter < 3 Then
                    '        ret = currentCoolDownCounter + 6
                    '    Else
                    '        ret = currentCoolDownCounter + 3
                    '    End If
                    'Else
                    '    ret = Math.Round((16 - chiller) / 2) + 2
                    'End If

                    ' mode 2
                    'If chiller <= 16 Then
                    'ret = Math.Round((16 - chiller) / 2) + 1

                    ' mode 1
                    'If chiller < cMaxTicks4BadChillout4 Then
                    '    ret = 6
                    'ElseIf chiller < cMaxTicks4BadChillout3 Then
                    '    ret = 5
                    'ElseIf chiller < cMaxTicks4BadChillout2 Then
                    '    ret = 4
                    'ElseIf chiller < cMaxTicks4BadChillout1 Then
                    '    ret = 3
                    'ElseIf chiller < cMinTicks4Chillout Then
                    '    ret = 1
                Else
                    waitonechill = 0
                    ret = 0
                End If
                chiller = 0
            End If
            If currentCoolDownCounter > ret Then
                ret = currentCoolDownCounter
            End If
        End If

        ' maybe write log entry
        If log IsNot Nothing AndAlso log.IsLogOn Then
            sb.Length = 0
            sb.Append(nowTick)
            sb.Append(": ")
            sb.Append(diff)
            sb.Append("  ,  ")
            sb.Append(average)
            sb.Append("  ,  ")
            If Not String.IsNullOrEmpty(info) Then
                sb.Append(info)
            End If
            If counter > cMinTicks4CoolDown Then
                If ret > 0 Then
                    sb.Append("  ,  ")
                    sb.Append("CoolDown: " & ret)
                End If
                If chiller4logfile > 0 Then
                    sb.Append("  ,  ")
                    sb.Append("Chiller: " & chiller4logfile)
                End If
            End If
            log.WriteLogEntry(sb.ToString())
        End If

        Return ret

    End Function

End Class
