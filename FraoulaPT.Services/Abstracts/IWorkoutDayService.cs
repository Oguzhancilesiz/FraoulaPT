using FraoulaPT.DTOs.WorkoutDayDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IWorkoutDayService : IBaseService<
    WorkoutDayListDTO,
    WorkoutDayDetailDTO,
    WorkoutDayCreateDTO,
    WorkoutDayUpdateDTO>
    { }

}
