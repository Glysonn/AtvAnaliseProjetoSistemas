namespace AttAnalise.Models
{
    public class Periferico : Produto
    {
        public bool IsGamer { get => _IsGamer; set => _IsGamer = value; }
        private bool _IsGamer { get; set; }
        
        
        public Periferico(int codigo, string nome, string tipo, string marca, string modelo, decimal valor, bool isGamer) : base (codigo, nome, tipo, marca, modelo, valor)
        {
            this.IsGamer = isGamer;
        }

    }
}