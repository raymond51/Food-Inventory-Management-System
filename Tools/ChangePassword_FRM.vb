Public Class ChangePassword_FRM

    Private Sub ButtonSavePassword_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSavePassword.Click
        If TextBoxConfirmNewPass.Text = TextBoxNewPass.Text And (TextBoxConfirmNewPass.Text & TextBoxNewPass.Text & TextBoxNewPass.Text) IsNot "" Then
            GenericSQLSub("update projectz.userinfo set  Password='" & TextBoxConfirmNewPass.Text & "' where Password='" & TextBoxCurrentPassword.Text & "'") ' a reused SQL sub which updates the users new password according to their previous password
            MessageBox.Show("Successfully changed password", "Updated")
        Else
            MessageBox.Show("New password and Confirm password do not match", "Not Match")
        End If
    End Sub
End Class