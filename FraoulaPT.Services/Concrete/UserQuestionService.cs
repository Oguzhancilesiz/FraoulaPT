using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.UserQuestionDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserQuestionService : BaseService<
    UserQuestion,
    UserQuestionListDTO,
    UserQuestionDetailDTO,
    UserQuestionCreateDTO,
    UserQuestionUpdateDTO>, IUserQuestionService
    {
        public UserQuestionService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
