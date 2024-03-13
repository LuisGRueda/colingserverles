using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class AfiliadoIdioma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdAfiliado { get; set; }

        [ForeignKey("IdAfiliado")]
        public Afiliados? Afiliado { get; set; }

        [Required]
        public int IdIdioma { get; set; }

        [ForeignKey("IdIdioma")]
        public Idioma? Idioma { get; set; }
    }
}
