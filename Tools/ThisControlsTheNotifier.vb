Imports System.Windows.Forms.MenuItem
Public Class ThisControlsTheNotifier


    Inherits MenuItem

End Class


Public Class ExitmenuItem
    Inherits MenuItem


    Public Sub New()
        Text = "Exit"
    End Sub

    Private Sub ExitmenuItem_Click(sender As Object, e As System.EventArgs) Handles Me.Click
        Application.Exit()
    End Sub
End Class

Public Class Signout
    Inherits MenuItem


    Public Sub New()
        Text = "Signout"""
    End Sub

    Private Sub ExitmenuItem_Click(sender As Object, e As System.EventArgs) Handles Me.Click
        MainInventory.WindowState = FormWindowState.Minimized
        Toolbar.Close()
        Login_FRM.textBox_Pass.Clear()
        Login_FRM.textBox_User.Clear()
        Login_FRM.ShowDialog()
    End Sub

End Class

