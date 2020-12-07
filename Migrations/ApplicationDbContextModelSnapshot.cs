﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projeto.Data;

namespace projeto.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("projeto.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Documento")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("clientes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataCadastro = new DateTime(2020, 12, 7, 15, 35, 19, 426, DateTimeKind.Local).AddTicks(465),
                            Documento = "12345678956",
                            Email = "fabiano@gft.com",
                            Nome = "Fabiano Preto",
                            Senha = "e10adc3949ba59abbe56e057f20f883e",
                            Status = true
                        },
                        new
                        {
                            Id = 2,
                            DataCadastro = new DateTime(2020, 12, 7, 15, 35, 19, 427, DateTimeKind.Local).AddTicks(8171),
                            Documento = "96531264962",
                            Email = "karine@gft.com",
                            Nome = "Karine Martins",
                            Senha = "e10adc3949ba59abbe56e057f20f883e",
                            Status = true
                        },
                        new
                        {
                            Id = 3,
                            DataCadastro = new DateTime(2020, 12, 7, 15, 35, 19, 427, DateTimeKind.Local).AddTicks(8306),
                            Documento = "84216596542",
                            Email = "felipe@gft.com",
                            Nome = "Felipe Pina",
                            Senha = "e10adc3949ba59abbe56e057f20f883e",
                            Status = true
                        },
                        new
                        {
                            Id = 4,
                            DataCadastro = new DateTime(2020, 12, 7, 15, 35, 19, 427, DateTimeKind.Local).AddTicks(8312),
                            Documento = "36492516953",
                            Email = "ingrid@gft.com",
                            Nome = "Ingrid Serello",
                            Senha = "e10adc3949ba59abbe56e057f20f883e",
                            Status = true
                        },
                        new
                        {
                            Id = 5,
                            DataCadastro = new DateTime(2020, 12, 7, 15, 35, 19, 427, DateTimeKind.Local).AddTicks(8315),
                            Documento = "96385274145",
                            Email = "jeziel@gft.com",
                            Nome = "Jeziel Santos",
                            Senha = "e10adc3949ba59abbe56e057f20f883e",
                            Status = true
                        });
                });

            modelBuilder.Entity("projeto.Models.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CNPJ")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("fornecedores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CNPJ = "12.362.612/4568.65",
                            Nome = "Amazon",
                            Status = true
                        },
                        new
                        {
                            Id = 2,
                            CNPJ = "62.456.852/8745.63",
                            Nome = "Dell",
                            Status = true
                        },
                        new
                        {
                            Id = 3,
                            CNPJ = "23.451.895/9512.62",
                            Nome = "Xiaomi",
                            Status = true
                        },
                        new
                        {
                            Id = 4,
                            CNPJ = "94.845.965/6324.84",
                            Nome = "MaxRacer",
                            Status = true
                        },
                        new
                        {
                            Id = 5,
                            CNPJ = "74.854.451/5698.32",
                            Nome = "Motospeed",
                            Status = true
                        });
                });

            modelBuilder.Entity("projeto.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Categoria")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CodigoProduto")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FornecedorId")
                        .HasColumnType("int");

                    b.Property<string>("Imagem")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool?>("Promocao")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ValorPromocao")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("produtos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Categoria = "Tecnologia",
                            CodigoProduto = "1454",
                            FornecedorId = 1,
                            Imagem = "echodot.png",
                            Nome = "Echo Dot",
                            Promocao = false,
                            Quantidade = 200,
                            Status = true,
                            Valor = 330m,
                            ValorPromocao = 0m
                        },
                        new
                        {
                            Id = 2,
                            Categoria = "Tecnologia",
                            CodigoProduto = "5415",
                            FornecedorId = 2,
                            Imagem = "notebook.png",
                            Nome = "Notebook",
                            Promocao = false,
                            Quantidade = 10,
                            Status = true,
                            Valor = 3500m,
                            ValorPromocao = 0m
                        },
                        new
                        {
                            Id = 3,
                            Categoria = "Móveis",
                            CodigoProduto = "2548",
                            FornecedorId = 4,
                            Imagem = "cadeira.png",
                            Nome = "Cadeira Gamer",
                            Promocao = false,
                            Quantidade = 30,
                            Status = true,
                            Valor = 1300m,
                            ValorPromocao = 0m
                        },
                        new
                        {
                            Id = 4,
                            Categoria = "Tecnologia",
                            CodigoProduto = "9514",
                            FornecedorId = 5,
                            Imagem = "teclado.png",
                            Nome = "Teclado",
                            Promocao = false,
                            Quantidade = 80,
                            Status = true,
                            Valor = 450m,
                            ValorPromocao = 0m
                        },
                        new
                        {
                            Id = 5,
                            Categoria = "Telecomunicação",
                            CodigoProduto = "9647",
                            FornecedorId = 1,
                            Imagem = "celular.png",
                            Nome = "Celular",
                            Promocao = false,
                            Quantidade = 500,
                            Status = true,
                            Valor = 1500m,
                            ValorPromocao = 0m
                        });
                });

            modelBuilder.Entity("projeto.Models.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("TotalCompra")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("venda");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClienteId = 1,
                            DataCompra = new DateTime(2020, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = true,
                            TotalCompra = 1750m
                        },
                        new
                        {
                            Id = 2,
                            ClienteId = 2,
                            DataCompra = new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = true,
                            TotalCompra = 3500m
                        },
                        new
                        {
                            Id = 3,
                            ClienteId = 4,
                            DataCompra = new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = true,
                            TotalCompra = 1500m
                        });
                });

            modelBuilder.Entity("projeto.Models.VendaProduto", b =>
                {
                    b.Property<int>("VendaId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("FornecedorId")
                        .HasColumnType("int");

                    b.HasKey("VendaId", "ProdutoId", "FornecedorId");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("vendasProdutos");

                    b.HasData(
                        new
                        {
                            VendaId = 1,
                            ProdutoId = 3,
                            FornecedorId = 4
                        },
                        new
                        {
                            VendaId = 1,
                            ProdutoId = 4,
                            FornecedorId = 5
                        },
                        new
                        {
                            VendaId = 2,
                            ProdutoId = 2,
                            FornecedorId = 2
                        },
                        new
                        {
                            VendaId = 3,
                            ProdutoId = 5,
                            FornecedorId = 3
                        });
                });

            modelBuilder.Entity("projeto.Models.Produto", b =>
                {
                    b.HasOne("projeto.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("projeto.Models.Venda", b =>
                {
                    b.HasOne("projeto.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("projeto.Models.VendaProduto", b =>
                {
                    b.HasOne("projeto.Models.Fornecedor", "Fornecedor")
                        .WithMany("VendasProdutos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projeto.Models.Produto", "Produto")
                        .WithMany("VendasProdutos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projeto.Models.Venda", "Venda")
                        .WithMany("VendasProdutos")
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
