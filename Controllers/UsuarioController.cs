using Microsoft.AspNetCore.Mvc;

using AttAnalise.Context;
using AttAnalise.Models;

namespace AttAnalise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {   
        private readonly LojaContext _context;
        public UsuarioController(LojaContext context)
        {
            _context = context;
        }

        // método para checar a conexão com o banco de dados (caso não seja possível, retorna um error)
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
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var clientes = _context.Clientes.Select(c => new {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    Senha = c.Senha,
                    TipoUsuario = "Cliente",
                }).ToList();

                var adms = _context.Administradores.Select(a => new {
                    Id = a.Id,
                    Nome = a.Nome,
                    Email = a.Email,
                    Senha = a.Senha,
                    TipoUsuario = "Administrador"
                }).ToList();

                var todasAsListas = clientes.Concat(adms).ToList();

                return Ok(todasAsListas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }

        [HttpPost]
        public IActionResult LogInUsers(string email, string senha)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;
                    
                var userAdm = _context.Administradores.FirstOrDefault( u => u.Email == email);
                var userCliente = _context.Clientes.FirstOrDefault( c => c.Email == email);
                Usuario user = null;

                if (userAdm != null)
                    user = userAdm;

                if (userCliente != null)
                    user = userCliente;

                if (user == null)
                    return NotFound("Email ou senha inválidos!");

                senha = user.CriptografarSenha(senha);
                var isSenha = user.ConfirmarSenha(senha);

                if (isSenha)
                    return Ok();
                else
                    return NotFound("Email ou senha inválidos!");
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(new {Error = "Aconteceu um erro com os dados enviados!", Mensagem = argEx.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }
    }
}