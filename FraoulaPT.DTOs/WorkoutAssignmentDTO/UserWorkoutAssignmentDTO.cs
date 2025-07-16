using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutAssignmentDTO
{
    public class UserWorkoutAssignmentDTO
    {
        public Guid Id { get; set; }
        public DateTime AssignedDate { get; set; }
        public string CoachNote { get; set; }
        public WorkoutProgramDTO WorkoutProgram { get; set; }
    }
}
