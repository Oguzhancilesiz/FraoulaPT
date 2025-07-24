using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutDayDTOs
{
    public class WorkoutDayUpdateDTO : WorkoutDayCreateDTO
    {
        public Guid Id { get; set; }
    }

}
