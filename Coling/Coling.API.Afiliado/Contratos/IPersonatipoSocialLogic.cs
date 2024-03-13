using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliado.Contratos
{
    public interface IPersonatipoSocialLogic
    {
        public Task<bool> InsertarPersonatipoSocial(PersonatipoSocial PersonatipoSocial);
        public Task<bool> ModificarPersonatipoSocial(PersonatipoSocial PersonatipoSocial, int id);
        public Task<bool> EliminarPersonatipoSocial(int id);
        public Task<List<PersonatipoSocial>> ListarPersonatipoSocialTodos();
        public Task<PersonatipoSocial> ObtnerPersonatipoSocialById(int id);
    }
}
