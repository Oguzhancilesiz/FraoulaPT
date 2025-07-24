using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserWeeklyFormDTOs
{
    public class UserWeeklyFormUpdateDTO : UserWeeklyFormCreateDTO
    {
        public Guid Id { get; set; }
        public FraoulaPT.Core.Enums.Status Status { get; set; }
    }

}
