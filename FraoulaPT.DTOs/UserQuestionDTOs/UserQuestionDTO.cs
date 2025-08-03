using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserQuestionDTOs
{
    public class UserQuestionDTO
    {
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string CoachName { get; set; }
        public DateTime AskedAt { get; set; }
        public DateTime? AnsweredAt { get; set; }
    }
}
