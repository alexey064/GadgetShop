using System;
using System.Security.Cryptography;
using Web.Areas.Api.Models;

namespace Web.UseCase
{
    public class GenerateRefreshTokenUseCase :IUseCase<GenerateRefreshTokenUseCase>
    {
        public RefreshToken Execute() 
        {
            RefreshToken token = new RefreshToken();
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            token.Token = Convert.ToBase64String(randomNumber);
            token.ExpiredDate = DateTime.UtcNow.AddMonths(1);
            return token;
        }
    }
}