using ApiBlackJack.Commands;
using ApiBlackJack.DataContext;
using ApiBlackJack.Models;
using ApiBlackJack.Results;
using Microsoft.AspNetCore.Authorization;
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
    public class PartidaController : ControllerBase
    {
        private readonly BaseBlackJackContext context;
        private List<Cartas> cartasJugador = new List<Cartas>();
        private List<Cartas> cartasCrupier = new List<Cartas>();

        public PartidaController(BaseBlackJackContext _context)
        {
            this.context = _context;

        }
        [HttpPost]
        [Route("iniciarPartida/{email}")]
        public async Task<ActionResult<ResultadoBase>> iniciarPartida(string email)
        {
            ResultadoBase result = new ResultadoBase();
            var usuario = await context.Usuarios.Where(c => c.Email.Equals(email)).FirstOrDefaultAsync();
            if (usuario == null)
            {
                result.setError("Error al iniciar la partida");
                return BadRequest(result);
            }
            else
            {
                Partidas p = new Partidas();
                p.Activo = true;
                p.IdUsuario = usuario.Id;
                p.Fecha = DateTime.Today;
                p.IdUsuarioNavigation = usuario;
                p.PuntosCrupier = 0;
                p.PuntosJugador = 0;

                context.Add(p);
                await context.SaveChangesAsync();

                result.setOk();
                return Ok(p.Id);
            }
        }

        [HttpPost]
        [Route("agregarDetallePartida")]
        public async Task<ActionResult> agregarJugada([FromBody] JugadaComando comando)
        {
            Detallepartidas detalle = new Detallepartidas();
            detalle.IdCarta = comando.IdCarta;
            detalle.IdPartida = comando.IdPartida;
            ResultadoBase result = new ResultadoBase();


            try
            {
                context.Detallepartidas.Add(detalle);
                await context.SaveChangesAsync();
                result.setOk();
                return Ok(result);

            }
            catch (Exception)
            {

                result.setError("Algo malo ocurrio");
                return BadRequest(result);
            }

        }


        [HttpPut]
        [Route("finalizarJuego")]
        public async Task<ActionResult> finalizarJuego([FromBody] FinalizarComando comando)
        {
            ResultadoBase result = new ResultadoBase();
            var partida = await context.Partidas.Where(c => c.Id.Equals(comando.IdPartida)).FirstOrDefaultAsync();

            try
            {
                partida.PuntosCrupier = comando.PuntosCrupier;
                partida.PuntosJugador = comando.PuntosJugador;
                partida.Activo = false;
                context.Update(partida);
                await context.SaveChangesAsync();
                result.setOk();
                return Ok(result);

            }
            catch (Exception)
            {
                result.setError("Algo malo ocurrio");
                return BadRequest(result);
            }


        }

        [HttpGet]
        [Route("obtenerPartidas")]
        public async Task<ActionResult> obtenerPartida()
        {
            var partidas = await context.Partidas.ToListAsync();
            return Ok(partidas);
        }

        [HttpGet]
        [Route("obtenerPartidaById/{id}")]
        public async Task<ActionResult> obtenerPartidaById(int id)
        {
            ResultadoBase result = new ResultadoBase();
            var partida = await context.Partidas.Where(c=>c.Id.Equals(id)).FirstOrDefaultAsync();
            if(partida == null)
            {
                result.setError("No existe esa partida");
                return BadRequest(result);
            }
            return Ok(partida);
        }

        [HttpGet]
        [Route("obtenerDetallesPartidaById/{id}")]
        public async Task<ActionResult> obtenerDetallesPartidaById(int id)
        {
            ResultadoBase result = new ResultadoBase();
            var partidas = await context.Detallepartidas.Where(c => c.IdPartida.Equals(id)).ToListAsync();
            if (partidas == null)
            {
                result.setError("No existe esa partida");
                return BadRequest(result);
            }
            return Ok(partidas);
        }

        [HttpGet]
        [Route("obtenerPartidasUsuario/{id}")]
        public async Task<ActionResult> obtenerPartidasById(int id)
        {
            ResultadoBase result = new ResultadoBase();
            var partida = await context.Partidas.Where(c => c.IdUsuario.Equals(id)).ToListAsync();
            if (partida == null)
            {
                result.setError("No existe esa partida");
                return BadRequest(result);
            }
            return Ok(partida);
        }

        [HttpGet]
        [Route("obtenerCartas")]
        public async Task<ActionResult> obtenerCartas()
        {
            var cartas = await context.Cartas.ToListAsync();
            return Ok(cartas);
        }

        [HttpGet]
        [Route("obtenerCartasById/{id}")]
        public async Task<ActionResult> obtenerCartas(int id)
        {
            var cartas = await context.Cartas.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return Ok(cartas);
        }


        [HttpGet]
        [Route("iniciarJugador")]
        public async Task<ActionResult> iniciarJugador()
        {
            this.cartasJugador.Clear();
            List<Cartas> cartasJugador = new List<Cartas>();
            Random r = new Random();
            for (int i = 0; i < 2; i++)
            {
                var id = r.Next(1, 57);
                Cartas? carta = await context.Cartas.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                this.cartasJugador.Add(carta);
            }

            return Ok(this.cartasJugador);
        }

        [HttpGet]
        [Route("iniciarCrupier")]
        public async Task<ActionResult> iniciarCupier()
        {
            this.cartasCrupier.Clear();
            Random r = new Random();
            var id = r.Next(1, 57);
            Cartas carta = await context.Cartas.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
            this.cartasCrupier.Add(carta);

            return Ok(this.cartasCrupier);
        }

        [HttpGet]
        [Route("pedirCarta")]
        public async Task<ActionResult> pedirCarta()
        {
            Cartas carta = new Cartas();
            Random r = new Random();
            var bandera = false;
            while (bandera == false)
            {
                var id = r.Next(1, 57);
                carta = await context.Cartas.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (!this.cartasJugador.Contains(carta))
                {
                    this.cartasJugador.Add(carta);
                    bandera = true;
                }
            }
            return Ok(carta);
        }
    }
}
