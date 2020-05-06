using CodeLaboratory.Enteties;
using System.Threading.Tasks;

namespace CodeLaboratory.Data.Repositories.Abstract
{
    public interface IUsersRepository : ICRUDRepository<UserEntity>
    {
        Task<bool> UserIsExist(string login, string password);
        Task<UserEntity> GetUser(string login, string password);
        bool UserWithSameLoginIsExist(string login);
        Task<UserEntity> GetUserByLogin(string login);
    }
}
