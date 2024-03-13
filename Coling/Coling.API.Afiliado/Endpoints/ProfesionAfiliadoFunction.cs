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
    public class ProfesionAfiliadoFunction
    {
        private readonly ILogger<ProfesionAfiliadoFunction> _logger;
        private readonly IProfesionAfiliadoLogic ProfesionAfiliadoLogic;

        public ProfesionAfiliadoFunction(ILogger<ProfesionAfiliadoFunction> logger, IProfesionAfiliadoLogic ProfesionAfiliadoLogic)
        {
            _logger = logger;
            this.ProfesionAfiliadoLogic = ProfesionAfiliadoLogic;
        }

        [Function("ListarProfesionAfiliados")]
        public async Task<HttpResponseData> ListarProfesionAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarProfesionAfiliados")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar ProfesionAfiliados.");
            try
            {
                var listaProfesionAfiliados = ProfesionAfiliadoLogic.ListarProfesionAfiliadoTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaProfesionAfiliados.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarProfesionAfiliado")]
        public async Task<HttpResponseData> InsertarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarProfesionAfiliado")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar ProfesionAfiliados.");
            try
            {
                var per = await req.ReadFromJsonAsync<ProfesionAfiliado>() ?? throw new Exception("Debe ingresar una ProfesionAfiliado con todos sus datos");
                bool seGuardo = await ProfesionAfiliadoLogic.InsertarProfesionAfiliado(per);
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
        [Function("EliminarProfesionAfiliado")]
        public async Task<HttpResponseData> EliminarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarProfesionAfiliado/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para eliminar ProfesionAfiliado con ID {id}");
            try
            {
                bool seElimino = await ProfesionAfiliadoLogic.EliminarProfesionAfiliado(id);
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

        [Function("ModificarProfesionAfiliado")]
        public async Task<HttpResponseData> ModificarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarProfesionAfiliado/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para modificar ProfesionAfiliado con ID {id}");
            try
            {
                var ProfesionAfiliado = await req.ReadFromJsonAsync<ProfesionAfiliado>() ?? throw new Exception("Debe ingresar una ProfesionAfiliado con todos sus datos");
                bool seModifico = await ProfesionAfiliadoLogic.ModificarProfesionAfiliado(ProfesionAfiliado, id);
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

        [Function("ObtenerProfesionAfiliadoById")]
        public async Task<HttpResponseData> ObtenerProfesionAfiliadoById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerProfesionAfiliado/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para obtener ProfesionAfiliado con ID {id}");
            try
            {
                var ProfesionAfiliado = await ProfesionAfiliadoLogic.ObtnerProfesionAfiliadoById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(ProfesionAfiliado);
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
