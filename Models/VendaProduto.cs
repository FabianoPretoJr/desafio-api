using Newtonsoft.Json;

namespace projeto.Models
{
    public class VendaProduto
    {
        public VendaProduto() {}
        public VendaProduto(int vendaId, int produtoId)
        {
            this.VendaId = vendaId;
            this.ProdutoId = produtoId;
        }
        [JsonIgnore]
        public int VendaId { get; set; }
        [JsonIgnore]
        public Venda Venda { get; set; }
        [JsonIgnore]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}