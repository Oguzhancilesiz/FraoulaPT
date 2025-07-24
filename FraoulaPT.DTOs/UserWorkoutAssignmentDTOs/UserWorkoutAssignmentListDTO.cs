using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserWorkoutAssignmentDTOs
{
    public class UserWorkoutAssignmentListDTO
    {
        public Guid Id { get; set; }
        public Guid UserPackageId { get; set; }
        public Guid WorkoutProgramId { get; set; }
        public DateTime AssignedDate { get; set; }
    }

}
