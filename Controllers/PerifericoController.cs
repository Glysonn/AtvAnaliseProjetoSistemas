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
                // verifica se há algo de errado com a conexão do banco de dados
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

        [HttpGet("{id}")]
        public IActionResult GetPerifericosById(int id)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if(responseBanco != null)
                    return responseBanco;

                var PerifericoBanco = _context.Perifericos.SingleOrDefault(a => a.Codigo == id);
                if(PerifericoBanco == null)
                    return NotFound($"Periférico de ID {id} não existe.");
                
                return Ok(PerifericoBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }

        [HttpPost]
        public IActionResult testePost([FromBody] Periferico pf)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                Periferico NovoPeriferico = new Periferico(pf.Nome, pf.Tipo, pf.Marca, pf.Modelo, pf.Valor, pf.IsGamer);

                _context.Perifericos.Add(NovoPeriferico);
                _context.SaveChanges();

                return Created("Produto cadastrado com sucesso!", NovoPeriferico);
            }
            catch (ArgumentException argEx)
            {
                return StatusCode(400, new {Error= "Houve um erro com sua requisição. Por favor, verifique os seus dados!",
                                            Mensagem = argEx.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }

    }
}