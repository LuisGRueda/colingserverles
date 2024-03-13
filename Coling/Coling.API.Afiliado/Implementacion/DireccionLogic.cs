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
    public class DireccionLogic : IDireccionLogic
    {
        private readonly Contexto contexto;

        public DireccionLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarDireccion(int id)
        {
            var Direccion = await contexto.Direcciones.FindAsync(id);
            if (Direccion == null)
            {
                return false;
            }
            contexto.Direcciones.Remove(Direccion);
            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<bool> InsertarDireccion(Direccion Direccion)
        {
            bool sw = false;
            contexto.Direcciones.Add(Direccion);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Direccion>> ListarDireccionTodos()
        {
            var lista = await contexto.Direcciones.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarDireccion(Direccion Direccion, int id)
        {
            var DireccionExistente = await contexto.Direcciones.FindAsync(id);
            if (DireccionExistente == null)
            {
                return false;
            }
            DireccionExistente.Descripcion = Direccion.Descripcion;

            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<Direccion> ObtnerDireccionById(int id)
        {
            var Direccion = await contexto.Direcciones.FindAsync(id);
            if (Direccion == null)
            {
                throw new Exception($"No se encontró ninguna Direccion con el ID {id}");
            }
            return Direccion;
        }
    }
}
