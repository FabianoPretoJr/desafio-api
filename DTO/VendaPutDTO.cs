using System.Collections.Generic;

namespace projeto.DTO
{
    public class VendaPutDTO
    {
        public int Fornecedor { get; set; }
        public int Cliente { get; set; }
        public List<int> Produtos { get; set; }
        public string DataCompra { get; set; }
    }
}