using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentalOfPremises.Infrastructure.Auth
{
    public class JWTProvider : IJWTProvider
    {
        private readonly JWTOptions _jwtOptions;
        public JWTProvider(IOptions<JWTOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = {new("userId", user.Id.ToString())};

            //настройка алгоритма
            var signingCredentials = new SigningCredentials(
                //принимает массив байт, поэтому приводим секретный ключ к массиву байт
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                //алгоритм кодирования токена
                SecurityAlgorithms.HmacSha256
                );

            //создание токена
            var token = new JwtSecurityToken(
                claims: claims,                                             //клеймы
                signingCredentials: signingCredentials,                     //настройки алгоритма
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours) //срок действия токена
                );

            //создание токена в виде строки
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
