using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;
using System;
using projeto.Container;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            HATEOAS.AddAction("GET_INFO_BY_NOME", "GET");
            HATEOAS.AddAction("EDIT_PRODUCT", "PUT");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            var clientes = database.clientes.Where(c => c.Status == true).ToList();

            List<ClienteContainer> clientesHATEOAS = new List<ClienteContainer>();
            foreach(var cliente in clientes)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add("nome/" + cliente.Nome.Replace(" ", "%20"));
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add(cliente.Id.ToString());

                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                clientesHATEOAS.Add(clienteHATEOAS);
            }

            return Ok(clientesHATEOAS);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
        {
            try
            {
                var cliente = database.clientes.Where(c => c.Status == true).First(c => c.Id == id);

                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add("nome/" + cliente.Nome.Replace(" ", "%20"));
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add(cliente.Id.ToString());

                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(formatoLinks);

                return Ok(clienteHATEOAS);
            }
            catch(Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Id não encontrado"});
            }
        }

        [HttpGet("asc")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByAsc()
        {
            var clientes = database.clientes.Where(c => c.Status == true).OrderBy(c => c.Nome).ToList();

            List<ClienteContainer> clientesHATEOAS = new List<ClienteContainer>();
            foreach(var cliente in clientes)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add("nome/" + cliente.Nome.Replace(" ", "%20"));
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add(cliente.Id.ToString());

                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                clientesHATEOAS.Add(clienteHATEOAS);
            }

            return Ok(clientesHATEOAS);
        }

        [HttpGet("desc")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByDesc()
        {
            var clientes = database.clientes.Where(c => c.Status == true).OrderByDescending(c => c.Nome).ToList();
            
            List<ClienteContainer> clientesHATEOAS = new List<ClienteContainer>();
            foreach(var cliente in clientes)
            {
                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add("nome/" + cliente.Nome.Replace(" ", "%20"));
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add(cliente.Id.ToString());

                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(formatoLinks);
                clientesHATEOAS.Add(clienteHATEOAS);
            }

            return Ok(clientesHATEOAS);
        }

        [HttpGet("nome/{nome}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByNome(string nome)
        {
            try
            {
                var cliente = database.clientes.Where(c => c.Status == true).First(c => c.Nome.ToUpper() == nome.ToUpper());

                List<string> formatoLinks = new List<string>();
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add("nome/" + cliente.Nome.Replace(" ", "%20"));
                formatoLinks.Add(cliente.Id.ToString());
                formatoLinks.Add(cliente.Id.ToString());
                
                ClienteContainer clienteHATEOAS = new ClienteContainer();
                clienteHATEOAS.cliente = cliente;
                clienteHATEOAS.links = HATEOAS.GetActions(formatoLinks);

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
                cliente.Senha = clienteTemp.Senha; // Fazer HASH da senha
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] CredenciaisDTO credenciais)
        {
            // Buscar um usuário por E-mail
            // Verificar se a senha está correta
            // Gerar um token JWT e retornar esse token para o usuário

            try
            {
                Cliente cliente = database.clientes.First(u => u.Email.Equals(credenciais.Email));

                if(cliente != null)
                {
                    if(cliente.Senha.Equals(credenciais.Senha))
                    {
                        string chaveDeSeguranca = "hdjflj5fv5v45fv54v65v";

                        var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                        var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                        var claims = new List<Claim>();
                        claims.Add(new Claim("id", cliente.Id.ToString()));
                        claims.Add(new Claim("email", cliente.Email));
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                        var JWT = new JwtSecurityToken(
                            issuer: "Fabiano Preto",
                            expires: DateTime.Now.AddHours(1),
                            audience: "usuario_comum",
                            signingCredentials: credenciaisDeAcesso,
                            claims: claims
                        );

                        return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));
                    }
                    else
                    {
                        Response.StatusCode = 401;
                        return new ObjectResult("");
                    }
                }
                else
                {
                    Response.StatusCode = 401;
                    return new ObjectResult("");
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 401;
                return new ObjectResult("");
            }
        }
    }
}