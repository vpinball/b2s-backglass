﻿Imports System

Public Class Log

    Private writeLog As Boolean = False

    Private filename As String = String.Empty

    Public Sub New(ByVal _filename As String)
        filename = _filename & ".txt"
    End Sub

    Public Property IsLogOn() As Boolean
        Get
            Return writeLog
        End Get
        Set(value As Boolean)
            writeLog = value
        End Set
    End Property

    Public Property LogPath() As String = B2SSettings.LogPath

    Public Sub WriteLogEntry(ByVal text As String)
        If writeLog AndAlso Not String.IsNullOrEmpty(LogPath) Then
            ' write to log file
            On Error Resume Next
            Dim log As IO.StreamWriter = New IO.StreamWriter(IO.Path.Combine(LogPath, filename), True)
            log.WriteLine(text)
            log.Flush()
            log.Close()
        End If
    End Sub

End Class
