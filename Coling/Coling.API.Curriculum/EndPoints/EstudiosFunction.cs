using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.EndPoints
{
    public class EstudiosFunction
    {
        private readonly ILogger<EstudiosFunction> _logger;
        private readonly IEstudiosRepositorio repos;

        public EstudiosFunction(ILogger<EstudiosFunction> logger, IEstudiosRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarEstudios")]
        public async Task<HttpResponseData> InsertarEstudios([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarestudio")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("ingrese");
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
        [Function("ListarEstudios")]
        public async Task<HttpResponseData> ListarEstudios([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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
        [Function("EliminarEstudios")]
        public async Task<HttpResponseData> EliminarEstudios([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Estudioses/{partitionKey}/{rowKey}")] HttpRequestData req, string partitionKey, string rowKey)
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
        [Function("ObtenerEstudios")]
        public async Task<HttpResponseData> ObtenerEstudios([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Estudioses/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuesta;
            try
            {
                var Estudios = await repos.Get(id);
                if (Estudios != null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(Estudios);
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
        [Function("ActualizarEstudios")]
        public async Task<HttpResponseData> ActualizarEstudios(
    [HttpTrigger(AuthorizationLevel.Function, "put", Route = "Estudios")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var contenido = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("ingrese");

                Estudios estudios = new Estudios
                {
                    PartitionKey = contenido.PartitionKey,
                    RowKey = contenido.RowKey,
                    TipoEstudio= contenido.TipoEstudio,
                    Afiliado = contenido.Afiliado,
                    NombreGrado = contenido.NombreGrado,
                    TituloRecibido = contenido.TituloRecibido,
                    Institucion = contenido.Institucion,
                    Anio = contenido.Anio,
                    Estado = contenido.Estado
                };
                var resultado = await repos.Update(estudios);
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
