using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class UserWeeklyFormMap : BaseMap<UserWeeklyForm>
    {
        public override void Configure(EntityTypeBuilder<UserWeeklyForm> builder)
        {
            base.Configure(builder);

            builder.Property(f => f.FormDate).IsRequired();
            builder.Property(f => f.UserNote).HasMaxLength(500);
            builder.Property(f => f.CoachFeedback).HasMaxLength(500);
            builder.Property(f => f.ProgressPhotoPath).HasMaxLength(250);
        }
    }
}
