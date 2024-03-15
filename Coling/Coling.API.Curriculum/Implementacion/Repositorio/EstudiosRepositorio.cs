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
    public class EstudiosRepositorio:IEstudiosRepositorio
    {
        private readonly string cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public EstudiosRepositorio(IConfiguration conf)
        {
            this.configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Estudios";
        }

        public async Task<bool> Create(Estudios Estudios)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(Estudios);
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

        public async Task<Estudios> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            await foreach (Estudios Estudios in tablaCliente.QueryAsync<Estudios>())
            {
                return Estudios;
            }
            return null;
        }

        public async Task<List<Estudios>> GetAll()
        {
            try
            {
                List<Estudios> lista = new List<Estudios>();
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await foreach (Estudios Estudios in tablaCliente.QueryAsync<Estudios>())
                {
                    lista.Add(Estudios);
                }
                return lista;

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<bool> Update(Estudios Estudios)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(Estudios, Estudios.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
