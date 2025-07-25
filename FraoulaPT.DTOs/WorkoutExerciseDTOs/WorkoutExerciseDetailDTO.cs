using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutExerciseDTOs
{
    public class WorkoutExerciseDetailDTO
    {
        public string ExerciseName { get; set; }
        public int SetCount { get; set; }
        public int Repetition { get; set; }
        public decimal? Weight { get; set; }
        public int? RestDurationInSeconds { get; set; }
        public string Note { get; set; }
    }

}
