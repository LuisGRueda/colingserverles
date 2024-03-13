using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliado.Contratos
{
    public interface IDireccionLogic
    {
        public Task<bool> InsertarDireccion(Direccion Direccion);
        public Task<bool> ModificarDireccion(Direccion Direccion, int id);
        public Task<bool> EliminarDireccion(int id);
        public Task<List<Direccion>> ListarDireccionTodos();
        public Task<Direccion> ObtnerDireccionById(int id);
    }
}
