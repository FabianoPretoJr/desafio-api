using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;
using System;
using projeto.Container;
using System.Collections.Generic;

namespace projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        private HATEOAS.HATEOAS HATEOAS;
        public ClienteController(ApplicationDbContext database)
        {
            this.database = database;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/cliente");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("GET_INFO_BY_ASC", "GET");
            HATEOAS.AddAction("GET_INFO_BY_DESC", "GET");
            HATEOAS.AddAction("GET_INFO_BY_NOME", "GET");
            HATEOAS.AddAction("EDIT_PRODUCT", "PUT");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clientes = database.clientes.Where(c => c.Status == true).ToList();

            List<ClienteContainer> clientesHATEOAS = new List<ClienteContainer>();
            foreach(var cliente in clientes)
            {
                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());
                clienteHATEOAS.linksByAsc = HATEOAS.GetActions("asc");
                clienteHATEOAS.linksByDesc = HATEOAS.GetActions("desc");
                clienteHATEOAS.linksByNome = HATEOAS.GetActions("nome" + cliente.Nome);
                clientesHATEOAS.Add(clienteHATEOAS);
            }

            return Ok(clientesHATEOAS);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var cliente = database.clientes.Where(c => c.Status == true).First(c => c.Id == id);

                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());

                return Ok(clienteHATEOAS);
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
            var clientes = database.clientes.Where(c => c.Status == true).OrderBy(c => c.Nome).ToList();

            List<ClienteContainer> clientesHATEOAS = new List<ClienteContainer>();
            foreach(var cliente in clientes)
            {
                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());
                clientesHATEOAS.Add(clienteHATEOAS);
            }

            return Ok(clientesHATEOAS);
        }

        [HttpGet("desc")]
        public IActionResult GetByDesc()
        {
            var clientes = database.clientes.Where(c => c.Status == true).OrderByDescending(c => c.Nome).ToList();
            
            List<ClienteContainer> clientesHATEOAS = new List<ClienteContainer>();
            foreach(var cliente in clientes)
            {
                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());
                clientesHATEOAS.Add(clienteHATEOAS);
            }

            return Ok(clientesHATEOAS);
        }

        [HttpGet("nome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            try
            {
                var cliente = database.clientes.Where(c => c.Status == true).First(c => c.Nome.ToUpper() == nome.ToUpper());
                
                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(cliente.Id.ToString());

                return Ok(clienteHATEOAS);
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Nome não encontrado"});
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ClienteDTO clienteTemp)
        {
            try
            {
                if(clienteTemp.Nome.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O nome deve ter mais de um caracter"});
                }

                if(clienteTemp.Email.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O e-mail deve ter mais de um caracter"});
                }
                // Validar se está em formato de email

                if(clienteTemp.Senha.Length < 6 || clienteTemp.Senha.Length > 12)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "A senha deve ter mais de 6 até 12 caracter"});
                }

                if(clienteTemp.Documento.Length <= 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "O documento deve ter mais de um caracter"});
                }
                // Testar validar usando (== null) como condição

                Cliente cliente = new Cliente();

                // Implementar autenticação

                cliente.Nome = clienteTemp.Nome;
                cliente.Email = clienteTemp.Email;
                cliente.Senha = clienteTemp.Senha;
                cliente.Documento = clienteTemp.Documento;
                cliente.DataCadastro = DateTime.Now;
                cliente.Status = true;
                
                database.clientes.Add(cliente);
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
        public IActionResult Put(int id, [FromBody]ClienteDTO clienteTemp)
        {
            if(id > 0)
            {
                try
                {
                    var cli = database.clientes.First(c => c.Id == id);

                    if(cli != null)
                    {
                        cli.Nome = clienteTemp.Nome != null ? clienteTemp.Nome : cli.Nome;
                        cli.Email = clienteTemp.Email != null ? clienteTemp.Email : cli.Email;
                        cli.Senha = clienteTemp.Senha != null ? clienteTemp.Senha : cli.Senha;
                        cli.Documento = clienteTemp.Documento != null ? clienteTemp.Documento : cli.Documento;
                        database.SaveChanges();

                        return Ok();
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Cliente não encontrado"});
                    }
                }
                catch(Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "Cliente não encontrado"});
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Id de cliente está inválido"});
            }
        } 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var cliente = database.clientes.First(c => c.Id == id);
                cliente.Status = false;
                database.SaveChanges();

                return Ok();
            }
            catch(Exception)
            {  
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id de cliente está inválido"});
            }
        }
    }
}