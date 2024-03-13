using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Telefono
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdPersona { get; set; }

        [ForeignKey("IdPersona")]
        public Persona ?Persona { get; set; }

        [Required]
        [StringLength(20)]
        public string? NroTelefono { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }
    }
}
