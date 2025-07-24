using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutDayDTOs
{
    public class WorkoutDayCreateDTO
    {
        public Guid WorkoutProgramId { get; set; }
        public int DayOfWeek { get; set; }
    }
}
