using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserWeeklyFormDTO
{
    public class UserWeeklyFormCoachFeedbackDTO
    {
        public Guid FormId { get; set; }
        public string CoachFeedback { get; set; }
    }
}
