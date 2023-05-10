using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MyWorldDbContext _DBmWorldDbContext;


        public EmployeeController(MyWorldDbContext _mWorldDbContext)
        {
            _DBmWorldDbContext = _mWorldDbContext;
        }

        [HttpGet]
        [Route("Employee")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                List<Employee> listdepartment = _DBmWorldDbContext.Employees.ToList();
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
