using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.PackageDTOs
{
    public class PackageDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FraoulaPT.Core.Enums.PackageType PackageType { get; set; }
        public FraoulaPT.Core.Enums.SubscriptionPeriod SubscriptionPeriod { get; set; }
        public int MaxQuestionsPerPeriod { get; set; }
        public int MaxMessagesPerPeriod { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Features { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public string HighlightColor { get; set; }
    }

}
