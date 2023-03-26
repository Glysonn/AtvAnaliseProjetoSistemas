namespace AttAnalise.Models
{
    public abstract class Produto
    {
        public int Codigo { get => _Codigo; set => _Codigo = value; }
        public string Nome
        {   get => _Nome;
            set
            {
                if(String.IsNullOrEmpty(value))
                    throw new ArgumentException("O nome não pode ser vazio! Por favor, preencha o campo corretamente");
                else
                    _Nome = value;
            }
        }
        public string Tipo
        {   get => _Tipo;
            set
            {
                if(String.IsNullOrEmpty(value))
                    throw new ArgumentException("O tipo não pode ser vazio! Por favor, preencha o campo corretamente");
                else
                    _Tipo = value;
            }
        }
        public string Marca
        {   get => _Marca;
            set
            {
                if(String.IsNullOrEmpty(value))
                    throw new ArgumentException("A marca não pode ser vazia! Por favor, preencha o campo corretamente");
                else
                    _Marca = value;
            }
        }
        public string Modelo
        {   get => _Modelo;
            set
            {
                if(String.IsNullOrEmpty(value))
                    throw new ArgumentException("O modelo não pode ser vazio! Por favor, preencha o campo corretamente");
                else
                    _Modelo = value;
            }
        }
        public decimal Valor
        {   get => _Valor; 
            set
            {
                if (value <= 0)
                    throw new ArgumentException("O valor do produto não pode ser menor ou igual a 0 (zero).");
                else
                    _Valor = value;
            }
        }
        
        
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