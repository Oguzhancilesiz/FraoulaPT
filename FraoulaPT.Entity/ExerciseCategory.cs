using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class ExerciseCategory : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
