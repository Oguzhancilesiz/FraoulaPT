using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.NortificationDTOs;
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
    public class NotificationService(IUnitOfWork uow) : INotificationService
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task<NotificationSummaryDTO> GetCoachSummaryAsync(Guid coachId, int take = 5)
        {
            var questions = _uow.Repository<UserQuestion>().Query()
                .Where(x => x.Status != Status.Deleted
                            && x.AnswerText == null
                            && x.AnsweredAt == null
                            && x.UserPackage != null
                            //&& x.UserPackage.ıd == coachId
                            ); // <- öğrencinin paketi bu koça ait

            var chats = _uow.Repository<ChatMessage>().Query()
                .Where(x => x.Status != Status.Deleted
                            && x.ReceiverId == coachId
                            && !x.IsRead);

            var dto = new NotificationSummaryDTO
            {
                UnansweredQuestionCount = await questions.CountAsync(),
                PendingChatCount = await chats.CountAsync(),
                Questions = await questions
                    .Include(x => x.AskedByUser)
                    .OrderByDescending(x => x.AskedAt).Take(take)
                    .Select(x => new QuestionNotifItemDTO
                    {
                        Id = x.Id,
                        FromUserName = x.AskedByUser != null
                            ? (x.AskedByUser.FullName ?? x.AskedByUser.UserName)
                            : "—",
                        Preview = BuildPreview(x.QuestionText),
                        AskedAt = x.AskedAt,
                        Link = $"/Admin/CoachQuestion/Detail/{x.Id}"
                    }).ToListAsync(),
                Chats = await chats
                    .Include(x => x.Sender)
                    .OrderByDescending(x => x.SentAt).Take(take)
                    .Select(x => new ChatNotifItemDTO
                    {
                        Id = x.Id,
                        FromUserName = x.Sender != null
                            ? (x.Sender.FullName ?? x.Sender.UserName)
                            : "—",
                        Preview = BuildPreview(x.MessageText),
                        SentAt = x.SentAt,
                        Link = $"/Admin/CoachChat/Index?openMessageId={x.Id}"
                    }).ToListAsync(),
                ServerTime = DateTime.UtcNow
            };
            return dto;
        }

        public async Task<NotificationSummaryDTO> GetAdminSummaryAsync(int take = 5)
        {
            var questions = _uow.Repository<UserQuestion>().Query()
                .Where(x => x.Status != Status.Deleted && x.AnswerText == null && x.AnsweredAt == null);

            var chats = _uow.Repository<ChatMessage>().Query()
                .Where(x => x.Status != Status.Deleted && !x.IsRead);

            return new NotificationSummaryDTO
            {
                UnansweredQuestionCount = await questions.CountAsync(),
                PendingChatCount = await chats.CountAsync(),
                Questions = await questions.Include(x => x.AskedByUser)
                    .OrderByDescending(x => x.AskedAt).Take(take)
                    .Select(x => new QuestionNotifItemDTO
                    {
                        Id = x.Id,
                        FromUserName = x.AskedByUser != null
                            ? (x.AskedByUser.FullName ?? x.AskedByUser.UserName)
                            : "—",
                        Preview = BuildPreview(x.QuestionText),
                        AskedAt = x.AskedAt,
                        Link = $"/Admin/CoachQuestion/Detail/{x.Id}"
                    }).ToListAsync(),
                Chats = await chats.Include(x => x.Sender)
                    .OrderByDescending(x => x.SentAt).Take(take)
                    .Select(x => new ChatNotifItemDTO
                    {
                        Id = x.Id,
                        FromUserName = x.Sender != null
                            ? (x.Sender.FullName ?? x.Sender.UserName)
                            : "—",
                        Preview = BuildPreview(x.MessageText),
                        SentAt = x.SentAt,
                        Link = $"/Admin/CoachChat/Index?openMessageId={x.Id}"
                    }).ToListAsync(),
                ServerTime = DateTime.UtcNow
            };
        }

        private static string BuildPreview(string s)
            => string.IsNullOrWhiteSpace(s) ? "—" : (s.Length <= 80 ? s : s[..80] + "...");
    }

}
