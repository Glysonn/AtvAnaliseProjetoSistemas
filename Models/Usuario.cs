using System.Security.Cryptography;
using System.Text;

using AttAnalise.Enums;

namespace AttAnalise.Models
{
    public class Usuario
    {
        public int Id { get => _Id; set => _Id = value; }
        public string Nome { get => _Nome; set => _Nome= value; }
        public string Email { get => _Email; set => _Email = value; }
        public string Senha { get => _Senha; set => _Senha = value; }
        public TipoUsuario TipoUsuario { get => _TipoUsuario; set => _TipoUsuario = value; }
        
        
        private int _Id { get; set; }
        private string _Nome { get; set; }
        private string _Email { get; set; }
        private string _Senha { get; set; }
        private TipoUsuario _TipoUsuario { get; set; }

        public Usuario(int id, string nome, string email, string senha)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = CriptografarSenha(senha);
        }

        // a criptografia não é das mais efetivas porém servirá para demonstração
        private string CriptografarSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerificarSenha(string senha)
        {
            return (CriptografarSenha(senha) == this.Senha) ? true : false;
        }
        // nesse caso, como a cripto é simples, não faz muito sentido. Porém continua sendo
        // uma boa prática restringir ao máximo o acesso à esse tipo de método
        public bool ConfirmarSenha(string senha)
        {
            return VerificarSenha(senha);
        }
        

        public void AlterarSenha(string senhaAtual, string novaSenha)
        {
            if (!VerificarSenha(senhaAtual))
            {
                throw new Exception ("Ops! A senha atual não coincide!");
            }
            this.Senha = CriptografarSenha(novaSenha);
        }
    }
}