using projeto.Models;

namespace projeto.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool Promocao { get; set; }
        public decimal ValorPromocao { get; set; }
        public string Categoria { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
        public int Fornecedor { get; set; }
    }
}