Namespace WebApiLivros.Domain.Models
    Public Class LivroListModel

        Public Sub New(cod As Integer, titulo As String)
            Me.Cod = cod
            Me.Titulo = titulo
        End Sub

        Public Property Cod As Integer

        Public Property Titulo As String

    End Class
End Namespace
