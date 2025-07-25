using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.MediaDTOs
{
    public class MediaDTO
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
    }
}
