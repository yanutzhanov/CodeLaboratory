using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
