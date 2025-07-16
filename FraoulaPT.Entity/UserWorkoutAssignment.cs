using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class UserWorkoutAssignment : BaseEntity
    {
        public Guid UserPackageId { get; set; }
        public virtual UserPackage UserPackage { get; set; }
        public Guid WorkoutProgramId { get; set; }
        public virtual WorkoutProgram WorkoutProgram { get; set; }
        public DateTime AssignedDate { get; set; }
        public string CoachNote { get; set; }
    }
}
