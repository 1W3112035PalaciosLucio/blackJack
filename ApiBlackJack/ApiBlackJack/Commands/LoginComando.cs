using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlackJack.Commands
{
    public class LoginComando
    {
        [Required(ErrorMessage ="El email es requerido")]
        public string Email { set; get; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Clave { set; get; }
    }
}
