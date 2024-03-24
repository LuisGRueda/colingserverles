using Coling.API.BolsaTrabajos.Context;
using Coling.API.BolsaTrabajos.Contratos.Repositorio;
using Coling.API.BolsaTrabajos.Modelos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajos.Implementacion.Repositorio
{
    public class SolicitudRepositorio : ISolicitudRepositorio
    {
        private readonly Contexto contexto;

        public SolicitudRepositorio(Contexto _contexto)
        {
            this.contexto = _contexto;
        }
        public async Task<bool> Create(Solicitud solicitud)
        {
            bool sw = false;

            try
            {
                await contexto.Solicitudes.InsertOneAsync(solicitud);
                sw = true;
            }
            catch (Exception)
            {

                sw = false;
            }
            return sw;
        }
        public async Task<bool> Update(Solicitud solicitud)
        {
            bool sw = false;
            var existe = Builders<Solicitud>.Filter.Eq(x => x.Id, solicitud.Id);
            if (existe != null)
            {
                await contexto.Solicitudes.ReplaceOneAsync(existe, solicitud);
                sw = true;
            }
            return sw;
        }
        public async Task<bool> Delete(string id)
        {
            bool sw = false;
            var existe = Builders<Solicitud>.Filter.Eq(x => x.Id, id);
            if (existe != null)
            {
                await contexto.Solicitudes.DeleteOneAsync(existe);
                sw = true;

            }
            return sw;
        }

        public async Task<List<Solicitud>> GetAll()
        {
            var lista = await contexto.Solicitudes.Find(Builders<Solicitud>.Filter.Empty).ToListAsync();
            return lista;
        }

        public async Task<Solicitud> Get(string id)
        {
            var filter = Builders<Solicitud>.Filter.Eq(x => x.Id, id);
            Solicitud? solicitud = await contexto.Solicitudes.Find(filter).SingleOrDefaultAsync();
            return solicitud;
        }
    }
}
