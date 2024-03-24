using Coling.API.BolsaTrabajos.Modelos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajos.Context
{
    public class Contexto
    {
        private readonly IMongoDatabase basedatos;

        public Contexto()
        {

            string cadenaConexion = Environment.GetEnvironmentVariable("cadenaConexion")!.ToString();
            string basedatosNombre = Environment.GetEnvironmentVariable("databaseName")!.ToString();
            var client = new MongoClient(cadenaConexion);
            if (client != null)
                this.basedatos = client.GetDatabase(basedatosNombre);

        }
        public IMongoCollection<Institucion> Instituciones
        {
            get
            {
                return basedatos.GetCollection<Institucion>("institucion");
            }
        }
        public IMongoCollection<OfertaLaboral> Ofertas
        {
            get
            {
                return basedatos.GetCollection<OfertaLaboral>("ofertalaboral");
            }
        }
        public IMongoCollection<Solicitud> Solicitudes
        {
            get
            {
                return basedatos.GetCollection<Solicitud>("solicitud");
            }
        }
    }
}
