using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ChatMessageDTOs
{
    public class StudentChatListDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ProfilePhotoUrl { get; set; }
    }
}
