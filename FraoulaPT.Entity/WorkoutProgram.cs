using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class WorkoutProgram : BaseEntity
    {
        public Guid UserWeeklyFormId { get; set; }
        public virtual UserWeeklyForm UserWeeklyForm { get; set; }

        public string ProgramTitle { get; set; } // Opsiyonel başlık
        public string CoachNote { get; set; } // Genel açıklama

        public virtual ICollection<WorkoutDay> Days { get; set; }
    }
}
