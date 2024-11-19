using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using app.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace app.Services
{
    public interface ITokenService
    {
        public string GetAccessToken(IEnumerable<Claim> claims);
        public string GetRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }

    public class TokenService(AuthOptions options) : ITokenService
    {
        public string GetAccessToken(IEnumerable<Claim> claims)
        {
            DateTime expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(2));
            SymmetricSecurityKey key = options.GetSymmetricSecurityKey();
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwt = new(
                    issuer: options.issuer,
                    audience: options.audience,
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials
            );
            JwtSecurityTokenHandler handler = new();
            return handler.WriteToken(jwt);
        }

        public string GetRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToHexString(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = options.issuer,
                ValidAudience = options.audience,
                IssuerSigningKey = options.GetSymmetricSecurityKey(),
            };
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }

    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddTokenService(this IServiceCollection services, IConfiguration configuration)
        {
            AuthOptions opt = new(configuration);

            // Добавляем в приложениие сервис аутентификации через jwt
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = opt.issuer,
                    ValidateAudience = true,
                    ValidAudience = opt.audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = opt.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddTransient<ITokenService>(t => new TokenService(opt));
            return services;
        }
    }
}