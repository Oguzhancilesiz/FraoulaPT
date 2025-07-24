using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ChatMessageDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class ChatMessageService : BaseService<
    ChatMessage,
    ChatMessageListDTO,
    ChatMessageDetailDTO,
    ChatMessageCreateDTO,
    ChatMessageUpdateDTO>, IChatMessageService
    {
        public ChatMessageService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
