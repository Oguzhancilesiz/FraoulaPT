using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutExerciseDTOs
{
    public class WorkoutExerciseCreateDTO
    {
        public Guid WorkoutDayId { get; set; }
        public Guid ExerciseId { get; set; }
        public int SetCount { get; set; }
        public int RepetitionCount { get; set; }
        public double? Weight { get; set; }
        public string CoachNote { get; set; }
    }

}
