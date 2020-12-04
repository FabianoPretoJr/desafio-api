using projeto.HATEOAS;
using projeto.Models;

namespace projeto.Container
{
    public class ProdutoContainer
    {
        public Produto produto { get; set; }
        public Link[] links { get; set; }
    }
}