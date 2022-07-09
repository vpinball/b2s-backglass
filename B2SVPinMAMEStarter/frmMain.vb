Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.Win32

Friend Class frmMain
	Inherits Form

	Private controller As Object
	'Private WithEvents controller As VPinMAMELib.controller

	Private tablename As String
	Private workingdir As String
	
	
	' controller events ****************************************************************
	
	Private Sub controller_OnSolenoid(ByVal nSolenoid As Integer, ByVal IsActive As Integer)
		
		' nothing to do
		
	End Sub
	
	Private Sub controller_OnStateChange(ByVal nState As Integer)
		
		' VPinMAME is stopped so unload me
		If nState = 0 Then
			Me.Close()
		End If
		
	End Sub
	
	
	' timer to read registry data ******************************************************
	
	Private Sub TimerRegistry_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TimerRegistry.Tick
		
		' timer tick is triggered
		TimerTick()
		
	End Sub
	
	
	' form loading and unloading *******************************************************
	
	Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		' set my startup stuff
		SetupMe(VB.Command())
		
		' start timer to read registry
		SetupTimer()
		
	End Sub
	
	Private Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		
		On Error Resume Next
		StopTimer()
		StopVPM()
		
	End Sub
	
	
	' private methods *****************************************************************
	
	Private Sub SetupMe(ByVal command_Renamed As String)
		
		Me.Visible = False
		
		'command = """close_encounters_fs"" ""c:\games\visual pinball\tables"""
		If Len(command_Renamed) > 0 Then
			tablename = Replace(Split(command_Renamed, """ """)(0), """", "")
			workingdir = Replace(Split(command_Renamed, """ """)(1), """", "")
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
		Dim gamename As String = String.Empty
		Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
			gamename = regkey.GetValue("GameName", "") 'gamename = GetRegX("GameName", "")
		End Using
		If Len(gamename) > 0 Then
			Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
				regkey.SetValue("GameName", "") 'SetReg("GameName", "")
			End Using
			If controller Is Nothing Then
				StartVPM(gamename)
			Else
				If Not controller.Running Then
					StartVPM(gamename)
				End If
			End If
		Else
			SetupSettings()
		End If
		
	End Sub
	
	Private Sub StartVPM(ByVal gamename As String)
		
		On Error GoTo Error_Renamed
		
		If controller Is Nothing Then
			controller = CreateObject("B2S.Server")
		End If
		
		SetupSettings()
		
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
		
		controller.Run(0)
		
		Exit Sub
		
Error_Renamed: 
		MsgBox(Err.Description)
		
	End Sub
	
	Private Sub StopVPM()
		
		If Not (controller Is Nothing) Then
			controller.Stop()
			controller = Nothing
		End If
		
	End Sub
	
	Private Sub SetupSettings()
		
		Dim ShowDMDOnly As Short
		Dim ShowTitle As Short
		Dim ShowFrame As Short
		Dim HandleKeyboard As Short
		Dim DoubleSize As Short
		Dim mode As String = String.Empty
		If Not (controller Is Nothing) Then
			
			' check some settings
			ShowDMDOnly = GetSetting("ShowDMDOnly")
			If ShowDMDOnly > -1 And controller.ShowDMDOnly <> ShowDMDOnly Then controller.ShowDMDOnly = ShowDMDOnly
			ShowTitle = GetSetting("ShowTitle")
			If ShowTitle > -1 And controller.ShowTitle <> ShowTitle Then controller.ShowTitle = ShowTitle
			ShowFrame = GetSetting("ShowFrame")
			If ShowFrame > -1 And controller.ShowFrame <> ShowFrame Then controller.ShowFrame = ShowFrame
			HandleKeyboard = GetSetting("HandleKeyboard")
			If HandleKeyboard > -1 And controller.HandleKeyboard <> HandleKeyboard Then controller.HandleKeyboard = HandleKeyboard
			DoubleSize = GetSetting("DoubleSize")
			If DoubleSize > -1 And controller.DoubleSize <> DoubleSize Then controller.DoubleSize = DoubleSize

			' maybe stop, pause or resume VPM
			Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
				mode = regkey.GetValue("Mode", "") 'mode = GetRegX("Mode", "")
			End Using
			If Len(mode) > 0 Then
				Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
					regkey.SetValue("Mode", "") 'SetReg("Mode", "")
				End Using
				If mode = "Unload" Then
					Me.Close()
				ElseIf mode = "Stop" Then 
					controller.Stop()
				ElseIf mode = "Pause" Then 
					controller.Pause = 1
				ElseIf mode = "Resume" Then 
					controller.Pause = 0
				End If
			End If
			
		End If
		
	End Sub
	
	Private Function GetSetting(ByVal settingname As String) As Short
		Dim setting As String = String.Empty
		Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
			setting = regkey.GetValue(settingname, "-1") 'setting = GetRegX(settingname, "-1")
		End Using
		If Len(setting) = 0 Then
			setting = "-1"
		End If
		If CDbl(setting) >= 0 Then
			Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\B2S\VPinMAME", True)
				regkey.SetValue(settingname, "") 'SetReg(settingname, "")
			End Using
		End If
		GetSetting = CShort(setting)
		
	End Function
End Class
