using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web.Models.EF;

namespace Web.UseCase
{
    public class GenerateAccessTokenUseCase :IUseCase<GenerateAccessTokenUseCase>
    {
        public string Execute(ApplicationUser user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.AccessTokenTime)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
