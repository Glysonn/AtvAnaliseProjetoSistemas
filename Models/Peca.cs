namespace AttAnalise.Models
{
    public class Peca : Produto
    {
        public string Arquitetura { get => _Arquitetura; set => _Arquitetura = value; }
        
        private string _Arquitetura { get; set; }
        public Peca(int codigo, string nome, string tipo, string marca, string modelo, decimal valor, string arquitetura) : base (codigo, nome, tipo, marca, modelo, valor)
        {
            this.Arquitetura = arquitetura;
        }
    }
}