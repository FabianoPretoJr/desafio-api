using System;
using System.Collections.Generic;

namespace projeto.Models
{
    public class Venda
    {
        public int Id { get; set; }
        //public Fornecedor Fornecedor { get; set; }
        public Cliente Cliente { get; set; }
        //public List<Produto> Produtos { get; set; }
        public decimal TotalCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public ICollection<VendaProduto> VendasProdutos { get; set; }
    }
}