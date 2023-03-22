using Microsoft.AspNetCore.Mvc;

using AttAnalise.Context;

namespace AttAnalise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly LojaContext _context;
        public AdministradorController(LojaContext context)
        {
            _context = context;
        }
    }

}