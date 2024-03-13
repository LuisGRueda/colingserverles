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
    public class AfiliadosFunction
    {
        private readonly ILogger<AfiliadosFunction> _logger;
        private readonly IAfiliadoLogic AfiliadosLogic;

        public AfiliadosFunction(ILogger<AfiliadosFunction> logger, IAfiliadoLogic AfiliadosLogic)
        {
            _logger = logger;
            this.AfiliadosLogic = AfiliadosLogic;
        }

        [Function("ListarAfiliadoss")]
        public async Task<HttpResponseData> ListarAfiliadoss([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarAfiliadoss")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar Afiliadoss.");
            try
            {
                var listaAfiliadoss = AfiliadosLogic.ListarAfiliadosTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaAfiliadoss.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarAfiliados")]
        public async Task<HttpResponseData> InsertarAfiliados([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarAfiliados")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar Afiliadoss.");
            try
            {
                var per = await req.ReadFromJsonAsync<Afiliados>() ?? throw new Exception("Debe ingresar una Afiliados con todos sus datos");
                bool seGuardo = await AfiliadosLogic.InsertarAfiliados(per);
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
        [Function("EliminarAfiliados")]
        public async Task<HttpResponseData> EliminarAfiliados([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarAfiliados/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para eliminar Afiliados con ID {id}");
            try
            {
                bool seElimino = await AfiliadosLogic.EliminarAfiliados(id);
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

        [Function("ModificarAfiliados")]
        public async Task<HttpResponseData> ModificarAfiliados([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarAfiliados/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para modificar Afiliados con ID {id}");
            try
            {
                var Afiliados = await req.ReadFromJsonAsync<Afiliados>() ?? throw new Exception("Debe ingresar una Afiliados con todos sus datos");
                bool seModifico = await AfiliadosLogic.ModificarAfiliados(Afiliados, id);
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

        [Function("ObtenerAfiliadosById")]
        public async Task<HttpResponseData> ObtenerAfiliadosById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerAfiliados/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para obtener Afiliados con ID {id}");
            try
            {
                var Afiliados = await AfiliadosLogic.ObtnerAfiliadosById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(Afiliados);
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
