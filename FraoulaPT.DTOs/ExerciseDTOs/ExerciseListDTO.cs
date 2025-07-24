using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ExerciseDTOs
{
    public class ExerciseListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Status Status { get; set; }
    }
}