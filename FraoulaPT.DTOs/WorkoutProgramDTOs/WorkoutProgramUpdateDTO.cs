using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.WorkoutProgramDTOs
{
    public class WorkoutProgramUpdateDTO : WorkoutProgramCreateDTO
    {
        public Guid Id { get; set; }
    }

}
