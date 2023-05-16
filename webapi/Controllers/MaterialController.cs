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
                var dbMaterial = _DBtestDbContext.Materials.Where(x => x.MaterialName == material.MaterialName).FirstOrDefault();
                if(dbMaterial != null)
                {
                    return BadRequest("이름이 이미 있습니다.");
                }
                if (material.MaterialAmount <0 || material.MaterialTotal < 0)
                {
                    return BadRequest("0보다 작은 수는 불가능 합니다.");
                }
                if (material.MaterialAmount > material.MaterialTotal)
                {
                    return BadRequest("현재 수량은 총합보다 작거나 같아야 합니다.");
                }
                _DBtestDbContext.Materials.Add(material);
                await _DBtestDbContext.SaveChangesAsync();


                return Ok(material);

            }
            catch (Exception ex)
            {
                //return BadRequest("다시 확인해 주세요");
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
