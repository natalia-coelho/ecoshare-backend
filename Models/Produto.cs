// Models/Produto.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoshare_backend.Models;
public class Produto
{
    public int ProdutoId { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }
    public byte[]? Imagem { get; set; }
    public Categoria? Categoria { get; set; }
    public Fornecedor? Fornecedor { get; set; }
    public int FornecedorId { get; set; }
}

public enum Categoria
{
    [Display(Name = "Cosméticos")]
    COSMETICOS,
    [Display(Name = "Alimentícios")]
    ALIMENTICIOS,
    [Display(Name = "Vestuário")]
    VESTUARIO,
    OUTROS
}