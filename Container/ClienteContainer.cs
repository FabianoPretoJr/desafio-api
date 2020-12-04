using projeto.HATEOAS;
using projeto.Models;

namespace projeto.Container
{
    public class ClienteContainer
    {
        public Cliente cliente { get; set; }
        public Link[] links { get; set; }
        public Link[] linksByAsc { get; set; }
        public Link[] linksByDesc { get; set; }
        public Link[] linksByNome { get; set; }
    }
}