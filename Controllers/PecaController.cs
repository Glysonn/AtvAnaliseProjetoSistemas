using Microsoft.AspNetCore.Mvc;
using AttAnalise.Context;

using AttAnalise.Models;
using AttAnalise.Models.Requests;

namespace AttAnalise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PecaController : ControllerBase
    {
        private readonly LojaContext _context;
        public PecaController (LojaContext context)
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
        public IActionResult GetPecas()
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var PecasBanco = _context.Pecas.ToList();

                if (!PecasBanco.Any())
                    return NotFound("Não há nenhuma peça cadastrada no sistema!");

                return Ok(PecasBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }

        [HttpGet("{codigo}")]
        public IActionResult GetPecasById(int codigo)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if(responseBanco != null)
                    return responseBanco;

                var PecaBanco = _context.Pecas.SingleOrDefault(a => a.Codigo == codigo);
                if(PecaBanco == null)
                    return NotFound($"Peça de codigo {codigo} não existe.");
                
                return Ok(PecaBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }

    }
}