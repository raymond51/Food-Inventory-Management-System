Imports MySql.Data.MySqlClient
Public Class FRM_Sub_InsertFood
    Dim tempFreezable As String ' holds a value if food is freezable
    Private Sub btn_SAVE_Click(sender As System.Object, e As System.EventArgs) Handles btn_SAVE.Click
        If CDate(DateTimePickerExpiry.Text) > Now And txtbox_Foodname.Text IsNot "" Then ' checks if expiry date is a future date
            DetermineFreezable() 'sub which determines which tells us if food is freezable or not
            Try 'detects an error if any
                MysqlConn.Open()
                COMMAND = New MySqlCommand("insert into projectz." & UserAccess.CallUniqueUserID & " (Foodname,Stock,Expiry,FoodType,Freezable,StartDateExpiry,NumofExpiry) values ('" & txtbox_Foodname.Text & "','" & NumericUpDownSTOCK.Value & "','" & DateTimePickerExpiry.Text & "','" & ComboBoxFoodType.Text & "','" & tempFreezable & "','" & FormattedCurrentDate & "',0)", MysqlConn) 'takes 2 arguments: query string & mysqlconn object
                READER = COMMAND.ExecuteReader 'execute this command and read the data from database
                MessageBox.Show("Data saved") 'tells user that food has been inserted
                MysqlConn.Close() 'close database connection
                Me.Close() 'close the form 
            Catch ex As MySqlException
                MessageBox.Show("Unable to Save data", "Failed") 'output a error message
            Finally
                MysqlConn.Dispose()
            End Try
            MainInventory.ResetTable() 'once food inserted refresh the inventory table and reset dataset
        Else
            MessageBox.Show("Please enter a date in the future/Complete all fields with valid details", "Invalid Date/Incomplete")
        End If
    End Sub

    Private Sub DetermineFreezable()
        Select Case True 'determines which radio button has been checked
            Case RBY.Checked
                tempFreezable = "Y"
            Case RBN.Checked
                tempFreezable = "N"
        End Select
    End Sub
End Class