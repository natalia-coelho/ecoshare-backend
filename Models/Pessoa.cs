﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models
{
    public class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }
        public string? CpfCnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? EmailContato { get; set; }
        public string? TelefoneContato { get; set; }
        public byte[]? FotoPerfil { get; set; }
        public string? Bio { get; set; }
        public string? TituloPerfil {  get; set; }
        public string? Formacao {  get; set; }
        public string? Descricao { get; set; }
        public Endereco? Endereco { get; set; }
        public int? EnderecoId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        public int? FornecedorId { get; set; }
        public Usuario? Usuario { get; set; }
        public string? UsuarioId { get; set; }
    }
}
