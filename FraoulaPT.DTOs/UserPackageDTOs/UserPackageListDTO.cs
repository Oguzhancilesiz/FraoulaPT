using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserPackageDTOs
{
    public class UserPackageListDTO
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid PackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

}
