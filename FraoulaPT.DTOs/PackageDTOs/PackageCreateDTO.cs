using FraoulaPT.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.PackageDTOs
{
    public class PackageCreateDTO
    {
        public string Name { get; set; }
        public PackageType PackageType { get; set; }
        public SubscriptionPeriod SubscriptionPeriod { get; set; }
        public int MaxQuestionsPerPeriod { get; set; }
        public int MaxMessagesPerPeriod { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Features { get; set; }
        public string? ImageUrl { get; set; }
        public int Order { get; set; }
        public string HighlightColor { get; set; }
        public IFormFile? ImageFile { get; set; } // Yeni eklenen alan

    }

}
