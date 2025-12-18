using HotelManagermentSystem.Api.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagermentSystem.Api.Controllers
{
    [Route("api/rooms/{roomId}/amenities")]
    [ApiController]
    public class RoomAmenityController : ControllerBase
    {
        private readonly IRoomAmenityService _service;

        public RoomAmenityController(IRoomAmenityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAmenities(int roomId)
        {
            return Ok(await _service.GetAmenitiesByRoomAsync(roomId));
        }

        [HttpPost("{amenityId}")]
        public async Task<IActionResult> AddAmenity(int roomId, int amenityId)
        {
            var success = await _service.AddAmenityToRoomAsync(roomId, amenityId);
            return success ? Ok() : BadRequest("Amenity already exists in this room or invalid ID.");
        }

        [HttpDelete("{amenityId}")]
        public async Task<IActionResult> RemoveAmenity(int roomId, int amenityId)
        {
            var success = await _service.RemoveAmenityFromRoomAsync(roomId, amenityId);
            return success ? NoContent() : NotFound();
        }
    }
}
