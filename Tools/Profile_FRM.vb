Imports MySql.Data.MySqlClient
Imports System.IO

Public Class Profile_FRM
    Private Sub btn_InsertImage_Click(sender As System.Object, e As System.EventArgs) Handles btn_InsertImage.Click
        FillPictureBox() 'loads this sub 
    End Sub

    Private Sub FillPictureBox()
        OpenFileDialogImage.Filter = "Image Files(*.BMP;*.JPG;*.JEPG;*.GIF)|*.BMP;*.JPG;*.JEPG;*.GIF" 'filter out images and gifs only
        If OpenFileDialogImage.ShowDialog() = DialogResult.OK Then 'when user has selected an image
            If My.Computer.FileSystem.FileExists(OpenFileDialogImage.FileName) Then 'determines if file path exists
                stream = OpenFileDialogImage.OpenFile() 'opens the file
                If Not (stream Is Nothing) Then ' determine if file not empty
                    PBoxUser.Image = Image.FromFile(OpenFileDialogImage.FileName) 'sets the picture box image to file image
                    stream.Close() ' close the stream
                    SaveImageToDatabase() 'calls the sub to save image to database
                End If
            End If
        End If

    End Sub

    Private Sub SaveImageToDatabase()
        MysqlConn.Close()
        MysqlConn.Open() 'opens database connection
        Dim ms As New MemoryStream()
        PBoxUser.Image.Save(ms, PBoxUser.Image.RawFormat) 'save the image in the picturebox as a stream stored in RAM
        Dim content As Byte() = ms.GetBuffer() ' save the streama as a string of bytes
        Try
            With COMMAND ' used to reduce repeated keyword
                COMMAND = New MySqlCommand("UPDATE userinfo SET Image=@Image WHERE UniqueUserId=@UniqueUserId", MysqlConn) 'update the database with a new image, set this image at UniqueUserID
                .Parameters.AddWithValue("@UniqueUserId", UserAccess.CallUniqueUserID) '
                .Parameters.AddWithValue("@Image", content) ' Save the string of byte (Image)
                .ExecuteNonQuery() ' Exectute the query
            End With
            MysqlConn.Close() 'close the database connection
            MessageBox.Show("Image Uploaded!", "Saved") 'output image saved successfully
        Catch ex As Exception
            MsgBox("Erorr: " & vbCrLf & ex.Message) 'display error message
        End Try
    End Sub

    Private Sub LoadImage()
        MysqlConn.Close()
        MysqlConn.Open()
        Dim stream As New MemoryStream()
        Try
            Dim command As New MySqlCommand("select Image from userinfo where UniqueUserId='" & UserAccess.CallUniqueUserID & "'", MysqlConn)
            Dim image As Byte() = DirectCast(command.ExecuteScalar(), Byte())
            stream.Write(image, 0, image.Length)
            MysqlConn.Close()
            Dim bitmap As New Bitmap(stream)
            PBoxUser.Image = bitmap
            MsgBox("Successfully load image")
        Catch ex As Exception
            PBoxUser.Image = Nothing '    if no image is read from the database the picture box is set to nothing
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub User_FRM_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load 'upon form load
        LoadImage() 'load this sub
        LoadUserInfo()
    End Sub

    Private Sub LoadUserInfo()
        'All the information retrieved from database during user login is stored in the user class, this sub displays what is stored in the user class or displays the users details 
        With UserAccess ' used to reduce repeated keyword
            LabelName.Text = "Name: " & .strFirstName & " " & .strSurName
            LabelBirthday.Text = "Birthday: " & .dateDOB
            LabelEmail.Text = "Email: " & .CallEmail
            LabelUsername.Text = "Username: " & .CallUsername
            LabelGender.Text = "Gender: " & .GenderType
            LabelCountry.Text = "Country: " & .CallCountry
            LabelAccountType.Text = "AccountType: " & .CallAccountType
        End With
    End Sub

    Private Sub ButtonChangePass_Click(sender As System.Object, e As System.EventArgs) Handles BtnChangePassword.Click
        ChangePassword_FRM.ShowDialog() 'loads the change password form
    End Sub
End Class