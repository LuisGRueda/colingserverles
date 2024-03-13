using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliado.Contratos
{
    public interface ITelefonoLogic
    {
        public Task<bool> InsertarTelefono(Telefono Telefono);
        public Task<bool> ModificarTelefono(Telefono Telefono, int id);
        public Task<bool> EliminarTelefono(int id);
        public Task<List<Telefono>> ListarTelefonoTodos();
        public Task<Telefono> ObtnerTelefonoById(int id);
    }
}
