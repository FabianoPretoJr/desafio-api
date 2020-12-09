using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace projeto.Models
{
    public class Venda
    {
        public Venda(){}
        public Venda(int id, int cliente, int forcenedor, decimal totalCompra, DateTime dataCompra, bool status)
        {
            this.Id = id;
            this.ClienteId = cliente;
            this.FornecedorId = forcenedor;
            this.TotalCompra = totalCompra;
            this.DataCompra = dataCompra;
            this.Status = status;
        }

        public int Id { get; set; }
        [JsonIgnore]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        [JsonIgnore]
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public decimal TotalCompra { get; set; }
        public DateTime DataCompra { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public ICollection<VendaProduto> VendasProdutos { get; set; }
    }
}