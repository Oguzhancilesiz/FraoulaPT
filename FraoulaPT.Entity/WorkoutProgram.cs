using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class WorkoutProgram : BaseEntity
    {
        public string Name { get; set; }
        public Guid? CoachId { get; set; }
        public virtual AppUser Coach { get; set; }
        public string Description { get; set; }
        public virtual ICollection<WorkoutDay> WorkoutDays { get; set; }
        public virtual ICollection<UserWorkoutAssignment> Assignments { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}
