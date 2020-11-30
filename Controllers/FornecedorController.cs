using Microsoft.AspNetCore.Mvc;
using projeto.Data;
using projeto.Models;
using projeto.DTO;

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
    }
}