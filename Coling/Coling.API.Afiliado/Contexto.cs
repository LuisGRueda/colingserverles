using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Coling.API.Afiliado
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Afiliados> Afiliados { get; set; }
        public virtual DbSet<AfiliadoIdioma> AfiliadosIdiomas { get; set; }
        public virtual DbSet<Direccion> Direcciones { get; set; }
        public virtual DbSet<Estudios> Estudios { get; set; }
        public virtual DbSet<ExperienciaLaboral> ExperienciasLaborales { get; set; }
        public virtual DbSet<GradoAcademico> GradosAcademicos { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<Institucion> Instituciones { get; set; }
        public virtual DbSet<PersonatipoSocial> PersonasTiposSociales { get; set; }
        public virtual DbSet<Profesion> Profesiones { get; set; }
        public virtual DbSet<ProfesionAfiliado> ProfesionesAfiliados { get; set; }
        public virtual DbSet<Telefono> Telefonos { get; set; }
        public virtual DbSet<TipoEstudio> TiposEstudios { get; set; }
        public virtual DbSet<TipoInstitucion> TiposInstituciones { get; set; }
        public virtual DbSet<TipoSocial> TiposSociales { get; set; }

    }
}