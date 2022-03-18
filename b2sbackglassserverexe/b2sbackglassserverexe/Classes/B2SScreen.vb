Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class B2SScreen

    Private Const FileName As String = "ScreenRes.txt"

    Public formBackglass As formBackglass = Nothing
    Public formDMD As formDMD = Nothing
    Public formbackground As Background = Nothing

    Public Enum eDMDViewMode
        NotDefined = 0
        NoDMD = 1
        ShowDMD = 2
        ShowDMDOnlyAtDefaultLocation = 3
        DoNotShowDMDAtDefaultLocation = 4
    End Enum

    Public Property PlayfieldSize() As Size = New Size(0, 0)
    Public Property BackglassMonitor() As String = String.Empty
    Public Property BackglassScreen() As Screen = Nothing
    Public Property BackglassSize() As Size = New Size(0, 0)
    Public Property BackglassLocation() As Point = New Point(0, 0)
    Public Property BackglassGrillHeight() As Integer = 0
    Public Property BackglassSmallGrillHeight() As Integer = 0
    Public Property DMDSize() As Size = New Size(0, 0)
    Public Property DMDLocation() As Point = New Point(0, 0)
    Public Property DMDFlipY() As Boolean = False
    Public Property DMDAtDefaultLocation() As Boolean = True
    Public Property DMDViewMode() As eDMDViewMode = eDMDViewMode.NotDefined

    Public Property BackgroundSize() As Size = New Size(0, 0)
    Public Property BackgroundLocation() As Point = New Point(0, 0)
    Public Property BackgroundPath() As String = String.Empty

    Public Property BackglassCutOff() As Rectangle = Nothing

    Public Property IsDMDToBeShown() As Boolean = False

    
#Region "constructor and startup"

    Public Sub New()

        ' read settings file
        ReadB2SSettingsFromFile()

    End Sub

    Public Sub Start(ByVal _formBackglass As Form)

        ' here we go with one form for the backglass and no DMD and no grill
        Me.Start(_formBackglass, Nothing, New Size(0, 0), eDMDViewMode.NoDMD, 0, 0)

    End Sub
    Public Sub Start(ByVal _formBackglass As Form, ByVal _BackglassGrillHeight As Integer, ByVal _BackglassSmallGrillHeight As Integer)

        ' here we go with one form for the backglass and no DMD but with grill
        Me.Start(_formBackglass, Nothing, New Size(0, 0), eDMDViewMode.NoDMD, _BackglassGrillHeight, _BackglassSmallGrillHeight)

    End Sub
    Public Sub Start(ByVal _formBackglass As Form, ByVal _formDMD As Form, ByVal _DefaultDMDLocation As Point)

        ' here we go with two forms for the backglass and DMD and default values for the DMD view mode and the grill height
        Me.Start(_formBackglass, _formDMD, _DefaultDMDLocation, eDMDViewMode.ShowDMD, 0, 0)

    End Sub
    Public Sub Start(ByVal _formBackglass As Form, ByVal _formDMD As Form, ByVal _DefaultDMDLocation As Point, ByVal _DMDViewMode As eDMDViewMode, ByVal _BackglassGrillHeight As Integer, ByVal _BackglassSmallGrillHeight As Integer)

        ' here we go with one or two forms for the backglass and the DMD
        Me.formBackglass = _formBackglass
        Me.formDMD = _formDMD

        Me.formbackground = New Background


        ' get all backglass settings
        GetB2SSettings(_DefaultDMDLocation, _DMDViewMode, _BackglassGrillHeight, _BackglassSmallGrillHeight)

        ' show all backglass stuff
        Show()

    End Sub

#End Region


