using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models;

public class Usuario
{
    [Key]
    public int UsuarioId { get; set; }
    public string NomeUsuario { get; set; }
    public Perfil Perfil { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string CpfCnpj { get; set; }
    public Pessoa? Pessoa { get; set;}
    public int PessoaId { get; set; }
}

public enum Perfil
{
    FORNECEDOR,
    CONSUMIDOR,
    ADMINISTRADOR
}

