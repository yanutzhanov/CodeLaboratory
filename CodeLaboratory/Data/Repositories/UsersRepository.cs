using System;
using System.Linq;
using CodeLaboratory.Data.Contexts;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Enteties;
using CodeLaboratory.Helpers;

namespace CodeLaboratory.Data.Repositories
{
    public class UsersRepository :  BaseRepository<UserEntity>, IUsersRepository
    {
        private CodeLabDbContext _context;
        public UsersRepository(CodeLabDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool UserIsExist(string login, string password)
        {
            string hashedPassword = MD5Algorithm.GetHashString(password);

            UserEntity user = _context.Users.FirstOrDefault(x => x.Login == login &&
                                                                         x.Password == hashedPassword);

            return !(user is null);
        }

        public bool UserWithSameLoginIsExist(string login)
        {
            //if (string.IsNullOrEmpty(login)) throw new ArgumentNullException(nameof(login));

            //UserEntity user = _context.Users.FirstOrDefault(x => x.Login.ToLower() == login.ToLower());

            //return !(user is null);

            foreach (var user in _context.Users)
            {
                if (user.Login.ToLower() == login.ToLower())
                    return true;
            }

            return false;
        }

        public UserEntity GetUser(string login, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Login == login &&
                                                              x.Password == password);
        }
    }
}
