using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliado.Contratos
{
    public interface IProfesionAfiliadoLogic
    {
        public Task<bool> InsertarProfesionAfiliado(ProfesionAfiliado ProfesionAfiliado);
        public Task<bool> ModificarProfesionAfiliado(ProfesionAfiliado ProfesionAfiliado, int id);
        public Task<bool> EliminarProfesionAfiliado(int id);
        public Task<List<ProfesionAfiliado>> ListarProfesionAfiliadoTodos();
        public Task<ProfesionAfiliado> ObtnerProfesionAfiliadoById(int id);
    }
}
