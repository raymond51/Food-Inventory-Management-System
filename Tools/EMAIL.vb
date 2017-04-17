Imports System.Net.Mail 'enable us to use the mail library 
Imports System.IO
Imports System.Threading 'enables us to control the sequential flow of data in processor
Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel 'we can use Excel spreadsheets and data
Imports System.Reflection 'an activator class, which creates an instance of a class method in runtime


Public Class EMAIL

    Private Sub txtbox_EmailTO_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtbox_EmailTO.TextChanged
        emailto = txtbox_EmailTO.Text
    End Sub
    Private Sub txtbox_EmailSUBJECT_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtbox_EmailSUBJECT.TextChanged
        EmailSUBJECT = txtbox_EmailSUBJECT.Text
    End Sub
    Dim executableLocation As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
    Dim GeneratedInventoryLocation As String = Path.Combine(executableLocation, "vbexcel.xlsx") 'grabs location of the generated user stock excel file
    Private Sub btn_EmailSEND_Click(sender As System.Object, e As System.EventArgs) Handles btn_EmailSEND.Click
        FontName = "arial" 'default font style
        FontSize = 10 'default message font size


        Try 'detecs an error if any
            SMTP.EnableSsl = True 'enable encryption first
            With EmailMessage
                .From = New MailAddress(UserAccess.CallEmail) 'assign the users email as from email
                .To.Add(EmailTO) 'sends email to user input recipient
                .Subject = EmailSUBJECT 'subject of mail
                .Body = txtBox_EmailBODYMESSAGE.Text 'message written by user
                If CheckBox_AddFoodStock.Checked = True Then 'if the check box has been checked then add food stock attachment
                    .Attachments.Add(New Attachment(GeneratedInventoryLocation)) ' make it a excel file
                End If
                If AttachedFileExists = True Then 'if has selected another attachment 
                    .Attachments.Add(New Attachment(OpenFileDialogEmailAttach.FileName.ToString())) 'attach file 
                End If
            End With
            With SMTP
                .Port = 587 'gmail port
                .Credentials = New System.Net.NetworkCredential(ProjectEmail, ProjectEmailPassword) 'email credientials
                System.Threading.Thread.Sleep(250) 'reduce processor speed temporarily
                'bottleneck
                .Send(EmailMessage) 'an function which sends the actual message to target email
            End With
            MessageBox.Show("Message sent!" & vbCrLf & "Congratulations!")
        Catch ex As Exception
            MessageBox.Show("Please ensure all fields are correct and filled. Error: " & vbCrLf & ex.Message, "Sending Error")
        End Try
    End Sub

    Private Sub EMAIL_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        InitialiseTextBoxFont()
        FillToolStripFontStyleComboBox()
    End Sub

    Private Sub btn_Clear_Click(sender As System.Object, e As System.EventArgs) Handles btn_Clear.Click
        txtBox_EmailBODYMESSAGE.Clear()
        txtbox_EmailSUBJECT.Clear()
        txtbox_EmailTO.Clear()
    End Sub

    Private Sub FillToolStripFontStyleComboBox() 'add all font style for user to use in the font tool strip
        For Each fnt In FontFamily.Families
            ToolStripAllFonts.Items.Add(fnt.Name)
        Next fnt
    End Sub

    Private Sub InitialiseTextBoxFont() 'set default font of body (message)
        txtBox_EmailBODYMESSAGE.Font = New Font(FontName, FontSize, FontStyle)
    End Sub

    Private Sub ToolStripAllFonts_TextChanged(sender As System.Object, e As System.EventArgs) Handles ToolStripAllFonts.TextChanged
        FontName = ToolStripAllFonts.Text 'user selected a font 
        InitialiseTextBoxFont() 'set the mail of the font to user selection
    End Sub

    Private Sub ToolStripAttachNewFile_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripAttachNewFile.Click
        OpenFileDialogEmailAttach.Title = "Select a file to attach to the email:" 'title of the dialog
        OpenFileDialogEmailAttach.ShowDialog() 'shows the attachment dialog so that user can select a file to attach
    End Sub

    Private Sub OpenFileDialogEmailAttach_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialogEmailAttach.FileOk
        If My.Computer.FileSystem.FileExists(OpenFileDialogEmailAttach.FileName) Then 'if file path exists
            stream = OpenFileDialogEmailAttach.OpenFile()
            LabelAttachedFile.Text = "Attached File: " & OpenFileDialogEmailAttach.FileName.ToString() '
            'hold the file directory of that file
            If Not (stream Is Nothing) Then
                AttachedFileExists = True
                stream.Close()
                MessageBox.Show("File successfully added!")
            End If
        End If
    End Sub

    Private Sub ToolStripRemoveFile_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripRemoveFile.Click
        AttachedFileExists = False
        LabelAttachedFile.Text = "Attached File: None"
    End Sub

    Private Sub IncreaseFontSizeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles IncreaseFontSizeToolStripMenuItem.Click
        FontSize = FontSize + 1 'increase the size of font 
        InitialiseTextBoxFont()
    End Sub

    Private Sub DecreaseFontSizeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DecreaseFontSizeToolStripMenuItem.Click
        FontSize = FontSize - 1 'decrease the size of font 
        InitialiseTextBoxFont()
    End Sub

    Private Sub ToolStripFontStyleEditor_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripFontStyleEditor.Click
        If myfont.ShowDialog = DialogResult.OK Then 'user selects the font they choose
            txtBox_EmailBODYMESSAGE.Font = myfont.Font 'assign the textbox to use that font
        End If
    End Sub

    Private Sub CheckBox_AddFoodStock_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox_AddFoodStock.CheckedChanged
        Dim cnn As MySqlConnection
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook 'need this to edit excel worksheet
        Dim xlWorkSheet As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        'New excel variables
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("sheet1") 'apply the inventory to sheet one of excel file
        cnn = New MySqlConnection("server=127.0.0.1;userid=root;password=root;database=projectz") 'database credentials
        cnn.Open() ' open the database connection
        Dim dscmd As New MySqlDataAdapter("SELECT * FROM  projectz." & UserAccess.CallUniqueUserID & "", cnn)
        Dim ds As New DataSet
        dscmd.Fill(ds) 'fill dataset with the data from query
        For i = 0 To ds.Tables(0).Columns.Count - 1
            xlWorkSheet.Cells(1, i + 1) = ds.Tables(0).Columns(i).ToString() 'write the title in the x direction
        Next
        Dim znum As Integer = 0
        For r = 0 To ds.Tables(0).Rows.Count - 1
            For z = 0 To ds.Tables(0).Columns.Count - 1
                xlWorkSheet.Cells(znum + 2, z + 1) = ds.Tables(0).Rows(znum).Item(z).ToString() 'write in the y column or each row
            Next
            znum = znum + 1 'increment to next column and repeat
        Next
        xlWorkSheet.SaveAs(GeneratedInventoryLocation)  'Save the user stock excel file in the debug folder

        xlWorkBook.Close() 'close the excel work book
        xlApp.Quit() 'close excel application

        releaseObject(xlApp) 'dispose excel objects
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
        cnn.Close() 'close the database connection
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj) 'drop references excel makes when closing
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect() 'release unreferenced objects from memory
        End Try
    End Sub
End Class

