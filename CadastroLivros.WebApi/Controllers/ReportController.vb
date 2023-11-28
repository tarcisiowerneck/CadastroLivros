Imports Microsoft.AspNetCore.Mvc
Imports System
Imports WebApiLivros.Domain.Interfaces
Imports WebApiLivros.Domain.Models

Namespace WebApiLivros.WebApi.Controllers
    <Route("api/report")>
    <ApiController>
    Public Class ReportController
        Inherits Controller
        Private ReadOnly _serviceLivros As IServiceLivros

        Public Sub New(serviceLivros As IServiceLivros)
            GenericRef.Assign(_serviceLivros, serviceLivros)
        End Sub

        <HttpGet>
        Public Function Report() As IActionResult
            Try
                Dim livro = _serviceLivros.GetReportData()
                Return MyBase.Ok(livro)
            Catch ex As Exception
                Return MyBase.BadRequest(ex)
            End Try
        End Function

    End Class
End Namespace
