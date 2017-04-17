Imports MySql.Data.MySqlClient
Public Class FRM_Sub_AddStockAndExpiry
    Private Sub btn_UPDATE_Click(sender As System.Object, e As System.EventArgs) Handles btn_UPDATE.Click
        If CDate(DateTimePickerExpiry.Text) > Now And txt_FoodName.Text IsNot "" Then ' checks if expiry date is a future date
            Try 'detects an error if any
                MysqlConn.Open() 'open the database connection
                COMMAND = New MySqlCommand("update projectz." & UserAccess.CallUniqueUserID & " set  Stock='" & NumericUpDownSTOCK.Value & "', Expiry='" & DateTimePickerExpiry.Text & "', StartDateExpiry='" & FormattedCurrentDate & "' where Foodname='" & txt_FoodName.Text & "'", MysqlConn)
                READER = COMMAND.ExecuteReader 'executes the query 
                MessageBox.Show("Data updated")
                MysqlConn.Close() 'close the database connection
                Me.Close() 'close the form
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            Finally
                MysqlConn.Dispose()
            End Try
            MainInventory.ResetTable() 'once food updated refresh the inventory table and reset dataset
        Else
            MessageBox.Show("Please enter a date in the future/Complete all fields with valid details", "Invalid Date/Incomplete")
        End If
    End Sub
End Class