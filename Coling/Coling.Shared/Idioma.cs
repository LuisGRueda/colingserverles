using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Idioma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(55)]
        public string? NombreIdioma { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        public List<AfiliadoIdioma>? Afiliados { get; set; }
    }
}
