Imports System.Data
Imports WebApiLivros.Domain.Entities
Imports WebApiLivros.Domain.Models

Namespace WebApiLivros.Domain.Interfaces
    Public Interface ILivroDataAccess
        Sub Remove(cod As Integer)
        Sub Save(livro As Livro)
        Function GetAll() As List(Of Livro)
        Function GetByCod(cod As Integer) As Livro
        Function GetList() As List(Of LivroListModel)
        Function GetReportData() As DataTable
    End Interface
End Namespace
