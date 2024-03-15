using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Institucion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdTipoInstitucion { get; set; }

        [ForeignKey("IdTipoInstitucion")]
        public TipoInstitucion? TipoInstitucion { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }

        [StringLength(255)]
        public string? Direccion { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }


    }
}
