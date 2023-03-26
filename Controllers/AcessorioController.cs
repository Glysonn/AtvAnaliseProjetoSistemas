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

        [HttpGet]
        public IActionResult GetAcessorios()
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var AcessoriosBanco = _context.Pecas.ToList();

                if (!AcessoriosBanco.Any())
                    return NotFound("Não há nenhum acessório cadastrado no sistema!");

                return Ok(AcessoriosBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }

    }
}