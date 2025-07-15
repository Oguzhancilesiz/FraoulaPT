using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class ChatMedia : BaseEntity
    {
        public Guid ChatMessageId { get; set; }
        public ChatMessage ChatMessage { get; set; }

        public string MediaUrl { get; set; }
        public string MediaType { get; set; } // image, video, pdf, doc, vs.
        public long FileSize { get; set; } // byte olarak dosya boyutu
        public string FileName { get; set; }
    }
}
