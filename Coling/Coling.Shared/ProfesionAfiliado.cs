using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class ProfesionAfiliado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdAfiliado { get; set; }

        [ForeignKey("IdAfiliado")]
        public Afiliados? Afiliado { get; set; }

        [Required]
        public int IdProfesion { get; set; }


        [Required]
        public DateTime FechaAsignacion { get; set; }

        [Required]
        [StringLength(25)]
        public string? NroSelloSIB { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}
