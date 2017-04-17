Imports System.Math
Public Class FRM_ManageBudget
    Private Sub btnCalculateExpenses_Click(sender As System.Object, e As System.EventArgs) Handles btnCalculateExpenses.Click
        Try
            If ComboBoxPeriodType.SelectedItem = "Month" Then
                MessageBox.Show("Approximate recommended spendage per " & ComboBoxPeriodType.SelectedItem & " is £" & Round((CInt(TxtBoxBudgetLeft.Text) / CInt(TxtBoxMonthLeft.Text)))) 'round to the nearest pound recommeneded spendage each month
            ElseIf ComboBoxPeriodType.SelectedItem = "Week" Then
                MessageBox.Show("Approximate recommended spendage per " & ComboBoxPeriodType.SelectedItem & " is £" & Round((CInt(TxtBoxBudgetLeft.Text) / CInt(TxtBoxWeeksLeft.Text)))) 'round to the nearest pound recommeneded spendage each week
            ElseIf ComboBoxPeriodType.SelectedItem = "Day" Then
                MessageBox.Show("Approximate recommended spendage per " & ComboBoxPeriodType.SelectedItem & " is £" & Round((CInt(TxtBoxBudgetLeft.Text) / (CInt(TxtBoxWeeksLeft.Text) * 5)))) 'round to the nearest pound recommeneded spendage each day (times 5 due to only 5 school days)
            End If
        Catch ex As Exception
            MessageBox.Show("Please ensure that numbers are entered and form complete!", "Error")
        End Try
    End Sub

    Private Sub ComboBoxPeriodType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxPeriodType.SelectedIndexChanged
        If ComboBoxPeriodType.SelectedItem = "Month" Then
            TxtBoxMonthLeft.Enabled = True
            TxtBoxWeeksLeft.Enabled = False
        ElseIf ComboBoxPeriodType.SelectedItem = "Week" Or ComboBoxPeriodType.SelectedItem = "Day" Then
            TxtBoxMonthLeft.Enabled = False
            TxtBoxWeeksLeft.Enabled = True
        End If
    End Sub
End Class