using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ChatMessageDTO
{
    public class MyCoachChatDTO
    {
        public Guid CoachId { get; set; }
        public string CoachFullName { get; set; }
        public string CoachAvatarUrl { get; set; }
        public int RemainingMessageCount { get; set; }
    }
}
