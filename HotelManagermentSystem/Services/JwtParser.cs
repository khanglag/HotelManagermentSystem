using System.Security.Claims;
using System.Text.Json;

namespace HotelManagermentSystem.Services
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes);

            if (keyValuePairs == null) return claims;

            foreach (var kvp in keyValuePairs)
            {
                // 👉 map chuẩn cho Blazor
                if (kvp.Key == "sub")
                {
                    claims.Add(new Claim(ClaimTypes.Name, kvp.Value.GetString()!));
                }
                else if (kvp.Key == "role")
                {
                    claims.Add(new Claim(ClaimTypes.Role, kvp.Value.GetString()!));
                }
                else if (kvp.Key == "perm" && kvp.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var p in kvp.Value.EnumerateArray())
                    {
                        claims.Add(new Claim("perm", p.GetString()!));
                    }
                }
                else
                {
                    claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
                }
            }

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
