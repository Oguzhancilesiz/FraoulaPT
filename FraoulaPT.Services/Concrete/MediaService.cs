using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.MediaDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class MediaService : BaseService<
    Media,
    MediaListDTO,
    MediaDetailDTO,
    MediaCreateDTO,
    MediaUpdateDTO>, IMediaService
    {
        public MediaService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
