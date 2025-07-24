using FraoulaPT.DTOs.ChatMessageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IChatMessageService : IBaseService<
    ChatMessageListDTO,
    ChatMessageDetailDTO,
    ChatMessageCreateDTO,
    ChatMessageUpdateDTO>
    { }

}
