using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserPackageDTOs
{
    public class UserPackageUpdateDTO : UserPackageCreateDTO
    {
        public Guid Id { get; set; }
        public int UsedQuestions { get; set; }
        public int UsedMessages { get; set; }
        public int RenewalCount { get; set; }
        public string CancelReason { get; set; }
        public string PaymentId { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public bool IsRenewable { get; set; }
        public int? TotalQuestions { get; set; } // Paket + ek paket toplamı
        public int? TotalMessages { get; set; }
    }

}
