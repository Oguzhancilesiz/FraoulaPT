using FraoulaPT.DTOs.UserQuestionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserQuestionService
    {
        Task<bool> AskQuestionAsync(Guid userId, string questionText);
        Task<List<UserQuestionListDTO>> GetMyQuestionsAsync(Guid userId);
    }
}
