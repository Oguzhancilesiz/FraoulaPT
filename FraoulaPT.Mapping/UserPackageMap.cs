using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class UserPackageMap : BaseMap<UserPackage>
    {
        public override void Configure(EntityTypeBuilder<UserPackage> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.AppUser)
                   .WithMany(x => x.UserPackages)
                   .HasForeignKey(x => x.AppUserId);

            builder.HasOne(x => x.Package)
                   .WithMany(x => x.UserPackages)
                   .HasForeignKey(x => x.PackageId);

            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
