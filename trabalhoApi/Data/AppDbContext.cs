using Microsoft.EntityFrameworkCore;

namespace TrabalhoApi;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração de Produto
        modelBuilder.Entity<Produto>().HasKey(p => p.Id);
        modelBuilder.Entity<Produto>().Property(p => p.Id).ValueGeneratedOnAdd(); // AUTO INCREMENT
        modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(120).IsRequired();
        modelBuilder.Entity<Produto>().Property(p => p.Preco).HasPrecision(18, 2).IsRequired();

        // Configuração de Categoria
        modelBuilder.Entity<Categoria>().HasKey(c => c.Id);
        modelBuilder.Entity<Categoria>().Property(c => c.Id).ValueGeneratedOnAdd(); // AUTO INCREMENT
        modelBuilder.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(80).IsRequired();
        modelBuilder.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(200);

        // Configuração de Cliente
        modelBuilder.Entity<Cliente>().HasKey(c => c.Id);
        modelBuilder.Entity<Cliente>().Property(c => c.Id).ValueGeneratedOnAdd(); // AUTO INCREMENT
        modelBuilder.Entity<Cliente>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Cliente>().Property(c => c.Email).IsRequired();
    }
}