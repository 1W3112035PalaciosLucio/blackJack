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

    }
}
