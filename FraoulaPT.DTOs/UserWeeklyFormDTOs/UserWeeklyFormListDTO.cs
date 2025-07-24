using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserWeeklyFormDTOs
{
    public class UserWeeklyFormListDTO
    {
        public Guid Id { get; set; }
        public Guid UserPackageId { get; set; }
        public DateTime FormDate { get; set; }
        public double? Weight { get; set; }
        public double? FatPercent { get; set; }
        public double? MuscleMass { get; set; }
        public FraoulaPT.Core.Enums.Status Status { get; set; }
    }
}
