using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.StudentDashboardDTOs;
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
    public class StudentDashboardService : IStudentDashboardService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserPackageService _userPackageService;

        public StudentDashboardService(IUnitOfWork uow, IUserPackageService userPackageService)
        {
            _uow = uow;
            _userPackageService = userPackageService;
        }

        public async Task<StudentDashboardDTO> BuildAsync(Guid userId)
        {
            var now = DateTime.UtcNow;

            // Kullanıcı + profil
            var user = await _uow.Repository<AppUser>()
                .Query().AsNoTracking()
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var profile = user.Profile;

            // Aktif paket (tarih filtresiyle) + kalan gün
            var activePackage = await _userPackageService.GetCurrentActiveAsync(userId, now);
            var remainingDays = await _userPackageService.GetRemainingDaysAsync(userId);

            // Haftalık form serisi (tarih artan)
            var forms = await _uow.Repository<UserWeeklyForm>()
                .Query().AsNoTracking()
                .Where(f => f.AppUserId == userId && f.Status != Status.Deleted)
                .OrderBy(f => f.FormDate)
                .Select(f => new { f.Id, f.FormDate, f.Weight, f.Waist, f.Hip })
                .ToListAsync();
            var last = forms.LastOrDefault();
            var prev = (forms.Count >= 2) ? forms[^2] : null;

            // İletişim (son 30 gün)
            var since = now.AddDays(-30);
            var messages30 = await _uow.Repository<ChatMessage>()
                .Query().AsNoTracking()
                .Where(m => (m.SenderId == userId || m.ReceiverId == userId) && m.SentAt >= since)
                .Select(m => new { m.SentAt })
                .ToListAsync();

            int questions30 = 0; // projende UserQuestion varsa buraya entegre edilir
            try
            {
                questions30 = await _uow.Repository<UserQuestion>()
                    .Query().AsNoTracking()
                    .Where(q => q.AskedByUserId == userId && q.Status != Status.Deleted && q.CreatedDate >= since)
                    .CountAsync();
            }
            catch { /* entity yoksa sessiz geç */ }

            // DTO
            var dto = new StudentDashboardDTO
            {
                UserId = user.Id,
                FullName = user.FullName ?? user.UserName,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePhotoUrl = profile?.ProfilePhotoUrl,
                HasCompletedProfile = profile != null,

                Package = new StudentDashboardDTO.PackageBlock
                {
                    Name = activePackage?.Package?.Name,
                    Start = activePackage?.StartDate,
                    End = activePackage?.EndDate,
                    RemainingDays = remainingDays,

                    TotalQuestions = activePackage?.TotalQuestions ?? 0,
                    UsedQuestions = activePackage?.UsedQuestions ?? 0,
                    TotalMessages = activePackage?.TotalMessages ?? 0,
                    UsedMessages = activePackage?.UsedMessages ?? 0,

                    ActiveExtraQuestionRights = activePackage?.ExtraRights?
                        .Where(x => x.RightType == ExtraRightType.Question && x.Status == Status.Active)
                        .Sum(x => x.Amount) ?? 0,
                    ActiveExtraMessageRights = activePackage?.ExtraRights?
                        .Where(x => x.RightType == ExtraRightType.Message && x.Status == Status.Active)
                        .Sum(x => x.Amount) ?? 0,
                },

                WeeklyForm = new StudentDashboardDTO.WeeklyFormBlock
                {
                    LastFormDate = last?.FormDate,
                    Weight = last?.Weight,
                    Waist = last?.Waist,
                    Hip = last?.Hip,
                    DeltaWeight = (last?.Weight != null && prev?.Weight != null)
                        ? Math.Round((double)(last.Weight - prev.Weight), 1)
                        : null,
                    ProgressPhotoUrls = new List<string>() // TODO: form foto URL alanlarını bağla
                },

                WeightSeries = forms.Select(f => new StudentDashboardDTO.MetricPoint(f.FormDate, f.Weight)).ToList(),
                WaistSeries = forms.Select(f => new StudentDashboardDTO.MetricPoint(f.FormDate, f.Waist)).ToList(),
                HipSeries = forms.Select(f => new StudentDashboardDTO.MetricPoint(f.FormDate, f.Hip)).ToList(),

                Comms = new StudentDashboardDTO.CommunicationBlock
                {
                    Last30dMessageCount = messages30.Count,
                    Last30dQuestionCount = questions30,
                    LastMessageAt = messages30.OrderBy(x => x.SentAt).LastOrDefault()?.SentAt
                },

                ShowBuyPackageCta = activePackage == null,
                ShowBuyExtraCta = activePackage != null,
                ShowProfileReminder = profile == null
            };

            return dto;
        }
    }
}
