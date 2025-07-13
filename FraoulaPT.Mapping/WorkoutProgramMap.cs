using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class WorkoutProgramMap : BaseMap<WorkoutProgram>
    {
        public override void Configure(EntityTypeBuilder<WorkoutProgram> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.HasMany(p => p.WorkoutDays)
                .WithOne(d => d.WorkoutProgram)
                .HasForeignKey(d => d.WorkoutProgramId);

            builder.HasMany(p => p.Assignments)
                .WithOne(a => a.WorkoutProgram)
                .HasForeignKey(a => a.WorkoutProgramId);
        }
    }
}
