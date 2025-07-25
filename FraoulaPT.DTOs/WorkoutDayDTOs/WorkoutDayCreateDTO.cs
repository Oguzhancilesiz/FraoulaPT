using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutDayDTOs
{
    public class WorkoutDayCreateDTO
    {
        public int DayOfWeek { get; set; }
        public List<WorkoutExerciseCreateDTO> Exercises { get; set; }
    }

}
