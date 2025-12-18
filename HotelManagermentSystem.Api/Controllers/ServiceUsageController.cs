using HotelManagementSystem.Api.Services;
using HotelManagermentSystem.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceUsageController : ControllerBase
    {
        private readonly IServiceUsageService _service;

        public ServiceUsageController(IServiceUsageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceUsageDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("reservation-detail/{detailId}")]
        public async Task<ActionResult<IEnumerable<ServiceUsageDto>>> GetByDetail(int detailId)
        {
            return Ok(await _service.GetByReservationDetailAsync(detailId));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceUsageDto>> Create(ServiceUsageDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}