using ApiBlackJack.DataContext;
using ApiBlackJack.Results;
using ApiBlackJack.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApiBlackJack.Helper;
using ApiBlackJack.Models;

namespace ApiBlackJack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private byte[] GetHash(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SHA256Managed().ComputeHash(bytes);
        }

        private readonly BaseBlackJackContext context;

        public UsuarioController(BaseBlackJackContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        [Route("getUsuarios")]
        public async Task<ActionResult> getUsuarios()
        {
            var lista = await context.Usuarios.ToListAsync();

            return Ok(lista);
        }

        [HttpPost]
        [Route("loginUsuario")]
        public async Task<ActionResult<ResultadoBase>> postLogin([FromBody] LoginComando comando)
        {
            ResultadoBase resultado = new ResultadoBase();
            byte[] ePass = GetHash(comando.Clave);
            var usuario = await context.Usuarios.Where(c => c.Email.Equals(comando.Email) && c.ClaveHash.Equals(ePass)).FirstOrDefaultAsync();
            if (usuario != null)
            {
                resultado.setOk();
                return Ok(resultado);
            }
            else
            {
                resultado.setError("Usuario no encontrado");
                return BadRequest(resultado);
            }
        }

        [HttpGet]
        [Route("getUsuarios/{id}")]
        public async Task<ActionResult<ResultadoBase>> getUsuarioById(int id)
        {
            var usuario = await context.Usuarios.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
            ResultadoBase resultado = new ResultadoBase();
            if(usuario == null)
            {
                resultado.setError("Usuario no encontrado");
                return BadRequest(resultado);
            }
            else
            {
                return Ok(usuario);
            }

        }


        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            List<UserComando> Usuarios = await context.Usuarios.Select(x => new UserComando()
            {
                idUsuario = x.Id,
                usuario = x.Email
            }).ToListAsync();
            return Ok(Usuarios);
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register(RegisterComando Usuario)
        {
            Usuarios u = new Usuarios();
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            }

            if (await context.Usuarios.Where(x => x.Email == Usuario.Email).AnyAsync())
            {
                return BadRequest(ErrorHelper.Response(400, $"El usuario {Usuario.Email} ya existe."));
            }

            HashedPassword Password = HashHelper.Hash(Usuario.ClaveHash);
            u.Nombre = Usuario.Nombre;
            u.Apellido = Usuario.Apellido;
            u.Email = Usuario.Email;
            u.ClaveHash = Password.Password;
            u.ClaveSalt = Password.Salt;
            context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = Usuario.Id }, new UserComando()
            {
                idUsuario = Usuario.Id,
                usuario = Usuario.Email
            });
        }

    }
}
