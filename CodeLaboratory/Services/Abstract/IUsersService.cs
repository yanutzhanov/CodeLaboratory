using System.Security.Claims;
using CodeLaboratory.Domain;

namespace CodeLaboratory.Services.Abstract
{
    public interface IUsersService
    {
        void Create(User user);
        bool UserIsExist(string login, string password);
        bool UserWithSameLoginIsExist(string login);
        string GetEncodedJwt(ClaimsIdentity identity);
        User GetUser(string login, string password);
        ClaimsIdentity GetIdentity(User user);
    }
}
