using Microsoft.AspNetCore.Mvc;

using AttAnalise.Context;

namespace AttAnalise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {   
        private readonly LojaContext _context;
        public ProdutoController(LojaContext context)
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
        [HttpGet("GetAllProdutos")]
        public IActionResult GetAllProdutos()
        {
            try
            {
                // verifica se há algo de errado com a conexão do banco de dados
                var responseBanco = ChecarConexaoBanco();
                if (responseBanco != null)
                    return responseBanco;

                var perifericos = _context.Perifericos.Select(p => new { 
                    Codigo = p.Codigo, 
                    Nome = p.Nome, 
                    Marca = p.Marca, 
                    Modelo = p.Modelo, 
                    Valor = p.Valor, 
                    Tipo = p.Tipo, 
                    Categoria = "Periférico" 
                }).ToList();

                var acessorios = _context.Acessorios.Select(a => new { 
                    Codigo = a.Codigo, 
                    Nome = a.Nome, 
                    Marca = a.Marca, 
                    Modelo = a.Modelo, 
                    Valor = a.Valor, 
                    Tipo = a.Tipo, 
                    Categoria = "Acessórios" 
                }).ToList();

                var pecas = _context.Pecas.Select(p => new { 
                    Codigo = p.Codigo, 
                    Nome = p.Nome, 
                    Marca = p.Marca, 
                    Modelo = p.Modelo, 
                    Valor = p.Valor, 
                    Tipo = p.Tipo, 
                    Categoria = "Peças"
                }).ToList();

                var TodosProdutos = perifericos
                                    .Concat(acessorios)
                                    .Concat(pecas)
                                    .ToList();


                return Ok(TodosProdutos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Error = "Aconteceu um erro interno no servidor!", Mensagem = ex.Message});
            }
        }
    }
}