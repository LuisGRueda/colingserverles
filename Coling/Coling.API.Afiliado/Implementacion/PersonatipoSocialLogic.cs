using Coling.API.Afiliado.Contratos;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliado.Implementacion
{
    public class PersonatipoSocialLogic : IPersonatipoSocialLogic
    {
        private readonly Contexto contexto;

        public PersonatipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarPersonatipoSocial(int id)
        {
            var PersonatipoSocialtipoSocial = await contexto.PersonasTiposSociales.FindAsync(id);
            if (PersonatipoSocialtipoSocial == null)
            {
                return false;
            }
            contexto.PersonasTiposSociales.Remove(PersonatipoSocialtipoSocial);
            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<bool> InsertarPersonatipoSocial(PersonatipoSocial PersonatipoSocial)
        {
            bool sw = false;
            contexto.PersonasTiposSociales.Add(PersonatipoSocial);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<PersonatipoSocial>> ListarPersonatipoSocialTodos()
        {
            var lista = await contexto.PersonasTiposSociales.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarPersonatipoSocial(PersonatipoSocial PersonatipoSocial, int id)
        {
            var PersonatipoSocialExistente = await contexto.PersonasTiposSociales.FindAsync(id);
            if (PersonatipoSocialExistente == null)
            {
                return false;
            }
            PersonatipoSocialExistente.TipoSocial = PersonatipoSocial.TipoSocial;

            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<PersonatipoSocial> ObtnerPersonatipoSocialById(int id)
        {
            var PersonatipoSocial = await contexto.PersonasTiposSociales.FindAsync(id);
            if (PersonatipoSocial == null)
            {
                throw new Exception($"No se encontró ninguna PersonatipoSocial con el ID {id}");
            }
            return PersonatipoSocial;
        }
    }
}
