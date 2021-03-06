using Newtonsoft.Json;

namespace projeto.Models
{
    public class Fornecedor
    {
        public Fornecedor(){}
        public Fornecedor(int id, string nome, string cNPJ, bool status)
        {
            this.Id = id;
            this.Nome = nome;
            this.CNPJ = cNPJ;
            this.Status = status;

        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
    }
}