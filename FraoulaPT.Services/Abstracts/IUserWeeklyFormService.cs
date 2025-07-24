using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserWeeklyFormService : IBaseService<
    UserWeeklyFormListDTO,
    UserWeeklyFormDetailDTO,
    UserWeeklyFormCreateDTO,
    UserWeeklyFormUpdateDTO>
    { }
}
