using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlackJack.DTO
{
    public class DTOPartidasUsuario
    {
        public DateTime fecha { set; get; }
        public int idUsuario { set; get; }
        public int cantidadPartidas { set; get; }
    }
}
