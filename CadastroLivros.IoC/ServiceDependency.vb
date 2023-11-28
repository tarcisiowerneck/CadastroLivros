Imports Microsoft.Extensions.DependencyInjection
Imports WebApiLivros.Domain.Interfaces
Imports WebApiLivros.Service
Imports System.Runtime.CompilerServices

Namespace WebApiLivros.IoC
    Public Module ServiceDependency
        <Extension()>
        Public Sub AddServiceDependency(services As IServiceCollection)
            services.AddScoped(Of IServiceLivros, LivrosService)()
        End Sub
    End Module
End Namespace
