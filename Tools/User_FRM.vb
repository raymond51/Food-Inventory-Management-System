Imports MySql.Data.MySqlClient
Imports System.IO

Public Class User_FRM


    Private Sub btn_InsertImage_Click(sender As System.Object, e As System.EventArgs) Handles btn_InsertImage.Click
        FillPictureBox()
    End Sub

    Private Sub FillPictureBox()

        OpenFileDialogImage.Filter = "Image Files(*.BMP;*.JPG;*.JEPG;*.GIF)|*.BMP;*.JPG;*.JEPG;*.GIF"
        If OpenFileDialogImage.ShowDialog() = DialogResult.OK Then
            If My.Computer.FileSystem.FileExists(OpenFileDialogImage.FileName) Then

                stream = OpenFileDialogImage.OpenFile()
                If Not (stream Is Nothing) Then

                    PBoxUser.Image = Image.FromFile(OpenFileDialogImage.FileName)
                    stream.Close()
                    SaveImageToDatabase()
                End If

            End If
        End If

    End Sub

    Dim cnn As MySqlConnection
    Dim connectionString As String
    '@@@@@@@@@@@@@@@@@@@@@@@@@
    'sub to save the changed details 
    'SaveChangedInfo
    '@@@@@@@@@@@@@@@@@@@@@@@@@
    Private Sub SaveImageToDatabase()

        connectionString = "server=127.0.0.1;userid=root;password=Maplestory;database=projectz"
        cnn = New MySqlConnection(connectionString)

        Dim ms As New MemoryStream()
        PBoxUser.Image.Save(ms, PBoxUser.Image.RawFormat)
        Dim content As Byte() = ms.GetBuffer()
        cnn.Open()
        Try
            Dim cmd As MySqlCommand

            '  cmd = New MySqlCommand("update FROM edata WHERE idEmployeeinfo=1", cnn)
            '    cmd.ExecuteNonQuery()
            'Delete previously stored image to upload a new one in the same primary key
            ' cmd = New MySqlCommand("insert into projectyasuo (idEmployeeInfo,Image) values ( @idEmployeeinfo,@Image)", cnn)
            cmd = New MySqlCommand("UPDATE userinfo SET Image= @Image WHERE UniqueUserId=@UniqueUserId", cnn)
            cmd.Parameters.AddWithValue("@UniqueUserId", UserAccess.CallUniqueUserID)
            cmd.Parameters.AddWithValue("@Image", content)
            cmd.ExecuteNonQuery()

            cnn.Close()
            MessageBox.Show("Image Uploaded!", "Saved")
        Catch ex As Exception
            MsgBox("Erorr: " & vbCrLf & ex.Message)
        End Try


    End Sub
    Private Sub LoadImage()
        connectionString = "server=127.0.0.1;userid=root;password=Maplestory;database=projectz "
        cnn = New MySqlConnection(connectionString)

        Dim stream As New MemoryStream()
        cnn.Open()
        Try
            Dim command As New MySqlCommand("select Image from userinfo where UniqueUserId='" & UserAccess.CallUniqueUserID & "'", cnn)
            Dim image As Byte() = DirectCast(command.ExecuteScalar(), Byte())
            stream.Write(image, 0, image.Length)
            cnn.Close()
            Dim bitmap As New Bitmap(stream)
            PBoxUser.Image = bitmap
            MsgBox("Successfully load image")
        Catch ex As Exception
            MsgBox(ex.Message)
            '  PBoxUser.Image = Nothing
            'if no image is read from the database the picture box is set to nothing
        End Try
    End Sub
  
    Private Sub User_FRM_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        LoadUserInfo()
    End Sub

    Private Sub LoadUserInfo()
        With UserAccess
            LabelName.Text = "Name: " & .strFirstName & " " & .strSurName
            LabelBirthday.Text = "Birthday: " & .dateDOB
            LabelEmail.Text = "Email: " & .CallEmail
            LabelUsername.Text = "Username: " & .CallUsername
            LabelGender.Text = "Gender: " & .GenderType
            LabelCountry.Text = "Country: " & .CallCountry
            LabelAccountType.Text = "AccountType: " & .CallAccountType

        End With
    End Sub
    Private Sub btn_UserInfo_Click(sender As System.Object, e As System.EventArgs) Handles btn_UserInfo.Click
        MessageBox.Show(UserAccess.ToString)
    End Sub

End Class