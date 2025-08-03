using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FraoulaPT.Entity
{
    public class UserProfile : BaseEntity, IEntity
    {
        public Guid AppUserId { get; set; }
        public string ProfilePhotoUrl { get; set; } = string.Empty;
        public string? Instagram { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public float? HeightCm { get; set; }
        public float? WeightKg { get; set; }
        public BodyType? BodyType { get; set; }
        public BloodType? BloodType { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public string ChronicDiseases { get; set; } = string.Empty;
        public string CurrentMedications { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public string PastInjuries { get; set; } = string.Empty;
        public string CurrentPain { get; set; } = string.Empty;
        public bool? PregnancyStatus { get; set; }
        public string LastCheckResults { get; set; } = string.Empty;
        public string SmokingAlcohol { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public ExperienceLevel? ExperienceLevel { get; set; }
        public string FavoriteSports { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DietType? DietType { get; set; }

        // Kalori hesaplama için yeni alanlar
        public ActivityLevel? ActivityLevel { get; set; }
        public GoalType? GoalType { get; set; }
        public float? TargetWeight { get; set; }
        public int? DailyCalorieGoal { get; set; }
        public int? DailyProteinGoal { get; set; }
        public int? DailyCarbGoal { get; set; }
        public int? DailyFatGoal { get; set; }
        public float? BodyFatPercentage { get; set; }
        public float? MuscleMassPercentage { get; set; }
        public float? WaterPercentage { get; set; }
        public float? BoneMass { get; set; }
        public float? VisceralFat { get; set; }
        public float? MetabolicAge { get; set; }
        public float? BMR { get; set; } // Basal Metabolic Rate
        public float? TDEE { get; set; } // Total Daily Energy Expenditure

        // Navigation Properties
        public virtual AppUser AppUser { get; set; } = null!;
    }
}
