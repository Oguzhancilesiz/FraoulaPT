using FraoulaPT.DTOs.ChatMediaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IChatMediaService : IBaseService<
     ChatMediaListDTO,
     ChatMediaDetailDTO,
     ChatMediaCreateDTO,
     ChatMediaUpdateDTO>
    { }

}
