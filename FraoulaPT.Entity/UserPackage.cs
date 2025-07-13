using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class UserPackage : BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Guid PackageId { get; set; }
        public Package Package { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsedQuestions { get; set; }
        public int UsedMessages { get; set; }
        public bool IsActive { get; set; }

        // Opsiyonel alanlar:
        public int RenewalCount { get; set; } // Kaç kez yenilendi
        public string CancelReason { get; set; }
        public string PaymentId { get; set; } // Gerçek ödeme sistemi ile entegreysen
        public DateTime? LastPaymentDate { get; set; }
        public bool IsRenewable { get; set; } // Otomatik yenileme

        // Navigation’lar (diğer ilişkiler)
        public ICollection<UserWeeklyForm> WeeklyForms { get; set; }
        public ICollection<UserQuestion> UserQuestions { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<UserWorkoutAssignment> WorkoutAssignments { get; set; }
    }
}
