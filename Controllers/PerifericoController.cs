using Microsoft.AspNetCore.Mvc;
using AttAnalise.Context;
using AttAnalise.Models;

namespace AttAnalise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerifericoController : ControllerBase
    {
        private readonly LojaContext _context;
        public PerifericoController (LojaContext context)
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

        [HttpGet]
        public IActionResult GetPerifericos()
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var PerifericosBanco = _context.Perifericos.ToList();

                if (!PerifericosBanco.Any())
                    return NotFound("Não há nenhum dado de clientes no sistema!");

                return Ok(PerifericosBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }

    }
}