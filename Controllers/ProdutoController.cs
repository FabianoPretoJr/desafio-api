using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using projeto.Container;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        private HATEOAS.HATEOAS HATEOAS;
        public ProdutoController(ApplicationDbContext database)
        {
            this.database = database;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/produto");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("GET_INFO_BY_NOME", "GET");
            HATEOAS.AddAction("EDIT_PRODUCT", "PUT");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
        }   

        [HttpGet]
        public IActionResult Get()
        {
            var produtos = database.produtos.Where(p => p.Status == true).Include(p => p.Fornecedor).ToList();
            
            List<ProdutoContainer> produtosHATEOAS = new List<ProdutoContainer>();
            foreach(var produto in produtos)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add("nome/" + produto.Nome.Replace(" ", "%20"));
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add(produto.Id.ToString());

                ProdutoContainer produtoHATEOAS = new ProdutoContainer();
                produtoHATEOAS.produto = produto;
                produtoHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                produtosHATEOAS.Add(produtoHATEOAS);
            }

            return Ok(produtosHATEOAS);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var produto = database.produtos.Where(p => p.Status == true).Include(p => p.Fornecedor).First(p => p.Id == id);
                
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add("nome/" + produto.Nome.Replace(" ", "%20"));
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add(produto.Id.ToString());

                ProdutoContainer produtoHATEOAS = new ProdutoContainer();
                produtoHATEOAS.produto = produto;
                produtoHATEOAS.links = HATEOAS.GetActions(formatoLinks);

                return Ok(produtoHATEOAS);
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
            var produtos = database.produtos.Where(p => p.Status == true).Include(p => p.Fornecedor).OrderBy(p => p.Nome).ToList();
            
            List<ProdutoContainer> produtosHATEOAS = new List<ProdutoContainer>();
            foreach(var produto in produtos)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add("nome/" + produto.Nome.Replace(" ", "%20"));
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add(produto.Id.ToString());

                ProdutoContainer produtoHATEOAS = new ProdutoContainer();
                produtoHATEOAS.produto = produto;
                produtoHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                produtosHATEOAS.Add(produtoHATEOAS);
            }

            return Ok(produtosHATEOAS);
        }

        [HttpGet("desc")]
        public IActionResult GetByDesc()
        {
            var produtos = database.produtos.Where(p => p.Status == true).Include(p => p.Fornecedor).OrderByDescending(p => p.Nome).ToList();
            
            List<ProdutoContainer> produtosHATEOAS = new List<ProdutoContainer>();
            foreach(var produto in produtos)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add("nome/" + produto.Nome.Replace(" ", "%20"));
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add(produto.Id.ToString());

                ProdutoContainer produtoHATEOAS = new ProdutoContainer();
                produtoHATEOAS.produto = produto;
                produtoHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                produtosHATEOAS.Add(produtoHATEOAS);
            }

            return Ok(produtosHATEOAS);
        }

        [HttpGet("nome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            try
            {
                var produto = database.produtos.Where(p => p.Status == true).Include(p => p.Fornecedor).First(p => p.Nome.ToUpper() == nome.ToUpper());

                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add("nome/" + produto.Nome.Replace(" ", "%20"));
                formatoLinks.Add(produto.Id.ToString());
                formatoLinks.Add(produto.Id.ToString());

                ProdutoContainer produtoHATEOAS = new ProdutoContainer();
                produtoHATEOAS.produto = produto;
                produtoHATEOAS.links = HATEOAS.GetActions(formatoLinks);

                return Ok(produtoHATEOAS);
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Nome não encontrado"});
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody]ProdutoDTO produtoTemp)
        {
            if(ModelState.IsValid)
            {
                try
                {

                    if(produtoTemp.ValorPromocao <= 0 && produtoTemp.Promocao)
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "O valor da promoção do produto deve ser maior do que 0"});
                    }

                    if(produtoTemp.Fornecedor <= 0)
                    {
                        Response.StatusCode = 404;
                        return new ObjectResult(new {msg = "Id de fornecedor está inválido"});
                    }

                    try
                    {
                        var idFornecedor = database.fornecedores.First(f => f.Id == produtoTemp.Fornecedor);

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

                    Produto produto = new Produto();
                    Random rnd = new Random();

                    produto.Nome = produtoTemp.Nome;
                    produto.CodigoProduto = rnd.Next(1000, 9999).ToString();
                    produto.Valor = produtoTemp.Valor;
                    produto.Promocao = produtoTemp.Promocao;
                    produto.ValorPromocao = produtoTemp.Promocao ? produtoTemp.ValorPromocao : 0;
                    produto.Categoria = produtoTemp.Categoria;
                    produto.Imagem = produtoTemp.Imagem;
                    produto.Quantidade = produtoTemp.Quantidade;
                    produto.Fornecedor = database.fornecedores.First(f => f.Id == produtoTemp.Fornecedor);
                    produto.Status = true;

                    database.produtos.Add(produto);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new {msg = "Produto criado com sucesso"});
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
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody]ProdutoPutDTO produtoTemp)
        {
            if(id > 0)
            {
                if(produtoTemp.ValorPromocao <= 0 && produtoTemp.Promocao)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O valor da promoção do produto deve ser maior do que 0"});
                }

                try
                {
                    var prod = database.produtos.First(p => p.Id == id);

                    if(prod != null)
                    {
                        prod.Nome = produtoTemp.Nome != null ? produtoTemp.Nome : prod.Nome;
                        prod.Valor = produtoTemp.Valor > 0 ? produtoTemp.Valor : prod.Valor;
                        prod.Promocao = produtoTemp.Promocao.ToString().Equals("true") ? prod.Promocao : produtoTemp.Promocao;
                        prod.ValorPromocao = produtoTemp.ValorPromocao > 0 ? produtoTemp.ValorPromocao : prod.ValorPromocao;
                        prod.Categoria = produtoTemp.Categoria != null ? produtoTemp.Categoria : prod.Categoria;
                        prod.Imagem = produtoTemp.Imagem != null ? produtoTemp.Imagem : prod.Imagem;
                        prod.Quantidade = produtoTemp.Quantidade > 0 ? produtoTemp.Quantidade : prod.Quantidade;
                        prod.Fornecedor = produtoTemp.Fornecedor > 0 ? database.fornecedores.First(f => f.Id == produtoTemp.Fornecedor) : prod.Fornecedor;
                        database.SaveChanges();

                        return Ok(new {msg = "Produto alterado com sucesso"});
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Produto não encontrado"});
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
                return new ObjectResult(new {msg = "Id de produto está inválido"});
            }
        } 

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var produto = database.produtos.Where(p => p.Status == true).First(p => p.Id == id);
                produto.Status = false;
                database.SaveChanges();

                return Ok(new {msg = "Produto deletado com sucesso"});
            }
            catch(Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id de produto está inválido"});
            }
        }
    }
}