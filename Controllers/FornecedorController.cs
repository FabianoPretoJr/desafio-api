using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using projeto.Container;
using System.Collections.Generic;

namespace projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        private HATEOAS.HATEOAS HATEOAS;
        public FornecedorController(ApplicationDbContext database)
        {
            this.database = database;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/fornecedor");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("GET_INFO_BY_NOME", "GET");
            HATEOAS.AddAction("EDIT_PRODUCT", "PUT");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var fornecedores = database.fornecedores.Where(f => f.Status == true).Include(f => f.VendasProdutos).ToList();

            List<FornecedorContainer> fornecedoresHATEOAS = new List<FornecedorContainer>();
            foreach(var fornecedor in fornecedores)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add("nome/" + fornecedor.Nome.Replace(" ", "%20"));
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add(fornecedor.Id.ToString());

                FornecedorContainer fornecedorHATEOAS = new FornecedorContainer();
                fornecedorHATEOAS.fornecedor = fornecedor;
                fornecedorHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                fornecedoresHATEOAS.Add(fornecedorHATEOAS);
            }

            return Ok(fornecedoresHATEOAS);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var fornecedor = database.fornecedores.Where(f => f.Status == true).Include(f => f.VendasProdutos).First(f => f.Id == id);

                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add("nome/" + fornecedor.Nome.Replace(" ", "%20"));
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add(fornecedor.Id.ToString());

                FornecedorContainer fornecedorHATEOAS = new FornecedorContainer();
                fornecedorHATEOAS.fornecedor = fornecedor;
                fornecedorHATEOAS.links = HATEOAS.GetActions(formatoLinks);

                return Ok(fornecedorHATEOAS);
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
            var fornecedores = database.fornecedores.Where(f => f.Status == true).Include(f => f.VendasProdutos).OrderBy(f => f.Nome).ToList();
            
            List<FornecedorContainer> fornecedoresHATEOAS = new List<FornecedorContainer>();
            foreach(var fornecedor in fornecedores)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add("nome/" + fornecedor.Nome.Replace(" ", "%20"));
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add(fornecedor.Id.ToString());

                FornecedorContainer fornecedorHATEOAS = new FornecedorContainer();
                fornecedorHATEOAS.fornecedor = fornecedor;
                fornecedorHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                fornecedoresHATEOAS.Add(fornecedorHATEOAS);
            }

            return Ok(fornecedoresHATEOAS);
        }

        [HttpGet("desc")]
        public IActionResult GetByDesc()
        {
            var fornecedores = database.fornecedores.Where(f => f.Status == true).Include(f => f.VendasProdutos).OrderByDescending(f => f.Nome).ToList();
            
            List<FornecedorContainer> fornecedoresHATEOAS = new List<FornecedorContainer>();
            foreach(var fornecedor in fornecedores)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add("nome/" + fornecedor.Nome.Replace(" ", "%20"));
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add(fornecedor.Id.ToString());

                FornecedorContainer fornecedorHATEOAS = new FornecedorContainer();
                fornecedorHATEOAS.fornecedor = fornecedor;
                fornecedorHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                fornecedoresHATEOAS.Add(fornecedorHATEOAS);
            }

            return Ok(fornecedoresHATEOAS);
        }

        [HttpGet("nome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            try
            {
                var fornecedor = database.fornecedores.Where(f => f.Status == true).Include(f => f.VendasProdutos).First(f => f.Nome.ToUpper() == nome.ToUpper());
                
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add("nome/" + fornecedor.Nome.Replace(" ", "%20"));
                formatoLinks.Add(fornecedor.Id.ToString());
                formatoLinks.Add(fornecedor.Id.ToString());

                FornecedorContainer fornecedorHATEOAS = new FornecedorContainer();
                fornecedorHATEOAS.fornecedor = fornecedor;
                fornecedorHATEOAS.links = HATEOAS.GetActions(formatoLinks);

                return Ok(fornecedorHATEOAS);
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Nome não encontrado"});
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]FornecedorDTO fornecedorTemp)
        {
            try
            {
                if(fornecedorTemp.Nome.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O nome deve ter mais de um caracter"});
                }

                if(fornecedorTemp.CNPJ.Length != 18)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O CNPJ deve ter 18 caracteres"});
                }

                Fornecedor fornecedor = new Fornecedor();

                fornecedor.Nome = fornecedorTemp.Nome;
                fornecedor.CNPJ = fornecedorTemp.CNPJ;
                fornecedor.Status = true;

                database.fornecedores.Add(fornecedor);
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
        public IActionResult Put(int id, [FromBody]FornecedorDTO fornecedorTemp)
        {
            if(id > 0)
            {
                try
                {
                    var forne = database.fornecedores.First(f => f.Id == id);

                    if(forne != null)
                    {
                        forne.Nome = fornecedorTemp.Nome != null ? fornecedorTemp.Nome : forne.Nome;
                        forne.CNPJ = fornecedorTemp.CNPJ != null ? fornecedorTemp.CNPJ : forne.CNPJ;
                        database.SaveChanges();

                        return Ok();
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Fornecedor não encontrado"});
                    }
                }
                catch(Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "Fornecedor não encontrado"});
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "id de fornecedor está inválido"});
            }
        } 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var fornecedor = database.fornecedores.First(f => f.Id == id);
                fornecedor.Status = false;
                database.SaveChanges();

                return Ok();
            }
            catch(Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id de fornecedor está inválido"});
            }
        }
    }
}