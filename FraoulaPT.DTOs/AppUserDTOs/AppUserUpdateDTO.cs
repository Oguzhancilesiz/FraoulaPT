using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.AppUserDTOs
{
    public class AppUserUpdateDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public FraoulaPT.Core.Enums.Status Status { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
