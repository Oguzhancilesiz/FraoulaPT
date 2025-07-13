using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class WorkoutExerciseMap : BaseMap<WorkoutExercise>
    {
        public override void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.ExerciseName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.SetCount).IsRequired();
            builder.Property(e => e.RepCount).IsRequired();
            builder.Property(e => e.DurationSeconds).IsRequired(false);
            builder.Property(e => e.Notes).HasMaxLength(500);
        }
    }
}
