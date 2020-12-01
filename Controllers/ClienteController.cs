using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;

namespace projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public ClienteController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clientes = database.clientes.ToList();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]ClienteDTO clienteTemp)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ClienteDTO clienteTemp)
        {
            return Ok();
        } 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}