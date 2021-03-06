﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projeto.Migrations
{
    public partial class AdicionandoEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Documento = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    CodigoProduto = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    Promocao = table.Column<bool>(nullable: true),
                    ValorPromocao = table.Column<decimal>(nullable: true),
                    Categoria = table.Column<string>(nullable: true),
                    Imagem = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    FornecedorId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_produtos_fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "venda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: false),
                    FornecedorId = table.Column<int>(nullable: false),
                    TotalCompra = table.Column<decimal>(nullable: false),
                    DataCompra = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_venda_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_venda_fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vendasProdutos",
                columns: table => new
                {
                    VendaId = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendasProdutos", x => new { x.VendaId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_vendasProdutos_produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vendasProdutos_venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "venda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "clientes",
                columns: new[] { "Id", "DataCadastro", "Documento", "Email", "Nome", "Senha", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 10, 9, 24, 28, 35, DateTimeKind.Local).AddTicks(4543), "39727557830", "fabiano@gft.com", "Fabiano Preto", "e10adc3949ba59abbe56e057f20f883e", true },
                    { 2, new DateTime(2020, 12, 10, 9, 24, 28, 37, DateTimeKind.Local).AddTicks(898), "96246712819", "karine@gft.com", "Karine Martins", "e10adc3949ba59abbe56e057f20f883e", true },
                    { 3, new DateTime(2020, 12, 10, 9, 24, 28, 37, DateTimeKind.Local).AddTicks(1004), "18990004888", "felipe@gft.com", "Felipe Pina", "e10adc3949ba59abbe56e057f20f883e", true },
                    { 4, new DateTime(2020, 12, 10, 9, 24, 28, 37, DateTimeKind.Local).AddTicks(1008), "55775371852", "ingrid@gft.com", "Ingrid Serello", "e10adc3949ba59abbe56e057f20f883e", true },
                    { 5, new DateTime(2020, 12, 10, 9, 24, 28, 37, DateTimeKind.Local).AddTicks(1010), "42775181848", "jeziel@gft.com", "Jeziel Santos", "e10adc3949ba59abbe56e057f20f883e", true }
                });

            migrationBuilder.InsertData(
                table: "fornecedores",
                columns: new[] { "Id", "CNPJ", "Nome", "Status" },
                values: new object[,]
                {
                    { 1, "42378548000110", "Amazon", true },
                    { 2, "91932580000100", "Dell", true },
                    { 3, "35834617000197", "Xiaomi", true },
                    { 4, "14813497000171", "MaxRacer", true },
                    { 5, "30119402000170", "Motospeed", true }
                });

            migrationBuilder.InsertData(
                table: "produtos",
                columns: new[] { "Id", "Categoria", "CodigoProduto", "FornecedorId", "Imagem", "Nome", "Promocao", "Quantidade", "Status", "Valor", "ValorPromocao" },
                values: new object[,]
                {
                    { 1, "Tecnologia", "1454", 1, "echodot.png", "Echo Dot", false, 200, true, 330m, 0m },
                    { 6, "Tecnologia", "4512", 1, "kindle.png", "Kindle", false, 100, true, 400m, 0m },
                    { 2, "Tecnologia", "5415", 2, "notebook.png", "Notebook", false, 10, true, 3500m, 0m },
                    { 5, "Telecomunicação", "9647", 3, "celular.png", "Celular", false, 500, true, 1500m, 0m },
                    { 3, "Móveis", "2548", 4, "cadeira.png", "Cadeira Gamer", false, 30, true, 1300m, 0m },
                    { 4, "Tecnologia", "9514", 5, "teclado.png", "Teclado", false, 80, true, 450m, 0m }
                });

            migrationBuilder.InsertData(
                table: "venda",
                columns: new[] { "Id", "ClienteId", "DataCompra", "FornecedorId", "Status", "TotalCompra" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 730m },
                    { 2, 2, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, 3500m },
                    { 3, 4, new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, true, 1300m }
                });

            migrationBuilder.InsertData(
                table: "vendasProdutos",
                columns: new[] { "VendaId", "ProdutoId" },
                values: new object[,]
                {
                    { 1, 6 },
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_produtos_FornecedorId",
                table: "produtos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_venda_ClienteId",
                table: "venda",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_venda_FornecedorId",
                table: "venda",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_vendasProdutos_ProdutoId",
                table: "vendasProdutos",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vendasProdutos");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "venda");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "fornecedores");
        }
    }
}
