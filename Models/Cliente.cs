using System;
using Newtonsoft.Json;

namespace projeto.Models
{
    public class Cliente
    {
        public Cliente(){}
        public Cliente(int id, string nome, string email, string senha, string documento, DateTime dataCadastro, bool status)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Documento = documento;
            this.DataCadastro = dataCadastro;
            this.Status = status;
        }
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Senha { get; set; }
        public string Documento { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Status { get; set; }
    }
}