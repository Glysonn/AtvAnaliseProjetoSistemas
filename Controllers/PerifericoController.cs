using Microsoft.AspNetCore.Mvc;
using AttAnalise.Context;

namespace AttAnalise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerifericoController : ControllerBase
    {
        private readonly LojaContext _context;
        public PerifericoController (LojaContext context)
        {
            //
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