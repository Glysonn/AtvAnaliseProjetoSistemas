using Microsoft.AspNetCore.Mvc;
using AttAnalise.Context;
using AttAnalise.Models;
using AttAnalise.Models.Requests;

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
                    return NotFound("Não há nenhum periférico cadastrado no sistema!");

                return Ok(PerifericosBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }

        [HttpGet("{codigo}")]
        public IActionResult GetPerifericosById(int codigo)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if(responseBanco != null)
                    return responseBanco;

                var PerifericoBanco = _context.Perifericos.SingleOrDefault(a => a.Codigo == codigo);
                if(PerifericoBanco == null)
                    return NotFound($"Periférico de codigo {codigo} não existe.");
                
                return Ok(PerifericoBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }

        [HttpPost]
        public IActionResult AdicionarPeriferico([FromBody] PerifericoRequest pf)
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

        [HttpPut ("{codigo}")]
        public IActionResult AtualizarPeriferico(int codigo, [FromBody] PerifericoRequest pf)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if(responseBanco != null)
                    return responseBanco;

                var PerifericoBanco = _context.Perifericos.SingleOrDefault(a => a.Codigo == codigo);
                if(PerifericoBanco == null)
                    return NotFound($"O periférico de código {codigo} não se encontra no sistema!");

                // aqui é verificado se o campo do corpo da requisição é vazio. Caso seja vazio, o dado tem que se mater o mesmo
                // a propriedade IsGamer (bool) não precisa de validação pois sempre terá um valor válido (true ou false)

                if (!String.IsNullOrEmpty(pf.Nome))
                    PerifericoBanco.Nome = pf.Nome;
                    
                if (!String.IsNullOrEmpty(pf.Marca))
                    PerifericoBanco.Marca = pf.Marca;

                if (!String.IsNullOrEmpty(pf.Modelo))
                    PerifericoBanco.Modelo = pf.Modelo;

                if (!String.IsNullOrEmpty(pf.Tipo))
                    PerifericoBanco.Tipo = pf.Tipo;

                // caso seja 0 (valor padrão do tipo Decimal, mantém o mesmo valor)
                if (pf.Valor != 0)
                    PerifericoBanco.Valor = pf.Valor;
            
                _context.Perifericos.Update(PerifericoBanco);
                _context.SaveChanges();

                return NoContent();
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
        
        [HttpDelete ("{codigo}")]
        public IActionResult DeletarPerifericoById(int codigo)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;
                
                var PerifericoBanco = _context.Perifericos.SingleOrDefault(a => a.Codigo == codigo);
                if (PerifericoBanco == null)
                    return NotFound($"O periférico de código {codigo} não se encontra no sistema!");
                
                _context.Perifericos.Remove(PerifericoBanco);
                _context.SaveChanges();

                return NoContent();
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