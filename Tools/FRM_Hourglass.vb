Imports System.Net.Mail
Public Class FRM_Hourglass
    Private Sub FRM_Hourglass_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        DataGridView1.Rows.Clear() 'when expiry hourglass is loaded, previous items are cleared
        InsertValuesIntoExpiryTable() 'called sub
    End Sub

    Sub LoadAll()
        Dim col As New CLS_ExpiryProgressColumn 'the expiryhourglass percentage column assigned with function
        DataGridView1.Columns.Add("Foodname", "Food")
        DataGridView1.Columns.Add(col) 'add the column
        DataGridView1.AllowUserToAddRows = False 'do not let users add values
        AddRemoveButton() 'called sub to add addition columns 
    End Sub

    Sub AddRemoveButton() 'creates new button columns so that user can interact
        InsertValuesIntoExpiryTable()
        Dim btn As New DataGridViewButtonColumn() 'new button column
        DataGridView1.Columns.Add(btn)
        btn.HeaderText = "Click Data"
        btn.Text = "Consumed" ' if user has consumed food it is not wasted
        btn.Name = "btn"
        btn.UseColumnTextForButtonValue = True

        Dim btn2 As New DataGridViewButtonColumn()
        DataGridView1.Columns.Add(btn2)
        btn2.HeaderText = "Click Data"
        btn2.Text = "Add to Wastage" 'if the expiry hourglass percentage is 100% ("Expired") then added as wasted
        btn2.Name = "btn"
        btn2.UseColumnTextForButtonValue = True
    End Sub
    Public Sub InsertValuesIntoExpiryTable()
        With MainInventory
            For itemDate = 0 To .DataGridViewInventory.Rows.Count - 1 'loops through the main inventory (datagridview in main form)

                If Not IsDBNull(.DataGridViewInventory.Item(3, itemDate).Value) Then
                    Dim temp As Date = .DataGridViewInventory.Item(3, itemDate).Value
                    DataGridView1.Rows.Add(.DataGridViewInventory.Item(1, itemDate).Value, calcDateLength(temp, .DataGridViewInventory.Item(7, itemDate).Value)) 'adds the food with expiry to expiry hourglass (Datagridview)

                End If
            Next
            DataGridView1.Rows.RemoveAt(DataGridView1.Rows.Count - 1) 'a duplicate item generated at last index
        End With
        NotfiyUser() ' starts the sub
    End Sub

    Sub NotfiyUser()
        If UserAccess.EmailEnabled = True Then 'checks if the email configuration is enabled
            Dim flagExpiry As Boolean = False ' initialse the flagExpiry variable, default value is false
            For i = 0 To DataGridView1.Rows.Count - 1
                If (DataGridView1.Item(1, i).Value() >= 80) And (DataGridView1.Item(1, i).Value <= 99) Then 'checks if the value in the cell is between 80 and 99
                    flagExpiry = True 'flagExpiry set to true, used later to show that an food is about to expire
                    TimerControl.Start() 'The button ExpiryHourglass in toolbar form blinks red when food almost about to expire
                    'does not notify the user when food has expired
                End If
            Next
            If flagExpiry = True Then '
                sendNotification() ' a sub which sends a email to user about food expiry
            End If
        End If
    End Sub
    Sub sendNotification()
        Try 'detects a error if any
            SMTP.EnableSsl = True 'encryption initialised first before sending message
            With EmailMessage
                .From = New MailAddress("nonReplyTastyFood@gmail.com") 'default from sender
                .Subject = "Food almost expired!" ' subject of the mail
                .To.Add(UserAccess.CallEmail.ToString) 'sends the message to the user email
                .Body = "Please check your expiry hourglass a food is about to expire!" 'notify user that food about to expire (this is the body of the mail)
            End With
            With SMTP
                .Port = 587 'gmail port
                .Credentials = New System.Net.NetworkCredential(ProjectEmail, ProjectEmailPassword) 'email credentials
                System.Threading.Thread.Sleep(250) 'used to prevent program from crashing, this function slows down cpu
                'bottleneck
                .Send(EmailMessage) 'sends the actual message
            End With
        Catch ex As Exception
            MessageBox.Show("Error ouccured when sending notification", "Sending Error")
        End Try

    End Sub

    Function calcDateLength(ByVal ExpiryDate, ByVal StartExpiryDate) 'takes expiry date and the date when the food was assigned an expiry date
        Dim DateStart As Date = StartExpiryDate 'assign start date to variable
        Dim CurrentDate As Date = DateTime.Now.ToLongDateString() 'grabs the current date
        Dim DateExpiry As Date = ExpiryDate 'assign expiry date to variable
        Return CType(DateDiff("d", DateStart, CurrentDate), Integer) / CType(DateDiff("d", DateStart, DateExpiry), Integer) * 100 'compares the ratio
        'gets integer difference of start date and current date and divides it by the integer difference of start date and expiry date 
    End Function

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = 3 Then 'determines if the third column in datagridview (Expiry hourglass) has been clicked
            If DataGridView1.Item(1, e.RowIndex).Value() >= 100 Then
                GenericSQLSub("UPDATE Projectz." & UserAccess.CallUniqueUserID & " set Expiry=null,StartDateExpiry= null, NumofExpiry=NumofExpiry+1 where Foodname='" & DataGridView1.Item(0, e.RowIndex).Value & "'") 'Remove the date in the form2 datagridview
                'ADD TO WASTED
                MainInventory.ResetTable()  'Refresh Main form datagridview
                InsertValuesIntoExpiryTable() 'refresh the datagridview in expiryhourglass
            Else
                MessageBox.Show("You cannot remove item at row: " & e.RowIndex.ToString & ", as food has not expired yet.", "Expiry Progress")
            End If
        End If
        If e.ColumnIndex = 2 Then 'determines if the second column in datagridview (Expiry hourglass) has been clicked
            Dim intResponse As Integer
            intResponse = MsgBox("Did you consume the food before it has expired?", _
            vbYesNo + vbQuestion, _
            "Consumed") ' loads a message box with yes/no answer buttons
            If intResponse = vbYes Then 'if the user has clicked yes
                GenericSQLSub("UPDATE Projectz." & UserAccess.CallUniqueUserID & " set Expiry=null,StartDateExpiry= null where Foodname='" & DataGridView1.Item(0, e.RowIndex).Value & "'") 'Remove the date in the form2 datagridview, but does not add to wasted
                MainInventory.ResetTable() 'Refresh Main form datagridview
                InsertValuesIntoExpiryTable() 'refresh the datagridview in expiryhourglass
            End If
        End If
    End Sub

    Private Sub TimerControl_Tick(sender As System.Object, e As System.EventArgs) Handles TimerControl.Tick
        With Toolbar
            'alternates back color of the button each tick
            If .btn_Hourglass.BackColor = System.Drawing.SystemColors.Control Then
                .btn_Hourglass.BackColor = Color.LightPink
            ElseIf .btn_Hourglass.BackColor = Color.LightPink Then
                .btn_Hourglass.BackColor = System.Drawing.SystemColors.Control
            End If
        End With
    End Sub
End Class