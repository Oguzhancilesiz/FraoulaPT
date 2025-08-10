using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.StudentDashboardDTOs
{
    public class StudentDashboardDTO
    {
        // Kullanıcı
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ProfilePhotoUrl { get; set; }

        // Profil durumu
        public bool HasCompletedProfile { get; set; }

        // Paket bilgisi
        public PackageBlock Package { get; set; } = new();

        // Haftalık Form / Ölçüm
        public WeeklyFormBlock WeeklyForm { get; set; } = new();
        public List<MetricPoint> WeightSeries { get; set; } = new();
        public List<MetricPoint> WaistSeries { get; set; } = new();
        public List<MetricPoint> HipSeries { get; set; } = new();

        // İletişim özeti
        public CommunicationBlock Comms { get; set; } = new();

        // CTA/uyarılar
        public bool ShowBuyPackageCta { get; set; }
        public bool ShowBuyExtraCta { get; set; }
        public bool ShowProfileReminder { get; set; }

        public class PackageBlock
        {
            public string? Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public int? RemainingDays { get; set; }

            public int TotalQuestions { get; set; }
            public int UsedQuestions { get; set; }
            public int AvailableQuestions => Math.Max(0, TotalQuestions - UsedQuestions);

            public int TotalMessages { get; set; }
            public int UsedMessages { get; set; }
            public int AvailableMessages => Math.Max(0, TotalMessages - UsedMessages);

            public int ActiveExtraQuestionRights { get; set; }
            public int ActiveExtraMessageRights { get; set; }
        }

        public class WeeklyFormBlock
        {
            public DateTime? LastFormDate { get; set; }
            public double? Weight { get; set; }
            public double? Waist { get; set; }
            public double? Hip { get; set; }
            public double? DeltaWeight { get; set; } // önceki forma göre değişim
            public List<string> ProgressPhotoUrls { get; set; } = new();
        }

        public class CommunicationBlock
        {
            public int Last30dMessageCount { get; set; }
            public int Last30dQuestionCount { get; set; }
            public DateTime? LastMessageAt { get; set; }
        }

        public record MetricPoint(DateTime Date, double? Value);
    }
}
