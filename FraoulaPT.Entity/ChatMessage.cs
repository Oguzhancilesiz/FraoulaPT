using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class ChatMessage : BaseEntity
    {
        public Guid UserPackageId { get; set; }
        public UserPackage UserPackage { get; set; }
        public Guid SenderId { get; set; }
        public AppUser Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }

}
