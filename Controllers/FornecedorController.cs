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
    public class FornecedorController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public FornecedorController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var fornecedores = database.fornecedores.Where(f => f.Status == true).Include(f => f.VendasProdutos).ToList();
            return Ok(fornecedores);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var fornecedor = database.fornecedores.Where(f => f.Status == true).Include(f => f.VendasProdutos).First(f => f.Id == id);

                return Ok(fornecedor);
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Id não encontrado"});
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