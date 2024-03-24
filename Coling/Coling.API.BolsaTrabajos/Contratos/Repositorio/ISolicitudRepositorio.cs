using Coling.API.BolsaTrabajos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajos.Contratos.Repositorio
{
    public interface ISolicitudRepositorio
    {
        public Task<bool> Create(Solicitud solicitud);
        public Task<bool> Update(Solicitud solicitud);
        public Task<bool> Delete(string id);
        public Task<List<Solicitud>> GetAll();
        public Task<Solicitud> Get(string id);
    }
}
