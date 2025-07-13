using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserPackageDTO
{
    public class UserPackageCreateDTO
    {
        public Guid AppUserId { get; set; }
        public Guid PackageId { get; set; }
        // Ekstra: ödeme detayları, vs. eklenecekse ekleyebilirsin
    }
}
