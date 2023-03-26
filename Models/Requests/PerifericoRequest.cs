namespace AttAnalise.Models.Requests
{
    public class PerifericoRequest : ProdutoRequest
    {
        public bool IsGamer { get => _IsGamer; set => _IsGamer = value; }
        private bool _IsGamer { get; set; }
    }
}