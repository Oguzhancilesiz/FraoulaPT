using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; } // Opsiyonel, Youtube/Drive vs.
        public string ImageUrl { get; set; } // Hareketi gösteren resim
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
