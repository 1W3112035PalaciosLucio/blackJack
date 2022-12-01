using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlackJack.DTO
{
    public class DTOPuntos
    {
        private int puntosJugador;
        private int puntosCroupier;

        public void setPuntosJugador(int puntos)
        {
            this.puntosJugador +=  puntos;
        }
        public void setPuntosCroupier(int puntos)
        {
            this.puntosCroupier += puntos;
        }

        public int getPuntosJugador()
        {
            return this.puntosJugador;
        }
        public int getPuntosCroupier()
        {
            return this.puntosCroupier;
        }

    }
}
