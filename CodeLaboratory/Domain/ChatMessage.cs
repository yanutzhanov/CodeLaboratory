namespace CodeLaboratory.Domain
{
    public class ChatMessage
    {
        public string SenderLogin { get; set; }
        public string Message { get; set; }
        public int ProjectId { get; set; } = 0;
        public bool Global { get; set; }
    }
}
