using FraoulaPT.Core.Enums;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class CalorieController : Controller
    {
        private readonly ICalorieCalculationService _calorieService;
        private readonly IUserProfileService _profileService;

        public CalorieController(ICalorieCalculationService calorieService, IUserProfileService profileService)
        {
            _calorieService = calorieService;
            _profileService = profileService;
        }

        public IActionResult Calculator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate([FromBody] CalorieCalculationRequest request)
        {
            try
            {
                var age = CalculateAge(request.BirthDate);
                var bmr = _calorieService.CalculateBMR(request.Weight, request.Height, age, request.Gender);
                var tdee = _calorieService.CalculateTDEE(bmr, request.ActivityLevel);
                var targetCalories = _calorieService.CalculateTargetCalories(tdee, request.GoalType, request.Weight, request.TargetWeight);
                var macros = _calorieService.CalculateMacroNutrients(targetCalories, request.GoalType, request.BodyFatPercentage);
                var bmi = _calorieService.CalculateBMI(request.Weight, request.Height);
                var estimatedBodyFat = _calorieService.EstimateBodyFatPercentage(bmi, age, request.Gender);
                var leanBodyMass = _calorieService.CalculateLeanBodyMass(request.Weight, estimatedBodyFat);
                var proteinNeeds = _calorieService.CalculateProteinNeeds(leanBodyMass, request.GoalType);

                var result = new CalorieCalculationResult
                {
                    BMR = (int)bmr,
                    TDEE = (int)tdee,
                    TargetCalories = targetCalories,
                    Macros = macros,
                    BMI = bmi,
                    EstimatedBodyFat = estimatedBodyFat,
                    LeanBodyMass = leanBodyMass,
                    ProteinNeeds = proteinNeeds,
                    Age = age
                };

                return Json(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfile([FromBody] CalorieCalculationRequest request)
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Json(new { success = false, message = "Kullanıcı bulunamadı" });

                var profile = await _profileService.GetByAppUserIdAsync(Guid.Parse(userId));
                if (profile == null)
                    return Json(new { success = false, message = "Profil bulunamadı" });

                // Profil bilgilerini güncelle
                profile.WeightKg = request.Weight;
                profile.HeightCm = request.Height;
                profile.BirthDate = request.BirthDate;
                profile.Gender = request.Gender;
                profile.ActivityLevel = request.ActivityLevel;
                profile.GoalType = request.GoalType;
                profile.TargetWeight = request.TargetWeight;
                profile.BodyFatPercentage = request.BodyFatPercentage;

                // Kalori hesaplamaları
                var age = CalculateAge(request.BirthDate);
                var bmr = _calorieService.CalculateBMR(request.Weight, request.Height, age, request.Gender);
                var tdee = _calorieService.CalculateTDEE(bmr, request.ActivityLevel);
                var targetCalories = _calorieService.CalculateTargetCalories(tdee, request.GoalType, request.Weight, request.TargetWeight);
                var macros = _calorieService.CalculateMacroNutrients(targetCalories, request.GoalType, request.BodyFatPercentage);

                profile.BMR = bmr;
                profile.TDEE = tdee;
                profile.DailyCalorieGoal = targetCalories;
                profile.DailyProteinGoal = macros.Protein;
                profile.DailyCarbGoal = macros.Carbohydrates;
                profile.DailyFatGoal = macros.Fat;

                await _profileService.UpdateAsync(profile);

                return Json(new { success = true, message = "Profil başarıyla güncellendi" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public class CalorieCalculationRequest
    {
        public float Weight { get; set; }
        public float Height { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
        public GoalType GoalType { get; set; }
        public float? TargetWeight { get; set; }
        public float? BodyFatPercentage { get; set; }
    }

    public class CalorieCalculationResult
    {
        public int BMR { get; set; }
        public int TDEE { get; set; }
        public int TargetCalories { get; set; }
        public MacroNutrients Macros { get; set; } = new();
        public float BMI { get; set; }
        public float EstimatedBodyFat { get; set; }
        public float LeanBodyMass { get; set; }
        public float ProteinNeeds { get; set; }
        public int Age { get; set; }
    }
} 