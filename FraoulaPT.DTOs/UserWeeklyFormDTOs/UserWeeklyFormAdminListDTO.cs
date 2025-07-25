using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserWeeklyFormDTOs
{
    public class UserWeeklyFormAdminListDTO
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; } 
        public DateTime FormDate { get; set; }
        public double? Weight { get; set; }
        public double? FatPercent { get; set; }
        public double? MuscleMass { get; set; }
        public string CoachFeedback { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public List<string> ProgressPhotoUrls { get; set; } = new();
        public FraoulaPT.Core.Enums.Status Status { get; set; }
    }
}
