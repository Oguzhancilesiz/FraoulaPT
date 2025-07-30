using FraoulaPT.DTOs.AppUserDTOs;
using FraoulaPT.DTOs.ExerciseDTOs;
using FraoulaPT.DTOs.WorkoutDayDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutProgramDTOs
{
    public class WorkoutProgramDetailDTO
    {
        public Guid Id { get; set; }
        public string ProgramName { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid UserWeeklyFormId { get; set; }
        public string CoachNote { get; set; }
        public List<WorkoutDayDetailDTO> Days { get; set; }

        public List<ExerciseListDTO> Exercises { get; set; } = new();
        public AppUserDetailDTO User { get; set; }

    }

}
