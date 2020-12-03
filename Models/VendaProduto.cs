namespace projeto.Models
{
    public class VendaProduto
    {
        public VendaProduto() {}
        public VendaProduto(int vendaId, int produtoId, int fornecedorId)
        {
            this.VendaId = vendaId;
            this.ProdutoId = produtoId;
            this.FornecedorId = fornecedorId;
        }

        public int VendaId { get; set; }
        public Venda Venda { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}