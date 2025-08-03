using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.AppUserDTOs
{
    public class CoachSimpleDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? ProfilePhotoUrl { get; set; }
    }
}
