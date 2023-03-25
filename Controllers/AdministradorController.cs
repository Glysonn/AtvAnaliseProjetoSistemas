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
        [HttpGet]
        public IActionResult GetAdministradores()
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;
                
                var Administradores = _context.Administradores.ToList();

                if (!Administradores.Any())
                    return NotFound("Não há nenhum dado de administradores no sistema!");

                return Ok(Administradores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAdministradoresById(int id)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;
                
                var AdministradorBanco = _context.Administradores.SingleOrDefault(a => a.Id == id);

                if (AdministradorBanco == null)
                    return NotFound($"Administrador de ID {id} não se encontra no sistema!");

                return Ok(AdministradorBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }

        // MÉTODOS HTTP POST
        [HttpPost]
        public IActionResult AdicionarAdministrador(Administrador adm)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                Administrador novoAdministrador = new Administrador(adm.Nome, adm.Email, adm.Senha);

                _context.Administradores.Add(novoAdministrador);
                _context.SaveChanges();
                
                return Created("Usuario Administrador cadastrado!", novoAdministrador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }

        // MÉTODOS HTTP PUT
        [HttpPut("{id}")]
        public IActionResult AtualizarAdministradorById (int id, string senhaAtual, [FromBody]Administrador adm)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var AdministradorBanco = _context.Administradores.SingleOrDefault(a => a.Id == id);
                if (AdministradorBanco == null)
                {
                    return NotFound($"Administrador de ID {id} não se encontra no sistema!");
                }

                // aqui é verificado se o campo do corpo da requisição é vazio. Caso seja vazio, o dado tem que se mater o mesmo
                if (!String.IsNullOrEmpty(adm.Nome))
                    AdministradorBanco.Nome = adm.Nome;

                if (!String.IsNullOrEmpty(adm.Email))
                    AdministradorBanco.Email = adm.Email;

                if (!String.IsNullOrEmpty(adm.Senha))
                    AdministradorBanco.Senha = adm.Senha;

                _context.Administradores.Update(AdministradorBanco);
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
        public IActionResult DeletarAdministradorById(int id)
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var AdministradorBanco = _context.Administradores.SingleOrDefault(a => a.Id == id);
                if (AdministradorBanco == null)
                    return NotFound($"Administrador de ID {id} não se encontra no sistema!");
                
                _context.Remove(AdministradorBanco);
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