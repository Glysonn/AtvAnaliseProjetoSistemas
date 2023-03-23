using Microsoft.AspNetCore.Mvc;

using AttAnalise.Context;
using AttAnalise.Models;

namespace AttAnalise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly LojaContext _context;
        public ClienteController(LojaContext context)
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


        // MÉTODOS HTTP GET
        [HttpGet]
        public IActionResult GetClientes()
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;
                
                var Clientes = _context.Clientes.ToList();

                if (!Clientes.Any())
                    return NotFound("Não há nenhum dado de clientes no sistema!");

                return Ok(Clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetClientesById(int id)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;
                
                var ClienteBanco = _context.Clientes.SingleOrDefault(a => a.Id == id);

                if (ClienteBanco == null)
                    return NotFound($"O Cliente de ID {id} não se encontra no sistema!");

                return Ok(ClienteBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }

    }
}