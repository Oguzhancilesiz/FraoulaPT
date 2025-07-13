using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class MediaMap : BaseMap<Media>
    {
        public override void Configure(EntityTypeBuilder<Media> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Url).IsRequired().HasMaxLength(250);
            builder.Property(m => m.AltText).HasMaxLength(100);
            builder.Property(m => m.ThumbnailUrl).HasMaxLength(250);
            builder.Property(m => m.MediaType).IsRequired();
        }
    }
}
