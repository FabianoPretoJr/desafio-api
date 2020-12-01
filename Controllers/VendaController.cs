using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;

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
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]VendaDTO vendaTemp)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]VendaDTO vendatemp)
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