Imports System

Public Class B2SStatistics

    Private Shared Property _LogStatistics() As Boolean = False
    Public Shared Property LogStatistics() As Boolean
        Get
            Return _LogStatistics
        End Get
        Set(ByVal value As Boolean)
            _LogStatistics = value
            B2SData.IsInfoDirty = True
        End Set
    End Property

    Public Class StatsCollection

        Public List As Generic.List(Of StatsEntry)

        Public CurrentState As Integer = 0
        Public SetCount As Integer() = Nothing
        Public MaxOn As TimeSpan() = Nothing
        Public AvgOn As TimeSpan() = Nothing
        Public MinOn As TimeSpan() = Nothing

        Public Sub New()

            List = New Generic.List(Of StatsEntry)

        End Sub

        Public ReadOnly Property IsValid() As Boolean
            Get
                Return (SetCount IsNot Nothing)
            End Get
        End Property

        Public Sub Add(ByVal _State As Integer)

            CurrentState = _State

            ' maybe get last item and set time span to last entry
            Dim lastitem As StatsEntry = Nothing
            Dim now As Date = Date.Now
            If List.Count > 0 Then
                lastitem = List(List.Count - 1)
                Dim lasttime As Date = lastitem.Time
                lastitem.TimeSpan = now - lasttime
            End If

            ' check arrays
            Dim upperbound As Integer = Math.Max(_State, If(lastitem IsNot Nothing, lastitem.State, 0))
            If SetCount Is Nothing Then
                ReDim SetCount(upperbound)
                ReDim MaxOn(upperbound)
                ReDim AvgOn(upperbound)
                ReDim MinOn(upperbound)
            ElseIf upperbound > SetCount.Length - 1 Then
                ReDim Preserve SetCount(upperbound)
                ReDim Preserve MaxOn(upperbound)
                ReDim Preserve AvgOn(upperbound)
                ReDim Preserve MinOn(upperbound)
            End If

            ' set some overall stats
            SetCount(_State) += 1
            If lastitem IsNot Nothing Then
                If MaxOn(lastitem.State) = Nothing Then
                    MaxOn(lastitem.State) = lastitem.TimeSpan
                Else
                    If MaxOn(lastitem.State) < lastitem.TimeSpan Then MaxOn(lastitem.State) = lastitem.TimeSpan
                End If
                If MinOn(lastitem.State) = Nothing Then
                    MinOn(lastitem.State) = lastitem.TimeSpan
                Else
                    If MinOn(lastitem.State) > lastitem.TimeSpan Then MinOn(lastitem.State) = lastitem.TimeSpan
                End If
                If AvgOn(lastitem.State) = Nothing Then
                    AvgOn(lastitem.State) = lastitem.TimeSpan
                Else
                    AvgOn(lastitem.State) = TimeSpan.FromTicks((TimeSpan.FromTicks(AvgOn(lastitem.State).Ticks * (SetCount(lastitem.State) - 1)) + lastitem.TimeSpan).Ticks / SetCount(lastitem.State))
                End If
            End If

            ' add entry
            List.Add(New StatsEntry(_State, now))

        End Sub

    End Class
    Public Class StatsEntry
        Public State As Integer = 0
        Public Time As Date = Nothing
        Public TimeSpan As TimeSpan = Nothing

        Public Sub New(ByVal _State As Integer, ByVal _Time As Date)
            State = _State
            Time = _Time
        End Sub
    End Class

    Public Shared Property LampsStats() As Generic.SortedList(Of Integer, StatsCollection) = New Generic.SortedList(Of Integer, StatsCollection)
    Public Shared Property SolenoidsStats() As Generic.SortedList(Of Integer, StatsCollection) = New Generic.SortedList(Of Integer, StatsCollection)
    Public Shared Property GIStringsStats() As Generic.SortedList(Of Integer, StatsCollection) = New Generic.SortedList(Of Integer, StatsCollection)

    Shared Sub New()

        LogStatistics = B2SSettings.IsStatisticsBackglassOn
        
    End Sub

    Public Shared Sub ClearAll()
        LampsStats.Clear()
        SolenoidsStats.Clear()
        GIStringsStats.Clear()
    End Sub

End Class
