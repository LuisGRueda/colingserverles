using Coling.API.BolsaTrabajos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajos.Contratos.Repositorio
{
    public interface IInstitucionRepositorio
    {
        public Task<bool> Create(Institucion institucion);
        public Task<bool> Update(Institucion institucion);
        public Task<bool> Delete(string id);
        public Task<List<Institucion>> GetAll();
        public Task<Institucion> Get(string id);
    }
}
