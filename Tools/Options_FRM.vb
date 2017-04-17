Public Class Options_FRM
    Dim inc As Integer = 0
    Private Sub btn_Custom_Click(sender As System.Object, e As System.EventArgs) Handles btn_Custom.Click
        If myfont.ShowDialog = DialogResult.OK Then 'shows a font dialog which the user can select and if they submit
            MainInventory.Font = myfont.Font ' the value they chose will be set on the main form
        End If
    End Sub

    Private Sub CheckBoxEmailNotification_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxEmailNotification.CheckedChanged
        If CheckBoxEmailNotification.Checked = True Then 'if the user has checked the checkbox
            UpdateChanges(CInt(OptionsUser.DetermineCSVLine.OptionNo.EmailNotification), "Y") 'pass these parameters to sub UpdateChanges
            'converts enum value to int writes the Y in the options.txt
            '
        Else
            UpdateChanges(CInt(OptionsUser.DetermineCSVLine.OptionNo.EmailNotification), "N")
        End If
    End Sub

    Sub UpdateChanges(ByVal LineIndex As Integer, ByVal Value As String)
        Dim lines() As String = System.IO.File.ReadAllLines("Options.txt")
        lines(LineIndex) = Value 'writes the value at a certain line index
        System.IO.File.WriteAllLines(currPath & "\Options.txt", lines) 'write the new values into the option.txt file 
        SaveChangesToUserAccesClass() 'calls the sub
    End Sub
    Public Sub SaveChangesToUserAccesClass()
        'This sub is also loaded when system loads
        Dim reader As New System.IO.StreamReader("Options.txt") 'reads the option.txt file in the debug folder
        Dim allLines As List(Of String) = New List(Of String) 'alllines holds a list of strings
        Do While Not reader.EndOfStream 'continue to loop until end of stream
            allLines.Add(reader.ReadLine()) 'add values read to list of strings
        Loop
        reader.Close() 'close the reader 
        If ReadLine(CInt(OptionsUser.DetermineCSVLine.OptionNo.EmailNotification), allLines) = "Y" Then
            UserAccess.EmailEnabled = True 'set the variable in 'userAccess class to true
        Else
            UserAccess.EmailEnabled = False
        End If

        If ReadLine(CInt(OptionsUser.DetermineCSVLine.OptionNo.BalloonTip), allLines) = "Y" Then
            UserAccess.BallonTipEnabled = True
        Else
            UserAccess.BallonTipEnabled = False
        End If
        If ReadLine(CInt(OptionsUser.DetermineCSVLine.OptionNo.sound), allLines) = "Y" Then
            UserAccess.soundEnabled = True
        Else
            UserAccess.soundEnabled = False
        End If
        If inc >= 1 Then
            ' MainInventory.Font = New Font("Arial", CDec(ReadLine(CInt(OptionsUser.DetermineCSVLine.OptionNo.TextSize), allLines))) 'set the font size of main inventory form according to user selection
        End If
    End Sub
    Public Function ReadLine(lineNumber As Integer, lines As List(Of String)) As String
        Return lines(lineNumber) 'return the value in the list according the the line index
    End Function
    Private Sub CheckBoxBalloonTip_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxBalloonTip.CheckedChanged
        If CheckBoxBalloonTip.Checked = True Then
            UpdateChanges(CInt(OptionsUser.DetermineCSVLine.OptionNo.BalloonTip), "Y") 'pass these parameters to sub UpdateChanges
        Else
            UpdateChanges(CInt(OptionsUser.DetermineCSVLine.OptionNo.BalloonTip), "N")
        End If
    End Sub

    Private Sub CheckBoxSound_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxSound.CheckedChanged
        If CheckBoxSound.Checked = True Then
            UpdateChanges(CInt(OptionsUser.DetermineCSVLine.OptionNo.sound), "Y") 'pass these parameters to sub UpdateChanges
        Else
            UpdateChanges(CInt(OptionsUser.DetermineCSVLine.OptionNo.sound), "N")
        End If
    End Sub

    Private Sub RadioButtonSmall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonSmall.CheckedChanged
        If RadioButtonSmall.Checked = True Then
            inc += 1
            UpdateChanges(CInt(OptionsUser.DetermineCSVLine.OptionNo.TextSize), CInt(OptionsUser.Size.sizes.S)) 'pass these parameters to sub UpdateChanges
        End If
    End Sub

    Private Sub RadioButtonMed_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonMed.CheckedChanged
        If RadioButtonMed.Checked = True Then
            inc += 1
            UpdateChanges(CDec(OptionsUser.DetermineCSVLine.OptionNo.TextSize), CDec(OptionsUser.Size.sizes.M)) 'pass these parameters to sub UpdateChanges
        End If
    End Sub

    Private Sub RadioButtonLarge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonLarge.CheckedChanged
        If RadioButtonLarge.Checked = True Then
            inc += 1
            UpdateChanges(CInt(OptionsUser.DetermineCSVLine.OptionNo.TextSize), CInt(OptionsUser.Size.sizes.L)) 'pass these parameters to sub UpdateChanges
        End If
    End Sub
End Class