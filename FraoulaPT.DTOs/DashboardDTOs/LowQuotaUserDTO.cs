using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.DashboardDTOs
{
    public class LowQuotaUserDTO
    {
        public string UserName { get; set; }
        public int RemainingMessages { get; set; }
    }
}
