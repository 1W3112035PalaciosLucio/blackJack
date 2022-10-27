using ApiBlackJack.Models;

namespace ApiBlackJack.Commands
{
    public class UsuarioLista
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public static implicit operator UsuarioLista(Usuarios entity)
        {
            return new UsuarioLista
            {
                Id = entity.Id,
                Email = entity.Email,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido
            };
        }
    }
}
