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

        public Guid ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public int SetCount { get; set; }
        public int Repetition { get; set; }
        public decimal? Weight { get; set; } // Opsiyonel: kg
        public int? RestDurationInSeconds { get; set; } // Dinlenme süresi
        public string Note { get; set; } // Koç notu
    }
}
