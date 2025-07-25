using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutProgramDTOs
{
    public class WorkoutProgramListDTO
    {
        public Guid Id { get; set; }
        public string ProgramName { get; set; }
        public Guid UserWeeklyFormId { get; set; }
        public DateTime AssignedDate { get; set; }
    }

}
