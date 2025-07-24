using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserQuestionDTOs
{
    public class UserQuestionDetailDTO
    {
        public Guid Id { get; set; }
        public Guid UserPackageId { get; set; }
        public Guid AskedByUserId { get; set; }
        public Guid? AnsweredByCoachId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public DateTime AskedAt { get; set; }
        public DateTime? AnsweredAt { get; set; }
    }

}
