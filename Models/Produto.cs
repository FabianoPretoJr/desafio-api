using System.Collections.Generic;

namespace projeto.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodigoProduto { get; set; }
        public decimal Valor { get; set; }
        public bool Promocao { get; set; }
        public decimal ValorPromocao { get; set; }
        public string Categoria { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public ICollection<VendaProduto> VendasProdutos { get; set; }
    }
}