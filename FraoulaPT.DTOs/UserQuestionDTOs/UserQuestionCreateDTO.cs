using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserQuestionDTOs
{
    public class UserQuestionCreateDTO
    {
        public Guid UserPackageId { get; set; }
        public Guid AskedByUserId { get; set; }
        public string QuestionText { get; set; }
    }

}
