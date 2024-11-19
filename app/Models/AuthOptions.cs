using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace app.Models
{
    public class AuthOptions(IConfiguration configuration)
    {

        public readonly string? issuer = configuration["Jwt:Issuer"]; // издатель токена
        public readonly string? audience = configuration["Jwt:Audience"]; // потребитель токена
        private readonly string key = configuration["Jwt:Key"] ?? throw new Exception("No secret key was provided");
        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(key));
    }
}