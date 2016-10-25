Imports System

Public Class formToolIllumination

    Public ignoreChange As Boolean = False

    Public Enum eIlluminationDataType
        NotDefined = 0
        Name = 1
        ID = 2
        B2SID = 3
        B2SIDType = 4
        B2SValue = 5
        RomID = 11
        RomIDType = 12
        RomInverted = 13
        InitialState = 21
        DualMode = 22
        Intensity = 23
        DodgeColor = 24
        IlluMode = 25
        Illuminationtext = 31
        IlluminationtextFont = 32
        IlluminationtextAlignment = 33
        ChangeLightColor = 36
        ChangeDodgeColor = 37
        Location = 41
        Size = 42
        ZOrder = 51
        SnippitInfo = 52
    End Enum

    Public Event DataChanged(ByVal sender As Object, ByVal e As IlluminationEventArgs)
    Public Class IlluminationEventArgs
        Inherits EventArgs

        Public TypeOfData As eIlluminationDataType = eIlluminationDataType.NotDefined
        Public Data As Object = Nothing

        Public Sub New(ByVal _typeofdata As eIlluminationDataType)
            TypeOfData = _typeofdata
        End Sub
        Public Sub New(ByVal _typeofdata As eIlluminationDataType, ByVal _data As Object)
            TypeOfData = _typeofdata
            Data = _data
        End Sub
    End Class

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MyBase.DefaultLocation = eDefaultLocation.SE
        MyBase.SaveName = Me.Name

    End Sub

    Private _MyFont As Font
    Public Property MyFont() As Font
        Get
            Return _MyFont
        End Get
        Set(ByVal value As Font)
            If _MyFont IsNot Nothing Then _MyFont.Dispose()
            _MyFont = value
            If value IsNot Nothing Then
                btnFonts.Text = value.Name & ", Size=" & value.Size
            Else
                btnFonts.Text = My.Resources.TXT_ChooseFont
            End If
        End Set
    End Property

    Private _IsSnippit As Boolean
    Public Property IsSnippit() As Boolean
        Get
            Return _IsSnippit
        End Get
        Set(ByVal value As Boolean)
            _IsSnippit = value
            btnSnippitSettings.Enabled = value
        End Set
    End Property
    Public Property ZOrder() As Integer = 0
    Public Property SnippitType() As eSnippitType = eSnippitType.StandardImage
    Public Property SnippitMechID() As Integer = 0
    Public Property SnippitRotatingSteps() As Integer = 0
    Public Property SnippitRotatingInterval() As Integer = 0
    Public Property SnippitRotatingDirection() As eSnippitRotationDirection = eSnippitRotationDirection.Clockwise
    Public Property SnippitRotatingStopBehaviour() As eSnippitRotationStopBehaviour = eSnippitRotationStopBehaviour.SpinOff

    Private Sub formToolIllumination_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cmbDodgeColor.SelectionLength = 0
        cmbInitState.SelectionLength = 0
        cmbROMIDType.SelectionLength = 0
        cmbB2SIDType.SelectionLength = 0
    End Sub

    Private Sub Name_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtName.TextChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.Name, txtName.Text))
    End Sub
    Private Sub ID_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtID.TextChanged
        If Not String.IsNullOrEmpty(txtID.Text) AndAlso Not IsNumeric(txtID.Text) Then txtID.Text = "0"
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.ID, txtID.Text))
    End Sub
    Private Sub InitState_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbInitState.SelectedIndexChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.InitialState, cmbInitState.SelectedIndex))
    End Sub
    Private Sub DualMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDualMode.SelectedIndexChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.DualMode, cmbDualMode.SelectedIndex))
    End Sub

    Private Sub B2SID_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtB2SID.TextChanged
        If Not String.IsNullOrEmpty(txtB2SID.Text) Then
            If (Not IsNumeric(txtB2SID.Text) OrElse txtB2SID.Text = "0") Then txtB2SID.Text = ""
            If Not ignoreChange AndAlso Not String.IsNullOrEmpty(txtB2SID.Text) Then cmbB2SIDType.SelectedIndex = 0
        End If
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.B2SID, txtB2SID.Text))
    End Sub
    Private Sub B2SIDType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbB2SIDType.SelectedIndexChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.B2SIDType, cmbB2SIDType.SelectedIndex))
        ignoreChange = True
        Select Case cmbB2SIDType.SelectedIndex
            Case 1 : txtB2SID.Text = 25
            Case 2 : txtB2SID.Text = 26
            Case 3 : txtB2SID.Text = 27
            Case 4 : txtB2SID.Text = 28
            Case 5 : txtB2SID.Text = 30
            Case 6 : txtB2SID.Text = 31
            Case 7 : txtB2SID.Text = 32
            Case 8 : txtB2SID.Text = 33
            Case 9 : txtB2SID.Text = 34
            Case 10 : txtB2SID.Text = 35
            Case 11 : txtB2SID.Text = 36
        End Select
        ignoreChange = False
    End Sub
    Private Sub B2SValue_TextChanged(sender As Object, e As System.EventArgs) Handles txtB2SValue.TextChanged
        If Not String.IsNullOrEmpty(txtB2SValue.Text) Then
            If (Not IsNumeric(txtB2SValue.Text) OrElse txtB2SValue.Text = "0") Then txtB2SValue.Text = ""
        End If
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.B2SValue, txtB2SValue.Text))
    End Sub
    Private Sub RomID_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtRomID.TextChanged
        If Not String.IsNullOrEmpty(txtRomID.Text) Then
            If (Not IsNumeric(txtRomID.Text) OrElse txtRomID.Text = "0") Then txtRomID.Text = ""
            If Not String.IsNullOrEmpty(txtRomID.Text) AndAlso cmbROMIDType.SelectedIndex = 0 Then cmbROMIDType.SelectedIndex = 1
        End If
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.RomID, txtRomID.Text))
    End Sub
    Private Sub RomIDType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbROMIDType.SelectedIndexChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.RomIDType, cmbROMIDType.SelectedIndex))
    End Sub
    Private Sub RomInverted_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRomInverted.CheckedChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.RomInverted, chkRomInverted.Checked))
    End Sub

    Private Sub Intensity_Scroll(sender As System.Object, e As System.EventArgs) Handles TrackBarIntensity.Scroll
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.Intensity, TrackBarIntensity.Value))
    End Sub
    Private Sub LightColor_Click(sender As System.Object, e As System.EventArgs) Handles btnLightColor.Click
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.ChangeLightColor))
    End Sub
    Private Sub DodgeColor_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDodgeColor.SelectedIndexChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.DodgeColor, TranslateIndex2DodgeColor(cmbDodgeColor.SelectedIndex)))
    End Sub
    Private Sub IlluMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbIlluMode.SelectedIndexChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.IlluMode, cmbIlluMode.SelectedIndex))
    End Sub

    Private Sub IlluminationText_TextChanged(sender As Object, e As System.EventArgs) Handles txtIlluminationText.TextChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.Illuminationtext, txtIlluminationText.Text))
    End Sub
    Private Sub AlignLeft_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAlignLeft.CheckedChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.IlluminationtextAlignment, Illumination.eTextAlignment.Left))
    End Sub
    Private Sub AlignCenter_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAlignCenter.CheckedChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.IlluminationtextAlignment, Illumination.eTextAlignment.Center))
    End Sub
    Private Sub AlignRight_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAlignRight.CheckedChanged
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.IlluminationtextAlignment, Illumination.eTextAlignment.Right))
    End Sub
    Private Sub Fonts_Click(sender As System.Object, e As System.EventArgs) Handles btnFonts.Click
        On Error Resume Next
        Dim fonts As FontDialog = New FontDialog()
        If _MyFont IsNot Nothing Then fonts.Font = _MyFont
        If fonts.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            btnFonts.Text = fonts.Font.Name & ", Size=" & fonts.Font.Size
            _MyFont = fonts.Font
            RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.IlluminationtextFont, fonts.Font))
        End If
        fonts.Dispose()
    End Sub

    Private Sub LocationX_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLocationX.TextChanged
        If ignoreChange Then Return
        If Not IsNumeric(txtLocationX.Text) Then txtLocationX.Text = 0
        If Not IsNumeric(txtLocationY.Text) Then Return
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.Location, New Point(CInt(txtLocationX.Text), CInt(txtLocationY.Text))))
    End Sub
    Private Sub LocationY_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLocationY.TextChanged
        If ignoreChange Then Return
        If Not IsNumeric(txtLocationY.Text) Then txtLocationY.Text = 0
        If Not IsNumeric(txtLocationX.Text) Then Return
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.Location, New Point(CInt(txtLocationX.Text), CInt(txtLocationY.Text))))
    End Sub
    Private Sub SizeWidth_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSizeWidth.TextChanged
        If ignoreChange Then Return
        If Not IsNumeric(txtSizeWidth.Text) Then txtSizeWidth.Text = 0
        If Not IsNumeric(txtSizeHeight.Text) Then Return
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.Size, New Size(CInt(txtSizeWidth.Text), CInt(txtSizeHeight.Text))))
    End Sub
    Private Sub SizeHeight_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSizeHeight.TextChanged
        If ignoreChange Then Return
        If Not IsNumeric(txtSizeHeight.Text) Then txtSizeHeight.Text = 0
        If Not IsNumeric(txtSizeWidth.Text) Then Return
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.Size, New Size(CInt(txtSizeWidth.Text), CInt(txtSizeHeight.Text))))
    End Sub
    Private Sub Size_Validated(sender As Object, e As System.EventArgs) Handles txtSizeWidth.Validated, txtSizeHeight.Validated
        If Not IsNumeric(txtSizeWidth.Text) Then txtSizeWidth.Text = "10"
        If Not IsNumeric(txtSizeHeight.Text) Then txtSizeHeight.Text = "10"
        If CInt(txtSizeWidth.Text) < 10 Then txtSizeWidth.Text = "10"
        If CInt(txtSizeHeight.Text) < 10 Then txtSizeHeight.Text = "10"
        RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.Size, New Size(CInt(txtSizeWidth.Text), CInt(txtSizeHeight.Text))))
    End Sub

    Private Sub SnippitSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnSnippitSettings.Click
        If IsSnippit Then
            Dim formSnippit As formSnippitSettings = New formSnippitSettings()
            If formSnippit.ShowDialog(Me, CInt(txtID.Text), txtName.Text, SnippitType, ZOrder, SnippitMechID, SnippitRotatingSteps, SnippitRotatingInterval, SnippitRotatingDirection, SnippitRotatingStopBehaviour) = Windows.Forms.DialogResult.OK Then
                Dim snippitinfo As Illumination.SnippitInfo = New Illumination.SnippitInfo()
                snippitinfo.SnippitType = SnippitType
                snippitinfo.SnippitMechID = SnippitMechID
                snippitinfo.SnippitRotatingSteps = SnippitRotatingSteps
                snippitinfo.SnippitRotatingInterval = SnippitRotatingInterval
                snippitinfo.SnippitRotatingDirection = SnippitRotatingDirection
                snippitinfo.SnippitRotatingStopBehaviour = SnippitRotatingStopBehaviour
                RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.ZOrder, ZOrder))
                RaiseEvent DataChanged(Me, New IlluminationEventArgs(eIlluminationDataType.SnippitInfo, snippitinfo))
            End If
        End If
    End Sub

End Class