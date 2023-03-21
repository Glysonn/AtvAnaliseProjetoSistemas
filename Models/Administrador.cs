using AttAnalise.Enums;

namespace AttAnalise.Models
{
    public class Administrador : Usuario
    {

        public Administrador(int id, string nome, string email, string senha, TipoUsuario tipoUsuario) : base (id, nome, email, senha, tipoUsuario)
        {

        }
    }
}