using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Tokens;
using FraoulaPT.DAL;
using FraoulaPT.Entity;
using FraoulaPT.Mapping;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Hubs;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FraoulaPT.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DI registration (Program.cs veya Startup.cs)
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
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<BaseContext>();

            //IOC -> Razor View Engine Dependency Injection yapmak için hangi interface hangi classla eþleþiyor buradan bilgi alýyor.
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEFContext, BaseContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IPackageService, PackageService>();
            builder.Services.AddScoped<IUserPackageService, UserPackageService>();
            builder.Services.AddScoped<IUserQuestionService, UserQuestionService>();
            builder.Services.AddScoped<IUserWeeklyFormService, UserWeeklyFormService>();
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IChatMediaService>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var uploadRoot = Path.Combine(env.WebRootPath, "uploads", "chat");
                return new ChatMediaService(uploadRoot);
            });

            builder.Services.AddScoped<IChatMessageService, ChatMessageService>();

            // ... diðer servislerin kayýtlarý

            builder.Services.Configure<PayTROptions>(builder.Configuration.GetSection("PayTR"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.MapHub<ChatHub>("/chathub");
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
