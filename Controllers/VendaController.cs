using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;
using System;

namespace projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public VendaController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var vendas = database.venda.ToList();
            return Ok(vendas);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var venda = database.venda.First(v => v.Id == id);

                return Ok();
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Id não encontrado"});
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]VendaDTO vendaTemp)
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

                foreach (var produto in vendaTemp.Produtos)
                {
                    if(produto <= 0)
                    {
                        Response.StatusCode = 404;
                        return new ObjectResult(new {msg = "Id de produto está inválido"}); // Trabalhar pra informar qual Id está errado
                    }
                    try
                    {
                        var idProduto = database.produtos.First(p => p.Id == produto);

                        if(idProduto == null)
                        {
                            Response.StatusCode = 400;
                            return new ObjectResult(new {msg = "Id de produto não encontrado"}); // Trabalhar pra informar qual Id está errado
                        }
                    }
                    catch(Exception)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Id de produto não encontrado"}); // Trabalhar pra informar qual Id está errado
                    }

                    var pro = database.produtos.First(p => p.Id == produto);
                    totalCompra = Convert.ToBoolean(pro.Promocao) ? totalCompra + Convert.ToDecimal(pro.ValorPromocao) : totalCompra + pro.Valor;
                }

                if(vendaTemp.DataCompra.Length == 10)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult(new {msg = "Data está em formato errado"});
                }

                Venda venda = new Venda();

                venda.Cliente = database.clientes.First(c => c.Id == vendaTemp.Cliente);
                venda.DataCompra = DateTime.ParseExact(vendaTemp.DataCompra, "dd/MM/yyyy", null);
                venda.TotalCompra = totalCompra;
                
                database.venda.Add(venda);
                database.SaveChanges();

                foreach (var produto in vendaTemp.Produtos)
                {
                    VendaProduto vp = new VendaProduto();

                    vp.Venda = database.venda.First(v => v.Id == venda.Id);
                    vp.Fornecedor = database.fornecedores.First(f => f.Id == vendaTemp.Fornecedor);
                    vp.Produto = database.produtos.First(p => p.Id == produto);

                    database.vendasProdutos.Add(vp);
                    database.SaveChanges();
                }

                Response.StatusCode = 201;
                return new ObjectResult("");
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Todos campos devem ser passados"});
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]VendaDTO vendaTemp)
        {
            if(id > 0)
            {
                try
                {
                    decimal totalCompra = 0;
                    var venda = database.venda.First(v => v.Id == id);

                    if(venda != null)
                    {
                        foreach (var produto in vendaTemp.Produtos)
                        {
                            if(produto <= 0)
                            {
                                Response.StatusCode = 404;
                                return new ObjectResult(new {msg = "Id de produto está inválido"}); // Trabalhar pra informar qual Id está errado
                            }
                            try
                            {
                                var idProduto = database.produtos.First(p => p.Id == produto);

                                if(idProduto == null)
                                {
                                    Response.StatusCode = 400;
                                    return new ObjectResult(new {msg = "Id de produto não encontrado"}); // Trabalhar pra informar qual Id está errado
                                }
                            }
                            catch(Exception)
                            {
                                Response.StatusCode = 400;
                                return new ObjectResult(new {msg = "Id de produto não encontrado"}); // Trabalhar pra informar qual Id está errado
                            }
                            var pro = database.produtos.First(p => p.Id == produto);
                            totalCompra = Convert.ToBoolean(pro.Promocao) ? totalCompra + Convert.ToDecimal(pro.ValorPromocao) : totalCompra + pro.Valor;
                        }

                        venda.Cliente = vendaTemp.Cliente > 0 ? database.clientes.First(c => c.Id == vendaTemp.Cliente) : database.clientes.First(c => c.Id == venda.Cliente.Id);
                        venda.DataCompra = vendaTemp.DataCompra.Length == 10 ? DateTime.ParseExact(vendaTemp.DataCompra, "dd/MM/yyyy", null) : venda.DataCompra;
                        venda.TotalCompra = totalCompra;
                        //database.SaveChanges();

                        var vpAntigos = database.vendasProdutos.Where(vp => vp.Venda.Id == venda.Id).ToList();
                        database.vendasProdutos.RemoveRange(vpAntigos);
                        database.SaveChanges();

                        foreach (var produto in vendaTemp.Produtos)
                        {
                            VendaProduto vp = new VendaProduto();

                            vp.Venda = database.venda.First(v => v.Id == venda.Id);
                            vp.Fornecedor = database.fornecedores.First(f => f.Id == vendaTemp.Fornecedor);
                            vp.Produto = database.produtos.First(p => p.Id == produto);

                            database.vendasProdutos.Add(vp);
                            database.SaveChanges();
                        }

                        return Ok();
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Produto não encontrado"});
                    }
                }
                catch(Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "Venda não encontrado"});
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

                var venda = database.venda.First(v => v.Id == id);
                database.venda.Remove(venda);
                database.SaveChanges();

                return Ok();
            }
            catch(Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id de venda está inválido"});
            }
        }
    }
}