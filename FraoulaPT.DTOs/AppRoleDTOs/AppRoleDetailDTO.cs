using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.AppRoleDTOs
{
    public class AppRoleDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AutoID { get; set; }
        public FraoulaPT.Core.Enums.Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
