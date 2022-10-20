using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBlackJack.Commands
{
    public class UserComando
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El legajo es requerido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string ClaveHash { get; set; }
        [Compare("ClaveHash", ErrorMessage = "Las claves deben ser iguales")]
        [NotMapped]
        public string ConfirmarClave { get; set; }

        public string ClaveSalt { get; set; }
    }
}
