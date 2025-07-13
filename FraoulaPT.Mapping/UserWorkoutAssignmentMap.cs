using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class UserWorkoutAssignmentMap : BaseMap<UserWorkoutAssignment>
    {
        public override void Configure(EntityTypeBuilder<UserWorkoutAssignment> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.AssignedDate).IsRequired();
            builder.Property(a => a.CoachNote).HasMaxLength(500);
        }
    }
}
