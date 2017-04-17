Namespace OptionsUser
    Public Class Size
        Enum sizes
            S = 6
            M = 8
            L = 12
            'make is .tostring will convert it into the letter
        End Enum
    End Class

    Public Class DetermineCSVLine
        Enum OptionNo
            TextSize = 0
            EmailNotification = 1
            BalloonTip = 2
            sound = 3
        End Enum
    End Class
End Namespace