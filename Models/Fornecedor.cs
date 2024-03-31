using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models;

public class Fornecedor
{
    [Key]
    public int FornecedorId { get; set; }
    public string RazaoSocial { get; set; }
    public string? NomeFantasia { get; set; }
    public ICollection<Produto> Produtos { get; set; }
    public Usuario Usuario { get; set; }
    public int UsuarioId { get; set; }
}