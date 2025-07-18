using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProgramDTO
{
    public class WorkoutExerciseLogCreateDTO
    {
        public Guid WorkoutExerciseId { get; set; }
        public Guid AppUserId { get; set; }
        public bool IsCompleted { get; set; }
        public int? ActualSetCount { get; set; }
        public int? ActualRepetitionCount { get; set; }
        public double? ActualWeight { get; set; }
        public string UserNote { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

}
