using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InformationController : Controller
    {
        private readonly TestDbContext _DBtestDbContext;


        public InformationController(TestDbContext _testDbContext)
        {
            this._DBtestDbContext = _testDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetInformation()
        {
            try
            {
                var informations = await _DBtestDbContext.Information
                             .ToListAsync();

                if (!informations.Any())
                {
                    return NotFound("Information not found");
                }
                return Ok(informations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
