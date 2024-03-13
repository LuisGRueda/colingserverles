using Coling.API.Afiliado.Contratos;
using Coling.API.Afiliado;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliado.Implementacion
{
    public class PersonaLogic : IPersonaLogic
    {
        private readonly Contexto contexto;

        public PersonaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarPersona(int id)
        {
            var persona = await contexto.Personas.FindAsync(id);
            if (persona == null)
            {
               return false;
            }
            contexto.Personas.Remove(persona);
            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<bool> InsertarPersona(Persona persona)
        {
            bool sw = false;
            contexto.Personas.Add(persona);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Persona>> ListarPersonaTodos()
        {
            var lista = await contexto.Personas.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarPersona(Persona persona, int id)
        {
            var personaExistente = await contexto.Personas.FindAsync(id);
            if (personaExistente == null)
            {
                return false;
            }
            personaExistente.Nombre = persona.Nombre;
            personaExistente.Apellidos = persona.Apellidos;
            personaExistente.FechaNacimiento = persona.FechaNacimiento;
            personaExistente.Foto = persona.Foto;

            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<Persona> ObtnerPersonaById(int id)
        {
            var persona = await contexto.Personas.FindAsync(id);
            if (persona == null)
            {
                throw new Exception($"No se encontró ninguna persona con el ID {id}");
            }
            return persona;
        }
    }
}