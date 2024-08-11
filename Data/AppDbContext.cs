using ecoshare_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecoshare_backend.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
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
            base.OnModelCreating(modelBuilder);

            // Configurações relacionadas à entidade Fornecedor
            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(a => a.FornecedorId);

                entity.HasMany(a => a.Produtos)
                    .WithOne(p => p.Fornecedor)
                    .HasForeignKey(p => p.FornecedorId)
                    .IsRequired();
            });

            // Configurações relacionadas à entidade Produto
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
        }
    }
}
