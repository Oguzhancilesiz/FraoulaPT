using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProgramDTO
{
    public class UserWorkoutAssignmentDTO
    {
        public Guid Id { get; set; }
        public Guid UserPackageId { get; set; }
        public Guid WorkoutProgramId { get; set; }
        public DateTime AssignedDate { get; set; }
        public string CoachNote { get; set; }
        public WorkoutProgramDTO WorkoutProgram { get; set; } // Opsiyonel: Atanan program detayları
    }

}
