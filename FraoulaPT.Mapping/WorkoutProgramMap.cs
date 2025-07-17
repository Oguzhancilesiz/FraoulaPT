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

        }
    }
}
