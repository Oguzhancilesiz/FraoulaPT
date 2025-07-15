using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserQuestionDTO;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserQuestionService : IUserQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserQuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AskQuestionAsync(Guid userId, string questionText)
        {
            // Aktif paket var mı ve hakkı var mı?
            var packageQuery = await _unitOfWork.Repository<UserPackage>()
                .GetBy(x => x.AppUserId == userId && x.IsActive && x.EndDate > DateTime.Now);

            packageQuery = packageQuery.Include(x => x.Package)
                                       .OrderByDescending(x => x.StartDate);

            var activePackage = await packageQuery.FirstOrDefaultAsync();
            if (activePackage == null || activePackage.UsedQuestions >= (activePackage.Package?.MaxQuestionsPerPeriod ?? 0))
                return false;

            var question = new UserQuestion
            {
                UserPackageId = activePackage.Id,
                AskedByUserId = userId,
                QuestionText = questionText,
                AskedAt = DateTime.Now,
                Status = Core.Enums.Status.Active,          // Senin altyapında zorunlu
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                AnswerText = "Henüz Cevaplanmadı..."
            };

            await _unitOfWork.Repository<UserQuestion>().AddAsync(question);

            // Soru hakkını artır
            activePackage.UsedQuestions++;
            await _unitOfWork.Repository<UserPackage>().Update(activePackage);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserQuestionListDTO>> GetMyQuestionsAsync(Guid userId)
        {
            var query = await _unitOfWork.Repository<UserQuestion>()
                .GetBy(x => x.AskedByUserId == userId);

            query = query.Include(x => x.AnsweredByCoach)
                         .OrderByDescending(x => x.AskedAt);

            var list = await query.ToListAsync();

            return list.Select(x => new UserQuestionListDTO
            {
                Id = x.Id,
                QuestionText = x.QuestionText,
                AnswerText = x.AnswerText,
                AskedAt = x.AskedAt,
                AnsweredAt = x.AnsweredAt,
                CoachName = x.AnsweredByCoach != null ? x.AnsweredByCoach.FullName : "",
                // IsAnswered property’sini DTO'da sadece get ile otomatik yapabilirsin
            }).ToList();
        }
    }
}
