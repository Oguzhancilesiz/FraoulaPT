using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserQuestionDTOs
{
    public class AllUserQuestionDTO
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string? AnswerText { get; set; }
        public DateTime AskedAt { get; set; }
        public DateTime? AnsweredAt { get; set; }

        // Soruyu soran kullanıcı
        public Guid AskedByUserId { get; set; }
        public string AskedByUserName { get; set; }
        public string? AskedByUserPhoto { get; set; }

        // Soruyu cevaplayan koç
        public Guid? AnsweredByCoachId { get; set; }
        public string? AnsweredByCoachName { get; set; }
        public string? AnsweredByCoachPhoto { get; set; }
    }
}
