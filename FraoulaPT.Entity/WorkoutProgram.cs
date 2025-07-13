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
        public AppUser Coach { get; set; } // Koç atayan
        public string Description { get; set; }
        public ICollection<WorkoutDay> WorkoutDays { get; set; }
        public ICollection<UserWorkoutAssignment> Assignments { get; set; }
        public ICollection<Media> Media { get; set; } // Program PDF, görsel, video
    }

}
