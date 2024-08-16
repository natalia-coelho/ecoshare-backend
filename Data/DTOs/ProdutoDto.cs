namespace ecoshare_backend.Data.DTOs
{
    public class ProdutoDto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string? Descricao { get; set; }
        public byte[]? Imagem { get; set; }
        public int? FornecedorId { get; set; }
        public string? FornecedorNome { get; set; }
    }

    public class FornecedorDto
    {
        public int FornecedorId { get; set; }
        public string RazaoSocial { get; set; }
        public string? NomeFantasia { get; set; }
    }
}
