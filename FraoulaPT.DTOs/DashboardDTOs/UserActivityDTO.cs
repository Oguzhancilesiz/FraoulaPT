using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.DashboardDTOs
{
    public class UserActivityDTO
    {
        public string UserName { get; set; }
        public string ActionDescription { get; set; }
        public string TimeAgo { get; set; }
    }
}
