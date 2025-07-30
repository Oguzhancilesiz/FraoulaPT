using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ExtraRightDTOs
{
    public class ExtraRightCreateDTO
    {
        public Guid UserPackageId { get; set; }
        public ExtraRightType RightType { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
    }
}
