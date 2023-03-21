using AttAnalise.Enums;

namespace AttAnalise.Models
{
    public class Cliente : Usuario
    {

        public Cliente(int id, string nome, string email, string senha, TipoUsuario tipoUsuario) : base (id, nome, email, senha, tipoUsuario)
        {

        }
    }
}