using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projeto.DTO
{
    public class VendaDTO
    {
        [Required(ErrorMessage = "Necessário informar ID do fornecedor")]
        public int Fornecedor { get; set; }

        [Required(ErrorMessage = "Necessário informar ID do cliente")]
        public int Cliente { get; set; }

        [Required(ErrorMessage = "Necessário informar ID do produto")]
        public List<int> Produtos { get; set; }

        [Required(ErrorMessage = "Necessário informar data da venda")]
        public string DataCompra { get; set; }
    }
}