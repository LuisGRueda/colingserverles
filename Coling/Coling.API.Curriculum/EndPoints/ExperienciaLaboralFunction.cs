using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.EndPoints
{
    public class ExperienciaLaboralFunction
    {
        private readonly ILogger<ExperienciaLaboralFunction> _logger;
        private readonly IExperienciaLaboralRepositorio repos;

        public ExperienciaLaboralFunction(ILogger<ExperienciaLaboralFunction> logger, IExperienciaLaboralRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarExperienciaLboral")]
        public async Task<HttpResponseData> InsertarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarpersona")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<ExperienciaLaboral>() ?? throw new Exception("ingrese");
                registro.RowKey = Guid.NewGuid().ToString();
                registro.Timestamp = DateTime.UtcNow;
                bool sw = await repos.Create(registro);
                if (sw)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }

            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }

        }
        [Function("ListarExperienciaLaboral")]
        public async Task<HttpResponseData> ListarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var lista = repos.GetAll();
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista);
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }

        }
        [Function("EliminarExperienciaLaboral")]
        public async Task<HttpResponseData> EliminarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "ExperienciaLaborales/{partitionKey}/{rowKey}")] HttpRequestData req, string partitionKey, string rowKey)
        {
            HttpResponseData respuesta;
            try
            {
                var resultado = await repos.Delete(partitionKey, rowKey);
                if (resultado)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                }
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ObtenerExperienciaLaboral")]
        public async Task<HttpResponseData> ObtenerExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ExperienciaLaborales/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuesta;
            try
            {
                var ExperienciaLaboral = await repos.Get(id);
                if (ExperienciaLaboral != null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(ExperienciaLaboral);
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    await respuesta.WriteAsJsonAsync(new { message = "Institución no encontrada" });
                }
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ActualizarExperienciaLaboral")]
        public async Task<HttpResponseData> ActualizarExperienciaLaboral(
    [HttpTrigger(AuthorizationLevel.Function, "put", Route = "ExperienciaLaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var contenido = await req.ReadFromJsonAsync<ExperienciaLaboral>() ?? throw new Exception("ingrese");

                ExperienciaLaboral ExperienciaLaboral = new ExperienciaLaboral
                {
                    PartitionKey = contenido.PartitionKey,
                    RowKey = contenido.RowKey,
                    Afiliado = contenido.Afiliado,
                    Institucion = contenido.Institucion,
                    CargoTitulo=contenido.CargoTitulo,
                    FechaInicio = contenido.FechaInicio,
                    FechaFinal = contenido.FechaFinal,
                    Estado = contenido.Estado
                };
                var resultado = await repos.Update(ExperienciaLaboral);
                if (resultado)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                }
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
    }
}
