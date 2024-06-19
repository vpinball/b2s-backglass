Imports System

Public Class B2SReelDisplay

    Implements IDisposable

    Private timerRR As System.Windows.Forms.Timer = Nothing
    Private timerIA As System.Windows.Forms.Timer = Nothing

    Public Sub New()

        ' create timers
        timerRR = New System.Windows.Forms.Timer
        timerRR.Interval = 17
        timerRR.Enabled = False
        AddHandler timerRR.Tick, AddressOf TimerRR_Tick
        timerIA = New System.Windows.Forms.Timer
        timerIA.Interval = 17
        timerIA.Enabled = False
        AddHandler timerIA.Tick, AddressOf TimerIA_Tick

    End Sub

    Public Reels As ReelBoxCollection = New ReelBoxCollection()
    Public Class ReelBoxCollection
        Inherits Generic.Dictionary(Of Integer, B2SReelBox)

        Public Property IsLED() As Boolean = False

        Public Shadows Sub Add(ByVal key As Integer, ByVal value As B2SReelBox)
            If Not IsLED AndAlso (value.ReelType.StartsWith("LED", StringComparison.CurrentCultureIgnoreCase) OrElse value.ReelType.StartsWith("ImportedLED", StringComparison.CurrentCultureIgnoreCase)) Then
                IsLED = True
            End If
            MyBase.Add(key, value)
        End Sub
    End Class

    Public Property StartDigit() As Integer = 0
    Public Property Digits() As Integer = 0

    Public ReadOnly Property IsInAction() As Boolean
        Get
            Dim ret As Boolean = False
            For Each reelbox As KeyValuePair(Of Integer, B2SReelBox) In Reels
                If reelbox.Value.IsInAction Then ret = True : Exit For
            Next
            Return ret
        End Get
    End Property

    Private _Score As Integer = -1
    Private _NextScore As Integer = -1
    Public Property Score() As Integer
        Get
            Return _Score
        End Get
        Set(ByVal value As Integer)
            If IsInAction Then
                If _Score <> value Then
                    _NextScore = value
                End If
            Else
                _Score = value
                SetScore(_Score)
            End If
        End Set
    End Property

    Private Sub SetScore(ByVal score As Integer, Optional ByVal startAtIndex As Integer = 0)

        If Reels.Count > 0 Then

            timerIA.Start()

            Dim scoreAsStringX As String = score.ToString("D" & Digits.ToString())

            Dim j As Integer = 1
            For i As Integer = StartDigit + Digits - startAtIndex - 1 To StartDigit Step -1
                If Reels.ContainsKey(i) Then
                    Dim reelbox As B2SReelBox = Reels(i)
                    Dim value As Integer = reelbox.CurrentText
                    Dim newvalue As Integer = CInt(scoreAsStringX.Substring(i - StartDigit, 1))
                    Dim nextReelShouldWait = (value > newvalue AndAlso score > 0)
                    Reels(i).Text(True) = CInt(scoreAsStringX.Substring(i - StartDigit, 1))
                    ' maybe get out here since the current reel is rolling over '9'
                    If nextReelShouldWait Then
                        StartTimer(i, newvalue, score, j)
                        Exit For
                    End If
                End If
                j += 1
            Next

        End If

    End Sub


    ' reel rolling timer stuff

    Dim currentindex As Integer = 0
    Dim currentnewvalue As Integer = 0
    Dim currentscore As Integer = 0
    Dim currentrestartat As Integer = 0

    Private Sub StartTimer(ByVal index As Integer, ByVal newvalue As Integer, ByVal score As Integer, ByVal restartfromright As Integer)
        currentindex = index
        currentnewvalue = newvalue
        currentscore = score
        currentrestartat = restartfromright
        timerRR.Start()
    End Sub

    Private Sub TimerRR_Tick(ByVal sender As Object, ByVal e As EventArgs)

        If currentrestartat = 0 OrElse Reels(currentindex).CurrentText <= currentnewvalue OrElse (Reels(currentindex).CurrentText >= 9 AndAlso Not Reels(currentindex).IsInReelRolling) Then

            timerRR.Stop()

            Dim restartfromright As Integer = currentrestartat
            Dim score As Integer = currentscore
            currentindex = 0
            currentnewvalue = 0
            currentscore = 0
            currentrestartat = 0
            SetScore(score, restartfromright)

        End If

    End Sub

    Private Sub TimerIA_Tick(ByVal sender As Object, ByVal e As EventArgs)

        If Not IsInAction Then

            timerIA.Stop()

            If _NextScore > 0 Then
                Dim nextscore As Integer = _NextScore
                _NextScore = 0
                StartTimer(0, 0, nextscore, 0)
            End If

        End If

    End Sub


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                On Error Resume Next
                RemoveHandler timerIA.Tick, AddressOf TimerIA_Tick
                RemoveHandler timerRR.Tick, AddressOf TimerRR_Tick
                timerIA.Stop()
                timerRR.Stop()
                timerIA.Dispose()
                timerRR.Stop()
                timerIA = Nothing
                timerRR = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
