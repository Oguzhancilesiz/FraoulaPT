using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserPackageDTOs
{
    public class UserPackageCreateDTO
    {
        public Guid AppUserId { get; set; }
        public Guid PackageId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UsedQuestions { get; set; } = 0;
        public int UsedMessages { get; set; } = 0;

        public bool IsActive { get; set; } = true;
        public int RenewalCount { get; set; } = 0;
        public int? TotalQuestions { get; set; } // Paket + ek paket toplamı
        public int? TotalMessages { get; set; }
        public string? CancelReason { get; set; }
        public string? PaymentId { get; set; }
        public DateTime? LastPaymentDate { get; set; }

        public bool IsRenewable { get; set; } = false;
        public Status Status { get; set; } = Status.Active;
    }

}
