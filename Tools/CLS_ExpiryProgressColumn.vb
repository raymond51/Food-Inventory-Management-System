Public Class CLS_ExpiryProgressColumn
    Inherits DataGridViewColumn 'inherits the library used to manipulate value in cell
    Public Sub New() 'constructor
        MyBase.New(New ProgressCell()) 'creates a new instance of a base class with reference of the progress cell
    End Sub

    Public Overrides Property CellTemplate() As DataGridViewCell ' reuse the name of elements while changing what happens
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal Value As DataGridViewCell)
            ' Ensure that the cell used for the template is a ProgressCell.
            If Value IsNot Nothing And Not TypeOf (Value) Is ProgressCell Then
                Throw New InvalidCastException("Must be a ProgressCell")
            End If
            MyBase.CellTemplate = Value
        End Set
    End Property 'property provides a get and setter
End Class

Public Class ProgressCell
    Inherits DataGridViewImageCell
    Protected Overrides Function GetFormattedValue(ByVal value As Object, ByVal rowIndex As Integer, ByRef cellStyle As System.Windows.Forms.DataGridViewCellStyle, ByVal valueTypeConverter As System.ComponentModel.TypeConverter, ByVal formattedValueTypeConverter As System.ComponentModel.TypeConverter, ByVal context As System.Windows.Forms.DataGridViewDataErrorContexts) As Object 'takes several parameter
        'byref uses the original value of the variable, the original value of the variable changes when this sub is called
        Dim bmp As Bitmap = New Bitmap(Me.Size.Width, Me.Size.Height)  ' Create bitmap.
        Using g As Graphics = Graphics.FromImage(bmp)
            Dim percentage As Double = 0
            Double.TryParse(Me.Value.ToString(), percentage) 'converts the value in the expiry progress column into a double 
            Dim text As String
            If percentage >= 100 Then
                text = "100 %" 'if value in the expiry progress percentage column is greater than 100, cap the value to 100 & 
            Else
                text = Math.Round(percentage, 1, MidpointRounding.AwayFromZero).ToString() + " %" 'round the percentage 
            End If
            Dim textExpired As String = "Expired" 'variable holds text expired which is displayed when expiry progress is = 100%
            Dim f As Font = New Font("Verdana", 10, FontStyle.Regular)
            Dim w As Integer = CType(g.MeasureString(text, f).Width, Integer) ' Get width and height of text.
            Dim h As Integer = CType(g.MeasureString(text, f).Height, Integer)
            ' Draw pile.
            g.DrawRectangle(Pens.Black, 2, 2, Me.Size.Width - 6, Me.Size.Height - 6)
            If percentage >= 100 Then
                g.FillRectangle(Brushes.Red, 3, 3, CInt((Me.Size.Width - 6) * percentage / 100), CInt(Me.Size.Height - 7)) 'draws a filled rectangle in the cell, this looks similar to a progress bar
                Dim rect As RectangleF = New RectangleF(0, 0, bmp.Width, bmp.Height)
                Dim sf As StringFormat = New StringFormat() 'enable manipulate of strings
                sf.Alignment = StringAlignment.Center 'allign text in centre of cell
                g.DrawString(textExpired, f, Brushes.Black, rect, sf) 'draw text into the expiry progress percentage column 
            Else
                g.FillRectangle(Brushes.Aqua, 3, 3, CInt((Me.Size.Width - 6) * percentage / 100), CInt(Me.Size.Height - 7))
                ' -6 to ensure that the drawn progress bar fits within the datagridview cell
                Dim rect As RectangleF = New RectangleF(0, 0, bmp.Width, bmp.Height)
                Dim sf As StringFormat = New StringFormat()
                sf.Alignment = StringAlignment.Center
                g.DrawString(text, f, Brushes.Red, rect, sf)
            End If
        End Using
        Return bmp
    End Function
End Class

