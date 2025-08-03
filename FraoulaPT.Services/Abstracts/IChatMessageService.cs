using FraoulaPT.DTOs.ChatMessageDTOs;
using FraoulaPT.DTOs.DashboardDTOs;
using FraoulaPT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IChatMessageService
    {
        Task<ChatMessage> CreateAsync(Guid senderId, Guid receiverId, string messageText, Guid? userPackageId);
        Task<List<ChatMessage>> GetChatHistoryAsync(Guid senderId, Guid receiverId);
        Task<List<StudentChatListDTO>> GetStudentsWhoMessagedCoachAsync(Guid coachId);


        //dashboard için
        Task<int> GetActiveChatsCountAsync();
        Task<string> GetAverageResponseTimeAsync();
        Task<List<UserActivityDTO>> GetRecentActivitiesAsync(int limit);
    }

}
