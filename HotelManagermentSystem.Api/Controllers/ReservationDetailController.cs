using HotelManagementSystem.Api.Services;
using HotelManagermentSystem.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [Route("api/reservations/{reservationId}/details")]
    [ApiController]
    public class ReservationDetailController : ControllerBase
    {
        private readonly IReservationDetailService _service;

        public ReservationDetailController(IReservationDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDetailDto>>> GetByReservation(int reservationId)
        {
            return Ok(await _service.GetByReservationIdAsync(reservationId));
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDetailDto>> Create(int reservationId, ReservationDetailDto dto)
        {
            dto.ReservationId = reservationId;
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { reservationId, id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDetailDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}