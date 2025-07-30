using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class ExtraRight : BaseEntity
    {
        public Guid UserPackageId { get; set; }
        public UserPackage UserPackage { get; set; }

        public ExtraRightType RightType { get; set; } // Enum: Question, Message
        public int Amount { get; set; } // Eklenen hak sayısı
        public DateTime PurchaseDate { get; set; }
        public string PaymentId { get; set; }
        public string Note { get; set; } // "Ramazan kampanyasından alındı" gibi
    }
}
