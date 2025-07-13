using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class ChatMessageMap : BaseMap<ChatMessage>
    {
        public override void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.MessageText).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.SentAt).IsRequired();
            builder.Property(m => m.IsRead).IsRequired();
        }
    }
}
