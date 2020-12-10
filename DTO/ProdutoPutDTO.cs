using System.ComponentModel.DataAnnotations;

namespace projeto.DTO
{
    public class ProdutoPutDTO
    {
        [MinLength(2, ErrorMessage = "O nome do produto deve ter no minimo 2 caracteres")]
        public string Nome { get; set; }

        public decimal Valor { get; set; }

        public bool Promocao { get; set; }
        public decimal ValorPromocao { get; set; }

        [MinLength(2, ErrorMessage = "A categoria do produto deve ter no minimo 2 caracteres")]
        public string Categoria { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Imagem { get; set; }

        public int Quantidade { get; set; }

        public int Fornecedor { get; set; }
    }
}