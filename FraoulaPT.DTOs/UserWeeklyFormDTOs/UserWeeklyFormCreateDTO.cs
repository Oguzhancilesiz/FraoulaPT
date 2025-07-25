using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserWeeklyFormDTOs
{
    public class UserWeeklyFormCreateDTO
    {
        public DateTime FormDate { get; set; } = DateTime.Today;

        public double? Weight { get; set; }
        public double? FatPercent { get; set; }
        public double? MuscleMass { get; set; }
        public double? Waist { get; set; }
        public double? Hip { get; set; }
        public double? Chest { get; set; }
        public double? Arm { get; set; }
        public double? Leg { get; set; }
        public double? RestingPulse { get; set; }
        public double? BloodPressure { get; set; }
        public double? Vo2Max { get; set; }

        public string? FlexibilityNotes { get; set; }
        public string? UserNote { get; set; }

        // Fotoğraf yükleme
        public List<IFormFile>? ProgressPhotos { get; set; }
    }
}
