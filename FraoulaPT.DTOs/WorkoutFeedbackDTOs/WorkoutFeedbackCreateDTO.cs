using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutFeedbackDTOs
{
    public class WorkoutFeedbackCreateDTO
    {
        public Guid WorkoutExerciseId { get; set; }
        public string FeedbackText { get; set; }
        public int? ActualReps { get; set; }
        public decimal? ActualWeight { get; set; }
        public int? RPE { get; set; }
    }
}
