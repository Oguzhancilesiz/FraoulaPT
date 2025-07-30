using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserWeeklyFormDTOs
{
    public class UserWeeklyFormListDTO
    {
        public Guid Id { get; set; }
        public DateTime FormDate { get; set; }
        public Guid UserId { get; set; }

        public double? Weight { get; set; }
        public double? FatPercent { get; set; }
        public double? MuscleMass { get; set; }

        public double? Waist { get; set; }
        public double? Hip { get; set; }
        public double? Chest { get; set; }
        public Status  Status { get; set; }
        public string? CoachFeedback { get; set; }

        public bool HasWorkoutProgram { get; set; }
        public Guid? WorkoutProgramId { get; set; }
        public List<string> ProgressPhotoUrls { get; set; } = new();
    }
}
