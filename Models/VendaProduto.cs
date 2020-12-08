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

        public int VendaId { get; set; }
        public Venda Venda { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}