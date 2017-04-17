Imports MySql.Data.MySqlClient 'mySQL Library 
Imports System.Net.Mail 'mail library
Public Class Login_FRM
    Dim MysqlConn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Private Sub btnLogin_Click(sender As System.Object, e As System.EventArgs) Handles btnLogin.Click
        MysqlConn = New MySqlConnection("server=localhost;userid=root;password=root;database=projectz") 'connection credentials
        Try
            MysqlConn.Open() 'opens connection
            COMMAND = New MySqlCommand("Select * from projectz.userinfo where Username= '" & textBox_User.Text & "' and Password= '" & textBox_Pass.Text & "' ", MysqlConn)
            'take the username and password from textbox and validate it against the database
            'takes 2 arguments: query string & mysqlconn object
            READER = COMMAND.ExecuteReader      'execute this command and read the data from database
            Dim count As Integer 'this variable will detect if there is a login that matches user input in the database
            count = 0 'initialise
            While READER.Read 'loop through the database 
                count = count + 1
            End While
            If count = 1 Then 'if no duplicate login 
                GrabUserInfoFromLogin() ' a sub which will store user information from database 
                FRM_Loading.ShowDialog() ' Display the loading form
                MainInventory.Show() 'Once loading form complete display the inventory
                Me.Hide() 'hide the login form
            ElseIf count > 1 Then
                MessageBox.Show("Username and password are Duplicate")
                'more than one username or password present in the database which is duplicate
            Else
                MessageBox.Show("Username and password are not correct")
                textBox_Pass.Clear() ' clear password textbox
            End If
            MysqlConn.Close() 'close the connection
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            'similar to .close but object state will be reset 
        End Try
    End Sub

    Private Sub GrabUserInfoFromLogin()
        MysqlConn = New MySqlConnection("server=localhost;userid=root;password=root;database=projectz") 'connection credentials
        MysqlConn.Open() 'opens connection
        Dim READER As MySqlDataReader
        Dim Query As String 'A declared variable used to hold the SQl query/statement
        Query = "Select * from projectz.userinfo where Username= '" & textBox_User.Text & "' and Password= '" & textBox_Pass.Text & "' "
        COMMAND = New MySqlCommand(Query, MysqlConn)
        Try 'catches an error and displays message to user without crashing the program
            READER = COMMAND.ExecuteReader 'executes the query 
            While READER.Read 'loop through the database 
                With UserAccess 'UserAccess is a Class which will create a new instance and store user information
                    'used to prevent repeating the word 'UserAccess' 
                    .CallUniqueUserID = (READER.GetString("UniqueUserId")) 'getstring reads string values
                    .strFirstName = (READER.GetString("Firstname"))
                    .strSurName = (READER.GetString("Surname"))
                    .dateDOB = (READER.GetDateTime("Birthday")) 'getdateTime reads Date and time values
                    .CallUsername = (READER.GetString("Username"))
                    .CallEmail = (READER.GetString("Email"))
                    .GenderType = (READER.GetChar("Gender")) ' getchar reads a single charecter
                    .CallCountry = (READER.GetString("Country"))
                    .CallAccountType = (READER.GetString("AccountType"))
                    'before the equals operator these identifiers is a get set function which stores the value 
                End With
            End While
            MysqlConn.Close() 'close connection with database
        Catch ex As Exception
            MessageBox.Show(ex.Message) 'displays error message according to the type of error thrown
        End Try

    End Sub
    Private Sub btn_Register_Click(sender As System.Object, e As System.EventArgs) Handles btn_Register.Click#
        Me.Hide()
        Register_FRM.ShowDialog()
    End Sub
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LabelSignin.Parent = PictureBoxFoodBackground
        LabelUser.Parent = PictureBoxFoodBackground
        LabelPass.Parent = PictureBoxFoodBackground
        LinkLabelForgotPass.Parent = PictureBoxFoodBackground
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelForgotPass.LinkClicked
        If textBox_User.Text = "" Then 'Checks if the username textbox is empty 
            MsgBox("Please enter your username only and click hyperlink again") 'output a message to the user to give them guidance
        Else
            EmailPassword() 'calls the EmailPassword Sub
        End If
    End Sub
    Sub EmailPassword()
        Try 'detects an error, if an error is thrown
            SMTP.EnableSsl = True 'encryption initialised first before sending message
            With EmailMessage

                .Subject = "Recovery password" 'subject of the mail
                Dim MysqlConn As New MySqlConnection("server=127.0.0.1;userid=root;password=root;database=projectz") 'database credentials
                MysqlConn.Open() 'opens the connection
                COMMAND = New MySqlCommand("Select Password,Email from projectz.userinfo where Username='" & textBox_User.Text & "'", MysqlConn)
                'grabs the users password from database
                READER = COMMAND.ExecuteReader 'executes the query according to condition
                While READER.Read 'loop through the database
                    .To.Add(READER.GetString("Email")) 'appends the users email when registered to mail
                    .From = New MailAddress(READER.GetString("Email")) 'Where the email was sent from
                    .Body = "Your password is: " & READER.GetString("Password") & ",Your Welcome!" 'the body of the mail
                End While
                MysqlConn.Close() 'close the connection
            End With
            With SMTP
                .Port = 587 ' this port enables system to connect to gmail server
                .Credentials = New System.Net.NetworkCredential(ProjectEmail, ProjectEmailPassword) 'username and password of sender mail
                System.Threading.Thread.Sleep(250) 'slows the system down before sending mail, otherwise may crash
                .Send(EmailMessage)
            End With
            MessageBox.Show("Password sent!" & vbCrLf & "PLease check your email") 'message box output if succesful
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class
