using eAgenda.WebApp.Compartilhado;
using eAgenda.WebApp.Compartilhado.Infra.Orm;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eAgenda.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddPresentationConfig(builder.Configuration);
        builder.Services.AddServicesConfig(builder.Configuration, builder.Logging);
        builder.Services.AddRepositoriesConfig(builder.Configuration);
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<EAgendaDbContext>(
                name: "database_check",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["ready"]
            );

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.MapHealthChecks("/health");

        app.Run();  
    }
}
