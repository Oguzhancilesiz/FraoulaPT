using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class Package : BaseEntity
    {
        public string Name { get; set; } // Gold, Pro, Platinum, vb.
        public PackageType PackageType { get; set; } // Enum: Gold, Pro, ...
        public SubscriptionPeriod SubscriptionPeriod { get; set; } // Enum: Monthly, Yearly
        public int MaxQuestionsPerPeriod { get; set; }
        public int MaxMessagesPerPeriod { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        // Opsiyonel
        public string Features { get; set; } // JSON/text, ek haklar (isteğe bağlı)
        public string ImageUrl { get; set; } // Paket görseli (isteğe bağlı)
        public int Order { get; set; } // Sıralama için
        public string HighlightColor { get; set; } // Vitrin rengi vs.

        public ICollection<UserPackage> UserPackages { get; set; }
    }
}
