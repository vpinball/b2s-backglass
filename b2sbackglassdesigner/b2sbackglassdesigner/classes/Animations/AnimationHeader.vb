Namespace Animation

    Public Class AnimationHeader

        Public Enum eLightsStateAtAnimationStart
            Undefined = 0
            InvolvedLightsOff = 1
            InvolvedLightsOn = 2
            LightsOff = 3
            NoChange = 4
        End Enum
        Public Enum eLightsStateAtAnimationEnd
            Undefined = 0
            InvolvedLightsOff = 1
            InvolvedLightsOn = 2
            LightsReseted = 3
            NoChange = 4
        End Enum
        Public Enum eAnimationStopBehaviour
            Undefined = 0
            StopImmediatelly = 1
            RunAnimationTillEnd = 2
            RunAnimationToFirstStep = 3
        End Enum

        Public Name As String = String.Empty

        Public DualMode As eDualMode = eDualMode.Both

        Public Interval As Integer = 0
        Public Loops As Integer = 0

        Public IDJoin As String = String.Empty

        Public StartAnimationAtBackglassStartup As Boolean = False
        Public LightsStateAtAnimationStart As eLightsStateAtAnimationStart = eLightsStateAtAnimationStart.NoChange
        Public LightsStateAtAnimationEnd As eLightsStateAtAnimationEnd = eLightsStateAtAnimationEnd.InvolvedLightsOff
        Public AnimationStopBehaviour As eAnimationStopBehaviour = eAnimationStopBehaviour.StopImmediatelly
        Public LockInvolvedLamps As Boolean = False
        Public HideScoreDisplays As Boolean = False
        Public BringToFront As Boolean = False

        Public RandomStart As Boolean = False
        Public RandomQuality As Integer = 1

        Public AnimationSteps As AnimationStepCollection = New AnimationStepCollection()

        Public Overrides Function ToString() As String
            Return Name & If(Not String.IsNullOrEmpty(IDJoin), " (" & IDJoin & ")", "")
        End Function

    End Class

End Namespace