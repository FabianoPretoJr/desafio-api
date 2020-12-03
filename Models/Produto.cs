using System.Collections.Generic;

namespace projeto.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodigoProduto { get; set; }
        public decimal Valor { get; set; }
        public bool? Promocao { get; set; } = false;
        public decimal? ValorPromocao { get; set; } = 0;
        public string Categoria { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public bool Status { get; set; }
        public ICollection<VendaProduto> VendasProdutos { get; set; }
    }
}