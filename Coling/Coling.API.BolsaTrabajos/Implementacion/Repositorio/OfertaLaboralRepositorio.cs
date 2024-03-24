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
    public class OfertaLaboralRepositorio : IOfertaLaboralRepositorio
    {
        private readonly Contexto contexto;

        public OfertaLaboralRepositorio(Contexto _contexto)
        {
            this.contexto = _contexto;
        }

        public async Task<bool> Create(OfertaLaboral oferta)
        {
            bool sw = false;

            try
            {
                await contexto.Ofertas.InsertOneAsync(oferta);
                sw = true;
            }
            catch (Exception)
            {

                sw = false;
            }
            return sw;
        }
        public async Task<bool> Update(OfertaLaboral oferta)
        {
            bool sw = false;
            var existe = Builders<OfertaLaboral>.Filter.Eq(x => x.Id, oferta.Id);
            if (existe != null)
            {
                await contexto.Ofertas.ReplaceOneAsync(existe, oferta);
                sw = true;
            }
            return sw;
        }
        public async Task<bool> Delete(string id)
        {
            bool sw = false;
            var existe = Builders<OfertaLaboral>.Filter.Eq(x => x.Id, id);
            if (existe != null)
            {
                await contexto.Ofertas.DeleteOneAsync(existe);
                sw = true;

            }
            return sw;
        }
        public async Task<List<OfertaLaboral>> GetAll()
        {
            var lista = await contexto.Ofertas.Find(Builders<OfertaLaboral>.Filter.Empty).ToListAsync();
            return lista;
        }

        public async Task<OfertaLaboral> Get(string id)
        {
            var filter = Builders<OfertaLaboral>.Filter.Eq(x => x.Id, id);
            OfertaLaboral? oferta = await contexto.Ofertas.Find(filter).SingleOrDefaultAsync();
            return oferta;
        }

    }
}
