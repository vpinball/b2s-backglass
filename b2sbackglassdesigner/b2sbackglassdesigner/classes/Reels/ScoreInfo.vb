Namespace ReelAndLED

    Public Class ScoreInfo

        Inherits InfoBase

        Public ReelType As String = String.Empty
        Public ReelColor As Color = Nothing
        Public Digits As Integer = 0
        Public Spacing As Integer = 0
        Public DisplayState As eScoreDisplayState = eScoreDisplayState.Visible
        Public B2SStartDigit As Integer = 0
        Public B2SScoreType As eB2SScoreType = eB2SScoreType.NotUsed
        Public B2SPlayerNo As eB2SPlayerNo = eB2SPlayerNo.NotUsed

        Public ReelIlluLocation As eReelIlluminationLocation = eReelIlluminationLocation.Off
        Public ReelIlluB2SID As Integer = 0
        Public ReelIlluB2SIDType As eB2SIDType = eB2SIDType.NotUsed
        Public ReelIlluB2SValue As Integer = 0
        Public ReelIlluIntensity As Integer = 1

        Public SingleReelSize As SizeF = Nothing
        Public IsSingleReelSizeDirty As Boolean = True
        Public SingleReelFactor As Double = 1

        Public PerfectScaleWidthFix As Boolean = False

        ' property for internal use

        Friend numbered As Boolean = False

    End Class

End Namespace