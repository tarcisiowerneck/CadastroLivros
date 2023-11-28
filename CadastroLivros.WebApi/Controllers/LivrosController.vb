Imports Microsoft.AspNetCore.Mvc
Imports System
Imports WebApiLivros.Domain.Interfaces
Imports WebApiLivros.Domain.Models

Namespace WebApiLivros.WebApi.Controllers
    <Route("api/livros")>
    <ApiController>
    Public Class LivrosController
        Inherits Controller
        Private ReadOnly _serviceLivros As IServiceLivros

        Public Sub New(serviceLivros As IServiceLivros)
            GenericRef.Assign(_serviceLivros, serviceLivros)
        End Sub


        <HttpPost>
        Public Function Register(
        <FromBody> livroModel As LivroModel) As IActionResult
            Try
                Dim livro = _serviceLivros.Save(livroModel)

                Return MyBase.Created($"/api/livros/{livro?.Cod}", livro?.Cod)
            Catch ex As Exception
                Return MyBase.BadRequest(ex)
            End Try
        End Function

        <HttpDelete("{cod}")>
        Public Function Remove(
        <FromRoute> cod As Integer) As IActionResult
            Try
                _serviceLivros.Delete(cod)

                Return MyBase.NoContent()
            Catch ex As Exception
                Return MyBase.BadRequest(ex)
            End Try
        End Function

        <HttpGet>
        Public Function RecoverAll() As IActionResult
            Try
                Dim livro = _serviceLivros.RecoverAll()
                Return MyBase.Ok(livro)
            Catch ex As Exception
                Return MyBase.BadRequest(ex)
            End Try
        End Function

        <HttpGet("{cod}")>
        Public Function Recover(
        <FromRoute> cod As Integer) As IActionResult
            Try
                Dim livro = _serviceLivros.RecoverByCod(cod)
                Return MyBase.Ok(livro)
            Catch ex As Exception
                Return MyBase.BadRequest(ex)
            End Try
        End Function
    End Class
End Namespace
