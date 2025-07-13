using FraoulaPT.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Mapping
{
    public class AppUserMap : BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.FullName).HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.HasOne(u => u.Profile)
                .WithOne(p => p.AppUser)
                .HasForeignKey<UserProfile>(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserPackages)
                .WithOne(up => up.AppUser)
                .HasForeignKey(up => up.AppUserId);

            builder.HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.AskedQuestions)
                .WithOne(q => q.AskedByUser)
                .HasForeignKey(q => q.AskedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.AnsweredQuestions)
                .WithOne(q => q.AnsweredByCoach)
                .HasForeignKey(q => q.AnsweredByCoachId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.AuthoredPrograms)
                .WithOne(p => p.Coach)
                .HasForeignKey(p => p.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
