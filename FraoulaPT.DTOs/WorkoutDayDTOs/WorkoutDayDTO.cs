using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutDayDTOs
{
    public class WorkoutDayDTO
    {
        public int DayOfWeek { get; set; } // 0 = Pazartesi
        public List<WorkoutExerciseDTO> Exercises { get; set; }
    }
}
