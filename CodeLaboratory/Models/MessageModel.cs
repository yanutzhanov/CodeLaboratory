using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLaboratory.Models
{
    public class MessageModel
    {
        public string SenderLogin { get; set; }
        public string Message { get; set; }
        public int ProjectId { get; set; } = 0;
        public bool Global { get; set; }
    }
}
