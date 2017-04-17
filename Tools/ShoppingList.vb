Imports MySql.Data.MySqlClient
Public Class ShoppingList
    Private Sub btn_printpreview_Click(sender As System.Object, e As System.EventArgs) Handles btn_PrintPreview.Click
        PrintPreview.ShowDialog() 'show the print preview dialog
    End Sub

    Private Sub PrintShoppingList_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintShoppingList.PrintPage
         Dim ypos As Integer = 150 ' initialise where first drawn rectangle will appear
        e.Graphics.DrawString("Shopping List", fontTitle, System.Drawing.Brushes.Black, PixelsSize(0), PixelsSize(1)) 'draw title
        e.Graphics.DrawString("Things to buy:", fontSubTitle, System.Drawing.Brushes.Black, PixelsSize(2), PixelsSize(3)) 'draw sub title
        e.Graphics.DrawString("Check:", fontSubTitle, System.Drawing.Brushes.Black, PixelsSize(4), PixelsSize(3)) 'draw checkbox title
        For Each item As String In lstBox_ShoppingList.Items 'loops through the shoppingList
            e.Graphics.DrawString(item, fontFoodName, System.Drawing.Brushes.Black, PixelsSize(2), ypos) 'draw the name of the food in ShoppingList in print preview
            e.Graphics.DrawRectangle(Pens.Blue, PixelsSize(4), ypos, PixelsSize(1), PixelsSize(1)) 'draw a rectangular box corrosponding to food in shoppinglist
            ypos += 40 'increment the y position
        Next
    End Sub

    Private Sub btn_Print_Click(sender As System.Object, e As System.EventArgs) Handles btn_Print.Click
        PrintShoppingList.Print() 'printes the shopping list
    End Sub

    Private Sub LoadFoodNAMES()
        For Each item As DataGridViewRow In MainInventory.DataGridViewInventory.Rows 'loops through each food name in main inventory 
            If Not item.IsNewRow Then 'checks if no a new row
                lstBox_ShoppingSearch.Items.Add(item.Cells(1).Value.ToString) 'fill the shopping Search Listbox with foodnames from inventory
            End If
        Next
    End Sub
 
    Private Sub ShoppingList_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadFoodNAMES() 'when form loads, load this sub
    End Sub

    Private Sub txtBox_shoppingSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBox_shoppingSearch.TextChanged
        Dim words As String
        For a = 0 To lstBox_ShoppingSearch.Items.Count - 1 ' correct index offset, loops through the shopping Search listbox
            words = lstBox_ShoppingSearch.Items.Item(a) ' assign variable word equal to user input
            If InStr(words.ToLower, txtBox_shoppingSearch.Text.ToLower) Then 'if user input (foodname) exists in the shopping search listbox
                lstBox_ShoppingSearch.SelectedItem = words ' select the item in shopping search listbox according to text in search engine 
            End If
        Next
    End Sub

    Private Sub ButtonAddFood_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAddFood.Click
        If lstBox_ShoppingSearch.SelectedIndex <> -1 And lstBox_ShoppingList.Items.Count < 20 Then 'if an item has been selected in shoppingList Listbox
            lstBox_ShoppingList.Items.Add(lstBox_ShoppingSearch.SelectedItem) 'add an item from the shoppingSearch to the shopping list
            txtBox_shoppingSearch.Text = "" 'refresh the search engine
        Else
            MsgBox("Please select a food!")
        End If
    End Sub

    Private Sub btn_RemoveItem_Click(sender As System.Object, e As System.EventArgs) Handles btn_RemoveItem.Click
        If lstBox_ShoppingList.SelectedIndex <> -1 Then 'if an item has been selected in shoppingList Listbox
            lstBox_ShoppingList.Items.RemoveAt(lstBox_ShoppingList.SelectedIndex) 'clear a single item in the shoppingList listbox at selected index
        Else
            MsgBox("Choose a food to remove!")
        End If
    End Sub

    Private Sub btn_clearall_Click(sender As System.Object, e As System.EventArgs) Handles btn_clearall.Click
        lstBox_ShoppingList.Items.Clear() 'clear all item in the shoppingList listbox
    End Sub
End Class


