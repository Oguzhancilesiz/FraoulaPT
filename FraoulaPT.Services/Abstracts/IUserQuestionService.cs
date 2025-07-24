using FraoulaPT.DTOs.UserQuestionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserQuestionService : IBaseService<
      UserQuestionListDTO,
      UserQuestionDetailDTO,
      UserQuestionCreateDTO,
      UserQuestionUpdateDTO>
    { }

}
