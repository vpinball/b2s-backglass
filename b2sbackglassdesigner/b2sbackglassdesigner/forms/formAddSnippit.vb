Imports System

Public Class formAddSnippit

    Private onefileselected As Boolean = True
    Private filenames As String() = Nothing

    Public Event SnippitAdded(ByVal sender As Object, ByVal e As AddSnippitEventArgs)
    Public Class AddSnippitEventArgs

        Inherits EventArgs

        Public Name As String = String.Empty
        Public Image As Image = Nothing
        Public FileName As String = String.Empty
        Public Location As Point = Nothing

        Public Sub New(ByVal _name As String, ByVal _image As Image, ByVal _filename As String, ByVal _loc As Point)
            Name = _name
            Image = _image
            FileName = _filename
            Location = _loc
        End Sub

    End Class

    Private Sub formAddSnippit_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        btnChooseImageFile.Focus()
    End Sub

    Private Sub btnChooseImageFile_Click(sender As System.Object, e As System.EventArgs) Handles btnChooseImageFile.Click
        Using filedialog As OpenFileDialog = New OpenFileDialog()
            With filedialog
                .Filter = ImageFileExtensionFilter
                .FileName = String.Empty
                .Multiselect = True
                If .ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Try
                        If filedialog.FileNames.Count <= 1 Then
                            onefileselected = True
                            txtImageFileName.Text = filedialog.FileName
                            txtImageFileName.Enabled = True
                            Dim image As Image = Bitmap.FromFile(filedialog.FileName).Copy(True)
                            PictureBoxImage.BackgroundImage = image
                            txtName.Text = IO.Path.GetFileNameWithoutExtension(filedialog.FileName)
                            txtName.Enabled = True
                        Else
                            onefileselected = False
                            filenames = filedialog.FileNames
                            txtImageFileName.Text = "... " & filedialog.FileNames.Count.ToString() & " files selected ..."
                            txtImageFileName.Enabled = False
                            Dim image As Image = Bitmap.FromFile(filedialog.FileNames(0)).Copy(True)
                            PictureBoxImage.BackgroundImage = image
                            txtName.Text = "..."
                            txtName.Enabled = False
                        End If
                    Catch
                        MessageBox.Show(My.Resources.MSG_CannotLoadPic, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        If txtName.Enabled Then
                            txtName.Focus()
                        Else
                            txtX.Focus()
                        End If
                    End Try
                End If
            End With
        End Using
    End Sub

    Private Sub Apply_Click(sender As System.Object, e As System.EventArgs) Handles btnApply.Click
        Dim reset As Boolean = False
        If onefileselected Then
            If Not String.IsNullOrEmpty(txtImageFileName.Text) AndAlso
                    PictureBoxImage.BackgroundImage IsNot Nothing AndAlso
                    Not String.IsNullOrEmpty(txtX.Text) AndAlso
                    Not String.IsNullOrEmpty(txtY.Text) AndAlso
                    IsNumeric(txtX.Text) AndAlso IsNumeric(txtY.Text) Then
                Dim x As Integer = If(IsNumeric(txtX.Text), CInt(txtX.Text), 0)
                Dim y As Integer = If(IsNumeric(txtY.Text), CInt(txtY.Text), 0)
                RaiseEvent SnippitAdded(Me, New AddSnippitEventArgs(txtName.Text,
                                                                    PictureBoxImage.BackgroundImage,
                                                                    txtImageFileName.Text,
                                                                    New Point(x, y)))
                reset = True
            Else
                MessageBox.Show(My.Resources.MSG_AddSnippit, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            If filenames IsNot Nothing AndAlso
                    Not String.IsNullOrEmpty(txtX.Text) AndAlso
                    Not String.IsNullOrEmpty(txtY.Text) AndAlso
                    IsNumeric(txtX.Text) AndAlso IsNumeric(txtY.Text) Then
                Dim x As Integer = If(IsNumeric(txtX.Text), CInt(txtX.Text), 0)
                Dim y As Integer = If(IsNumeric(txtY.Text), CInt(txtY.Text), 0)
                For Each file As String In filenames
                    RaiseEvent SnippitAdded(Me, New AddSnippitEventArgs(IO.Path.GetFileNameWithoutExtension(file),
                                                                        Bitmap.FromFile(file).Copy(True),
                                                                        file,
                                                                        New Point(x, y)))
                    reset = True
                Next
            Else
                MessageBox.Show(My.Resources.MSG_AddSnippit, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        ' maybe reset all data
        If reset Then
            onefileselected = True
            filenames = Nothing
            PictureBoxImage.BackgroundImage = Nothing
            txtImageFileName.Text = String.Empty
            txtName.Text = String.Empty
            txtX.Text = String.Empty
            txtY.Text = String.Empty
            txtImageFileName.Enabled = True
            txtName.Enabled = True
            btnChooseImageFile.Focus()
        End If
    End Sub

End Class