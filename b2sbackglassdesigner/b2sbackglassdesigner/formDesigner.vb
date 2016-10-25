Imports System

Public Class formDesigner

    Private zooms As String() = New String() {"500", "450", "400", "350", "300", "275", "250", "225", "200", "175", "150", "125", "110", "100", "90", "80", "75", "70", "67", "60", "50", "40", "33", "30", "25", "20", "10", "5"}

    Private WithEvents formToolReelsAndLEDs As formToolReelsAndLEDs = New formToolReelsAndLEDs()
    Private WithEvents formToolIllumination As formToolIllumination = New formToolIllumination()
    Private WithEvents formToolUndo As formToolUndo = New formToolUndo()
    Private WithEvents formToolResources As formToolResources = New formToolResources()

    Private WithEvents formAddSnippit As formAddSnippit = New formAddSnippit()
    Private WithEvents formAnimations As formAnimations = New formAnimations()
    Private WithEvents formVPM As formVPM = New formVPM()

    Private save As Save = New Save()
    Private recent As Recent = New Recent()
    Private WithEvents coding As Coding = New Coding()

    Private WithEvents UndoEvents As Undo = New Undo()

    Private ignoreChanges As Boolean = False


#Region "constructor"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MyBase.SaveName = Me.Name

    End Sub

#End Region


