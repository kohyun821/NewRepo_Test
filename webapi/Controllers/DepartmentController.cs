using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly MyWorldDbContext _DBmWorldDbContext;


        public DepartmentController(MyWorldDbContext _mWorldDbContext)
        {
            _DBmWorldDbContext = _mWorldDbContext;
        }

        [HttpGet]
        [Route("Department")]
        public async Task<IActionResult> GetDepartment()
        {
            try
            {
                List<Department> listdepartment = _DBmWorldDbContext.Departments.ToList();
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
