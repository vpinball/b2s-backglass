Public Enum eTableType
    NotDefined = 0
    EM = 1
    SS = 2
    SSDMD = 3
    ORI = 4
End Enum

Public Enum eDMDType
    NotDefined = 0
    NoB2SDMD = 1
    B2SAlwaysOnSecondMonitor = 2
    B2SAlwaysOnThirdMonitor = 3
    B2SOnSecondOrThirdMonitor = 4
End Enum

Public Enum eCommType
    NotDefined = 0
    Rom = 1
    B2S = 2
End Enum

Public Enum eDestType
    NotDefined = 0
    DirectB2S = 1
    VisualStudio2010 = 2
End Enum

Public Enum eImageSetType
    NotDefined = 0
    ReelImages = 1
    CreditReelImages = 2
    LEDImages = 3
End Enum

Public Enum eParentForm
    NotDefined = 0
    Backglass = 1
    DMD = 2
End Enum

Public Enum eB2SScoreType
    NotUsed = 0
    Scores_01 = 1
    Credits_29 = 2
End Enum

Public Enum eB2SPlayerNo
    NotUsed = 0
    Player1 = 1
    Player2 = 2
    Player3 = 3
    Player4 = 4
End Enum

Public Enum eScoreDisplayState
    Visible = 0
    Hidden = 1
End Enum

Public Enum eB2SIDType
    NotUsed = 0
    ScoreRolloverPlayer1_25 = 1
    ScoreRolloverPlayer2_26 = 2
    ScoreRolloverPlayer3_27 = 3
    ScoreRolloverPlayer4_28 = 4
    PlayerUp_30 = 5
    CanPlay_31 = 6
    BallInPlay_32 = 7
    Tilt_33 = 8
    Match_34 = 9
    GameOver_35 = 10
    ShootAgain_36 = 11
End Enum

Public Enum eRomIDType
    NotUsed = 0
    Lamp = 1
    Solenoid = 2
    GIString = 3
End Enum

Public Enum eDualMode
    Both = 0
    Authentic = 1
    Fantasy = 2
End Enum

Public Enum eSnippitType
    StandardImage = 0
    SelfRotatingImage = 1
    MechRotatingImage = 2
End Enum

Public Enum eSnippitRotationDirection
    Clockwise = 0
    AntiClockwise = 1
End Enum

Public Enum eSnippitRotationStopBehaviour
    SpinOff = 0
    StopImmediatelly = 1
    RunAnimationTillEnd = 2
    RunAnimationToFirstStep = 3
End Enum

Public Enum eReelIlluminationLocation
    Off = 0
    Above = 1
    Below = 2
    AboveAndBelow = 3
End Enum

Public Enum eReelRollingDirection
    Up = 0
    Down = 1
End Enum