using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.NortificationDTOs
{
    public class QuestionNotifItemDTO
    {
        public Guid Id { get; set; }
        public string FromUserName { get; set; }
        public string Preview { get; set; }
        public DateTime AskedAt { get; set; }
        public string Link { get; set; } // detay sayfasına giden link
    }
}
