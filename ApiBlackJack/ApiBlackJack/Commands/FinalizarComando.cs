using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlackJack.Commands
{
    public class FinalizarComando
    {
        public int IdPartida { set; get; }
        public float PuntosJugador { set; get; }
        public float PuntosCrupier { set; get; }
    }
}
