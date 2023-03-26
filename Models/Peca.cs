namespace AttAnalise.Models
{
    public class Peca : Produto
    {
        public string Arquitetura
        {
            get => _Arquitetura;
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("A arquitetura não pode ser um campo vazio! Por favor, preencha o campo corretamente");
                else
                    _Arquitetura = value;
            }
        }
        
        private string _Arquitetura { get; set; }
        public Peca(string nome, string tipo, string marca, string modelo, decimal valor, string arquitetura) : base (0, nome, tipo, marca, modelo, valor)
        {
            this.Arquitetura = arquitetura;
        }
    }
}