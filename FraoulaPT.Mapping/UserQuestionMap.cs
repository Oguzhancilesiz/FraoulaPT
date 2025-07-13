using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class UserQuestionMap : BaseMap<UserQuestion>
    {
        public override void Configure(EntityTypeBuilder<UserQuestion> builder)
        {
            base.Configure(builder);

            builder.Property(q => q.QuestionText).IsRequired().HasMaxLength(500);
            builder.Property(q => q.AnswerText).HasMaxLength(1000);
            builder.Property(q => q.AskedAt).IsRequired();
        }
    }
}
