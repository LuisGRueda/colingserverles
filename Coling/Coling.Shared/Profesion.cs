using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Profesion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string? NombreProfesion { get; set; }

        [Required]
        public int IdGrado { get; set; }

        [ForeignKey("IdGrado")]
        public GradoAcademico? GradoAcademico { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        public List<ProfesionAfiliado>? Afiliados { get; set; }
    }
}
