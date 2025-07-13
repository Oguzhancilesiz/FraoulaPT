using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class UserProfileMap : BaseMap<UserProfile>
    {
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Gender).IsRequired(false);
            builder.Property(p => p.BirthDate).IsRequired(false);
            builder.Property(p => p.BloodType).HasMaxLength(10);
            builder.Property(p => p.PhoneNumber).HasMaxLength(20);
            builder.Property(p => p.Address).HasMaxLength(250);
            builder.Property(p => p.Occupation).HasMaxLength(100);
        }
    }
}
