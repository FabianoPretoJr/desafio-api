using System;
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
                name: "venda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: false),
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
                name: "vendasProdutos",
                columns: table => new
                {
                    VendaId = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false),
                    FornecedorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendasProdutos", x => new { x.VendaId, x.ProdutoId, x.FornecedorId });
                    table.ForeignKey(
                        name: "FK_vendasProdutos_fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1, new DateTime(2020, 12, 7, 15, 35, 19, 426, DateTimeKind.Local).AddTicks(465), "12345678956", "fabiano@gft.com", "Fabiano Preto", "e10adc3949ba59abbe56e057f20f883e", true },
                    { 2, new DateTime(2020, 12, 7, 15, 35, 19, 427, DateTimeKind.Local).AddTicks(8171), "96531264962", "karine@gft.com", "Karine Martins", "e10adc3949ba59abbe56e057f20f883e", true },
                    { 3, new DateTime(2020, 12, 7, 15, 35, 19, 427, DateTimeKind.Local).AddTicks(8306), "84216596542", "felipe@gft.com", "Felipe Pina", "e10adc3949ba59abbe56e057f20f883e", true },
                    { 4, new DateTime(2020, 12, 7, 15, 35, 19, 427, DateTimeKind.Local).AddTicks(8312), "36492516953", "ingrid@gft.com", "Ingrid Serello", "e10adc3949ba59abbe56e057f20f883e", true },
                    { 5, new DateTime(2020, 12, 7, 15, 35, 19, 427, DateTimeKind.Local).AddTicks(8315), "96385274145", "jeziel@gft.com", "Jeziel Santos", "e10adc3949ba59abbe56e057f20f883e", true }
                });

            migrationBuilder.InsertData(
                table: "fornecedores",
                columns: new[] { "Id", "CNPJ", "Nome", "Status" },
                values: new object[,]
                {
                    { 1, "12.362.612/4568.65", "Amazon", true },
                    { 2, "62.456.852/8745.63", "Dell", true },
                    { 3, "23.451.895/9512.62", "Xiaomi", true },
                    { 4, "94.845.965/6324.84", "MaxRacer", true },
                    { 5, "74.854.451/5698.32", "Motospeed", true }
                });

            migrationBuilder.InsertData(
                table: "produtos",
                columns: new[] { "Id", "Categoria", "CodigoProduto", "FornecedorId", "Imagem", "Nome", "Promocao", "Quantidade", "Status", "Valor", "ValorPromocao" },
                values: new object[,]
                {
                    { 1, "Tecnologia", "1454", 1, "echodot.png", "Echo Dot", false, 200, true, 330m, 0m },
                    { 5, "Telecomunicação", "9647", 1, "celular.png", "Celular", false, 500, true, 1500m, 0m },
                    { 2, "Tecnologia", "5415", 2, "notebook.png", "Notebook", false, 10, true, 3500m, 0m },
                    { 3, "Móveis", "2548", 4, "cadeira.png", "Cadeira Gamer", false, 30, true, 1300m, 0m },
                    { 4, "Tecnologia", "9514", 5, "teclado.png", "Teclado", false, 80, true, 450m, 0m }
                });

            migrationBuilder.InsertData(
                table: "venda",
                columns: new[] { "Id", "ClienteId", "DataCompra", "Status", "TotalCompra" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1750m },
                    { 2, 2, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3500m },
                    { 3, 4, new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1500m }
                });

            migrationBuilder.InsertData(
                table: "vendasProdutos",
                columns: new[] { "VendaId", "ProdutoId", "FornecedorId" },
                values: new object[,]
                {
                    { 3, 5, 3 },
                    { 2, 2, 2 },
                    { 1, 3, 4 },
                    { 1, 4, 5 }
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
                name: "IX_vendasProdutos_FornecedorId",
                table: "vendasProdutos",
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
                name: "fornecedores");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
