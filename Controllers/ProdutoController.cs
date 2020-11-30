using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;

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
    }
}