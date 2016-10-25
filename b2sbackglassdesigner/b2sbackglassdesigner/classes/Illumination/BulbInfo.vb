Namespace Illumination

    Public Enum eTextAlignment
        Center = 0
        Left = 1
        Right = 2
    End Enum
    Public Enum eIlluMode
        Standard = 0
        Flasher = 1
    End Enum

    Public Class BulbInfo

        Inherits InfoBase

        Public Name As String = String.Empty

        Public Text As String = String.Empty
        Public TextAlignment As eTextAlignment = eTextAlignment.Center
        Public FontName As String = String.Empty
        Public FontSize As Single = 10
        Public FontStyle As FontStyle = FontStyle.Regular

        Public Visible As Boolean = True

        Public InitialState As Integer = 0
        Public DualMode As eDualMode = eDualMode.Both

        Public LightColor As Color = DefaultLightColor
        Public DodgeColor As Color = Nothing
        Public Intensity As Integer = 1
        Public IlluMode As eIlluMode = eIlluMode.Standard

        Public ZOrder As Integer = 0

        Public IsImageSnippit As Boolean = False
        Public Image As Image = Nothing

        Public SnippitInfo As SnippitInfo = New SnippitInfo()

        Public IsIlluminatedImageDirty As Boolean = True

    End Class

    Public Class SnippitInfo

        Public SnippitType As eSnippitType = eSnippitType.StandardImage
        Public SnippitMechID As Integer = 0
        Public SnippitRotatingSteps As Integer = 0
        Public SnippitRotatingInterval As Integer = 0
        Public SnippitRotatingDirection As eSnippitRotationDirection = eSnippitRotationDirection.Clockwise
        Public SnippitRotatingStopBehaviour As eSnippitRotationStopBehaviour = eSnippitRotationStopBehaviour.SpinOff

    End Class

End Namespace