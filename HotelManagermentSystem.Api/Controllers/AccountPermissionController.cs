using HotelManagementSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [Route("api/accounts/{accountId}/permissions")]
    [ApiController]
    public class AccountPermissionController : ControllerBase
    {
        private readonly IAccountPermissionService _service;

        public AccountPermissionController(IAccountPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions(int accountId)
        {
            var permissions = await _service.GetPermissionsByAccountAsync(accountId);
            return Ok(permissions);
        }

        [HttpPost("{permissionId}")]
        public async Task<IActionResult> Assign(int accountId, int permissionId)
        {
            var success = await _service.AssignPermissionAsync(accountId, permissionId);
            return success ? Ok() : BadRequest("Permission already assigned or invalid.");
        }

        [HttpDelete("{permissionId}")]
        public async Task<IActionResult> Revoke(int accountId, int permissionId)
        {
            var success = await _service.RevokePermissionAsync(accountId, permissionId);
            return success ? NoContent() : NotFound();
        }
    }
}