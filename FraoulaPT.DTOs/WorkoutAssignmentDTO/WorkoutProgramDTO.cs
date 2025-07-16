using FraoulaPT.DTOs.MediaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutAssignmentDTO
{
    public class WorkoutProgramDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<WorkoutDayDTO> WorkoutDays { get; set; }
        public List<MediaDTO> Media { get; set; }
    }
    public class WorkoutDayDTO
    {
        public int DayOfWeek { get; set; }
        public List<WorkoutExerciseDTO> Exercises { get; set; }
    }
    public class WorkoutExerciseDTO
    {
        public string ExerciseName { get; set; }
        public int SetCount { get; set; }
        public int RepCount { get; set; }
        public int? DurationSeconds { get; set; }
        public string Notes { get; set; }
        public MediaDTO ExerciseMedia { get; set; }
    }
}
