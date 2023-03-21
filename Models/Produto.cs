namespace AttAnalise.Models
{
    public class Produto
    {
        public int Codigo { get => _Codigo; set => _Codigo = value; }
        public string Nome { get => _Nome; set => _Nome = value; }
        public string Tipo { get => _Tipo; set => _Tipo = value; }
        public string Marca { get => _Marca; set => _Marca = value; }
        public string Modelo { get => _Modelo; set => _Modelo = value; }
        public decimal Valor { get => _Valor; set => _Valor = value; }
        
        
        
        
        private int _Codigo { get; set; }
        private string _Nome { get; set; }
        private string _Tipo { get; set; }
        private string _Marca { get; set; }
        private string _Modelo { get; set; }
        private decimal _Valor { get; set; }


        public Produto (int codigo, string nome, string tipo, string marca, string modelo, decimal valor)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Tipo = tipo;
            this.Marca = marca;
            this.Modelo = modelo;
            this.Valor = valor;
        }

    }
}