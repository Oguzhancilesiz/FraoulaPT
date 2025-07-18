using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProgramDTO
{
    public class WorkoutHistorySummaryDTO
    {
        public DateTime Date { get; set; }
        public string ProgramTitle { get; set; }
        public string Day { get; set; }
        public List<WorkoutExerciseLogDTO> ExerciseLogs { get; set; }
    }

}
