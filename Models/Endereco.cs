﻿using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models;

public class Endereco
{
    [Key]
    public int EnderecoId { get; set; }
    public string Logradouro { get; set; }
    public long Numero { get; set; }
    public string? Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cep { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Pais { get; set; }
}