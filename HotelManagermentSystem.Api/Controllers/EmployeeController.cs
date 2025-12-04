using HotelManagementSystem.Api.Services;
using HotelManagementSystem.Api.Services.IServices;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagermentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        //[RequiredPermission("GET:/api/employee")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }
        [HttpGet]
        [Route("by-name/{name}")]
        //[RequiredPermission("GET:/api/employee")]
        public async Task<IActionResult> GetByName(string name)
        {
            var employees = await _employeeService.GetEmployeeByNameAsync(name);
            return Ok(employees);
        }
        [HttpGet]
        [Route("by-branch/{branchId:int}")]
        //[RequiredPermission("GET:/api/employee")]
        public async Task<IActionResult> GetByBranch(int branchId)
        {
            var employees = await _employeeService.GetEmployeeByBranchAsync(branchId);
            return Ok(employees);
        }
        [HttpGet]
        [Route("by-name-and-branch")]
        //[RequiredPermission("GET:/api/employee")]
        public async Task<IActionResult> GetByNameAndBranch([FromQuery] string name, [FromQuery] int branchId)
        {
            var employees = await _employeeService.GetEmployeesByNameAndBranchAsync(name, branchId);
            return Ok(employees);
        }
        [HttpGet("{id:int}")]
        //[RequiredPermission("GET:/api/employee")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }
        [HttpPost]
        //[RequiredPermission("POST:/api/employee")]
        public async Task<IActionResult> Create(EmployeeDto employee)
        {
            await _employeeService.AddAsync(employee);
            return Ok(new { message = "Add Successful" });
        }
        [HttpPut("{id}")]
        //[RequiredPermission("PUT:/api/employee")]
        public async Task<IActionResult> Update(int id, EmployeeDto employee)
        {
            if (id != employee.Id)
                return BadRequest("ID mismatch");
            await _employeeService.UpdateAsync(employee);
            return Ok(new { message = "Update Successful" });
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyDetail()
        {
            var userClaims = HttpContext.User.Claims;
            if (userClaims == null) return NotFound();
            var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");

            if (idClaim == null) return NotFound("ID Claim not found in token."); 
            int idUser = -1;
            if (int.TryParse(idClaim.Value, out idUser))
            {
                var employee = await _employeeService.GetByIdAsync(idUser);

                if (employee == null) return NotFound("Employee not found in database.");
                return Ok(employee);
            }

            return BadRequest("User ID in token is not a valid integer.");
        }
    } 
}
