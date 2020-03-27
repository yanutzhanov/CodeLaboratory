using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeLaboratory.Domain;

namespace CodeLaboratory.Services.Abstract
{
    public interface IChatMessageService
    {
        void Send(ChatMessage message);
        void Delete(int messageId);
    }
}
