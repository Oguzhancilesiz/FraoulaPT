using FraoulaPT.Core.Abstracts;
using FraoulaPT.Entity;
using FraoulaPT.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DAL
{
    public class BaseContext : IdentityDbContext<AppUser, AppRole, Guid>, IEFContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {

        }
        public override DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        
        async Task<int> IEFContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            string id = Guid.NewGuid().ToString().Replace("-", "") + DateTime.Now.ToBinary();//Log veritabanları için genelde kullanılan işlemID(proccessId) kolonuna eklenecek veriyi temsil eder.

            //HttpContext.User.Identity.Name
            //HttpContext.User.Claims.GetClaims()

            DateTime now = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added | entry.State == EntityState.Modified | entry.State == EntityState.Deleted)
                {
                    entry.Entity.ModifiedDate = now;
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedDate = now;
                        entry.Entity.Status = Core.Enums.Status.Active;
                    }
                }
            }

            int rowCount = 0;

            try
            {
                rowCount = await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return rowCount;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mapping İşlemleri
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseMap<IEntity>).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ChatMedia> ChatMedias { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<UserPackage> UserPackages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserQuestion> UserQuestions { get; set; }
        public DbSet<UserWeeklyForm> UserWeeklyForms { get; set; }
        public DbSet<UserWorkoutAssignment> UserWorkoutAssignments { get; set; }
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<WorkoutFeedback> WorkoutFeedbacks { get; set; }
        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
        public DbSet<ExtraPackageOption> ExtraPackageOptions { get; set; }
        public DbSet<ExtraPackageUsage> ExtraPackageUsages { get; set; }
        public DbSet<ExtraRight> ExtraRights { get; set; }
        
        // E-ticaret tabloları
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingRate> ShippingRates { get; set; }
        
        // Video programları
        public DbSet<VideoProgram> VideoPrograms { get; set; }
        public DbSet<ProgramVideo> ProgramVideos { get; set; }
        public DbSet<ProgramPurchase> ProgramPurchases { get; set; }
        
        // Mesajlaşma
        public DbSet<AdminMessage> AdminMessages { get; set; }
        
        // Sporcu Grupları ve Gelişim Paketleri
        public DbSet<SportsGroup> SportsGroups { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }
        public DbSet<DevelopmentPackage> DevelopmentPackages { get; set; }
        public DbSet<PackagePurchase> PackagePurchases { get; set; }
        public DbSet<PackageContent> PackageContents { get; set; }
        public DbSet<GroupAssignment> GroupAssignments { get; set; }
        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }
        public DbSet<GroupProgress> GroupProgresses { get; set; }
        public DbSet<PackageReview> PackageReviews { get; set; }





    }
}
