using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiBlackJack.Models
{
    public partial class Partidas
    {
        public Partidas()
        {
            Detallepartidas = new HashSet<Detallepartidas>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public ulong Activo { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<Detallepartidas> Detallepartidas { get; set; }
    }
}
