using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;
using System;
using projeto.Container;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
            var fornecedores = database.fornecedores.Where(f => f.Status == true).ToList();

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
                var fornecedor = database.fornecedores.Where(f => f.Status == true).First(f => f.Id == id);

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
            var fornecedores = database.fornecedores.Where(f => f.Status == true).OrderBy(f => f.Nome).ToList();
            
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
            var fornecedores = database.fornecedores.Where(f => f.Status == true).OrderByDescending(f => f.Nome).ToList();
            
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
                var fornecedor = database.fornecedores.Where(f => f.Status == true).First(f => f.Nome.ToUpper() == nome.ToUpper());
                
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
            if(ModelState.IsValid)
            {
                try
                {
                    if(!Validadores.ValidarCnpj.IsCnpj(fornecedorTemp.CNPJ))
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "CNPJ de fornecedor está inválido"});
                    }

                    Fornecedor fornecedor = new Fornecedor();

                    fornecedor.Nome = fornecedorTemp.Nome;
                    fornecedor.CNPJ = fornecedorTemp.CNPJ;
                    fornecedor.Status = true;

                    database.fornecedores.Add(fornecedor);
                    database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult(new {msg = "Fornecedor criado com sucesso"});
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
        public IActionResult Put(int id, [FromBody]FornecedorPutDTO fornecedorTemp)
        {
            if(id > 0)
            {
                try
                {
                    if(fornecedorTemp.CNPJ != null)
                    {
                        if(!Validadores.ValidarCnpj.IsCnpj(fornecedorTemp.CNPJ))
                        {
                            Response.StatusCode = 400;
                            return new ObjectResult(new {msg = "CNPJ de fornecedor está inválido"});
                        }
                    }

                    var forne = database.fornecedores.First(f => f.Id == id);

                    if(forne != null)
                    {
                        forne.Nome = fornecedorTemp.Nome != null ? fornecedorTemp.Nome : forne.Nome;
                        forne.CNPJ = fornecedorTemp.CNPJ != null ? fornecedorTemp.CNPJ : forne.CNPJ;
                        database.SaveChanges();

                        return Ok(new {msg = "Fornecedor alterado com sucesso"});
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Fornecedor não encontrado"});
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
                return new ObjectResult(new {msg = "id de fornecedor está inválido"});
            }
        } 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var fornecedor = database.fornecedores.Where(f => f.Status == true).First(f => f.Id == id);
                fornecedor.Status = false;
                database.SaveChanges();

                return Ok(new {msg = "Fornecedor deletado com sucesso"});
            }
            catch(Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id de fornecedor está inválido"});
            }
        }
    }
}