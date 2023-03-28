using System.Security.Cryptography;
using System.Text;

using AttAnalise.Enums;

namespace AttAnalise.Models
{
    public abstract class Usuario
    {
        public int Id { get => _Id; set => _Id = value; }
        public string Nome
        {   get => _Nome;
            set
            {
                if(String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome não pode ser vazio ou conter somente espaços em branco! Por favor, preencha o campo corretamente");
                else
                    _Nome = value;
            }
        }
        public string Email
        {   get => _Email;
            set
            {
                if(String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O email não pode ser vazio ou conter somente espaços em branco! Por favor, preencha o campo corretamente");
                else
                    _Email = value;
            }
        }
        public string Senha
        {   get => _Senha;
            set
            {
                if(String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A senha não pode ser vazia ou conter somente espaços em branco! Por favor, preencha o campo corretamente");
                else
                    _Senha = value;
            }
        }
        public TipoUsuario TipoUsuario { get => _TipoUsuario; set => _TipoUsuario = value; }
        
        
        private int _Id { get; set; }
        private string _Nome { get; set; }
        private string _Email { get; set; }
        private string _Senha { get; set; }
        private TipoUsuario _TipoUsuario { get; set; }

        public Usuario(int id, string nome, string email, string senha, TipoUsuario tipoUsuario)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = CriptografarSenha(senha);
            this.TipoUsuario = tipoUsuario;
        }

        // a criptografia não é das mais efetivas porém servirá para demonstração
        public string CriptografarSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool ConfirmarSenha(string senha)
        {
            return (CriptografarSenha(senha) == this._Senha);
        }
        

        public void AlterarSenha(string senhaAtual, string novaSenha)
        {
            if (!ConfirmarSenha(senhaAtual))
            {
                throw new Exception ("Ops! A senha atual não coincide!");
            }
            this.Senha = novaSenha;
        }

    }
}