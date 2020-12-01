using System.Collections.Generic;

namespace projeto.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public ICollection<VendaProduto> VendasProdutos { get; set; }
    }
}