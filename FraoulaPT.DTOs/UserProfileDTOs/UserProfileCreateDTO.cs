using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProfileDTOs
{
    public class UserProfileCreateDTO
    {
        public Guid AppUserId { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public string? Instagram { get; set; }
        public FraoulaPT.Core.Enums.Gender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public double? HeightCm { get; set; }
        public double? WeightKg { get; set; }
        public FraoulaPT.Core.Enums.BodyType? BodyType { get; set; }
        public FraoulaPT.Core.Enums.BloodType? BloodType { get; set; }
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
        public string LastCheckResults { get; set; }
        public string SmokingAlcohol { get; set; }
        public string Occupation { get; set; }
        public FraoulaPT.Core.Enums.ExperienceLevel? ExperienceLevel { get; set; }
        public string FavoriteSports { get; set; }
        public string Notes { get; set; }
        public FraoulaPT.Core.Enums.DietType? DietType { get; set; }

        // Kalori hesaplama için yeni alanlar
        public FraoulaPT.Core.Enums.ActivityLevel? ActivityLevel { get; set; }
        public FraoulaPT.Core.Enums.GoalType? GoalType { get; set; }
        public double? TargetWeight { get; set; }
        public int? DailyCalorieGoal { get; set; }
        public int? DailyProteinGoal { get; set; }
        public int? DailyCarbGoal { get; set; }
        public int? DailyFatGoal { get; set; }
        public double? BodyFatPercentage { get; set; }
        public double? MuscleMassPercentage { get; set; }
        public double? WaterPercentage { get; set; }
        public double? BoneMass { get; set; }
        public double? VisceralFat { get; set; }
        public double? MetabolicAge { get; set; }
        public double? BMR { get; set; } // Basal Metabolic Rate
        public double? TDEE { get; set; } // Total Daily Energy Expenditure
    }
}
