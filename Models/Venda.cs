using System;
using System.Collections.Generic;

namespace projeto.Models
{
    public class Venda
    {
        public Venda(){}
        public Venda(int id, int cliente, decimal totalCompra, DateTime dataCompra, bool status)
        {
            this.Id = id;
            this.ClienteId = cliente;
            this.TotalCompra = totalCompra;
            this.DataCompra = dataCompra;
            this.Status = status;
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public decimal TotalCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public bool Status { get; set; }
        public ICollection<VendaProduto> VendasProdutos { get; set; }
    }
}