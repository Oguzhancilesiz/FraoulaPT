using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ChatMessageDTO
{
    public class SendChatMessageDTO
    {
        public string MessageText { get; set; }
        public List<IFormFile> MediaFiles { get; set; }
    }
}