#Region "get backglass settings and show backglass"

    Private Sub ReadB2SSettingsFromFile()

        Dim specialFileName As String = String.Empty
        Dim specialDirFileName As String = String.Empty
        Dim loadSpecialOne As Boolean = False
        Dim loadSpecialDirOne As Boolean = False
        Try
            specialFileName = IO.Path.Combine(B2SData.TableFileName & ".res")
            loadSpecialOne = IO.File.Exists(specialFileName)
            specialDirFileName = IO.Path.Combine(B2SData.TableFileName, FileName)
            loadSpecialDirOne = IO.File.Exists(specialDirFileName)
        Catch
        End Try

        If loadSpecialOne OrElse loadSpecialDirOne OrElse IO.File.Exists(FileName) Then

            ' open settings file
            FileOpen(1, If(loadSpecialOne, specialFileName, If(loadSpecialDirOne, specialDirFileName, FileName)), OpenMode.Input)

            ' get all settings
            Dim line(50) As String
            Dim i As Integer = 0
            Do Until EOF(1)
                line(i) = LineInput(1)
                i += 1
            Loop
            line(i) = 0
            line(i + 1) = 0
            Me.PlayfieldSize = New Size(CInt(line(0)), CInt(line(1)))
            Me.BackglassSize = New Size(CInt(line(2)), CInt(line(3)))
            Me.BackglassMonitor = line(4)
            Me.BackglassLocation = New Point(CInt(line(5)), CInt(line(6)))
            Me.DMDSize = New Size(CInt(line(7)), CInt(line(8)))
            Me.DMDLocation = New Point(CInt(line(9)), CInt(line(10)))
            Me.DMDFlipY = (Trim(line(11)) = "1")

            If (i > 15) Then
                Me.BackgroundLocation = New Point(CInt(line(12)), CInt(line(13)))
                Me.BackgroundSize = New Size(CInt(line(14)), CInt(line(15)))
                Me.BackgroundPath = line(16)
            Else
                Me.BackgroundLocation = New Point(0, 0)
                Me.BackgroundSize = New Point(0, 0)
                Me.BackgroundPath = ""
            End If


            ' close file handle
            FileClose(1)

            Else

                MessageBox.Show("There is no B2S screen resolution file '" & FileName & "' in the current folder '" & IO.Directory.GetCurrentDirectory() & "'." & vbCrLf & vbCrLf &
                             "Please create this file with the tool 'B2S_ScreenResEditor.exe'.", _
                             "B2S backglass error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End

        End If
    End Sub

    Private Sub GetB2SSettings(ByVal _DefaultDMDLocation As Point, ByVal _DMDViewMode As eDMDViewMode, ByVal _BackglassGrillHeight As Integer, ByVal _BackglassSmallGrillHeight As Integer)

        Me.DMDViewMode = _DMDViewMode

        ' show or do not show the grill and do some more DMD stuff
        Dim showTheGrill As Boolean = (Me.DMDLocation.X = 0 AndAlso Me.DMDLocation.Y = 0)
        If B2SSettings.HideGrill = CheckState.Unchecked Then
            showTheGrill = True
        ElseIf B2SSettings.HideGrill = CheckState.Checked Then
            showTheGrill = False
        End If

        If showTheGrill Then

            ' show the grill
            Me.DMDAtDefaultLocation = True

            ' use default values for DMD location
            If _DefaultDMDLocation <> Nothing AndAlso _DMDViewMode <> eDMDViewMode.DoNotShowDMDAtDefaultLocation Then
                Me.DMDLocation = _DefaultDMDLocation
            End If

            ' maybe do some corrections since there is a small grill
            If _BackglassSmallGrillHeight > 0 AndAlso Me.formBackglass.DarkImage IsNot Nothing Then
                If Me.DMDLocation.Y > 0 Then
                    Me.DMDLocation = New Point(Me.DMDLocation.X, Me.DMDLocation.Y - _BackglassSmallGrillHeight)
                End If

                Me.BackglassSmallGrillHeight = _BackglassSmallGrillHeight

                Me.BackglassCutOff = New Rectangle(0, Me.formBackglass.DarkImage.Height - _BackglassGrillHeight - _BackglassSmallGrillHeight, Me.formBackglass.DarkImage.Width, _BackglassSmallGrillHeight)

                ' shrink some images to remove the small grill
                Me.formBackglass.DarkImage4Authentic = CutOutImage(Me.formBackglass.DarkImage4Authentic, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                If Me.formBackglass.TopLightImage4Authentic IsNot Nothing Then
                    Me.formBackglass.TopLightImage4Authentic = CutOutImage(Me.formBackglass.TopLightImage4Authentic, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If Me.formBackglass.SecondLightImage4Authentic IsNot Nothing Then
                    Me.formBackglass.SecondLightImage4Authentic = CutOutImage(Me.formBackglass.SecondLightImage4Authentic, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If Me.formBackglass.TopAndSecondLightImage4Authentic IsNot Nothing Then
                    Me.formBackglass.TopAndSecondLightImage4Authentic = CutOutImage(Me.formBackglass.TopAndSecondLightImage4Authentic, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If Me.formBackglass.DarkImage4Fantasy IsNot Nothing Then
                    Me.formBackglass.DarkImage4Fantasy = CutOutImage(Me.formBackglass.DarkImage4Fantasy, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If Me.formBackglass.TopLightImage4Fantasy IsNot Nothing Then
                    Me.formBackglass.TopLightImage4Fantasy = CutOutImage(Me.formBackglass.TopLightImage4Fantasy, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If Me.formBackglass.SecondLightImage4Fantasy IsNot Nothing Then
                    Me.formBackglass.SecondLightImage4Fantasy = CutOutImage(Me.formBackglass.SecondLightImage4Fantasy, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If
                If Me.formBackglass.TopAndSecondLightImage4Fantasy IsNot Nothing Then
                    Me.formBackglass.TopAndSecondLightImage4Fantasy = CutOutImage(Me.formBackglass.TopAndSecondLightImage4Fantasy, _BackglassGrillHeight, _BackglassSmallGrillHeight)
                End If

                ' set background image and new backglass form height (without grill)
                Me.formBackglass.BackgroundImage = Me.formBackglass.DarkImage
                Me.formBackglass.Size = Me.formBackglass.DarkImage.Size

            End If

        Else

            ' do not show the grill (if possible)
            Me.DMDAtDefaultLocation = False

            ' maybe hide grill
            If _BackglassGrillHeight > 0 AndAlso Me.formBackglass.DarkImage IsNot Nothing Then

                Me.BackglassGrillHeight = _BackglassGrillHeight
                Me.BackglassSmallGrillHeight = _BackglassSmallGrillHeight

                Me.BackglassCutOff = New Rectangle(0, Me.formBackglass.DarkImage.Height - _BackglassGrillHeight, Me.formBackglass.DarkImage.Width, _BackglassGrillHeight)

                ' shrink some images to remove the grill
                Me.formBackglass.DarkImage4Authentic = ResizeImage(Me.formBackglass.DarkImage4Authentic, _BackglassGrillHeight)
                If Me.formBackglass.TopLightImage4Authentic IsNot Nothing Then
                    Me.formBackglass.TopLightImage4Authentic = ResizeImage(Me.formBackglass.TopLightImage4Authentic, _BackglassGrillHeight)
                End If
                If Me.formBackglass.SecondLightImage4Authentic IsNot Nothing Then
                    Me.formBackglass.SecondLightImage4Authentic = ResizeImage(Me.formBackglass.SecondLightImage4Authentic, _BackglassGrillHeight)
                End If
                If Me.formBackglass.TopAndSecondLightImage4Authentic IsNot Nothing Then
                    Me.formBackglass.TopAndSecondLightImage4Authentic = ResizeImage(Me.formBackglass.TopAndSecondLightImage4Authentic, _BackglassGrillHeight)
                End If
                If Me.formBackglass.DarkImage4Fantasy IsNot Nothing Then
                    Me.formBackglass.DarkImage4Fantasy = ResizeImage(Me.formBackglass.DarkImage4Fantasy, _BackglassGrillHeight)
                End If
                If Me.formBackglass.TopLightImage4Fantasy IsNot Nothing Then
                    Me.formBackglass.TopLightImage4Fantasy = ResizeImage(Me.formBackglass.TopLightImage4Fantasy, _BackglassGrillHeight)
                End If
                If Me.formBackglass.SecondLightImage4Fantasy IsNot Nothing Then
                    Me.formBackglass.SecondLightImage4Fantasy = ResizeImage(Me.formBackglass.SecondLightImage4Fantasy, _BackglassGrillHeight)
                End If
                If Me.formBackglass.TopAndSecondLightImage4Fantasy IsNot Nothing Then
                    Me.formBackglass.TopAndSecondLightImage4Fantasy = ResizeImage(Me.formBackglass.TopAndSecondLightImage4Fantasy, _BackglassGrillHeight)
                End If

                ' set background image and new backglass form height (without grill)
                Me.formBackglass.BackgroundImage = Me.formBackglass.DarkImage
                Me.formBackglass.Size = Me.formBackglass.DarkImage.Size

            End If

            ' maybe rotate DMD image
            If Me.DMDFlipY AndAlso Me.formDMD IsNot Nothing AndAlso Me.formDMD.BackgroundImage IsNot Nothing Then
                Me.formDMD.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
            End If
        End If

    End Sub

    Private Sub Show()

        'On Error Resume Next

        ' first of all get the info whether the DMD is to be shown or not
        IsDMDToBeShown = (Me.formDMD IsNot Nothing AndAlso Not Point.Empty.Equals(Me.DMDLocation) AndAlso
            ((Me.DMDViewMode = eDMDViewMode.ShowDMD) OrElse
             (Me.DMDViewMode = eDMDViewMode.ShowDMDOnlyAtDefaultLocation AndAlso Me.DMDAtDefaultLocation) OrElse
             (Me.DMDViewMode = eDMDViewMode.DoNotShowDMDAtDefaultLocation AndAlso Not Me.DMDAtDefaultLocation)))

        ' get the correct screen
        Me.BackglassScreen = Screen.AllScreens(0)
        Dim s As Screen

        For Each s In Screen.AllScreens
            If Left(BackglassMonitor, 1) = "@" Then
                If s.Bounds.Left = CInt(Mid(BackglassMonitor, 2)) Then
                    Me.BackglassScreen = s
                    Exit For
                End If
            ElseIf Mid(s.DeviceName, 1, 12) = "\\.\DISPLAY" + BackglassMonitor Then
                Me.BackglassScreen = s
                Exit For
            End If
        Next
        On Error GoTo 0


        ' Westworld show background form, only if background is set and enabled in setting
        Dim OriginalOffset = Me.BackglassLocation
        If ((Not (Me.BackgroundSize.IsEmpty)) And B2SSettings.StartBackground) Then
            Dim swapSize = Me.BackgroundSize
            Dim swapLocation = Me.BackgroundLocation
            Me.BackgroundSize = Me.BackglassSize
            Me.BackglassSize = swapSize
            Me.BackgroundLocation = Me.BackglassLocation
            Me.BackglassLocation = swapLocation

            Me.formbackground.StartPosition = FormStartPosition.Manual
            Me.formbackground.BackgroundImageLayout = ImageLayout.Stretch
            Me.formbackground.FormBorderStyle = FormBorderStyle.None
            Me.formbackground.ControlBox = False
            Me.formbackground.MaximizeBox = False
            Me.formbackground.MinimizeBox = False
            Me.formbackground.Location = Me.BackglassScreen.Bounds.Location + Me.BackgroundLocation
            Me.formbackground.Size = Me.BackgroundSize
            Me.formbackground.Text = "Background"
            Me.formbackground.BackColor = Color.Black
            If (IO.File.Exists(Me.BackgroundPath)) Then
                Me.formbackground.BackgroundImage = Image.FromFile(Me.BackgroundPath) ' ("C:\backglass.png")
            End If
            Me.formbackground.Show()
            If B2SSettings.FormToFront Then
                Me.formbackground.BringToFront()
            Else
                Me.formbackground.SendToBack()
            End If
        End If

        ' set forms to background image size
        If Me.formBackglass IsNot Nothing AndAlso Me.formBackglass.BackgroundImage IsNot Nothing Then
            Me.formBackglass.Size = Me.formBackglass.BackgroundImage.Size
        End If
        If Me.formDMD IsNot Nothing AndAlso Me.formDMD.BackgroundImage IsNot Nothing Then
            Me.formDMD.Size = Me.formDMD.BackgroundImage.Size
        End If

        ' calculate backglass rescale factors
        Dim rescaleBackglassX As Single = Me.formBackglass.Width / Me.BackglassSize.Width
        Dim rescaleBackglassY As Single = Me.formBackglass.Height / Me.BackglassSize.Height
        If Me.formBackglass.BackgroundImage IsNot Nothing Then
            rescaleBackglassX = Me.formBackglass.BackgroundImage.Width / Me.BackglassSize.Width
            rescaleBackglassY = Me.formBackglass.BackgroundImage.Height / Me.BackglassSize.Height
        End If

        ' maybe rescale the location and the size because this is the default and therefore it has to be done
        Dim rescaleDMDX As Single = 1
        Dim rescaleDMDY As Single = 1
        If IsDMDToBeShown Then
            If Me.DMDAtDefaultLocation Then
                Me.DMDSize = Me.formDMD.Size
                If rescaleBackglassX <> 1 OrElse rescaleBackglassY <> 1 Then
                    Me.DMDLocation = New Point(Int(Me.DMDLocation.X / rescaleBackglassX), Int(Me.DMDLocation.Y / rescaleBackglassY))
                    Me.DMDSize = New Size(Int(Me.DMDSize.Width / rescaleBackglassX), Int(Me.DMDSize.Height / rescaleBackglassY))
                End If
            End If

            ' calculate DMD rescale factors
            rescaleDMDX = Me.formDMD.Width / Me.DMDSize.Width
            rescaleDMDY = Me.formDMD.Height / Me.DMDSize.Height
            If Me.formDMD.BackgroundImage IsNot Nothing Then
                rescaleDMDX = Me.formDMD.BackgroundImage.Width / Me.DMDSize.Width
                rescaleDMDY = Me.formDMD.BackgroundImage.Height / Me.DMDSize.Height
            End If
        End If

        ' move and scale all picked objects
        ScaleAllControls(rescaleBackglassX, rescaleBackglassY, rescaleDMDX, rescaleDMDY)


        ' show the backglass form
        Me.formBackglass.StartPosition = FormStartPosition.Manual
        Me.formBackglass.BackgroundImageLayout = ImageLayout.Stretch
        Me.formBackglass.FormBorderStyle = FormBorderStyle.None
        Me.formBackglass.ControlBox = False
        Me.formBackglass.MaximizeBox = False
        Me.formBackglass.MinimizeBox = False
        Me.formBackglass.Location = Me.BackglassScreen.Bounds.Location + Me.BackglassLocation
        Me.formBackglass.Size = Me.BackglassSize
        Me.formBackglass.Text = "Form1"
        Me.formBackglass.Show()

        ' bring backglass screen to the front
        Me.formBackglass.BringToFront()

        ' maybe show DMD form
        If IsDMDToBeShown Then
            ' set DMD location relative to the backglass location
            Me.formDMD.StartPosition = FormStartPosition.Manual
            Me.formDMD.BackgroundImageLayout = ImageLayout.Stretch
            Me.formDMD.FormBorderStyle = FormBorderStyle.None
            Me.formDMD.ControlBox = False
            Me.formDMD.MaximizeBox = False
            Me.formDMD.MinimizeBox = False
            Me.formDMD.Location = Me.BackglassScreen.Bounds.Location + OriginalOffset + Me.DMDLocation  ' was Me.formBackglass.Location + Me.DMDLocation
            Me.formDMD.Size = Me.DMDSize
            ' show the DMD form
            Me.formDMD.Show() 'formBackglass)
            Me.formDMD.BringToFront()
            Me.formDMD.TopMost = True
        End If

    End Sub

    Private Sub ScaleAllControls(ByVal _rescaleX As Single, ByVal _rescaleY As Single, ByVal _rescaleDMDX As Single, ByVal _rescaleDMDY As Single)

        ' get scale info for all picked objects and scale some of them
        For Each cntrlinfo As KeyValuePair(Of String, B2SReelBox) In B2SData.Reels
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, If(isOnDMD, _rescaleDMDX, _rescaleX), If(isOnDMD, _rescaleDMDY, _rescaleY), isOnDMD)
        Next
        For Each cntrlinfo As KeyValuePair(Of String, B2SLEDBox) In B2SData.LEDs
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, If(isOnDMD, _rescaleDMDX, _rescaleX), If(isOnDMD, _rescaleDMDY, _rescaleY), isOnDMD, isOnDMD AndAlso DMDFlipY AndAlso Not DMDAtDefaultLocation)
        Next
        For Each cntrlinfo As KeyValuePair(Of String, Dream7Display) In B2SData.LEDDisplays
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, If(isOnDMD, _rescaleDMDX, _rescaleX), If(isOnDMD, _rescaleDMDY, _rescaleY), isOnDMD, isOnDMD AndAlso DMDFlipY AndAlso Not DMDAtDefaultLocation)
        Next
        For Each cntrlinfo As KeyValuePair(Of String, B2SPictureBox) In B2SData.Illuminations
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, _rescaleX, _rescaleY, isOnDMD)
        Next
        For Each cntrlinfo As KeyValuePair(Of String, B2SPictureBox) In B2SData.DMDIlluminations
            Dim isOnDMD As Boolean = (cntrlinfo.Value.Parent IsNot Nothing AndAlso cntrlinfo.Value.Parent.Name.Equals("formDMD"))
            ScaleControl(cntrlinfo.Value, _rescaleDMDX, _rescaleDMDY, isOnDMD, DMDFlipY AndAlso Not DMDAtDefaultLocation)
        Next

        ' get scale info for the LED areas
        For Each ledarea As KeyValuePair(Of String, B2SData.LEDAreaInfo) In B2SData.LEDAreas
            If ledarea.Value.IsOnDMD Then
                If formDMD IsNot Nothing Then
                    Dim y As Single = ledarea.Value.Rect.Y / _rescaleDMDY
                    If DMDFlipY AndAlso Not DMDAtDefaultLocation Then
                        y = formDMD.Height / _rescaleDMDY - y - ledarea.Value.Rect.Height / _rescaleDMDY
                    End If
                    ledarea.Value.Rect = Rectangle.Round(New Rectangle(ledarea.Value.Rect.X / _rescaleDMDX, y, ledarea.Value.Rect.Width / _rescaleDMDX, ledarea.Value.Rect.Height / _rescaleDMDY))
                End If
            Else
                ledarea.Value.Rect = Rectangle.Round(New Rectangle(ledarea.Value.Rect.X / _rescaleX, ledarea.Value.Rect.Y / _rescaleY, ledarea.Value.Rect.Width / _rescaleX, ledarea.Value.Rect.Height / _rescaleY))
            End If
        Next

        ' and now recalc the backglass cut off rectangle
        If BackglassCutOff <> Nothing Then
            BackglassCutOff = New Rectangle(BackglassCutOff.X / _rescaleX, BackglassCutOff.Y / _rescaleY, BackglassCutOff.Width / _rescaleX, BackglassCutOff.Height / _rescaleY)
        End If

    End Sub

    Private Sub ScaleControl(ByVal _cntrl As B2SBaseBox, ByVal _rescaleX As Single, ByVal _rescaleY As Single, ByVal isOnDMD As Boolean, Optional ByVal flipY As Boolean = False)

        ' calculate the exact location and size of all controls
        If Not isOnDMD AndAlso BackglassCutOff <> Nothing Then
            If BackglassCutOff.IntersectsWith(New Rectangle(_cntrl.Left, _cntrl.Top, _cntrl.Width, _cntrl.Height)) Then
                _cntrl.RectangleF = New RectangleF(0, 0, 0, 0)
            ElseIf BackglassCutOff.Top < _cntrl.Top Then
                _cntrl.RectangleF = New RectangleF(_cntrl.Left / _rescaleX, (_cntrl.Top - Me.BackglassSmallGrillHeight) / _rescaleY, _cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY)
            Else
                _cntrl.RectangleF = New RectangleF(_cntrl.Left / _rescaleX, _cntrl.Top / _rescaleY, _cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY)
            End If
        Else
            _cntrl.RectangleF = New RectangleF(_cntrl.Left / _rescaleX, _cntrl.Top / _rescaleY, _cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY)
        End If
        
        ' scale not more than the LED and reel boxes
        If TypeOf _cntrl Is B2SLEDBox OrElse TypeOf _cntrl Is B2SReelBox Then
            _cntrl.Location = New Point(Point.Round(_cntrl.RectangleF.Location))
            _cntrl.Size = New Size(Size.Round(_cntrl.RectangleF.Size))
        End If

        ' maybe flip DMD images
        If flipY AndAlso formDMD IsNot Nothing Then
            If TypeOf _cntrl Is B2SLEDBox Then
            Else
                Dim picbox As B2SPictureBox = TryCast(_cntrl, B2SPictureBox)
                If picbox IsNot Nothing Then
                    ' set new top location
                    Dim newY As Single = formDMD.Height / _rescaleY - picbox.RectangleF.Y - picbox.RectangleF.Height
                    picbox.RectangleF = New RectangleF(picbox.RectangleF.X, newY, picbox.RectangleF.Width, picbox.RectangleF.Height)
                    ' flip the images
                    If picbox.BackgroundImage IsNot Nothing Then
                        picbox.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
                    End If
                    If picbox.OffImage IsNot Nothing Then
                        picbox.OffImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
                    End If
                End If
            End If
        End If

    End Sub
    Private Sub ScaleControl(ByVal _cntrl As Dream7Display, ByVal _rescaleX As Single, ByVal _rescaleY As Single, ByVal isOnDMD As Boolean, Optional ByVal flipY As Boolean = False)

        ' store current LED location
        Dim rectF As RectangleF = New RectangleF(New PointF(_cntrl.Left / _rescaleX, _cntrl.Top / _rescaleY), New Size(_cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY))

        ' scale LED display
        _cntrl.Location = Point.Round(New PointF(_cntrl.Left / _rescaleX, _cntrl.Top / _rescaleY))
        _cntrl.Size = Size.Round(New Size(_cntrl.Width / _rescaleX, _cntrl.Height / _rescaleY))
        _cntrl.Spacing = _cntrl.Spacing / _rescaleX
        _cntrl.OffsetWidth = _cntrl.OffsetWidth / _rescaleX
        _cntrl.Thickness = _cntrl.Thickness / _rescaleX

        ' maybe flip LEDs
        If flipY Then
            _cntrl.Mirrored = True
            ' set new top location
            If formDMD IsNot Nothing Then
                Dim newY As Single = formDMD.Height / _rescaleY - rectF.Y - rectF.Height
                _cntrl.Location = Point.Round(New PointF(rectF.X, newY))
            End If
        End If

    End Sub

    Private Function ResizeImage(ByVal sourceimage As Image, ByVal grillheight As Integer) As Image
        Dim imageWithoutGrill As Bitmap = New Bitmap(sourceimage.Width, sourceimage.Height - grillheight)
        Using gr As Graphics = Graphics.FromImage(imageWithoutGrill)
            gr.DrawImage(sourceimage, New Rectangle(0, 0, imageWithoutGrill.Width, imageWithoutGrill.Height + grillheight))
        End Using
        Return imageWithoutGrill
    End Function
    Private Function CutOutImage(ByVal sourceimage As Image, ByVal grillheight As Integer, ByVal smallgrillheight As Integer) As Image
        Dim imageWithoutSmallGrill As Bitmap = New Bitmap(sourceimage.Width, sourceimage.Height - smallgrillheight)
        Using imageBackglass As Bitmap = PartFromImage(sourceimage, New Rectangle(0, 0, sourceimage.Width, sourceimage.Height - grillheight - smallgrillheight))
            Using imageGrill As Bitmap = PartFromImage(sourceimage, New Rectangle(0, sourceimage.Height - grillheight, sourceimage.Width, grillheight))
                Using gr As Graphics = Graphics.FromImage(imageWithoutSmallGrill)
                    gr.DrawImage(imageBackglass, New Rectangle(0, 0, imageBackglass.Width, imageBackglass.Height))
                    gr.DrawImage(imageGrill, New Rectangle(0, imageBackglass.Height, imageGrill.Width, imageGrill.Height))
                End Using
            End Using
        End Using
        Return imageWithoutSmallGrill
    End Function
    Private Function PartFromImage(image As Image, area As Rectangle) As Image
        If image Is Nothing Then Return Nothing
        Dim imageBackground As Bitmap = CType(image, Bitmap)
        Dim imagePart As Image = New Bitmap(area.Width, area.Height)
        imagePart = imageBackground.Clone(area, Imaging.PixelFormat.Format32bppArgb)
        Return imagePart
    End Function

#End Region


#Region "screenshot stuff"

    Public Function MakeScreenShot(ByVal filename As String,
                                   ByVal fileformat As Imaging.ImageFormat) As Boolean

        ' get screenshot
        Dim screenshot As Bitmap = New Bitmap(Me.formBackglass.Width, Me.formBackglass.Height)
        Using gr As Graphics = Graphics.FromImage(screenshot)
            gr.CopyFromScreen(Me.formBackglass.Location, Point.Empty, Me.formBackglass.Size)
        End Using

        ' save it
        Try
            screenshot.Save(filename, fileformat)
        Catch ex As Exception
            MessageBox.Show(ex.Message, My.Resources.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            screenshot.Dispose()
            screenshot = Nothing
        End Try

        Return True

    End Function

#End Region


End Class
