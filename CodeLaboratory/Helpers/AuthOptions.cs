using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CodeLaboratory.Helpers
{
    public class AuthOptions
    {
        public const string ISSUER = "CodeLabAOYU";
        public const string AUDIENCE = "CodeLabUser";
        const string KEY = "NoOneKnowsThatThere'sNoSecretKey!";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
