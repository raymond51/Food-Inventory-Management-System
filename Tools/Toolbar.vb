Public Class Toolbar

    Private Sub ButtonWasted_Click(sender As System.Object, e As System.EventArgs) Handles ButtonWasted.Click
        My.Computer.Audio.Play(My.Resources.Menu_AlarmClick, AudioPlayMode.Background) 'plays a click sounds audio
        Wasted.ShowDialog()
    End Sub

 
    Private Sub btn_signout_Click(sender As System.Object, e As System.EventArgs) Handles btn_signout.Click
        Dim LeaveFormToSignOut As Integer = MessageBox.Show("SignOut?", "Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 'yes/no message box 
        If UserAccess.soundEnabled = True Then 'play an logout audio if audio enabled in configuration
            My.Computer.Audio.Play(My.Resources.Alright, AudioPlayMode.Background)
        End If
        If LeaveFormToSignOut = DialogResult.Yes Then ' if yes has been clicked in yes/no messsage box
            Login_FRM.textBox_Pass.Clear()
            Login_FRM.textBox_User.Clear()
            MainInventory.WindowState = FormWindowState.Minimized 'main form minimised
            Me.Close() ' close the toolbar form
            Login_FRM.Show()
        End If
    End Sub

    Private Sub btn_ShoppingList_Click(sender As System.Object, e As System.EventArgs) Handles btn_ShoppingList.Click
        My.Computer.Audio.Play(My.Resources.Menu_AlarmClick, AudioPlayMode.Background) 'plays a click sounds audio
        ShoppingList.ShowDialog()
    End Sub

  
    Private Sub btn_UserProfile_Click(sender As System.Object, e As System.EventArgs) Handles btn_UserProfile.Click
        My.Computer.Audio.Play(My.Resources.Menu_AlarmClick, AudioPlayMode.Background) 'plays a click sounds audio
        Profile_FRM.ShowDialog()
    End Sub

    Private Sub btn_Recipe_Click(sender As System.Object, e As System.EventArgs) Handles btn_Recipe.Click
        My.Computer.Audio.Play(My.Resources.Menu_AlarmClick, AudioPlayMode.Background) 'plays a click sounds audio
        FRM_Recipe.ShowDialog()
    End Sub

    
    Private Sub btn_EMAIL_Click(sender As System.Object, e As System.EventArgs) Handles btn_EMAIL.Click
        My.Computer.Audio.Play(My.Resources.Menu_AlarmClick, AudioPlayMode.Background) 'plays a click sounds audio
        EMAIL.ShowDialog()
    End Sub

    Private Sub btn_Nutrients_Click(sender As System.Object, e As System.EventArgs) Handles btn_Nutrients.Click
        My.Computer.Audio.Play(My.Resources.Menu_AlarmClick, AudioPlayMode.Background) 'plays a click sounds audio
        Nutrition_FRM.ShowDialog()
    End Sub
  
    Private Sub btn_Options_Click(sender As System.Object, e As System.EventArgs) Handles btn_Options.Click
        Options_FRM.Show()
    End Sub

    Public Sub btn_Hourglass_Click(sender As System.Object, e As System.EventArgs) Handles btn_Hourglass.Click
        My.Computer.Audio.Play(My.Resources.Menu_AlarmClick, AudioPlayMode.Background)  'plays a click sounds audio
        FRM_Hourglass.ShowDialog()
    End Sub

    Private Sub ButtonManageBudget_Click(sender As System.Object, e As System.EventArgs) Handles ButtonManageBudget.Click
        My.Computer.Audio.Play(My.Resources.Menu_AlarmClick, AudioPlayMode.Background)  'plays a click sounds audio
        FRM_ManageBudget.ShowDialog()
    End Sub
End Class