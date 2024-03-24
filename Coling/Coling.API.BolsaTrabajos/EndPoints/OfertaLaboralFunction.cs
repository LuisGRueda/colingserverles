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
    public class OfertaLaboralFunction
    {
        private readonly ILogger<OfertaLaboralFunction> _logger;
        private readonly IOfertaLaboralRepositorio repos;

        public OfertaLaboralFunction(ILogger<OfertaLaboralFunction> logger, IOfertaLaboralRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("ListarOfertasLaborales")]
        [OpenApiOperation("Listarspec", "ListarOfertasLaborales", Description = "Sirve para listar todas las Ofertas Laborales")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OfertaLaboral>), Description = "Mostrara una Lista de Ofertas Laborales")]
        public async Task<HttpResponseData> ListarOfertas([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ListarOfertasLaborales")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var listaOfertas = repos.GetAll();
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaOfertas.Result);
                return respuesta;
            }
            catch (Exception e)
            {

                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                await respuesta.WriteAsJsonAsync(e.Message);
                return respuesta;
            }
        }

        [Function("ObtenerOfertaLaboralesId")]
        [OpenApiOperation("Obtenerspec", "ObtenerOfertaLaboralesId", Description = "Sirve para obtener una Oferta Laboral")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OfertaLaboral), Description = "Mostrara una Oferta Laboral")]
        public async Task<HttpResponseData> ObtenerOfertaById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerOfertaLaboralId/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuesta;
            try
            {
                var oferta = repos.Get(id);
                if (oferta.Result != null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(oferta.Result);
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
        [Function("InsertarOfertaLboral")]
        [OpenApiOperation("Insertarspec", "InsertarOfertaLaboral", Description = "Sirve para Insertar una Oferta Laboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral), Description = "Oferta modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OfertaLaboral), Description = "Mostrara la Oferta Laboral Creada")]
        public async Task<HttpResponseData> InsertarOferta([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InsertarOfertaLabolar")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar una Oferta Laboral con todos sus datos");
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
        [Function("ModificarOfertaLaboral")]
        [OpenApiOperation("Modificarspec", "ModificarOfertaLaboral", Description = "Sirve para Modificar una Oferta Laboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral), Description = "Oferta modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OfertaLaboral), Description = "Mostrara la Oferta Laboral Modificada")]
        public async Task<HttpResponseData> ModificarOferta([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "ModificarOfertaLaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var oferta = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar una Oferta Laboral con todos sus datos");
                bool seModifico = await repos.Update(oferta);
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

        [Function("EliminarOfertaLaboral")]
        [OpenApiOperation("Eliminarspec", "EliminarOfertaLaboral", Description = "Sirve para Eliminar una Oferta Laboral")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        public async Task<HttpResponseData> EliminarOferta([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "EliminarOfertaLaboral/{id}")] HttpRequestData req, string id)
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
