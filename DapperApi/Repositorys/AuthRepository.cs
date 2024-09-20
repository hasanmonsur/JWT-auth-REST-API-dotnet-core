using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DapperApi.Repositorys
{
    public class AuthRepository
    {

        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string username)
        {
            // Define claims (you can add more claims as per your requirement)
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // Get the secret key and generate the signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expiration time
                signingCredentials: creds);

            // Return the serialized token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
