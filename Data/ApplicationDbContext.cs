using Microsoft.EntityFrameworkCore;
using projeto.Models;

namespace projeto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Produto> produtos { get; set; }
        public DbSet<Fornecedor> fornecedores { get; set; }
        public DbSet<Venda> venda { get; set; }
        public DbSet<VendaProduto> vendasProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendaProduto>().HasKey(sc => new { sc.VendaId, sc.ProdutoId});
            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){}
    }
}