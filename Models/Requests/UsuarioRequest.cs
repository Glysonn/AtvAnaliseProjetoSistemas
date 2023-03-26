using System.Security.Cryptography;
using System.Text;

using AttAnalise.Enums;

namespace AttAnalise.Models.Requests
{
    // !!! CLASSES REQUEST SÃO SOMENTE PARA FAZER A REQUISIÇÃO PARA O USUÁRIO. !!!
    // Pois a validação não precisa ser feita na hora que o dado for enviado, mas sim na hora que for submetido
    // EXEMPLO: No método HTTP PUT, o valor pode ser enviado como vazio, para permanecer o mesmo valor (isso é tratado na controller)

    public class UsuarioRequest
    {
        public int Id { get => _Id; set => _Id = value; }
        public string Nome { get => _Nome; set => _Nome = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string Senha { get => _Senha; set => _Senha = value; }
        public TipoUsuario TipoUsuario { get => _TipoUsuario; set => _TipoUsuario = value; }
        
        
        private int _Id { get; set; }
        private string _Nome { get; set; }
        private string _Email { get; set; }
        private string _Senha { get; set; }
        private TipoUsuario _TipoUsuario { get; set; }
    }
}