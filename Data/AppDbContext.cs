using ecoshare_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.HasKey(a => a.FornecedorId);

            entity.HasMany(a => a.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p =>  p.FornecedorId)
                .IsRequired();
        });

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
