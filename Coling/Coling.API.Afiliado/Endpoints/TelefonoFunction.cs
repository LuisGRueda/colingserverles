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
    public class TelefonoFunction
    {
        private readonly ILogger<TelefonoFunction> _logger;
        private readonly ITelefonoLogic TelefonoLogic;

        public TelefonoFunction(ILogger<TelefonoFunction> logger, ITelefonoLogic TelefonoLogic)
        {
            _logger = logger;
            this.TelefonoLogic = TelefonoLogic;
        }

        [Function("ListarTelefonos")]
        public async Task<HttpResponseData> ListarTelefonos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarTelefonos")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar Telefonos.");
            try
            {
                var listaTelefonos = TelefonoLogic.ListarTelefonoTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaTelefonos.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarTelefono")]
        public async Task<HttpResponseData> InsertarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarTelefono")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar Telefonos.");
            try
            {
                var per = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar una Telefono con todos sus datos");
                bool seGuardo = await TelefonoLogic.InsertarTelefono(per);
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
        [Function("EliminarTelefono")]
        public async Task<HttpResponseData> EliminarTelefono([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarTelefono/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para eliminar Telefono con ID {id}");
            try
            {
                bool seElimino = await TelefonoLogic.EliminarTelefono(id);
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

        [Function("ModificarTelefono")]
        public async Task<HttpResponseData> ModificarTelefono([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarTelefono/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para modificar Telefono con ID {id}");
            try
            {
                var Telefono = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar una Telefono con todos sus datos");
                bool seModifico = await TelefonoLogic.ModificarTelefono(Telefono, id);
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

        [Function("ObtenerTelefonoById")]
        public async Task<HttpResponseData> ObtenerTelefonoById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerTelefono/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para obtener Telefono con ID {id}");
            try
            {
                var Telefono = await TelefonoLogic.ObtnerTelefonoById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(Telefono);
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
