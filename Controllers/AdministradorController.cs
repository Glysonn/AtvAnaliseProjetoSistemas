using Microsoft.AspNetCore.Mvc;

using AttAnalise.Context;
using AttAnalise.Models;

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

        // MÉTODOS HTTP GET
        [HttpGet]
        public IActionResult GetAdministradores()
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                if (!_context.Database.CanConnect())
                    return Problem( detail: "Erro: O banco de dados não foi encontrado!",
                                    statusCode: StatusCodes.Status400BadRequest,
                                    title: "Bad Request");
                
                var Administradores = _context.Administradores.ToList();

                if (!Administradores.Any())
                    return NotFound("Não há nenhum dado de administradores no sistema!");

                return Ok(Administradores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = ex.Message, Mensagem = "Aconteceu um erro interno no servidor!"});
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAdministradoresById(int id)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                if (!_context.Database.CanConnect())
                    return Problem( detail: "Houve um problema com a conexão ao banco de dados",
                                    statusCode: StatusCodes.Status400BadRequest,
                                    title: "Bad Request");
                
                var AdministradorBanco = _context.Administradores.SingleOrDefault(a => a.Id == id);

                if (AdministradorBanco == null)
                    return NotFound($"Administrador de ID {id} não se encontra no sistema!");

                return Ok(AdministradorBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = ex.Message, Mensagem = "Aconteceu um erro interno no servidor!"});
            }
        }

        // MÉTODOS HTTP POST
        [HttpPost]
        public IActionResult AdicionarAdministrador(Administrador adm)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                if (!_context.Database.CanConnect())
                    return Problem( detail: "Houve um problema com a conexão ao banco de dados",
                                    statusCode: StatusCodes.Status400BadRequest,
                                    title: "Bad Request");

                // faz validação simples dos dados de entrada
                if (!adm.ValidarDados())
                {
                    return BadRequest("Todos os campos são obrigatórios!");
                }

                Administrador novoAdministrador = new Administrador(adm.Nome, adm.Email, adm.Senha);

                _context.Administradores.Add(novoAdministrador);
                _context.SaveChanges();
                
                return Created("Usuario Administrador cadastrado!", novoAdministrador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = ex.Message, Mensagem = "Aconteceu um erro interno no servidor!"});
            }
        }

    }
}