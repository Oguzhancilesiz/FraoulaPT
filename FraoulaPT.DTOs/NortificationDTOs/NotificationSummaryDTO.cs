using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.NortificationDTOs
{
    public class NotificationSummaryDTO
    {
        public int UnansweredQuestionCount { get; set; }
        public int PendingChatCount { get; set; }
        public List<QuestionNotifItemDTO> Questions { get; set; } = new();
        public List<ChatNotifItemDTO> Chats { get; set; } = new();
        public DateTime ServerTime { get; set; } = DateTime.UtcNow;
    }
}
