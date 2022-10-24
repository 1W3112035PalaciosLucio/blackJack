using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiBlackJack.Models
{
    public partial class Detallepartidas
    {
        public int Id { get; set; }
        public int IdPartida { get; set; }
        public int IdCarta { get; set; }

        public virtual Partidas IdPartidaNavigation { get; set; }
    }
}
