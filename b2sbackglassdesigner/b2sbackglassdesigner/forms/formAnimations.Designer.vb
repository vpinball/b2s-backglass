<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formAnimations
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formAnimations))
        Me.cmbAnimations = New System.Windows.Forms.ComboBox()
        Me.lblAnimations = New System.Windows.Forms.Label()
        Me.btnNewAnimation = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtInterval = New System.Windows.Forms.TextBox()
        Me.lblInterval = New System.Windows.Forms.Label()
        Me.txtLoops = New System.Windows.Forms.TextBox()
        Me.lblLoops = New System.Windows.Forms.Label()
        Me.chkAutostart = New System.Windows.Forms.CheckBox()
        Me.lvwSteps = New System.Windows.Forms.ListView()
        Me.hdStep = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdOn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdWaitAfterOn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdOff = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdWaitAfterOff = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hdPulseSwitch = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtLampsOn = New System.Windows.Forms.TextBox()
        Me.lblLampsOn = New System.Windows.Forms.Label()
        Me.txtWaitLoopsAfterOn = New System.Windows.Forms.TextBox()
        Me.lblWaitAfterOn = New System.Windows.Forms.Label()
        Me.txtWaitLoopsAfterOff = New System.Windows.Forms.TextBox()
        Me.lblWaitLoopsAfterOff = New System.Windows.Forms.Label()
        Me.txtLampsOff = New System.Windows.Forms.TextBox()
        Me.lblLampsOff = New System.Windows.Forms.Label()
        Me.btnStepUp = New System.Windows.Forms.Button()
        Me.btnStepDown = New System.Windows.Forms.Button()
        Me.txtIDJoin = New System.Windows.Forms.TextBox()
        Me.lblIDJoin = New System.Windows.Forms.Label()
        Me.btnNewStep = New System.Windows.Forms.Button()
        Me.btnShowAnimation = New System.Windows.Forms.Button()
        Me.btnRemoveStep = New System.Windows.Forms.Button()
        Me.btnRemoveAnimation = New System.Windows.Forms.Button()
        Me.cmbLightsStateAtEnd = New System.Windows.Forms.ComboBox()
        Me.B2SLine2 = New B2SBackglassDesigner.B2SLine()
        Me.B2SLine1 = New B2SBackglassDesigner.B2SLine()
        Me.chkLockInvolvedLamps = New System.Windows.Forms.CheckBox()
        Me.btnAllNames4LampsOn = New System.Windows.Forms.Button()
        Me.btnAllLamps4LampsOff = New System.Windows.Forms.Button()
        Me.chkBringToFront = New System.Windows.Forms.CheckBox()
        Me.chkHideScoreDisplays = New System.Windows.Forms.CheckBox()
        Me.cmbDualMode = New System.Windows.Forms.ComboBox()
        Me.lblDualMode = New System.Windows.Forms.Label()
        Me.cmbAnimationStopBehaviour = New System.Windows.Forms.ComboBox()
        Me.cmbLightsStateAtStart = New System.Windows.Forms.ComboBox()
        Me.chkRandomStart = New System.Windows.Forms.CheckBox()
        Me.numRandomQuality = New System.Windows.Forms.NumericUpDown()
        Me.txtPulseSwitch = New System.Windows.Forms.TextBox()
        Me.lblPulseSwitch = New System.Windows.Forms.Label()
        CType(Me.numRandomQuality, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbAnimations
        '
        resources.ApplyResources(Me.cmbAnimations, "cmbAnimations")
        Me.cmbAnimations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnimations.FormattingEnabled = True
        Me.cmbAnimations.Name = "cmbAnimations"
        Me.cmbAnimations.Sorted = True
        '
        'lblAnimations
        '
        resources.ApplyResources(Me.lblAnimations, "lblAnimations")
        Me.lblAnimations.Name = "lblAnimations"
        '
        'btnNewAnimation
        '
        resources.ApplyResources(Me.btnNewAnimation, "btnNewAnimation")
        Me.btnNewAnimation.Name = "btnNewAnimation"
        Me.btnNewAnimation.UseVisualStyleBackColor = True
        '
        'txtName
        '
        resources.ApplyResources(Me.txtName, "txtName")
        Me.txtName.Name = "txtName"
        '
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.Name = "lblName"
        '
        'txtInterval
        '
        resources.ApplyResources(Me.txtInterval, "txtInterval")
        Me.txtInterval.Name = "txtInterval"
        '
        'lblInterval
        '
        resources.ApplyResources(Me.lblInterval, "lblInterval")
        Me.lblInterval.Name = "lblInterval"
        '
        'txtLoops
        '
        resources.ApplyResources(Me.txtLoops, "txtLoops")
        Me.txtLoops.Name = "txtLoops"
        '
        'lblLoops
        '
        resources.ApplyResources(Me.lblLoops, "lblLoops")
        Me.lblLoops.Name = "lblLoops"
        '
        'chkAutostart
        '
        resources.ApplyResources(Me.chkAutostart, "chkAutostart")
        Me.chkAutostart.Name = "chkAutostart"
        Me.chkAutostart.UseVisualStyleBackColor = True
        '
        'lvwSteps
        '
        resources.ApplyResources(Me.lvwSteps, "lvwSteps")
        Me.lvwSteps.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.hdStep, Me.hdOn, Me.hdWaitAfterOn, Me.hdOff, Me.hdWaitAfterOff, Me.hdPulseSwitch})
        Me.lvwSteps.FullRowSelect = True
        Me.lvwSteps.GridLines = True
        Me.lvwSteps.HideSelection = False
        Me.lvwSteps.MultiSelect = False
        Me.lvwSteps.Name = "lvwSteps"
        Me.lvwSteps.UseCompatibleStateImageBehavior = False
        Me.lvwSteps.View = System.Windows.Forms.View.Details
        '
        'hdStep
        '
        resources.ApplyResources(Me.hdStep, "hdStep")
        '
        'hdOn
        '
        resources.ApplyResources(Me.hdOn, "hdOn")
        '
        'hdWaitAfterOn
        '
        resources.ApplyResources(Me.hdWaitAfterOn, "hdWaitAfterOn")
        '
        'hdOff
        '
        resources.ApplyResources(Me.hdOff, "hdOff")
        '
        'hdWaitAfterOff
        '
        resources.ApplyResources(Me.hdWaitAfterOff, "hdWaitAfterOff")
        '
        'hdPulseSwitch
        '
        resources.ApplyResources(Me.hdPulseSwitch, "hdPulseSwitch")
        '
        'txtLampsOn
        '
        resources.ApplyResources(Me.txtLampsOn, "txtLampsOn")
        Me.txtLampsOn.Name = "txtLampsOn"
        '
        'lblLampsOn
        '
        resources.ApplyResources(Me.lblLampsOn, "lblLampsOn")
        Me.lblLampsOn.Name = "lblLampsOn"
        '
        'txtWaitLoopsAfterOn
        '
        resources.ApplyResources(Me.txtWaitLoopsAfterOn, "txtWaitLoopsAfterOn")
        Me.txtWaitLoopsAfterOn.Name = "txtWaitLoopsAfterOn"
        '
        'lblWaitAfterOn
        '
        resources.ApplyResources(Me.lblWaitAfterOn, "lblWaitAfterOn")
        Me.lblWaitAfterOn.Name = "lblWaitAfterOn"
        '
        'txtWaitLoopsAfterOff
        '
        resources.ApplyResources(Me.txtWaitLoopsAfterOff, "txtWaitLoopsAfterOff")
        Me.txtWaitLoopsAfterOff.Name = "txtWaitLoopsAfterOff"
        '
        'lblWaitLoopsAfterOff
        '
        resources.ApplyResources(Me.lblWaitLoopsAfterOff, "lblWaitLoopsAfterOff")
        Me.lblWaitLoopsAfterOff.Name = "lblWaitLoopsAfterOff"
        '
        'txtLampsOff
        '
        resources.ApplyResources(Me.txtLampsOff, "txtLampsOff")
        Me.txtLampsOff.Name = "txtLampsOff"
        '
        'lblLampsOff
        '
        resources.ApplyResources(Me.lblLampsOff, "lblLampsOff")
        Me.lblLampsOff.Name = "lblLampsOff"
        '
        'btnStepUp
        '
        resources.ApplyResources(Me.btnStepUp, "btnStepUp")
        Me.btnStepUp.BackgroundImage = Global.B2SBackglassDesigner.My.Resources.Resources.arrow2_up
        Me.btnStepUp.Name = "btnStepUp"
        Me.btnStepUp.UseVisualStyleBackColor = True
        '
        'btnStepDown
        '
        resources.ApplyResources(Me.btnStepDown, "btnStepDown")
        Me.btnStepDown.BackgroundImage = Global.B2SBackglassDesigner.My.Resources.Resources.arrow2_down
        Me.btnStepDown.Name = "btnStepDown"
        Me.btnStepDown.UseVisualStyleBackColor = True
        '
        'txtIDJoin
        '
        resources.ApplyResources(Me.txtIDJoin, "txtIDJoin")
        Me.txtIDJoin.Name = "txtIDJoin"
        '
        'lblIDJoin
        '
        resources.ApplyResources(Me.lblIDJoin, "lblIDJoin")
        Me.lblIDJoin.Name = "lblIDJoin"
        '
        'btnNewStep
        '
        resources.ApplyResources(Me.btnNewStep, "btnNewStep")
        Me.btnNewStep.BackgroundImage = Global.B2SBackglassDesigner.My.Resources.Resources.new1
        Me.btnNewStep.Name = "btnNewStep"
        Me.btnNewStep.UseVisualStyleBackColor = True
        '
        'btnShowAnimation
        '
        resources.ApplyResources(Me.btnShowAnimation, "btnShowAnimation")
        Me.btnShowAnimation.BackgroundImage = Global.B2SBackglassDesigner.My.Resources.Resources.illumination
        Me.btnShowAnimation.Name = "btnShowAnimation"
        Me.btnShowAnimation.UseVisualStyleBackColor = True
        '
        'btnRemoveStep
        '
        resources.ApplyResources(Me.btnRemoveStep, "btnRemoveStep")
        Me.btnRemoveStep.BackgroundImage = Global.B2SBackglassDesigner.My.Resources.Resources.delete_red
        Me.btnRemoveStep.Name = "btnRemoveStep"
        Me.btnRemoveStep.UseVisualStyleBackColor = True
        '
        'btnRemoveAnimation
        '
        resources.ApplyResources(Me.btnRemoveAnimation, "btnRemoveAnimation")
        Me.btnRemoveAnimation.Name = "btnRemoveAnimation"
        Me.btnRemoveAnimation.UseVisualStyleBackColor = True
        '
        'cmbLightsStateAtEnd
        '
        resources.ApplyResources(Me.cmbLightsStateAtEnd, "cmbLightsStateAtEnd")
        Me.cmbLightsStateAtEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLightsStateAtEnd.FormattingEnabled = True
        Me.cmbLightsStateAtEnd.Items.AddRange(New Object() {resources.GetString("cmbLightsStateAtEnd.Items"), resources.GetString("cmbLightsStateAtEnd.Items1"), resources.GetString("cmbLightsStateAtEnd.Items2"), resources.GetString("cmbLightsStateAtEnd.Items3")})
        Me.cmbLightsStateAtEnd.Name = "cmbLightsStateAtEnd"
        '
        'B2SLine2
        '
        resources.ApplyResources(Me.B2SLine2, "B2SLine2")
        Me.B2SLine2.Name = "B2SLine2"
        '
        'B2SLine1
        '
        resources.ApplyResources(Me.B2SLine1, "B2SLine1")
        Me.B2SLine1.Name = "B2SLine1"
        '
        'chkLockInvolvedLamps
        '
        resources.ApplyResources(Me.chkLockInvolvedLamps, "chkLockInvolvedLamps")
        Me.chkLockInvolvedLamps.Name = "chkLockInvolvedLamps"
        Me.chkLockInvolvedLamps.UseVisualStyleBackColor = True
        '
        'btnAllNames4LampsOn
        '
        resources.ApplyResources(Me.btnAllNames4LampsOn, "btnAllNames4LampsOn")
        Me.btnAllNames4LampsOn.Name = "btnAllNames4LampsOn"
        Me.btnAllNames4LampsOn.UseVisualStyleBackColor = True
        '
        'btnAllLamps4LampsOff
        '
        resources.ApplyResources(Me.btnAllLamps4LampsOff, "btnAllLamps4LampsOff")
        Me.btnAllLamps4LampsOff.Name = "btnAllLamps4LampsOff"
        Me.btnAllLamps4LampsOff.UseVisualStyleBackColor = True
        '
        'chkBringToFront
        '
        resources.ApplyResources(Me.chkBringToFront, "chkBringToFront")
        Me.chkBringToFront.Name = "chkBringToFront"
        Me.chkBringToFront.UseVisualStyleBackColor = True
        '
        'chkHideScoreDisplays
        '
        resources.ApplyResources(Me.chkHideScoreDisplays, "chkHideScoreDisplays")
        Me.chkHideScoreDisplays.Name = "chkHideScoreDisplays"
        Me.chkHideScoreDisplays.UseVisualStyleBackColor = True
        '
        'cmbDualMode
        '
        resources.ApplyResources(Me.cmbDualMode, "cmbDualMode")
        Me.cmbDualMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDualMode.FormattingEnabled = True
        Me.cmbDualMode.Items.AddRange(New Object() {resources.GetString("cmbDualMode.Items"), resources.GetString("cmbDualMode.Items1"), resources.GetString("cmbDualMode.Items2")})
        Me.cmbDualMode.Name = "cmbDualMode"
        '
        'lblDualMode
        '
        resources.ApplyResources(Me.lblDualMode, "lblDualMode")
        Me.lblDualMode.Name = "lblDualMode"
        '
        'cmbAnimationStopBehaviour
        '
        resources.ApplyResources(Me.cmbAnimationStopBehaviour, "cmbAnimationStopBehaviour")
        Me.cmbAnimationStopBehaviour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnimationStopBehaviour.FormattingEnabled = True
        Me.cmbAnimationStopBehaviour.Items.AddRange(New Object() {resources.GetString("cmbAnimationStopBehaviour.Items"), resources.GetString("cmbAnimationStopBehaviour.Items1"), resources.GetString("cmbAnimationStopBehaviour.Items2")})
        Me.cmbAnimationStopBehaviour.Name = "cmbAnimationStopBehaviour"
        '
        'cmbLightsStateAtStart
        '
        resources.ApplyResources(Me.cmbLightsStateAtStart, "cmbLightsStateAtStart")
        Me.cmbLightsStateAtStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLightsStateAtStart.FormattingEnabled = True
        Me.cmbLightsStateAtStart.Items.AddRange(New Object() {resources.GetString("cmbLightsStateAtStart.Items"), resources.GetString("cmbLightsStateAtStart.Items1"), resources.GetString("cmbLightsStateAtStart.Items2"), resources.GetString("cmbLightsStateAtStart.Items3")})
        Me.cmbLightsStateAtStart.Name = "cmbLightsStateAtStart"
        '
        'chkRandomStart
        '
        resources.ApplyResources(Me.chkRandomStart, "chkRandomStart")
        Me.chkRandomStart.Name = "chkRandomStart"
        Me.chkRandomStart.UseVisualStyleBackColor = True
        '
        'numRandomQuality
        '
        resources.ApplyResources(Me.numRandomQuality, "numRandomQuality")
        Me.numRandomQuality.Name = "numRandomQuality"
        '
        'txtPulseSwitch
        '
        resources.ApplyResources(Me.txtPulseSwitch, "txtPulseSwitch")
        Me.txtPulseSwitch.Name = "txtPulseSwitch"
        '
        'lblPulseSwitch
        '
        resources.ApplyResources(Me.lblPulseSwitch, "lblPulseSwitch")
        Me.lblPulseSwitch.Name = "lblPulseSwitch"
        '
        'formAnimations
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Controls.Add(Me.txtPulseSwitch)
        Me.Controls.Add(Me.lblPulseSwitch)
        Me.Controls.Add(Me.numRandomQuality)
        Me.Controls.Add(Me.chkRandomStart)
        Me.Controls.Add(Me.cmbLightsStateAtStart)
        Me.Controls.Add(Me.cmbAnimationStopBehaviour)
        Me.Controls.Add(Me.cmbDualMode)
        Me.Controls.Add(Me.lblDualMode)
        Me.Controls.Add(Me.chkHideScoreDisplays)
        Me.Controls.Add(Me.chkBringToFront)
        Me.Controls.Add(Me.btnAllLamps4LampsOff)
        Me.Controls.Add(Me.btnAllNames4LampsOn)
        Me.Controls.Add(Me.chkLockInvolvedLamps)
        Me.Controls.Add(Me.cmbLightsStateAtEnd)
        Me.Controls.Add(Me.btnRemoveAnimation)
        Me.Controls.Add(Me.btnRemoveStep)
        Me.Controls.Add(Me.btnShowAnimation)
        Me.Controls.Add(Me.btnNewStep)
        Me.Controls.Add(Me.txtIDJoin)
        Me.Controls.Add(Me.lblIDJoin)
        Me.Controls.Add(Me.btnStepDown)
        Me.Controls.Add(Me.btnStepUp)
        Me.Controls.Add(Me.txtWaitLoopsAfterOff)
        Me.Controls.Add(Me.lblWaitLoopsAfterOff)
        Me.Controls.Add(Me.txtLampsOff)
        Me.Controls.Add(Me.lblLampsOff)
        Me.Controls.Add(Me.txtWaitLoopsAfterOn)
        Me.Controls.Add(Me.lblWaitAfterOn)
        Me.Controls.Add(Me.txtLampsOn)
        Me.Controls.Add(Me.lblLampsOn)
        Me.Controls.Add(Me.lvwSteps)
        Me.Controls.Add(Me.B2SLine2)
        Me.Controls.Add(Me.chkAutostart)
        Me.Controls.Add(Me.txtLoops)
        Me.Controls.Add(Me.lblLoops)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.lblInterval)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.B2SLine1)
        Me.Controls.Add(Me.btnNewAnimation)
        Me.Controls.Add(Me.cmbAnimations)
        Me.Controls.Add(Me.lblAnimations)
        Me.Name = "formAnimations"
        CType(Me.numRandomQuality, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbAnimations As System.Windows.Forms.ComboBox
    Friend WithEvents lblAnimations As System.Windows.Forms.Label
    Friend WithEvents btnNewAnimation As System.Windows.Forms.Button
    Friend WithEvents B2SLine1 As B2SBackglassDesigner.B2SLine
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    Friend WithEvents lblInterval As System.Windows.Forms.Label
    Friend WithEvents txtLoops As System.Windows.Forms.TextBox
    Friend WithEvents lblLoops As System.Windows.Forms.Label
    Friend WithEvents chkAutostart As System.Windows.Forms.CheckBox
    Friend WithEvents B2SLine2 As B2SBackglassDesigner.B2SLine
    Friend WithEvents lvwSteps As System.Windows.Forms.ListView
    Friend WithEvents hdStep As System.Windows.Forms.ColumnHeader
    Friend WithEvents hdOn As System.Windows.Forms.ColumnHeader
    Friend WithEvents hdWaitAfterOn As System.Windows.Forms.ColumnHeader
    Friend WithEvents hdOff As System.Windows.Forms.ColumnHeader
    Friend WithEvents hdWaitAfterOff As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtLampsOn As System.Windows.Forms.TextBox
    Friend WithEvents lblLampsOn As System.Windows.Forms.Label
    Friend WithEvents txtWaitLoopsAfterOn As System.Windows.Forms.TextBox
    Friend WithEvents lblWaitAfterOn As System.Windows.Forms.Label
    Friend WithEvents txtWaitLoopsAfterOff As System.Windows.Forms.TextBox
    Friend WithEvents lblWaitLoopsAfterOff As System.Windows.Forms.Label
    Friend WithEvents txtLampsOff As System.Windows.Forms.TextBox
    Friend WithEvents lblLampsOff As System.Windows.Forms.Label
    Friend WithEvents btnStepUp As System.Windows.Forms.Button
    Friend WithEvents btnStepDown As System.Windows.Forms.Button
    Friend WithEvents txtIDJoin As System.Windows.Forms.TextBox
    Friend WithEvents lblIDJoin As System.Windows.Forms.Label
    Friend WithEvents btnNewStep As System.Windows.Forms.Button
    Friend WithEvents btnShowAnimation As System.Windows.Forms.Button
    Friend WithEvents btnRemoveStep As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAnimation As System.Windows.Forms.Button
    Friend WithEvents cmbLightsStateAtEnd As System.Windows.Forms.ComboBox
    Friend WithEvents chkLockInvolvedLamps As System.Windows.Forms.CheckBox
    Friend WithEvents btnAllNames4LampsOn As System.Windows.Forms.Button
    Friend WithEvents btnAllLamps4LampsOff As System.Windows.Forms.Button
    Friend WithEvents chkBringToFront As System.Windows.Forms.CheckBox
    Friend WithEvents chkHideScoreDisplays As System.Windows.Forms.CheckBox
    Friend WithEvents cmbDualMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblDualMode As System.Windows.Forms.Label
    Friend WithEvents cmbAnimationStopBehaviour As System.Windows.Forms.ComboBox
    Friend WithEvents cmbLightsStateAtStart As System.Windows.Forms.ComboBox
    Friend WithEvents hdPulseSwitch As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkRandomStart As System.Windows.Forms.CheckBox
    Friend WithEvents numRandomQuality As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtPulseSwitch As System.Windows.Forms.TextBox
    Friend WithEvents lblPulseSwitch As System.Windows.Forms.Label
End Class
