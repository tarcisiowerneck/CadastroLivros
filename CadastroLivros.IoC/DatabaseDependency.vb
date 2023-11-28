Imports System.Runtime.CompilerServices
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection

Namespace WebApiLivros.IoC
    Public Module DatabaseDependency
        <Extension()>
        Public Sub AddDatabaseDependency(services As IServiceCollection, configuration As IConfiguration)
            services.AddSingleton(Of String)(Function(provider) configuration.GetConnectionString("DefaultConnection"))
        End Sub
    End Module
End Namespace
