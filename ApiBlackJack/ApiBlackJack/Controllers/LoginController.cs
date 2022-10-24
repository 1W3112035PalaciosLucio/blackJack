using ApiBlackJack.Commands;
using ApiBlackJack.DataContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiBlackJack.Helper;
using ApiBlackJack.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace ApiBlackJack.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly BaseBlackJackContext context;
        private readonly IConfiguration config;
        public LoginController(BaseBlackJackContext _context, IConfiguration _config)
        {
            this.context = _context;
            this.config = _config;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginPost(LoginComando comando)
                        {
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            }

            Usuarios usuario = await context.Usuarios.Where(x => x.Email == comando.Email).FirstOrDefaultAsync();
            if(usuario == null)
            {
                return NotFound(ErrorHelper.Response(404, "Usuario no encontrado"));
            }
            if(HashHelper.CheckHash(comando.Clave, usuario.ClaveHash, usuario.ClaveSalt))
            {
                var secretKey = config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, comando.Email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);
                return Ok(bearer_token);

            }
            else
            {
                return Forbid();
            }
        }

        //[HttpGet]
        //[Route("getA")]
        //public IActionResult GetA()
        //{
        //    var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
        //    return Ok(r == null ? "" : r.Value);
        //}

    }
}
