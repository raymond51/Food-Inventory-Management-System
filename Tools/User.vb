Public Class User

    'Variables which holds user record
    Private FirstName As String
    Private SurName As String
    Private DateOfBirth As Date
    Private Email As String
    Private Username As String
    Private Gender As String
    Private Country As String
    Private AccountType As String
    Private UniqueUserId As String


    '@@@@@@ Option Variables
    Private EnableEmailNotification As Boolean
    Private EnableBalloonTip As Boolean
    Private EnableSound As Boolean

    Public Property strFirstName As String
        Get
            Return FirstName
        End Get
        Set(ByVal value As String)
            FirstName = value
        End Set
    End Property
    Public Property strSurName As String
        Get
            Return SurName
        End Get
        Set(ByVal value As String)
            SurName = value
        End Set
    End Property
    Public Property dateDOB As Date
        Get
            Return DateOfBirth
        End Get
        Set(ByVal value As Date)
            DateOfBirth = value
        End Set
    End Property
    Public Property CallEmail As String
        Get
            Return Email
        End Get
        Set(ByVal value As String)
            Email = value
        End Set
    End Property
    Public Property GenderType As Char
        Get
            Return Gender
        End Get
        Set(ByVal value As Char)
            Gender = value
        End Set
    End Property

    Public Property CallCountry As String
        Get
            Return Country
        End Get
        Set(ByVal value As String)
            Country = value
        End Set
    End Property
    Public Property CallAccountType As String
        Get
            Return AccountType
        End Get
        Set(ByVal value As String)
            AccountType = value
        End Set
    End Property

    Public Property CallUniqueUserID As String
        Get
            Return UniqueUserId
        End Get
        Set(value As String)
            UniqueUserId = value
        End Set
    End Property
    Public Property CallUsername As String
        Get
            Return Username
        End Get
        Set(value As String)
            Username = value
        End Set
    End Property


    Public Property EmailEnabled As Boolean
        Get
            Return EnableEmailNotification
        End Get
        Set(ByVal value As Boolean)
            EnableEmailNotification = value
        End Set
    End Property
    Public Property BallonTipEnabled As Boolean
        Get
            Return EnableBalloonTip
        End Get
        Set(ByVal value As Boolean)
            EnableBalloonTip = value
        End Set
    End Property
    Public Property soundEnabled As Boolean
        Get
            Return EnableSound
        End Get
        Set(ByVal value As Boolean)
            EnableSound = value
        End Set
    End Property


    Public Overrides Function ToString() As String
        Return "Name: " & strFirstName & " " & strSurName & Environment.NewLine & "Date of Birth: " & dateDOB.ToString & Environment.NewLine & "Email: " &
            CallEmail & Environment.NewLine & "Username: " & CallUsername & Environment.NewLine & "Gender: " & GenderType & Environment.NewLine & "Country: " & CallCountry & Environment.NewLine & "Account-Type: " & CallAccountType
    End Function
End Class
