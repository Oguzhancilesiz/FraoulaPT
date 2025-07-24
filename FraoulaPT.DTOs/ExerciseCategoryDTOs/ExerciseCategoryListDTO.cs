using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ExerciseCategoryDTOs
{
    public class ExerciseCategoryListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Hareket sayısı göstermek icin
        public int ExerciseCount { get; set; }
    }
}
