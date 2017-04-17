Namespace Foods 'organise the food category
    Public Class FoodNutrition
        Public FoodName As String
        Public Property Carbs As String 'A Property is similar to a Function. With a getter and a setter, it controls access to a value. This value is called a backing store.
        Public Property Calories As String
        Public Property Fat As String
        Public Property Protein As String
        Public Property Amount As String
        'food variables
        Public Sub New(ByVal Food As String, ByVal AmountInGrams As String, ByVal AmountCalories As String, ByVal AmountCarbs As String, ByVal AmountFat As String, ByVal AmountProtein As String) 'constructor
            FoodName = Food
            Carbs = AmountCarbs
            Calories = AmountCalories
            Fat = AmountFat
            Protein = AmountProtein
            Amount = AmountInGrams
        End Sub
    End Class
    Namespace FoodType 'organise the types of food
        Public Class Fruits 'individual class
            Inherits FoodNutrition 'inherits all the methods and variables of food nutrition class
            Public Sub New(ByVal Food As String, ByVal AmountInGrams As String, ByVal AmountCalories As String, ByVal AmountCarbs As String, ByVal AmountFat As String, ByVal AmountProtein As String) 'takes several food variable parameters
                MyBase.New(Food, AmountInGrams, AmountCalories, AmountCarbs, AmountFat, AmountProtein) 'virtual function needs to call its parent’s version
            End Sub
        End Class
        Public Class Protein
            Inherits FoodNutrition
            Public Sub New(ByVal Food As String, ByVal AmountInGrams As String, ByVal AmountCalories As String, ByVal AmountCarbs As String, ByVal AmountFat As String, ByVal AmountProtein As String)
                MyBase.New(Food, AmountInGrams, AmountCalories, AmountCarbs, AmountFat, AmountProtein)
            End Sub
        End Class
        Public Class Dairy
            Inherits FoodNutrition
            Public Sub New(ByVal Food As String, ByVal AmountInGrams As String, ByVal AmountCalories As String, ByVal AmountCarbs As String, ByVal AmountFat As String, ByVal AmountProtein As String)
                MyBase.New(Food, AmountInGrams, AmountCalories, AmountCarbs, AmountFat, AmountProtein) 'virtual function needs to call its parent’s version
            End Sub
        End Class
        Public Class Vegetable
            Inherits FoodNutrition
            Public Sub New(ByVal Food As String, ByVal AmountInGrams As String, ByVal AmountCalories As String, ByVal AmountCarbs As String, ByVal AmountFat As String, ByVal AmountProtein As String)
                MyBase.New(Food, AmountInGrams, AmountCalories, AmountCarbs, AmountFat, AmountProtein)
            End Sub
        End Class
    End Namespace
End Namespace

