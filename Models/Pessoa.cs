using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models;

public class Pessoa
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public Endereco? Endereco { get; set; }
    public int EnderecoId { get; set; }
}