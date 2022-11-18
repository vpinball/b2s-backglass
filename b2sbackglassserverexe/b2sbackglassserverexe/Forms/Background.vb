Imports System
Imports System.Windows.Forms

Public Class Background
    Private Const MA_NOACTIVATE As System.Int32 = 3
    Private Const WM_MOUSEACTIVATE As Integer = &H21
#Region " Properties "

    Protected Overrides Sub WndProc(ByRef m As Message)
        'Don't allow the window to be activated by swallowing the mouse event.
        If B2SSettings.FormNoFocus And m.Msg = WM_MOUSEACTIVATE Then
            m.Result = New IntPtr(MA_NOACTIVATE)
            Return
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Background_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

        'If B2SSettings.FormToFront And Me.TopMost = True Then
        '    ' We have TopMost set, so it is safe to send back!
        '    Me.SendToBack()
        '    Me.TopMost = True
        'ElseIf Me.TopMost = False Then
        '    Me.BringToFront()
        'End If

    End Sub
#End Region 'Properties
#Region "constructor"

    Public Sub New()

        InitializeComponent()

        ' set some styles
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.DoubleBuffered = True

    End Sub

#End Region

End Class