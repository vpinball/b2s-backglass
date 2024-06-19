Imports System.Drawing

Public Class B2SLED

    Public Enum eLEDType
        Undefined = 0
        LED8 = 1
        LED10 = 2
        LED14 = 3
        LED16 = 4
    End Enum

#Region "constructor and startup"

    Public Sub New()
        ' nothing to do
    End Sub

#End Region

#Region "standard colors"

    Private ReadOnly Property LitLEDSegmentStandardColor() As Color
        Get
            Return Color.OrangeRed
        End Get
    End Property
    Private ReadOnly Property DarkLEDSegmentStandardColor() As Color
        Get
            Return Color.FromArgb(39, 34, 31)
        End Get
    End Property

#End Region

End Class