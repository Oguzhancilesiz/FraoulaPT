using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class ExtraPackageOption : BaseEntity
    {
        public string Name { get; set; } // Örn: "Ekstra 100 Soru Hakkı"
        public ExtraUsageType Type { get; set; } // Enum: Question, Message, etc.
        public int Amount { get; set; } // Kaç adet hak tanıyor (örn: 10)
        public decimal Price { get; set; }
        public string Description { get; set; } // Opsiyonel açıklama
        public bool IsActive { get; set; } = true;

        public ICollection<ExtraPackageUsage> Usages { get; set; }
    }
}
