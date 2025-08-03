using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraoulaPT.Entity
{
    public class SportsGroup : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public GroupType GroupType { get; set; }

        public ExperienceLevel TargetLevel { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public int MaxMembers { get; set; } = 50;

        public int CurrentMemberCount { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public bool IsPrivate { get; set; } = false;

        [MaxLength(500)]
        public string TelegramGroupLink { get; set; }

        [MaxLength(100)]
        public string TelegramGroupId { get; set; }

        // Coach/Trainer
        public Guid CoachId { get; set; }

        [ForeignKey("CoachId")]
        public AppUser Coach { get; set; }

        // Goals and Focus
        public GroupFocus PrimaryFocus { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

        [MaxLength(1000)]
        public string GroupRules { get; set; }

        [MaxLength(500)]
        public string WeeklySchedule { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }

        // Navigation properties
        public virtual ICollection<GroupMembership> Memberships { get; set; }
        public virtual ICollection<DevelopmentPackage> DevelopmentPackages { get; set; }
        public virtual ICollection<GroupAssignment> Assignments { get; set; }
        public virtual ICollection<GroupProgress> ProgressReports { get; set; }
    }

    public class GroupMembership : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public SportsGroup Group { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.Now;

        public MembershipRole Role { get; set; } = MembershipRole.Member;

        public bool IsActive { get; set; } = true;

        [MaxLength(200)]
        public string TelegramUsername { get; set; }

        [MaxLength(500)]
        public string PersonalGoals { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        // Progress tracking
        public decimal InitialWeight { get; set; }
        public decimal CurrentWeight { get; set; }
        public decimal TargetWeight { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public enum GroupType
    {
        FitnessGroup = 1,       // Fitness Grubu
        MotorClub = 2,          // Motor Kulübü
        HybridGroup = 3,        // Hibrit (Motor + Fitness)
        SpecialTraining = 4,    // Özel Antrenman
        BeginnerGroup = 5,      // Başlangıç Grubu
        AdvancedGroup = 6       // İleri Seviye
    }

    public enum GroupFocus
    {
        WeightLoss = 1,         // Kilo Verme
        MuscleGain = 2,         // Kas Kazanma
        Endurance = 3,          // Dayanıklılık
        Strength = 4,           // Güç
        Flexibility = 5,        // Esneklik
        MotorSkills = 6,        // Motor Becerileri
        GeneralFitness = 7      // Genel Fitness
    }

    public enum MembershipRole
    {
        Member = 1,             // Üye
        Moderator = 2,          // Moderatör
        Assistant = 3,          // Asistan
        Coach = 4               // Antrenör
    }
}