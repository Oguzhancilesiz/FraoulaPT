using FraoulaPT.DTOs.NortificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface INotificationService
    {
        Task<NotificationSummaryDTO> GetCoachSummaryAsync(Guid coachId, int take = 5);
        Task<NotificationSummaryDTO> GetAdminSummaryAsync(int take = 5);
    }

}
