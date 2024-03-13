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
    public class TelefonoLogic : ITelefonoLogic
    {
        private readonly Contexto contexto;

        public TelefonoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarTelefono(int id)
        {
            var Telefono = await contexto.Telefonos.FindAsync(id);
            if (Telefono == null)
            {
                return false;
            }
            contexto.Telefonos.Remove(Telefono);
            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<bool> InsertarTelefono(Telefono Telefono)
        {
            bool sw = false;
            contexto.Telefonos.Add(Telefono);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Telefono>> ListarTelefonoTodos()
        {
            var lista = await contexto.Telefonos.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarTelefono(Telefono Telefono, int id)
        {
            var TelefonoExistente = await contexto.Telefonos.FindAsync(id);
            if (TelefonoExistente == null)
            {
                return false;
            }
            TelefonoExistente.NroTelefono = Telefono.NroTelefono;

            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<Telefono> ObtnerTelefonoById(int id)
        {
            var Telefono = await contexto.Telefonos.FindAsync(id);
            if (Telefono == null)
            {
                throw new Exception($"No se encontró ninguna Telefono con el ID {id}");
            }
            return Telefono;
        }
    }
}
