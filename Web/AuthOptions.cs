using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Web
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "H+MbQeThWmZq4t7w!z%C*F)J@NcRfUjXn2r5u8x/A?D(G+KaPdSgVkYp3s6v9y$B";   // ключ для шифрации
        public const int AccessTokenTime = 10; //время действия токена доступа
        public const int RefreshTokenTime = 30; //время действия токена обновления
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
