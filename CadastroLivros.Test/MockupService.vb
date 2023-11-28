Imports System.Collections.Generic
Imports System.Linq
Imports WebApiLivros.Domain.Entities
Imports WebApiLivros.Domain.Interfaces
Imports WebApiLivros.Domain.Models
Imports WebApiLivros.Infra.Converter

Namespace WebApiLivros.Test
    Public Class MockupService
        Implements IServiceLivros
        Private ReadOnly _repositoryLivros As List(Of Domain.Entities.Livro)

        Public Sub New(ByVal repositoryLivros As List(Of Domain.Entities.Livro))
            _repositoryLivros = repositoryLivros
        End Sub

        Public Function RecoverAll() As IEnumerable(Of LivroModel) Implements IServiceLivros.RecoverAll
            Dim livro = _repositoryLivros
            Return livro.ConvertToLivro()
        End Function

        Public Function RecoverById(ByVal id As Integer) As LivroModel Implements IServiceLivros.RecoverById
            Dim livro = _repositoryLivros.Where(Function(p) p.Id.Equals(id)).[Select](Function(p) p).First()

            If livro Is Nothing Then Return Nothing

            Return livro.ConvertToLivro()
        End Function

        Public Sub Delete(ByVal id As Integer) Implements IServiceLivros.Delete
            _repositoryLivros.Remove(_repositoryLivros.Where(Function(p) p.Id.Equals(id)).[Select](Function(p) p).First())
        End Sub

        Public Function Insert(ByVal livroModel As CreateProdutosModel) As LivroModel Implements IServiceLivros.Insert
            Dim livro = livroModel.ConvertToProdutosEntity()

            _repositoryLivros.Add(livro)
            Return livro.ConvertToProdutos()
        End Function


        Public Function Update(ByVal id As Integer, ByVal livroModel As UpdateProdutosModel) As LivroModel Implements IServiceLivros.Update
            If id <> livroModel.Id Then Return Nothing

            Dim livro = livroModel.ConvertToProdutosEntity()
            _repositoryLivros.Remove(_repositoryLivros.Where(Function(p) p.Id.Equals(id)).[Select](Function(p) p).First())
            _repositoryLivros.Add(livro)
            Return livro.ConvertToProdutos()
        End Function
    End Class
End Namespace
