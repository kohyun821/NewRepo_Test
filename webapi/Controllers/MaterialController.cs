using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : Controller
    {
        private readonly TestDbContext _DBtestDbContext;


        public MaterialController(TestDbContext _testDbContext)
        {
            this._DBtestDbContext = _testDbContext;
        }

        [HttpPost]
        [Route("RegistMaterial")]
        public async Task<IActionResult> RegistMaterial([FromBody] Material material)
        {
            try
            {
                var dbMaterial = _DBtestDbContext.Materials;
                material.MaterialName = material.MaterialName;
                material.MaterialStatus = true;
                _DBtestDbContext.Materials.Add(material);
                await _DBtestDbContext.SaveChangesAsync();

                return Ok("Success");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetMaterial()
        {
            try
            {
                var materials = await _DBtestDbContext.Materials
                             .ToListAsync();

                if (!materials.Any())
                {
                    return NotFound("Material not found");
                }
                return Ok(materials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
