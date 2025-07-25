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
        public virtual WorkoutProgram WorkoutProgram { get; set; }

        public int DayOfWeek { get; set; } // 1= Pazartesi, 7= Pazar
        public string Description { get; set; } // O gün için açıklama

        public virtual ICollection<WorkoutExercise> Exercises { get; set; }
    }
}
