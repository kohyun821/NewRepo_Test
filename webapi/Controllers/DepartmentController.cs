using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : Controller
    {

        private readonly TestDbContext _DBtestDbContext;


        public DepartmentController(TestDbContext _testDbContext)
        {
            this._DBtestDbContext = _testDbContext;
        }

        [HttpPost]
        [Route("RegistDepartment")]
        public async Task<IActionResult> RegistDepartment([FromBody] Department department)
        {
            try
            {
                //같은 이름으로 생성 불가능 하도록 
                var dbDepartment = _DBtestDbContext.Departments.Where(u => u.DepartmentName == department.DepartmentName).FirstOrDefault();
                if (dbDepartment != null)
                {
                    return BadRequest("Fail");
                }
                //department.DepartmentStatus = true;
                _DBtestDbContext.Departments.Add(department);
                await _DBtestDbContext.SaveChangesAsync();

                return Ok(department);

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            try
            {
                var departments = await _DBtestDbContext.Departments
                              .ToListAsync();

                if (!departments.Any())
                {
                    return NotFound("Department not found");
                }
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _DBtestDbContext.Departments.FindAsync(id);
                if (department == null)
                {
                    return NotFound("Department not found");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByName/{departmentName}")]
        public async Task<IActionResult> GetDepartmentByName(string departmentName)
        {
            try
            {
                //여러개 리턴 할 수 있도록 department 가 아닌 departments로 변수명 정함..
                var departments = await _DBtestDbContext.Departments
                               .Where(d => d.DepartmentName == departmentName)
                               .ToListAsync();

                if (!departments.Any())
                {
                    return NotFound("Department not found");
                }
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
