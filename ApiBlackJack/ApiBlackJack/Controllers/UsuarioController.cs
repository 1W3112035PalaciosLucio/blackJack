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
using Microsoft.AspNetCore.Authorization;

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
        [Route("getUser")]
        public async Task<IActionResult> GetUser()
        {
            List<UsuarioComando> Usuarios = await context.Usuarios.Select(x => new UsuarioComando()
            {
                IdUsuario = x.Id,
                Usuario = x.Email,
            }).ToListAsync();
            return Ok(Usuarios);
        }

        [HttpGet]
        [Route("getUserId/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UsuarioComando Usuarios = await context.Usuarios.Where(x => x.Id == id).Select(x => new UsuarioComando()
            {
                IdUsuario = x.Id,
                Usuario = x.Email
            }).SingleOrDefaultAsync();
            return Ok(Usuarios);
        }

        [HttpPost]
        [Route("CrearUsuario")]

        public async Task<IActionResult> Post(UserComando Usuario)
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


            byte[] passwordSalt;
            byte[] passwordHash;

            HashHelper.Hash(Usuario.Password, out passwordSalt,out passwordHash);
            u.Nombre = Usuario.Nombre;
            u.Apellido = Usuario.Apellido;
            u.Email = Usuario.Email;
            u.ClaveHash = passwordHash;
            u.ClaveSalt = passwordSalt;
            context.Usuarios.Add(u);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = Usuario.Id }, new UsuarioComando()
            {
                IdUsuario = Usuario.Id,
                Usuario = Usuario.Email
            });
        }

    }
}
