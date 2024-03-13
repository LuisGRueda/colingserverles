using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliado.Contratos
{
    public interface IAfiliadoLogic
    {
        public Task<bool> InsertarAfiliados(Afiliados Afiliados);
        public Task<bool> ModificarAfiliados(Afiliados Afiliados, int id);
        public Task<bool> EliminarAfiliados(int id);
        public Task<List<Afiliados>> ListarAfiliadosTodos();
        public Task<Afiliados> ObtnerAfiliadosById(int id);
    }
}
