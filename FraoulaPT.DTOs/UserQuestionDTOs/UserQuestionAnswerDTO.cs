using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserQuestionDTOs
{
    public class UserQuestionAnswerDTO
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AskedByUserName { get; set; }
        public string AskedByUserPhoto { get; set; }
        public DateTime AskedAt { get; set; }
        public string? AnswerText { get; set; } // Mevcut cevap varsa düzenleme için
    }
}