#Region "events"

    Private Sub formDesigner_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' start app title and status bar
        Me.Text = Headline
        'Me.KeyPreview = True
        ShowStatus()

        ' initialize some values
        For Each zoom As String In zooms
            tscmbZoomInPercent.Items.Add(zoom & "%")
        Next
        tscmbZoomInPercent.Items.Add(My.Resources.TXT_ZoomWindow)
        tscmbZoomInPercent.Text = "100%"

        ' listbox to undo
        Undo.ListBox = formToolUndo.lbHistory

    End Sub
    Private Sub formDesigner_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

        ' open up the tool windows
        For Each formName As String In MyBase.StartToolForms
            Select Case formName
                Case "formToolReelsAndLEDs"
                    CheckToolReelsAndLEDsForm()
                    ShowToolReelsAndLEDsForm()
                Case "formToolIllumination"
                    CheckToolIlluminationForm()
                    ShowToolIlluminationForm()
                Case "formToolUndo"
                    CheckToolUndoForm()
                    ShowToolUndoForm()
                Case "formToolResources"
                    CheckToolResourcesForm()
                    ShowToolResourcesForm()
            End Select
        Next
        Me.Focus()
        Me.BringToFront()

        ' check saving
        Try
            Dim XmlSettings As Xml.XmlDocument = New Xml.XmlDocument
            If IO.File.Exists(SettingsFileName) Then
                XmlSettings.Load(SettingsFileName)
                XmlSettings.Save(SettingsFileName)
            End If
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.MSG_StartupSaveError, ex.Message), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub formDesigner_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Do While B2STab.TabPages.Count > 0
            B2STab.SelectedIndex = B2STab.TabPages.Count - 1
            B2STab.Refresh()
            If B2STab.RemoveBackglass(B2STab.SelectedIndex) = Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
                Exit Do
            End If
        Loop
    End Sub

    Private Sub B2STab_LightsReportProgress(sender As Object, e As Illumination.Lights.LightsProgressEventArgs) Handles B2STab.LightsReportProgress
        ShowProgress(e.Progress)
    End Sub

    Private Sub B2STab_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles B2STab.SelectedIndexChanged
        RefreshSettings()
        LockUnlockMenus()
    End Sub
    Private Sub B2STab_MyMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles B2STab.MouseDown
        LoadToolReelsAndLEDsForm()
    End Sub
    Private Sub B2STab_MyMouseMove(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs) Handles B2STab.MouseMove
        UpdateStatusBar4Mouse(Me, Backglass.currentTabPage, New Point(e.Location.X, e.Location.Y))
    End Sub
    Private Sub B2STab_CopyDMDCopyArea(sender As Object, e As System.EventArgs) Handles B2STab.CopyDMDCopyArea
        CopyDMDArea()
    End Sub
    Private Sub B2STab_SelectedItemClicked(sender As Object, e As Mouse.MouseMoveEventArgs) Handles B2STab.SelectedItemClicked
        ' load tool window info
        If e.ItemType = Mouse.MouseMoveEventArgs.eItemType.Score Then
            LoadToolReelsAndLEDsForm()
        ElseIf e.ItemType = Mouse.MouseMoveEventArgs.eItemType.Bulb Then
            LoadToolIlluminationForm()
        End If
        ' maybe communicate with animation window
        If formAnimations IsNot Nothing AndAlso formAnimations.Visible Then
            If e.ItemType = Mouse.MouseMoveEventArgs.eItemType.Bulb AndAlso TypeOf sender Is Mouse Then
                With DirectCast(sender, Mouse)
                    formAnimations.BulbClicked(.SelectedBulb.Name, .SelectedBulb.ID)
                End With
            End If
        End If
        ' set focus to tab
        B2STab.Focus()
    End Sub
    Private Sub B2STab_SelectedItemMoving(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs) Handles B2STab.SelectedItemMoving
        If e.ItemType = Mouse.MouseMoveEventArgs.eItemType.Score Then
            If formToolReelsAndLEDs IsNot Nothing Then
                formToolReelsAndLEDs.ignoreChange = True
                formToolReelsAndLEDs.txtLocationX.Text = e.Location.X.ToString()
                formToolReelsAndLEDs.txtLocationY.Text = e.Location.Y.ToString()
                formToolReelsAndLEDs.txtSizeWidth.Text = e.Size.Width.ToString()
                formToolReelsAndLEDs.txtSizeHeight.Text = e.Size.Height.ToString()
                formToolReelsAndLEDs.ignoreChange = False
            End If
        ElseIf e.ItemType = Mouse.MouseMoveEventArgs.eItemType.Bulb Then
            If formToolIllumination IsNot Nothing Then
                formToolIllumination.ignoreChange = True
                formToolIllumination.txtLocationX.Text = e.Location.X.ToString()
                formToolIllumination.txtLocationY.Text = e.Location.Y.ToString()
                formToolIllumination.txtSizeWidth.Text = e.Size.Width.ToString()
                formToolIllumination.txtSizeHeight.Text = e.Size.Height.ToString()
                formToolIllumination.ignoreChange = False
            End If
        End If
        'B2STab.Focus()
    End Sub
    Private Sub B2STab_SelectedBulbMoved(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles B2STab.SelectedBulbMoved
        If Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentTabPage.ShowIllumination Then
            Backglass.currentTabPage.RefreshIllumination()
        End If
    End Sub
    Private Sub B2STab_SelectedBulbEdited(sender As Object, e As System.EventArgs) Handles B2STab.SelectedBulbEdited
        If Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentTabPage.Mouse.SelectedBulb IsNot Nothing Then
            If formToolIllumination IsNot Nothing Then
                formToolIllumination.TrackBarIntensity.Value = Backglass.currentTabPage.Mouse.SelectedBulb.Intensity
                formToolIllumination.cmbDodgeColor.SelectedIndex = TranslateDodgeColor2Index(Backglass.currentTabPage.Mouse.SelectedBulb.DodgeColor)
            End If
        End If
    End Sub
    Private Sub B2STab_LightColorChanged(sedner As Object, e As Illumination.Lights.LightColorChangedEventArgs) Handles B2STab.LightColorChanged
        If Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentTabPage.Mouse.SelectedBulb IsNot Nothing Then
            If formToolIllumination IsNot Nothing Then
                formToolIllumination.btnLightColor.BackColor = Backglass.currentTabPage.Mouse.SelectedBulb.LightColor
            End If
        End If
    End Sub

    Private Sub ProgressReset_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProgressReset.Tick
        timerProgressReset.Stop()
        tsProgress.Value = 0
        tsProgress.Visible = False
    End Sub

    Private Sub Coding_ReportProgress(sender As Object, e As Coding.CodingProgressEventArgs) Handles coding.ReportProgress
        ShowProgress(e.Progress)
    End Sub

#Region "tool form disposing (important!!!)"

    Private Sub ToolReelsAndLEDs_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles formToolReelsAndLEDs.Disposed
        formToolReelsAndLEDs = Nothing
    End Sub
    Private Sub ToolIllumination_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles formToolIllumination.Disposed
        formToolIllumination = Nothing
    End Sub
    Private Sub ToolUndo_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles formToolUndo.Disposed
        formToolUndo = Nothing
    End Sub
    Private Sub ToolResources_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles formToolResources.Disposed
        formToolResources = Nothing
    End Sub

    Private Sub AddSnippit_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles formAddSnippit.Disposed
        formAddSnippit = Nothing
    End Sub

    Private Sub Animations_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles formAnimations.Disposed
        formAnimations = Nothing
    End Sub

    Private Sub VPM_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles formVPM.Disposed
        formVPM = Nothing
    End Sub

#End Region

#Region "data events of the tool windows"

    Private Sub formToolReelsAndLEDs_DataChanged(ByVal sender As Object, ByVal e As formToolReelsAndLEDs.ScoreEventArgs) Handles formToolReelsAndLEDs.DataChanged
        If NoToolEvents Then Return
        If Backglass.currentTabPage IsNot Nothing Then
            Select Case e.TypeOfData
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.NumberOfPlayers
                    Backglass.currentTabPage.ReelsAndLEDs_SetNumberOfPlayers(CInt(e.Data))
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.B2SStartDigit
                    Backglass.currentTabPage.ReelsAndLEDs_SetB2SStartDigit(e.Data)
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.B2SScoreType
                    Backglass.currentTabPage.ReelsAndLEDs_SetB2SScoreType(e.Data)
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.B2SPlayerNo
                    Backglass.currentTabPage.ReelsAndLEDs_SetB2SPlayerNo(e.Data)
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.NumberOfDigits
                    Backglass.currentTabPage.ReelsAndLEDs_SetDigits(CInt(e.Data))
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.Spacing
                    Backglass.currentTabPage.ReelsAndLEDs_SetSpacing(CInt(e.Data))
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.Location
                    Backglass.currentTabPage.ReelsAndLEDs_SetLocation(e.Data)
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.Size
                    Backglass.currentTabPage.ReelsAndLEDs_SetSize(e.Data)
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.DisplayState
                    Backglass.currentTabPage.ReelsAndLEDs_SetState(e.Data)
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.ReelType
                    Backglass.currentTabPage.ReelsAndLEDs_SetReelType(e.Data)
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.UseDream7LEDs
                    Backglass.currentTabPage.ReelsAndLEDs_SetDream7LEDs(e.Data)
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.PerfectScaleWidthFix
                    Backglass.currentTabPage.ReelsAndLEDs_PerfectScaleWidthFix()
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.ChangeLEDColor
                    Backglass.currentTabPage.ReelsAndLEDs_ChangeLEDColor()
                Case B2SBackglassDesigner.formToolReelsAndLEDs.eScoreDataType.ReelIllumination
                    Backglass.currentTabPage.ReelsAndLEDs_ReelIllumination()
            End Select
        End If
    End Sub
    Private Sub formToolIllumination_DataChanged(ByVal sender As Object, ByVal e As formToolIllumination.IlluminationEventArgs) Handles formToolIllumination.DataChanged
        If NoToolEvents Then Return
        If Backglass.currentTabPage IsNot Nothing Then
            Select Case e.TypeOfData
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.Name
                    Backglass.currentTabPage.Illumination_SetName(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.ID
                    Backglass.currentTabPage.Illumination_SetID(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.B2SID
                    Backglass.currentTabPage.Illumination_SetB2SID(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.B2SIDType
                    Backglass.currentTabPage.Illumination_SetB2SIDType(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.B2SValue
                    Backglass.currentTabPage.Illumination_SetB2SValue(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.RomID
                    Backglass.currentTabPage.Illumination_SetRomID(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.RomIDType
                    Backglass.currentTabPage.Illumination_SetRomIDType(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.RomInverted
                    Backglass.currentTabPage.Illumination_SetRomInverted(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.InitialState
                    Backglass.currentTabPage.Illumination_SetInitialState(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.DualMode
                    Backglass.currentTabPage.Illumination_SetDualMode(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.Intensity
                    Backglass.currentTabPage.Illumination_SetIntensity(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.DodgeColor
                    Backglass.currentTabPage.Illumination_DodgeColor(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.IlluMode
                    Backglass.currentTabPage.Illumination_IlluMode(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.Illuminationtext
                    Backglass.currentTabPage.Illumination_SetText(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.IlluminationtextAlignment
                    Backglass.currentTabPage.Illumination_SetTextAlignment(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.IlluminationtextFont
                    Backglass.currentTabPage.Illumination_SetTextFont(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.ChangeLightColor
                    Backglass.currentTabPage.Illumination_ChangeLightColor()
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.Location
                    Backglass.currentTabPage.Illumination_SetLocation(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.Size
                    Backglass.currentTabPage.Illumination_SetSize(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.ZOrder
                    Backglass.currentTabPage.Illumination_SetZOrder(e.Data)
                Case B2SBackglassDesigner.formToolIllumination.eIlluminationDataType.SnippitInfo
                    Backglass.currentTabPage.Illumination_SetSnippitInfo(e.Data)
            End Select
            ' refresh rom filter
            RefreshIDFilter()
        End If
    End Sub
    Private Sub formToolResources_DataChanged(ByVal sender As Object, ByVal e As formToolResources.ImagesEventArgs) Handles formToolResources.DataChanged
        If NoToolEvents Then Return
        If Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentData IsNot Nothing Then
            Select Case e.TypeOfData
                Case B2SBackglassDesigner.formToolResources.eImagesDataType.BackgroundImageRemoved
                    Backglass.currentData.IsDirty = True

                Case B2SBackglassDesigner.formToolResources.eImagesDataType.IlluminationImageRemoved
                    Backglass.currentData.IsDirty = True
                Case B2SBackglassDesigner.formToolResources.eImagesDataType.DMDImageRemoved
                    Backglass.currentData.IsDirty = True
                    Dim currentDMDImageInfo As Images.ImageInfo = Backglass.currentData.Images.CurrentDMDImageInfo()
                    If currentDMDImageInfo Is Nothing Then
                        Backglass.currentTabPage.DMDImage = Nothing
                    Else
                        Backglass.currentTabPage.DMDImage(currentDMDImageInfo.Text) = currentDMDImageInfo.Image
                        Backglass.currentTabPage.Zoom(tscmbZoomInPercent.Text)
                        tscmbImage.SelectedIndex = 1
                    End If
                Case B2SBackglassDesigner.formToolResources.eImagesDataType.BackgroundImageSelectionChanged
                    Undo.AddEntry(New Undo.UndoEntry(Undo.Type.ImageChanged, Backglass.currentData.Image))
                    If MessageBox.Show(My.Resources.MSG_BackgroundImageChange, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        Backglass.currentBulbs.Resize(Backglass.currentData.Image.Size, e.Data.Image.Size)
                        Backglass.currentScores.Resize(Backglass.currentData.Image.Size, e.Data.Image.Size)
                        Dim factor As Single = Backglass.currentData.Image.Size.Height / e.Data.Image.Size.Height
                        Backglass.currentData.GrillHeight = Backglass.currentData.GrillHeight / factor
                        Backglass.currentData.SmallGrillHeight = Backglass.currentData.SmallGrillHeight / factor
                    End If
                    Backglass.currentTabPage.Image(e.Data.Text) = e.Data.Image
                    Backglass.currentTabPage.Zoom(tscmbZoomInPercent.Text)
                    tscmbImage.SelectedIndex = 0
                    UpdateStatusBar(Me, Backglass.currentTabPage)
                Case B2SBackglassDesigner.formToolResources.eImagesDataType.IlluminatedImageSelectionChanged

                    e.Data.Selected = True

                Case B2SBackglassDesigner.formToolResources.eImagesDataType.DMDImageSelectionChanged
                    Undo.AddEntry(New Undo.UndoEntry(Undo.Type.DMDImageChanged, Backglass.currentData.DMDImage))
                    Backglass.currentTabPage.DMDImage(e.Data.Text) = e.Data.Image
                    Backglass.currentTabPage.Zoom(tscmbZoomInPercent.Text)
                    tscmbImage.SelectedIndex = 1
                    UpdateStatusBar(Me, Backglass.currentTabPage)
                Case B2SBackglassDesigner.formToolResources.eImagesDataType.BackgroundImageTypeChanged
                    Backglass.currentData.IsDirty = True
                Case B2SBackglassDesigner.formToolResources.eImagesDataType.BackgroundImageRomIDChanged
                    Backglass.currentData.IsDirty = True
                Case B2SBackglassDesigner.formToolResources.eImagesDataType.BackgroundImageRomIDTypeChanged
                    Backglass.currentData.IsDirty = True
            End Select
        End If
    End Sub

    Private Sub formAddSnippit_SnippitAdded(sender As Object, e As formAddSnippit.AddSnippitEventArgs) Handles formAddSnippit.SnippitAdded
        Backglass.currentTabPage.Illumination_AddSnippit(e.Name, e.Image, e.Location)
    End Sub

    Private Sub formAnimations_ResetAnimationLights(sender As Object, e As System.EventArgs) Handles formAnimations.ResetAnimationLights
        Backglass.currentTabPage.ResetAnimationLights()
    End Sub
    Private Sub formAnimations_ShowAnimation(sender As Object, e As formAnimations.ShowAnimationEventArgs) Handles formAnimations.ShowAnimation
        Backglass.currentTabPage.ShowAnimation(e.Name)
    End Sub

#End Region

#End Region


#Region "menu stuff"

#Region "file menu"

    Private Sub New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbNew.Click, tsmiNew.Click
        OpenSettings(True)
    End Sub
    Private Sub Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiOpen.Click, tsbOpen.Click
        Using filedialog As OpenFileDialog = New OpenFileDialog
            With filedialog
                .Filter = "B2S project file (*.b2s)|*.b2s|ALL (*.*)|*.*"
                .FileName = String.Empty
                .InitialDirectory = BackglassProjectsPath
                If .ShowDialog(Me) = DialogResult.OK Then
                    Dim backglassdata As Backglass.Data = Nothing
                    Cursor.Current = Cursors.WaitCursor
                    save.LoadData(backglassdata, .FileName)
                    Cursor.Current = Cursors.Default
                    If backglassdata IsNot Nothing Then
                        If backglassdata.IsBackup Then
                            If MessageBox.Show(My.Resources.MSG_BackupFile, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                Exit Sub
                            End If
                        End If
                        LoadData(backglassdata)
                    End If
                End If
            End With
            ShowStatus()
        End Using
        LockUnlockMenus()
    End Sub
    Private Sub OpenBackup_Click(sender As System.Object, e As System.EventArgs) Handles tsmiOpenBackup.Click
        Using filedialog As OpenFileDialog = New OpenFileDialog
            With filedialog
                .Filter = "B2S backup file (*.b2b)|*.b2b|ALL (*.*)|*.*"
                .FileName = String.Empty
                .InitialDirectory = BackglassProjectsPath
                If .ShowDialog(Me) = DialogResult.OK Then
                    Dim backglassdata As Backglass.Data = Nothing
                    Cursor.Current = Cursors.WaitCursor
                    save.LoadData(backglassdata, .FileName)
                    Cursor.Current = Cursors.Default
                    If backglassdata IsNot Nothing Then
                        If backglassdata.IsBackup Then
                            LoadData(backglassdata)
                        Else
                            MessageBox.Show(My.Resources.MSG_NoBackupFile, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If
                End If
            End With
            ShowStatus()
        End Using
        LockUnlockMenus()
    End Sub

    Private Sub Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiClose.Click
        B2STab.RemoveBackglass(B2STab.SelectedIndex)
        ShowStatus()
        LockUnlockMenus()
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiSave.Click, tsbSave.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Dim doStandardSave As Boolean = True
            Dim doBackupSave As Boolean = False
            If Backglass.currentData.IsBackup Then
                Dim ret As DialogResult = MessageBox.Show(My.Resources.MSG_HowToSave1, AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If ret = Windows.Forms.DialogResult.Cancel Then
                    doStandardSave = False
                ElseIf ret = Windows.Forms.DialogResult.Yes Then
                    doBackupSave = True
                Else
                    ret = MessageBox.Show(My.Resources.MSG_HowToSave2, AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If ret = Windows.Forms.DialogResult.Cancel OrElse ret = Windows.Forms.DialogResult.No Then
                        doStandardSave = False
                    End If
                End If
            End If
            Cursor.Current = Cursors.WaitCursor
            If doBackupSave Then
                save.SaveData(Backglass.currentData, Backglass.currentData.BackupName)
            ElseIf doStandardSave Then
                Backglass.currentData.BackupName = String.Empty
                save.SaveData(Backglass.currentData, , recent)
            End If
            Cursor.Current = Cursors.Default
        Else
            tsmiNew.PerformClick()
        End If
        LockUnlockMenus()
    End Sub
    Private Sub SaveBackupAs_Click(sender As System.Object, e As System.EventArgs) Handles tsmiSaveBackupAs.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Dim backupname As String = String.Empty
            If formBackup.ShowDialog(Me, backupname) = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(backupname) Then
                    Cursor.Current = Cursors.WaitCursor
                    save.SaveData(Backglass.currentData, backupname)
                    Cursor.Current = Cursors.Default
                End If
            End If
        End If
    End Sub
    Private Sub SaveAs_Click(sender As System.Object, e As System.EventArgs) Handles tsmiSaveAs.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Dim size As Size = Backglass.currentData.Image.Size
            If OpenSettings(True, True) Then
                Backglass.currentData.LoadedName = String.Empty
                Cursor.Current = Cursors.WaitCursor
                save.SaveData(Backglass.currentData)
                Cursor.Current = Cursors.Default
                If MessageBox.Show(My.Resources.MSG_SaveAsAndImportNewImage, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    ImportBackgroundImage(size)
                    recent.ResetThumbnailImage(Backglass.currentData)
                End If
            End If
        End If
    End Sub
    Private Sub SaveAll_Click(sender As System.Object, e As System.EventArgs) Handles tsmiSaveAll.Click
        For Each tabpage As B2STabPage In B2STab.TabPages
            If tabpage.BackglassData IsNot Nothing Then
                Cursor.Current = Cursors.WaitCursor
                If String.IsNullOrEmpty(tabpage.BackglassData.BackupName) Then
                    save.SaveData(tabpage.BackglassData, , recent)
                Else
                    save.SaveData(tabpage.BackglassData, tabpage.BackglassData.BackupName)
                End If
                Cursor.Current = Cursors.Default
            End If
        Next
    End Sub

    Private Sub Settings_Click(sender As System.Object, e As System.EventArgs) Handles tsmiSettings.Click
        If Backglass.currentTabPage IsNot Nothing Then
            OpenSettings(False)
        End If
    End Sub

    Private Sub ImportBackglassFile_Click(sender As System.Object, e As System.EventArgs) Handles tsmiImportBackglassFile.Click
        Using filedialog As OpenFileDialog = New OpenFileDialog
            With filedialog
                .Filter = "'directB2S' backglass file (*.directb2s)|*.directb2s|ALL (*.*)|*.*"
                .FileName = String.Empty
                .InitialDirectory = BackglassProjectsPath
                If .ShowDialog(Me) = DialogResult.OK Then
                    Dim backglassdata As Backglass.Data = Nothing
                    Cursor.Current = Cursors.WaitCursor
                    Try
                        coding.ImportDirectB2SFile(backglassdata, .FileName)
                        If backglassdata IsNot Nothing Then
                            LoadData(backglassdata)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(My.Resources.MSG_ImportError2, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Cursor.Current = Cursors.Default
                End If
            End With
            ShowStatus()
        End Using
        LockUnlockMenus()
    End Sub

    Private Sub Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiExit.Click
        Me.Close()
    End Sub

    ' open recent stuff
    Private Sub OpenRecent_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmiOpenRecent.DropDownOpening
        If recent.IsDirty Then
            Do While True
                Dim found As Boolean = False
                For Each menuitem As ToolStripItem In tsmiOpenRecent.DropDownItems
                    If TypeOf menuitem Is ToolStripMenuItem Then
                        If menuitem.Name.StartsWith("recent") Then
                            found = True
                            tsmiOpenRecent.DropDownItems.Remove(menuitem)
                            menuitem.Image = Nothing
                            menuitem.Dispose()
                            Exit For
                        End If
                    End If
                Next
                If Not found Then Exit Do
            Loop
            Dim i As Integer = recent.recentEntries.Count
            For Each recentEntry As KeyValuePair(Of Integer, Recent.recentEntry) In recent.recentEntries
                With recentEntry.Value
                    Dim newTSMI As ToolStripMenuItem = New ToolStripMenuItem(If(i = 10, "1&0 ", "&" & i.ToString() & " ") & .Name, .ThumbnailImage, AddressOf OpenRecent_ChildClick)
                    newTSMI.Name = "recent" & recentEntry.Key
                    newTSMI.Tag = recentEntry
                    newTSMI.ImageScaling = ToolStripItemImageScaling.None
                    tsmiOpenRecent.DropDownItems.Insert(0, newTSMI)
                End With
                i -= 1
            Next
            recent.IsDirty = False
        End If
    End Sub
    Private Sub OpenRecent_ChildClick(ByVal sender As Object, ByVal e As EventArgs)
        If TypeOf sender.Tag Is KeyValuePair(Of Integer, Recent.recentEntry) Then
            Dim path As String = BackglassProjectsPath
            Dim backglassdata As Backglass.Data = Nothing
            Cursor.Current = Cursors.WaitCursor
            If save.LoadData(backglassdata, IO.Path.Combine(path, sender.Tag.Value.Name) & ".b2s") Then
                LoadData(backglassdata)
            Else
                MessageBox.Show(My.Resources.MSG_NoProjectFileFound, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                recent.RemoveFromRecentList(sender.Tag.Key)
            End If
            Cursor.Current = Cursors.Default
        End If
        ShowStatus()
    End Sub

    Private Sub ClearThisList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiClearThisList.Click
        recent.RemoveAllFromRecentList()
    End Sub

#End Region

#Region "edit"

    Private Sub Undo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiUndo.Click, tsbUndo.Click
        Undo.Undo()
        UpdateStatusBar(Me, Backglass.currentTabPage)
    End Sub
    Private Sub Redo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiRedo.Click, tsbRedo.Click
        Undo.Redo()
        UpdateStatusBar(Me, Backglass.currentTabPage)
    End Sub

    Private Sub Cut_Click(sender As System.Object, e As System.EventArgs) Handles tsmiCut.Click
        MessageBox.Show("Will be implemented in one of the next versions.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Copy_Click(sender As System.Object, e As System.EventArgs) Handles tsmiCopy.Click
        MessageBox.Show("Will be implemented in one of the next versions.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Paste_Click(sender As System.Object, e As System.EventArgs) Handles tsmiPaste.Click
        MessageBox.Show("Will be implemented in one of the next versions.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub tsmiDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsmiDelete.Click
        If Backglass.currentTabPage.Mouse.SelectedItem IsNot Nothing Then
            Backglass.currentTabPage.Mouse.KeyIsPressed(False, Keys.Delete)
        End If
    End Sub

#End Region

#Region "view"

    Private zoomAtWindow As Integer = 100

    Private Sub ZoomIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbZoomIn.Click, tsmiZoomIn.Click
        If Backglass.currentTabPage IsNot Nothing Then
            If tscmbZoomInPercent.Text.Equals("Window", StringComparison.CurrentCultureIgnoreCase) Then
                tscmbZoomInPercent.Text = zoomAtWindow.ToString() & "%"
            End If
            If IsNumeric(tscmbZoomInPercent.Text.Replace("%", "")) Then
                Dim currentzoom As Integer = CInt(tscmbZoomInPercent.Text.Replace("%", ""))
                Dim lastzoom As Integer = CInt(zooms(0))
                For Each zoom As String In zooms
                    If CInt(zoom) <= currentzoom Then
                        'currentBackglass.Zoom(lastzoom)
                        tscmbZoomInPercent.Text = lastzoom.ToString() & "%"
                        Exit For
                    End If
                    lastzoom = CInt(zoom)
                Next
            End If
        End If
    End Sub
    Private Sub ZoomOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbZoomOut.Click, tsmiZoomOut.Click
        If Backglass.currentTabPage IsNot Nothing Then
            If tscmbZoomInPercent.Text.Equals("Window", StringComparison.CurrentCultureIgnoreCase) Then
                tscmbZoomInPercent.Text = zoomAtWindow.ToString() & "%"
            End If
            If IsNumeric(tscmbZoomInPercent.Text.Replace("%", "")) Then
                Dim currentzoom As Integer = CInt(tscmbZoomInPercent.Text.Replace("%", ""))
                For Each zoom As String In zooms
                    If CInt(zoom) < currentzoom Then
                        'currentBackglass.Zoom(CInt(zoom))
                        tscmbZoomInPercent.Text = CInt(zoom).ToString() & "%"
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub ZoomToWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiZoomToWindow.Click
        If Backglass.currentTabPage IsNot Nothing Then
            If tscmbZoomInPercent.SelectedIndex <> tscmbZoomInPercent.Items.Count - 1 Then
                tscmbZoomInPercent.SelectedIndex = tscmbZoomInPercent.Items.Count - 1
            Else
                tscmbZoomInPercent.Text = My.Resources.TXT_ZoomWindow
                ZoomInPercent_SelectedIndexChanged(Me, New EventArgs())
            End If
        End If
    End Sub

    Private Sub ZoomActualSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiZoomActualSize.Click
        If Backglass.currentTabPage IsNot Nothing Then
            tscmbZoomInPercent.Text = "100%"
        End If
    End Sub

    Private Sub ZoomInPercent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tscmbZoomInPercent.SelectedIndexChanged
        If Backglass.currentTabPage IsNot Nothing Then
            If IsNumeric(tscmbZoomInPercent.Text.Replace("%", "")) Then
                Backglass.currentTabPage.Zoom(CInt(tscmbZoomInPercent.Text.Replace("%", "")))
            ElseIf tscmbZoomInPercent.SelectedIndex = tscmbZoomInPercent.Items.Count - 1 Then
                Backglass.currentTabPage.Zoom("Window")
                zoomAtWindow = Backglass.currentData.Zoom
            End If
            Me.VerticalScroll.Value = 0
        End If
    End Sub
    Private Sub ZoomInPercent_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tscmbZoomInPercent.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(tscmbZoomInPercent.Text.Replace("%", "")) Then
                tscmbZoomInPercent.Text = CInt(tscmbZoomInPercent.Text.Replace("%", "")).ToString() & "%"
                If CInt(tscmbZoomInPercent.Text.Replace("%", "")) <= CInt(zooms(0)) Then
                    Backglass.currentTabPage.Zoom(CInt(tscmbZoomInPercent.Text.Replace("%", "")))
                End If
            End If
        End If
    End Sub

#End Region

#Region "image"

    Private Sub Image_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmiImage.DropDownOpening
        Dim isValid As Boolean = (Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentData.Image IsNot Nothing)
        tsmiReloadBackglassImage.Enabled = isValid
        tsmiImportDMDImage.Enabled = isValid AndAlso (Backglass.currentData.DMDType <> eDMDType.NoB2SDMD)
        tsmiImportIlluminationImage.Enabled = isValid
        tsmiGrillHeight.Enabled = isValid
        tsmiSetGrillHeight.Enabled = isValid
        tsmiSetMiniGrillHeight.Enabled = isValid
        tsmiDMDArea.Enabled = isValid
        tsmiCopyDMDImageFromBackglass.Enabled = isValid AndAlso (Backglass.currentData.DMDType <> eDMDType.NoB2SDMD)
        tsmiSetDefaultDMDLocation.Enabled = isValid AndAlso (Backglass.currentData.DMDType <> eDMDType.NoB2SDMD)
        tsmiResize.Enabled = isValid
        tsmiBrightness.Enabled = isValid
        If Backglass.currentTabPage IsNot Nothing Then
            tsmiSetGrillHeight.Checked = Backglass.currentTabPage.SetGrillHeight
            tsmiSetMiniGrillHeight.Checked = Backglass.currentTabPage.SetSmallGrillHeight
            tsmiCopyDMDImageFromBackglass.Checked = Backglass.currentTabPage.CopyDMDImageFromBackglass
            tsmiSetDefaultDMDLocation.Checked = Backglass.currentTabPage.SetDMDDefaultLocation
        End If
    End Sub

    Private Sub ImportBackgroundImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiImportBackglassImage.Click, tsbImportBackgroundImage.Click
        If Backglass.currentTabPage IsNot Nothing Then
            ImportBackgroundImage()
        Else
            tsmiNew.PerformClick()
        End If
        LockUnlockMenus()
    End Sub
    Private Sub ReloadBackgroundImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiReloadBackglassImage.Click
        If Backglass.currentTabPage IsNot Nothing Then
            If IO.File.Exists(Backglass.currentData.ImageFileName) Then
                Undo.AddEntry(New Undo.UndoEntry(Undo.Type.ImageReloaded, Backglass.currentData.Image))
                Dim oldimagesize As Size = Backglass.currentTabPage.Image.Size
                Dim image As Image = Bitmap.FromFile(Backglass.currentData.ImageFileName).Copy(True).Resized(Backglass.currentData.Image.Size)
                'Backglass.currentBulbs.Resize(oldimagesize, image.Size)
                'Backglass.currentScores.Resize(oldimagesize, image.Size)
                Backglass.currentTabPage.Image() = image
                Backglass.currentTabPage.Zoom(tscmbZoomInPercent.Text)
                Backglass.currentData.IsSavedImageDirty = True
                tscmbImage.SelectedIndex = 0
                UpdateStatusBar(Me, Backglass.currentTabPage)
            Else
                MessageBox.Show(String.Format(My.Resources.MSG_CannotReloadBackPic, Backglass.currentData.ImageFileName), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub ImportIlluminationImages_Click(sender As System.Object, e As System.EventArgs) Handles tsmiImportIlluminationImage.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Using filedialog As OpenFileDialog = New OpenFileDialog
                With filedialog
                    .Filter = ImageFileExtensionFilter
                    .FileName = String.Empty
                    If .ShowDialog(Me) = DialogResult.OK Then
                        Try
                            Undo.AddEntry(New Undo.UndoEntry(Undo.Type.IlluminationImageImported, Backglass.currentData.Image))
                            Dim image As Image = Bitmap.FromFile(.FileName).Copy(True)
                            Backglass.currentData.Images.Insert(Images.eImageInfoType.Title4IlluminationImages, New Images.ImageInfo(Images.eImageInfoType.IlluminationImage, .FileName, image))
                        Catch
                            MessageBox.Show(My.Resources.MSG_CannotLoadPic, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            LoadToolResourcesForm()
                            UpdateStatusBar(Me, Backglass.currentTabPage)
                        End Try
                    End If
                End With
            End Using
        End If
    End Sub
    Private Sub ImportDMDImage_Click(sender As System.Object, e As System.EventArgs) Handles tsmiImportDMDImage.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Using filedialog As OpenFileDialog = New OpenFileDialog
                With filedialog
                    .Filter = ImageFileExtensionFilter
                    .FileName = String.Empty
                    If .ShowDialog(Me) = DialogResult.OK Then
                        Try
                            Undo.AddEntry(New Undo.UndoEntry(Undo.Type.DMDImageImported, Backglass.currentData.Image))
                            Dim image As Image = Bitmap.FromFile(.FileName).Copy(True)
                            Backglass.currentTabPage.DMDImage(.FileName) = image
                            tscmbImage.SelectedIndex = 1
                            Backglass.currentData.Images.Insert(Images.eImageInfoType.Title4DMDImages, New Images.ImageInfo(Images.eImageInfoType.DMDImage, .FileName, image))
                            Backglass.currentData.IsSavedDMDImageDirty = True
                        Catch
                            MessageBox.Show(My.Resources.MSG_CannotLoadPic, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            LoadToolResourcesForm()
                            UpdateStatusBar(Me, Backglass.currentTabPage)
                        End Try
                    End If
                End With
            End Using
        End If
    End Sub

    Private Sub CopyDMDImageFromBackglass_Click(sender As System.Object, e As System.EventArgs) Handles tsmiCopyDMDImageFromBackglass.Click
        If Backglass.currentTabPage IsNot Nothing Then
            If Backglass.currentData.DMDCopyArea.Location = Nothing OrElse Backglass.currentData.DMDCopyArea.Size = Nothing Then
                Dim size As Size = Backglass.currentData.Image.Size
                Backglass.currentData.DMDCopyArea.Location = New Point(CInt(size.Width / 2) - CInt(size.Width / 6), CInt(size.Height / 4) * 3)
                Backglass.currentData.DMDCopyArea.Size = New Size(CInt(size.Width / 3), CInt(size.Height / 6))
            End If
            Backglass.currentTabPage.CopyDMDImageFromBackglass = Not Backglass.currentTabPage.CopyDMDImageFromBackglass
            tsmiCopyDMDImageFromBackglass.Checked = Backglass.currentTabPage.CopyDMDImageFromBackglass
            Backglass.currentTabPage.SetGrillHeight = False
            Backglass.currentTabPage.SetSmallGrillHeight = False
            Backglass.currentTabPage.SetDMDDefaultLocation = False
        End If
    End Sub
    Private Sub SetDefaultDMDLocation_Click(sender As System.Object, e As System.EventArgs) Handles tsmiSetDefaultDMDLocation.Click
        If Backglass.currentTabPage IsNot Nothing Then
            If Backglass.currentData.DMDImage IsNot Nothing Then
                Backglass.currentTabPage.SetDMDDefaultLocation = Not Backglass.currentTabPage.SetDMDDefaultLocation
                tsmiSetDefaultDMDLocation.Checked = Backglass.currentTabPage.SetDMDDefaultLocation
            Else
                MessageBox.Show(My.Resources.MSG_NoDMDFile4SettingLocation, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Backglass.currentTabPage.SetGrillHeight = False
            Backglass.currentTabPage.SetSmallGrillHeight = False
            Backglass.currentTabPage.CopyDMDImageFromBackglass = False
        End If
    End Sub

    Private Sub SetGrillHeight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiSetGrillHeight.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Backglass.currentTabPage.SetGrillHeight = Not Backglass.currentTabPage.SetGrillHeight
            tsmiSetGrillHeight.Checked = Backglass.currentTabPage.SetGrillHeight
            Backglass.currentTabPage.SetSmallGrillHeight = False
            Backglass.currentTabPage.CopyDMDImageFromBackglass = False
            Backglass.currentTabPage.SetDMDDefaultLocation = False
        End If
    End Sub
    Private Sub SetMiniGrillHeight_Click(sender As System.Object, e As System.EventArgs) Handles tsmiSetMiniGrillHeight.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Backglass.currentTabPage.SetSmallGrillHeight = Not Backglass.currentTabPage.SetSmallGrillHeight
            tsmiSetMiniGrillHeight.Checked = Backglass.currentTabPage.SetSmallGrillHeight
            Backglass.currentTabPage.SetGrillHeight = False
            Backglass.currentTabPage.CopyDMDImageFromBackglass = False
            Backglass.currentTabPage.SetDMDDefaultLocation = False
        End If
    End Sub

    Private Sub Resize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiResize.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Dim image As Image = If(Backglass.currentData.IsDMDImageShown, Backglass.currentData.DMDImage, Backglass.currentData.Image)
            If image IsNot Nothing Then
                Dim newsize As Size = image.Size
                If formResize.ShowDialog(Me, newsize) = Windows.Forms.DialogResult.OK Then
                    Backglass.currentBulbs.Resize(image.Size, newsize)
                    Backglass.currentScores.Resize(image.Size, newsize)
                    Undo.AddEntry(New Undo.UndoEntry(Undo.Type.ImageResized, image))
                    Dim newimage As Image = image.Resized(newsize)
                    If Backglass.currentData.IsDMDImageShown Then
                        Backglass.currentImages.Resize(Images.eImageInfoType.DMDImage, newsize)
                        Backglass.currentTabPage.DMDImage = newimage
                    Else
                        Backglass.currentImages.Resize(Images.eImageInfoType.BackgroundImage, newsize)
                        Backglass.currentTabPage.Image = newimage
                    End If
                    UpdateStatusBar(Me, Backglass.currentTabPage)
                End If
            End If
        End If
    End Sub

    Private Sub Brightness_Click(sender As System.Object, e As System.EventArgs) Handles tsmiBrightness.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Dim image As Image = If(Backglass.currentData.IsDMDImageShown, Backglass.currentData.DMDImage, Backglass.currentData.Image)
            If image IsNot Nothing Then
                If formBrightness.ShowDialog(Me, image) = Windows.Forms.DialogResult.OK Then
                    Undo.AddEntry(New Undo.UndoEntry(Undo.Type.ImageBrightnessChanged, If(Backglass.currentData.IsDMDImageShown, Backglass.currentData.DMDImage, Backglass.currentData.Image)))
                    If Backglass.currentData.IsDMDImageShown Then
                        Backglass.currentTabPage.DMDImage = image
                    Else
                        Backglass.currentTabPage.Image = image
                    End If
                    Backglass.currentTabPage.Zoom(tscmbZoomInPercent.Text)
                End If
            End If
        End If
    End Sub

#End Region

#Region "scores, reels and leds"

    Private Sub ReelsLEDs_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmiReelsLEDs.DropDownOpening
        Dim isValid As Boolean = (Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentData.Image IsNot Nothing)
        tsmiChooseReelType.Enabled = isValid
        tsmiShowScoreFrames.Enabled = isValid
        tsmiAddNewReelOrLEDFrame.Enabled = isValid
        tsmiShowScoring.Enabled = isValid
    End Sub

    Private Sub ChooseReelType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiChooseReelType.Click
        If Backglass.currentTabPage IsNot Nothing Then
            Dim reeltype As String = Backglass.currentData.ReelType
            Dim reelcolor As Color = Backglass.currentData.ReelColor
            Dim reelrollingdirection As eReelRollingDirection = Backglass.currentData.ReelRollingDirection
            Dim reelrollinginterval As Integer = Backglass.currentData.ReelRollingInterval
            Dim reelintermediatecount As Integer = Backglass.currentData.ReelIntermediateImageCount
            Dim usedream7leds As Boolean = Backglass.currentData.UseDream7LEDs
            Dim d7glow As Single = Backglass.currentData.D7Glow
            Dim d7thickness As Single = Backglass.currentData.D7Thickness
            Dim d7shear As Single = Backglass.currentData.D7Shear
            If formReelType.ShowDialog(Me, reeltype, reelcolor, reelrollingdirection, reelrollinginterval, reelintermediatecount, usedream7leds, d7glow, d7thickness, d7shear) = Windows.Forms.DialogResult.OK Then
                Backglass.currentData.ReelType = reeltype
                Backglass.currentData.ReelColor = reelcolor
                Backglass.currentData.ReelRollingDirection = reelrollingdirection
                Backglass.currentData.ReelRollingInterval = reelrollinginterval
                Backglass.currentData.ReelIntermediateImageCount = reelintermediatecount
                Backglass.currentData.UseDream7LEDs = usedream7leds
                Backglass.currentData.D7Glow = d7glow
                Backglass.currentData.D7Thickness = d7thickness
                Backglass.currentData.D7Shear = d7shear
                LoadToolReelsAndLEDsForm(True)
                Backglass.currentTabPage.Invalidate()
            End If
            If formToolReelsAndLEDs IsNot Nothing Then formToolReelsAndLEDs.ReloadReels()
        End If
    End Sub

    Private Sub ShowScoreFrames_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiShowScoreFrames.Click, tsbShowScoreFrames.Click
        If Backglass.currentTabPage IsNot Nothing Then
            tsmiShowScoreFrames.Checked = Not tsmiShowScoreFrames.Checked
            tsbShowScoreFrames.Checked = tsmiShowScoreFrames.Checked
            Backglass.currentTabPage.ShowScoreFrames = tsmiShowScoreFrames.Checked
            If tsmiShowScoreFrames.Checked Then
                CheckToolReelsAndLEDsForm()
                ShowToolReelsAndLEDsForm()
            End If
        End If
    End Sub

    Private Sub AddNewReelOrLEDFrame_Click(sender As System.Object, e As System.EventArgs) Handles tsmiAddNewReelOrLEDFrame.Click, tsbAddNewReelOrLEDFrame.Click
        If Backglass.currentTabPage IsNot Nothing Then
            ' maybe show frames
            If Not Backglass.currentTabPage.ShowScoreFrames Then
                tsmiShowScoreFrames.PerformClick()
            End If
            Backglass.currentTabPage.ShowScoreFrames = True
            ' add score frame
            Backglass.currentTabPage.ReelsAndLEDs_AddScore()
        End If
    End Sub

    Private Sub ShowScoring_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiShowScoring.Click, tsbShowScoring.Click
        If Backglass.currentTabPage IsNot Nothing Then
            tsmiShowScoring.Checked = Not tsmiShowScoring.Checked
            tsbShowScoring.Checked = tsmiShowScoring.Checked
            Backglass.currentTabPage.ShowScoring = tsmiShowScoring.Checked
        End If
    End Sub

#End Region

#Region "illumination"

    Private Sub Illumination_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmiIllumination.DropDownOpening
        Dim isValid As Boolean = (Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentData.Image IsNot Nothing)
        tsmiShowIlluFrames.Enabled = isValid
        tsmiAddNewBulbFrame.Enabled = isValid
        tsmiAddANewIlluminationSnippit.Enabled = isValid
        tsmiShowIllumination.Enabled = isValid
        tsmiShowIlluminationWithAccurateIntensity.Enabled = isValid
        tsmiManageAnimations.Enabled = isValid
    End Sub

    Private Sub ShowIlluFrames_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiShowIlluFrames.Click, tsbShowIlluFrames.Click
        If Backglass.currentTabPage IsNot Nothing Then
            tsmiShowIlluFrames.Checked = Not tsmiShowIlluFrames.Checked
            tsbShowIlluFrames.Checked = tsmiShowIlluFrames.Checked
            Backglass.currentTabPage.ShowIlluFrames = tsmiShowIlluFrames.Checked
            If tsmiShowIlluFrames.Checked Then
                If formToolIllumination Is Nothing Then formToolIllumination = New formToolIllumination()
                If Not formToolIllumination.Visible Then formToolIllumination.Show(Me)
            End If
        End If
    End Sub

    Private Sub AddNewBulbFrame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiAddNewBulbFrame.Click, tsbAddNewBulbFrame.Click
        If Backglass.currentTabPage IsNot Nothing Then
            ' maybe show frames
            If Not Backglass.currentTabPage.ShowIlluFrames Then
                tsmiShowIlluFrames.PerformClick()
            End If
            Backglass.currentTabPage.ShowIlluFrames = True
            ' add bulb
            Backglass.currentTabPage.Illumination_AddBulb()
        End If
    End Sub
    Private Sub AddANewIlluminationSnippit_Click(sender As System.Object, e As System.EventArgs) Handles tsmiAddANewIlluminationSnippit.Click
        If Backglass.currentTabPage IsNot Nothing Then
            ' maybe show frames
            If Not Backglass.currentTabPage.ShowIlluFrames Then
                tsmiShowIlluFrames.PerformClick()
            End If
            Backglass.currentTabPage.ShowIlluFrames = True
            ' add bulb
            If formAddSnippit Is Nothing Then formAddSnippit = New formAddSnippit()
            formAddSnippit.ShowDialog(Me)
        End If
    End Sub

    Private Sub ShowIllumination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiShowIllumination.Click, tsbShowIllumination.Click
        Cursor.Current = Cursors.WaitCursor
        If Backglass.currentTabPage IsNot Nothing Then
            tsmiShowIllumination.Checked = Not tsmiShowIllumination.Checked
            tsbShowIllumination.Checked = tsmiShowIllumination.Checked
            tsmiShowIlluminationWithAccurateIntensity.Enabled = Not tsmiShowIllumination.Checked
            tsbShowIlluminationWithAccurateIntensity.Enabled = Not tsmiShowIllumination.Checked
            Backglass.currentTabPage.ShowIllumination = tsmiShowIllumination.Checked
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub ShowIlluminationWithAccurateIntensity_Click(sender As System.Object, e As System.EventArgs) Handles tsmiShowIlluminationWithAccurateIntensity.Click, tsbShowIlluminationWithAccurateIntensity.Click
        Cursor.Current = Cursors.WaitCursor
        If Backglass.currentTabPage IsNot Nothing Then
            tsmiShowIlluminationWithAccurateIntensity.Checked = Not tsmiShowIlluminationWithAccurateIntensity.Checked
            tsbShowIlluminationWithAccurateIntensity.Checked = tsmiShowIlluminationWithAccurateIntensity.Checked
            tsmiShowIllumination.Enabled = Not tsmiShowIlluminationWithAccurateIntensity.Checked
            tsbShowIllumination.Enabled = Not tsmiShowIlluminationWithAccurateIntensity.Checked
            Backglass.currentTabPage.ShowIntensityIllumination = tsmiShowIlluminationWithAccurateIntensity.Checked
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub ManageAnimations_Click(sender As System.Object, e As System.EventArgs) Handles tsmiManageAnimations.Click

        If Backglass.currentData IsNot Nothing AndAlso Backglass.currentTabPage IsNot Nothing Then
            ' maybe show and filter frames
            If Not Backglass.currentTabPage.ShowIlluFrames Then
                tsmiShowIlluFrames.PerformClick()
            End If
            tscmbIDFilter.SelectedIndex = tscmbIDFilter.Items.Count - 1
            ' open animation dialog
            If formAnimations Is Nothing Then formAnimations = New formAnimations()
            formAnimations.Show(Me)
        End If

    End Sub

    Private Sub IDFilter_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tscmbIDFilter.SelectedIndexChanged
        If Backglass.currentTabPage IsNot Nothing AndAlso Not ignoreChanges Then
            Dim filter As String = If(tscmbIDFilter.Text.Contains(" "), tscmbIDFilter.Text.Substring(0, tscmbIDFilter.Text.IndexOf(" ")), tscmbIDFilter.Text)
            If tscmbIDFilter.SelectedIndex = 1 Then
                filter = "off"
            ElseIf tscmbIDFilter.SelectedIndex = 2 Then
                filter = "on"
            ElseIf tscmbIDFilter.SelectedIndex = 3 Then
                filter = "alwayson"
            ElseIf Backglass.currentData.DualBackglass AndAlso tscmbIDFilter.SelectedIndex = 4 Then
                filter = "authentic"
            ElseIf Backglass.currentData.DualBackglass AndAlso tscmbIDFilter.SelectedIndex = 5 Then
                filter = "fantasy"
            ElseIf tscmbIDFilter.SelectedIndex = tscmbIDFilter.Items.Count - 2 Then
                filter = "withoutid"
            ElseIf tscmbIDFilter.SelectedIndex = tscmbIDFilter.Items.Count - 1 Then
                filter = "withname"
            End If
            Backglass.currentTabPage.Illumination_SetRomFilter(filter)
        End If
    End Sub

#End Region

#Region "backglass"

    Private Sub Backglass_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmiBackglass.DropDownOpening
        Dim isValid As Boolean = (Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentData.Image IsNot Nothing)
        tsmiBackglassPreviewAndTest.Enabled = isValid
        tsmiCreateDarkBackglassImage.Enabled = isValid
        tsmiCreateIlluminatedBackglassImage.Enabled = isValid
        tsmiCreateDirectAccessBackglassCodeFile.Enabled = isValid AndAlso Backglass.currentData.DestType = eDestType.DirectB2S
        tsmiCreateMSBackglassCode.Enabled = isValid AndAlso Backglass.currentData.DestType = eDestType.VisualStudio2010
    End Sub

    Private Sub BackglassPreviewAndTest_Click(sender As System.Object, e As System.EventArgs) Handles tsmiBackglassPreviewAndTest.Click

        If Backglass.currentData IsNot Nothing AndAlso Backglass.currentTabPage IsNot Nothing Then
            If formVPM Is Nothing Then formVPM = New formVPM()
            formVPM.ShowDialog(Me)
            SaveSettings()
        End If

    End Sub

    Private Sub CreateDarkBackglassImage_Click(sender As System.Object, e As System.EventArgs) Handles tsmiCreateDarkBackglassImage.Click
        If Backglass.currentTabPage IsNot Nothing Then
            ShowProgress(0)
            Dim image As Image = Backglass.currentTabPage.DarkImage()
            ShowProgress(50)
            Dim filename As String = IO.Path.Combine(ProjectPath, Backglass.currentData.Name & " Dark" & IO.Path.GetExtension(Backglass.currentData.ImageFileName))
            IO.Directory.CreateDirectory(ProjectPath)
            ShowProgress(75)
            image.Save(filename)
            ShowProgress(100)
        End If
    End Sub
    Private Sub CreateIlluminatedBackglassImage_Click(sender As System.Object, e As System.EventArgs) Handles tsmiCreateIlluminatedBackglassImage.Click
        If Backglass.currentTabPage IsNot Nothing Then
            ShowProgress(0)
            Dim image As Image = Backglass.currentTabPage.IlluminatedImage()
            ShowProgress(50)
            Dim filename As String = IO.Path.Combine(ProjectPath, Backglass.currentData.Name & " Illuminated" & IO.Path.GetExtension(Backglass.currentData.ImageFileName))
            IO.Directory.CreateDirectory(ProjectPath)
            ShowProgress(75)
            image.Save(filename)
            ShowProgress(100)
        End If
    End Sub

    Private Sub CreateDirectAccessBackglassCodeFile_Click(sender As System.Object, e As System.EventArgs) Handles tsmiCreateDirectAccessBackglassCodeFile.Click

        If Backglass.currentTabPage IsNot Nothing Then
            coding.CreateDirectB2SFile()
        End If

    End Sub
    Private Sub CreateMSBackglassCode_Click(sender As System.Object, e As System.EventArgs) Handles tsmiCreateMSBackglassCode.Click

        If Backglass.currentTabPage IsNot Nothing Then
            coding.CreateVisualStudioCode()
        End If

    End Sub

#End Region

#Region "window"

    Private Sub Window_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmiWindow.DropDownOpening
        tsmiBackglassImage.Checked = (Backglass.currentData IsNot Nothing AndAlso Not Backglass.currentData.IsDMDImageShown)
        tsmiDMDImage.Checked = (Backglass.currentData IsNot Nothing AndAlso Backglass.currentData.IsDMDImageShown)
        tsmiReelsAndLEDSettings.Checked = (formToolReelsAndLEDs IsNot Nothing AndAlso formToolReelsAndLEDs.Visible = True)
        tsmiIlluminationSettings.Checked = (formToolIllumination IsNot Nothing AndAlso formToolIllumination.Visible = True)
        tsmiHistory.Checked = (formToolUndo IsNot Nothing AndAlso formToolUndo.Visible = True)
        tsmiImages.Checked = (formToolResources IsNot Nothing AndAlso formToolResources.Visible = True)
        tsmiTranslucent.Checked = (DefaultOpacity <> 1)
    End Sub

    Private Sub BackglassImage_Click(sender As System.Object, e As System.EventArgs) Handles tsmiBackglassImage.Click
        If Backglass.currentTabPage IsNot Nothing Then
            tscmbImage.SelectedIndex = 0
            Backglass.currentTabPage.ShowBackglassImage()
            UpdateStatusBar(Me, Backglass.currentTabPage)
        End If
    End Sub
    Private Sub DMDImage_Click(sender As System.Object, e As System.EventArgs) Handles tsmiDMDImage.Click
        If Backglass.currentTabPage IsNot Nothing Then
            tscmbImage.SelectedIndex = 1
            Backglass.currentTabPage.ShowDMDImage()
            UpdateStatusBar(Me, Backglass.currentTabPage)
        End If
    End Sub
    Private Sub Image_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tscmbImage.SelectedIndexChanged
        If tscmbImage.SelectedIndex = 0 Then
            If Backglass.currentTabPage IsNot Nothing Then Backglass.currentTabPage.ShowBackglassImage()
        ElseIf tscmbImage.SelectedIndex = 1 Then
            If Backglass.currentTabPage IsNot Nothing Then Backglass.currentTabPage.ShowDMDImage()
        End If
        UpdateStatusBar(Me, Backglass.currentTabPage)
        ' refresh rom filter
        RefreshIDFilter()
    End Sub

    Private Sub ReelsAndLEDSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiReelsAndLEDSettings.Click
        CheckToolReelsAndLEDsForm()
        If Not formToolReelsAndLEDs.Visible Then
            formToolReelsAndLEDs.Show(Me)
        Else
            formToolReelsAndLEDs.Hide()
        End If
    End Sub
    Private Sub IlluminationSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiIlluminationSettings.Click
        CheckToolIlluminationForm()
        If Not formToolIllumination.Visible Then
            formToolIllumination.Show(Me)
        Else
            formToolIllumination.Hide()
        End If
    End Sub
    Private Sub History_Click(sender As System.Object, e As System.EventArgs) Handles tsmiHistory.Click
        CheckToolUndoForm()
        If Not formToolUndo.Visible Then
            formToolUndo.Show(Me)
        Else
            formToolUndo.Hide()
        End If
    End Sub
    Private Sub Images_Click(sender As System.Object, e As System.EventArgs) Handles tsmiImages.Click
        CheckToolResourcesForm()
        If Not formToolResources.Visible Then
            formToolResources.Show(Me)
        Else
            formToolResources.Hide()
        End If
    End Sub

    Private Sub Translucent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiTranslucent.Click
        If DefaultOpacity = 1 Then
            DefaultOpacity = 0.8
        Else
            DefaultOpacity = 1
        End If
        tsmiTranslucent.Checked = (DefaultOpacity <> 1)
        MyBase.SaveSettings()
        For Each form As Form In Me.OwnedForms
            If TypeOf form Is formBase Then
                form.Opacity = DefaultOpacity
            End If
        Next
    End Sub

#End Region

#Region "help"

    Private Sub HelpTopics_Click(sender As System.Object, e As System.EventArgs) Handles tsmiHelpTopics.Click, tsbHelp.Click
        Help.ShowHelp(Me, "B2SBackglassDesigner.chm", HelpNavigator.TableOfContents)
    End Sub

    Private Sub About_Click(sender As System.Object, e As System.EventArgs) Handles tsmiAbout.Click
        formAbout.ShowDialog(Me)
    End Sub

#End Region

#End Region


#Region "private methods"

    Private Function OpenSettings(ByVal newtable As Boolean, Optional ByVal savetableas As Boolean = False) As Boolean
        Dim ret As Boolean = False
        Dim isANewTable As Boolean = (newtable AndAlso Not savetableas)
        Dim name As String = String.Empty
        Dim vsname As String = String.Empty
        Dim dualbackglass As Boolean = False
        Dim author As String = String.Empty
        Dim artwork As String = String.Empty
        Dim tabletype As eTableType = eTableType.NotDefined
        Dim addedmdefaults As Boolean = False
        Dim numberofplayers As Integer = 4
        Dim b2sdatacount As Integer = 5
        Dim dmdtype As eDMDType = eDMDType.NotDefined
        Dim commtype As eCommType = eCommType.NotDefined
        Dim desttype As eDestType = eDestType.NotDefined
        If Not isANewTable Then
            If Not savetableas Then
                name = Backglass.currentData.Name
                vsname = Backglass.currentData.VSName
                dualbackglass = Backglass.currentData.DualBackglass
                author = Backglass.currentData.Author
                artwork = Backglass.currentData.Artwork
            End If
            tabletype = Backglass.currentData.TableType
            addedmdefaults = Backglass.currentData.AddEMDefaults
            numberofplayers = Backglass.currentData.NumberOfPlayers
            b2sdatacount = Backglass.currentData.B2SDataCount
            dmdtype = Backglass.currentData.DMDType
            commtype = Backglass.currentData.CommType
            desttype = Backglass.currentData.DestType
        End If
        If formSettings.ShowDialog(Me, newtable, name, vsname, dualbackglass, author, artwork, tabletype, addedmdefaults, numberofplayers, b2sdatacount, dmdtype, commtype, desttype) = Windows.Forms.DialogResult.OK Then
            If Not String.IsNullOrEmpty(name) Then
                ret = True
                ' get data
                If isANewTable Then
                    B2STab.AddBackglass(New B2STabPage(name, vsname, dualbackglass, author, artwork, tabletype, addedmdefaults, numberofplayers, b2sdatacount, dmdtype, commtype, desttype))
                    B2STab.SelectedIndex = B2STab.TabPages.Count - 1
                    recent.AddToRecentList(Backglass.currentData)
                Else
                    Backglass.currentData.Name = name
                    B2STab.SelectedTabPage.Text = name
                    B2STab.SelectedTabPage.Invalidate()
                    B2STab.Invalidate()
                    Backglass.currentData.VSName = vsname
                    Backglass.currentData.DualBackglass = dualbackglass
                    Backglass.currentData.Author = author
                    Backglass.currentData.Artwork = artwork
                    Backglass.currentData.TableType = tabletype
                    Backglass.currentData.AddEMDefaults = addedmdefaults
                    Backglass.currentData.NumberOfPlayers = numberofplayers
                    Backglass.currentData.B2SDataCount = b2sdatacount
                    Backglass.currentData.DMDType = dmdtype
                    Backglass.currentData.CommType = commtype
                    Backglass.currentData.DestType = desttype
                    If savetableas Then
                        recent.AddToRecentList(Backglass.currentData)
                    End If
                End If
            End If
        End If
        SaveSettings()
        ShowStatus()
        LockUnlockMenus()
        Return ret
    End Function

    Private Sub ImportBackgroundImage(Optional ByVal oldimagesize As Size = Nothing)
        Using filedialog As OpenFileDialog = New OpenFileDialog
            With filedialog
                .Filter = ImageFileExtensionFilter
                .FileName = String.Empty
                If .ShowDialog(Me) = DialogResult.OK Then
                    Try
                        Undo.AddEntry(New Undo.UndoEntry(Undo.Type.ImageImported, Backglass.currentData.Image))
                        Dim image As Image = Bitmap.FromFile(.FileName).Copy(True)
                        If oldimagesize <> Nothing Then
                            Backglass.currentBulbs.Resize(oldimagesize, image.Size)
                            Backglass.currentScores.Resize(oldimagesize, image.Size)
                            Dim factor As Single = oldimagesize.Height / image.Size.Height
                            Backglass.currentData.GrillHeight = Backglass.currentData.GrillHeight / factor
                            Backglass.currentData.SmallGrillHeight = Backglass.currentData.SmallGrillHeight / factor
                        ElseIf Backglass.currentData.Image IsNot Nothing Then
                            If MessageBox.Show(My.Resources.MSG_BackgroundImageChange, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                                Backglass.currentBulbs.Resize(Backglass.currentData.Image.Size, image.Size)
                                Backglass.currentScores.Resize(Backglass.currentData.Image.Size, image.Size)
                                Dim factor As Single = Backglass.currentData.Image.Size.Height / image.Size.Height
                                Backglass.currentData.GrillHeight = Backglass.currentData.GrillHeight / factor
                                Backglass.currentData.SmallGrillHeight = Backglass.currentData.SmallGrillHeight / factor
                            End If
                        End If
                        Backglass.currentTabPage.Image(.FileName) = image
                        Backglass.currentData.Images.Insert(Images.eImageInfoType.Title4BackgroundImages, New Images.ImageInfo(Images.eImageInfoType.BackgroundImage, .FileName, image))
                        Backglass.currentTabPage.Zoom(tscmbZoomInPercent.Text)
                        Backglass.currentData.IsSavedImageDirty = True
                    Catch
                        MessageBox.Show(My.Resources.MSG_CannotLoadPic, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        LoadToolResourcesForm()
                        UpdateStatusBar(Me, Backglass.currentTabPage)
                    End Try
                End If
            End With
        End Using
    End Sub

    Private Sub RefreshSettings()

        ' set undo backglass
        Undo.SelectedBackglass = Backglass.currentTabPage

        ' set some overall settings
        tsmiShowScoreFrames.Checked = If(Backglass.currentData IsNot Nothing, Backglass.currentData.ShowScoreFrames, False)
        tsbShowScoreFrames.Checked = If(Backglass.currentData IsNot Nothing, Backglass.currentData.ShowScoreFrames, False)
        tsmiShowScoring.Checked = If(Backglass.currentData IsNot Nothing, Backglass.currentData.ShowScoring, False)
        tsbShowScoring.Checked = If(Backglass.currentData IsNot Nothing, Backglass.currentData.ShowScoring, False)
        tsmiShowIlluFrames.Checked = If(Backglass.currentData IsNot Nothing, Backglass.currentData.ShowIlluFrames, False)
        tsbShowIlluFrames.Checked = If(Backglass.currentData IsNot Nothing, Backglass.currentData.ShowIlluFrames, False)
        tsmiShowIllumination.Checked = If(Backglass.currentData IsNot Nothing, Backglass.currentData.ShowIllumination, False)
        tsbShowIllumination.Checked = If(Backglass.currentData IsNot Nothing, Backglass.currentData.ShowIllumination, False)
        tscmbImage.SelectedIndex = If(Backglass.currentData IsNot Nothing AndAlso Backglass.currentData.IsDMDImageShown, 1, 0)

        ' refresh reels tool window
        LoadToolReelsAndLEDsForm(True)

        ' refresh illumination tool window
        LoadToolIlluminationForm()

        ' refresh images tool window
        LoadToolResourcesForm()

        ' refresh rom filters
        RefreshIDFilter()

        UpdateStatusBar(Me, Backglass.currentTabPage)

    End Sub

    Private Sub RefreshIDFilter()
        ' refresh rom filter
        If Backglass.currentData IsNot Nothing Then
            ' store all filters
            Dim filters As String() = Nothing
            AddFilter(filters, "")
            AddFilter(filters, My.Resources.TXT_FilterOff)
            AddFilter(filters, My.Resources.TXT_FilterOn)
            AddFilter(filters, My.Resources.TXT_FilterAlwaysOn)
            If Backglass.currentData.DualBackglass Then
                AddFilter(filters, My.Resources.TXT_FilterAuthentic)
                AddFilter(filters, My.Resources.TXT_FilterFantasy)
            End If
            For Each item As KeyValuePair(Of String, Integer) In Backglass.currentUsedIDs
                AddFilter(filters, item.Key & " (" & item.Value.ToString() & ")")
            Next
            AddFilter(filters, My.Resources.TXT_FilterWithoutID)
            AddFilter(filters, My.Resources.TXT_FilterWithName)
            Dim i As Integer = 0
            Dim clearall As Boolean = (tscmbIDFilter.Items.Count <> filters.Length)
            If Not clearall Then
                For Each oldfilter As String In tscmbIDFilter.Items
                    If Not filters(i).Equals(oldfilter) Then
                        clearall = True
                        Exit For
                    End If
                    i += 1
                Next
            End If
            ' maybe refresh filter
            If clearall Then
                ignoreChanges = True
                Dim filter As String = tscmbIDFilter.Text
                If Not String.IsNullOrEmpty(filter) Then
                    filter = If(filter.Contains(" "), filter.Substring(0, filter.IndexOf(" ")), filter) & " "
                End If
                tscmbIDFilter.Items.Clear()
                For Each newfilter As String In filters
                    tscmbIDFilter.Items.Add(newfilter)
                    If Not String.IsNullOrEmpty(filter) Then
                        If (newfilter & " ").StartsWith(filter) Then filter = newfilter
                    End If
                Next
                If Not String.IsNullOrEmpty(filter) Then
                    tscmbIDFilter.Text = filter
                End If
                ignoreChanges = False
            End If
        End If
    End Sub
    Private Sub AddFilter(ByRef filters As String(), ByVal newfilter As String)
        If filters Is Nothing Then
            ReDim filters(0)
            filters(0) = newfilter
        Else
            ReDim Preserve filters(filters.Length)
            filters(filters.Length - 1) = newfilter
        End If
    End Sub

    Private Sub LockUnlockMenus()
        Dim isValid As Boolean = (Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentData.Image IsNot Nothing)
        tsbShowScoreFrames.Enabled = isValid
        tsbAddNewReelOrLEDFrame.Enabled = isValid
        tsbShowScoring.Enabled = isValid
        tsbShowIlluFrames.Enabled = isValid
        tsbAddNewBulbFrame.Enabled = isValid
        tsbShowIllumination.Enabled = isValid
        tsbShowIlluminationWithAccurateIntensity.Enabled = isValid
    End Sub

    Private Sub CheckToolReelsAndLEDsForm()
        If formToolReelsAndLEDs Is Nothing Then formToolReelsAndLEDs = New formToolReelsAndLEDs()
        LoadToolReelsAndLEDsForm()
    End Sub
    Private Sub ShowToolReelsAndLEDsForm()
        If Not formToolReelsAndLEDs.Visible Then formToolReelsAndLEDs.Show(Me)
    End Sub
    Private Sub LoadToolReelsAndLEDsForm(Optional ByVal refreshReelsAndLEDs As Boolean = False)
        NoToolEvents = True
        If formToolReelsAndLEDs IsNot Nothing AndAlso Backglass.currentTabPage IsNot Nothing Then
            formToolReelsAndLEDs.ignoreChange = True
            If Backglass.currentData IsNot Nothing Then
                formToolReelsAndLEDs.cmbNumberOfPlayers.Text = Backglass.currentData.NumberOfPlayers.ToString
                formToolReelsAndLEDs.chkDream7.Checked = Backglass.currentData.UseDream7LEDs
            Else
                formToolReelsAndLEDs.cmbNumberOfPlayers.Text = ""
                formToolReelsAndLEDs.chkDream7.Checked = False
            End If
            If Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentTabPage.SelectedScore IsNot Nothing Then
                With Backglass.currentTabPage.SelectedScore
                    formToolReelsAndLEDs.txtID.Text = .ID
                    formToolReelsAndLEDs.numericDigits.Text = .Digits
                    formToolReelsAndLEDs.numericSpacing.Text = .Spacing
                    formToolReelsAndLEDs.cmbInitState.SelectedIndex = .DisplayState
                    formToolReelsAndLEDs.txtB2SStartID.Text = If(.B2SStartDigit = 0, "", .B2SStartDigit.ToString())
                    formToolReelsAndLEDs.cmbB2SScoreType.SelectedIndex = .B2SScoreType
                    formToolReelsAndLEDs.cmbB2SPlayerNo.SelectedIndex = .B2SPlayerNo
                End With
            Else
                formToolReelsAndLEDs.txtID.Text = ""
                formToolReelsAndLEDs.numericDigits.Text = ""
                formToolReelsAndLEDs.numericSpacing.Text = ""
                formToolReelsAndLEDs.cmbInitState.SelectedIndex = 0
                formToolReelsAndLEDs.txtB2SStartID.Text = ""
                formToolReelsAndLEDs.cmbB2SScoreType.SelectedIndex = 0
                formToolReelsAndLEDs.cmbB2SPlayerNo.SelectedIndex = 0
            End If
            If Backglass.currentData IsNot Nothing Then
                formToolReelsAndLEDs.txtB2SStartID.Enabled = (Backglass.currentData.CommType = eCommType.B2S)
                formToolReelsAndLEDs.cmbB2SScoreType.Enabled = (Backglass.currentData.CommType = eCommType.B2S)
                formToolReelsAndLEDs.cmbB2SPlayerNo.Enabled = (Backglass.currentData.CommType = eCommType.B2S)
            End If
            If refreshReelsAndLEDs Then formToolReelsAndLEDs.ReloadReels()
            formToolReelsAndLEDs.btnPerfectScaleWidthFix.Enabled = (Backglass.currentTabPage.SelectedScore IsNot Nothing)
            formToolReelsAndLEDs.btnChangeLEDColor.Enabled = (Backglass.currentTabPage.SelectedScore IsNot Nothing AndAlso IsReelImageRendered(Backglass.currentTabPage.SelectedScore.ReelType))
            formToolReelsAndLEDs.btnReelIllumination.Enabled = (Backglass.currentTabPage.SelectedScore IsNot Nothing AndAlso Not IsReelImageRendered(Backglass.currentTabPage.SelectedScore.ReelType))
            formToolReelsAndLEDs.ignoreChange = False
        End If
        NoToolEvents = False
    End Sub

    Private Sub CheckToolIlluminationForm()
        If formToolIllumination Is Nothing Then formToolIllumination = New formToolIllumination()
        LoadToolReelsAndLEDsForm()
    End Sub
    Private Sub ShowToolIlluminationForm()
        If Not formToolIllumination.Visible Then formToolIllumination.Show(Me)
    End Sub
    Private Sub LoadToolIlluminationForm()
        NoToolEvents = True
        If formToolIllumination IsNot Nothing Then
            If Backglass.currentTabPage IsNot Nothing AndAlso Backglass.currentTabPage.SelectedBulb IsNot Nothing Then
                With Backglass.currentTabPage.SelectedBulb
                    formToolIllumination.txtName.Text = .Name
                    formToolIllumination.txtID.Text = .ID.ToString()
                    formToolIllumination.txtB2SID.Text = If(.B2SID = 0, "", .B2SID.ToString())
                    formToolIllumination.cmbB2SIDType.SelectedIndex = .B2SIDType
                    formToolIllumination.txtB2SValue.Text = If(.B2SValue = 0, "", .B2SValue.ToString())
                    formToolIllumination.txtRomID.Text = If(.RomID = 0, "", .RomID.ToString())
                    formToolIllumination.cmbROMIDType.SelectedIndex = .RomIDType
                    formToolIllumination.chkRomInverted.Checked = .RomInverted
                    formToolIllumination.cmbInitState.SelectedIndex = .InitialState
                    If Backglass.currentData.DualBackglass Then
                        formToolIllumination.cmbDualMode.SelectedIndex = .DualMode
                    Else
                        formToolIllumination.cmbDualMode.Text = String.Empty
                    End If
                    formToolIllumination.TrackBarIntensity.Value = .Intensity
                    formToolIllumination.btnLightColor.BackColor = .LightColor
                    formToolIllumination.cmbDodgeColor.SelectedIndex = TranslateDodgeColor2Index(.DodgeColor)
                    formToolIllumination.cmbIlluMode.SelectedIndex = .IlluMode
                    formToolIllumination.txtIlluminationText.Text = .Text
                    If .TextAlignment = Illumination.eTextAlignment.Left Then
                        formToolIllumination.rbAlignLeft.Checked = True
                    ElseIf .TextAlignment = Illumination.eTextAlignment.Right Then
                        formToolIllumination.rbAlignRight.Checked = True
                    Else
                        formToolIllumination.rbAlignCenter.Checked = True
                    End If
                    formToolIllumination.MyFont = If(String.IsNullOrEmpty(.FontName), Nothing, New Font(.FontName, .FontSize, .FontStyle))
                    formToolIllumination.ZOrder = .ZOrder
                    formToolIllumination.IsSnippit = .IsImageSnippit
                    formToolIllumination.SnippitType = .SnippitInfo.SnippitType
                    formToolIllumination.SnippitMechID = .SnippitInfo.SnippitMechID
                    formToolIllumination.SnippitRotatingSteps = .SnippitInfo.SnippitRotatingSteps
                    formToolIllumination.SnippitRotatingInterval = .SnippitInfo.SnippitRotatingInterval
                    formToolIllumination.SnippitRotatingDirection = .SnippitInfo.SnippitRotatingDirection
                    formToolIllumination.SnippitRotatingStopBehaviour = .SnippitInfo.SnippitRotatingStopBehaviour
                End With
            Else
                formToolIllumination.txtName.Text = ""
                formToolIllumination.txtID.Text = ""
                formToolIllumination.txtB2SID.Text = ""
                formToolIllumination.cmbB2SIDType.SelectedIndex = 0
                formToolIllumination.txtB2SValue.Text = ""
                formToolIllumination.txtRomID.Text = ""
                formToolIllumination.cmbROMIDType.SelectedIndex = 0
                formToolIllumination.chkRomInverted.Checked = False
                formToolIllumination.cmbInitState.SelectedIndex = 0
                formToolIllumination.cmbDualMode.Text = String.Empty
                formToolIllumination.TrackBarIntensity.Value = 1
                formToolIllumination.btnLightColor.BackColor = DefaultLightColor
                formToolIllumination.cmbDodgeColor.SelectedIndex = 0
                formToolIllumination.cmbIlluMode.SelectedIndex = 0
                formToolIllumination.txtIlluminationText.Text = ""
                formToolIllumination.rbAlignCenter.Checked = True
                formToolIllumination.MyFont = Nothing
                formToolIllumination.ZOrder = 0
                formToolIllumination.IsSnippit = False
            End If
            ' set headlines and lock/unlock some fields
            If Backglass.currentData IsNot Nothing Then
                formToolIllumination.cmbDualMode.Enabled = (Backglass.currentData.DualBackglass AndAlso formToolIllumination.cmbInitState.SelectedIndex <> 2)
                formToolIllumination.lblB2SID.Visible = (Backglass.currentData.CommType = eCommType.B2S)
                formToolIllumination.txtB2SID.Visible = (Backglass.currentData.CommType = eCommType.B2S)
                formToolIllumination.lblB2SIDType.Visible = (Backglass.currentData.CommType = eCommType.B2S)
                formToolIllumination.cmbB2SIDType.Visible = (Backglass.currentData.CommType = eCommType.B2S)
                formToolIllumination.lblB2SValue.Visible = (Backglass.currentData.CommType = eCommType.B2S)
                formToolIllumination.txtB2SValue.Visible = (Backglass.currentData.CommType = eCommType.B2S)
                formToolIllumination.lblRomID.Visible = (Backglass.currentData.CommType = eCommType.Rom)
                formToolIllumination.txtRomID.Visible = (Backglass.currentData.CommType = eCommType.Rom)
                formToolIllumination.lblRomIDType.Visible = (Backglass.currentData.CommType = eCommType.Rom)
                formToolIllumination.cmbROMIDType.Visible = (Backglass.currentData.CommType = eCommType.Rom)
                formToolIllumination.chkRomInverted.Visible = (Backglass.currentData.CommType = eCommType.Rom)
            End If
        End If
        NoToolEvents = False
    End Sub

    Private Sub CheckToolUndoForm()
        If formToolUndo Is Nothing Then
            formToolUndo = New formToolUndo()
            Undo.ListBox = formToolUndo.lbHistory
            For Each undoentry As Undo.UndoEntry In Undo.UndoList
                formToolUndo.lbHistory.Items.Add(undoentry)
            Next
        End If
        LoadToolUndoForm()
    End Sub
    Private Sub ShowToolUndoForm()
        If Not formToolUndo.Visible Then formToolUndo.Show(Me)
    End Sub
    Private Sub LoadToolUndoForm()
        NoToolEvents = True
        If formToolUndo IsNot Nothing Then

        End If
        NoToolEvents = False
    End Sub

    Private Sub CheckToolResourcesForm()
        If formToolResources Is Nothing Then
            formToolResources = New formToolResources()
        End If
        LoadToolResourcesForm()
    End Sub
    Private Sub ShowToolResourcesForm()
        If Not formToolResources.Visible Then formToolResources.Show(Me)
    End Sub
    Private Sub LoadToolResourcesForm()
        NoToolEvents = True
        If formToolResources IsNot Nothing Then
            If Backglass.currentData IsNot Nothing Then
                formToolResources.ImageInfoList = Backglass.currentData.Images
            Else
                formToolResources.ImageInfoList = Nothing
                formToolResources.cmbImageType.SelectedIndex = 0
                formToolResources.txtRomID.Text = String.Empty
                formToolResources.cmbROMIDType.SelectedIndex = 0
            End If
        End If
        NoToolEvents = False
    End Sub

    Private Sub CopyDMDArea()
        If Backglass.currentData IsNot Nothing Then
            With Backglass.currentData
                ' get location of rectangle
                .DMDDefaultLocation = .DMDCopyArea.Location
                ' get image
                Dim image As Image = .Image.PartFromImage(New Rectangle(.DMDCopyArea.Location, .DMDCopyArea.Size))
                Backglass.currentTabPage.DMDImage() = image
                tscmbImage.SelectedIndex = 1
                Backglass.currentData.Images.Insert(Images.eImageInfoType.Title4DMDImages, New Images.ImageInfo(Images.eImageInfoType.DMDImage, "", image))
                Backglass.currentData.IsSavedDMDImageDirty = True
                LoadToolResourcesForm()
            End With
        End If
    End Sub

    Private Sub LoadData(ByVal _backglassdata As Backglass.Data)
        If _backglassdata IsNot Nothing Then
            Dim loadData As Boolean = True
            Dim i As Integer = 0
            ' check whether the table is already loaded
            For Each backglass As B2STabPage In B2STab.TabPages
                If backglass.BackglassData.Name = _backglassdata.Name AndAlso backglass.BackglassData.BackupName = _backglassdata.BackupName Then
                    loadData = False
                    B2STab.SelectedIndex = i
                    Exit For
                End If
                i += 1
            Next
            ' load data
            If loadData Then
                B2STab.AddBackglass(New B2STabPage(_backglassdata))
                B2STab.SelectedIndex = B2STab.TabPages.Count - 1
                Backglass.currentData.IsDirty = False
            End If
            recent.AddToRecentList(_backglassdata)
        End If
    End Sub

    Private Sub ShowStatus(Optional ByVal text As String = "")
        If Not String.IsNullOrEmpty(text) Then
            tsLabelStatusInfo.Text = text
        Else
            If String.IsNullOrEmpty(tsLabelFileInfo.Text) Then
                tsLabelStatusInfo.Text = My.Resources.STATUS_Start
            Else
                tsLabelStatusInfo.Text = My.Resources.STATUS_Default
            End If
        End If
    End Sub

    Private Sub ShowProgress(ByVal value As Integer)
        tsProgress.Value = value
        If Not tsProgress.Visible OrElse (value = 0 AndAlso Not timerProgressReset.Enabled) Then
            tsProgress.Visible = True
        End If
        ssB2SDesigner.Refresh()
        If timerProgressReset.Enabled Then
            timerProgressReset.Stop()
        End If
        If value = 100 Then
            timerProgressReset.Start()
        End If
    End Sub

#End Region


End Class
