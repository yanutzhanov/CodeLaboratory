using CodeLaboratory.Data.Contexts;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Enteties;

namespace CodeLaboratory.Data.Repositories
{
    public class ChatMessageRepository : BaseRepository<ChatMessageEntity>, IChatMessageRepository
    {
        private readonly CodeLabDbContext _context;

        public ChatMessageRepository(CodeLabDbContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            ChatMessageEntity message = _context.ChatMessages.Find(id);
            _context.ChatMessages.Remove(message);
            _context.SaveChangesAsync();
        }
    }
}
