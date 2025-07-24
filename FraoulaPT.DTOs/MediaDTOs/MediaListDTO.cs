using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.MediaDTOs
{
    public class MediaListDTO
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public FraoulaPT.Core.Enums.MediaType MediaType { get; set; }
    }

}
