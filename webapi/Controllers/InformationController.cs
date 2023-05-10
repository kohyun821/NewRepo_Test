using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    public class InformationController : Controller
    {
        private readonly MyWorldDbContext _DBmWorldDbContext;


        public InformationController(MyWorldDbContext _mWorldDbContext)
        {
            _DBmWorldDbContext = _mWorldDbContext;
        }

        [HttpGet]
        [Route("Information")]
        public async Task<IActionResult> GetInformation()
        {
            try
            {
                List<Information> listdepartment = _DBmWorldDbContext.Information.ToList();
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
