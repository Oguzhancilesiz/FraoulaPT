using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProgramDTO
{
    public class WorkoutDayDTO
    {
        public Guid Id { get; set; }
        public Guid WorkoutProgramId { get; set; }
        public int DayOfWeek { get; set; } // 1 = Pazartesi, 7 = Pazar gibi
        public List<WorkoutExerciseDTO> Exercises { get; set; }
    }

}
