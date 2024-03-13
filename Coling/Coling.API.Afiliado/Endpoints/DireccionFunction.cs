using Coling.API.Afiliado.Contratos;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Afiliado.Endpoints
{
    public class DireccionFunction
    {
        private readonly ILogger<DireccionFunction> _logger;
        private readonly IDireccionLogic DireccionLogic;

        public DireccionFunction(ILogger<DireccionFunction> logger, IDireccionLogic DireccionLogic)
        {
            _logger = logger;
            this.DireccionLogic = DireccionLogic;
        }

        [Function("ListarDirecciones")]
        public async Task<HttpResponseData> ListarDireccions([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarDireccions")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar Direccions.");
            try
            {
                var listaDireccions = DireccionLogic.ListarDireccionTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaDireccions.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarDireccion")]
        public async Task<HttpResponseData> InsertarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarDireccion")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar Direccions.");
            try
            {
                var per = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una Direccion con todos sus datos");
                bool seGuardo = await DireccionLogic.InsertarDireccion(per);
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("EliminarDireccion")]
        public async Task<HttpResponseData> EliminarDireccion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarDireccion/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para eliminar Direccion con ID {id}");
            try
            {
                bool seElimino = await DireccionLogic.EliminarDireccion(id);
                if (seElimino)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }
        }

        [Function("ModificarDireccion")]
        public async Task<HttpResponseData> ModificarDireccion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarDireccion/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para modificar Direccion con ID {id}");
            try
            {
                var Direccion = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una Direccion con todos sus datos");
                bool seModifico = await DireccionLogic.ModificarDireccion(Direccion, id);
                if (seModifico)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }
        }

        [Function("ObtenerDireccionById")]
        public async Task<HttpResponseData> ObtenerDireccionById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerDireccion/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para obtener Direccion con ID {id}");
            try
            {
                var Direccion = await DireccionLogic.ObtnerDireccionById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(Direccion);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }
        }
    }
}
