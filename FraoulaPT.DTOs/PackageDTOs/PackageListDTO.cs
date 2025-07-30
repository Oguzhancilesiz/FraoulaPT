using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.PackageDTOs
{
    public class PackageListDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public PackageType PackageType { get; set; }
        public SubscriptionPeriod SubscriptionPeriod { get; set; }
        public int MaxQuestionsPerPeriod { get; set; }  // ✅ Eklendi
        public int MaxMessagesPerPeriod { get; set; }   // ✅ Eklendi
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public Status Status { get; set; }
        // ✨ Eksik olanlar:
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string HighlightColor { get; set; } // Eğer kullanacaksan
    }

}
