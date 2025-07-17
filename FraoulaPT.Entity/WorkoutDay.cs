using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class WorkoutDay : BaseEntity
    {
        public Guid WorkoutProgramId { get; set; }
        public int DayOfWeek { get; set; }
        public virtual ICollection<WorkoutExercise> Exercises { get; set; }
    }

}
