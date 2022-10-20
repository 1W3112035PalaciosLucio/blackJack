using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBlackJack.Commands
{
    public class UsuarioComando
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
    }
}
