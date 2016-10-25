Imports System.Drawing.Drawing2D

Public Class frmBetterLed

    Private Sub BetterLed_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.DarkGray

        Me.SegmentDisplay1.Text = TextBox1.Text
        Me.SegmentDisplay1.SetValue(2, "x.")
        Me.SegmentDisplay1.SetValue(0, 2312323)

        For Each led As Dream7Display.Dream7Display In {SegmentDisplay1, SegmentDisplay2}
            led.OffColor = Color.FromArgb(50, 50, 50)
            led.LightColor = Color.FromArgb(255, 120, 90)
            led.GlassColor = Color.FromArgb(254, 50, 25)
            led.GlassColorCenter = Color.FromArgb(200, 200, 120)
            led.GlassAlpha = 120
            led.GlassAlphaCenter = 230
            led.Glow = 10
            led.Type = Dream7Display.SegmentNumberType.FourteenSegment
            led.Shear = 0
            led.Thickness = 16
        Next
        'Me.SegmentDisplay1.LightColor = Color.FromArgb(196, 220, 255)
        ''cntrl.Mirrored = True
        'Me.SegmentDisplay1.SetExtraSpacing(3, 200)
        'Me.SegmentDisplay1.SetExtraSpacing(14, 200)
        'Me.SegmentDisplay1.GlassColor = Color.FromArgb(96, 128, 255)
        'Me.SegmentDisplay1.GlassAlphaCenter = 200
        'Me.SegmentDisplay1.GlassAlpha = 150
        'Me.SegmentDisplay1.Glow = 6

        'Dim Path As New System.Drawing.Drawing2D.GraphicsPath
        'Dim PointsA() As Point = {New Point(0, 0), New Point(40, 60), New Point(Me.Width - 100, 10)}

        'Path.AddCurve(PointsA)
        'Dim PointsB() As Point = {New Point(Me.Width - 40, Me.Height - 60), _
        '                          New Point(Me.Width, Me.Height), New Point(10, Me.Height)}
        'Path.AddCurve(PointsB)
        'Path.CloseAllFigures()
        'Me.Region = New Region(Path)

    End Sub




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SegmentDisplay1.LightColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SegmentDisplay1.GlassColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Me.SegmentDisplay1.Text = TextBox1.Text
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        SegmentDisplay1.Shear = CSng(TrackBar1.Value) / 100.0
        SegmentDisplay3.Shear = CSng(TrackBar1.Value) / 100.0
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        SegmentDisplay1.ScaleFactor = CSng(TrackBar2.Value) / 100.0
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        SegmentDisplay1.Type = Val(ComboBox1.SelectedItem)
        SegmentDisplay2.Type = Val(ComboBox1.SelectedItem)
    End Sub

    Private Sub TrackBar3_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.Scroll
        SegmentDisplay1.Spacing = TrackBar3.Value
    End Sub

    Private Sub TrackBar4_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar4.Scroll
        SegmentDisplay1.GlassAlpha = TrackBar4.Value
    End Sub

    Private Sub TrackBar5_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar5.Scroll
        SegmentDisplay1.Glow = TrackBar5.Value
        SegmentDisplay2.Glow = TrackBar5.Value
    End Sub

    Private Sub TrackBar6_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar6.Scroll
        SegmentDisplay1.GlassAlphaCenter = TrackBar6.Value
    End Sub

    Private Sub TrackBar7_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar7.Scroll
        SegmentDisplay1.Angle = TrackBar7.Value
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        SegmentDisplay1.ScaleMode = If(ComboBox2.Text = "Manual", 0, If(ComboBox2.Text = "Zoom", 2, 1))
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Dim sText As String = TextBox2.Text
        If sText.Length > 10 Then sText = sText.Substring(0, 10)
        SegmentDisplay1.SetValue(0, CLng(Val(sText)))
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        SegmentDisplay1.Mirrored = CheckBox1.Checked
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Timer1.Enabled Then
            Timer1.Stop()
            Timer2.Stop()
        Else
            Dream7Display.Dream7Display.SegmentPaintCounter = 0
            Timer2.Interval = 1000
            Timer2.Start()
            Timer1.Interval = 20
            Timer1.Start()
        End If
    End Sub

    Dim bOn As Boolean
    Private nLastCounter As Long = 0
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Me.SegmentDisplay1.Text = Microsoft.VisualBasic.Right(StrDup(5, " ") & oStop.ElapsedTicks.ToString, 5)
        Timer1.Stop()
        If bOn Then
            Dim nSegment As Integer = 1
            For Each nNumber As Integer In {255, 123, 342, 45, 12, 8, 22, 55, 454, 89, 45, 44}
                Me.SegmentDisplay1.SetValue(nSegment, nNumber)
                Me.SegmentDisplay2.SetValue(nSegment, nNumber)
                nSegment += 1
            Next
        Else
            For nSegment As Integer = 0 To 14
                Me.SegmentDisplay1.SetValue(nSegment, 0)
                Me.SegmentDisplay2.SetValue(nSegment, 0)
            Next

        End If
        bOn = Not bOn
        'If Me.SegmentDisplay1.Text = " 80" Then
        '    Me.SegmentDisplay1.Text = "      "
        'Else
        '    Me.SegmentDisplay1.Text = " 80"
        'End If

        Me.SegmentDisplay1.Update()
        Timer1.Start()
        ' show performance 
        Me.TextBox3.Text = Dream7Display.Dream7Display.SegmentPaintCounter

    End Sub

    Private Sub TrackBar8_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar8.Scroll
        Me.SegmentDisplay1.Thickness = TrackBar8.Value
        Me.SegmentDisplay2.Thickness = TrackBar8.Value
        Me.SegmentDisplay3.Thickness = TrackBar8.Value
        Me.TextBox3.Text = TrackBar8.Value
    End Sub
    'Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    '    Dim oClip As New Region(e.ClipRectangle)
    '    For Each oControl As Control In Me.Controls
    '        oClip.Exclude(oControl.Bounds)
    '    Next
    '    e.Graphics.SetClip(oClip, CombineMode.Replace)
    '    e.Graphics.DrawImage(My.Resources.Back, Me.ClientRectangle)
    'End Sub
    'Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
    '    'MyBase.OnPaintBackground(e)
    'End Sub


    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            Me.SegmentDisplay1.BulbSize = New SizeF(0.1, 0.3)
        Else
            Me.SegmentDisplay1.BulbSize = SizeF.Empty
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Me.SegmentDisplay1.WireFrame = CheckBox3.Checked
        Me.SegmentDisplay2.WireFrame = CheckBox3.Checked
        Me.SegmentDisplay3.WireFrame = CheckBox3.Checked
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.TextBox4.Text = CDbl(Dream7Display.Dream7Display.SegmentPaintCounter - nLastCounter) & " / Sek"
        nLastCounter = Dream7Display.Dream7Display.SegmentPaintCounter
    End Sub
End Class