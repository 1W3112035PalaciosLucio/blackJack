using ApiBlackJack.DataContext;
using ApiBlackJack.DTO;
using ApiBlackJack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlackJack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly BaseBlackJackContext context;
        public ReportesController(BaseBlackJackContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        [Route("IndiceVictoriasCrupier")]
        public async Task<float> getIndiceCroupier()
        {
            var partidas = await context.Partidas.ToListAsync();
            var query = await context.Partidas.Where(c => c.PuntosCrupier > c.PuntosJugador && c.PuntosCrupier <= 21 || c.PuntosJugador > 21).ToListAsync();

            var cantidadPartidas = partidas.ToArray().Length;
            var vecesganoCroupier = query.ToArray().Length;
            float retorno = (vecesganoCroupier * 100) / cantidadPartidas;

            return retorno;
        }

        [HttpGet]
        [Route("CantidadJuegosPorDia/{id}")]
        public async Task<List<DTOPartidasUsuario>> getCantidadPartidas(int id)
        {

            var query = context.Partidas.Where(c => c.IdUsuario.Equals(id)).GroupBy(x => new { x.Fecha, x.IdUsuario }).
                Select(x => new DTOPartidasUsuario {
                    fecha = x.Key.Fecha,
                    idUsuario = x.Key.IdUsuario,
                    cantidadPartidas = x.Count()
                }).ToList();
                


            return query;
        }


        [HttpGet]
        [Route("promedioJugadas21/{id}")]
        public async Task<IndiceCroupierDTO> getPromedioJugadas(int id)
        {

            var partidas = await context.Partidas.ToListAsync();
            var Crupier21 = await context.Partidas.Where(c => c.PuntosCrupier == 21 && c.IdUsuario.Equals(id)).ToListAsync();
            var Jugador21 = await context.Partidas.Where(c => c.PuntosJugador == 21 && c.IdUsuario.Equals(id)).ToListAsync();


            
            var veces21Croupier = Crupier21.ToArray().Length;
            var veces21Jugador = Jugador21.ToArray().Length;
            var cantidadPartidas = veces21Croupier+veces21Jugador;

            IndiceCroupierDTO resultado = new IndiceCroupierDTO();
            if (cantidadPartidas > 0)
            {
                resultado.promedioCroupier = (veces21Croupier * 100) / cantidadPartidas;
                resultado.promedioJugadores = (veces21Jugador * 100) / cantidadPartidas;
            }
            return resultado;
  
        }



    }
}
