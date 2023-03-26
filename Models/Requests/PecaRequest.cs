namespace AttAnalise.Models.Requests
{
    public class PecaRequest : ProdutoRequest
    {
        public string Arquitetura { get => _Arquitetura; set => _Arquitetura = value; }
        private string _Arquitetura { get; set; }
    }
}