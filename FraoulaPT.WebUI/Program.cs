using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Tokens;
using FraoulaPT.DAL;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace FraoulaPT.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // IEFContext’i DI Container’a kaydet
            builder.Services.AddScoped<IEFContext>(provider => provider.GetService<BaseContext>());

            builder.Services.AddSignalR();

            builder.Services.AddIdentity<AppUser, AppRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 3;
                option.Password.RequireDigit = false;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireUppercase = false;
                option.SignIn.RequireConfirmedEmail = true;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<BaseContext>().AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(2); // 2 dakika
            });

            //IOC -> Razor View Engine Dependency Injection yapmak için hangi interface hangi classla eþleþiyor buradan bilgi alýyor.
            builder.Services.AddScoped<IEFContext, BaseContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Application Service Registration
            builder.Services.AddScoped<IAppRoleService, AppRoleService>();
            builder.Services.AddScoped<IAppUserService, AppUserService>();
            builder.Services.AddScoped<IChatMediaService, ChatMediaService>();
            builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IExerciseCategoryService, ExerciseCategoryService>();
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IPackageService, PackageService>();
            builder.Services.AddScoped<IUserPackageService, UserPackageService>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IUserQuestionService, UserQuestionService>();
            builder.Services.AddScoped<IUserWeeklyFormService, UserWeeklyFormService>();
            builder.Services.AddScoped<IUserWorkoutAssignmentService, UserWorkoutAssignmentService>();
            builder.Services.AddScoped<IWorkoutDayService, WorkoutDayService>();
            builder.Services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();
            builder.Services.AddScoped<IWorkoutExerciseLogService, WorkoutExerciseLogService>();
            builder.Services.AddScoped<IWorkoutProgramService, WorkoutProgramService>();
            builder.Services.AddScoped<IMailService, MailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                       name: "areas",
                       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                     );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();


            app.Run();

        }
    }
}
