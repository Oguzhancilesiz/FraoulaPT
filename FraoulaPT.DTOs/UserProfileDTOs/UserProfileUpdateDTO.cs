using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProfileDTOs
{
    public class UserProfileUpdateDTO : UserProfileCreateDTO
    {
        public Guid Id { get; set; }
    }
}
