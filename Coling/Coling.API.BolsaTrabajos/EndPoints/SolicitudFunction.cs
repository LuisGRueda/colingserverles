using Coling.API.BolsaTrabajos.Contratos.Repositorio;
using Coling.API.BolsaTrabajos.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.BolsaTrabajos.EndPoints
{
    public class SolicitudFunction
    {
        private readonly ILogger<SolicitudFunction> _logger;
        private readonly ISolicitudRepositorio repos;

        public SolicitudFunction(ILogger<SolicitudFunction> logger, ISolicitudRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("ListarSolicitudes")]
        [OpenApiOperation("Listarspec", "ListarSolicitudes", Description = "Sirve para listar todas las Solicitudes")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Solicitud>), Description = "Mostrara una Lista de Solicitudes")]
        public async Task<HttpResponseData> ListarSolicitudes([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ListarSolicitudes")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var listaSolicitudes = repos.GetAll();
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaSolicitudes.Result);
                return respuesta;
            }
            catch (Exception e)
            {

                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                await respuesta.WriteAsJsonAsync(e.Message);
                return respuesta;
            }
        }

        [Function("ObtenerSolicitudId")]
        [OpenApiOperation("Obtenerspec", "ObtenerSolicitudId", Description = "Sirve para obtener una Solicitud")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Mostrara una Solicitud")]
        public async Task<HttpResponseData> ObtenerSolicitudById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerSolicitudId/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuesta;
            try
            {
                var solicitud = repos.Get(id);
                if (solicitud.Result != null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(solicitud.Result);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    return respuesta;
                }

            }
            catch (Exception e)
            {

                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                await respuesta.WriteAsJsonAsync(e.Message);
                return respuesta;
            }
        }
        [Function("InsertarSolicitud")]
        [OpenApiOperation("Insertarspec", "InsertarSolicitud", Description = "Sirve para Insertar una Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud), Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Mostrara la Solicitud Creada")]
        public async Task<HttpResponseData> InsertarSolicitud([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InsertarSolicitud")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una Solicitud con todos sus datos");
                bool seGuardo = await repos.Create(registro);
                if (seGuardo)
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
            catch (Exception e)
            {

                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                await respuesta.WriteAsJsonAsync(e.Message);
                return respuesta;
            }
        }

        [Function("ModificarSolicitud")]
        [OpenApiOperation("Modificarspec", "ModificarSolicitud", Description = "Sirve para Modificar una Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud), Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Solicitud), Description = "Mostrara la Solicitud Modificada")]
        public async Task<HttpResponseData> ModificarSolicitud([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "ModificarSolicitud")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var solicitud = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una Solicitud con todos sus datos");
                bool seModifico = await repos.Update(solicitud);
                if (seModifico)
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
            catch (Exception e)
            {

                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                await respuesta.WriteAsJsonAsync(e.Message);
                return respuesta;
            }
        }

        [Function("EliminarSolicitud")]
        [OpenApiOperation("Eliminarspec", "EliminarSolicitud", Description = "Sirve para Eliminar una Solicitud")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        public async Task<HttpResponseData> EliminarSolicitud([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "EliminarSolicitud/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuesta;
            try
            {
                bool seElimino = await repos.Delete(id);
                if (seElimino)
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
            catch (Exception e)
            {

                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                await respuesta.WriteAsJsonAsync(e.Message);
                return respuesta;
            }
        }
    }
}
