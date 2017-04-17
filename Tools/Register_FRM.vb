Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.IO 'system input / output
Imports System.Reflection 'provide data about the different objects, and functionality to perform action on those objects

Public Class Register_FRM
    'Moving the form variables
    Private IsFormBeingDragged As Boolean = False
    Private MouseDwnY As Integer
    Private MouseDwnX As Integer
    'Register variables
    Dim passW As Boolean = False
    Dim Cpass As Boolean = False
    Dim FirstnameLengthSufficient As Boolean = False
    Dim sur As Boolean = False
    Dim checkedConditionsAndTerms As Boolean = False
    Dim YearValid As Boolean = False
    Dim accountType As String = Nothing
    Dim RegisterGender As String

    Private Sub RegisterForm_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            IsFormBeingDragged = True
            MouseDwnX = e.X 'current x postion of mouse
            MouseDwnY = e.Y 'current y postion of mouse
        End If
    End Sub

    Private Sub RegisterForm_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            IsFormBeingDragged = False
        End If
    End Sub

    Private Sub RegisterForm_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If IsFormBeingDragged = True Then
            Dim tmp As Point = New Point
            tmp.X = Me.Location.X + (e.X - MouseDwnX) 'move form in x direction
            tmp.Y = Me.Location.Y + (e.Y - MouseDwnY) 'move form in y direction
            Me.Location = tmp 'assign the form to that location
            tmp = Nothing 'reset 
        End If
    End Sub

    Private Sub HelpPassword(ByVal text As String) 'accepts a error text as parameter e.g pass needs uppercase
        passW = False 'set valid password to false
        txt_PasswordR.BackColor = Color.LightPink 'red indicator
        Label9.Text = text 'label changes to tell validation required
    End Sub


    Private Sub txt_PasswordR_TextChanged(sender As System.Object, e As System.EventArgs) Handles txt_PasswordR.TextChanged
        If txt_PasswordR.Text.Contains("123") Then
            HelpPassword("You're password contains 123")
        ElseIf txt_PasswordR.Text.Contains("abc") Then
            HelpPassword("You're password contains abc")
        ElseIf txt_PasswordR.Text.Length < 7 Then
            HelpPassword("Minimum 7 charecters needed")
        ElseIf txt_PasswordR.Text.Contains(txt_Firstname.Text) Then
            HelpPassword("Pass cannot contain firstname")
        ElseIf txt_PasswordR.Text.Contains(txt_Surname.Text) Then
            HelpPassword("Pass cannot contain surname")
        ElseIf Lower.Matches(txt_PasswordR.Text).Count < numRequired Then
            HelpPassword("Need at least 1 lowercase")
        ElseIf Upper.Matches(txt_PasswordR.Text).Count < numRequired Then
            HelpPassword("Need at least 1 Uppercase")
        ElseIf Special.Matches(txt_PasswordR.Text).Count < numRequired Then
            HelpPassword("Need any 1 special Char")
        Else
            passW = True
            txt_PasswordR.BackColor = Color.White
            Label9.Text = ""
        End If
    End Sub


    Private Sub txt_Firstname_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txt_Firstname.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) _
                  Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            e.Handled = True
        End If

        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub txt_Surname_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txt_Surname.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) _
                Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            e.Handled = True
        End If

        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub

    Private Sub txt_Confirmpassword_TextChanged(sender As System.Object, e As System.EventArgs) Handles txt_Confirmpassword.TextChanged
        If txt_Confirmpassword.Text = txt_PasswordR.Text Then
            Label10.Text = ""
            txt_Confirmpassword.BackColor = Color.White
            Cpass = True
        Else
            txt_Confirmpassword.BackColor = Color.LightPink
            Cpass = False
            Label10.Text = "Password does not match"
        End If
    End Sub

    Private Sub CheckBoxConditions_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxConditionsAndTerms.CheckedChanged
        If CheckBoxConditionsAndTerms.Checked Then
            checkedConditionsAndTerms = True
            CheckBoxConditionsAndTerms.BackColor = Color.White
        Else
            CheckBoxConditionsAndTerms.BackColor = Color.LightPink
            checkedConditionsAndTerms = False
        End If
    End Sub

    Private Sub txt_Email_Click(sender As System.Object, e As System.EventArgs) Handles txt_Email.Click
        txt_Email.Text = "" 'removes the example email
        txt_Email.TextAlign = HorizontalAlignment.Left 'allows the user to write from left to right
    End Sub

    Private Sub DetermineGender()
        Select Case True
            Case RadioButtonMale.Checked
                RegisterGender = "Male"
            Case RadioButtonFemale.Checked
                RegisterGender = "Female"
        End Select
    End Sub
    Private Sub DetermineAccountType()
        Select Case True
            Case RadioButtonBronze.Checked
                Me.BackColor = Color.Sienna
                accountType = "Basic"
            Case RadioButtonSilver.Checked
                Me.BackColor = Color.Silver
                accountType = "Advance"
        End Select
    End Sub
    Private Sub RadioButtonBronze_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonBronze.CheckedChanged
        DetermineAccountType()
    End Sub

    Private Sub RadioButtonSilver_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonSilver.CheckedChanged
        DetermineAccountType()
    End Sub

    Private Sub btn_submit_Click(sender As System.Object, e As System.EventArgs) Handles btn_submit.Click
        If checkedConditionsAndTerms And Cpass = True And txt_Firstname.Text IsNot "" And txt_Surname IsNot "" Then
            'execute this block of code if condtions are met e.g check if firstname is not empty
            Dim MysqlConn As MySqlConnection = New MySqlConnection
            MysqlConn.ConnectionString = "server=127.0.0.1;userid=root;password=root;database=Projectz" 'database credentials
            DetermineGender() 'sub which determines which radio button checked for gender
            GeneratedUniqueUserID = RandomStringGenerator.ToString 'randomStringGenerator is a function which generates a random 15 charecter string
            Try 'detects an error 
                MysqlConn.Open() ' opens connection with database
                Dim command As MySqlCommand
                Dim Reader As MySqlDataReader
                command = New MySqlCommand("insert into Projectz.Userinfo (Firstname,Surname,Birthday,Email,Username,Password,Gender,Country,AccountType,UniqueUserID,Emailnotification) values ('" & txt_Firstname.Text & "','" & txt_Surname.Text & "','" & DateTimePickerUserDOB.Text & "','" & txt_Email.Text & "','" & txtbox_Username.Text & "','" & txt_Confirmpassword.Text & "','" & RegisterGender & "','" & ComboBoxCountry.Text & "','" & accountType & "','" & GeneratedUniqueUserID.ToLower & "','Y')", MysqlConn)
                'insert all information provided by the user into the database
                Reader = command.ExecuteReader 'executes the query
                MessageBox.Show("Registered, now you can login", "Completed")
                MysqlConn.Close() 'close database connection
                GenerateNewTable() ' a sub which creates a new inventory table for user
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                MysqlConn.Dispose()
            End Try
        Else 'if condition not met
            MsgBox("Please complete form", , "Not Completed")
        End If
    End Sub

    Private Sub GenerateNewTable()
        Dim MysqlConn As MySqlConnection = New MySqlConnection 'create a new connection
        MysqlConn.ConnectionString = "server=127.0.0.1;userid=root;password=root;database=Projectz" 'database credentials
        Dim command As MySqlCommand
        Dim Reader As MySqlDataReader
        Try 'detects an error 
            MysqlConn.Open() ' opens database connection
            command = New MySqlCommand("CREATE TABLE " & GeneratedUniqueUserID & " (ID int NOT NULL AUTO_INCREMENT PRIMARY KEY, Foodname varchar(20),Stock int(5),Expiry date,FoodType varchar(20),Freezable varchar(1),Image longblob,StartDateExpiry date,NumofExpiry int(5))", MysqlConn)
            'The SQL Database has been set to autoincrement to the next ID
            Reader = command.ExecuteReader
            MysqlConn.Close() 'close the database connection
            InsertDefaultFoodItem() 'A sub which will insert all common food data into the newly generated table 
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Private Sub InsertDefaultFoodItem()
        Dim lstStr() As String   'loads csv into an array
        Dim itemsList As List(Of String) 'holds a list of string 
        Dim executableLocation As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        lstStr = File.ReadAllLines(currPath & "\DefaultFoodItems.csv") 'read the excel file in debug folder which contains default foods
        Dim cnn As MySqlConnection
        cnn = New MySqlConnection("server=127.0.0.1;userid=root;password=root;database=projectz") 'database credentials
        Dim cmd As MySqlCommand
        '@@@@@@@@@@
        cnn.Open() ' opens the connection 
        For i = 0 To lstStr.Count - 1 'correct index offset and determines how many line default food excel spreadsheet has
            itemsList = (From s In lstStr(i).Split(",") Select s).ToList 'separate the split data into records
            Dim FoodIMAGE As Bitmap
            Dim ms1 As New MemoryStream
            Dim ImgLocation As String = Path.Combine(executableLocation, itemsList(3)) 'locaation of the image of food 
            FoodIMAGE = New Bitmap(ImgLocation) 'save the image format as bitmap
            FoodIMAGE.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg) 'saves the bitmap into memory stream
            Dim content As Byte() = ms1.ToArray() 'convert the image into a string of bytes
            Try 'detect error
                cmd = New MySqlCommand("insert " & GeneratedUniqueUserID & " SET Image= @Image,Foodname=@Foodname,FoodType=@FoodType,Freezable=@Freezable,NumofExpiry=@NumofExpiry", cnn)
                cmd.Parameters.AddWithValue("@Image", content) 'inserts the image from folder into database image in byte format
                cmd.Parameters.AddWithValue("@Foodname", itemsList(0)) 'insert name of food into database from e.g egg
                cmd.Parameters.AddWithValue("@FoodType", itemsList(1)) 'inserts the type of food e.g protein
                cmd.Parameters.AddWithValue("@Freezable", itemsList(2))
                cmd.Parameters.AddWithValue("@NumofExpiry", 0) ' insert default number of expiry
                cmd.ExecuteNonQuery() 'execute the query
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Next 'loop until contion met
        cnn.Close() 'close database connection
        Me.Hide() 'hide the register form
        Login_FRM.ShowDialog() 'load the login form
    End Sub

    Private Function RandomStringGenerator()
        Dim StringBank As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        '35 long charecter bank of string
        Dim randomNo As New Random
        'class which enables random numbers to be generated
        Dim sb As New StringBuilder
        'string builder enables charecters to be added to existing string
        For i As Integer = 1 To 15 ' loop 15 times 
            Dim idx As Integer = randomNo.Next(0, 35)
            'generates a random number from 0 to 35
            sb.Append(StringBank.Substring(idx, 1))
            'adds a random charecter from stringBank to the string builder
        Next
        Return sb 'sends the 15 random charecter long string to where it was called 
    End Function

    Private Sub txtbox_Username_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles txtbox_Username.MouseClick
        txtbox_Username.Clear()
    End Sub

    Private Sub CheckBoxcondition_MouseHover(sender As System.Object, e As System.EventArgs) Handles CheckBoxConditionsAndTerms.MouseHover
        Me.ToolTipAssist.SetToolTip(Me.CheckBoxConditionsAndTerms, "The terms and conditions document includes the following provisions:" & vbCrLf & vbCrLf & "The terms and conditions document includes the following provisions:" & vbCrLf & ">a licence of the copyright" & vbCrLf & ">a disclaimer of liability" & vbCrLf & ">a clause governing the use of passwords" & vbCrLf & ">an acceptable use clause" & vbCrLf & ">a variation clause" & vbCrLf & ">an entire agreement clause" & vbCrLf & ">a clause specifying the applicable law and the jurisdiction in which disputes will be decided" & vbCrLf & ">a provision specifying some of the information which needs to be disclosed under the Ecommerce Regulations") ' terms and condition
    End Sub
End Class