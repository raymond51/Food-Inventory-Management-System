Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.DataVisualization.Charting 'class which enables us to use chart function
Public Class Wasted
    Private Sub btn_LoadChart_Click(sender As System.Object, e As System.EventArgs) Handles btn_LoadChart.Click 'upon form load
        ChartFoods.Series("Foodname vs Wasted").Points.Clear() 'clears the chart points 
        ChartFoodType.Series("FoodType_VS_NumofExpiry").Points.Clear() 'clears the chart values
        Try 'detects an error if any
            MysqlConn.Open() 'opens the database connection
            COMMAND = New MySqlCommand("select Foodname,numofexpiry,foodType from Projectz." & UserAccess.CallUniqueUserID & " where numofexpiry > 0", MysqlConn)
            READER = COMMAND.ExecuteReader 'executes the query
            While READER.Read 'reads through the database
                ChartFoods.Series("Foodname vs Wasted").Points.AddXY(READER.GetString("Foodname"), READER.GetInt32("numofexpiry")) 'sets points on the bar chart
                ChartFoodType.Series("FoodType_VS_NumofExpiry").Points.AddXY(READER.GetString("foodType"), READER.GetInt32("numofexpiry")) 'sets the percentage of food
            End While
            MysqlConn.Close() 'close the database connection 
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

        Me.ChartFoodType.Series(0)("PieLabelStyle") = "Outside" 'labels outside of pie chart
        Me.ChartFoodType.Series(0).BorderWidth = 1
        Me.ChartFoodType.Series(0).BorderColor = System.Drawing.Color.FromArgb(26, 59, 105)
        Me.ChartFoodType.Legends(0).Enabled = True
        Me.ChartFoodType.Legends(0).Docking = Docking.Bottom 'docks the keys at the bottom of chart
        Me.ChartFoodType.Legends(0).Alignment = System.Drawing.StringAlignment.Center
        Me.ChartFoodType.Series(0).LegendText = "#PERCENT{P2}" ' illustrate the wasted in percentage 
        Me.ChartFoodType.DataManipulator.Sort(PointSortOrder.Descending, ChartFoodType.Series(0))
    End Sub
End Class