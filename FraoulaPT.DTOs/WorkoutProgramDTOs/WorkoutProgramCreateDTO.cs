using FraoulaPT.DTOs.WorkoutDayDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutProgramDTOs
{
    public class WorkoutProgramCreateDTO
    {
        public string ProgramName { get; set; }
        public string CoachNote { get; set; }    // ← Eksik olan
        public Guid UserWeeklyFormId { get; set; }
        public List<WorkoutDayCreateDTO> Days { get; set; }
    }
}
