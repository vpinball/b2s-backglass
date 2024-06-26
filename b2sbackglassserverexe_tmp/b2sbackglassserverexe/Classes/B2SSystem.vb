Imports System
Imports System.Text
Imports System.Windows.Forms

Public Class B2SSystem

    Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Int32) As Short

    Private Const writeLogFile As Boolean = False

    Private formBackglass As formBackglass = Nothing

    Public Class B2SSystemEventArgs
        Inherits EventArgs

        Public Value As Integer()
        Public IsValueChanged As Boolean()

        Public Sub New(ByVal _value As Integer(), ByVal _isValueChanged As Boolean())
            Value = _value
            IsValueChanged = _isValueChanged
        End Sub
    End Class

    Public Event DataIsSent(ByVal sender As Object, ByVal e As B2SSystemEventArgs)
    Public Event EscapeIsHit(ByVal sender As Object, ByVal e As EventArgs)
    Public Event VPIsActivated(ByVal sender As Object, ByVal e As EventArgs)

    Private TimerData As Timer = Nothing
    Private TimerEscape As Timer = Nothing
    Private TimerActivateVP As Timer = Nothing

    Private B2SDataCount As Integer = 0

#Region "constructor and startup"

    Public Sub New()

        ' here is the place for a system checkup
        CheckUp()

        ' timer to pass data to backglass
        TimerData = New Timer
        With TimerData
            AddHandler .Tick, AddressOf TimerData_Tick
            .Interval = 51
            .Enabled = False
        End With

        ' timer to get escape key event
        TimerEscape = New Timer
        With TimerEscape
            AddHandler .Tick, AddressOf TimerEscape_Tick
            .Interval = 150
            .Enabled = False
        End With

        ' timer to activate visual pinball
        TimerActivateVP = New Timer
        With TimerActivateVP
            AddHandler .Tick, AddressOf TimerActivateVP_Tick
            .Interval = 2000
            .Enabled = False
        End With

    End Sub

    Public Sub Start(ByVal _formBackglass As Form, ByVal _B2SDataCount As Integer, Optional ByVal _StartDataTimerImmediatelly As Boolean = True)

        ' here we go with the backglass form
        Me.formBackglass = _formBackglass

        ' this is the count of high priority data elements
        ' max count is 50
        Me.B2SDataCount = _B2SDataCount
        If Me.B2SDataCount > 50 Then Me.B2SDataCount = 50

        ' maybe start the VP activation and the escape timer
        If Not TimerActivateVP.Enabled Then TimerActivateVP.Start()
        'If Not TimerEscape.Enabled Then TimerEscape.Start()

        ' maybe start the remaining timers
        'If _StartDataTimerImmediatelly Then
        '    If Not TimerData.Enabled Then TimerData.Start()
        'End If

    End Sub

    Private Sub CheckUp()

        ' have a look for the .NET framework


    End Sub

#End Region

#Region "timers"

    Private Sub TimerData_Tick(ByVal sender As Object, ByVal e As EventArgs)

        '' initialize the value storage - this storage is to avoid too much update traffic
        'Static value(75) As Integer
        'Static isvaluechanged(75) As Boolean
        'Static valueInit As Boolean = False
        'If Not valueInit Then
        '    valueInit = True
        '    For I As Integer = 0 To Me.B2SDataCount
        '        isvaluechanged(I) = True
        '        value(I) = 0
        '    Next
        'End If

        '' get current data
        'Dim data As String = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\B2S\", "B2SDATA", Nothing)
        'If data = Nothing OrElse data.Length = 0 Then
        '    data = Space(75) ' "                                                                           "
        'End If

        '' get thru all data receiving objects
        'For i As Integer = 1 To Me.B2SDataCount
        '    isvaluechanged(i) = False
        '    Dim currentvalue As Integer = Asc(data.Substring(i, 1)) - 1
        '    If currentvalue <> value(i) Then
        '        isvaluechanged(i) = True
        '        value(i) = currentvalue
        '    End If
        'Next
        'If writeLogFile Then
        '    Dim sb As StringBuilder = New StringBuilder()
        '    Dim log As IO.StreamWriter = New IO.StreamWriter("c:\tmp\log.txt", True)
        '    For i As Integer = 0 To 50
        '        sb.Append(value(i))
        '        sb.Append(" ")
        '    Next
        '    log.WriteLine(sb.ToString())
        '    log.Flush()
        '    log.Close()
        'End If
        'RaiseEvent DataIsSent(Me, New B2SSystemEventArgs(value, isvaluechanged))

    End Sub

    Private Sub TimerEscape_Tick(ByVal sender As Object, ByVal e As EventArgs)

        '' stop current timer
        'TimerEscape.Stop()

        '' check whether the escape is pressed
        'Dim keyState As Short = GetAsyncKeyState(Keys.Escape)
        'If keyState <> 0 Then
        '    ' raise matching escape key event
        '    RaiseEvent EscapeIsHit(Me, New EventArgs())
        '    ' close the backglass
        '    formBackglass.Close()
        'Else
        '    ' restart current timer
        '    TimerEscape.Start()
        'End If

    End Sub

    Private Sub TimerActivateVP_Tick(ByVal sender As Object, ByVal e As EventArgs)

        '' stop current timer
        'TimerActivateVP.Stop()

        '' set focus to VP
        'Try
        '    AppActivate("Visual Pinball Player")
        'Catch
        'End Try

        '' raise matching event
        'RaiseEvent VPIsActivated(Me, New EventArgs())

        '' maybe start the remaining timers
        'If Not TimerData.Enabled Then TimerData.Start()
        'If Not TimerEscape.Enabled Then TimerEscape.Start()

    End Sub

#End Region

End Class