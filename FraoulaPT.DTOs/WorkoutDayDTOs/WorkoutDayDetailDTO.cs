using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutDayDTOs
{
    public class WorkoutDayDetailDTO
    {
        public Guid Id { get; set; }                  
        public int DayOfWeek { get; set; }             
        public string Description { get; set; }
        public List<WorkoutExerciseDetailDTO> Exercises { get; set; }
    }
}
