using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace AttAnalise.Models
{
    public class Usuario
    {
        private int Id { get; set; }
        private string Nome { get; set; }
        private string Email { get; set; }
        private string Senha { get; set; }
        private TipoUsuario TipoUsuario { get; set; }

        public Usuario(int id, string nome, string email, string senha)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = CriptografarSenha(senha);
        }

        public void descricao(){Console.WriteLine($"id: {Id}\nnome: {Nome}\nemail: {Email}\nsenha: {Senha}");}


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