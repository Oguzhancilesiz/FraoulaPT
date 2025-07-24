using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutProgramDTOs
{
    public class WorkoutProgramCreateDTO
    {
        public Guid UserWeeklyFormId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CoachNote { get; set; }
    }

}
