Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Cors.Infrastructure
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports WebApiLivros.IoC
Imports WebApiProdutos.IoC

Namespace WebApiLivros.WebApi
    Public Class Startup
        Public Sub New(configuration As IConfiguration)
            Me.Configuration = configuration
        End Sub

        Public ReadOnly Property Configuration As IConfiguration

        Public Sub ConfigureServices(services As IServiceCollection)
            services.AddControllers()
            services.AddServiceDependency()
            services.AddSwaggerDependency()
            services.AddDatabaseDependency(Configuration)
        End Sub

        Public Sub Configure(app As IApplicationBuilder, env As IWebHostEnvironment)
            If env.IsDevelopment() Then
                app.UseDeveloperExceptionPage()
                app.UseSwaggerDependency()
            End If

            app.UseHttpsRedirection()

            app.UseCors(Function(options)
                            options.AllowAnyHeader()
                            options.AllowAnyOrigin()
                            options.AllowAnyMethod()
                        End Function)

            app.UseRouting()
            app.UseAuthorization()
            app.UseEndpoints(Sub(endpoints) endpoints.MapControllers())
        End Sub
    End Class
End Namespace
