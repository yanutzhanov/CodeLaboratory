using System.ComponentModel.DataAnnotations.Schema;
using CodeLaboratory.Enteties.Abstract;

namespace CodeLaboratory.Enteties
{
    [Table("chat_messages")]
    public class ChatMessageEntity : BaseEntity
    {
        public string SenderLogin { get; set; }
        public string Message { get; set; }
        public int ProjectId { get; set; } = 0;
        public bool Global { get; set; }
    }
}
