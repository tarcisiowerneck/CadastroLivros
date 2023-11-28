Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports WebApiLivros.Domain.Entities
Imports WebApiLivros.Domain.Interfaces
Imports WebApiLivros.Domain.Models
Imports WebApiLivros.Infra.Converter
Imports WebApiLivros.Infra.Repository

Namespace WebApiLivros.Service
    Public Class LivrosService
        Implements IServiceLivros
        Private ReadOnly _repositoryLivros As LivroDataAccess

        Public Sub New(connection As String)
            _repositoryLivros = New LivroDataAccess(connection)
        End Sub

        Public Function RecoverAll() As IEnumerable(Of LivroListModel) Implements IServiceLivros.RecoverAll
            Dim livros = _repositoryLivros.GetList()
            Return livros.ToArray()
        End Function

        Public Function RecoverByCod(cod As Integer) As LivroModel Implements IServiceLivros.RecoverByCod
            Dim livro = _repositoryLivros.GetByCod(cod)

            If livro Is Nothing Then Return Nothing

            Return livro.ConvertToLivro()
        End Function

        Public Sub Delete(cod As Integer) Implements IServiceLivros.Delete
            _repositoryLivros.Remove(cod)
        End Sub

        Public Function Save(livroModel As LivroModel) As LivroModel Implements IServiceLivros.Save
            Dim livro = livroModel.ConvertToLivroEntity()

            _repositoryLivros.Save(livro)
            Return livro.ConvertToLivro()
        End Function

        Public Function GetReportData() As DataTable Implements IServiceLivros.GetReportData
            Return _repositoryLivros.GetReportData()
        End Function
    End Class
End Namespace
