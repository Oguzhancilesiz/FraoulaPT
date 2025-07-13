using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class PackageMap : BaseMap<Package>
    {
        public override void Configure(EntityTypeBuilder<Package> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Price).HasColumnType("decimal(10,2)");
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.Order).HasDefaultValue(0);
            builder.Property(x => x.ImageUrl).HasMaxLength(255);

            builder.HasMany(x => x.UserPackages)
                   .WithOne(x => x.Package)
                   .HasForeignKey(x => x.PackageId);
        }
    }
}
