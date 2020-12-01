using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;
using System.Linq;

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
            var produtos = database.produtos.ToList();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProdutoDTO produtoTemp)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProdutoDTO produtoTemp)
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