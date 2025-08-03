using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraoulaPT.Entity
{
    public class VideoProgram : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string ThumbnailUrl { get; set; }

        public ProgramCategory Category { get; set; }

        public DifficultyLevel Difficulty { get; set; }

        public int DurationMinutes { get; set; } // Program süresi (dakika)

        public bool IsFeatured { get; set; }

        public bool IsInfluencerChoice { get; set; }

        [MaxLength(500)]
        public string? InfluencerComment { get; set; }

        public decimal Rating { get; set; } = 5.0m;

        public int ReviewCount { get; set; } = 0;

        [MaxLength(100)]
        public string Slug { get; set; }

        // Instructor bilgisi
        public Guid InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public AppUser Instructor { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }

        // Navigation properties
        public virtual ICollection<ProgramVideo> Videos { get; set; }
        public virtual ICollection<ProgramPurchase> Purchases { get; set; }
    }

    public class ProgramVideo : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public VideoProgram Program { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string VideoUrl { get; set; }

        public int OrderIndex { get; set; } // Video sırası

        public int DurationSeconds { get; set; } // Video süresi (saniye)

        [MaxLength(500)]
        public string? ThumbnailUrl { get; set; }

        public bool IsPreview { get; set; } = false; // Önizleme videosu mu?

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public class ProgramPurchase : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public Guid ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public VideoProgram Program { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        public DateTime? AccessExpiryDate { get; set; } // Erişim bitiş tarihi (opsiyonel)

        public bool IsActive { get; set; } = true;

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public enum ProgramCategory
    {
        Fitness = 1,
        Yoga = 2,
        Pilates = 3,
        Cardio = 4,
        Strength = 5,
        Dance = 6,
        Meditation = 7
    }

    public enum DifficultyLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3,
        Expert = 4
    }
}