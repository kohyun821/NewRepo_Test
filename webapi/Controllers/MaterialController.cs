using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : Controller
    {
        private readonly MyWorldDbContext _DBmWorldDbContext;

        
        public MaterialController(MyWorldDbContext _mWorldDbContext)
        {
            _DBmWorldDbContext = _mWorldDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterial()
        {
            try
            {
                List<Material> listdepartment = _DBmWorldDbContext.Materials.ToList();
                if (listdepartment != null)
                {
                    return Ok(listdepartment);
                }
                return Ok("No Database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
