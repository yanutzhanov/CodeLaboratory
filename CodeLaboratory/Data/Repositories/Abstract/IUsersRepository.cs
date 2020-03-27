using CodeLaboratory.Enteties;

namespace CodeLaboratory.Data.Repositories.Abstract
{
    public interface IUsersRepository : ICRUDRepository<UserEntity>
    {
        bool UserIsExist(string login, string password);
        UserEntity GetUser(string login, string password);
        bool UserWithSameLoginIsExist(string login);
    }
}
