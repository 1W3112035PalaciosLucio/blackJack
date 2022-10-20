using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string ClaveHash { get; set; }
        public string ClaveSalt { get; set; }

        public virtual ICollection<Partidas> Partidas { get; set; }
    }
}
