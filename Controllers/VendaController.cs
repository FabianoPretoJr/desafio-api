using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using projeto.Container;
using Microsoft.AspNetCore.Authorization;

namespace projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class VendaController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        private HATEOAS.HATEOAS HATEOAS;
        public VendaController(ApplicationDbContext database)
        {
            this.database = database;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/venda");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("GET_INFO_BY_NOME", "GET");
            HATEOAS.AddAction("EDIT_PRODUCT", "PUT");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var vendas = database.venda.Where(v => v.Status == true)
                                       .Include(v => v.Cliente)
                                       .Include(v => v.Fornecedor)
                                       .Include(v => v.VendasProdutos)
                                       .ThenInclude(v => v.Produto)
                                       .ToList();
            
            List<VendaContainer> vendasHATEOAS = new List<VendaContainer>();
            foreach(var venda in vendas)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add("data/" + venda.DataCompra.ToString().Replace("/", "%2F").Replace(" 00:00:00", ""));
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add(venda.Id.ToString());

                VendaContainer vendaHATEOAS = new VendaContainer();
                vendaHATEOAS.venda = venda;
                vendaHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                vendasHATEOAS.Add(vendaHATEOAS);
            }

            return Ok(vendasHATEOAS);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var venda = database.venda.Where(v => v.Status == true)
                                          .Include(v => v.Cliente)
                                          .Include(v => v.Fornecedor)
                                          .Include(v => v.VendasProdutos)
                                          .ThenInclude(v => v.Produto)
                                          .First(v => v.Id == id);

                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add("data/" + venda.DataCompra.ToString().Replace("/", "%2F"));
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add(venda.Id.ToString());

                VendaContainer vendaHATEOAS = new VendaContainer();
                vendaHATEOAS.venda = venda;
                vendaHATEOAS.links = HATEOAS.GetActions(formatoLinks);

                return Ok(vendaHATEOAS);
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Id não encontrado"});
            }
        }

        [HttpGet("asc")]
        public IActionResult GetByAsc()
        {
            var vendas = database.venda.Where(v => v.Status == true)
                                       .Include(v => v.Cliente)
                                       .Include(v => v.Fornecedor)
                                       .Include(v => v.VendasProdutos)
                                       .ThenInclude(v => v.Produto)
                                       .OrderBy(v => v.DataCompra)
                                       .ToList();
            
            List<VendaContainer> vendasHATEOAS = new List<VendaContainer>();
            foreach(var venda in vendas)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add("data/" + venda.DataCompra.ToString().Replace("/", "%2F"));
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add(venda.Id.ToString());

                VendaContainer vendaHATEOAS = new VendaContainer();
                vendaHATEOAS.venda = venda;
                vendaHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                vendasHATEOAS.Add(vendaHATEOAS);
            }

            return Ok(vendasHATEOAS);
        }

        [HttpGet("desc")]
        public IActionResult GetByDesc()
        {
            var vendas = database.venda.Where(v => v.Status == true)
                                      .Include(v => v.Cliente)
                                      .Include(v => v.Fornecedor)
                                      .Include(v => v.VendasProdutos)
                                      .ThenInclude(v => v.Produto)
                                      .OrderByDescending(v => v.DataCompra)
                                      .ToList();
            
            List<VendaContainer> vendasHATEOAS = new List<VendaContainer>();
            foreach(var venda in vendas)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add("data/" + venda.DataCompra.ToString().Replace("/", "%2F"));
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add(venda.Id.ToString());

                VendaContainer vendaHATEOAS = new VendaContainer();
                vendaHATEOAS.venda = venda;
                vendaHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                vendasHATEOAS.Add(vendaHATEOAS);
            }

            return Ok(vendasHATEOAS);
        }

        [HttpGet("data/{data}")]
        public IActionResult GetByData(string data)
        {
            data = data.Replace("%2F", "/");
            try
            {
                var venda = database.venda.Where(v => v.Status == true)
                                          .Include(v => v.Cliente)
                                          .Include(v => v.Fornecedor)
                                          .Include(v => v.VendasProdutos)
                                          .ThenInclude(v => v.Produto)
                                          .First(v => v.DataCompra == DateTime.ParseExact(data, "dd/MM/yyyy", null));
                
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add("data/" + venda.DataCompra.ToString().Replace("/", "%2F"));
                formatoLinks.Add(venda.Id.ToString());
                formatoLinks.Add(venda.Id.ToString());

                VendaContainer vendaHATEOAS = new VendaContainer();
                vendaHATEOAS.venda = venda;
                vendaHATEOAS.links = HATEOAS.GetActions(formatoLinks);

                return Ok(vendaHATEOAS);
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Data não encontrada"});
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]VendaDTO vendaTemp)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    decimal totalCompra = 0;

                    if(vendaTemp.Fornecedor <= 0)
                    {
                        Response.StatusCode = 404;
                        return new ObjectResult(new {msg = "Id de fornecedor está inválido"});
                    }
                    try
                    {
                        var idFornecedor = database.fornecedores.First(f => f.Id == vendaTemp.Fornecedor);

                        if(idFornecedor == null)
                        {
                            Response.StatusCode = 400;
                            return new ObjectResult(new {msg = "Id de fornecedor não encontrado"});
                        }
                    }
                    catch(Exception)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Id de fornecedor não encontrado"});
                    }

                    if(vendaTemp.Cliente <= 0)
                    {
                        Response.StatusCode = 404;
                        return new ObjectResult(new {msg = "Id de cliente está inválido"});
                    }
                    try
                    {
                        var idCliente = database.clientes.First(c => c.Id == vendaTemp.Cliente);

                        if(idCliente == null)
                        {
                            Response.StatusCode = 400;
                            return new ObjectResult(new {msg = "Id de cliente não encontrado"});
                        }
                    }
                    catch(Exception)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Id de cliente não encontrado"});
                    }

                    if(vendaTemp.Produtos.Count == 0)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Necessário informar pelo menos 1 produto"});
                    }

                    foreach(var produto in vendaTemp.Produtos)
                    {
                        if(produto <= 0)
                        {
                            Response.StatusCode = 404;
                            return new ObjectResult(new {msg = "Id " + produto + " de produto está inválido"});
                        }
                        try
                        {
                            var idProduto = database.produtos.First(p => p.Id == produto);
                            var idFornecedor = database.fornecedores.First(f => f.Id == vendaTemp.Fornecedor);

                            if(idProduto == null)
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new {msg = "Id " + produto + " de produto não encontrado"});
                            }

                            if(idProduto.Quantidade <= 0)
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new {msg = "Quantidade insuficiente do produto em estoque, ID: " + produto});
                            }

                            if(idProduto.Fornecedor != idFornecedor)
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new {msg = "Produto ID " + produto + " não pertence ao fornecedor informado"});
                            }
                        }
                        catch(Exception)
                        {
                            Response.StatusCode = 400;
                            return new ObjectResult(new {msg = "Id " + produto + " de produto não encontrado"});
                        }

                        var pro = database.produtos.First(p => p.Id == produto);
                        totalCompra = Convert.ToBoolean(pro.Promocao) ? totalCompra + Convert.ToDecimal(pro.ValorPromocao) : totalCompra + pro.Valor;
                    }

                    Venda venda = new Venda();

                    venda.Cliente = database.clientes.First(c => c.Id == vendaTemp.Cliente);
                    venda.Fornecedor = database.fornecedores.First(f => f.Id == vendaTemp.Fornecedor);
                    venda.DataCompra = DateTime.ParseExact(vendaTemp.DataCompra, "dd/MM/yyyy", null);
                    venda.TotalCompra = totalCompra;
                    venda.Status = true;
                    
                    database.venda.Add(venda);
                    database.SaveChanges();

                    foreach (var produto in vendaTemp.Produtos)
                    {
                        VendaProduto vp = new VendaProduto();

                        vp.Venda = database.venda.First(v => v.Id == venda.Id);
                        vp.Produto = database.produtos.First(p => p.Id == produto);

                        var prod = database.produtos.First(p => p.Id == produto);
                        prod.Quantidade = prod.Quantidade - 1;

                        database.vendasProdutos.Add(vp);
                        database.SaveChanges();
                    }

                    Response.StatusCode = 201;
                    return new ObjectResult(new {msg = "Venda criada com sucesso"});
                }
                catch(Exception e)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "" + e});
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Todos campos devem ser passados"});
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]VendaPutDTO vendaTemp)
        {
            if(id > 0)
            {
                try
                {
                    decimal totalCompra = 0;
                    var venda = database.venda.Include(v => v.Cliente).Include(v => v.Fornecedor).First(v => v.Id == id);

                    if(venda != null)
                    {
                        venda.Cliente = vendaTemp.Cliente > 0 ? database.clientes.First(c => c.Id == vendaTemp.Cliente) : venda.Cliente;
                        venda.DataCompra = vendaTemp.DataCompra != null || vendaTemp.DataCompra == "" ? DateTime.ParseExact(vendaTemp.DataCompra, "dd/MM/yyyy", null) : venda.DataCompra;

                        if(vendaTemp.Produtos != null)
                        {
                            foreach (var produto in vendaTemp.Produtos)
                            {
                                if(produto <= 0)
                                {
                                    Response.StatusCode = 404;
                                    return new ObjectResult(new {msg = "Id " + produto + " de produto está inválido"});
                                }
                                try
                                {
                                    var idProduto = database.produtos.Include(p => p.Fornecedor).First(p => p.Id == produto);

                                    if(idProduto == null)
                                    {
                                        Response.StatusCode = 400;
                                        return new ObjectResult(new {msg = "Id " + produto + "  de produto não encontrado"});
                                    }

                                    if(idProduto.Quantidade <= 0)
                                    {
                                        Response.StatusCode = 400;
                                        return new ObjectResult(new {msg = "Quantidade insuficiente desse produto em estoque, ID: " + produto});
                                    }

                                    if(idProduto.Fornecedor.Id != vendaTemp.Fornecedor)
                                    {
                                        Response.StatusCode = 400;
                                        return new ObjectResult(new {msg = "Produto ID " + produto + " não pertence ao fornecedor informado"});
                                    }
                                }
                                catch(Exception)
                                {
                                    Response.StatusCode = 400;
                                    return new ObjectResult(new {msg = "Id " + produto + " de produto não encontrado"});
                                }
                                var pro = database.produtos.First(p => p.Id == produto);
                                totalCompra = Convert.ToBoolean(pro.Promocao) ? totalCompra + Convert.ToDecimal(pro.ValorPromocao) : totalCompra + pro.Valor;
                            }
                        }
                        else if(vendaTemp.Fornecedor > 0)
                        {
                            Response.StatusCode = 400;
                            return new ObjectResult(new {msg = "Necessário enviar uma lista de novos produtos que sejam condizentes com este novo fornecedor"});
                        }

                        venda.Fornecedor = vendaTemp.Fornecedor > 0 ? database.fornecedores.First(f => f.Id == vendaTemp.Fornecedor) : venda.Fornecedor;
                        venda.TotalCompra = vendaTemp.Produtos != null ? totalCompra : venda.TotalCompra;
                        database.SaveChanges();

                        if(vendaTemp.Produtos != null)
                        {
                            var vpAntigos = database.vendasProdutos.Where(vp => vp.Venda.Id == id).ToList();
                            database.vendasProdutos.RemoveRange(vpAntigos);
                            database.SaveChanges();

                            foreach (var produto in vendaTemp.Produtos)
                            {
                                VendaProduto vp = new VendaProduto();

                                vp.Venda = database.venda.First(v => v.Id == id);
                                vp.Produto = database.produtos.First(p => p.Id == produto);

                                database.vendasProdutos.Add(vp);
                                database.SaveChanges();
                            }
                        }

                        return Ok(new {msg = "Venda alterada com sucesso"});
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Venda não encontrada"});
                    }
                }
                catch(Exception e)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "" + e});
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Id de venda está inválido"});
            }
        } 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var vpAntigos = database.vendasProdutos.Where(vp => vp.Venda.Id == id).ToList();
                database.vendasProdutos.RemoveRange(vpAntigos);

                var venda = database.venda.Where(v => v.Status == true).First(v => v.Id == id);
                venda.Status = false;
                database.SaveChanges();

                return Ok(new {msg = "Venda deletada com sucesso"});
            }
            catch(Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id de venda está inválido"});
            }
        }
    }
}