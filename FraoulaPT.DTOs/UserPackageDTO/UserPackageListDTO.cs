using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserPackageDTO
{
    public class UserPackageListDTO
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid PackageId { get; set; }
        public string PackageName { get; set; }
        public PackageType PackageType { get; set; }
        public string PackageDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsedQuestions { get; set; }
        public int UsedMessages { get; set; }
        public int MaxQuestionsPerPeriod { get; set; }
        public int MaxMessagesPerPeriod { get; set; }
        public bool IsActive { get; set; }
        // İsteğe bağlı:
        public string HighlightColor { get; set; }
        public decimal Price { get; set; }
    }
}
