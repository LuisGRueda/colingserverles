using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.EndPoints
{
    public class ProfesionFunction
    {
        private readonly ILogger<ProfesionFunction> _logger;
        private readonly IProfesionRepositorio repos;

        public ProfesionFunction(ILogger<ProfesionFunction> logger, IProfesionRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarProfesion")]
        public async Task<HttpResponseData> InsertarProfesion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarprofesion")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("ingrese");
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
        [Function("ListarProfesion")]
        public async Task<HttpResponseData> ListarProfesion([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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
        [Function("EliminarProfesion")]
        public async Task<HttpResponseData> EliminarProfesion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Profesiones/{partitionKey}/{rowKey}")] HttpRequestData req, string partitionKey, string rowKey)
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
        [Function("ObtenerProfesion")]
        public async Task<HttpResponseData> ObtenerProfesion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Profesiones/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuesta;
            try
            {
                var Profesion = await repos.Get(id);
                if (Profesion != null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(Profesion);
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
        [Function("ActualizarProfesion")]
        public async Task<HttpResponseData> ActualizarProfesion(
    [HttpTrigger(AuthorizationLevel.Function, "put", Route = "Profesion")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var contenido = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("ingrese");

                Profesion Profesion = new Profesion
                {
                    PartitionKey = contenido.PartitionKey,
                    RowKey = contenido.RowKey,
                    NombreProfesion = contenido.NombreProfesion,
                    NombreGrado = contenido.NombreGrado,
                    Estado = contenido.Estado
                };
                var resultado = await repos.Update(Profesion);
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
