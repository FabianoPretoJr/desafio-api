using System;

namespace projeto.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Documento { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Status { get; set; }
    }
}