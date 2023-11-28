Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.Hosting

Namespace WebApiLivros.WebApi
    Public Class Program
        Public Shared Sub Main(args As String())
            Call CreateHostBuilder(args).Build().Run()
        End Sub

        Public Shared Function CreateHostBuilder(args As String()) As IHostBuilder
            Return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(Sub(webBuilder) webBuilder.UseStartup(Of Startup)())
        End Function
    End Class
End Namespace
