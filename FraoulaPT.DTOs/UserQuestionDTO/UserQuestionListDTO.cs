using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserQuestionDTO
{
    public class UserQuestionListDTO
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public DateTime AskedAt { get; set; }
        public DateTime? AnsweredAt { get; set; }
        public string CoachName { get; set; }
        public bool IsAnswered => !string.IsNullOrEmpty(AnswerText);
    }
}
