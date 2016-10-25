VERSION 5.00
Begin VB.Form frmMain 
   Caption         =   "Form1"
   ClientHeight    =   2010
   ClientLeft      =   120
   ClientTop       =   450
   ClientWidth     =   4620
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   2010
   ScaleWidth      =   4620
   StartUpPosition =   3  'Windows Default
   WindowState     =   1  'Minimized
   Begin VB.Timer TimerRegistry 
      Left            =   600
      Top             =   1440
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private controller As Object
'Private WithEvents controller As VPinMAMELib.controller

Private tablename As String
Private workingdir As String


' controller events ****************************************************************

Private Sub controller_OnSolenoid(ByVal nSolenoid As Long, ByVal IsActive As Long)

    ' nothing to do

End Sub

Private Sub controller_OnStateChange(ByVal nState As Long)

    ' VPinMAME is stopped so unload me
    If nState = 0 Then
        Unload Me
    End If
    
End Sub


' timer to read registry data ******************************************************

Private Sub TimerRegistry_Timer()

    ' timer tick is triggered
    TimerTick
    
End Sub


' form loading and unloading *******************************************************

Private Sub Form_Load()
    
    ' set my startup stuff
    SetupMe command
    
    ' start timer to read registry
    SetupTimer
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    StopTimer
    StopVPM

End Sub


' private methods *****************************************************************

Private Sub SetupMe(ByVal command As String)

    Me.Visible = False
    
    'command = """close_encounters_fs"" ""c:\games\visual pinball\tables"""
    If Len(command) > 0 Then
        tablename = Replace(Split(command, """ """)(0), """", "")
        workingdir = Replace(Split(command, """ """)(1), """", "")
    End If
        
End Sub

Private Sub SetupTimer()

    With TimerRegistry
        .Interval = 177
        .Enabled = True
    End With

End Sub

Private Sub StopTimer()

    TimerRegistry.Enabled = False
    
End Sub

Private Sub TimerTick()

    ' get current game
    Dim gamename As String
    gamename = GetRegX("GameName", "")
    If Len(gamename) > 0 Then
        SetReg "GameName", ""
        If controller Is Nothing Then
            StartVPM gamename
        Else
            If Not controller.Running Then
                StartVPM gamename
            End If
        End If
    Else
        SetupSettings
    End If

End Sub

Private Sub StartVPM(ByVal gamename As String)

    On Error GoTo Error

    If controller Is Nothing Then
        Set controller = CreateObject("B2S.Server")
        'Set controller = CreateObject("VPinMAME.Controller")
    End If
    
    SetupSettings
    
    controller.tablename = tablename
    controller.workingdir = workingdir
    controller.HandleKeyboard = 1
    controller.gamename = gamename
    controller.SplashInfoLine = "B2S VPinMAME test app"
    controller.HandleMechanics = 255
'    controller.Dip(0) = &H40
'    controller.Dip(1) = &HC1
'    controller.Dip(2) = &HCB
'    controller.Dip(3) = &H3F

    controller.Run 0

    Exit Sub
    
Error:
    MsgBox Err.Description

End Sub

Private Sub StopVPM()

    If Not (controller Is Nothing) Then
        controller.Stop
        controller = Nothing
    End If

End Sub

Private Sub SetupSettings()

    If Not (controller Is Nothing) Then
        
        ' check some settings
        Dim ShowDMDOnly As Integer
        ShowDMDOnly = GetSetting("ShowDMDOnly")
        If ShowDMDOnly > -1 And controller.ShowDMDOnly <> ShowDMDOnly Then controller.ShowDMDOnly = ShowDMDOnly
        Dim ShowTitle As Integer
        ShowTitle = GetSetting("ShowTitle")
        If ShowTitle > -1 And controller.ShowTitle <> ShowTitle Then controller.ShowTitle = ShowTitle
        Dim ShowFrame As Integer
        ShowFrame = GetSetting("ShowFrame")
        If ShowFrame > -1 And controller.ShowFrame <> ShowFrame Then controller.ShowFrame = ShowFrame
        Dim HandleKeyboard As Integer
        HandleKeyboard = GetSetting("HandleKeyboard")
        If HandleKeyboard > -1 And controller.HandleKeyboard <> HandleKeyboard Then controller.HandleKeyboard = HandleKeyboard
        Dim DoubleSize As Integer
        DoubleSize = GetSetting("DoubleSize")
        If DoubleSize > -1 And controller.DoubleSize <> DoubleSize Then controller.DoubleSize = DoubleSize
        
        ' maybe stop, pause or resume VPM
        Dim mode As String
        mode = GetRegX("Mode", "")
        If Len(mode) > 0 Then
            SetReg "Mode", ""
            If mode = "Unload" Then
                Unload Me
            ElseIf mode = "Stop" Then
                controller.Stop
            ElseIf mode = "Pause" Then
                controller.Pause = 1
            ElseIf mode = "Resume" Then
                controller.Pause = 0
            End If
        End If
        
    End If
    
End Sub

Private Function GetSetting(ByVal settingname As String) As Integer
    
    Dim setting As String
    setting = GetRegX(settingname, "-1")
    If Len(setting) = 0 Then
        setting = "-1"
    End If
    If setting >= 0 Then
        SetReg settingname, ""
    End If
    GetSetting = CInt(setting)

End Function
