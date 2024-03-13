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
    public class AfiliadosLogic : IAfiliadoLogic
    {
        private readonly Contexto contexto;

        public AfiliadosLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarAfiliados(int id)
        {
            var Afiliados = await contexto.Afiliados.FindAsync(id);
            if (Afiliados == null)
            {
                return false;
            }
            contexto.Afiliados.Remove(Afiliados);
            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<bool> InsertarAfiliados(Afiliados Afiliados)
        {
            bool sw = false;
            contexto.Afiliados.Add(Afiliados);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Afiliados>> ListarAfiliadosTodos()
        {
            var lista = await contexto.Afiliados.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarAfiliados(Afiliados Afiliados, int id)
        {
            var AfiliadosExistente = await contexto.Afiliados.FindAsync(id);
            if (AfiliadosExistente == null)
            {
                return false;
            }
            AfiliadosExistente.FechaAfiliacion = Afiliados.FechaAfiliacion;
            AfiliadosExistente.CodigoAfiliado = Afiliados.CodigoAfiliado;
            AfiliadosExistente.NroTituloProvisional = Afiliados.NroTituloProvisional;

            int response = await contexto.SaveChangesAsync();
            return response == 1;
        }

        public async Task<Afiliados> ObtnerAfiliadosById(int id)
        {
            var Afiliados = await contexto.Afiliados.FindAsync(id);
            if (Afiliados == null)
            {
                throw new Exception($"No se encontró ningun Afiliados con el ID {id}");
            }
            return Afiliados;
        }
    }
}
