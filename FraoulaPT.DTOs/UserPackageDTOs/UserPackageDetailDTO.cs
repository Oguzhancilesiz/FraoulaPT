using FraoulaPT.DTOs.ExtraRightDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserPackageDTOs
{
    public class UserPackageDetailDTO
    {
        public Guid Id { get; set; }
        public string PackageName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public int? TotalMessages { get; set; }
        public int? TotalQuestions { get; set; }
        public int? UsedMessages { get; set; }
        public int? UsedQuestions { get; set; }

        public List<ExtraRightDTO> ExtraRights { get; set; } = new();
    }

}
