using Coling.API.BolsaTrabajos.Context;
using Coling.API.BolsaTrabajos.Contratos.Repositorio;
using Coling.API.BolsaTrabajos.Implementacion.Repositorio;
using Coling.Utilitarios.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(x => { x.UseMiddleware<JwtMiddleware>(); })
    .ConfigureServices(services =>
    {
        services.AddSingleton<Contexto>();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IInstitucionRepositorio, InstitucionRepositorio>();
        services.AddScoped<IOfertaLaboralRepositorio, OfertaLaboralRepositorio>();
        services.AddScoped<ISolicitudRepositorio, SolicitudRepositorio>();
        services.AddSingleton<JwtMiddleware>();
    })
    .Build();

host.Run();
