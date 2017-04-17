Imports System.IO 'access input and output class
Public Class Nutrition_FRM
    Public Sub InitialiseNutrition() 'this sub is intialised when the form loads, this is initalised during loading form 
        getContents() 'calls sub
        DisplayList(listFruits, listProtein, listDairy, listVegetable) 'calls sub, displays a collection of food category 
    End Sub

    Private Sub initialiseListViewColumn(ByVal Name As String, ByVal ConstantSize As Integer) 'takes 2 parameters value of title and the size of column
        ListViewNutritionFruits.Columns.Add(Name, ConstantSize, HorizontalAlignment.Center)
        ListViewNutritionProtein.Columns.Add(Name, ConstantSize, HorizontalAlignment.Center)
        ListViewNutritionDairy.Columns.Add(Name, ConstantSize, HorizontalAlignment.Center)
        ListViewNutritionVegetable.Columns.Add(Name, ConstantSize, HorizontalAlignment.Center)
        'titles need to come first before storing values into the listview 
    End Sub
    Public Sub getContents()
        Dim lstStr() As String   'loads csv into an array
        Dim itemsList As List(Of String) 'this is a collection of resizable array
        lstStr = File.ReadAllLines(currPath & "\FoodNutrientSheet.csv") 'currpath is a global varaible which 
        'splits items by comma
        For i = 0 To lstStr.Count - 1 'correct database offset, loops through excel database
            itemsList = (From s In lstStr(i).Split(",") Select s).ToList 'separate the split data into records
            Add(listFruits, itemsList(0), itemsList(1), itemsList(2), itemsList(3), itemsList(4), itemsList(5)) 'numbers in bracket represent the column which should be read from the food nutrition excel sheet in the debug folder
            AddProtein(listProtein, itemsList(6), itemsList(7), itemsList(8), itemsList(9), itemsList(10), itemsList(11))
            AddDairy(listDairy, itemsList(12), itemsList(13), itemsList(14), itemsList(15), itemsList(16), itemsList(17))
            AddVegetable(listVegetable, itemsList(18), itemsList(19), itemsList(20), itemsList(21), itemsList(22), itemsList(23))
        Next
        For i = 0 To 5 ' loops a certain  amount of times
            itemsList = (From s In lstStr(0).Split(",") Select s).ToList
            initialiseListViewColumn(itemsList(i), Sizes) 'sets the title of the column in the listview
        Next
    End Sub
    Sub Add(ByVal genericList As List(Of Foods.FoodType.Fruits),
           ByVal Foodname As String, ByVal amount As String, ByVal calories As String, ByVal carbs As String, ByVal fat As String, ByVal protein As String)
        genericList.Add(New Foods.FoodType.Fruits(Foodname, amount, calories, carbs, fat, protein)) 'adds these items to collection array
    End Sub
    Sub AddProtein(ByVal genericList As List(Of Foods.FoodType.Protein), ByVal Foodname As String, ByVal amount As String, ByVal calories As String, ByVal carbs As String, ByVal fat As String, ByVal protein As String)

        genericList.Add(New Foods.FoodType.Protein(Foodname, amount, calories, carbs, fat, protein)) 'adds these items to collection array
    End Sub
    Sub AddDairy(ByVal genericList As List(Of Foods.FoodType.Dairy), ByVal Foodname As String, ByVal amount As String, ByVal calories As String, ByVal carbs As String, ByVal fat As String, ByVal protein As String)

        genericList.Add(New Foods.FoodType.Dairy(Foodname, amount, calories, carbs, fat, protein)) 'adds these items to collection array
    End Sub
    Sub AddVegetable(ByVal genericList As List(Of Foods.FoodType.Vegetable), ByVal Foodname As String, ByVal amount As String, ByVal calories As String, ByVal carbs As String, ByVal fat As String, ByVal protein As String)

        genericList.Add(New Foods.FoodType.Vegetable(Foodname, amount, calories, carbs, fat, protein)) 'adds these items to collection array
    End Sub
    Public Sub DisplayList(ByVal ListF As IEnumerable(Of Foods.FoodType.Fruits), ByVal ListP As IEnumerable(Of Foods.FoodType.Protein), ByVal ListD As IEnumerable(Of Foods.FoodType.Dairy), ByVal ListV As IEnumerable(Of Foods.FoodType.Vegetable)) 'An IEnumerable collection contains elements
        For Each i As Foods.FoodType.Fruits In ListF
            ListViewNutritionFruits.Items.Add(New ListViewItem({i.FoodName, i.Amount, i.Calories, i.Carbs, i.Fat, i.Protein})) 'adds items in collection to the list view
        Next
        ListViewNutritionFruits.Items.RemoveAt(0)
        'Removes the titles 
        For Each i As Foods.FoodType.Protein In ListP
            ListViewNutritionProtein.Items.Add(New ListViewItem({i.FoodName, i.Amount, i.Calories, i.Carbs, i.Fat, i.Protein}))
        Next
        ListViewNutritionProtein.Items.RemoveAt(0) 'Removes the titles 
        For Each i As Foods.FoodType.Dairy In ListD

            ListViewNutritionDairy.Items.Add(New ListViewItem({i.FoodName, i.Amount, i.Calories, i.Carbs, i.Fat, i.Protein}))
        Next
        ListViewNutritionDairy.Items.RemoveAt(0) 'Removes the titles 
        For Each i As Foods.FoodType.Vegetable In ListV
            ListViewNutritionVegetable.Items.Add(New ListViewItem({i.FoodName, i.Amount, i.Calories, i.Carbs, i.Fat, i.Protein}))
        Next
        ListViewNutritionVegetable.Items.RemoveAt(0) 'Removes the titles 
    End Sub
End Class