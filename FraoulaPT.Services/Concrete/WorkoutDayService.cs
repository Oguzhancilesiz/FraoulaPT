using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.WorkoutDayDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class WorkoutDayService : BaseService<
    WorkoutDay,
    WorkoutDayListDTO,
    WorkoutDayDetailDTO,
    WorkoutDayCreateDTO,
    WorkoutDayUpdateDTO>, IWorkoutDayService
    {
        public WorkoutDayService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
