using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.NortificationDTOs
{
    public class ChatNotifItemDTO
    {
        public Guid Id { get; set; }
        public string FromUserName { get; set; }
        public string Preview { get; set; }
        public DateTime SentAt { get; set; }
        public string Link { get; set; } // sohbet ekranına giden link
    }
}
