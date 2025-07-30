using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class ExtraPackageUsage : BaseEntity
    {
        public Guid ExtraPackageOptionId { get; set; }
        public ExtraPackageOption ExtraPackageOption { get; set; }

        public Guid UserPackageId { get; set; }
        public UserPackage UserPackage { get; set; }

        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }
}
