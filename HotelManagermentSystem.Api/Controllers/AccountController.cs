using HotelManagementSystem.Api.Dtos;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public AccountController(IAccountService accountService, IAuthService authService)
        {
            _accountService = accountService;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var account = await _accountService.LoginAsync(dto.Username, dto.Password);

            if (account == null)
                return Unauthorized(new { message = "Đăng nhập thất bại" });
            
            var token = _authService.GenerateJwtToken(account);
            var refreshToken = _authService.GenerateRefreshToken();
            account.RefreshToken = refreshToken;
            account.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _accountService.UpdateAsync(account);

            return Ok(new
            {
                token,
                refreshToken
            });
        }
    }
}
