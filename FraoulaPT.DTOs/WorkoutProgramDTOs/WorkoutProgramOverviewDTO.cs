using FraoulaPT.DTOs.UserProfileDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutProgramDTOs
{
    public class WorkoutProgramOverviewDTO
    {
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserProfilePhoto { get; set; }
        public Guid FormId { get; set; }
        public DateTime FormCreatedDate { get; set; }
        public bool HasWorkoutProgram { get; set; }
        public Guid? WorkoutProgramId { get; set; }
        public string? CoachFeedback { get; set; }
    }
}
