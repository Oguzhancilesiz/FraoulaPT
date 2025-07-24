using FraoulaPT.DTOs.ExerciseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ExerciseCategoryDTOs
{
    public class ExerciseCategoryDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Eğer istersen kategoriye ait hareketler:
        public List<ExerciseListDTO> Exercises { get; set; }
    }
}
