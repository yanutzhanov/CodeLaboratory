using System;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Domain;
using CodeLaboratory.Enteties;
using CodeLaboratory.Services.Abstract;
using Mapster;

namespace CodeLaboratory.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatMessageService(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository ?? throw new ArgumentNullException(nameof(chatMessageRepository));
        }


        public void Send(ChatMessage message)
        {
            _chatMessageRepository.Create(message.Adapt<ChatMessageEntity>());
        }

        public void Delete(int messageId)
        {
            _chatMessageRepository.DeleteById(messageId);
        }
    }
}
