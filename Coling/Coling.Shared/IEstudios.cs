﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IEstudios
    {
        public string TipoEstudio { get; set; }
        public string Afiliado { get; set; }
        public string NombreGrado { get; set; }
        public string TituloRecibido {  get; set; }
        public string Institucion { get; set; }
        public string Anio { get; set  ;}
        public string Estado { get; set;}
    }
}
