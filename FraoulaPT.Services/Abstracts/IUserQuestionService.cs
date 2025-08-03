using FraoulaPT.DTOs.UserQuestionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserQuestionService
    {
        Task<List<UserQuestionDTO>> GetQuestionsByUserAsync(Guid userId);
        Task<bool> AskQuestionAsync(UserQuestionCreateDTO dto);
        Task<UserQuestionDTO?> GetByIdAsync(Guid id);


        //admin
        Task<List<AllUserQuestionDTO>> GetAllQuestionsAsync();
        Task<UserQuestionAnswerDTO> GetByIdAnswerAsync(Guid id);
        Task<bool> AnswerQuestionAsync(Guid questionId, string answerText, Guid coachId);

        //dashboard için
        Task<int> GetPendingCountAsync();
    }
}
