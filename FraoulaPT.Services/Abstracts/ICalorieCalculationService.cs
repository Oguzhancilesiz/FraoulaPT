using FraoulaPT.Core.Enums;

namespace FraoulaPT.Services.Abstracts
{
    public interface ICalorieCalculationService
    {
        /// <summary>
        /// Basal Metabolic Rate (BMR) hesaplar
        /// </summary>
        float CalculateBMR(float weight, float height, int age, Gender gender);

        /// <summary>
        /// Total Daily Energy Expenditure (TDEE) hesaplar
        /// </summary>
        float CalculateTDEE(float bmr, ActivityLevel activityLevel);

        /// <summary>
        /// Hedef kalori hesaplar
        /// </summary>
        int CalculateTargetCalories(float tdee, GoalType goalType, float? currentWeight = null, float? targetWeight = null);

        /// <summary>
        /// Makro besin hedeflerini hesaplar
        /// </summary>
        MacroNutrients CalculateMacroNutrients(int targetCalories, GoalType goalType, float? bodyFatPercentage = null);

        /// <summary>
        /// BMI hesaplar
        /// </summary>
        float CalculateBMI(float weight, float height);

        /// <summary>
        /// Vücut yağ oranını tahmin eder
        /// </summary>
        float EstimateBodyFatPercentage(float bmi, int age, Gender gender);

        /// <summary>
        /// Yağsız vücut kütlesini hesaplar
        /// </summary>
        float CalculateLeanBodyMass(float weight, float bodyFatPercentage);

        /// <summary>
        /// Protein ihtiyacını hesaplar
        /// </summary>
        float CalculateProteinNeeds(float leanBodyMass, GoalType goalType);
    }

    public class MacroNutrients
    {
        public int Protein { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        public int Fiber { get; set; }
    }
} 