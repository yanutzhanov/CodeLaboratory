using System;
using System.Linq;
using System.Threading.Tasks;
using CodeLaboratory.Data.Contexts;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Enteties;
using CodeLaboratory.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CodeLaboratory.Data.Repositories
{
    public class UsersRepository :  BaseRepository<UserEntity>, IUsersRepository
    {
        private CodeLabDbContext _context;
        public UsersRepository(CodeLabDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> UserIsExist(string login, string password)
        {
            string hashedPassword = MD5Algorithm.GetHashString(password);

            UserEntity user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login &&
                                                                         x.Password == hashedPassword);

            return !(user is null);
        }

        public bool UserWithSameLoginIsExist(string login)
        {
            foreach (var user in _context.Users)
            {
                if (user.Login.ToLower() == login.ToLower())
                    return true;
            }

            return false;
        }

        public async Task<UserEntity> GetUser(string login, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login &&
                                                              x.Password == password);
        }

        public async Task<UserEntity> GetUserByLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}
