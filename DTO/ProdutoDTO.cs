using System.ComponentModel.DataAnnotations;

namespace projeto.DTO
{
    public class ProdutoDTO
    {
        [Required(ErrorMessage = "Necessário informar nome do produto")]
        [MinLength(2, ErrorMessage = "O nome do produto deve ter no minimo 2 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Necessário informar valor do produto")]
        [Range(1, 1000000, ErrorMessage= "Valor do produto deve estar entre {1} até {2}")]
        public decimal Valor { get; set; }

        public bool Promocao { get; set; }
        public decimal ValorPromocao { get; set; }

        [Required(ErrorMessage = "Necessário informar categoria do produto")]
        [MinLength(2, ErrorMessage = "A categoria do produto deve ter no minimo 2 caracteres")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "Necessário informar imagem do produtoo")]
        [DataType(DataType.ImageUrl)]
        public string Imagem { get; set; }

        [Required(ErrorMessage = "Necessário informar quantidade do produto")]
        [Range(1, 1000000, ErrorMessage= "Quantidade do produto deve estar entre {1} até {2}")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Necessário informar ID do fornecedor")]
        public int Fornecedor { get; set; }
    }
}