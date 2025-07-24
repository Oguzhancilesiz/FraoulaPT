using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.AppRoleDTOs
{
    public class AppRoleListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FraoulaPT.Core.Enums.Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
