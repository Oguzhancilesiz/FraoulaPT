using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProfileDTOs
{
    public class UserProfileListDTO
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string? Instagram { get; set; }
        public FraoulaPT.Core.Enums.Gender? Gender { get; set; }
        public double? HeightCm { get; set; }
        public double? WeightKg { get; set; }
    }

}
