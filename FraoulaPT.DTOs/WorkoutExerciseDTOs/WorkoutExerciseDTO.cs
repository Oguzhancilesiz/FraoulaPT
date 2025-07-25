using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutExerciseDTOs
{
    public class WorkoutExerciseDTO
    {
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal? WeightKg { get; set; }
        public int RestSeconds { get; set; }
        public string? Note { get; set; }
    }
}
