using Coling.API.Afiliado;
using Coling.API.Afiliado.Contratos;
using Coling.API.Afiliado.Implementacion;
using Coling.Utilitarios.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(x => { x.UseMiddleware<JwtMiddleware>(); })
    .ConfigureServices(services =>
    {
        var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables()
           .Build();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<Contexto>(options => options.UseSqlServer(
                     configuration.GetConnectionString("cadenaConexion")));
        services.AddScoped<IPersonaLogic, PersonaLogic>();
        services.AddScoped<IDireccionLogic, DireccionLogic>();
        services.AddScoped<ITelefonoLogic, TelefonoLogic>();
        services.AddScoped<IAfiliadoLogic, AfiliadosLogic>();
        services.AddScoped<IPersonatipoSocialLogic, PersonatipoSocialLogic>();
        services.AddScoped<IProfesionAfiliadoLogic, ProfesionAfiliadoLogic>();
        services.AddSingleton<JwtMiddleware>();
    })
    .Build();

host.Run();