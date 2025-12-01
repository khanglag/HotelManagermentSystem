using HotelManagementSystem.Api.Dtos;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories.IRepsiories;
using HotelManagementSystem.Api.Services;
using HotelManagementSystem.Api.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService  )
        {
            _customerService = customerService;
        }
        [HttpGet]
        [RequiredPermission("GET:/api/customer")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }
        [HttpPost]
        [RequiredPermission("POST:/api/customer")]
        public async Task<IActionResult> Create(CustomerDto customer)
        {
            await _customerService.AddAsync(customer);
            return Ok(new { message = "Add Successful" });
        }
        [HttpGet("{id:int}")]
        [RequiredPermission("GET:/api/customer")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
        [HttpGet("by-email/{email}")]
        [RequiredPermission("GET:/api/customer")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var customer = await _customerService.GetByEmailAsync(email);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
        [HttpGet("by-phone/{phone}")]
        [RequiredPermission("GET:/api/customer")]
        public async Task<IActionResult> GetByPhoneNumber(string phonenumber)
        {
            var customer = await _customerService.GetByPhoneNumberAsync(phonenumber);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
        [HttpPut("{id}")]
        [RequiredPermission("PUT:/api/customer")]
        public async Task<IActionResult> Update(int id, CustomerDto dto)
        {
            dto.ID = id;
            await _customerService.UpdateAsync(dto);
            return Ok(new { message = "Update successfull" });
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyDetail()
        {
            var userClaims = HttpContext.User.Claims;
            if (userClaims == null) return NotFound();
            var idClaim = userClaims.FirstOrDefault();
            if (idClaim == null) return NotFound();
            int idUser = -1;
            if (int.TryParse(idClaim.Value, out int id))
                idUser = id;

            var customer = await _customerService.GetByIdAsync(idUser);

            if (customer == null) return NotFound();
            return Ok(customer);
        }
    }
}
