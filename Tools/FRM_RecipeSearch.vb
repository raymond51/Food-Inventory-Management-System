Imports MySql.Data.MySqlClient
Imports System.IO

Public Class FRM_RecipeSearch
    Dim item As ListViewItem 'create an object which will hold the list view item elements
    Private Sub FRM_RecipeSearch_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadRecipes() 'upon form load, this sub is executed
    End Sub
    Sub LoadRecipes()
        ListView1.Clear() 'clear all item so no duplication
        MysqlConn.Open() 'opens the database connection
        COMMAND = New MySqlCommand("Select idRecipeList,RecipeName,Image from projectz.recipelist ", MysqlConn)
        Try 'detects an error if any
            READER = COMMAND.ExecuteReader 'excutes the query
            While READER.Read 'reads data from database 
                Dim imageData As Byte() = DirectCast(READER("Image"), Byte()) 'saves the image as byte, directcase handles the error in conversion
                If (Not imageData Is Nothing) And (imageData.Length > 5) Then 'checks if image data is not empty
                    Using ms As New MemoryStream(imageData, 0, imageData.Length)
                        ms.Write(imageData, 0, imageData.Length) 'writes the bytes into the memory as backing store until all bytes read
                        Dim picture As Image = Image.FromStream(ms) 'converts the stream of bytes into an image
                        ImageList1.Images.Add("" & READER.GetString("idRecipeList") & "", picture) 'using the idRecipeList identifier to name the image
                        item = ListView1.Items.Add(READER.GetString("RecipeName"), "" & READER.GetString("idRecipeList") & "") 'adds an item to listview, so that the user could see what recipes avaliable (with recipe image and name)
                    End Using
                Else
                    ImageList1.Images.Add("" & READER.GetString("idRecipeList") & "", CType(My.Resources.FoodBackground, Image)) 'if there is no image in the database then use the default image in the system resource
                    item = ListView1.Items.Add(READER.GetString("RecipeName"), "" & READER.GetString("idRecipeList") & "") 'adds the item to listview, howver without recipe image but recipe name
                End If
            End While
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub LoadImage()
        Dim pictureData As Byte() = DirectCast(COMMAND.ExecuteScalar(), Byte()) 'saves the image as byte, directcase handles the error in conversion
        Dim picture As Image = Nothing
        Using stream As New IO.MemoryStream(pictureData)
            picture = Image.FromStream(stream)      'Create a stream in memory containing the bytes that comprise the image.'
        End Using
    End Sub

    Sub GrabIngredients() 'sub which retrieves the ingredients
        MysqlConn = New MySqlConnection("server=127.0.0.1;userid=root;password=root;database=projectz")
        MysqlConn.Open() 'opens the database connection
        COMMAND = New MySqlCommand("Select * from projectz.`" & ListView1.SelectedItems(0).Text & "`", MysqlConn)
        Dim bSource2 As New BindingSource 'declare new binding source for different datagridview ()
        Dim SDA2 As New MySqlDataAdapter 'declare new data adapter for different datagridview
        Dim dbDataSet2 As New DataTable 'declare new datatable for different datagridview
        Try
            SDA2.SelectCommand = COMMAND
            SDA2.Fill(dbDataSet2) 'fill the data adapter with datatable
            bSource2.DataSource = dbDataSet2 ' assign data set as datagridview (in recipe) data set
            FRM_Recipe.DataGridViewIngredients.DataSource = bSource2
            SDA2.Update(dbDataSet2) ' updates the dataset
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        MysqlConn.Close() 'close the database connection
    End Sub
 
    Private Sub ButtonDelete_Click(sender As System.Object, e As System.EventArgs) Handles ButtonDelete.Click
        If ListView1.SelectedIndices.Count > 0 Then ' if user has selected a item in listview
            GenericSQLSub("DROP TABLE `" & ListView1.SelectedItems(0).Text & "`") 'delete ingredients table first
            GenericSQLSub("Delete from projectz.recipelist where RecipeName ='" & ListView1.SelectedItems(0).Text & "'") 'then delete the record from database
            LoadRecipes() 'refresh the listview
        Else
            MessageBox.Show("Please select an item")
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedIndices.Count > 0 Then ' if user has selected a item in listview
            MsgBox("Recipe: " & ListView1.SelectedItems(0).Text & " was selected.")
            MysqlConn.Open() 'opens the database connection
            COMMAND = New MySqlCommand("Select * from projectz.recipelist where RecipeName = '" & ListView1.SelectedItems(0).Text & "' ", MysqlConn)
            Try 'detects an error if any
                READER = COMMAND.ExecuteReader 'executes the query
                While READER.Read 'reads the recipe from database
                    With FRM_Recipe
                        .LabelRecipeName.Text = "Recipe Name: " & READER.GetString("RecipeName") 'Assign name of recipe to label
                        .LabelServing.Text = "Serves: " & READER.GetString("ServingSize") 'Assign servingSize of recipe to label
                        .LabelCalories.Text = "Calories: " & READER.GetString("Calories") 'Assign calories of recipe to label
                        .LabelPrepTime.Text = "Preparation Time: " & READER.GetString("PreparationTime") & "mins" 'Assign PrepTime of recipe to label
                        .RichTextBoxInstruction.Text = READER.GetString("Instruction")
                        Dim data As Byte() = DirectCast(READER("Image"), Byte()) 'saves the image as byte, directcase handles the error in conversion
                        Dim ms As New MemoryStream(data)
                        .PictureBoxRecipe.Image = Image.FromStream(ms) 'Create a stream in memory containing the bytes that comprise the image.'
                        .PictureBoxRecipe.SizeMode = PictureBoxSizeMode.StretchImage 'stretch the image, so that the image can auto fit in the picture box
                    End With
                End While
                MysqlConn.Close()
                GrabIngredients() 'a sub which retrieves the ingredients based on which recipe the user has selected 
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Me.Hide() 'hide the recipe search form
        End If
    End Sub
End Class