using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutExerciseLogDTOs
{
    public class WorkoutExerciseLogListDTO
    {
        public Guid Id { get; set; }
        public Guid WorkoutExerciseId { get; set; }
        public Guid AppUserId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

}
