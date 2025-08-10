using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.StudentReportDTOs;
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
    public class StudentReportService : IStudentReportService
    {
        private readonly IUnitOfWork _uow;
        public StudentReportService(IUnitOfWork uow) => _uow = uow;

        public async Task<StudentReportDTO> BuildAsync(Guid userId)
        {
            // Kullanıcı + profil
            var user = await _uow.Repository<AppUser>()
                .Query().AsNoTracking()
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Aktif paket (en günceli)
            var activePackage = await _uow.Repository<UserPackage>()
                .Query().AsNoTracking()
                .Include(p => p.Package)
                .Include(p => p.ExtraRights)
                .Where(p => p.AppUserId == userId && p.IsActive)
                .OrderByDescending(p => p.StartDate)
                .FirstOrDefaultAsync();

            // Haftalık formlar (seriler için tarih sıralı)
            var forms = await _uow.Repository<UserWeeklyForm>()
                .Query().AsNoTracking()
                .Where(f => f.AppUserId == userId && f.Status != Status.Deleted)
                .OrderBy(f => f.FormDate)
                .Select(f => new
                {
                    f.Id,
                    f.FormDate,
                    f.Weight,   // double?
                    f.Waist,    // double?
                    f.Hip,      // double?
                    ProgressPhotos = (ICollection<Media>?)null // TODO: Gerçek ilişki adını bağla
                })
                .ToListAsync();
            var lastForm = forms.LastOrDefault();

            // İletişim — mesajlar (son 30 gün)
            var since = DateTime.UtcNow.AddDays(-30);
            var messages30 = await _uow.Repository<ChatMessage>()
                .Query().AsNoTracking()
                .Where(m => (m.SenderId == userId || m.ReceiverId == userId) && m.SentAt >= since)
                .Select(m => new { m.SentAt })
                .ToListAsync();

            // Program adı (WorkoutDays navigasyonunu kullanmadan)
            string? programName = null;
            try
            {
                if (lastForm != null)
                {
                    var program = await _uow.Repository<WorkoutProgram>()
                        .Query().AsNoTracking()
                        .Where(p => p.UserWeeklyFormId == lastForm.Id && p.Status != Status.Deleted)
                        .FirstOrDefaultAsync();
                    programName = program?.AppUser.FullName;
                }
            }
            catch { /* entity isimleri farklıysa şimdilik boş geç */ }

            // DTO
            var dto = new StudentReportDTO
            {
                UserId = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePhotoUrl = user.Profile?.ProfilePhotoUrl,
                RegisteredAt = user.CreatedDate,

                Package = new StudentReportDTO.PackageBlock
                {
                    Name = activePackage?.Package?.Name,
                    Start = activePackage?.StartDate,
                    End = activePackage?.EndDate,

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

                WeeklyForm = new StudentReportDTO.WeeklyFormBlock
                {
                    LastFormDate = lastForm?.FormDate,
                    Weight = lastForm?.Weight,
                    Waist = lastForm?.Waist,
                    Hip = lastForm?.Hip,
                    ProgressPhotoUrls = new List<string>() // TODO: URL alanını bağla (örn: p.Url / p.Path / p.ImageUrl)
                },

                WeightSeries = forms.Select(f => new StudentReportDTO.MetricPoint(f.FormDate, f.Weight)).ToList(),
                WaistSeries = forms.Select(f => new StudentReportDTO.MetricPoint(f.FormDate, f.Waist)).ToList(),
                HipSeries = forms.Select(f => new StudentReportDTO.MetricPoint(f.FormDate, f.Hip)).ToList(),

                Workout = new StudentReportDTO.WorkoutBlock
                {
                    ProgramName = programName,
                    PlannedDays = 0,
                    WithFeedbackDays = 0,
                    ComplianceRate = null
                },

                Comms = new StudentReportDTO.CommunicationBlock
                {
                    Last30dMessageCount = messages30.Count,
                    Last30dQuestionCount = 0, // TODO: UserQuestion servisinden çekilecek
                    LastMessageAt = messages30.OrderBy(x => x.SentAt).LastOrDefault()?.SentAt
                }
            };

            return dto;
        }
    }
}
