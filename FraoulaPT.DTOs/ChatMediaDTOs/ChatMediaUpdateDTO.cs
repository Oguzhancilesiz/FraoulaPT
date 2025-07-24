using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ChatMediaDTOs
{
    public class ChatMediaUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid ChatMessageId { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
        public long FileSize { get; set; }
        public string FileName { get; set; }
    }
}
