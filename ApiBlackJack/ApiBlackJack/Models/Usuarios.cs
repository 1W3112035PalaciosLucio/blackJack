using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiBlackJack.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Partidas = new HashSet<Partidas>();
        }

        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        
        [Required(ErrorMessage = "El email no puede estar vacío.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "La contraseña no debe estar vacía.")]
        public byte[] ClaveHash { get; set; }
        public byte[] ClaveSalt { get; set; }

        [Compare("ClaveHash", ErrorMessage = "Las contraseñas no coinciden.")]
        [NotMapped]
        public string ConfirmaClave { get; set; }
        public virtual ICollection<Partidas> Partidas { get; set; }
    }
}
