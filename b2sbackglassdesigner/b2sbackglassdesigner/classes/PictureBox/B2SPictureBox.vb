Imports System
Imports System.Drawing.Imaging

Public Class B2SPictureBox

    Inherits PictureBox

    Private helper As HelperBase = New HelperBase()

    Public Event MyMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MyMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MyMouseMove(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event CopyDMDCopyArea(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectedItemClicked(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event SelectedItemMoving(ByVal sender As Object, ByVal e As Mouse.MouseMoveEventArgs)
    Public Event SelectedBulbMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event SelectedBulbEdited(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectedItemRemoved(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event LightsReportProgress(ByVal sender As Object, ByVal e As Illumination.Lights.LightsProgressEventArgs)

    Public WithEvents Mouse As Mouse = Nothing
    Public WithEvents Lights As Illumination.Lights = Nothing

    Private currentMouseLocation As Point = Nothing

    Private IsExternalIlluminationImageFramed As Boolean = False
    Private IsExternalIlluminationImageIlluminated As Boolean = False

    Protected Overrides Sub OnPaint(pe As System.Windows.Forms.PaintEventArgs)

        MyBase.OnPaint(pe)

        ' draw the overlay illumination picture
        pe.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        If Lights IsNot Nothing Then
            If currentAnimationSteps IsNot Nothing Then
                If Lights.Images IsNot Nothing Then
                    Dim x As Single = Me.Image.Width / Me.Width
                    Dim y As Single = Me.Image.Height / Me.Height
                    For Each light As KeyValuePair(Of Integer, Illumination.Lights.ImageInfo) In Lights.Images
                        With light.Value
                            If animationOn.Count > 0 AndAlso Not String.IsNullOrEmpty(.Name) AndAlso animationOn.Contains(.Name.ToLower) Then
                                Dim rectF As RectangleF = New RectangleF(.Rectangle.X / x, .Rectangle.Y / y, .Rectangle.Width / x, .Rectangle.Height / y)
                                pe.Graphics.DrawImage(.Image, rectF)
                            End If
                        End With
                    Next
                End If
            ElseIf ShowIllumination Then
                If IsExternalIlluminationImageIlluminated Then
                    ' nothing to do
                ElseIf Lights.Image IsNot Nothing Then
                    pe.Graphics.DrawImage(Lights.Image, New Rectangle(0, 0, Me.Width, Me.Height))
                ElseIf Lights.Images IsNot Nothing Then
                    Dim x As Single = Me.Image.Width / Me.Width
                    Dim y As Single = Me.Image.Height / Me.Height
                    For Each light As KeyValuePair(Of Integer, Illumination.Lights.ImageInfo) In Lights.Images
                        With light.Value
                            If .Image IsNot Nothing Then
                                Dim rectF As RectangleF = New RectangleF(.Rectangle.X / x, .Rectangle.Y / y, .Rectangle.Width / x, .Rectangle.Height / y)
                                pe.Graphics.DrawImage(.Image, rectF)
                            End If
                        End With
                    Next
                End If
            End If
        End If

        ' show dmd cut image frame
        If CopyDMDImageFromBackglass AndAlso Mouse IsNot Nothing Then
            Dim framecolor As Color = Color.Yellow
            Dim pen As Pen = New Pen(framecolor)
            Dim pendashed As Pen = New Pen(Brushes.Black)
            pendashed.DashPattern = New Single() {2.0F, 20.0F}
            Dim factor As Double = Mouse.factor
            If Backglass.currentData.DMDCopyArea.Location <> Nothing AndAlso Backglass.currentData.DMDCopyArea.Size <> Nothing Then
                ' draw marker frame
                Dim rect As Rectangle = New Rectangle(New Point(CInt(Backglass.currentData.DMDCopyArea.Location.X * factor), CInt(Backglass.currentData.DMDCopyArea.Location.Y * factor)),
                                                      New Size(CInt(Backglass.currentData.DMDCopyArea.Size.Width * factor), CInt(Backglass.currentData.DMDCopyArea.Size.Height * factor)))
                pe.Graphics.DrawRectangle(pen, rect)
                pe.Graphics.DrawRectangle(pendashed, rect)
                ' draw camera image
                Dim size As Size = New Size(16 * factor, 16 * factor)
                Dim loc As Point = New Point((Backglass.currentData.DMDCopyArea.Location.X + Backglass.currentData.DMDCopyArea.Size.Width - 3) * factor - size.Width,
                                             (Backglass.currentData.DMDCopyArea.Location.Y + 1) * factor)
                pe.Graphics.DrawImage(My.Resources.Camera32x32, New Rectangle(loc, size))
            End If
            pendashed.Dispose()
            pen.Dispose()
        End If

        ' show illumination frames
        If ShowIlluFrames AndAlso Mouse IsNot Nothing AndAlso Backglass.currentBulbs IsNot Nothing AndAlso Backglass.currentBulbs.Count > 0 Then
            Dim framecolor As Color = Color.White
            Dim pen As Pen = New Pen(framecolor)
            Dim pendashed As Pen = New Pen(Brushes.Black)
            pendashed.DashPattern = New Single() {2.0F, 20.0F}
            Dim factor As Double = Mouse.factor
            Dim bulbloop As Integer = 1
            Do While True
                For index As Integer = Backglass.currentBulbs.Count - 1 To 0 Step -1
                    With Backglass.currentBulbs(index)
                        If String.IsNullOrEmpty(RomInfoFilter) OrElse
                            RomInfoFilter.Equals(.B2SInfo2String) OrElse
                            RomInfoFilter.Equals(.RomInfo2String) OrElse
                            (RomInfoFilter.Equals("withoutid") AndAlso ((Backglass.currentData.CommType = eCommType.B2S AndAlso String.IsNullOrEmpty(.B2SInfo2String)) OrElse (Backglass.currentData.CommType = eCommType.Rom AndAlso String.IsNullOrEmpty(.RomInfo2String)))) OrElse
                            (RomInfoFilter.Equals("withname") AndAlso Not String.IsNullOrEmpty(.Name)) OrElse
                            (RomInfoFilter.Equals("off") AndAlso .InitialState = 0) OrElse
                            (RomInfoFilter.Equals("on") AndAlso .InitialState = 1) OrElse
                            (RomInfoFilter.Equals("alwayson") AndAlso .InitialState = 2) OrElse
                            (RomInfoFilter.Equals("authentic") AndAlso .DualMode <> eDualMode.Fantasy) OrElse
                            (RomInfoFilter.Equals("fantasy") AndAlso .DualMode <> eDualMode.Authentic) Then
                            ' maybe draw image
                            If bulbloop = 1 Then
                                If .IsImageSnippit AndAlso .Image IsNot Nothing Then
                                    'Dim cm As ColorMatrix = New ColorMatrix()
                                    'Dim ia As ImageAttributes = New ImageAttributes()
                                    'cm.Matrix33 = 0.1
                                    'ia.SetColorMatrix(cm)
                                    'pe.Graphics.DrawImage(.Image, New Rectangle(.Location.X * factor, .Location.Y * factor, .Image.Width * factor, .Image.Height * factor), 0, 0, .Image.Width, .Image.Height, GraphicsUnit.Pixel, ia)
                                    'ia.Dispose()
                                    pe.Graphics.DrawImage(.Image, New Rectangle(.Location.X * factor, .Location.Y * factor, .Image.Width * factor, .Image.Height * factor))
                                End If
                            Else
                                ' draw marker frame
                                Dim rect As Rectangle = New Rectangle(CInt(.Location.X * factor), CInt(.Location.Y * factor), CInt(.Size.Width * factor), CInt(.Size.Height * factor))
                                pe.Graphics.DrawRectangle(pen, rect)
                                Dim x As Integer = CInt(rect.X + rect.Width / 2)
                                Dim y As Integer = CInt(rect.Y + rect.Height / 2)
                                If .Equals(Mouse.SelectedBulb) Then
                                    pe.Graphics.DrawRectangle(pen, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2)
                                    pe.Graphics.DrawRectangle(pen, rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4)
                                    pe.Graphics.DrawLine(pen, x - 3, y, x + 3, y)
                                    pe.Graphics.DrawLine(pen, x, y - 3, x, y + 3)
                                Else
                                    pe.Graphics.DrawRectangle(pendashed, rect)
                                End If
                                ' maybe draw preview frame
                                If .Equals(Mouse.PreviewedBulb) Then
                                    pe.Graphics.DrawRectangle(pen, rect)
                                    pe.Graphics.DrawRectangle(pen, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2)
                                End If
                                ' draw remove X
                                Dim isSmallRect As Boolean = (rect.Width * factor < 25 OrElse rect.Height * factor < 25)
                                If .Equals(Mouse.SelectedBulb) Then
                                    If Not isSmallRect Then
                                        pe.Graphics.FillRectangle(New SolidBrush(framecolor), New Rectangle(rect.X + rect.Width - If(isSmallRect, 11, 15), rect.Y + 5, If(isSmallRect, 6, 10), If(isSmallRect, 6, 10)))
                                        pe.Graphics.DrawLine(Pens.Black, rect.X + rect.Width - If(isSmallRect, 9, 13), rect.Y + 7, rect.X + rect.Width - 7, rect.Y + If(isSmallRect, 9, 13))
                                        pe.Graphics.DrawLine(Pens.Black, rect.X + rect.Width - 7, rect.Y + 7, rect.X + rect.Width - If(isSmallRect, 9, 13), rect.Y + If(isSmallRect, 9, 13))
                                    End If
                                End If
                                ' maybe draw bulb name
                                Dim font As Font = New Font("Tahoma", 7, If(.Equals(Mouse.SelectedBulb), FontStyle.Bold, FontStyle.Regular))
                                If Not String.IsNullOrEmpty(.Name) Then
                                    TextRenderer.DrawText(pe.Graphics, .Name, font, New Point(rect.X + 3, rect.Y + 4), framecolor, TextFormatFlags.Left Or TextFormatFlags.NoPrefix)
                                End If
                                ' maybe draw lamp state text
                                If .InitialState >= 0 Then
                                    TextRenderer.DrawText(pe.Graphics, Choose(.InitialState + 1, "Off", "On", "Always on", "At startup on") & If(Backglass.currentData.DualBackglass, "/" + Choose(.DualMode + 1, "B", "A", "F"), ""), font, New Rectangle(rect.X + 3, rect.Y + rect.Height - 14, 80, 15), framecolor, TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or TextFormatFlags.NoPrefix)
                                End If
                                ' maybe draw lamp or solenoid info text
                                If .B2SID > 0 Then
                                    Dim toleft As Integer = If(.Equals(Mouse.SelectedBulb), If(isSmallRect, -1, 13), 0)
                                    TextRenderer.DrawText(pe.Graphics, .B2SID.ToString() & If(.B2SValue > 0, "/" & .B2SValue.ToString(), ""), font, New Rectangle(rect.X + rect.Width - 50, rect.Y + 3, 47 - toleft, 15), framecolor, TextFormatFlags.VerticalCenter Or TextFormatFlags.Right Or TextFormatFlags.NoPrefix)
                                End If
                                If .RomID > 0 AndAlso .RomIDType > eRomIDType.NotUsed Then
                                    Dim text As String = If(.RomInverted, "I", String.Empty) & Choose(.RomIDType, "L", "S", "GI") & .RomID.ToString()
                                    '    Dim toleft As Integer = If(.Equals(Mouse.SelectedBulb), If((rect.Width < 30 OrElse rect.Height < 30), 9, 13), 0)
                                    TextRenderer.DrawText(pe.Graphics, text, font, New Rectangle(rect.X + rect.Width - 50, rect.Y + rect.Height - 14, 47, 15), framecolor, TextFormatFlags.VerticalCenter Or TextFormatFlags.Right Or TextFormatFlags.NoPrefix)
                                End If
                                font.Dispose()
                                ' maybe render text
                                If Not ShowIllumination AndAlso Not String.IsNullOrEmpty(.Text) Then
                                    font = New Font(.FontName, CSng(.FontSize * factor), .FontStyle)
                                    Dim horizontal As TextFormatFlags = If(.TextAlignment = Illumination.eTextAlignment.Left, TextFormatFlags.Left, If(.TextAlignment = Illumination.eTextAlignment.Right, TextFormatFlags.Right, TextFormatFlags.HorizontalCenter))
                                    TextRenderer.DrawText(pe.Graphics, .Text, font, rect, framecolor, TextFormatFlags.VerticalCenter Or horizontal Or TextFormatFlags.NoPrefix)
                                    font.Dispose()
                                End If
                            End If
                        End If
                    End With
                Next
                bulbloop += 1
                If bulbloop > 2 Then Exit Do
            Loop
            pendashed.Dispose()
            pen.Dispose()
        End If

        ' show score frames
        If (ShowScoreFrames OrElse ShowScoring) AndAlso Mouse IsNot Nothing AndAlso Backglass.currentScores IsNot Nothing AndAlso Backglass.currentScores.Count > 0 Then
            Dim fontbold As Font = New Font("Tahoma", 8, FontStyle.Bold) ' Segoe UI
            Dim font As Font = New Font("Tahoma", 7, FontStyle.Regular)
            Dim framecolor As Color = Color.DarkOrange
            Dim pen As Pen = New Pen(framecolor)
            Dim pendashed As Pen = New Pen(Brushes.White)
            pendashed.DashPattern = New Single() {5.0F, 5.0F}
            'Dim brush As SolidBrush = New SolidBrush(framecolor)
            Dim factor As Double = Mouse.factor
            For index As Integer = Backglass.currentScores.Count - 1 To 0 Step -1
                With Backglass.currentScores(index)
                    Dim rect As Rectangle = New Rectangle(CInt(.Location.X * factor), CInt(.Location.Y * factor), CInt(.Size.Width * factor), CInt(.Size.Height * factor))
                    ' draw images for score reels
                    If .Digits > 0 Then
                        If .IsSingleReelSizeDirty OrElse .SingleReelFactor <> factor Then
                            Dim width As Single = ((rect.Width - .Spacing * factor / 2 * (.Digits - 1)) / .Digits)
                            Do While width * .Digits > rect.Width - 1
                                width -= 1
                            Loop
                            Dim height As Integer = rect.Height - 1
                            If .PerfectScaleWidthFix Then
                                height = CInt(My.Resources.LED_0.Height / My.Resources.LED_0.Width * width)
                                .Size.Height = CInt(height / factor) + 1
                                .PerfectScaleWidthFix = False
                                Me.Invalidate()
                            End If
                            .SingleReelSize = New SizeF(width, height)
                            .IsSingleReelSizeDirty = False
                            .SingleReelFactor = factor
                        End If
                        Dim x As Single = rect.X + 1
                        Dim y As Integer = rect.Y + 1
                        Dim newsize As Size = New Size(.SingleReelSize.Width, .SingleReelSize.Height)
                        Dim image As Image = GetReelImage(.ReelType, .ReelColor, Backglass.currentData.UseDream7LEDs, Backglass.currentData.D7Thickness, Backglass.currentData.D7Shear, Backglass.currentData.D7Glow, newsize).Resized(newsize)
                        If image IsNot Nothing Then
                            For i As Integer = 1 To .Digits
                                pe.Graphics.DrawImage(image, New Point(x, y))
                                x += .SingleReelSize.Width + .Spacing * factor / 2
                            Next
                            image.Dispose()
                        End If
                    End If
                    ' draw marker frame
                    If ShowScoreFrames Then
                        pe.Graphics.DrawRectangle(pen, rect)
                        If .Equals(Mouse.SelectedScore) Then
                            pe.Graphics.DrawRectangle(pen, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2)
                            pe.Graphics.DrawRectangle(pen, rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4)
                        Else
                            pe.Graphics.DrawRectangle(pendashed, rect)
                        End If
                    End If
                    ' maybe draw preview frame
                    If .Equals(Mouse.PreviewedScore) Then
                        pe.Graphics.DrawRectangle(pen, rect)
                        pe.Graphics.DrawRectangle(pen, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2)
                    End If
                    ' draw remove X
                    If ShowScoreFrames AndAlso .Equals(Mouse.SelectedScore) Then
                        Dim isSmallRect As Boolean = (rect.Width * factor < 25 OrElse rect.Height * factor < 25)
                        If Not isSmallRect Then
                            pe.Graphics.FillRectangle(New SolidBrush(framecolor), New Rectangle(rect.X + rect.Width - 15, rect.Y + 5, 10, 10))
                            pe.Graphics.DrawLine(Pens.Black, rect.X + rect.Width - 13, rect.Y + 7, rect.X + rect.Width - 7, rect.Y + 13)
                            pe.Graphics.DrawLine(Pens.Black, rect.X + rect.Width - 7, rect.Y + 7, rect.X + rect.Width - 13, rect.Y + 13)
                        End If
                    End If
                    ' maybe draw start digit
                    If .ReelIlluB2SID > 0 Then
                        TextRenderer.DrawText(pe.Graphics, .ReelIlluB2SID.ToString() & If(.ReelIlluB2SValue > 0, "/" & .ReelIlluB2SValue.ToString(), ""), font, New Rectangle(rect.X + 3, rect.Y + rect.Height - 15, 80, 15), framecolor, Color.White, TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or TextFormatFlags.NoPrefix)
                    End If
                    ' maybe draw reel illumination info
                    If .B2SStartDigit > 0 Then
                        Dim toleft As Integer = If(.Equals(Mouse.SelectedScore), 13, 0) 'If(.Equals(Mouse.SelectedBulb), If(isSmallRect, -1, 13), 0)
                        TextRenderer.DrawText(pe.Graphics, If(.B2SPlayerNo <> eB2SPlayerNo.NotUsed, "P" & CInt(.B2SPlayerNo).ToString() & "/", "") & .B2SStartDigit.ToString(), font, New Rectangle(rect.X + rect.Width - 50, rect.Y + 3, 47 - toleft, 15), framecolor, Color.White, TextFormatFlags.VerticalCenter Or TextFormatFlags.Right Or TextFormatFlags.NoPrefix)
                    End If
                    ' draw player number
                    If ShowScoreFrames Then
                        TextRenderer.DrawText(pe.Graphics, .ID.ToString(), fontbold, New Point(rect.X + 3, rect.Y + 3), framecolor, Color.White)
                        'pe.Graphics.DrawString(.ID.ToString(), fontbold, brush, New Point(rect.X + 3, rect.Y + 3))
                    End If
                End With
            Next
            'brush.Dispose()
            pendashed.Dispose()
            pen.Dispose()
            font.Dispose()
            fontbold.Dispose()
        End If

        ' maybe show grill height marker
        If (SetGrillHeight OrElse SetSmallGrillHeight) AndAlso Mouse IsNot Nothing AndAlso Backglass.currentData IsNot Nothing AndAlso Not Backglass.currentData.IsDMDImageShown AndAlso currentMouseLocation <> Nothing Then
            Dim font As Font = New Font("Tahoma", 7, FontStyle.Bold)
            If Backglass.currentData.GrillHeight > 0 Then
                pe.Graphics.FillRectangle(Brushes.ForestGreen, New Rectangle(0, (Backglass.currentData.Image.Height - Backglass.currentData.GrillHeight) * Mouse.factor - 2, Me.Width - 1, 3))
                Dim size As Size = TextRenderer.MeasureText(My.Resources.TXT_GrillTop, font)
                Dim y As Integer = (Backglass.currentData.Image.Height - Backglass.currentData.GrillHeight) * Mouse.factor - size.Height - 4
                TextRenderer.DrawText(pe.Graphics, My.Resources.TXT_GrillTop, font, New Point(Me.Width - size.Width - 3 - 15, y), Color.ForestGreen)
                ' draw remove X
                pe.Graphics.FillRectangle(Brushes.ForestGreen, New Rectangle(Me.Width - 15, y, 12, 12))
                pe.Graphics.DrawLine(Pens.White, Me.Width - 13, y + 2, Me.Width - 5, y + 10)
                pe.Graphics.DrawLine(Pens.White, Me.Width - 13, y + 10, Me.Width - 5, y + 2)
            End If
            If Backglass.currentData.SmallGrillHeight > 0 Then
                pe.Graphics.FillRectangle(Brushes.DarkRed, New Rectangle(0, (Backglass.currentData.Image.Height - Backglass.currentData.SmallGrillHeight) * Mouse.factor - 2, Me.Width - 1, 3))
                Dim size As Size = TextRenderer.MeasureText(My.Resources.TXT_MiniGrillTop, font)
                Dim y As Integer = (Backglass.currentData.Image.Height - Backglass.currentData.SmallGrillHeight) * Mouse.factor - size.Height - 4
                TextRenderer.DrawText(pe.Graphics, My.Resources.TXT_MiniGrillTop, font, New Point(Me.Width - size.Width - 3 - 15, y), Color.DarkRed)
                ' draw remove X
                pe.Graphics.FillRectangle(Brushes.DarkRed, New Rectangle(Me.Width - 15, y, 12, 12))
                pe.Graphics.DrawLine(Pens.White, Me.Width - 13, y + 2, Me.Width - 5, y + 10)
                pe.Graphics.DrawLine(Pens.White, Me.Width - 13, y + 10, Me.Width - 5, y + 2)
            End If
            pe.Graphics.FillRectangle(If(SetSmallGrillHeight, Brushes.OrangeRed, Brushes.LightGreen), New Rectangle(0, currentMouseLocation.Y * Mouse.factor - 2, Me.Width - 1, 3))
            font.Dispose()
        End If

        ' maybe show dmd for default location
        If SetDMDDefaultLocation AndAlso Mouse IsNot Nothing AndAlso Backglass.currentData IsNot Nothing AndAlso Not Backglass.currentData.IsDMDImageShown AndAlso currentMouseLocation <> Nothing Then
            If Backglass.currentData.DMDImage IsNot Nothing Then
                Dim dmdsize As Size = New Size(Backglass.currentData.DMDImage.Width * Mouse.factor, Backglass.currentData.DMDImage.Height * Mouse.factor)
                If Backglass.currentData.DMDDefaultLocation <> Nothing Then
                    Dim dmdloc As Point = New Point(Backglass.currentData.DMDDefaultLocation.X * Mouse.factor, Backglass.currentData.DMDDefaultLocation.Y * Mouse.factor)
                    pe.Graphics.FillRectangle(Brushes.ForestGreen, New Rectangle(dmdloc, dmdsize))
                    ' draw remove X
                    pe.Graphics.FillRectangle(Brushes.ForestGreen, New Rectangle(dmdloc.X + dmdsize.Width + 5, dmdloc.Y, 12, 12))
                    pe.Graphics.DrawLine(Pens.White, dmdloc.X + dmdsize.Width + 5 + 2, dmdloc.Y + 2, dmdloc.X + dmdsize.Width + 5 + 10, dmdloc.Y + 10)
                    pe.Graphics.DrawLine(Pens.White, dmdloc.X + dmdsize.Width + 5 + 2, dmdloc.Y + 10, dmdloc.X + dmdsize.Width + 5 + 10, dmdloc.Y + 2)
                End If
                pe.Graphics.FillRectangle(Brushes.DarkRed, New Rectangle(New Point(currentMouseLocation.X * Mouse.factor, currentMouseLocation.Y * Mouse.factor), dmdsize))
            End If
        End If

        'If True Then
        '    Dim pendashed As Pen = New Pen(Brushes.Gray)
        '    pendashed.DashPattern = New Single() {1.0F, 2.0F}
        '    For i As Integer = 1 To 300
        '        Dim y As Single = i * Mouse.factor
        '        pe.Graphics.DrawLine(pendashed, 0, y, Me.Width - 1, y)
        '    Next
        '    For i As Integer = 1 To 300
        '        Dim x As Single = i * Mouse.factor
        '        pe.Graphics.DrawLine(pendashed, x, 0, x, Me.Height - 1)
        '    Next
        '    pendashed.Dispose()
        'End If

    End Sub
    Protected Overrides Sub OnPaintBackground(pevent As System.Windows.Forms.PaintEventArgs)
        ' nothing to do
    End Sub

    Private Sub Lights_ReportProgress(sender As Object, e As Illumination.Lights.LightsProgressEventArgs) Handles Lights.ReportProgress
        RaiseEvent LightsReportProgress(sender, e)
    End Sub

    Private Sub Mouse_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Mouse.MouseDown
        ' raise event
        RaiseEvent MyMouseDown(sender, e)
    End Sub
    Private Sub Mouse_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Mouse.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If SetGrillHeight AndAlso currentMouseLocation <> Nothing AndAlso Not Mouse.IsMouseOverGrillRemover Then
                Dim grillheight As Integer = (currentMouseLocation.Y * -1) + Backglass.currentData.Image.Height
                Backglass.currentData.GrillHeight = grillheight
                MessageBox.Show(String.Format(My.Resources.TXT_GrillHeight, grillheight), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                DirectCast(Parent, B2STabPage).SetGrillHeight = False
            ElseIf SetSmallGrillHeight AndAlso currentMouseLocation <> Nothing AndAlso Not Mouse.IsMouseOverSmallGrillRemover Then
                Dim smallgrillheight As Integer = (currentMouseLocation.Y * -1) + Backglass.currentData.Image.Height
                Backglass.currentData.SmallGrillHeight = smallgrillheight
                MessageBox.Show(String.Format(My.Resources.TXT_MiniGrillHeight, smallgrillheight), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                DirectCast(Parent, B2STabPage).SetSmallGrillHeight = False
            ElseIf SetDMDDefaultLocation AndAlso currentMouseLocation <> Nothing AndAlso Not Mouse.IsMouseOverDMDLocationRemover Then
                Backglass.currentData.DMDDefaultLocation = currentMouseLocation
                MessageBox.Show(String.Format(My.Resources.TXT_DMDDefaultLocation, currentMouseLocation.X, currentMouseLocation.Y), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                DirectCast(Parent, B2STabPage).SetDMDDefaultLocation = False
            ElseIf SetGrillHeight AndAlso Mouse.IsMouseOverGrillRemover Then
                Backglass.currentData.GrillHeight = 0
                MessageBox.Show(My.Resources.TXT_GrillRemoved, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                DirectCast(Parent, B2STabPage).SetGrillHeight = False
            ElseIf SetSmallGrillHeight AndAlso Mouse.IsMouseOverSmallGrillRemover Then
                Backglass.currentData.SmallGrillHeight = 0
                MessageBox.Show(My.Resources.TXT_MiniGrillRemoved, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                DirectCast(Parent, B2STabPage).SetSmallGrillHeight = False
            ElseIf SetDMDDefaultLocation AndAlso Mouse.IsMouseOverDMDLocationRemover Then
                Backglass.currentData.DMDDefaultLocation = Nothing
                MessageBox.Show(My.Resources.TXT_DMDDefaultLocationRemoved, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                DirectCast(Parent, B2STabPage).SetDMDDefaultLocation = False
            End If
            Me.Invalidate()
        End If
        ' raise event
        RaiseEvent MyMouseUp(sender, e)
    End Sub
    Private Sub Mouse_MouseMove(sender As Object, e As Mouse.MouseMoveEventArgs) Handles Mouse.MouseMove
        If SetGrillHeight OrElse SetSmallGrillHeight OrElse SetDMDDefaultLocation Then
            currentMouseLocation = e.Location
            Me.Invalidate()
        End If
        RaiseEvent MyMouseMove(sender, e)
    End Sub
    Private Sub Mouse_CopyDMDCopyArea(sender As Object, e As System.EventArgs) Handles Mouse.CopyDMDCopyArea
        DirectCast(Parent, B2STabPage).CopyDMDImageFromBackglass = False
        RaiseEvent CopyDMDCopyArea(sender, e)
    End Sub
    Private Sub Mouse_SelectedBulbMoved(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Mouse.SelectedBulbMoved
        RaiseEvent SelectedBulbMoved(sender, e)
    End Sub
    Private Sub Mouse_SelectedBulbEdited(sender As Object, e As System.EventArgs) Handles Mouse.SelectedBulbEdited
        RaiseEvent SelectedBulbEdited(sender, e)
    End Sub
    Private Sub Mouse_SelectedItemClicked(sender As Object, e As Mouse.MouseMoveEventArgs) Handles Mouse.SelectedItemClicked
        RaiseEvent SelectedItemClicked(sender, e)
    End Sub
    Private Sub Mouse_SelectedItemMoving(sender As Object, e As Mouse.MouseMoveEventArgs) Handles Mouse.SelectedItemMoving
        RaiseEvent SelectedItemMoving(sender, e)
    End Sub
    Private Sub Mouse_SelectedItemRemoved(sender As Object, e As System.EventArgs) Handles Mouse.SelectedItemRemoved
        Lights.ClearImages()
        RaiseEvent SelectedItemRemoved(sender, e)
    End Sub

    Public Sub New(ByVal _isDMD As Boolean)
        Me.IsDMDPictureBox = _isDMD
        Me.SizeMode = PictureBoxSizeMode.StretchImage
        Mouse = New Mouse(Me)
        Lights = New Illumination.Lights(Me, Me.IsDMDPictureBox)
    End Sub

    Public Shadows Sub Invalidate()
        ' recalc the factor
        If Backglass.currentData IsNot Nothing AndAlso Me.Image IsNot Nothing AndAlso Mouse IsNot Nothing Then
            Mouse.factor = Me.Width / Me.Image.Width 'Backglass.currentData.Image.Width
            Lights.factor = Mouse.factor
        End If
        ' the real invalidate
        MyBase.Invalidate()
    End Sub

    Private _SetGrillHeight As Boolean = False
    Public Property SetGrillHeight() As Boolean
        Get
            Return _SetGrillHeight
        End Get
        Set(ByVal value As Boolean)
            If _SetGrillHeight <> value Then
                _SetGrillHeight = value
                Mouse.SetGrillHeight = value
                Me.Invalidate()
            End If
        End Set
    End Property
    Private _SetSmallGrillHeight As Boolean = False
    Public Property SetSmallGrillHeight() As Boolean
        Get
            Return _SetSmallGrillHeight
        End Get
        Set(ByVal value As Boolean)
            If _SetSmallGrillHeight <> value Then
                _SetSmallGrillHeight = value
                Mouse.SetSmallGrillHeight = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property _CutDMDImageFromBackglass() As Boolean
    Public Property CopyDMDImageFromBackglass() As Boolean
        Get
            Return _CutDMDImageFromBackglass
        End Get
        Set(ByVal value As Boolean)
            If _CutDMDImageFromBackglass <> value Then
                _CutDMDImageFromBackglass = value
                Mouse.CopyDMDImageFromBackglass = value
                Me.Invalidate()
            End If
        End Set
    End Property
    Public Property _SetDMDDefaultLocation() As Boolean
    Public Property SetDMDDefaultLocation() As Boolean
        Get
            Return _SetDMDDefaultLocation
        End Get
        Set(ByVal value As Boolean)
            If _SetDMDDefaultLocation <> value Then
                _SetDMDDefaultLocation = value
                Mouse.SetDMDLocation = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowScoreFrames As Boolean = False
    Public Property ShowScoreFrames() As Boolean
        Get
            Return _ShowScoreFrames
        End Get
        Set(ByVal value As Boolean)
            If _ShowScoreFrames <> value Then
                _ShowScoreFrames = value
                Mouse.ShowScoreFrames = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowScoring As Boolean = False
    Public Property ShowScoring() As Boolean
        Get
            Return _ShowScoring
        End Get
        Set(ByVal value As Boolean)
            If _ShowScoring <> value Then
                _ShowScoring = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowIlluFrames As Boolean = False
    Public Property ShowIlluFrames() As Boolean
        Get
            Return _ShowIlluFrames
        End Get
        Set(ByVal value As Boolean)
            If _ShowIlluFrames <> value Then
                _ShowIlluFrames = value
                Mouse.ShowIlluFrames = value
                If (Backglass.currentData.IsExternalIlluminationImageSelected OrElse IsExternalIlluminationImageFramed) AndAlso Not IsExternalIlluminationImageIlluminated Then
                    If _ShowIlluFrames Then
                        IsExternalIlluminationImageFramed = True
                        MyBase.Image = GetFirstSelectedIlluImage()
                    Else
                        IsExternalIlluminationImageFramed = False
                        MyBase.Image = _image
                    End If
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowIllumination As Boolean = False
    Public Property ShowIllumination() As Boolean
        Get
            Return _ShowIllumination
        End Get
        Set(ByVal value As Boolean)
            If _ShowIllumination <> value Then
                _ShowIllumination = value
                If _ShowIllumination Then
                    If _image IsNot Nothing AndAlso Not IsDMDPictureBox AndAlso Backglass.currentData.IsExternalIlluminationImageSelected Then
                        IsExternalIlluminationImageIlluminated = True
                        Dim factor As Double = Mouse.factor
                        Dim illuimage As Image = GetFirstSelectedIlluImage()
                        Dim newimage As Image = _image.Clone
                        Using gr As Graphics = Graphics.FromImage(newimage)
                            For Each bulb As Illumination.BulbInfo In Backglass.currentBulbs
                                With bulb
                                    Dim rect As Rectangle = New Rectangle(bulb.LocationX, bulb.SizeX)
                                    Dim frameimage As Image = illuimage.PartFromImage(rect)
                                    gr.DrawImage(frameimage, rect)
                                End With
                            Next
                        End Using
                        MyBase.Image = newimage
                    Else
                        Dim image As Image = If(IsDMDPictureBox, Backglass.currentData.DMDImage, Backglass.currentData.Image)
                        If image IsNot Nothing Then
                            If _ShowIntensityIllumination Then
                                Lights.PrepareImage(image)
                                Lights.DrawImages(image, , , , RomInfoFilter)
                            Else
                                Lights.DrawImage(image, RomInfoFilter)
                            End If
                        End If
                    End If
                Else
                    If MyBase.Image IsNot Nothing AndAlso Not IsDMDPictureBox AndAlso IsExternalIlluminationImageIlluminated Then
                        IsExternalIlluminationImageIlluminated = False
                        MyBase.Image.Dispose()
                        If ShowIlluFrames AndAlso Backglass.currentData.IsExternalIlluminationImageSelected Then
                            IsExternalIlluminationImageFramed = True
                            MyBase.Image = GetFirstSelectedIlluImage()
                        Else
                            MyBase.Image = _image
                        End If
                    Else
                        'Lights.ClearImages()
                        Lights.ClearImage()
                    End If
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _ShowIntensityIllumination As Boolean
    Public Property ShowIntensityIllumination() As Boolean
        Get
            Return _ShowIntensityIllumination
        End Get
        Set(ByVal value As Boolean)
            _ShowIntensityIllumination = value
            ShowIllumination = value
        End Set
    End Property

    Public Sub ResetAnimationLights()
        Lights.ClearImages()
    End Sub
    Public Sub ShowAnimation(ByVal _animationname As String)
        If Lights.Images.Count = 0 Then
            Lights.DrawImages(Image)
        End If
        StartAnimation(_animationname)
    End Sub

    Private _image As Image = Nothing
    Public Shadows Property Image() As Image
        Get
            Return _image
        End Get
        Set(ByVal value As Image)
            MyBase.Image = value
            _image = value
            ' set size
            If value IsNot Nothing Then
                Me.Size = value.Size
            Else
                Me.Size = New Size(0, 0)
            End If
            ' maybe show picture box
            If Not Me.Visible Then Me.Visible = (value IsNot Nothing)
        End Set
    End Property

    Public Property IsDMDPictureBox() As Boolean = False

    Private _rominfofilter As String = String.Empty
    Public Property RomInfoFilter() As String
        Get
            Return _rominfofilter
        End Get
        Set(value As String)
            _rominfofilter = value
            Mouse.RomInfoFilter = value
            Lights.ClearImages()
            Me.Invalidate()
        End Set
    End Property

    'Public ReadOnly Property ImportedIlluminatedImage() As Image
    '    Get
    '        Return If(myIsExternalIlluminationImageSelected, MyBase.Image, Nothing)
    '    End Get
    'End Property

    Public ReadOnly Property OffImage() As Image
        Get
            Return GetFirstOffImage()
        End Get
    End Property

    Public ReadOnly Property OnImageRomID() As Integer
        Get
            Return GetOnImageRomID()
        End Get
    End Property
    Public ReadOnly Property OnImageRomIDType() As eRomIDType
        Get
            Return GetOnImageRomIDType()
        End Get
    End Property

    Public ReadOnly Property DarkImage() As Image
        Get
            Dim newimage As Bitmap = New Bitmap(MyBase.Image)
            Using gr As Graphics = Graphics.FromImage(newimage)
                For Each score As ReelAndLED.ScoreInfo In Backglass.currentScores
                    With score
                        If .Digits > 0 Then
                            Dim rect As Rectangle = New Rectangle(score.Location, score.Size)
                            Dim width As Single = ((rect.Width - .Spacing / 2 * (.Digits - 1)) / .Digits)
                            Do While width * .Digits > rect.Width - 1
                                width -= 1
                            Loop
                            Dim height As Integer = rect.Height - 1
                            Dim x As Single = rect.X + 1
                            Dim y As Integer = rect.Y + 1
                            Dim newsize As Size = New Size(width, height)
                            Dim image As Image = GetReelImage(.ReelType, Color.FromArgb(0, 0, 0), Backglass.currentData.UseDream7LEDs, Backglass.currentData.D7Thickness, Backglass.currentData.D7Shear, Backglass.currentData.D7Glow, newsize).Resized(newsize)
                            If image IsNot Nothing Then
                                For i As Integer = 1 To .Digits
                                    gr.DrawImage(image, New Point(x, y))
                                    x += width + .Spacing / 2
                                Next
                                image.Dispose()
                            End If
                        End If
                    End With
                Next
            End Using
            ' that's it
            Return newimage
        End Get
    End Property
    Public ReadOnly Property IlluminatedImage() As Image
        Get
            ' create the light image
            Lights.DrawImage(GetFirstOnImage())
            ' do the overlay of the images
            Dim newimage As Bitmap = New Bitmap(MyBase.Image)
            Dim lightimage As Bitmap = New Bitmap(Lights.Image)
            'lightimage.MakeTransparent(Color.White)
            Using gr As Graphics = Graphics.FromImage(newimage)
                gr.DrawImage(lightimage, 0, 0)
                For Each bulb As Illumination.BulbInfo In Backglass.currentBulbs
                    If bulb.IsImageSnippit AndAlso bulb.Image IsNot Nothing Then
                        gr.DrawImage(bulb.Image, New Rectangle(bulb.Location, bulb.Size))
                    End If
                Next
                For Each score As ReelAndLED.ScoreInfo In Backglass.currentScores
                    With score
                        If .Digits > 0 Then
                            Dim rect As Rectangle = New Rectangle(score.Location, score.Size)
                            Dim width As Single = ((rect.Width - .Spacing / 2 * (.Digits - 1)) / .Digits)
                            Do While width * .Digits > rect.Width - 1
                                width -= 1
                            Loop
                            Dim height As Integer = rect.Height - 1
                            Dim x As Single = rect.X + 1
                            Dim y As Integer = rect.Y + 1
                            Dim newsize As Size = New Size(width, height)
                            Dim image As Image = GetReelImage(.ReelType, .ReelColor, Backglass.currentData.UseDream7LEDs, Backglass.currentData.D7Thickness, Backglass.currentData.D7Shear, Backglass.currentData.D7Glow, newsize).Resized(newsize)
                            If image IsNot Nothing Then
                                For i As Integer = 1 To .Digits
                                    gr.DrawImage(image, New Point(x, y))
                                    x += width + .Spacing / 2
                                Next
                                image.Dispose()
                            End If
                        End If
                    End With
                Next
            End Using
            lightimage.Dispose()
            ' that's it
            Return newimage
        End Get
    End Property
    Public ReadOnly Property IlluminatedImageOnlyWithAlwaysOnLights() As Image
        Get
            If MyBase.Image IsNot Nothing Then
                Lights.ClearImages()
                ' create the light image
                Lights.DrawImages(GetFirstOnImage(), , True)
                ' do the overlay of the images
                Dim newimage As Bitmap = New Bitmap(MyBase.Image)
                For Each light As KeyValuePair(Of Integer, Illumination.Lights.ImageInfo) In Lights.Images
                    With light.Value
                        Dim lightimage As Bitmap = New Bitmap(.Image)
                        lightimage.MakeTransparent(Color.White)
                        Using gr As Graphics = Graphics.FromImage(newimage)
                            gr.DrawImage(lightimage, .Rectangle.X, .Rectangle.Y)
                        End Using
                        lightimage.Dispose()
                    End With
                Next
                ' that's it
                Return newimage
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property IlluminatedImageOnlyWithOnLights() As Image
        Get
            If MyBase.Image IsNot Nothing Then
                Lights.ClearImages()
                ' create the light image
                Lights.DrawImages(GetFirstOnImage(), , , True)
                ' do the overlay of the images
                Dim newimage As Bitmap = New Bitmap(MyBase.Image)
                For Each light As KeyValuePair(Of Integer, Illumination.Lights.ImageInfo) In Lights.Images
                    With light.Value
                        Dim lightimage As Bitmap = New Bitmap(.Image)
                        lightimage.MakeTransparent(Color.White)
                        Using gr As Graphics = Graphics.FromImage(newimage)
                            gr.DrawImage(lightimage, .Rectangle.X, .Rectangle.Y)
                        End Using
                        lightimage.Dispose()
                    End With
                Next
                ' that's it
                Return newimage
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property FirstStoredIlluminationImage() As Image
        Get
            Return GetFirstSelectedIlluImage()
        End Get
    End Property

    Public ReadOnly Property IlluminatedImages(ByVal currentimage As Image, _
                                               Optional ByVal currentoffimage As Image = Nothing) As Generic.SortedList(Of Integer, Illumination.Lights.ImageInfo)
        Get
            Lights.ClearImages()
            Lights.DrawImages(currentimage, currentoffimage)
            Return Lights.Images()
        End Get
    End Property

    Public Function DrawIlluminatedReelImage(ByVal reelimage As Image, ByVal reelintensity As Integer, ByVal reelillulocation As eReelIlluminationLocation) As Image
        Return Lights.DrawIlluminatedReelImage(reelimage, reelintensity, reelillulocation)
    End Function

    Private Function GetFirstOffImage() As Image
        Dim ret As Image = Nothing
        For Each item As Images.ImageInfo In Backglass.currentData.Images
            If item.Type = Images.eImageInfoType.BackgroundImage AndAlso item.BackgroundImageType = Images.eBackgroundImageType.Off Then
                ret = item.Image
                Exit For
            End If
        Next
        Return ret
    End Function

    Private Function GetFirstOnImage() As Image
        Dim ret As Image = MyBase.Image
        For Each item As Images.ImageInfo In Backglass.currentData.Images
            If item.Type = Images.eImageInfoType.BackgroundImage AndAlso item.BackgroundImageType = Images.eBackgroundImageType.On Then
                ret = item.Image
                Exit For
            End If
        Next
        Return ret
    End Function
    Private Function GetOnImageRomID() As Integer
        Dim ret As Integer = 0
        For Each item As Images.ImageInfo In Backglass.currentData.Images
            If item.Type = Images.eImageInfoType.BackgroundImage AndAlso item.BackgroundImageType = Images.eBackgroundImageType.On Then
                ret = item.RomID
                Exit For
            End If
        Next
        Return ret
    End Function
    Private Function GetOnImageRomIDType() As eRomIDType
        Dim ret As eRomIDType = eRomIDType.NotUsed
        For Each item As Images.ImageInfo In Backglass.currentData.Images
            If item.Type = Images.eImageInfoType.BackgroundImage AndAlso item.BackgroundImageType = Images.eBackgroundImageType.On Then
                ret = item.RomIDType
                Exit For
            End If
        Next
        Return ret
    End Function

    Private Function GetFirstSelectedIlluImage() As Image
        Dim ret As Image = Nothing
        For Each item As Images.ImageInfo In Backglass.currentData.Images
            If item.Type = Images.eImageInfoType.IlluminationImage Then 'AndAlso item.Selected Then
                ret = item.Image
                Exit For
            End If
        Next
        Return ret
    End Function

    Private animationOn As Generic.List(Of String) = New Generic.List(Of String)
    Private animationtimer As Timer = Nothing
    Private currentAnimationStep As Integer = -1
    Private currentAnimationSteps As Animation.AnimationStepCollection = Nothing
    Private Sub StartAnimation(ByVal _animationname As String)
        ' reset some basic animation stuff
        animationOn.Clear()
        animationtimeroff = False
        animationtimerticks = 0
        animationtimerloops = 0
        ' look for the animation
        Dim animation As Animation.AnimationHeader = Nothing
        For Each animationheader As Animation.AnimationHeader In Backglass.currentAnimations
            If animationheader.Name.Equals(_animationname) Then
                animation = animationheader
                Exit For
            End If
        Next
        ' maybe start the animation
        If animation IsNot Nothing AndAlso animation.AnimationSteps.Count > 0 Then
            If animationtimer Is Nothing Then
                animationtimer = New Timer()
                With animationtimer
                    .Enabled = False
                    AddHandler .Tick, AddressOf AnimationTimer_Tick
                End With
            End If
            animationtimer.Interval = animation.Interval
            animationtimerloops = If(animation.Loops = 0, 1, animation.Loops)
            currentAnimationStep = 0
            currentAnimationSteps = animation.AnimationSteps
            AnimationTimer_Tick()
        End If
    End Sub
    Private animationtimeroff As Boolean = False
    Private animationtimerticks As Integer = 0
    Private animationtimerloops As Integer = 0
    Private Sub AnimationTimer_Tick()
        animationtimer.Stop()
        animationtimerticks -= 1
        Do While animationtimerticks <= 0
            If Not animationtimeroff Then
                animationtimeroff = True
                animationtimerticks = currentAnimationSteps(currentAnimationStep).WaitLoopsAfterOn
                For Each cntrl As String In currentAnimationSteps(currentAnimationStep).On.Split(",")
                    If Not animationOn.Contains(cntrl.ToLower) Then animationOn.Add(cntrl.ToLower)
                Next
                Me.Invalidate()

            Else
                animationtimeroff = False
                animationtimerticks = currentAnimationSteps(currentAnimationStep).WaitLoopsAfterOff
                For Each cntrl As String In currentAnimationSteps(currentAnimationStep).Off.Split(",")
                    If animationOn.Contains(cntrl.ToLower) Then animationOn.Remove(cntrl.ToLower)
                Next
                Me.Invalidate()

                currentAnimationStep += 1
            End If
            If currentAnimationStep >= currentAnimationSteps.Count Then
                Exit Do
            End If
        Loop
        If currentAnimationStep < currentAnimationSteps.Count Then
            animationtimer.Start()
        ElseIf animationtimerloops > 1 Then
            animationtimerloops -= 1
            currentAnimationStep = 0
            AnimationTimer_Tick()
        Else
            currentAnimationSteps = Nothing
        End If
    End Sub

End Class
