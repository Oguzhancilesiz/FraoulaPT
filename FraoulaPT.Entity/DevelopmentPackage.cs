using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraoulaPT.Entity
{
    public class DevelopmentPackage : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public PackageType PackageType { get; set; }

        public int DurationDays { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        // Group restriction - hangi gruplara özel
        public Guid? TargetGroupId { get; set; }

        [ForeignKey("TargetGroupId")]
        public SportsGroup? TargetGroup { get; set; }

        public ExperienceLevel TargetLevel { get; set; }

        public GroupFocus PrimaryFocus { get; set; }

        // Package contents
        [MaxLength(2000)]
        public string WhatIncludes { get; set; } // JSON array olarak da kullanılabilir

        public int VideoCount { get; set; } = 0;

        public int WorkoutCount { get; set; } = 0;

        public bool IncludesNutrition { get; set; } = false;

        public bool IncludesPersonalSupport { get; set; } = false;

        public bool IncludesTelegramAccess { get; set; } = true;

        // Telegram integration
        [MaxLength(500)]
        public string TelegramChannelLink { get; set; }

        [MaxLength(100)]
        public string TelegramChannelId { get; set; }

        // Pricing options
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPrice { get; set; }

        public DateTime? DiscountValidUntil { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? GroupDiscountPrice { get; set; } // Grup indirimi

        public int MinGroupSize { get; set; } = 1; // Grup indirimi için minimum kişi

        // Success metrics
        public decimal Rating { get; set; } = 5.0m;

        public int ReviewCount { get; set; } = 0;

        public int SoldCount { get; set; } = 0;

        public int ActiveSubscriptions { get; set; } = 0;

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }

        // Navigation properties
        public virtual ICollection<PackagePurchase> Purchases { get; set; }
        public virtual ICollection<PackageContent> Contents { get; set; }
        public virtual ICollection<PackageReview> Reviews { get; set; }
    }

    public class PackagePurchase : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PackageId { get; set; }

        [ForeignKey("PackageId")]
        public DevelopmentPackage Package { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public Guid? GroupId { get; set; }

        [ForeignKey("GroupId")]
        public SportsGroup? Group { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public PurchaseStatus PurchaseStatus { get; set; } = PurchaseStatus.Active;

        // Telegram access
        public bool TelegramAccessGranted { get; set; } = false;

        public DateTime? TelegramJoinDate { get; set; }

        [MaxLength(200)]
        public string TelegramUsername { get; set; }

        // Progress tracking
        public decimal ProgressPercentage { get; set; } = 0;

        public int CompletedWorkouts { get; set; } = 0;

        public int TotalWorkouts { get; set; } = 0;

        [MaxLength(1000)]
        public string Notes { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public class PackageContent : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PackageId { get; set; }

        [ForeignKey("PackageId")]
        public DevelopmentPackage Package { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public ContentType ContentType { get; set; }

        [MaxLength(500)]
        public string ContentUrl { get; set; } // Video, PDF, etc.

        public int OrderIndex { get; set; }

        public int? DurationMinutes { get; set; }

        public bool IsRequired { get; set; } = true;

        public bool IsUnlocked { get; set; } = false; // Progressively unlock

        public int UnlockAfterDays { get; set; } = 0;

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public class GroupAssignment : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public SportsGroup Group { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public AssignmentType AssignmentType { get; set; }

        public bool IsRequired { get; set; } = true;

        public int Points { get; set; } = 10;

        [MaxLength(500)]
        public string ResourceUrl { get; set; }

        // Telegram integration
        public bool SendToTelegram { get; set; } = true;

        public bool TelegramSent { get; set; } = false;

        public DateTime? TelegramSentDate { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }

        // Navigation properties
        public virtual ICollection<AssignmentSubmission> Submissions { get; set; }
    }

    public class AssignmentSubmission : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AssignmentId { get; set; }

        [ForeignKey("AssignmentId")]
        public GroupAssignment Assignment { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        [MaxLength(2000)]
        public string SubmissionText { get; set; }

        [MaxLength(500)]
        public string FileUrl { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        public SubmissionStatus SubmissionStatus { get; set; } = SubmissionStatus.Submitted;

        public int? Score { get; set; }

        [MaxLength(1000)]
        public string Feedback { get; set; }

        public DateTime? ReviewedDate { get; set; }

        public Guid? ReviewedByUserId { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public class GroupProgress : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public SportsGroup Group { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public DateTime ReportDate { get; set; } = DateTime.Now;

        // Physical measurements
        public decimal? Weight { get; set; }

        public decimal? BodyFatPercentage { get; set; }

        public decimal? MusclePercentage { get; set; }

        // Performance metrics
        public int? WorkoutsCompleted { get; set; }

        public int? CaloriesBurned { get; set; }

        public int? MinutesExercised { get; set; }

        // Self assessment
        public int EnergyLevel { get; set; } = 5; // 1-10

        public int MotivationLevel { get; set; } = 5; // 1-10

        public int SatisfactionLevel { get; set; } = 5; // 1-10

        [MaxLength(1000)]
        public string Notes { get; set; }

        [MaxLength(500)]
        public string PhotoUrl { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public class PackageReview : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PackageId { get; set; }

        [ForeignKey("PackageId")]
        public DevelopmentPackage Package { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public int Rating { get; set; } // 1-5

        [MaxLength(1000)]
        public string Comment { get; set; }

        public bool IsRecommended { get; set; } = true;

        public bool IsApproved { get; set; } = false;

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public enum ContentType
    {
        Video = 1,              // Video
        Document = 2,           // Döküman
        Audio = 3,              // Ses
        Workout = 4,            // Antrenman
        Recipe = 5,             // Tarif
        Assignment = 6          // Ödev
    }

    public enum PurchaseStatus
    {
        Active = 1,             // Aktif
        Completed = 2,          // Tamamlandı
        Cancelled = 3,          // İptal Edildi
        Expired = 4,            // Süresi Doldu
        Paused = 5              // Durduruldu
    }

    public enum AssignmentType
    {
        Workout = 1,            // Antrenman
        Photo = 2,              // Fotoğraf
        Measurement = 3,        // Ölçüm
        Nutrition = 4,          // Beslenme
        Video = 5,              // Video
        Text = 6                // Metin
    }

    public enum SubmissionStatus
    {
        Submitted = 1,          // Gönderildi
        Reviewed = 2,           // İncelendi
        Approved = 3,           // Onaylandı
        Rejected = 4,           // Reddedildi
        NeedsRevision = 5       // Düzeltme Gerekli
    }
}