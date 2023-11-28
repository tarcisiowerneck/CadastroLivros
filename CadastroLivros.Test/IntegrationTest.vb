Imports Microsoft.AspNetCore.Mvc
Imports WebApiLivros.Domain.Entities
Imports WebApiLivros.Domain.Models
Imports WebApiLivros.WebApi.Controllers
Imports Xunit

Namespace WebApiLivros.Test
    Public Class IntegrationTest
        <Fact>
        Public Sub TestRecoverAll()
            Dim mock = New List(Of Livro)() From {
    New Produtos(1, "Produto1", 10.21D, Date.Now, 12.37D, Domain.Enums.TipoProdutos.Outros),
    New Produtos(2, "Produto2", 1.37D, Date.Now, 1D, Domain.Enums.TipoProdutos.Limpeza),
    New Produtos(3, "Produto3", -2.37D, New DateTime(), 3.3D, Domain.Enums.TipoProdutos.Eletrodomestico),
    New Produtos(4, "Produto4", 3D, Date.Now, -4.4D, Domain.Enums.TipoProdutos.Alimento)
}

            Dim mockService = New MockupService(mock)

            Dim controller = New LivrosController(mockService)

            Dim result = controller.RecoverAll()

            Assert.IsAssignableFrom(Of OkObjectResult)(result)
        End Sub

        <Fact>
        Public Sub TestRecoverById()
            Dim mock = New List(Of Livro)() From {
    New Produtos(1, "Produto1", 10.21D, Date.Now, 12.37D, Domain.Enums.TipoProdutos.Outros),
    New Produtos(2, "Produto2", 1.37D, Date.Now, 1D, Domain.Enums.TipoProdutos.Limpeza),
    New Produtos(3, "Produto3", -2.37D, New DateTime(), 3.3D, Domain.Enums.TipoProdutos.Eletrodomestico),
    New Produtos(4, "Produto4", 3D, Date.Now, -4.4D, Domain.Enums.TipoProdutos.Alimento)
}

            Dim mockService = New MockupService(mock)

            Dim controller = New LivrosController(mockService)

            Dim result = controller.Recover(3)

            Assert.IsAssignableFrom(Of OkObjectResult)(result)
            Dim ok As OkObjectResult = Nothing, livroModel As LivroModel = Nothing
            If CSharpImpl.__Assign(ok, TryCast(result, OkObjectResult)) IsNot Nothing AndAlso CSharpImpl.__Assign(livroModel, TryCast(ok.Value, LivroModel)) IsNot Nothing Then Assert.Equal("Produto3", livroModel.Descricao)
        End Sub

        <Fact>
        Public Sub TestRecoverWithInvalidId()
            Dim mock = New List(Of Livro)() From {
    New Produtos(1, "Produto1", 10.21D, Date.Now, 12.37D, Domain.Enums.TipoProdutos.Outros),
    New Produtos(2, "Produto2", 1.37D, Date.Now, 1D, Domain.Enums.TipoProdutos.Limpeza),
    New Produtos(4, "Produto4", 3D, Date.Now, -4.4D, Domain.Enums.TipoProdutos.Alimento)
}

            Dim mockService = New MockupService(mock)

            Dim controller = New LivrosController(mockService)

            Dim result = controller.Recover(3)

            Assert.IsAssignableFrom(Of BadRequestObjectResult)(result)
        End Sub
    End Class
End Namespace
