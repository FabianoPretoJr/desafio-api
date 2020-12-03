using System.Collections.Generic;

namespace projeto.Models
{
    public class Produto
    {
        public Produto() {}
        public Produto(int id, string nome, string codigoProduto, decimal valor, bool promocao, decimal valorPromocao, string categoria, string imagem, int quantidade, int fornecedor, bool status)
        {
            this.Id = id;
            this.Nome = nome;
            this.CodigoProduto = codigoProduto;
            this.Valor = valor;
            this.Categoria = categoria;
            this.Imagem = imagem;
            this.Quantidade = quantidade;
            this.FornecedorId = fornecedor; 
            this.Status = status;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodigoProduto { get; set; }
        public decimal Valor { get; set; }
        public bool? Promocao { get; set; } = false;
        public decimal? ValorPromocao { get; set; } = 0;
        public string Categoria { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public bool Status { get; set; }
        public ICollection<VendaProduto> VendasProdutos { get; set; }
    }
}