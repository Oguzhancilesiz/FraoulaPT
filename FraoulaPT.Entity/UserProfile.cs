using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class UserProfile : BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Gender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public double? HeightCm { get; set; }
        public double? WeightKg { get; set; }
        public BodyType? BodyType { get; set; }
        public string BloodType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string MedicalHistory { get; set; }
        public string ChronicDiseases { get; set; }
        public string CurrentMedications { get; set; }
        public string Allergies { get; set; }
        public string PastInjuries { get; set; }
        public string CurrentPain { get; set; }
        public bool? PregnancyStatus { get; set; }
        public string LastCheckResults { get; set; } // JSON/text
        public string SmokingAlcohol { get; set; }
        public string Occupation { get; set; }
        public string ExperienceLevel { get; set; }
        public string FavoriteSports { get; set; }
        public string Notes { get; set; }
        public string DietType { get; set; }
    }

}
