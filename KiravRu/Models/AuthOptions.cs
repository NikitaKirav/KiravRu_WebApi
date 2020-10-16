using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace KiravRu.Models
{
    public class AuthOptions
    {
        public string ISSUER = Environment.GetEnvironmentVariable("AUTHTOKEN_ISSUER"); // издатель токена
        public string AUDIENCE = Environment.GetEnvironmentVariable("AUTHTOKEN_AUDIENCE"); // потребитель токена
        private string KEY = Environment.GetEnvironmentVariable("AUTHTOKEN_KEY");   // ключ для шифрации не менее 16 символов
        public const int LIFETIME = 300; // время жизни токена - 300 минут
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
