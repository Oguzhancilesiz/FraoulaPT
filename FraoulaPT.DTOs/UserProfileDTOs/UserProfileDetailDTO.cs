using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserProfileDTOs
{
    public class UserProfileDetailDTO : UserProfileUpdateDTO
    {
        public DateTime CreatedDate { get; set; }

        //  AppUser'dan gelen kimlik alanları
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }

}
