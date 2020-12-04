using projeto.HATEOAS;
using projeto.Models;

namespace projeto.Container
{
    public class FornecedorContainer
    {
        public Fornecedor fornecedor { get; set; }
        public Link[] links { get; set; }
    }
}