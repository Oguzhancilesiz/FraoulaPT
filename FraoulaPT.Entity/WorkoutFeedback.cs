using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class WorkoutFeedback : BaseEntity
    {
        public Guid WorkoutExerciseId { get; set; }
        public virtual WorkoutExercise WorkoutExercise { get; set; }

        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;

        public string FeedbackText { get; set; } // "Bu set zor geldi, tekrar azalttım" gibi
        public int? ActualReps { get; set; }     // Gerçekleşen tekrar
        public decimal? ActualWeight { get; set; } // Gerçekleşen kilo
        public int? RPE { get; set; } // Rating of Perceived Exertion (1–10)
    }
}
