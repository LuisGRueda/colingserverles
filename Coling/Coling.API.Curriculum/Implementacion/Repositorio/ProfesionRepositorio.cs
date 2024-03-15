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
    public class ProfesionRepositorio:IProfesionRepositorio
    {
        private readonly string cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public ProfesionRepositorio(IConfiguration conf)
        {
            this.configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Profesion";
        }

        public async Task<bool> Create(Profesion Profesion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(Profesion);
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

        public async Task<Profesion> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            await foreach (Profesion Profesion in tablaCliente.QueryAsync<Profesion>())
            {
                return Profesion;
            }
            return null;
        }

        public async Task<List<Profesion>> GetAll()
        {
            try
            {
                List<Profesion> lista = new List<Profesion>();
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await foreach (Profesion Profesion in tablaCliente.QueryAsync<Profesion>())
                {
                    lista.Add(Profesion);
                }
                return lista;

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<bool> Update(Profesion Profesion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(Profesion, Profesion.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
