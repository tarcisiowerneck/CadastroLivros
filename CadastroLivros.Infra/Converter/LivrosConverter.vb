Imports System.Collections.Generic
Imports System.Linq
Imports WebApiLivros.Domain.Entities
Imports WebApiLivros.Domain.Models
Imports System.Runtime.CompilerServices

Namespace WebApiLivros.Infra.Converter
    Public Module LivrosConverter
        <Extension()>
        Public Function ConvertToLivroEntity(livroModel As LivroModel) As Domain.Entities.Livro
            Return New Livro(livroModel.Cod, livroModel.Titulo, livroModel.Editora, livroModel.Edicao, livroModel.AnoPublicacao, livroModel.Preco, livroModel.Autores, livroModel.Assuntos)
        End Function

        <Extension()>
        Public Function ConvertToLivros(livro As IList(Of Domain.Entities.Livro)) As IEnumerable(Of LivroModel)
            Return New List(Of LivroModel)(livro.[Select](Function(l) New LivroModel(l.Cod, l.Titulo, l.Editora, l.Edicao, l.AnoPublicacao, l.Preco, l.Autores, l.Assuntos)))
        End Function

        <Extension()>
        Public Function ConvertToLivro(livro As Domain.Entities.Livro) As LivroModel
            Return New LivroModel(livro.Cod, livro.Titulo, livro.Editora, livro.Edicao, livro.AnoPublicacao, livro.Preco, livro.Autores, livro.Assuntos)
        End Function
    End Module
End Namespace
