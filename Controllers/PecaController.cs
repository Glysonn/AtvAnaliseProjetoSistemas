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


        [HttpPost]
        public IActionResult AdicionarPeca([FromBody] PecaRequest peca)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                Peca NovaPeca = new Peca(peca.Nome, peca.Tipo, peca.Marca, peca.Modelo, peca.Valor, peca.Arquitetura);

                _context.Pecas.Add(NovaPeca);
                _context.SaveChanges();

                return Created("Produto cadastrado com sucesso!", NovaPeca);
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
        public IActionResult AtualizarPeca(int codigo, [FromBody] PecaRequest peca)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if(responseBanco != null)
                    return responseBanco;

                var PecaBanco = _context.Pecas.SingleOrDefault(a => a.Codigo == codigo);
                if(PecaBanco == null)
                    return NotFound($"A peça de código {codigo} não se encontra no sistema!");

                // aqui é verificado se o campo do corpo da requisição é vazio. Caso seja vazio, o dado tem que se mater o mesmo
                // a propriedade IsGamer (bool) não precisa de validação pois sempre terá um valor válido (true ou false)

                if (!String.IsNullOrEmpty(peca.Nome))
                    PecaBanco.Nome = peca.Nome;
                    
                if (!String.IsNullOrEmpty(peca.Marca))
                    PecaBanco.Marca = peca.Marca;

                if (!String.IsNullOrEmpty(peca.Modelo))
                    PecaBanco.Modelo = peca.Modelo;

                if (!String.IsNullOrEmpty(peca.Tipo))
                    PecaBanco.Tipo = peca.Tipo;
                
                if(!String.IsNullOrEmpty(peca.Arquitetura))
                    PecaBanco.Arquitetura = peca.Arquitetura;

                // caso seja 0 (valor padrão do tipo Decimal, mantém o mesmo valor)
                if (peca.Valor != 0)
                    PecaBanco.Valor = peca.Valor;
            
                _context.Pecas.Update(PecaBanco);
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