using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class WorkoutDayMap : BaseMap<WorkoutDay>
    {
        public override void Configure(EntityTypeBuilder<WorkoutDay> builder)
        {
            base.Configure(builder);

            builder.Property(d => d.DayOfWeek).IsRequired();
            builder.HasMany(d => d.Exercises)
                .WithOne(e => e.WorkoutDay)
                .HasForeignKey(e => e.WorkoutDayId);
        }
    }
}
