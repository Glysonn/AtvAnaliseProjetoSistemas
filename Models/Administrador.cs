using AttAnalise.Enums;

namespace AttAnalise.Models
{
    public class Administrador : Usuario
    {
        

        public Administrador(string nome, string email, string senha) : base (0, nome, email, senha, TipoUsuario.Administrador)
        {
            // o valor de TipoUsuario é com valor padrão: Administrador (é passado para a classe pai (Usuario) através do construtor)
            // o valor de ID é definido passado como 0 pois é AutoIncrement, então não é preciso especificar
        }
    }
}