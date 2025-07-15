using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ChatMessageDTO
{
    public class ChatMediaDTO
    {
        public string MediaUrl { get; set; }
        public string MediaType { get; set; } // image, video, pdf, doc
        public long FileSize { get; set; }
        public string FileName { get; set; }
    }
}
