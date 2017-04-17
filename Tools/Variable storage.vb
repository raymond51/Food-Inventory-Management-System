Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Imports System.IO
Module VariableStorage

    'CONNECTION VARIABLES
    Public MysqlConn As New MySqlConnection("server=127.0.0.1;userid=root;password=root;database=projectz")  'holds the database credentials to allow program to connect to the database
    Public COMMAND As New MySqlCommand
    Public dbDataSet As New DataTable
    Public READER As MySqlDataReader

    'INVENTORY VARIABLES
    Public bSource As New BindingSource
    Public SDA As New MySqlDataAdapter

    'REGISTER VARIABLE
    Public GeneratedUniqueUserID As String
    'holds the newly generated user inventory table name from the register form

    'VERIFICATION VARIABLES
    Public Upper As New System.Text.RegularExpressions.Regex("[A-Z]") 'increments if text contains a uppercase
    Public Lower As New System.Text.RegularExpressions.Regex("[a-z]") 'increments if text contains a lowercase
    Public Special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]") 'increments if text contains a special
    Public number As New System.Text.RegularExpressions.Regex("[0-9]") 'increments if text contains a numbers
    Public numRequired As Integer = 1


    'USER class
    Public UserAccess As New User
    'Global variable which enables all forms to access the UserAccess class

    'EMAIL VARIABLES
    Public SMTP As New SmtpClient("smtp.gmail.com") 'connect client with gmail server
    Public EmailMessage As New MailMessage
    Public Const ProjectEmail As String = "MrExpiredFood@gmail.com" 'email credentials (SENDER)
    Public Const ProjectEmailPassword As String = "ilikepizzahut"

    'Current DATE
    Public FormattedCurrentDate = Date.Now.Year & "-" & Date.Now.Month & "-" & Date.Now.Day
    'Formatted date 

    'Email MESSAGE VARIABLES
    Public EmailTO As String
    Public EmailSUBJECT As String
    Public EmailBODYMESSAGE As String

    'FONT FOR TEXT
    Public fontFoodName As New Font("aerial", 16, FontStyle.Regular)
    Public fontTitle As New Font("aerial", 24, Drawing.FontStyle.Bold Or Drawing.FontStyle.Underline)
    Public fontSubTitle As New Font("aerial", 20, FontStyle.Underline)
    'font sizes variations used in shopping list form for print dialog (shopping list design)

    'LISTVIEWNUTRITION COLUMN SIZE
    Public Const Sizes As Integer = 100  'Holds the column size for listview in the nutrition form


    'FONT initialiser for EMAIL Fonts
    Public FontName As String = "aerial"
    Public FontSize As Integer = 10
    Public FontStyle As FontStyle = Drawing.FontStyle.Regular
    Public myfont As New FontDialog
    'default email message fonts


    'NUTRITION VARIABLES
    Public currPath As String = Environment.CurrentDirectory   'Grabs the file path to the debug folder
    Public listFruits As New List(Of Foods.FoodType.Fruits)
    Public listProtein As New List(Of Foods.FoodType.Protein)
    Public listDairy As New List(Of Foods.FoodType.Dairy)
    Public listVegetable As New List(Of Foods.FoodType.Vegetable)

    'IO STREAM 
    Public stream As Stream

    Public AttachedFileExists As Boolean = False
    'determines if the new excel file of the user inventory has been generated and stored in the debug folder

    'Const ARRAY to store Pixel Size
    Public ReadOnly PixelsSize() As Integer = {300, 20, 50, 90, 450}
    'array which holds the postion of text in the shopping list print dialog design

    'CONTEXT MENU
    Public objMenu As New ContextMenu

    'STORE value of cell Click
    Public RowIndexClicked As Integer

    'Generic SQL SUB
    Sub GenericSQLSub(ByVal Text As String)  'a sub which accessible by all forms, performs a task and requires a paramenter(query)
        Try
            MysqlConn.Open()
            COMMAND = New MySqlCommand(Text, MysqlConn)
            READER = COMMAND.ExecuteReader
            '  MessageBox.Show("Task Completed")
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show("Error occured: " & ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    'NOTIFIER
    Public Sub notifierLOAD()
        objMenu.MenuItems.Add(New Signout) 'adds a logout function to the objectmenu
        objMenu.MenuItems.Add(New ExitmenuItem) 'adds a close function to the objectmenu, which closes application
        MainInventory.IconNotifyThis.ContextMenu = objMenu
    End Sub

End Module
