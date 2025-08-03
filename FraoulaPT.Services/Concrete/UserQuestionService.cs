using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserQuestionDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public UserQuestionService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<List<UserQuestionDTO>> GetQuestionsByUserAsync(Guid userId)
        {
            var questions = await _unitOfWork.Repository<UserQuestion>()
                .Query()
                .Where(q => q.AskedByUserId == userId && q.Status != Status.Deleted)
                .Include(q => q.AnsweredByCoach)
                .OrderByDescending(q => q.AskedAt)
                .ToListAsync();

            return questions.Select(q => new UserQuestionDTO
            {
                QuestionId = q.Id,
                QuestionText = q.QuestionText,
                AnswerText = q.AnswerText,
                AskedAt = q.AskedAt,
                AnsweredAt = q.AnsweredAt,
                CoachName = q.AnsweredByCoach?.FullName
            }).ToList();
        }

        public async Task<bool> AskQuestionAsync(UserQuestionCreateDTO dto)
        {
            var userPackage = await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Include(x => x.ExtraRights)
                .FirstOrDefaultAsync(x => x.Id == dto.UserPackageId && x.Status == Status.Active && x.IsActive && x.EndDate > DateTime.UtcNow);

            if (userPackage == null)
                return false;

            // Toplam hak = paket + ekstra
            int totalQuestionRight = userPackage.TotalQuestions ?? 0;

            if (userPackage.UsedQuestions >= totalQuestionRight)
                return false; // Soru hakkı dolmuş


            if (userPackage.UsedQuestions >= totalQuestionRight)
                return false; // Hakkı yok, gönderim yapılmasın

            // DTO'dan entity map
            var question = new UserQuestion
            {
                Id = Guid.NewGuid(),
                UserPackageId = dto.UserPackageId,
                AskedByUserId = dto.AskedByUserId,
                AnsweredByCoachId = dto.AnsweredByCoachId,
                QuestionText = dto.QuestionText,
                AnswerText = null,
                AskedAt = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Status = Status.Active
            };

            await _unitOfWork.Repository<UserQuestion>().AddAsync(question);

            // Kullanımı artır
            userPackage.UsedQuestions += 1;
            userPackage.ModifiedDate = DateTime.UtcNow;
            _unitOfWork.Repository<UserPackage>().Update(userPackage);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<UserQuestionDTO?> GetByIdAsync(Guid id)
        {
            var question = await _unitOfWork.Repository<UserQuestion>()
                .Query()
                .Include(q => q.AnsweredByCoach) // Koç bilgisi için
                .FirstOrDefaultAsync(q => q.Id == id && q.Status != Status.Deleted);

            if (question == null)
                return null;

            // DTO map işlemi
            return new UserQuestionDTO
            {
                QuestionId = question.Id,
                UserId = question.AskedByUserId,
                QuestionText = question.QuestionText,
                AskedAt = question.AskedAt,
                AnswerText = question.AnswerText,
                AnsweredAt = question.AnsweredAt,
                CoachName = question.AnsweredByCoach != null ? question.AnsweredByCoach.FullName : null
            };
        }

        //admin
        public async Task<List<AllUserQuestionDTO>> GetAllQuestionsAsync()
        {
            var list = await _unitOfWork.Repository<UserQuestion>()
                .Query()
                .Include(q => q.AskedByUser)
                    .ThenInclude(u => u.Profile)
                .Include(q => q.AnsweredByCoach)
                    .ThenInclude(c => c.Profile)
                .OrderByDescending(q => q.AskedAt)
                .ToListAsync();

            return list.Select(q => new AllUserQuestionDTO
            {
                QuestionId = q.Id,
                QuestionText = q.QuestionText,
                AnswerText = q.AnswerText,
                AskedAt = q.AskedAt,
                AnsweredAt = q.AnsweredAt,

                AskedByUserId = q.AskedByUserId,
                AskedByUserName = q.AskedByUser?.FullName ?? q.AskedByUser?.UserName ?? "Bilinmiyor",
                AskedByUserPhoto = q.AskedByUser?.Profile?.ProfilePhotoUrl,

                AnsweredByCoachId = q.AnsweredByCoachId,
                AnsweredByCoachName = q.AnsweredByCoach?.FullName ?? q.AnsweredByCoach?.UserName ?? "Bilinmiyor",
                AnsweredByCoachPhoto = q.AnsweredByCoach?.Profile?.ProfilePhotoUrl
            }).ToList();
        }


        public async Task<UserQuestionAnswerDTO> GetByIdAnswerAsync(Guid id)
        {
            var q = await _unitOfWork.Repository<UserQuestion>()
                .Query()
                .Include(x => x.AskedByUser).ThenInclude(u => u.Profile)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (q == null) return null;

            return new UserQuestionAnswerDTO
            {
                QuestionId = q.Id,
                QuestionText = q.QuestionText,
                AskedByUserName = q.AskedByUser?.FullName,
                AskedByUserPhoto = q.AskedByUser?.Profile?.ProfilePhotoUrl ?? "/uploads/user-default.jpg",
                AskedAt = q.AskedAt,
                AnswerText = q.AnswerText
            };
        }

        public async Task<bool> AnswerQuestionAsync(Guid questionId, string answerText, Guid coachId)
        {
            var question = await _unitOfWork.Repository<UserQuestion>()
                .GetById(questionId);

            if (question == null) return false;

            question.AnswerText = answerText;
            question.AnsweredByCoachId = coachId;
            question.AnsweredAt = DateTime.UtcNow;
            // question.Status = QuestionStatus.Answered; // Enum varsa ekle

            _unitOfWork.Repository<UserQuestion>().Update(question);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }


        //dashboard için
        public async Task<int> GetPendingCountAsync()
        {
            return await _unitOfWork.Repository<UserQuestion>()
                .Query()
                .CountAsync(q => string.IsNullOrEmpty(q.AnswerText));
        }

    }

}
