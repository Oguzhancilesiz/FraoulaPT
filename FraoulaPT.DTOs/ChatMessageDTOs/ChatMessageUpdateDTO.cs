using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ChatMessageDTOs
{
    public class ChatMessageUpdateDTO
    {
        public Guid Id { get; set; }
        public string MessageText { get; set; }
        public bool IsRead { get; set; }
    }

}
