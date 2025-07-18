using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProgramDTO
{
    public class WorkoutExerciseDTO
    {
        public Guid Id { get; set; }
        public Guid WorkoutDayId { get; set; }
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; }  // Kullanıcı ekranı için kolaylık
        public int SetCount { get; set; }
        public int RepetitionCount { get; set; }
        public double? Weight { get; set; }
        public string CoachNote { get; set; }
        public string ImageUrl { get; set; }    // <- Hareket görseli
        public string VideoUrl { get; set; }    // <- Video URL
        public WorkoutExerciseLogDTO LastLog { get; set; }
    }

}
