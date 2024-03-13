using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Ci { get; set; }

        [Required]
        [StringLength(70)]
        public string? Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string? Apellidos { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string? Foto { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        public List<Direccion>? Direcciones { get; set; }

        public List<Telefono>? Telefonos { get; set; }

        public List<PersonatipoSocial>? TiposSociales { get; set; }


    }
}