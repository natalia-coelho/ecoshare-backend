using ecoshare_backend.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>()
               .HasKey(p => p.PessoaId);

        modelBuilder.Entity<Pessoa>()
            .HasOne(p => p.Endereco)
            .WithMany()
            .HasForeignKey(p => p.EnderecoId);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId);

            entity.Property(e => e.NomeUsuario).IsRequired();
            entity.Property(e => e.Perfil).HasConversion<string>();

            entity.HasOne(u => u.Pessoa)
                .WithMany()
                .HasForeignKey(u => u.PessoaId)
                .IsRequired();
        });

        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.HasKey(a => a.FornecedorId);

            entity.HasMany(a => a.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p =>  p.FornecedorId)
                .IsRequired();

            entity.HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .IsRequired();
        });

        //modelBuilder.Entity<Endereco>(entity =>
        //{
        //    entity.HasKey(e => e.EnderecoId);
        //});

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.ProdutoId);

            // Relacionamento com Categoria (enum)
            entity.Property(e => e.Categoria).HasConversion<string>();
            entity.Property(e => e.Preco).HasColumnType("decimal(18,2)");

            entity.HasOne(e => e.Fornecedor)
                .WithMany(f => f.Produtos)
                .HasForeignKey(e => e.FornecedorId)
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
