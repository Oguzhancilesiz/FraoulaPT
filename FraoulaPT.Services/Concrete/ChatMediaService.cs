using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ChatMediaDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class ChatMediaService : BaseService<
        ChatMedia,
        ChatMediaListDTO,
        ChatMediaDetailDTO,
        ChatMediaCreateDTO,
        ChatMediaUpdateDTO>, IChatMediaService
    {
        public ChatMediaService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
