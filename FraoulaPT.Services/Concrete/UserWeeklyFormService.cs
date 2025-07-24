using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserWeeklyFormService : BaseService<
    UserWeeklyForm,
    UserWeeklyFormListDTO,
    UserWeeklyFormDetailDTO,
    UserWeeklyFormCreateDTO,
    UserWeeklyFormUpdateDTO>, IUserWeeklyFormService
    {
        public UserWeeklyFormService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
