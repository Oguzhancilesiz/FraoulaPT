using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserWeeklyFormDTOs
{
    public class UserWeeklyFormUpdateDTO
    {
        public Guid Id { get; set; }

        public DateTime FormDate { get; set; }

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

        public string FlexibilityNotes { get; set; }
        public string UserNote { get; set; }

        // Koç tarafından güncellenebilir
        public string CoachFeedback { get; set; }

        // Yeni ek fotoğraflar
        public List<IFormFile> NewProgressPhotos { get; set; }

        // Silinecek mevcut görsel ID'leri
        public List<Guid> DeletedMediaIds { get; set; }
    }
}
