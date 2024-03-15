using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorio
{
    public class ExperienciaLaboralRepositorio:IExperienciaLaboralRepositorio
    {
        private readonly string cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public ExperienciaLaboralRepositorio(IConfiguration conf)
        {
            this.configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "ExperienciaLaboral";
        }

        public async Task<bool> Create(ExperienciaLaboral ExperienciaLaboral)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(ExperienciaLaboral);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(string partitionKey, string rowKey)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.DeleteEntityAsync(partitionKey, rowKey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ExperienciaLaboral> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            await foreach (ExperienciaLaboral ExperienciaLaboral in tablaCliente.QueryAsync<ExperienciaLaboral>())
            {
                return ExperienciaLaboral;
            }
            return null;
        }

        public async Task<List<ExperienciaLaboral>> GetAll()
        {
            try
            {
                List<ExperienciaLaboral> lista = new List<ExperienciaLaboral>();
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await foreach (ExperienciaLaboral ExperienciaLaboral in tablaCliente.QueryAsync<ExperienciaLaboral>())
                {
                    lista.Add(ExperienciaLaboral);
                }
                return lista;

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<bool> Update(ExperienciaLaboral ExperienciaLaboral)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(ExperienciaLaboral, ExperienciaLaboral.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
