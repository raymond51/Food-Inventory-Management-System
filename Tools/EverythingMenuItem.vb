Imports System.Windows.Forms.MenuItem
Public Class EverythingMenuItem
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
Public Class AttackOnTitan
    Inherits MenuItem


    Public Sub New()
        Text = "Call For Assistance u Noob"
    End Sub

    Private Sub ExitmenuItem_Click(sender As Object, e As System.EventArgs) Handles Me.Click
        My.Computer.Audio.Play(My.Resources.MEDIC_wav, AudioPlayMode.Background)
    End Sub
End Class