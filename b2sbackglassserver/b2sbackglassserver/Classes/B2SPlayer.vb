Imports System

Public Class B2SPlayer

    Inherits Generic.Dictionary(Of Integer, ControlCollection)

    Public Enum eControlType
        NotDefined = 0
        LEDBox = 1
        Dream7LEDDisplay = 2
        ReelBox = 3
        ReelDisplay = 4
    End Enum

    Public Class ControlCollection

        Inherits Generic.List(Of ControlInfo)

        Public Shadows Sub Add(ByVal item As ControlInfo)
            Dim add As Boolean = True
            For Each control As ControlInfo In Me
                If control.StartDigit = item.StartDigit Then
                    add = False
                    Exit For
                End If
            Next
            If add Then
                MyBase.Add(item)
                Digits += item.Digits
            End If
        End Sub

        Public Property Digits() As Integer = 0

        Private _Score As Integer = -1
        Public Property Score() As Integer
            Get
                Return _Score
            End Get
            Set(ByVal value As Integer)
                'If _Score <> value Then
                _Score = value
                SetScore(value)
                'End If
            End Set
        End Property

        Private Sub SetScore(ByVal score As Integer)

            Dim scoreAsString As String = New String(" ", Math.Max(0, Digits - score.ToString().Length)) & score.ToString()
            If scoreAsString.Length > Digits Then
                scoreAsString = scoreAsString.Substring(scoreAsString.Length - Digits)
            End If

            For Each control As ControlInfo In Me

                ' get the part of the score
                Dim partofscore As String = scoreAsString.Substring(0, control.Digits)

                ' pass matching score part to real control
                Select Case control.Type
                    Case eControlType.LEDBox
                        ' nothing to do
                    Case eControlType.Dream7LEDDisplay
                        If Not control.LEDDisplay.Visible Then
                            For i As Integer = control.StartDigit To control.StartDigit + control.Digits - 1
                                B2SData.LEDs("LEDBox" & i.ToString()).Text = partofscore.Substring(i - control.StartDigit, 1)
                            Next
                        Else
                            For i As Integer = 0 To control.Digits - 1
                                control.LEDDisplay.SetValue(i, partofscore.Substring(i, 1))
                            Next
                        End If
                    Case eControlType.ReelBox
                        ' nothing to do
                    Case eControlType.ReelDisplay
                        control.ReelDisplay.Score = If(IsNumeric(partofscore), CInt(partofscore), 0)
                End Select

                ' remove already passed score part
                scoreAsString = scoreAsString.Substring(control.Digits)

            Next

        End Sub

    End Class
    Public Class ControlInfo

        Public StartDigit As Integer = 0
        Public Digits As Integer = 0
        Public Type As eControlType = eControlType.NotDefined
        Public LEDBox As B2SLEDBox = Nothing
        Public LEDDisplay As Dream7Display = Nothing
        Public ReelBox As B2SReelBox = Nothing
        Public ReelDisplay As B2SReelDisplay = Nothing

        Public Sub New(ByVal _startdigit As Integer, ByVal _digits As Integer, ByVal _type As eControlType, ByVal _ledbox As B2SLEDBox)
            MyNew(_startdigit, _digits, eControlType.LEDBox)
            LEDBox = _ledbox
        End Sub
        Public Sub New(ByVal _startdigit As Integer, ByVal _digits As Integer, ByVal _type As eControlType, ByVal _leddisplay As Dream7Display)
            MyNew(_startdigit, _digits, eControlType.Dream7LEDDisplay)
            LEDDisplay = _leddisplay
        End Sub
        Public Sub New(ByVal _startdigit As Integer, ByVal _digits As Integer, ByVal _type As eControlType, ByVal _reelbox As B2SReelBox)
            MyNew(_startdigit, _digits, eControlType.ReelBox)
            ReelBox = _reelbox
        End Sub
        Public Sub New(ByVal _startdigit As Integer, ByVal _digits As Integer, ByVal _type As eControlType, ByVal _reeldisplay As B2SReelDisplay)
            MyNew(_startdigit, _digits, eControlType.ReelDisplay)
            ReelDisplay = _reeldisplay
        End Sub

        Private Sub MyNew(ByVal _startdigit As Integer, ByVal _digits As Integer, ByVal _type As eControlType)
            StartDigit = _startdigit
            Digits = _digits
            Type = _type
        End Sub

    End Class

    Public Shadows Sub Add(ByVal playerno As Integer)

        Dim controls As ControlCollection = New ControlCollection()
        MyBase.Add(playerno, controls)

    End Sub

End Class

