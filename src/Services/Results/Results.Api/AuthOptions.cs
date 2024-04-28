using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Results.Api
{
    public class AuthOptions
    {
        public const string ISSUER = "Issuer";
        public const string AUDIENCE = "Audience";

        private const string KEY = "secret secret secret secret secret";

        public static SymmetricSecurityKey SymmetricKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
