using FraoulaPT.DTOs.MediaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IMediaService : IBaseService<
    MediaListDTO,
    MediaDetailDTO,
    MediaCreateDTO,
    MediaUpdateDTO>
    { 
    
    }
}
