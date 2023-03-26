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

                var AcessoriosBanco = _context.Acessorios.ToList();

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

        [HttpGet("{codigo}")]
        public IActionResult GetAcessorioById(int codigo)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if(responseBanco != null)
                    return responseBanco;

                var AcessorioBanco = _context.Acessorios.SingleOrDefault(a => a.Codigo == codigo);
                if(AcessorioBanco == null)
                    return NotFound($"Acessorio de codigo {codigo} não existe.");
                
                return Ok(AcessorioBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!",
                                            Mensagem = ex.Message});
            }
        }


        [HttpPost]
        public IActionResult AdicionarAcessorio([FromBody] AcessorioRequest acessorio)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                Acessorio NovoAcessorio = new Acessorio(acessorio.Nome, acessorio.Tipo, acessorio.Marca, acessorio.Modelo, acessorio.Valor);

                _context.Acessorios.Add(NovoAcessorio);
                _context.SaveChanges();

                return Created("Acessorio cadastrado com sucesso!", NovoAcessorio);
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
        public IActionResult AtualizarPeca(int codigo, [FromBody] AcessorioRequest acessorio)
        {
            try
            {
                var responseBanco = ChecarConexaoBanco();
                if(responseBanco != null)
                    return responseBanco;

                var AcessorioBanco = _context.Acessorios.SingleOrDefault(a => a.Codigo == codigo);
                if(AcessorioBanco == null)
                    return NotFound($"O acessorio de código {codigo} não se encontra no sistema!");

                // aqui é verificado se o campo do corpo da requisição é vazio. Caso seja vazio, o dado tem que se mater o mesmo
                // a propriedade IsGamer (bool) não precisa de validação pois sempre terá um valor válido (true ou false)

                if (!String.IsNullOrEmpty(acessorio.Nome))
                    AcessorioBanco.Nome = acessorio.Nome;
                    
                if (!String.IsNullOrEmpty(acessorio.Marca))
                    AcessorioBanco.Marca = acessorio.Marca;

                if (!String.IsNullOrEmpty(acessorio.Modelo))
                    AcessorioBanco.Modelo = acessorio.Modelo;

                if (!String.IsNullOrEmpty(acessorio.Tipo))
                    AcessorioBanco.Tipo = acessorio.Tipo;

                // caso seja 0 (valor padrão do tipo Decimal, mantém o mesmo valor)
                if (acessorio.Valor != 0)
                    AcessorioBanco.Valor = acessorio.Valor;
            
                _context.Acessorios.Update(AcessorioBanco);
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