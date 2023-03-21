namespace AttAnalise.Models
{
    public class Cliente : Usuario
    {

        public Cliente(int id, string nome, string email, string senha) : base (id, nome, email, senha)
        {

        }
    }
}