using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagermentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _service;

        public BranchController(IBranchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetAll()
        {
            return Ok(await _service.GetAllBranchesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDto>> GetById(int id)
        {
            var branch = await _service.GetBranchByIdAsync(id);
            return branch == null ? NotFound() : Ok(branch);
        }

        [HttpPost]
        public async Task<ActionResult<BranchDto>> Create(BranchDto branchDto)
        {
            var result = await _service.CreateBranchAsync(branchDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BranchDto branchDto)
        {
            var success = await _service.UpdateBranchAsync(id, branchDto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteBranchAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
