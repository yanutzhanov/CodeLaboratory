using System.Security.Claims;
using System.Threading.Tasks;
using CodeLaboratory.Domain;
using Microsoft.AspNetCore.Http;

namespace CodeLaboratory.Services.Abstract
{
    public interface IUsersService
    {
        Task Create(User user);
        Task<bool> UserIsExist(string login, string password);
        bool UserWithSameLoginIsExist(string login);
        string GetEncodedJwt(ClaimsIdentity identity);
        Task<User> GetUser(string login, string password);
        ClaimsIdentity GetIdentity(User user);
        Task Authenticate(User user, HttpContext context);
        Task<User> GetAuthenticatedUser(string login);
    }
}
