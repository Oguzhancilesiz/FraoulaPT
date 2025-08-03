using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraoulaPT.Entity
{
    public class AdminMessage : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        public Guid SenderId { get; set; }

        [ForeignKey("SenderId")]
        public AppUser Sender { get; set; }

        public Guid ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public AppUser Receiver { get; set; }

        public MessageType MessageType { get; set; } = MessageType.General;

        public MessagePriority Priority { get; set; } = MessagePriority.Normal;

        public bool IsRead { get; set; } = false;

        public DateTime? ReadDate { get; set; }

        public bool IsAdminMessage { get; set; } // Admin'den mi gönderildi?

        [MaxLength(500)]
        public string? AttachmentUrl { get; set; }

        public Guid? ParentMessageId { get; set; } // Yanıtlanan mesaj ID'si

        [ForeignKey("ParentMessageId")]
        public AdminMessage? ParentMessage { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public enum MessageType
    {
        General = 1,
        Support = 2,
        Complaint = 3,
        Suggestion = 4,
        Order = 5,
        Payment = 6,
        Program = 7
    }

    public enum MessagePriority
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Urgent = 4
    }
}