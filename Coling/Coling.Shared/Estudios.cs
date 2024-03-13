using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Estudios
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdTipoEstudio { get; set; }

        [ForeignKey("IdTipoEstudio")]
        public TipoEstudio? TipoEstudio { get; set; }

        [Required]
        public int IdAfiliado { get; set; }

        [ForeignKey("IdAfiliado")]
        public Afiliados? Afiliado { get; set; }

        [Required]
        public int IdGrado { get; set; }

        [ForeignKey("IdGrado")]
        public GradoAcademico? GradoAcademico { get; set; }

        [Required]
        public int IdInstitucion { get; set; }

        [ForeignKey("IdInstitucion")]
        public Institucion? Institucion { get; set; }

        [Required]
        [StringLength(255)]
        public string? TituloRecibido { get; set; }

        [Required]
        public int Anio { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}
