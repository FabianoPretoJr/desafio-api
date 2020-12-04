using projeto.HATEOAS;
using projeto.Models;

namespace projeto.Container
{
    public class VendaContainer
    {
        public Venda venda { get; set; }
        public Link[] links { get; set; }
    }
}