using FraoulaPT.Core.Enums;
using FraoulaPT.Services.Abstracts;

namespace FraoulaPT.Services.Concrete
{
    public class CalorieCalculationService : ICalorieCalculationService
    {
        public float CalculateBMR(float weight, float height, int age, Gender gender)
        {
            // Mifflin-St Jeor Equation
            if (gender == Gender.Male)
            {
                return (10 * weight) + (6.25f * height) - (5 * age) + 5;
            }
            else
            {
                return (10 * weight) + (6.25f * height) - (5 * age) - 161;
            }
        }

        public float CalculateTDEE(float bmr, ActivityLevel activityLevel)
        {
            float multiplier = activityLevel switch
            {
                ActivityLevel.Sedentary => 1.2f,
                ActivityLevel.LightlyActive => 1.375f,
                ActivityLevel.ModeratelyActive => 1.55f,
                ActivityLevel.VeryActive => 1.725f,
                ActivityLevel.ExtremelyActive => 1.9f,
                _ => 1.2f
            };

            return bmr * multiplier;
        }

        public int CalculateTargetCalories(float tdee, GoalType goalType, float? currentWeight = null, float? targetWeight = null)
        {
            float calorieAdjustment = goalType switch
            {
                GoalType.WeightLoss => -500f, // Günlük 500 kalori açık
                GoalType.WeightGain => 300f,  // Günlük 300 kalori fazla
                GoalType.Maintenance => 0f,    // Değişiklik yok
                GoalType.MuscleGain => 200f,   // Hafif fazla
                GoalType.FatLoss => -300f,     // Orta açık
                _ => 0f
            };

            return (int)(tdee + calorieAdjustment);
        }

        public MacroNutrients CalculateMacroNutrients(int targetCalories, GoalType goalType, float? bodyFatPercentage = null)
        {
            var macros = new MacroNutrients();

            // Protein hesaplama
            float proteinPercentage = goalType switch
            {
                GoalType.MuscleGain => 0.3f,    // %30
                GoalType.FatLoss => 0.35f,       // %35
                GoalType.WeightLoss => 0.3f,     // %30
                GoalType.WeightGain => 0.25f,    // %25
                _ => 0.25f                        // %25
            };

            // Yağ hesaplama
            float fatPercentage = goalType switch
            {
                GoalType.FatLoss => 0.25f,       // %25
                GoalType.MuscleGain => 0.25f,    // %25
                _ => 0.3f                         // %30
            };

            // Karbonhidrat hesaplama (kalan)
            float carbPercentage = 1 - proteinPercentage - fatPercentage;

            macros.Protein = (int)(targetCalories * proteinPercentage / 4); // 1g protein = 4 kalori
            macros.Fat = (int)(targetCalories * fatPercentage / 9);         // 1g yağ = 9 kalori
            macros.Carbohydrates = (int)(targetCalories * carbPercentage / 4); // 1g karbonhidrat = 4 kalori
            macros.Fiber = (int)(targetCalories * 0.014f); // Günlük fiber hedefi (kalorinin %1.4'ü)

            return macros;
        }

        public float CalculateBMI(float weight, float height)
        {
            float heightInMeters = height / 100f;
            return weight / (heightInMeters * heightInMeters);
        }

        public float EstimateBodyFatPercentage(float bmi, int age, Gender gender)
        {
            // Basit tahmin formülü
            if (gender == Gender.Male)
            {
                return (1.2f * bmi) + (0.23f * age) - 16.2f;
            }
            else
            {
                return (1.2f * bmi) + (0.23f * age) - 5.4f;
            }
        }

        public float CalculateLeanBodyMass(float weight, float bodyFatPercentage)
        {
            return weight * (1 - bodyFatPercentage / 100f);
        }

        public float CalculateProteinNeeds(float leanBodyMass, GoalType goalType)
        {
            float proteinPerKg = goalType switch
            {
                GoalType.MuscleGain => 2.2f,    // 2.2g/kg
                GoalType.FatLoss => 2.0f,        // 2.0g/kg
                GoalType.WeightLoss => 1.8f,     // 1.8g/kg
                GoalType.WeightGain => 1.6f,     // 1.6g/kg
                _ => 1.6f                         // 1.6g/kg
            };

            return leanBodyMass * proteinPerKg;
        }
    }
} 