using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ChatMessageDTO
{
    public class ChatViewModel
    {
        public MyCoachChatDTO Coach { get; set; }
        public Guid CurrentUserId { get; set; }
        public List<ChatMessageDTO> Messages { get; set; }
    }

}
