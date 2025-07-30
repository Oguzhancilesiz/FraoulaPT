using FraoulaPT.Core.Enums;
using FraoulaPT.Entity;

public class Package : BaseEntity
{
    public string Name { get; set; } // Gold, Pro, Platinum, vb.
    public PackageType PackageType { get; set; } // Enum: Gold, Pro, ...
    public SubscriptionPeriod SubscriptionPeriod { get; set; } // Enum: Monthly, Yearly, etc.

    public int MaxQuestionsPerPeriod { get; set; } = 0;
    public int MaxMessagesPerPeriod { get; set; } = 0;

    public decimal Price { get; set; }

    public string Description { get; set; }
    public string Features { get; set; } // JSON / Markdown destekli açıklamalar
    public string ImageUrl { get; set; }
    public string HighlightColor { get; set; }

    public int Order { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    // ⬇️ Navigation
    public ICollection<UserPackage> UserPackages { get; set; }
}