using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttAnalise.Models
{
    public class Administrador : Usuario
    {
        // prop publica para acessar a privada da classe base
        public int IdAdm
        {
            get => Id; 
            set => Id = value;
        }
        
        public Administrador(int id, string nome, string email, string senha) : base (id, nome, email, senha)
        {

        }
    }
}