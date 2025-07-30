using FraoulaPT.DTOs.WorkoutDayDTOs;
using FraoulaPT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutFeedbackDTOs
{
    public class WorkoutDayWithFeedbackVM
    {
        public WorkoutDay WorkoutDay { get; set; }
        public List<WorkoutFeedback> TodayFeedbacks { get; set; }
    }
}
