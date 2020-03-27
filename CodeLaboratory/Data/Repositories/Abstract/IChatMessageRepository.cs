using CodeLaboratory.Enteties;

namespace CodeLaboratory.Data.Repositories.Abstract
{
    public interface IChatMessageRepository : ICRUDRepository<ChatMessageEntity>
    {
        void DeleteById(int id);
    }
}
