using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserPackageDTOs
{
    public class UserPackageStatusDTO
    {
        public Guid UserPackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsExpired => EndDate <= DateTime.UtcNow;

        public int UsedMessages { get; set; }
        public int UsedQuestions { get; set; }

        public int TotalMessages { get; set; }
        public int TotalQuestions { get; set; }

        public int ExtraMessageRights { get; set; }
        public int ExtraQuestionRights { get; set; }

        public int RemainingMessages => (TotalMessages + ExtraMessageRights) - UsedMessages;
        public int RemainingQuestions => (TotalQuestions + ExtraQuestionRights) - UsedQuestions;

        public bool CanSendMessage => !IsExpired && RemainingMessages > 0;
        public bool CanAskQuestion => !IsExpired && RemainingQuestions > 0;
    }
}
