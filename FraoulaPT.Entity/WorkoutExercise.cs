using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class WorkoutExercise : BaseEntity
    {
        public Guid WorkoutDayId { get; set; }
        public virtual WorkoutDay WorkoutDay { get; set; }
        public string ExerciseName { get; set; }
        public int SetCount { get; set; }
        public int RepCount { get; set; }
        public int? DurationSeconds { get; set; }
        public string Notes { get; set; }
        public Guid? ExerciseMediaId { get; set; }
        public virtual Media ExerciseMedia { get; set; }
    }
}
