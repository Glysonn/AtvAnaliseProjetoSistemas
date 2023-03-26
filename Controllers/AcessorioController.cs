using Microsoft.AspNetCore.Mvc;
using AttAnalise.Context;

using AttAnalise.Models;
using AttAnalise.Models.Requests;

namespace AttAnalise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcessorioController : ControllerBase
    {
        private readonly LojaContext _context;
        public AcessorioController (LojaContext context)
        {
            _context = context;
        }
        private IActionResult ChecarConexaoBanco()
        {
            if (!_context.Database.CanConnect())
            {
                return Problem(
                    detail: "Houve um problema com a conexão ao banco de dados",
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error");
            }
            return null;
        }

    }
}