Imports System.Runtime.CompilerServices
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.OpenApi.Models
Imports Swashbuckle.AspNetCore.SwaggerUI

Namespace WebApiLivros.IoC
    Public Module SwaggerDependency
        <Extension()>
        Public Sub AddSwaggerDependency(services As IServiceCollection)
            services.AddSwaggerGen(Sub(c) c.SwaggerDoc("v1", New OpenApiInfo With {
                .Title = "Livros API",
                .Version = "v1",
                .Description = "API VB.NET 8"
                }))
        End Sub

        <Extension()>
        Public Sub UseSwaggerDependency(app As IApplicationBuilder)
            app.UseSwagger()
            app.UseSwaggerUI(Sub(c)
                                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livros.WebApi v1")
                                 c.DocumentTitle = "Livros.WebApi"
                                 c.DocExpansion(DocExpansion.List)
                             End Sub)
        End Sub
    End Module
End Namespace
