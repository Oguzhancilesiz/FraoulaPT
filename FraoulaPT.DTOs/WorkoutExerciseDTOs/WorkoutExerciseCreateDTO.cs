using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutExerciseDTOs
{
    public class WorkoutExerciseCreateDTO
    {
        public Guid ExerciseId { get; set; }
        public int SetCount { get; set; }
        public int RepetitionCount { get; set; }
        public decimal WeightKg { get; set; }
        public string Note { get; set; }
    }

}
