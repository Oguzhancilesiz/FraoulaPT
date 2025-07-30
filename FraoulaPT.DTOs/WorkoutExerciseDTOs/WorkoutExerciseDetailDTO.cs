using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.MediaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutExerciseDTOs
{
    public class WorkoutExerciseDetailDTO
    {
        public Guid Id { get; set; }
        public string ExerciseName { get; set; }
        public int SetCount { get; set; }
        public int Repetition { get; set; }
        public decimal? Weight { get; set; }
        public int? RestDurationInSeconds { get; set; }
        public string Note { get; set; }
        public Status Status { get; set; }
        public string VideoUrl { get; set; }  // YouTube / Vimeo / mp4 linki
        public string ImageUrls { get; set; }// Görsel linkleri
    }

}
