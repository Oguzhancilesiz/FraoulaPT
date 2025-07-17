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
        public Guid ExerciseId { get; set; } // Seçilen hareket!
        public virtual Exercise Exercise { get; set; }

        public int SetCount { get; set; }
        public int RepetitionCount { get; set; }
        public double? Weight { get; set; } // Planlanan kilo
        public string CoachNote { get; set; }
        public virtual ICollection<WorkoutExerciseLog> Logs { get; set; }
    }
}
