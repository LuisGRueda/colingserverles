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
    public class PersonatipoSocialFunction
    {
        private readonly ILogger<PersonatipoSocialFunction> _logger;
        private readonly IPersonatipoSocialLogic PersonatipoSocialLogic;

        public PersonatipoSocialFunction(ILogger<PersonatipoSocialFunction> logger, IPersonatipoSocialLogic PersonatipoSocialLogic)
        {
            _logger = logger;
            this.PersonatipoSocialLogic = PersonatipoSocialLogic;
        }

        [Function("ListarPersonatipoSocials")]
        public async Task<HttpResponseData> ListarPersonatipoSocials([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarPersonatipoSocials")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar PersonatipoSocials.");
            try
            {
                var listaPersonatipoSocials = PersonatipoSocialLogic.ListarPersonatipoSocialTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaPersonatipoSocials.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarPersonatipoSocial")]
        public async Task<HttpResponseData> InsertarPersonatipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarPersonatipoSocial")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando azure function para isnertar PersonatipoSocials.");
            try
            {
                var per = await req.ReadFromJsonAsync<PersonatipoSocial>() ?? throw new Exception("Debe ingresar una PersonatipoSocial con todos sus datos");
                bool seGuardo = await PersonatipoSocialLogic.InsertarPersonatipoSocial(per);
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
        [Function("EliminarPersonatipoSocial")]
        public async Task<HttpResponseData> EliminarPersonatipoSocial([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarPersonatipoSocial/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para eliminar PersonatipoSocial con ID {id}");
            try
            {
                bool seElimino = await PersonatipoSocialLogic.EliminarPersonatipoSocial(id);
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

        [Function("ModificarPersonatipoSocial")]
        public async Task<HttpResponseData> ModificarPersonatipoSocial([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarPersonatipoSocial/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para modificar PersonatipoSocial con ID {id}");
            try
            {
                var PersonatipoSocial = await req.ReadFromJsonAsync<PersonatipoSocial>() ?? throw new Exception("Debe ingresar una PersonatipoSocial con todos sus datos");
                bool seModifico = await PersonatipoSocialLogic.ModificarPersonatipoSocial(PersonatipoSocial, id);
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

        [Function("ObtenerPersonatipoSocialById")]
        public async Task<HttpResponseData> ObtenerPersonatipoSocialById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerPersonatipoSocial/{id}")] HttpRequestData req, int id)
        {
            _logger.LogInformation($"Ejecutando azure function para obtener PersonatipoSocial con ID {id}");
            try
            {
                var PersonatipoSocial = await PersonatipoSocialLogic.ObtnerPersonatipoSocialById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(PersonatipoSocial);
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
