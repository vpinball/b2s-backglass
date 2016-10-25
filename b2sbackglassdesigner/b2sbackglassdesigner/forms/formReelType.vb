Imports System
Imports System.Runtime.InteropServices

Public Class formReelType

    <DllImport("gdi32.dll")>
    Private Shared Function CreateDC(ByVal lpszDriver As String, ByVal lpszDevice As String, ByVal lpszOutput As String, ByVal lpInitData As IntPtr) As IntPtr
    End Function
    <DllImport("gdi32.dll")> _
    Private Shared Function DeleteDC(ByVal hdc As IntPtr) As Boolean
    End Function
    <DllImport("gdi32.dll")> _
    Private Shared Function GetPixel(ByVal hdc As IntPtr, ByVal nXPos As Integer, ByVal nYPos As Integer) As Integer
    End Function
    <DllImport("user32.dll")> _
    Private Shared Function GetAsyncKeyState(ByVal vKey As Int32) As Short
    End Function

    Private IsDirty As Boolean = False
    Private currentKeycode As Keys = Keys.D0

    Public Shadows Function ShowDialog(ByVal owner As IWin32Window,
                                       ByRef reeltypes As String,
                                       ByRef reelcolor As Color,
                                       ByRef reelrollingdirection As eReelRollingDirection,
                                       ByRef reelrollinginterval As Integer,
                                       ByRef reelintermediatecount As Integer,
                                       ByRef usedream7leds As Boolean,
                                       ByRef d7glow As Single,
                                       ByRef d7thickness As Single,
                                       ByRef d7shear As Single) As DialogResult
        ' set a value to the LED controls
        formReelType_KeyDown(Me, New KeyEventArgs(Keys.D0))
        ' set current color for rendered LEDs
        If reelcolor = Nothing Then
            B2SColorBarLEDs.CurrentColor = Color.OrangeRed
        Else
            B2SColorBarLEDs.CurrentColor = reelcolor
        End If
        cmbRollingDirection.SelectedIndex = reelrollingdirection
        If reelrollinginterval < numRollingInterval.Minimum Then reelrollinginterval = numRollingInterval.Minimum
        numRollingInterval.Value = reelrollinginterval
        numIntermediateCount.Value = reelintermediatecount
        ' set some value for Dream7 LEDs
        chkUseDream7LEDs.Checked = usedream7leds
        If d7glow = 0 Then d7glow = 9
        If d7thickness = 0 Then d7thickness = 14
        If d7shear = 0 Then d7shear = 0.1
        TrackBarGlow.Value = CInt(d7glow)
        TrackBarThickness.Value = Math.Max(CInt(d7thickness / 2), 1)
        TrackBarShear.Value = CInt(d7shear * 50)
        ' show dialog
        If lvEMReels.Items.Count = 0 Then FillReelListView(eImageSetType.ReelImages, ilReels, lvEMReels, DefaultEMReels)
        If lvEMCreditReels.Items.Count = 0 Then FillReelListView(eImageSetType.CreditReelImages, ilEMCeditReels, lvEMCreditReels, DefaultEMCreditReels)
        If lvLEDs.Items.Count = 0 Then FillReelListView(eImageSetType.LEDImages, ilLEDs, lvLEDs, DefaultLEDs)
        If Not String.IsNullOrEmpty(reeltypes) Then
            Dim i As Integer = 0
            For Each item As Object In lvEMReels.Items
                lvEMReels.Items(i).Selected = (reeltypes.Contains("," & item.Name & ","))
                i += 1
            Next
            i = 0
            For Each item As Object In lvEMCreditReels.Items
                lvEMCreditReels.Items(i).Selected = (reeltypes.Contains("," & item.Name & ","))
                i += 1
            Next
            i = 0
            For Each item As Object In lvLEDs.Items
                lvLEDs.Items(i).Selected = (reeltypes.Contains("," & item.Name & ","))
                i += 1
            Next
            For Each item As Object In GetAllRenderedLEDs()
                item.Selected = (reeltypes.Contains("," & GetRenderedLEDName(item.Name) & ","))
            Next
        End If
        IsDirty = False
        ' now show the dialog
        Dim nRet As DialogResult = MyBase.ShowDialog(owner)
        If nRet = Windows.Forms.DialogResult.OK Then
            reeltypes = String.Empty
            reelrollingdirection = cmbRollingDirection.SelectedIndex
            reelrollinginterval = numRollingInterval.Value
            reelintermediatecount = numIntermediateCount.Value
            usedream7leds = chkUseDream7LEDs.Checked
            d7glow = TrackBarGlow.Value
            d7thickness = TrackBarThickness.Value * 2
            d7shear = TrackBarShear.Value / 50
            ' return some values
            reelcolor = Nothing
            reelcolor = B2SColorBarLEDs.CurrentColor
            For Each item As Object In lvEMReels.SelectedItems
                reeltypes &= "," & item.Name
            Next
            For Each item As Object In lvEMCreditReels.SelectedItems
                reeltypes &= "," & item.Name
            Next
            For Each item As Object In lvLEDs.SelectedItems
                reeltypes &= "," & item.Name
            Next
            For Each item As Control In GetSelectedRenderedLEDs()
                If Not reeltypes.Contains(GetRenderedLEDName(item.Name)) Then reeltypes &= "," & GetRenderedLEDName(item.Name)
            Next
            If Not String.IsNullOrEmpty(reeltypes) Then
                reeltypes &= ","
            End If
            For Each score As ReelAndLED.ScoreInfo In Backglass.currentData.Scores
                If Not reeltypes.Contains("," & score.ReelType & ",") Then
                    reeltypes &= "," & score.ReelType & ","
                End If
                If String.IsNullOrEmpty(score.ReelType) Then
                    score.ReelType = GetFirstSelectedReelType(reeltypes)
                End If
            Next
        End If
        Return nRet
    End Function

    Private Sub formReelType_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        PanelRenderedLEDs.Location = PanelDream7LEDs.Location
    End Sub
    Private Sub formReelType_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
            currentKeycode = e.KeyCode
            RenderedLED08.LED.Draw(e.KeyCode)
            RenderedLED10.LED.Draw(e.KeyCode)
            RenderedLED14.LED.Draw(e.KeyCode)
            Dream7LED08.LED.SetValue(0, Chr(e.KeyCode))
            Dream7LED10.LED.SetValue(0, Chr(e.KeyCode))
            Dream7LED14.LED.SetValue(0, Chr(e.KeyCode))
        End If
    End Sub

    Private Sub Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        MyBase.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If IsDirty Then
            Dim ret As DialogResult = MessageBox.Show(My.Resources.MSG_IsDirty, AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If ret = Windows.Forms.DialogResult.Yes Then
                btnOk.PerformClick()
            ElseIf ret = Windows.Forms.DialogResult.No Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub UseDream7LEDs_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUseDream7LEDs.CheckedChanged
        PanelDream7LEDs.Visible = chkUseDream7LEDs.Checked
        PanelRenderedLEDs.Visible = Not chkUseDream7LEDs.Checked
    End Sub
    Private Sub Dream7LED_Click(sender As Object, e As System.EventArgs) Handles Dream7LED08.Click, Dream7LED10.Click, Dream7LED14.Click
        DirectCast(PanelRenderedLEDs.Controls("RenderedLED" & sender.Name.ToString().Substring(sender.Name.ToString().Length - 2)), B2SRenderedLEDPanel).Selected = sender.Selected
    End Sub
    Private Sub RenderedLED_Click(sender As Object, e As System.EventArgs) Handles RenderedLED08.Click, RenderedLED10.Click, RenderedLED14.Click
        DirectCast(PanelDream7LEDs.Controls("Dream7LED" & sender.Name.ToString().Substring(sender.Name.ToString().Length - 2)), B2SDream7LEDPanel).Selected = sender.Selected
    End Sub

    Private Sub B2SColorBarLEDs_ColorChanged(sender As Object, e As System.EventArgs) Handles B2SColorBarLEDs.ColorChanged
        RenderedLED08.LED.ForeColor = B2SColorBarLEDs.CurrentColor
        RenderedLED10.LED.ForeColor = B2SColorBarLEDs.CurrentColor
        RenderedLED14.LED.ForeColor = B2SColorBarLEDs.CurrentColor
        ' set dream7 colors
        Dream7LED08.LEDColor = B2SColorBarLEDs.CurrentColor
        Dream7LED10.LEDColor = B2SColorBarLEDs.CurrentColor
        Dream7LED14.LEDColor = B2SColorBarLEDs.CurrentColor
    End Sub

    Private Sub TrackBarGlow_ValueChanged(sender As Object, e As System.EventArgs) Handles TrackBarGlow.ValueChanged
        Dream7LED08.LED.Glow = TrackBarGlow.Value
        Dream7LED10.LED.Glow = TrackBarGlow.Value
        Dream7LED14.LED.Glow = TrackBarGlow.Value
        txtGlow.Text = TrackBarGlow.Value.ToString()
    End Sub
    Private Sub TrackBarThickness_ValueChanged(sender As Object, e As System.EventArgs) Handles TrackBarThickness.ValueChanged
        Dream7LED08.LED.Thickness = TrackBarThickness.Value * 2
        Dream7LED10.LED.Thickness = TrackBarThickness.Value * 2
        Dream7LED14.LED.Thickness = TrackBarThickness.Value * 2
        txtSize.Text = TrackBarThickness.Value.ToString()
        ' refresh current key
        formReelType_KeyDown(sender, New KeyEventArgs(currentKeycode))
    End Sub
    Private Sub TrackBarShear_ValueChanged(sender As Object, e As System.EventArgs) Handles TrackBarShear.ValueChanged
        Dream7LED08.LED.Shear = TrackBarShear.Value / 50
        Dream7LED10.LED.Shear = TrackBarShear.Value / 50
        Dream7LED14.LED.Shear = TrackBarShear.Value / 50
        txtShear.Text = TrackBarShear.Value.ToString()
    End Sub

    Private Sub Reels_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lvEMReels.MouseDoubleClick
        MyBase.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub GetColor_Click(sender As System.Object, e As System.EventArgs) Handles btnGetColor.Click
        If TimerGetColor.Enabled Then
            TimerGetColor.Stop()
            Me.CancelButton = btnCancel
        Else
            Me.CancelButton = Nothing
            MessageBox.Show(My.Resources.MSG_GetColor, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            TimerGetColor.Start()
        End If
    End Sub
    Private Sub TimerGetColor_Tick(sender As Object, e As System.EventArgs) Handles TimerGetColor.Tick
        Dim keyState As Short = GetAsyncKeyState(Keys.Escape)
        If keyState <> 0 Then
            TimerGetColor.Stop()
            Me.CancelButton = btnCancel
        Else
            B2SColorBarLEDs.CurrentColor = GetPixelColor(Cursor.Position.X, Cursor.Position.Y)
        End If
    End Sub
    Private Function GetPixelColor(ByVal x As Integer, ByVal y As Integer) As Color
        Dim hdcScreen As IntPtr = CreateDC("Display", Nothing, Nothing, IntPtr.Zero)
        Dim colorRef As Integer = GetPixel(hdcScreen, x, y)
        DeleteDC(hdcScreen)
        Return Color.FromArgb(colorRef And &HFF, (colorRef And &HFF00) >> 8, (colorRef And &HFF0000) >> 16)
    End Function

    Private Sub LEDs_Click(sender As Object, e As System.EventArgs) Handles lvLEDs.Click
        If lvLEDs.SelectedItems.Count > 0 Then
            If MessageBox.Show(My.Resources.MSG_ReelTypeLEDsSelected, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                lvLEDs.SelectedItems.Clear()
            End If
        End If
    End Sub

    Private Sub ImportReels_Click(sender As System.Object, e As System.EventArgs) Handles btnImportReels.Click
        AddReelOrLEDSet(eImageSetType.ReelImages)
    End Sub
    Private Sub ImportCreditReels_Click(sender As System.Object, e As System.EventArgs) Handles btnImportCreditReels.Click
        AddCreditReel()
    End Sub
    Private Sub ImportLEDs_Click(sender As System.Object, e As System.EventArgs) Handles btnImportLEDs.Click
        AddReelOrLEDSet(eImageSetType.LEDImages)
    End Sub

    Private Function GetAllRenderedLEDs() As Generic.List(Of Control)
        Dim ret As Generic.List(Of Control) = New Generic.List(Of Control)
        For Each panel As Panel In GroupBoxRendered.Controls.OfType(Of Panel)()
            For Each cntrl As B2SRenderedLEDPanel In panel.Controls.OfType(Of B2SRenderedLEDPanel)()
                ret.Add(cntrl)
            Next
            For Each cntrl As B2SDream7LEDPanel In panel.Controls.OfType(Of B2SDream7LEDPanel)()
                ret.Add(cntrl)
            Next
        Next
        Return ret
    End Function
    Private Function GetSelectedRenderedLEDs() As Generic.List(Of Control)
        Dim ret As Generic.List(Of Control) = New Generic.List(Of Control)
        For Each panel As Panel In GroupBoxRendered.Controls.OfType(Of Panel)()
            For Each cntrl As B2SRenderedLEDPanel In panel.Controls.OfType(Of B2SRenderedLEDPanel)()
                If cntrl.Selected Then
                    ret.Add(cntrl)
                End If
            Next
            For Each cntrl As B2SDream7LEDPanel In panel.Controls.OfType(Of B2SDream7LEDPanel)()
                If cntrl.Selected Then
                    ret.Add(cntrl)
                End If
            Next
        Next
        Return ret
    End Function
    Private Function GetFirstSelectedReelType(ByVal reeltypes As String) As String
        Dim ret As String = String.Empty
        For Each reeltype As String In reeltypes.Split(",")
            If Not String.IsNullOrEmpty(reeltype) Then
                ret = reeltype
                Exit For
            End If
        Next
        Return ret
    End Function

    Private Sub AddReelOrLEDSet(ByVal type As eImageSetType)
        If MessageBox.Show(My.Resources.MSG_ReelImportStart, AppTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
            Dim images(9) As Image
            Using filedialog As OpenFileDialog = New OpenFileDialog
                With filedialog
                    .Filter = ImageFileExtensionFilter
                    Dim i As Integer = 0
                    Do While i <= 9
                        Dim ret As DialogResult = .ShowDialog(Me)
                        If ret = Windows.Forms.DialogResult.OK Then
                            Try
                                images(i) = New Bitmap(.FileName)
                            Catch
                                MessageBox.Show(My.Resources.MSG_CannotLoadPic, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                If images(i) IsNot Nothing Then i += 1
                            End Try
                            'If i = 10 Then
                            '    MessageBox.Show(My.Resources.MSG_ReelImportAt10, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'End If
                        Else
                            If MessageBox.Show(String.Format(My.Resources.MSG_ReelStopImagesImport, i), AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                Exit Do
                            End If
                        End If
                    Loop
                End With
            End Using
            ' is the import okay
            If images(9) IsNot Nothing Then
                MessageBox.Show(My.Resources.MSG_ReelImagesImportIsOkay, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                GeneralData.currentData.AddImageSet(images, type)
                If type = eImageSetType.LEDImages Then
                    FillReelListView(type, ilLEDs, lvLEDs, DefaultLEDs, True)
                Else
                    FillReelListView(type, ilReels, lvEMReels, DefaultEMReels, True)
                End If
            End If
        End If
    End Sub
    Private Sub AddCreditReel()
        If MessageBox.Show(My.Resources.MSG_CreditReelImportStart, AppTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
            Dim images() As Image = Nothing
            Dim save As Boolean = True
            Using filedialog As OpenFileDialog = New OpenFileDialog
                With filedialog
                    .Filter = ImageFileExtensionFilter
                    Dim i As Integer = 0
                    Do While True
                        Dim ret As DialogResult = .ShowDialog(Me)
                        If ret = Windows.Forms.DialogResult.OK Then
                            Try
                                'If images Is Nothing Then
                                '    ReDim images(0)
                                'Else
                                ReDim Preserve images(i)
                                'End If
                                images(i) = New Bitmap(.FileName)
                            Catch
                                MessageBox.Show(My.Resources.MSG_CannotLoadPic, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                If images(i) IsNot Nothing Then i += 1
                            End Try
                        Else
                            If MessageBox.Show(String.Format(My.Resources.MSG_CreditReelStopImagesImport, i), AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                save = (MessageBox.Show(My.Resources.MSG_CreditReelSaveImagesImport, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes)
                                Exit Do
                            End If
                        End If
                    Loop
                End With
            End Using
            ' is the import okay
            If save Then
                MessageBox.Show(My.Resources.MSG_CreditReelImagesImportIsOkay, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                GeneralData.currentData.AddImageSet(images, eImageSetType.CreditReelImages)
                FillReelListView(eImageSetType.CreditReelImages, ilEMCeditReels, lvEMCreditReels, DefaultEMCreditReels, True)
            End If
        End If
    End Sub

End Class