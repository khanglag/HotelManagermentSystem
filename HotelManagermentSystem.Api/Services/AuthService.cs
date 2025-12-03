using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Services.IService;
using HotelManagementSystem.Api.Repositories.IRepsiories;

namespace HotelManagementSystem.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        public AuthService(IConfiguration configuration, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
        }
        public string GenerateJwtToken(Account account)
        {
            // Lấy danh sách quyền
            var permissions = account.AccountPermissions?
                .Select(p => $"{p.Permission.Method.ToUpper()}:{p.Permission.Path.ToLower()}")
                .ToList() ?? new List<string>();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Username),
                new Claim("id", account.Id.ToString()),
                new Claim("role", account.Role.ToString())
            };

            // Thêm quyền vào token
            foreach (var perm in permissions)
            {
                claims.Add(new Claim("perm", perm));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);

        }

        public async Task<Account?> RefreshTokenAsync(string refreshToken, string userName)
        {
            var account = await _accountRepository.GetByUserNameAsync(userName);
            if (account == null || account.RefreshToken != refreshToken || account.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            var newRefreshToken = GenerateRefreshToken();
            account.RefreshToken = newRefreshToken;
            account.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _accountRepository.UpdateAsync(account);
            return account;
        }
    }
}
