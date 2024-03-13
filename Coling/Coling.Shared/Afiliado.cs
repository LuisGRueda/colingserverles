using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Afiliados
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdPersona { get; set; }

        [ForeignKey("IdPersona")]
        public Persona? Persona { get; set; }

        [Required]
        public DateTime FechaAfiliacion { get; set; }

        [Required]
        [StringLength(50)]
        public string? CodigoAfiliado { get; set; }

        [Required]
        [StringLength(50)]
        public string? NroTituloProvisional { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}
