using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public ProdutoController(ApplicationDbContext database)
        {
            this.database = database;
        }   

        [HttpGet]
        public IActionResult Get()
        {
            var produtos = database.produtos.Include(p => p.Fornecedor).Include(p => p.VendasProdutos).ToList();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var produto = database.produtos.Where(p => p.Status == true).Include(p => p.Fornecedor).Include(p => p.VendasProdutos).First(p => p.Id == id);
                
                return Ok(produto);
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
            var produtos = database.produtos.Where(p => p.Status == true).OrderBy(p => p.Nome).ToList();
            return Ok(produtos);
        }

        [HttpGet("desc")]
        public IActionResult GetByDesc()
        {
            var produtos = database.produtos.Where(p => p.Status == true).OrderByDescending(p => p.Nome).ToList();
            return Ok(produtos);
        }

        [HttpGet("nome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            try
            {
                var produto = database.produtos.Where(p => p.Status == true).First(p => p.Nome.ToUpper() == nome.ToUpper());

                return Ok(produto);
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Nome não encontrado"});
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProdutoDTO produtoTemp)
        {
            try
            {
                if(produtoTemp.Nome.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O nome deve ter mais de um caracter"});
                }

                if(produtoTemp.Valor <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O valor do produto deve ser maior do que 0 (zero)"});
                }

                if(produtoTemp.ValorPromocao <= 0 && produtoTemp.Promocao)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O valor da promoção deve ser maior do que 0 (zero)"});
                }

                if(produtoTemp.Categoria.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "A categoria deve ter mais de um caracter"});
                }

                if(produtoTemp.Imagem.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O link da imagem deve ter mais de um caracter"});
                }

                if(produtoTemp.Quantidade <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "A quantidade deve ser pelo menos maior do que 0 (zero)"});
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
                produto.Promocao = produtoTemp.Promocao; // Talvez fazer um if pra impedir atribuição de valor ao valorPromocao caso seja false promocao, um ternario resolve
                produto.ValorPromocao = produtoTemp.ValorPromocao;
                produto.Categoria = produtoTemp.Categoria;
                produto.Imagem = produtoTemp.Imagem;
                produto.Quantidade = produtoTemp.Quantidade;
                produto.Fornecedor = database.fornecedores.First(f => f.Id == produtoTemp.Fornecedor);
                produto.Status = true;

                database.produtos.Add(produto);
                database.SaveChanges();

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
        public IActionResult Put(int id, [FromBody]ProdutoDTO produtoTemp)
        {
            if(id > 0)
            {
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
                    return new ObjectResult(new {msg = "Produto não encontrado"});
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Id de produto está inválido"});
            }
        } 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var produto = database.produtos.First(p => p.Id == id);
                produto.Status = false;
                database.SaveChanges();

                return Ok();
            }
            catch(Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id de produto está inválido"});
            }
        }
    }
}