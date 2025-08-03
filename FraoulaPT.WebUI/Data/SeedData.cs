using FraoulaPT.Core.Enums;
using FraoulaPT.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FraoulaPT.DAL;

namespace FraoulaPT.WebUI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var context = serviceProvider.GetRequiredService<BaseContext>();

            // Rolleri oluştur
            string[] roles = { "Admin", "Coach", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole { Name = role });
                }
            }

            // Admin hesabı
            var adminUser = new AppUser
            {
                UserName = "admin@fraoula.com",
                Email = "admin@fraoula.com",
                FullName = "Admin User",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            if (await userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Coach hesabı
            var coachUser = new AppUser
            {
                UserName = "coach@fraoula.com",
                Email = "coach@fraoula.com",
                FullName = "John Coach",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            if (await userManager.FindByEmailAsync(coachUser.Email) == null)
            {
                var result = await userManager.CreateAsync(coachUser, "Coach123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(coachUser, "Coach");
                }
            }

            // Test User hesabı
            var testUser = new AppUser
            {
                UserName = "user@fraoula.com",
                Email = "user@fraoula.com",
                FullName = "Test User",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            if (await userManager.FindByEmailAsync(testUser.Email) == null)
            {
                var result = await userManager.CreateAsync(testUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser, "User");
                }
            }

            // Test paketleri oluştur
            if (!context.Packages.Any())
            {
                var packages = new List<Package>
                {
                    new Package
                    {
                        Name = "Başlangıç Paketi",
                        Price = 299.99m,
                        Description = "Fitness yolculuğunuza başlangıç paketi",
                        Features = "Haftalık form takibi, Temel antrenman programları, E-posta desteği",
                        ImageUrl = "/images/packages/basic.jpg",
                        HighlightColor = "#FF6B35"
                    },
                    new Package
                    {
                        Name = "Premium Paket",
                        Price = 599.99m,
                        Description = "Kapsamlı fitness deneyimi",
                        Features = "Günlük antrenman programları, Canlı sohbet desteği, Video analizi, Özel beslenme planı",
                        ImageUrl = "/images/packages/premium.jpg",
                        HighlightColor = "#FF8C42"
                    },
                    new Package
                    {
                        Name = "VIP Paket",
                        Price = 999.99m,
                        Description = "En üst düzey kişisel antrenör deneyimi",
                        Features = "7/24 destek, Özel egzersiz videoları, İlerleme raporları, Beslenme danışmanlığı",
                        ImageUrl = "/images/packages/vip.jpg",
                        HighlightColor = "#FFA500"
                    }
                };

                context.Packages.AddRange(packages);
                await context.SaveChangesAsync();
            }

            // Exercise kategorileri ve egzersizler mevcut entity'lerle çalışacak

            // Test ürünleri oluştur
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "FraoulaPT Signature Tişört",
                        Description = "Premium kalite pamuklu fitness tişörtü. Nefes alabilir kumaş teknolojisi.",
                        Price = 199.99m,
                        StockQuantity = 50,
                        ImageUrl = "/images/products/tshirt1.jpg",
                        Category = ProductCategory.Clothing,
                        IsFeatured = true,
                        IsInfluencerChoice = true,
                        InfluencerComment = "Antrenmanda mükemmel nefes alıyor! Favorim ⚡",
                        Slug = "fraoula-signature-tshirt",
                        Rating = 4.9m,
                        ReviewCount = 127
                    },
                    new Product
                    {
                        Name = "Premium Whey Protein 2kg",
                        Description = "Yüksek kaliteli whey protein. Kas gelişimi için ideal.",
                        Price = 299.99m,
                        StockQuantity = 30,
                        ImageUrl = "/images/products/whey.jpg",
                        Category = ProductCategory.Supplement,
                        IsFeatured = true,
                        IsInfluencerChoice = true,
                        InfluencerComment = "Kaslarım için vazgeçilmez! 💪",
                        Slug = "premium-whey-protein",
                        Rating = 4.8m,
                        ReviewCount = 89
                    },
                    new Product
                    {
                        Name = "FraoulaPT Hoodie",
                        Description = "Rahat kesim hoodie. Günlük kullanım için mükemmel.",
                        Price = 349.99m,
                        StockQuantity = 25,
                        ImageUrl = "/images/products/hoodie.jpg",
                        Category = ProductCategory.Clothing,
                        IsFeatured = true,
                        IsInfluencerChoice = true,
                        InfluencerComment = "Sokak stilim için mükemmel! 🔥",
                        Slug = "fraoula-hoodie",
                        Rating = 4.9m,
                        ReviewCount = 203
                    },
                    new Product
                    {
                        Name = "Pro Gym Eldivenleri",
                        Description = "Dayanıklı gym eldivenleri. Avuç içi koruması.",
                        Price = 149.99m,
                        StockQuantity = 40,
                        ImageUrl = "/images/products/gloves.jpg",
                        Category = ProductCategory.Accessory,
                        IsFeatured = false,
                        IsInfluencerChoice = false,
                        Slug = "pro-gym-gloves",
                        Rating = 4.6m,
                        ReviewCount = 45
                    },
                    new Product
                    {
                        Name = "BCAA Energy Drink",
                        Description = "Antrenman öncesi enerji içeceği. BCAA içerikli.",
                        Price = 89.99m,
                        StockQuantity = 60,
                        ImageUrl = "/images/products/bcaa.jpg",
                        Category = ProductCategory.Supplement,
                        IsFeatured = false,
                        IsInfluencerChoice = false,
                        Slug = "bcaa-energy-drink",
                        Rating = 4.4m,
                        ReviewCount = 67
                    },
                    new Product
                    {
                        Name = "Resistance Bands Set",
                        Description = "Ev antrenmanları için direnç lastiği seti.",
                        Price = 179.99m,
                        StockQuantity = 35,
                        ImageUrl = "/images/products/bands.jpg",
                        Category = ProductCategory.Equipment,
                        IsFeatured = true,
                        IsInfluencerChoice = true,
                        InfluencerComment = "Evde antrenman için harika! 🏠",
                        Slug = "resistance-bands-set",
                        Rating = 4.7m,
                        ReviewCount = 156
                    }
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
} 