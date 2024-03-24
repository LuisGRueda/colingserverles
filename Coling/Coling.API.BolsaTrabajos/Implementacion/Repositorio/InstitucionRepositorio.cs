using Coling.API.BolsaTrabajos.Contratos.Repositorio;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.API.BolsaTrabajos.Context;
using Coling.API.BolsaTrabajos.Modelos;

namespace Coling.API.BolsaTrabajos.Implementacion.Repositorio
{
    public class InstitucionRepositorio : IInstitucionRepositorio
    {
        private readonly Contexto contexto;

        public InstitucionRepositorio(Contexto _contexto)
        {
            contexto = _contexto;
        }
        public async Task<bool> Create(Institucion institucion)
        {
            bool sw = false;

            try
            {
                await contexto.Instituciones.InsertOneAsync(institucion);
                sw = true;
            }
            catch (Exception)
            {

                sw = false;
            }


            return sw;
        }
        public async Task<bool> Update(Institucion institucion)
        {
            bool sw = false;
            var existe = Builders<Institucion>.Filter.Eq(x => x.Id, institucion.Id);
            if (existe != null)
            {
                await contexto.Instituciones.ReplaceOneAsync(existe, institucion);
                sw = true;
            }
            return sw;

        }
        public async Task<bool> Delete(string id)
        {
            bool sw = false;
            var existe = Builders<Institucion>.Filter.Eq(x => x.Id, id);
            if (existe != null)
            {
                await contexto.Instituciones.DeleteOneAsync(existe);
                sw = true;

            }
            return sw;

        }
        public async Task<List<Institucion>> GetAll()
        {
            var lista = await contexto.Instituciones.Find(Builders<Institucion>.Filter.Empty).ToListAsync();
            return lista;
        }

        public async Task<Institucion> Get(string id)
        {
            var filter = Builders<Institucion>.Filter.Eq(x => x.Id, id);
            Institucion? institucion = await contexto.Instituciones.Find(filter).SingleOrDefaultAsync();
            return institucion;

        }
    }
}
