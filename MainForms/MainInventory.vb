Imports MySql.Data.MySqlClient
Imports System.IO

Public Class MainInventory
    'main inventory variables
    Public ToolbarShown As Boolean = True
    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If UserAccess.soundEnabled = True Then 'only play audio if configuration for audio enabled
            My.Computer.Audio.Play(My.Resources.Welcome, AudioPlayMode.Background) 'play a audio ("Welcome")
        End If
        Toolbar.Show() 'display toolbar form
        Toolbar.Location = New Point(Me.Location.X - 200, Me.Location.Y) 'load the toolbar 200 pixels to the left of the maininventory form
        TimerDate.Start()
        LoadTable()
        VariableStorage.notifierLOAD()
    End Sub

    Public Sub ResetTable()
        dbDataSet.Clear() 'clears dataset
        LoadTable() 'loads updated inventory into new dataset
    End Sub

    Public Sub LoadTable()
        dbDataSet.Clear() 'clear the datagridveiw dataset
        Try 'detects an error
            MysqlConn.Open() ' opens the database connection
            COMMAND = New MySqlCommand("Select id,Foodname,Stock,Expiry,FoodType,Freezable,Image,StartDateExpiry from " & UserAccess.CallUniqueUserID, MysqlConn)
            SDA.SelectCommand = COMMAND 'perform query
            SDA.Fill(dbDataSet) ' 'fill all the data from query into the dataset
            bSource.DataSource = dbDataSet   'bind dataset and datagridview
            DataGridViewInventory.DataSource = bSource 'set the datasource of datagridview to bind dataset
            SDA.Update(dbDataSet) 'update the mysql data adapter
            MysqlConn.Close() 'close the database connection

            For i = 0 To DataGridViewInventory.ColumnCount - 1 'correct column count offset
                If TypeOf DataGridViewInventory.Columns(i) Is DataGridViewImageColumn Then
                    DirectCast(DataGridViewInventory.Columns(i), DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Stretch
                    'if a image column has been found then set the image to stretch (means that the image can be resized)
                End If
            Next
            For i = 0 To DataGridViewInventory.RowCount - 1 'correct the row offset, counts how many rows there are
                DataGridViewInventory.Rows(i).Height = 65 'set the row height 
            Next
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub

    Private Sub txt_search_TextChanged(sender As System.Object, e As System.EventArgs) Handles txt_search.TextChanged
        Dim DV As New DataView(dbDataSet) 'takes dataset as parameter
        DV.RowFilter = String.Format("Foodname Like'%" & txt_search.Text & "%'", txt_search.Text) 'search all the names you write in search textbox
        DataGridViewInventory.DataSource = DV  'transfer all the serach into datagrid
    End Sub

    Private Sub Form2_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Do you want to really want to exit?", "EXIT", MessageBoxButtons.YesNo) 'stores user response
        If dialog = DialogResult.No Then 'if user has pressed no
            e.Cancel = True
            'stops the closing event of the form 
            'nothing will happen
        Else
            Application.ExitThread()   'closes all the threads attachted to the form
        End If
    End Sub

    Private Sub btn_toolbar_Click(sender As System.Object, e As System.EventArgs) Handles btn_toolbar.Click
        If ToolbarShown Then
            Toolbar.Hide() 'hides the toolbar form
            btn_toolbar.Text = "<"
            ToolbarShown = False
        ElseIf ToolbarShown = False Then
            Toolbar.Show()
            ToolbarShown = True
            btn_toolbar.Text = ">"
        End If
    End Sub
    Private Sub Form2_Move(sender As Object, e As System.EventArgs) Handles Me.Move
        Toolbar.Location = New Point(Me.Location.X - 200, Me.Location.Y) 'moves the toolbar 200 pixels to the left of the maininventory form
    End Sub

    Private Sub IconNotifyThis_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles IconNotifyThis.MouseDoubleClick
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Show() 'if notifier has been clicked then display the main inventory to normal size
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub MainInventory_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then 'if the maininventory has been minimised
            Me.Hide() ' place inventory in background task
            Toolbar.Hide()
            ToolbarShown = False
        End If
    End Sub

    Private Sub btn_InsertFood_Click(sender As System.Object, e As System.EventArgs) Handles btn_InsertFood.Click
        FRM_Sub_InsertFood.ShowDialog() ' show the InsertFood form
    End Sub

    Private Sub btn_AddStockandExpiry_Click(sender As System.Object, e As System.EventArgs) Handles btn_AddStockandExpiry.Click
        FRM_Sub_AddStockAndExpiry.txt_FoodName.Text = DataGridViewInventory.Item(1, RowIndexClicked).Value
        FRM_Sub_AddStockAndExpiry.ShowDialog() 'show the AddStockAndExpiryForm
    End Sub

    Private Sub btn_DeleteFoods_Click(sender As System.Object, e As System.EventArgs) Handles btn_DeleteFoods.Click
        Dim result As Integer = MessageBox.Show("Are you sure you want to delete this food?", "Cancel?", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Try
                MysqlConn.Open() 'opens connection
                COMMAND = New MySqlCommand("Delete from projectz." & UserAccess.CallUniqueUserID & " where Foodname ='" & DataGridViewInventory.Item(1, RowIndexClicked).Value & "'", MysqlConn) 'takes 2 arguments: query string & mysqlconn object
                'deletes the food where the user has selected the row index in the inventory datagridview
                READER = COMMAND.ExecuteReader 'execute this command and read the data from database
                MessageBox.Show("Data deleted") 'tells the user that this food has been deleted
                MysqlConn.Close()
            Catch ex As MySqlException
                MsgBox(ex.Message)
            Finally
                MysqlConn.Dispose()  'similar to .close but object state will be reset 
            End Try
            Me.ResetTable() 'a sub which refreshes the table
        End If
    End Sub

    Private Sub PBRefresh_Click(sender As System.Object, e As System.EventArgs) Handles PBRefresh.Click
        LoadTable() 'reloads table
    End Sub

    Private Sub TimerDate_Tick(sender As System.Object, e As System.EventArgs) Handles TimerDate.Tick
        ToolStripLabelDate.Text = DateTime.Now.ToLongDateString 'shows the current date to user in the toolstrip
        ToolStripLabelTime.Text = DateTime.Now.ToLongTimeString() 'shows the current time to user in the toolstrip
    End Sub

    Private Sub btn_PrintInvetory_Click(sender As System.Object, e As System.EventArgs) Handles btn_PrintInvetory.Click
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog() 'shows the docuemnt as preview
    End Sub


    Private Sub PrintDocument1_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim mRow As Integer = 0
        Dim newpage As Boolean = True
        With DataGridViewInventory
            Dim fmt As StringFormat = New StringFormat(StringFormatFlags.LineLimit)
            fmt.LineAlignment = StringAlignment.Center
            fmt.Trimming = StringTrimming.EllipsisCharacter
            Dim y As Single = e.MarginBounds.Top
            Do While mRow < .RowCount
                Dim row As DataGridViewRow = .Rows(mRow)
                Dim x As Single = e.MarginBounds.Left
                Dim h As Single = 0
                For Each cell As DataGridViewCell In row.Cells
                    Dim rc As RectangleF = New RectangleF(x, y, cell.Size.Width, cell.Size.Height) 'declaring a rectangle with datagridview size
                    e.Graphics.DrawRectangle(Pens.Black, rc.Left, rc.Top, rc.Width, rc.Height) 'draws a rectangle according the the size of the cell in the inventory datagridview
                    If (newpage) Then
                        e.Graphics.DrawString(DataGridViewInventory.Columns(cell.ColumnIndex).HeaderText, .Font, Brushes.Black, rc, fmt) 'draws the column along with its header in black paint brush
                    Else
                        e.Graphics.DrawString(DataGridViewInventory.Rows(cell.RowIndex).Cells(cell.ColumnIndex).FormattedValue.ToString(), .Font, Brushes.Black, rc, fmt) 'draws the text of what the cell contains
                    End If
                    x += rc.Width 'increment the width of the rectangle
                    h = Math.Max(h, rc.Height)
                Next
                newpage = False
                y += h
                mRow += 1
                If y + h > e.MarginBounds.Bottom Then
                    e.HasMorePages = True
                    mRow -= 1
                    newpage = True
                    Exit Sub
                End If
            Loop
            mRow = 0
        End With
    End Sub

    Private Sub DataGridViewInventory_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewInventory.CellClick
        RowIndexClicked = e.RowIndex 'determines the rowindex which the user has clicked and stores it in variable
    End Sub
End Class