using ApiBlackJack.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
