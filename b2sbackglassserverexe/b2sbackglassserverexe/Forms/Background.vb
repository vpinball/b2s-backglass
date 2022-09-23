Imports System
Imports System.Windows.Forms

Public Class Background
    Private Const WS_EX_NOACTIVATE As Integer = &H8000000L

#Region " Properties "

    ''' <summary>
    ''' This member overrides <see cref="System.Windows.Forms.Form.CreateParams">Form.CreateParams</see>.
    ''' </summary>
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim value As CreateParams = MyBase.CreateParams

            'Don't allow the window to be activated.
            If B2SSettings.FormToBack Then
                value.ExStyle = value.ExStyle Or WS_EX_NOACTIVATE
            End If
            Return value
        End Get
    End Property

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