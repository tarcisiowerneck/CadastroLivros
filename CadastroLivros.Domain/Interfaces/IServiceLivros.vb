Imports System.Collections.Generic
Imports System.Data
Imports WebApiLivros.Domain.Models

Namespace WebApiLivros.Domain.Interfaces
    Public Interface IServiceLivros
        Function Save(livroModel As LivroModel) As LivroModel

        Sub Delete(cod As Integer)

        Function RecoverByCod(cod As Integer) As LivroModel

        Function RecoverAll() As IEnumerable(Of LivroListModel)

        Function GetReportData() As DataTable

    End Interface
End Namespace
