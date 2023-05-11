using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly TestDbContext _DBtestDbContext;


        public EmployeeController(TestDbContext _testDbContext)
        {
            this._DBtestDbContext = _testDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                var emplyees = await _DBtestDbContext.Employees
                                .Include(e => e.Department)
                                .Select(e=> new
                                {
                                    e.EmployeeId,
                                    e.EmployeeName,
                                    e.DepartmentId,
                                    DepartmentName = e.Department.DepartmentName
                                })
                              .ToListAsync();

                if (!emplyees.Any())
                {
                    return NotFound("Employee not found");
                }
                return Ok(emplyees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
