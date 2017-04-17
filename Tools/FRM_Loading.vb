Public Class FRM_Loading

    Private Sub FRM_Loading_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        PictureBox1.Image = Image.FromFile(currPath & "\Pictures\Loading.gif") 'loading gif loaded from picture folder
        TimerStart.Start() 'start the timer
        InitialiseAllProcess()
    End Sub

    Sub InitialiseAllProcess() 'load all intense task
        Nutrition_FRM.InitialiseNutrition() 'load the nutrition from excel file to listview table
        MainInventory.LoadTable() 'load the user inventory datagridview
        FRM_Hourglass.LoadAll() 'sub which grabs all food in the inventory datagridview with expiry date and stores it in the expiry hourglass
        Options_FRM.SaveChangesToUserAccesClass() 'loads the user configuration from option text file in debug folder
        EnableFeatures() ' determines what account type is the user, if basic account type then disable all features and display only inventory
    End Sub

    Private Sub TimerStart_Tick(sender As System.Object, e As System.EventArgs) Handles TimerStart.Tick
        Me.ProgressBarLoading.Value = Me.ProgressBarLoading.Value + 2 ' increment progressbar value by 2
        If Me.ProgressBarLoading.Value = 100 Then 'upon reaching 100
            TimerStart.Stop() 'stop the timer
            Me.ProgressBarLoading.Value = 0
            Me.Close() 'close the loading form
        End If
    End Sub
    Sub EnableFeatures()
        If UserAccess.CallAccountType = "Basic" Then
            Toolbar.ButtonWasted.Enabled = False
            Toolbar.btn_Recipe.Enabled = False
            Toolbar.btn_ShoppingList.Enabled = False
            Toolbar.btn_Hourglass.Enabled = False
            Toolbar.btn_EMAIL.Enabled = False
            Toolbar.btn_Nutrients.Enabled = False
            Toolbar.btn_Options.Enabled = False
        Else
            Toolbar.ButtonWasted.Enabled = True
            Toolbar.btn_Recipe.Enabled = True
            Toolbar.btn_ShoppingList.Enabled = True
            Toolbar.btn_Hourglass.Enabled = True
            Toolbar.btn_EMAIL.Enabled = True
            Toolbar.btn_Nutrients.Enabled = True
            Toolbar.btn_Options.Enabled = True
        End If
    End Sub
End Class

