using System;
using System.Collections.Generic;
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

            modelBuilder.Entity<Cliente>()
                .HasData(new List<Cliente>(){
                    new Cliente(1, "Fabiano Preto", "fabiano@gft.com", "e10adc3949ba59abbe56e057f20f883e", "39727557830", DateTime.Now, true), // senha 123456 para todos
                    new Cliente(2, "Karine Martins", "karine@gft.com", "e10adc3949ba59abbe56e057f20f883e", "96246712819", DateTime.Now, true),
                    new Cliente(3, "Felipe Pina", "felipe@gft.com", "e10adc3949ba59abbe56e057f20f883e", "18990004888", DateTime.Now, true),
                    new Cliente(4, "Ingrid Serello", "ingrid@gft.com", "e10adc3949ba59abbe56e057f20f883e", "55775371852", DateTime.Now, true),
                    new Cliente(5, "Jeziel Santos", "jeziel@gft.com", "e10adc3949ba59abbe56e057f20f883e", "42775181848", DateTime.Now, true)
                });

            modelBuilder.Entity<Fornecedor>()
                .HasData(new List<Fornecedor>(){
                    new Fornecedor(1, "Amazon", "42378548000110", true),
                    new Fornecedor(2, "Dell", "91932580000100", true),
                    new Fornecedor(3, "Xiaomi", "35834617000197", true),
                    new Fornecedor(4, "MaxRacer", "14813497000171", true),
                    new Fornecedor(5, "Motospeed", "30119402000170", true)
                });

            modelBuilder.Entity<Produto>()
                .HasData(new List<Produto>(){
                    new Produto(1, "Echo Dot", "1454", 330, true, 180, "Tecnologia", "echodot.png", 200, 1, true),
                    new Produto(2, "Notebook", "5415", 3500, false, 0, "Tecnologia", "notebook.png", 10, 2, true),
                    new Produto(3, "Cadeira Gamer", "2548", 1300, false, 0, "Móveis", "cadeira.png", 30, 4, true),
                    new Produto(4, "Teclado", "9514", 450, false, 0, "Tecnologia", "teclado.png", 80, 5, true),
                    new Produto(5, "Celular", "9647", 1500, false, 0, "Telecomunicação", "celular.png", 500, 3, true),
                    new Produto(6, "Kindle", "4512", 400, false, 0, "Tecnologia", "kindle.png", 100, 1, true)
                });

            modelBuilder.Entity<Venda>()
                .HasData(new List<Venda>(){
                    new Venda(1, 1, 1, 730, DateTime.ParseExact("20/11/2020", "dd/MM/yyyy", null), true),
                    new Venda(2, 2, 2,3500, DateTime.ParseExact("03/12/2020", "dd/MM/yyyy", null), true),
                    new Venda(3, 4, 4,1300, DateTime.ParseExact("04/12/2020", "dd/MM/yyyy", null), true)
                });

            modelBuilder.Entity<VendaProduto>()
                .HasData(new List<VendaProduto>(){
                    new VendaProduto(1, 6),
                    new VendaProduto(1, 1),
                    new VendaProduto(2, 2),
                    new VendaProduto(3, 3)
                });

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){}
    }
}