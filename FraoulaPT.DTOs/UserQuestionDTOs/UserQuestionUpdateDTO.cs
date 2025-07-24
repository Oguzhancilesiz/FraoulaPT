using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserQuestionDTOs
{
    public class UserQuestionUpdateDTO
    {
        public Guid Id { get; set; }
        public string AnswerText { get; set; }
        public Guid? AnsweredByCoachId { get; set; }
        public DateTime? AnsweredAt { get; set; }
    }

}
