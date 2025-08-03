using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.Core.Tokens;
using FraoulaPT.DAL;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Hubs;
using FraoulaPT.WebUI.Infrastructure.Auth;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
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

            // DbContext
            builder.Services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IEFContext>(provider => provider.GetService<BaseContext>());
            builder.Services.AddScoped<IEFContext, BaseContext>(); // Gerekliyse

            // Identity
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
            })
            .AddEntityFrameworkStores<BaseContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.EventsType = typeof(CustomRedirectHandler);
            });

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(2);
            });

            builder.Services.AddScoped<CustomRedirectHandler>();

            // SignalR + Custom UserIdProvider
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            // Service Registration
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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
            builder.Services.AddScoped<IWorkoutProgramService, WorkoutProgramService>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<IExtraPackageOptionService, ExtraPackageOptionService>();
            builder.Services.AddScoped<IExtraRightService, ExtraRightService>();

            var app = builder.Build();

            // SignalR Hub route
            app.MapHub<ChatHub>("/chathub");

            // Middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); // Giriþ kontrolü
            app.UseAuthorization();  // Rol/Yetki kontrolü

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
