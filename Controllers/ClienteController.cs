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

        // MÉTODOS HTTP POST
        [HttpPost]
        public IActionResult AdicionarCliente(Cliente cliente)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                // faz validação simples dos dados de entrada
                if (!cliente.ValidarDados())
                {
                    return BadRequest("Todos os campos são obrigatórios!");
                }

                Cliente NovoCliente = new Cliente(cliente.Nome, cliente.Email, cliente.Senha);

                _context.Clientes.Add(NovoCliente);
                _context.SaveChanges();
                
                return Created("Cliente cadastrado!", NovoCliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }

        // MÉTODOS HTTP PUT
        [HttpPut("{id}")]
        public IActionResult AtualizarCliente (int id, string senhaAtual, [FromBody]Cliente cliente)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var ClienteBanco = _context.Clientes.SingleOrDefault(a => a.Id == id);
                if (ClienteBanco == null)
                {
                    return NotFound($"Cliente de ID {id} não se encontra no sistema!");
                }

                // aqui é verificado se o campo do corpo da requisição é vazio. Caso seja vazio, o dado tem que se mater o mesmo
                if (!String.IsNullOrEmpty(cliente.Nome))
                {
                    ClienteBanco.Nome = cliente.Nome;
                } if (!String.IsNullOrEmpty(cliente.Email))
                {
                    ClienteBanco.Email = cliente.Email;
                } if (!String.IsNullOrEmpty(cliente.Senha))
                {
                    ClienteBanco.Senha = cliente.Senha;
                }

                _context.Clientes.Update(ClienteBanco);
                _context.SaveChanges();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }

        // MÉTODOS HTTP DELETE
        [HttpDelete("{id}")]
        public IActionResult DeletarCliente(int id)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var ClienteBanco = _context.Clientes.SingleOrDefault(a => a.Id == id);
                if (ClienteBanco == null)
                    return NotFound($"O cliente de ID {id} não se encontra no sistema!");
                
                _context.Remove(ClienteBanco);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }
        
    }
}