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
    public class ProfesionAfiliadoLogic : IProfesionAfiliadoLogic
    {
        private readonly Contexto contexto;

        public ProfesionAfiliadoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarProfesionAfiliado(int id)
        {
            var ProfesionalAfiliado = await contexto.ProfesionesAfiliados.FindAsync(id);
            if (ProfesionalAfiliado == null)
            {
                return false;
            }
            contexto.ProfesionesAfiliados.Remove(ProfesionalAfiliado);
            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<bool> InsertarProfesionAfiliado(ProfesionAfiliado ProfesionalAfiliado)
        {
            bool sw = false;
            contexto.ProfesionesAfiliados.Add(ProfesionalAfiliado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<ProfesionAfiliado>> ListarProfesionAfiliadoTodos()
        {
            var lista = await contexto.ProfesionesAfiliados.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarProfesionAfiliado(ProfesionAfiliado ProfesionalAfiliado, int id)
        {
            var ProfesionalAfiliadoExistente = await contexto.ProfesionesAfiliados.FindAsync(id);
            if (ProfesionalAfiliadoExistente == null)
            {
                return false;
            }
            ProfesionalAfiliadoExistente.FechaAsignacion = ProfesionalAfiliado.FechaAsignacion;
            ProfesionalAfiliadoExistente.NroSelloSIB = ProfesionalAfiliado.NroSelloSIB;

            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<ProfesionAfiliado> ObtnerProfesionAfiliadoById(int id)
        {
            var ProfesionalAfiliado = await contexto.ProfesionesAfiliados.FindAsync(id);
            if (ProfesionalAfiliado == null)
            {
                throw new Exception($"No se encontró ninguna ProfesionalAfiliado con el ID {id}");
            }
            return ProfesionalAfiliado;
        }
    }
}
