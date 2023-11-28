Namespace WebApiLivros.Domain.Entities
    Public Class Livro

        Public Sub New(cod As Integer, titulo As String, editora As String, edicao As Integer, anoPublicacao As String, preco As Decimal, autores As List(Of String), assunto As List(Of String))
            Me.Cod = cod
            Me.Titulo = titulo
            Me.Editora = editora
            Me.Edicao = edicao
            Me.AnoPublicacao = anoPublicacao
            Me.Preco = preco
            Me.Autores = autores
            Me.Assuntos = assunto
        End Sub

        Public Sub New()

        End Sub

        Public Property Cod As Integer

        Public Property Titulo As String

        Public Property Editora As String

        Public Property Edicao As Integer

        Public Property AnoPublicacao As String

        Public Property Preco As Decimal

        Public Property Autores As List(Of String) = New List(Of String)

        Public Property Assuntos As List(Of String) = New List(Of String)

    End Class
End Namespace