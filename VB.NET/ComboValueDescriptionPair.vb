Public Class ComboValueDescriptionPair
    Public Value As Integer
    Public Description As String

    Public Sub New(ByVal NewValue As Integer, ByVal NewDescription As String)
        Value = NewValue
        Description = NewDescription
    End Sub

    Public Overrides Function ToString() As String
        Return Description
    End Function
End Class
