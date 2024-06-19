Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class B2SBaseBox

    Inherits Control
    Implements System.ComponentModel.ISupportInitialize

    Public Enum eType
        NotDefined = 0
        OnBackglass = 1
        OnDMD = 2
    End Enum
    Public Property Type() As eType = eType.NotDefined

    Public Property ID() As Integer
    Public Property DisplayID() As Integer

    Public Property RomID() As Integer = 0
    Public Enum eRomIDType
        NotDefined = 0
        Lamp = 1
        Solenoid = 2
        GIString = 3
        Mech = 4
    End Enum
    Public Property RomIDType() As eRomIDType = eType.NotDefined
    Public Property RomIDValue() As Integer = 0
    Public Property RomInverted() As Boolean = False

    Public Property RectangleF As RectangleF = Nothing

    Public Property StartDigit() As Integer = 0
    Public Property Digits() As Integer = 0

    Public Property Hidden() As Boolean = False

    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit
        ' nothing to do right now
    End Sub

    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        ' nothing to do right now
    End Sub

End Class
