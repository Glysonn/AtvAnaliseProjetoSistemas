using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttAnalise.Models
{
    public class Administrador : Usuario
    {
        public Administrador(int id, string nome, string email, string senha) : base (id, nome, email, senha)
        {

        }
    }
}