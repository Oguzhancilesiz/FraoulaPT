using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class WorkoutExerciseLog : BaseEntity
    {
        public Guid WorkoutExerciseId { get; set; }
        public virtual WorkoutExercise WorkoutExercise { get; set; }
        public Guid AppUserId { get; set; }
        public bool IsCompleted { get; set; } // Günün bu hareketi tamamlandı mı
        public int? ActualSetCount { get; set; } // Gerçekte yaptığı set
        public int? ActualRepetitionCount { get; set; }
        public double? ActualWeight { get; set; } // Gerçekte kaldırılan kilo
        public string UserNote { get; set; } // Zorlandım, kilo düşürdüm, vs.
        public DateTime? CompletedAt { get; set; } 
    }
}
