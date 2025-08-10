using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.StudentReportDTOs
{
    public class StudentReportDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public DateTime? RegisteredAt { get; set; }

        public PackageBlock Package { get; set; } = new();
        public WeeklyFormBlock WeeklyForm { get; set; } = new();
        public List<MetricPoint> WeightSeries { get; set; } = new();
        public List<MetricPoint> WaistSeries { get; set; } = new();
        public List<MetricPoint> HipSeries { get; set; } = new();
        public WorkoutBlock Workout { get; set; } = new();
        public CommunicationBlock Comms { get; set; } = new();
        public string? CoachNotes { get; set; }

        public class PackageBlock
        {
            public string? Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }

            // NOT: Toplam hak değerleri UserPackage.Total* alanlarından gelir.
            public int TotalQuestions { get; set; }
            public int UsedQuestions { get; set; }
            public int AvailableQuestions => Math.Max(0, TotalQuestions - UsedQuestions);

            public int TotalMessages { get; set; }
            public int UsedMessages { get; set; }
            public int AvailableMessages => Math.Max(0, TotalMessages - UsedMessages);

            // Sadece bilgi amaçlı (toplam hesaba katmadan)
            public int ActiveExtraQuestionRights { get; set; }
            public int ActiveExtraMessageRights { get; set; }
        }

        public class WeeklyFormBlock
        {
            public DateTime? LastFormDate { get; set; }
            public double? Weight { get; set; }
            public double? Waist { get; set; }
            public double? Hip { get; set; }
            public List<string> ProgressPhotoUrls { get; set; } = new();
        }

        public class WorkoutBlock
        {
            public string? ProgramName { get; set; }
            public int PlannedDays { get; set; }
            public int WithFeedbackDays { get; set; }
            public double? ComplianceRate { get; set; } // 0..1
        }

        public class CommunicationBlock
        {
            public int Last30dMessageCount { get; set; }
            public int Last30dQuestionCount { get; set; }
            public DateTime? LastMessageAt { get; set; }
            public TimeSpan? AvgCoachReplyTime { get; set; }
        }

        public record MetricPoint(DateTime Date, double? Value);
    }
}
